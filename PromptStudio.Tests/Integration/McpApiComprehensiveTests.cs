using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Integration;

/// <summary>
/// Comprehensive tests for MCP API endpoints
/// Converted from test_mcp_api_comprehensive.ps1
/// </summary>
public class McpApiComprehensiveTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public McpApiComprehensiveTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Collections_Endpoint_Should_ReturnCollections()
    {
        // Test collections endpoint
        _output.WriteLine("1. Testing Collections endpoint...");
        
        var response = await _client.GetAsync("/api/Mcp/collections");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine($"✓ Collections: Status {response.StatusCode}");
        
        var content = await response.Content.ReadAsStringAsync();
        var collections = JsonSerializer.Deserialize<JsonElement[]>(content);
        
        collections.Should().NotBeNull();
        _output.WriteLine($"  Found {collections!.Length} collections");
    }

    [Fact]
    public async Task Prompts_Endpoint_Should_ReturnPrompts()
    {
        // Test prompts endpoint
        _output.WriteLine("2. Testing Prompts endpoint...");
        
        var response = await _client.GetAsync("/api/Mcp/prompts");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine($"✓ Prompts: Status {response.StatusCode}");
        
        var content = await response.Content.ReadAsStringAsync();
        var prompts = JsonSerializer.Deserialize<JsonElement[]>(content);
        
        prompts.Should().NotBeNull();
        _output.WriteLine($"  Found {prompts!.Length} prompts");
    }

    [Fact]
    public async Task CsvTemplate_Generation_Should_Work()
    {
        // First get a prompt to test with
        var promptsResponse = await _client.GetAsync("/api/Mcp/prompts");
        promptsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var promptsContent = await promptsResponse.Content.ReadAsStringAsync();
        var prompts = JsonSerializer.Deserialize<JsonElement[]>(promptsContent);
        
        if (prompts!.Length == 0)
        {
            _output.WriteLine("⚠ No prompts available to test CSV template generation");
            return;
        }

        var firstPromptId = prompts[0].GetProperty("id").GetInt32();
        
        // Test CSV template generation
        _output.WriteLine("3. Testing CSV Template generation...");
        
        var response = await _client.GetAsync($"/api/Mcp/prompt-templates/{firstPromptId}/csv-template");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine($"✓ CSV Template: Status {response.StatusCode}");
        
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeEmpty();
        
        var preview = content.Length > 100 ? content.Substring(0, 100) + "..." : content;
        _output.WriteLine($"  Template content: {preview}");
    }

    [Fact]
    public async Task VariableCollection_Creation_Should_Work()
    {
        // First get a prompt to test with
        var promptsResponse = await _client.GetAsync("/api/Mcp/prompts");
        promptsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var promptsContent = await promptsResponse.Content.ReadAsStringAsync();
        var prompts = JsonSerializer.Deserialize<JsonElement[]>(promptsContent);
        
        if (prompts!.Length == 0)
        {
            _output.WriteLine("⚠ No prompts available to test variable collection creation");
            return;
        }

        var firstPromptId = prompts[0].GetProperty("id").GetInt32();
        
        // Test variable collection creation
        _output.WriteLine("4. Testing Variable Collection creation...");
        
        var requestBody = new
        {
            PromptId = firstPromptId,
            Name = $"API Test Collection {DateTime.Now:yyyyMMdd-HHmmss}",
            Description = "Test collection created via API",
            CsvData = "language,code\npython,print('hello from API')\njavascript,console.log('hello from API')"
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync("/api/Mcp/variable-collections", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine($"✓ Variable Collection Creation: Status {response.StatusCode}");
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var newCollection = JsonSerializer.Deserialize<JsonElement>(responseContent);
        
        newCollection.TryGetProperty("name", out var nameProperty).Should().BeTrue();
        newCollection.TryGetProperty("id", out var idProperty).Should().BeTrue();
        
        _output.WriteLine($"  Created collection: {nameProperty.GetString()} (ID: {idProperty.GetInt32()})");
    }

    [Fact]
    public async Task BatchExecution_Should_Work()
    {
        // This test requires existing data, so we'll create a minimal test
        _output.WriteLine("5. Testing Batch Execution capabilities...");
        
        // Test that the endpoint exists and returns proper error for non-existent collection
        var response = await _client.PostAsync("/api/Mcp/batch-execute", 
            new StringContent("{\"collectionId\":99999,\"promptId\":99999}", Encoding.UTF8, "application/json"));
        
        // We expect either a 404 (not found) or 400 (bad request) for non-existent data
        response.StatusCode.Should().BeOneOf(HttpStatusCode.NotFound, HttpStatusCode.BadRequest);
        _output.WriteLine($"✓ Batch Execution endpoint accessible: Status {response.StatusCode}");
    }

    [Fact]
    public async Task ExecutionHistory_Should_BeAccessible()
    {
        // Test execution history endpoint
        _output.WriteLine("6. Testing Execution History...");
        
        var response = await _client.GetAsync("/api/Mcp/execution-history");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine($"✓ Execution History: Status {response.StatusCode}");
        
        var content = await response.Content.ReadAsStringAsync();
        var history = JsonSerializer.Deserialize<JsonElement[]>(content);
        
        history.Should().NotBeNull();
        _output.WriteLine($"  Found {history!.Length} execution records");
    }

    [Fact]
    public async Task AllEndpoints_Should_BeAccessible_InSequence()
    {
        // Run a comprehensive test of all endpoints in sequence
        _output.WriteLine("=== Comprehensive MCP API Test Sequence ===");
        
        // 1. Collections
        var collectionsResponse = await _client.GetAsync("/api/Mcp/collections");
        collectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Collections endpoint working");
        
        // 2. Prompts
        var promptsResponse = await _client.GetAsync("/api/Mcp/prompts");
        promptsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Prompts endpoint working");
        
        // 3. Variable Collections
        var variableCollectionsResponse = await _client.GetAsync("/api/Mcp/variable-collections");
        variableCollectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Variable Collections endpoint working");
        
        // 4. Execution History
        var historyResponse = await _client.GetAsync("/api/Mcp/execution-history");
        historyResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Execution History endpoint working");
        
        _output.WriteLine("✅ All MCP API endpoints are accessible and responding correctly");
    }
}
