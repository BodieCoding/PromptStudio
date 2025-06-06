using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Integration;

/// <summary>
/// Tests MCP validation and simple integration scenarios
/// Converted from test_mcp_simple_validation.ps1 and test_mcp_final_validation.ps1
/// </summary>
public class McpValidationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public McpValidationTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task McpEndpoints_Should_BeAccessible()
    {
        _output.WriteLine("=== MCP Endpoint Validation Test ===");
        
        var endpoints = new[]
        {
            "/api/mcp/collections",
            "/api/mcp/prompts",
            "/api/mcp/variable-collections",
            "/api/mcp/execution-history"
        };

        foreach (var endpoint in endpoints)
        {
            var response = await _client.GetAsync(endpoint);
            
            response.StatusCode.Should().Be(HttpStatusCode.OK, $"Endpoint {endpoint} should be accessible");
            _output.WriteLine($"✓ {endpoint}: {response.StatusCode}");
        }
        
        _output.WriteLine("✅ All MCP endpoints are accessible");
    }

    [Fact]
    public async Task McpCollections_Should_ReturnValidJson()
    {
        _output.WriteLine("Testing MCP Collections JSON validation...");
        
        var response = await _client.GetAsync("/api/mcp/collections");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeEmpty();
        
        // Validate JSON structure
        var collections = JsonSerializer.Deserialize<JsonElement[]>(content);
        collections.Should().NotBeNull();
        
        _output.WriteLine($"✓ Collections endpoint returned valid JSON with {collections!.Length} items");
        
        // If collections exist, validate structure
        if (collections.Length > 0)
        {
            var firstCollection = collections[0];
            firstCollection.TryGetProperty("id", out _).Should().BeTrue("Collection should have an id");
            firstCollection.TryGetProperty("name", out _).Should().BeTrue("Collection should have a name");
            
            _output.WriteLine("✓ Collection JSON structure validation passed");
        }
    }

    [Fact]
    public async Task McpPrompts_Should_ReturnValidJson()
    {
        _output.WriteLine("Testing MCP Prompts JSON validation...");
        
        var response = await _client.GetAsync("/api/mcp/prompts");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeEmpty();
        
        // Validate JSON structure
        var prompts = JsonSerializer.Deserialize<JsonElement[]>(content);
        prompts.Should().NotBeNull();
        
        _output.WriteLine($"✓ Prompts endpoint returned valid JSON with {prompts!.Length} items");
        
        // If prompts exist, validate structure
        if (prompts.Length > 0)
        {
            var firstPrompt = prompts[0];
            firstPrompt.TryGetProperty("id", out _).Should().BeTrue("Prompt should have an id");
            firstPrompt.TryGetProperty("name", out _).Should().BeTrue("Prompt should have a name");
            
            _output.WriteLine("✓ Prompt JSON structure validation passed");
        }
    }

    [Fact]
    public async Task McpValidation_Should_HandleErrors_Gracefully()
    {
        _output.WriteLine("Testing MCP error handling...");
        
        // Test non-existent resource
        var response = await _client.GetAsync("/api/mcp/prompts/99999");
        
        // Should return 404 or handle gracefully
        response.StatusCode.Should().BeOneOf(HttpStatusCode.NotFound, HttpStatusCode.BadRequest);
        _output.WriteLine($"✓ Non-existent resource handled gracefully: {response.StatusCode}");
        
        // Test invalid collection ID
        var invalidCollectionResponse = await _client.GetAsync("/api/mcp/prompts?collectionId=99999");
        
        // Should return OK with empty array or handle gracefully
        invalidCollectionResponse.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        _output.WriteLine($"✓ Invalid collection ID handled gracefully: {invalidCollectionResponse.StatusCode}");
    }

    [Fact]
    public async Task McpWorkflow_Should_WorkEndToEnd()
    {
        _output.WriteLine("Testing complete MCP workflow...");
        
        // Step 1: Get collections
        var collectionsResponse = await _client.GetAsync("/api/mcp/collections");
        collectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Step 1: Collections retrieved");
        
        // Step 2: Get prompts
        var promptsResponse = await _client.GetAsync("/api/mcp/prompts");
        promptsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Step 2: Prompts retrieved");
        
        // Step 3: Get variable collections
        var variableCollectionsResponse = await _client.GetAsync("/api/mcp/variable-collections");
        variableCollectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Step 3: Variable collections retrieved");
        
        // Step 4: Get execution history
        var historyResponse = await _client.GetAsync("/api/mcp/execution-history");
        historyResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Step 4: Execution history retrieved");
        
        _output.WriteLine("✅ Complete MCP workflow validation passed");
    }

    [Fact]
    public async Task McpResponseTimes_Should_BeReasonable()
    {
        _output.WriteLine("Testing MCP response times...");
        
        var endpoints = new[]
        {
            "/api/mcp/collections",
            "/api/mcp/prompts",
            "/api/mcp/variable-collections"
        };

        foreach (var endpoint in endpoints)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await _client.GetAsync(endpoint);
            stopwatch.Stop();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            stopwatch.ElapsedMilliseconds.Should().BeLessThan(5000, $"Endpoint {endpoint} should respond within 5 seconds");
            
            _output.WriteLine($"✓ {endpoint}: {stopwatch.ElapsedMilliseconds}ms");
        }
        
        _output.WriteLine("✅ All MCP endpoints respond within acceptable time limits");
    }

    [Fact]
    public async Task McpConcurrency_Should_HandleMultipleRequests()
    {
        _output.WriteLine("Testing MCP concurrent request handling...");
        
        // Make multiple concurrent requests
        var tasks = new List<Task<HttpResponseMessage>>();
        
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(_client.GetAsync("/api/mcp/collections"));
            tasks.Add(_client.GetAsync("/api/mcp/prompts"));
        }
        
        var responses = await Task.WhenAll(tasks);
        
        // All requests should succeed
        foreach (var response in responses)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        _output.WriteLine($"✓ {responses.Length} concurrent requests handled successfully");
        _output.WriteLine("✅ MCP concurrent request handling validation passed");
    }

    [Fact]
    public async Task McpDataConsistency_Should_BeValidated()
    {
        _output.WriteLine("Testing MCP data consistency...");
        
        // Get collections
        var collectionsResponse = await _client.GetAsync("/api/mcp/collections");
        var collectionsContent = await collectionsResponse.Content.ReadAsStringAsync();
        var collections = JsonSerializer.Deserialize<JsonElement[]>(collectionsContent);
        
        // Get prompts
        var promptsResponse = await _client.GetAsync("/api/mcp/prompts");
        var promptsContent = await promptsResponse.Content.ReadAsStringAsync();
        var prompts = JsonSerializer.Deserialize<JsonElement[]>(promptsContent);
        
        _output.WriteLine($"Found {collections!.Length} collections and {prompts!.Length} prompts");
        
        // Basic consistency check: if there are prompts, there should be collections
        if (prompts.Length > 0)
        {
            collections.Length.Should().BeGreaterThan(0, "If prompts exist, at least one collection should exist");
            _output.WriteLine("✓ Data consistency: Prompts and collections relationship validated");
        }
        
        // Check that collection counts are reasonable
        foreach (var collection in collections.Take(3)) // Check first 3 collections
        {
            if (collection.TryGetProperty("id", out var idProp))
            {
                var collectionId = idProp.GetInt32();
                var collectionPromptsResponse = await _client.GetAsync($"/api/mcp/prompts?collectionId={collectionId}");
                collectionPromptsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
                
                _output.WriteLine($"✓ Collection {collectionId} prompts accessible");
            }
        }
        
        _output.WriteLine("✅ MCP data consistency validation completed");
    }
}
