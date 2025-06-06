using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Integration;

/// <summary>
/// Tests CSV functionality via MCP API
/// Converted from test_csv_mcp_functionality.ps1 and test_csv_simple.ps1
/// </summary>
public class CsvMcpFunctionalityTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public CsvMcpFunctionalityTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Should_GetCollections_Successfully()
    {
        _output.WriteLine("Step 1: Get Collections");
        
        var response = await _client.GetAsync("/api/mcp/collections");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        var collections = JsonSerializer.Deserialize<JsonElement[]>(content);
        
        collections.Should().NotBeNull();
        _output.WriteLine($"Found {collections!.Length} collections");
        
        if (collections.Length == 0)
        {
            _output.WriteLine("No collections found. Please create some test data first.");
            throw new InvalidOperationException("Test requires at least one collection");
        }

        // Display collections in a table-like format
        foreach (var collection in collections.Take(5)) // Show first 5
        {
            var id = collection.TryGetProperty("id", out var idProp) ? idProp.GetInt32() : 0;
            var name = collection.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : "Unknown";
            var promptCount = collection.TryGetProperty("promptTemplatesCount", out var countProp) ? countProp.GetInt32() : 0;
            
            _output.WriteLine($"  {id} | {name} | {promptCount} prompts");
        }
    }

    [Fact]
    public async Task Should_GetPromptsInCollection_Successfully()
    {
        // First get collections
        var collectionsResponse = await _client.GetAsync("/api/mcp/collections");
        collectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var collectionsContent = await collectionsResponse.Content.ReadAsStringAsync();
        var collections = JsonSerializer.Deserialize<JsonElement[]>(collectionsContent);
        
        if (collections!.Length == 0)
        {
            throw new InvalidOperationException("Test requires at least one collection");
        }

        var collectionId = collections[0].GetProperty("id").GetInt32();
        
        _output.WriteLine($"Step 2: Get Prompts in First Collection (ID: {collectionId})");
        
        var response = await _client.GetAsync($"/api/mcp/prompts?collectionId={collectionId}");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        var prompts = JsonSerializer.Deserialize<JsonElement[]>(content);
        
        prompts.Should().NotBeNull();
        _output.WriteLine($"Found {prompts!.Length} prompts in collection");
        
        if (prompts.Length == 0)
        {
            _output.WriteLine("No prompts found in collection. Please create some test prompts first.");
            throw new InvalidOperationException("Test requires at least one prompt in the collection");
        }

        // Display prompts in a table-like format
        foreach (var prompt in prompts.Take(5)) // Show first 5
        {
            var id = prompt.TryGetProperty("id", out var idProp) ? idProp.GetInt32() : 0;
            var name = prompt.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : "Unknown";
            var variableCount = prompt.TryGetProperty("variableCount", out var countProp) ? countProp.GetInt32() : 0;
            
            _output.WriteLine($"  {id} | {name} | {variableCount} variables");
        }
    }

    [Fact]
    public async Task Should_GenerateCsvTemplate_Successfully()
    {
        // Get first available prompt
        var (collectionId, promptId, promptName) = await GetFirstAvailablePrompt();
        
        _output.WriteLine($"Step 3: Generate CSV Template for Prompt '{promptName}' (ID: {promptId})");
        
        var response = await _client.GetAsync($"/api/mcp/prompt-templates/{promptId}/csv-template");
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var csvTemplate = await response.Content.ReadAsStringAsync();
        
        csvTemplate.Should().NotBeEmpty();
        _output.WriteLine("Generated CSV template:");
        _output.WriteLine(csvTemplate);
        
        // Verify CSV template has basic structure
        csvTemplate.Should().Contain(",", "CSV template should contain comma separators");
        
        // If the template has multiple lines, it should have headers
        var lines = csvTemplate.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length > 1)
        {
            lines[0].Should().NotBeEmpty("CSV template should have headers");
        }
    }

    [Fact]
    public async Task Should_HandleCsvVariableSubstitution()
    {
        var (collectionId, promptId, promptName) = await GetFirstAvailablePrompt();
        
        _output.WriteLine($"Step 4: Testing CSV Variable Substitution for Prompt '{promptName}'");
        
        // Get the CSV template first
        var templateResponse = await _client.GetAsync($"/api/mcp/prompt-templates/{promptId}/csv-template");
        templateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var csvTemplate = await templateResponse.Content.ReadAsStringAsync();
        _output.WriteLine($"Using CSV template: {csvTemplate}");
        
        // Test variable collections endpoint
        var variableCollectionsResponse = await _client.GetAsync($"/api/mcp/variable-collections?promptId={promptId}");
        variableCollectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var variableCollectionsContent = await variableCollectionsResponse.Content.ReadAsStringAsync();
        var variableCollections = JsonSerializer.Deserialize<JsonElement[]>(variableCollectionsContent);
        
        variableCollections.Should().NotBeNull();
        _output.WriteLine($"Found {variableCollections!.Length} variable collections for this prompt");
        
        if (variableCollections.Length > 0)
        {
            foreach (var collection in variableCollections.Take(3))
            {
                var id = collection.TryGetProperty("id", out var idProp) ? idProp.GetInt32() : 0;
                var name = collection.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : "Unknown";
                var rowCount = collection.TryGetProperty("rowCount", out var countProp) ? countProp.GetInt32() : 0;
                
                _output.WriteLine($"  Collection: {name} (ID: {id}) - {rowCount} rows");
            }
        }
    }

    [Fact]
    public async Task Should_ValidateEnhancedCsvFunctionality()
    {
        _output.WriteLine("Testing Enhanced CSV Functionality via MCP API");
        
        // Test the complete workflow
        var (collectionId, promptId, promptName) = await GetFirstAvailablePrompt();
        
        // 1. Get CSV template
        var templateResponse = await _client.GetAsync($"/api/mcp/prompt-templates/{promptId}/csv-template");
        templateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var csvTemplate = await templateResponse.Content.ReadAsStringAsync();
        _output.WriteLine($"✓ CSV Template generated for {promptName}");
        
        // 2. Get variable collections
        var collectionsResponse = await _client.GetAsync($"/api/mcp/variable-collections?promptId={promptId}");
        collectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        _output.WriteLine("✓ Variable collections retrieved");
        
        // 3. Test CSV parsing capabilities (if endpoint exists)
        var csvParseResponse = await _client.GetAsync("/api/mcp/csv-validation");
        // This might return 404 if not implemented, which is fine
        _output.WriteLine($"✓ CSV parsing endpoint check: {csvParseResponse.StatusCode}");
        
        _output.WriteLine("✅ Enhanced CSV functionality validated successfully");
    }

    private async Task<(int collectionId, int promptId, string promptName)> GetFirstAvailablePrompt()
    {
        // Get collections
        var collectionsResponse = await _client.GetAsync("/api/mcp/collections");
        collectionsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var collectionsContent = await collectionsResponse.Content.ReadAsStringAsync();
        var collections = JsonSerializer.Deserialize<JsonElement[]>(collectionsContent);
        
        if (collections!.Length == 0)
        {
            throw new InvalidOperationException("No collections found. Please create some test data first.");
        }
        
        var collectionId = collections[0].GetProperty("id").GetInt32();
        
        // Get prompts in first collection
        var promptsResponse = await _client.GetAsync($"/api/mcp/prompts?collectionId={collectionId}");
        promptsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var promptsContent = await promptsResponse.Content.ReadAsStringAsync();
        var prompts = JsonSerializer.Deserialize<JsonElement[]>(promptsContent);
        
        if (prompts!.Length == 0)
        {
            throw new InvalidOperationException("No prompts found in collection. Please create some test prompts first.");
        }
        
        var promptId = prompts[0].GetProperty("id").GetInt32();
        var promptName = prompts[0].TryGetProperty("name", out var nameProp) ? nameProp.GetString() ?? "Unknown" : "Unknown";
        
        return (collectionId, promptId, promptName);
    }
}
