using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using PromptStudio.Core.Domain;
using PromptStudio.Data;
using PromptStudio.Tests.Infrastructure;

namespace PromptStudio.Tests.Integration;

/// <summary>
/// Integration tests for CSV parsing functionality
/// </summary>
public class CsvParsingIntegrationTests : IClassFixture<PromptStudioWebApplicationFactory>
{
    private readonly PromptStudioWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public CsvParsingIntegrationTests(PromptStudioWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task VerifyTestInfrastructure_CanAccessWorkingEndpoint()
    {
        // Try a simple endpoint like health check or a GET endpoint you know exists
        var response = await _client.GetAsync("/api/health"); // or another known endpoint
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task GetCsvTemplate_WithValidPromptId_ReturnsTemplate()
    {
        // Arrange
        int promptId;
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();
            var template = new PromptTemplate
            {
                Name = "Test Template",
                Content = "Hello {{name}}, your age is {{age}}",
                Variables = new List<PromptVariable>
            {
                new() { Name = "name", Type = VariableType.Text },
                new() { Name = "age", Type = VariableType.Number }
            }
            };
            context.PromptTemplates.Add(template);
            await context.SaveChangesAsync();
            promptId = template.Id;
        }

        // Act
        var response = await _client.GetAsync($"/api/VariableCollectionsApi/template/{promptId}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.Content.Headers.ContentType?.MediaType.Should().Be("text/csv");

        var csvContent = await response.Content.ReadAsStringAsync();
        csvContent.Should().Contain("name");
        csvContent.Should().Contain("age");
    }

    [Fact]
    public async Task GetCsvTemplate_WithInvalidPromptId_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/VariableCollectionsApi/template/999999");

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("name,age\nJohn,25\nJane,30", 2)]
    [InlineData("name,age\nAlice,35", 1)]
    public void CsvParsing_WithValidData_ParsesCorrectly(string csvContent, int expectedCount)
    {
        // Arrange
        int promptId;
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();
            var template = new PromptTemplate
            {
                Name = "Test Template For Parsing",
                Content = "Hello {{name}}, your age is {{age}}",
                Variables = new List<PromptVariable>
                {
                    new() { Name = "name", Type = VariableType.Text },
                    new() { Name = "age", Type = VariableType.Number }
                }
            };
            context.PromptTemplates.Add(template);
            context.SaveChanges();
            promptId = template.Id;
        }

        // Get the prompt service to test parsing directly
        using var scopeForService = _factory.Services.CreateScope();
        var promptService = scopeForService.ServiceProvider.GetRequiredService<PromptStudio.Core.Interfaces.IPromptService>();
        var expectedVariables = new List<string> { "name", "age" };

        // Act
        var variableSets = promptService.ParseVariableCsv(csvContent, expectedVariables);

        // Assert
        variableSets.Should().HaveCount(expectedCount);
        
        if (expectedCount > 0)
        {
            variableSets.First().Should().ContainKey("name");
            variableSets.First().Should().ContainKey("age");
        }
    }

    [Fact]
    public void CsvParsing_WithMissingRequiredColumns_ThrowsException()
    {
        // Arrange
        var csvContent = "name\nJohn\nJane"; // Missing 'age' column
        var expectedVariables = new List<string> { "name", "age" };

        using var scope = _factory.Services.CreateScope();
        var promptService = scope.ServiceProvider.GetRequiredService<PromptStudio.Core.Interfaces.IPromptService>();

        // Act & Assert
        var act = () => promptService.ParseVariableCsv(csvContent, expectedVariables);
        act.Should().Throw<ArgumentException>()
           .WithMessage("*age*");
    }

    [Fact]
    public void CsvParsing_WithEmptyContent_ThrowsException()
    {
        // Arrange
        var csvContent = "";
        var expectedVariables = new List<string> { "name", "age" };

        using var scope = _factory.Services.CreateScope();
        var promptService = scope.ServiceProvider.GetRequiredService<PromptStudio.Core.Interfaces.IPromptService>();

        // Act & Assert
        var act = () => promptService.ParseVariableCsv(csvContent, expectedVariables);
        act.Should().Throw<ArgumentException>()
           .WithMessage("CSV must contain at least a header row and one data row.");
    }
}
