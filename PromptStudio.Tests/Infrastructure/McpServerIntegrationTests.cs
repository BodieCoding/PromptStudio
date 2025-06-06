using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Net;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Infrastructure;

/// <summary>
/// Tests Node.js MCP server integration setup and configuration
/// Converted from test-mcp-integration.ps1, test-mcp-simple.ps1 and related PowerShell scripts
/// </summary>
public class NodeJsMcpServerIntegrationTests : IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly HttpClient _httpClient;
    private Process? _mcpServerProcess;

    public NodeJsMcpServerIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
        _httpClient = new HttpClient();
    }

    [Fact]
    public async Task PromptStudio_Should_BeAccessible()
    {
        // Test if PromptStudio is running on localhost:5131
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5131");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            _output.WriteLine("✅ PromptStudio is running on localhost:5131");
        }
        catch (HttpRequestException)
        {
            _output.WriteLine("❌ PromptStudio not accessible on localhost:5131");
            throw new InvalidOperationException("PromptStudio must be running on localhost:5131 before running these tests");
        }
    }

    [Fact]
    public void NodeJs_Should_BeInstalled()
    {
        // Check if Node.js is installed
        try
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "node",
                Arguments = "--version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            process!.WaitForExit();
            var version = process.StandardOutput.ReadToEnd().Trim();
            
            process.ExitCode.Should().Be(0);
            version.Should().StartWith("v");
            _output.WriteLine($"✅ Node.js: {version}");
        }
        catch (Exception ex)
        {
            _output.WriteLine("❌ Node.js not found. Please install Node.js");
            throw new InvalidOperationException("Node.js is required", ex);
        }
    }    [Fact]
    public async Task NodeJsMcpServer_Should_BuildSuccessfully()
    {
        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "mcp-server");
        var distPath = Path.Combine(mcpServerPath, "dist", "index.js");

        // Check if dependencies are installed
        var nodeModulesPath = Path.Combine(mcpServerPath, "node_modules");
        if (!Directory.Exists(nodeModulesPath))
        {
            _output.WriteLine("📦 Installing MCP server dependencies...");
            InstallNpmDependencies(mcpServerPath);
        }

        // Build MCP server
        _output.WriteLine("🔨 Building MCP server...");
        BuildMcpServer(mcpServerPath);        // Verify build output
        File.Exists(distPath).Should().BeTrue("Node.js MCP server should build successfully");
        _output.WriteLine("✅ Node.js MCP server built successfully");
    }    [Fact]
    public async Task NodeJsMcpServer_Should_RespondToToolsListRequest()
    {
        // Set environment variable
        Environment.SetEnvironmentVariable("PROMPTSTUDIO_URL", "http://localhost:5131");

        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "mcp-server");
        var distPath = Path.Combine(mcpServerPath, "dist", "index.js");        // Ensure server is built
        if (!File.Exists(distPath))
        {
            await BuildMcpServerAsync(mcpServerPath);
        }

        // Test tools/list request
        var toolsRequest = @"{""jsonrpc"":""2.0"",""id"":2,""method"":""tools/list"",""params"":{}}";
        
        var processInfo = new ProcessStartInfo
        {
            FileName = "node",
            Arguments = $"\"{distPath}\"",
            WorkingDirectory = mcpServerPath,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        processInfo.Environment["PROMPTSTUDIO_URL"] = "http://localhost:5131";

        using var process = Process.Start(processInfo);
        process.Should().NotBeNull();

        // Send request
        await process!.StandardInput.WriteLineAsync(toolsRequest);
        process.StandardInput.Close();

        // Wait for response
        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();        output.Should().Contain("tools", "Node.js MCP server should return tools list");
        _output.WriteLine("✅ Node.js MCP server lists tools correctly");
    }

    private void InstallNpmDependencies(string mcpServerPath)
    {
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "npm",
            Arguments = "install",
            WorkingDirectory = mcpServerPath,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        process!.WaitForExit();
        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException("Failed to install npm dependencies");
        }
    }    private async Task BuildMcpServerAsync(string mcpServerPath)
    {
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "npm",
            Arguments = "run build",
            WorkingDirectory = mcpServerPath,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        await process!.WaitForExitAsync();
        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException("Failed to build Node.js MCP server");
        }
    }

    private void BuildMcpServer(string mcpServerPath)
    {
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "npm",
            Arguments = "run build",
            WorkingDirectory = mcpServerPath,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        process!.WaitForExit();
        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException("Failed to build Node.js MCP server");
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
        _mcpServerProcess?.Kill();
        _mcpServerProcess?.Dispose();
    }
}
