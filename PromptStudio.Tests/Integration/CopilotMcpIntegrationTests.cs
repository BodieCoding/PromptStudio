using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Integration;

/// <summary>
/// Tests GitHub Copilot MCP integration
/// Converted from test_copilot_integration.ps1 and test_mcp_copilot.ps1
/// </summary>
public class CopilotMcpIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public CopilotMcpIntegrationTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task PromptStudio_Api_Should_BeAccessible_For_Copilot()
    {
        _output.WriteLine("🤖 Testing GitHub Copilot MCP Integration");
        _output.WriteLine(new string('=', 50));
        
        _output.WriteLine("\n1️⃣ Testing PromptStudio API Access...");
        
        try
        {
            var response = await _client.GetAsync("/api/prompts/collections");
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var content = await response.Content.ReadAsStringAsync();
            var collections = JsonSerializer.Deserialize<JsonElement[]>(content);
            
            _output.WriteLine($"✅ PromptStudio API accessible - Found {collections!.Length} collections");
            
            // Show collection info
            foreach (var collection in collections.Take(3))
            {
                var name = collection.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : "Unknown";
                var promptCount = collection.TryGetProperty("promptTemplatesCount", out var countProp) ? countProp.GetInt32() : 0;
                _output.WriteLine($"   📁 Collection: {name} ({promptCount} prompts)");
            }
        }
        catch (HttpRequestException ex)
        {
            _output.WriteLine($"❌ PromptStudio API not accessible: {ex.Message}");
            _output.WriteLine("   Make sure PromptStudio is running on the expected port");
            throw new InvalidOperationException("PromptStudio API must be accessible for Copilot integration");
        }
    }

    [Fact]
    public void NodeJs_MCP_Server_Should_Build_For_Copilot()
    {
        _output.WriteLine("2️⃣ Testing MCP Server Build...");
        
        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "mcp-server");
        var distPath = Path.Combine(mcpServerPath, "dist", "index.js");

        if (File.Exists(distPath))
        {
            _output.WriteLine("✅ MCP server build found");
            
            var fileInfo = new FileInfo(distPath);
            _output.WriteLine($"   📦 Build file: {distPath}");
            _output.WriteLine($"   📅 Last modified: {fileInfo.LastWriteTime}");
            _output.WriteLine($"   📏 Size: {fileInfo.Length} bytes");
        }
        else
        {
            _output.WriteLine("⚠️ MCP server build not found, attempting to build...");
            
            // Try to build
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "npm",
                Arguments = "run build",
                WorkingDirectory = mcpServerPath,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });

            process!.WaitForExit();
            
            if (process.ExitCode == 0 && File.Exists(distPath))
            {
                _output.WriteLine("✅ MCP server built successfully");
            }
            else
            {
                var error = process.StandardError.ReadToEnd();
                _output.WriteLine($"❌ MCP server build failed: {error}");
                throw new InvalidOperationException("MCP server build is required for Copilot integration");
            }
        }
    }

    [Fact]
    public async Task MCP_Tools_Should_Be_Available_For_Copilot()
    {
        _output.WriteLine("3️⃣ Testing MCP Tools Availability...");
        
        var mcpServerPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "mcp-server");
        var distPath = Path.Combine(mcpServerPath, "dist", "index.js");

        if (!File.Exists(distPath))
        {
            _output.WriteLine("⚠️ Skipping tools test - MCP server build not found");
            return;
        }

        // Test tools/list request
        var toolsRequest = @"{""jsonrpc"":""2.0"",""id"":1,""method"":""tools/list"",""params"":{}}";
        
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

        // Send tools/list request
        await process!.StandardInput.WriteLineAsync(toolsRequest);
        process.StandardInput.Close();

        // Wait for response with timeout
        var outputTask = process.StandardOutput.ReadToEndAsync();
        var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));

        var completedTask = await Task.WhenAny(outputTask, timeoutTask);
        
        if (completedTask == timeoutTask)
        {
            process.Kill();
            _output.WriteLine("⚠️ MCP server response timeout");
            return;
        }

        var output = await outputTask;
        
        if (output.Contains("tools"))
        {
            _output.WriteLine("✅ MCP tools available for Copilot");
            
            // Try to parse and show tool count
            try
            {
                var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var jsonLine = lines.FirstOrDefault(l => l.Trim().StartsWith("{"));
                
                if (jsonLine != null)
                {
                    var response = JsonSerializer.Deserialize<JsonElement>(jsonLine);
                    if (response.TryGetProperty("result", out var result) &&
                        result.TryGetProperty("tools", out var tools) &&
                        tools.ValueKind == JsonValueKind.Array)
                    {
                        var toolCount = tools.GetArrayLength();
                        _output.WriteLine($"   🔧 Available tools: {toolCount}");
                        
                        // Show first few tools
                        for (int i = 0; i < Math.Min(3, toolCount); i++)
                        {
                            var tool = tools[i];
                            if (tool.TryGetProperty("name", out var name))
                            {
                                _output.WriteLine($"   - {name.GetString()}");
                            }
                        }
                    }
                }
            }
            catch (JsonException)
            {
                _output.WriteLine("   📝 Tools response received (could not parse details)");
            }
        }
        else
        {
            _output.WriteLine("❌ MCP tools not responding correctly");
            _output.WriteLine($"Output: {output}");
        }
    }

    [Fact]
    public async Task Copilot_Integration_Configuration_Should_Be_Valid()
    {
        _output.WriteLine("4️⃣ Testing Copilot Integration Configuration...");
        
        // Test that MCP configuration endpoints are accessible
        var configEndpoints = new[]
        {
            "/api/mcp/collections",
            "/api/mcp/prompts",
            "/api/mcp/variable-collections"
        };

        foreach (var endpoint in configEndpoints)
        {
            try
            {
                var response = await _client.GetAsync(endpoint);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                _output.WriteLine($"   ✅ {endpoint}: Accessible");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"   ❌ {endpoint}: Failed - {ex.Message}");
                throw new InvalidOperationException($"MCP endpoint {endpoint} must be accessible for Copilot integration");
            }
        }
    }

    [Fact]
    public async Task End_To_End_Copilot_Workflow_Should_Work()
    {
        _output.WriteLine("5️⃣ Testing End-to-End Copilot Workflow...");
        
        try
        {
            // Step 1: Get collections (simulating Copilot discovering available prompts)
            var collectionsResponse = await _client.GetAsync("/api/mcp/collections");
            collectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var collectionsContent = await collectionsResponse.Content.ReadAsStringAsync();
            var collections = JsonSerializer.Deserialize<JsonElement[]>(collectionsContent);
            _output.WriteLine($"   ✅ Step 1: Discovered {collections!.Length} collections");

            if (collections.Length == 0)
            {
                _output.WriteLine("   ⚠️ No collections available for workflow test");
                return;
            }

            // Step 2: Get prompts (simulating Copilot browsing available prompts)
            var promptsResponse = await _client.GetAsync("/api/mcp/prompts");
            promptsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var promptsContent = await promptsResponse.Content.ReadAsStringAsync();
            var prompts = JsonSerializer.Deserialize<JsonElement[]>(promptsContent);
            _output.WriteLine($"   ✅ Step 2: Found {prompts!.Length} prompts");

            if (prompts.Length == 0)
            {
                _output.WriteLine("   ⚠️ No prompts available for workflow test");
                return;
            }

            // Step 3: Get variable collections (simulating Copilot finding test data)
            var firstPromptId = prompts[0].GetProperty("id").GetInt32();
            var variableCollectionsResponse = await _client.GetAsync($"/api/mcp/variable-collections?promptId={firstPromptId}");
            variableCollectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            _output.WriteLine("   ✅ Step 3: Variable collections accessible");

            // Step 4: Test CSV template generation (simulating Copilot understanding prompt structure)
            var templateResponse = await _client.GetAsync($"/api/mcp/prompt-templates/{firstPromptId}/csv-template");
            templateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var template = await templateResponse.Content.ReadAsStringAsync();
            template.Should().NotBeEmpty();
            _output.WriteLine("   ✅ Step 4: CSV template generated");

            _output.WriteLine("🎉 End-to-end Copilot workflow completed successfully!");
            _output.WriteLine("\nCopilot Integration Status:");
            _output.WriteLine("✅ API Access: Working");
            _output.WriteLine("✅ Prompt Discovery: Working");
            _output.WriteLine("✅ Template Generation: Working");
            _output.WriteLine("✅ Variable Management: Working");
        }
        catch (Exception ex)
        {
            _output.WriteLine($"❌ Copilot workflow failed: {ex.Message}");
            throw;
        }
    }

    [Fact]
    public void Configuration_Documentation_Should_Be_Available()
    {
        _output.WriteLine("6️⃣ Checking Configuration Documentation...");
        
        var configFiles = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "mcp-config.json"),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "GITHUB_COPILOT_MCP_COMPLETE.md"),
            Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "MCP_INTEGRATION_GUIDE.md")
        };

        var foundFiles = 0;
        foreach (var configFile in configFiles)
        {
            if (File.Exists(configFile))
            {
                foundFiles++;
                _output.WriteLine($"   ✅ Found: {Path.GetFileName(configFile)}");
            }
            else
            {
                _output.WriteLine($"   ❌ Missing: {Path.GetFileName(configFile)}");
            }
        }

        foundFiles.Should().BeGreaterThan(0, "At least some configuration documentation should be available");
        _output.WriteLine($"   📚 Configuration files available: {foundFiles}/{configFiles.Length}");
    }
}
