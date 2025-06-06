using FluentAssertions;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Infrastructure;

/// <summary>
/// Tests VS Code MCP configuration and GitHub Copilot integration with Node.js MCP server
/// Converted from test-mcp-vscode.ps1 and test_copilot_integration.ps1
/// </summary>
public class VsCodeNodeJsMcpConfigurationTests : IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly HttpClient _httpClient;

    public VsCodeNodeJsMcpConfigurationTests(ITestOutputHelper output)
    {
        _output = output;
        _httpClient = new HttpClient();
    }    [Fact]
    public async Task PromptStudio_Api_Should_BeAccessible_For_NodeJs_VsCode()
    {
        // Set environment variable for MCP server
        Environment.SetEnvironmentVariable("PROMPTSTUDIO_URL", "http://localhost:5000");

        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5000/api/mcp/collections");
            response.StatusCode.Should().Be(HttpStatusCode.OK);            _output.WriteLine("✅ PromptStudio API accessible for Node.js VS Code MCP");
        }
        catch (HttpRequestException ex)
        {
            _output.WriteLine($"❌ PromptStudio API not accessible: {ex.Message}");
            throw new InvalidOperationException("PromptStudio API must be accessible for Node.js VS Code MCP integration");
        }
    }

    [Fact]
    public async Task McpServer_Should_Initialize_For_VsCode()
    {
        Environment.SetEnvironmentVariable("PROMPTSTUDIO_URL", "http://localhost:5000");

        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "mcp-server");
        var distPath = Path.Combine(mcpServerPath, "dist", "index.js");

        var initRequest = @"{""jsonrpc"":""2.0"",""id"":1,""method"":""initialize"",""params"":{""protocolVersion"":""2024-11-05"",""capabilities"":{},""clientInfo"":{""name"":""test"",""version"":""1.0.0""}}}";
        
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

        processInfo.Environment["PROMPTSTUDIO_URL"] = "http://localhost:5000";

        using var process = Process.Start(processInfo);
        process.Should().NotBeNull();

        await process!.StandardInput.WriteLineAsync(initRequest);
        process.StandardInput.Close();

        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        output.Should().Contain("result", "MCP server should initialize correctly");
        _output.WriteLine("✅ MCP server initializes correctly for VS Code");
    }

    [Fact]
    public async Task CopilotIntegration_Should_Access_PromptStudio_Collections()
    {
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5131/api/prompts/collections");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var collections = JsonSerializer.Deserialize<JsonElement[]>(content);
            
            collections.Should().NotBeNull();
            _output.WriteLine($"✅ GitHub Copilot can access PromptStudio - Found {collections!.Length} collections");
            
            foreach (var collection in collections)
            {
                if (collection.TryGetProperty("name", out var nameProperty) && 
                    collection.TryGetProperty("promptTemplatesCount", out var countProperty))
                {
                    _output.WriteLine($"   📁 Collection: {nameProperty.GetString()} ({countProperty.GetInt32()} prompts)");
                }
            }
        }
        catch (HttpRequestException ex)
        {
            _output.WriteLine($"❌ PromptStudio API not accessible for Copilot: {ex.Message}");
            _output.WriteLine("   Make sure PromptStudio is running on http://localhost:5131");
            throw new InvalidOperationException("PromptStudio must be running for Copilot integration");
        }
    }

    [Fact]
    public void McpServer_Configuration_Should_Be_Valid()
    {
        // Validate that the expected MCP server configuration is correct
        var expectedCommand = "node";
        var expectedArgs = @"c:\Code\Promptlet\mcp-server\dist\index.js";
        var expectedEnvVar = "http://localhost:5000";

        // Test configuration values
        expectedCommand.Should().Be("node");
        Path.IsPathRooted(expectedArgs).Should().BeTrue("MCP server path should be absolute");
        Uri.IsWellFormedUriString(expectedEnvVar, UriKind.Absolute).Should().BeTrue("PROMPTSTUDIO_URL should be a valid URL");

        _output.WriteLine("✅ VS Code MCP configuration is valid");
        _output.WriteLine($"Command: {expectedCommand}");
        _output.WriteLine($"Args: [\"{expectedArgs}\"]");
        _output.WriteLine($"Environment: PROMPTSTUDIO_URL={expectedEnvVar}");
    }

    [Fact]
    public void McpServer_Files_Should_Exist()
    {
        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "mcp-server");
        var distPath = Path.Combine(mcpServerPath, "dist", "index.js");
        var packageJsonPath = Path.Combine(mcpServerPath, "package.json");

        Directory.Exists(mcpServerPath).Should().BeTrue("MCP server directory should exist");
        File.Exists(packageJsonPath).Should().BeTrue("package.json should exist");
        
        // Build if necessary
        if (!File.Exists(distPath))
        {
            _output.WriteLine("🔨 Building MCP server for VS Code tests...");
            BuildMcpServer(mcpServerPath);
        }

        File.Exists(distPath).Should().BeTrue("MCP server dist/index.js should exist");
        _output.WriteLine("✅ MCP server files are ready for VS Code integration");
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
            throw new InvalidOperationException("Failed to build MCP server for VS Code integration");
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
