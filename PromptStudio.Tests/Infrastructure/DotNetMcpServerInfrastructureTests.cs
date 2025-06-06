using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Infrastructure;

/// <summary>
/// Tests .NET PromptStudio.Mcp server infrastructure and setup
/// Converted from test_mcp_native.ps1, test_mcp_final.ps1, and test_refactored_mcp.ps1
/// </summary>
public class DotNetMcpServerInfrastructureTests : IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly HttpClient _httpClient;
    private Process? _mcpServerProcess;

    public DotNetMcpServerInfrastructureTests(ITestOutputHelper output)
    {
        _output = output;
        _httpClient = new HttpClient();
    }

    [Fact]
    public async Task PromptStudio_Should_BeRunning_For_DotNetMcpServer()
    {
        // Test if PromptStudio is running on localhost:5131 (standard for .NET MCP server tests)
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5131/api/prompts/collections");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var collections = JsonSerializer.Deserialize<JsonElement[]>(content);
            
            _output.WriteLine("✅ PromptStudio is running and responding");
            _output.WriteLine($"Found {collections!.Length} collections");
        }
        catch (HttpRequestException ex)
        {
            _output.WriteLine("❌ PromptStudio is not running or not responding");
            _output.WriteLine($"Error: {ex.Message}");
            _output.WriteLine("Please make sure PromptStudio is running on http://localhost:5131");
            throw new InvalidOperationException("PromptStudio must be running on localhost:5131 for .NET MCP server tests");
        }
    }

    [Fact]
    public void PromptStudioMcpServer_Should_BuildSuccessfully()
    {
        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "PromptStudioMcpServer");
        
        _output.WriteLine("Building .NET MCP server...");
        
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "build --verbosity quiet",
            WorkingDirectory = mcpServerPath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        });

        process!.WaitForExit();
        
        if (process.ExitCode != 0)
        {
            var error = process.StandardError.ReadToEnd();
            _output.WriteLine($"Build failed: {error}");
            throw new InvalidOperationException("Failed to build PromptStudioMcpServer");
        }

        _output.WriteLine("✅ .NET MCP server built successfully!");
    }

    [Fact]
    public void PromptStudioMcp_Should_BuildSuccessfully()
    {
        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "PromptStudio.Mcp");
        
        _output.WriteLine("Building PromptStudio.Mcp...");
        
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "build --verbosity quiet",
            WorkingDirectory = mcpServerPath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        });

        process!.WaitForExit();
        
        if (process.ExitCode != 0)
        {
            var error = process.StandardError.ReadToEnd();
            _output.WriteLine($"Build failed: {error}");
            throw new InvalidOperationException("Failed to build PromptStudio.Mcp");
        }

        _output.WriteLine("✅ PromptStudio.Mcp built successfully!");
    }

    [Fact]
    public async Task PromptApiController_Endpoints_Should_BeAccessible()
    {
        // Test specific API endpoints that the .NET MCP server will use
        _output.WriteLine("Testing PromptApiController endpoints...");

        var endpoints = new[]
        {
            new { Name = "Get Collections", Url = "http://localhost:5131/api/prompts/collections" },
            new { Name = "Get Prompts", Url = "http://localhost:5131/api/prompts/prompts" },
            new { Name = "Get Executions", Url = "http://localhost:5131/api/prompts/executions" },
            new { Name = "Get Variable Collections", Url = "http://localhost:5131/api/prompts/variable-collections?promptId=1" }
        };

        foreach (var endpoint in endpoints)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint.Url);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                _output.WriteLine($"✅ {endpoint.Name}: Working");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"❌ {endpoint.Name}: Failed - {ex.Message}");
                throw new InvalidOperationException($"API endpoint {endpoint.Name} failed", ex);
            }
        }
    }

    [Fact]
    public async Task DotNetMcpServer_Should_RespondToToolsListRequest()
    {
        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "PromptStudioMcpServer");
        
        // Ensure server is built
        if (!Directory.Exists(Path.Combine(mcpServerPath, "bin")))
        {
            PromptStudioMcpServer_Should_BuildSuccessfully();
        }

        var toolsListRequest = @"{""jsonrpc"":""2.0"",""id"":1,""method"":""tools/list"",""params"":{}}";

        var processInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "run",
            WorkingDirectory = mcpServerPath,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo);
        process.Should().NotBeNull();

        // Send request
        await process!.StandardInput.WriteLineAsync(toolsListRequest);
        process.StandardInput.Close();

        // Wait for response with timeout
        var outputTask = process.StandardOutput.ReadToEndAsync();
        var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));

        var completedTask = await Task.WhenAny(outputTask, timeoutTask);
        
        if (completedTask == timeoutTask)
        {
            process.Kill();
            throw new TimeoutException(".NET MCP server did not respond within 10 seconds");
        }

        var output = await outputTask;
        
        // Filter out just the JSON response
        var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var jsonResponse = lines.FirstOrDefault(line => line.Trim().StartsWith("{"));

        jsonResponse.Should().NotBeNull(".NET MCP server should return a JSON response");
        jsonResponse.Should().Contain("tools", ".NET MCP server should return tools list");
        
        _output.WriteLine("✅ .NET MCP server responded to tools/list request");
        _output.WriteLine($"Response: {jsonResponse}");
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
        _mcpServerProcess?.Kill();
        _mcpServerProcess?.Dispose();
    }
}
