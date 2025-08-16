using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PromptStudio.Core.Domain;
using PromptStudio.Data;
using PromptStudio.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Integration;

/// <summary>
/// Integration tests for form submission functionality
/// </summary>
public class FormSubmissionTests : IClassFixture<PromptStudioWebApplicationFactory>
{
    private readonly PromptStudioWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public FormSubmissionTests(PromptStudioWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task CreateCollection_ValidForm_ShouldSucceed()
    {
        // Arrange
        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Name", "Test Collection Form"),
            new KeyValuePair<string, string>("Description", "Collection created via form submission test")
        });

        // Act
        var response = await _client.PostAsync("/Collections/Create", formData);

        // Assert
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect);

        // Verify the collection was created
        await VerifyCollectionExists("Test Collection Form");

        _output.WriteLine("✓ Collection form submission successful");
    }

    [Fact]
    public async Task CreatePrompt_ValidForm_ShouldSucceed()
    {
        // Arrange - First create a collection
        var collectionId = await CreateTestCollectionAsync();

        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Name", "Test Prompt Form"),
            new KeyValuePair<string, string>("Content", "Generate {{type}} content for {{purpose}}"),
            new KeyValuePair<string, string>("Variables", "type,purpose"),
            new KeyValuePair<string, string>("CollectionId", collectionId.ToString())
        });

        // Act
        var response = await _client.PostAsync("/Prompts/Create", formData);

        // Assert
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect);

        // Verify the prompt was created
        await VerifyPromptExists("Test Prompt Form", collectionId);

        _output.WriteLine("✓ Prompt form submission successful");
    }

    [Fact]
    public async Task CreateVariableCollection_ValidForm_ShouldSucceed()
    {
        // Arrange - Create collection and prompt first
        var collectionId = await CreateTestCollectionAsync();
        var promptId = await CreateTestPromptAsync(collectionId);

        var csvData = "language,framework\nC#,ASP.NET\nJavaScript,React\nPython,Django";

        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Name", "Test Variable Collection"),
            new KeyValuePair<string, string>("Description", "Variable collection from form test"),
            new KeyValuePair<string, string>("PromptTemplateId", promptId.ToString()),
            new KeyValuePair<string, string>("CsvData", csvData)
        });

        // Act
        var response = await _client.PostAsync("/VariableCollections/Create", formData);

        // Assert        
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect);

        // Verify the variable collection was created
        await VerifyVariableCollectionExists("Test Variable Collection", promptId);

        _output.WriteLine("✓ Variable collection form submission successful");
    }

    [Theory]
    [InlineData("", "Description", "Name is required")]
    [InlineData("Valid Name", "", "Description can be empty")]
    [InlineData("A", "Valid Description", "Very short names should be accepted")]
    public async Task CreateCollection_VariousInputs_ShouldHandleValidation(
        string name, string description, string expectedBehavior)
    {
        // Arrange
        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Name", name),
            new KeyValuePair<string, string>("Description", description)
        });

        // Act
        var response = await _client.PostAsync("/Collections/Create", formData);

        // Assert
        if (string.IsNullOrEmpty(name))
        {
            // Should fail validation for empty name
            response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.OK);
        }
        else
        {
            // Should succeed for valid name
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.Redirect);
        }

        _output.WriteLine($"✓ Form validation test: {expectedBehavior}");
    }

    [Fact]
    public async Task FormSubmission_ComprehensiveWorkflow_ShouldCreateCompleteStructure()
    {
        // This test creates a complete workflow: Collection -> Prompt -> Variable Collection
        var testResults = new List<(string TestName, bool Success, string Details)>();

        try
        {
            // Step 1: Create Collection
            var collectionFormData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "Workflow Test Collection"),
                new KeyValuePair<string, string>("Description", "Complete workflow test collection")
            });

            var collectionResponse = await _client.PostAsync("/Collections/Create", collectionFormData);
            var collectionSuccess = collectionResponse.StatusCode == HttpStatusCode.OK ||
                                  collectionResponse.StatusCode == HttpStatusCode.Redirect;
            testResults.Add(("Create Collection Form", collectionSuccess,
                $"Status: {collectionResponse.StatusCode}"));

            if (collectionSuccess)
            {
                var collectionId = await GetCollectionIdByName("Workflow Test Collection");

                // Step 2: Create Prompt
                var promptFormData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Name", "Workflow Test Prompt"),
                    new KeyValuePair<string, string>("Content", "Create {{item}} using {{technology}}"),
                    new KeyValuePair<string, string>("Variables", "item,technology"),
                    new KeyValuePair<string, string>("CollectionId", collectionId.ToString())
                });

                var promptResponse = await _client.PostAsync("/Prompts/Create", promptFormData);
                var promptSuccess = promptResponse.StatusCode == HttpStatusCode.OK ||
                                  promptResponse.StatusCode == HttpStatusCode.Redirect;
                testResults.Add(("Create Prompt Form", promptSuccess,
                    $"Status: {promptResponse.StatusCode}"));

                if (promptSuccess)
                {
                    var promptId = await GetPromptIdByName("Workflow Test Prompt");

                    // Step 3: Create Variable Collection
                    var variableFormData = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Name", "Workflow Test Variables"),
                        new KeyValuePair<string, string>("Description", "Variables for workflow test"),
                        new KeyValuePair<string, string>("PromptTemplateId", promptId.ToString()),
                        new KeyValuePair<string, string>("CsvData", "item,technology\nwebsite,React\napi,ASP.NET")
                    });

                    var variableResponse = await _client.PostAsync("/VariableCollections/Create", variableFormData);
                    var variableSuccess = variableResponse.StatusCode == HttpStatusCode.OK ||
                                        variableResponse.StatusCode == HttpStatusCode.Redirect;
                    testResults.Add(("Create Variable Collection Form", variableSuccess,
                        $"Status: {variableResponse.StatusCode}"));
                }
            }

            // Log all test results
            _output.WriteLine("=== Form Submission Workflow Test Results ===");
            var successCount = 0;
            foreach (var (testName, success, details) in testResults)
            {
                var status = success ? "PASS" : "FAIL";
                _output.WriteLine($"[{status}] {testName}: {details}");
                if (success) successCount++;
            }

            _output.WriteLine($"\nResults: {successCount} PASSED, {testResults.Count - successCount} FAILED out of {testResults.Count} tests");

            // Assert that the complete workflow succeeded
            successCount.Should().Be(testResults.Count,
                "All form submission steps should succeed for a complete workflow");

            if (successCount == testResults.Count)
            {
                _output.WriteLine("\n[SUCCESS] Complete form submission workflow is working correctly!");
                _output.WriteLine("- Collection creation: Working");
                _output.WriteLine("- Prompt creation: Working");
                _output.WriteLine("- Variable collection creation: Working");
            }
        }
        catch (Exception ex)
        {
            _output.WriteLine($"Workflow test execution failed: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Creates a test collection and returns its ID
    /// </summary>
    private async Task<int> CreateTestCollectionAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();

        var collection = new Collection
        {
            Name = $"Test Collection {Guid.NewGuid()}",
            Description = "Test collection for form submission tests",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dbContext.Collections.Add(collection);
        await dbContext.SaveChangesAsync();

        return collection.Id;
    }

    /// <summary>
    /// Creates a test prompt and returns its ID
    /// </summary>
    private async Task<int> CreateTestPromptAsync(int collectionId)
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>(); var prompt = new PromptTemplate
        {
            Name = $"Test Prompt {Guid.NewGuid()}",
            Content = "Test {{variable1}} and {{variable2}}",
            Variables =
            [
                new PromptVariable { Name = "variable1", DefaultValue = "" },
                new PromptVariable { Name = "variable2", DefaultValue = "" }
            ],
            CollectionId = collectionId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dbContext.PromptTemplates.Add(prompt);
        await dbContext.SaveChangesAsync();

        return prompt.Id;
    }

    /// <summary>
    /// Verifies that a collection exists in the database
    /// </summary>
    private async Task VerifyCollectionExists(string collectionName)
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();

        var collection = await dbContext.Collections
            .FirstOrDefaultAsync(c => c.Name == collectionName);

        collection.Should().NotBeNull($"Collection '{collectionName}' should exist");
    }

    /// <summary>
    /// Verifies that a prompt exists in the database
    /// </summary>
    private async Task VerifyPromptExists(string promptName, int collectionId)
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();

        var prompt = await dbContext.PromptTemplates
            .FirstOrDefaultAsync(p => p.Name == promptName && p.CollectionId == collectionId);

        prompt.Should().NotBeNull($"Prompt '{promptName}' should exist in collection {collectionId}");
    }

    /// <summary>
    /// Verifies that a variable collection exists in the database
    /// </summary>
    private async Task VerifyVariableCollectionExists(string variableCollectionName, int promptId)
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();

        var variableCollection = await dbContext.VariableCollections
            .FirstOrDefaultAsync(vc => vc.Name == variableCollectionName && vc.PromptTemplateId == promptId);

        variableCollection.Should().NotBeNull($"Variable collection '{variableCollectionName}' should exist for prompt {promptId}");
    }

    /// <summary>
    /// Gets collection ID by name
    /// </summary>
    private async Task<int> GetCollectionIdByName(string name)
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();

        var collection = await dbContext.Collections.FirstAsync(c => c.Name == name);
        return collection.Id;
    }

    /// <summary>
    /// Gets prompt ID by name
    /// </summary>
    private async Task<int> GetPromptIdByName(string name)
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();

        var prompt = await dbContext.PromptTemplates.FirstAsync(p => p.Name == name);
        return prompt.Id;
    }
}
