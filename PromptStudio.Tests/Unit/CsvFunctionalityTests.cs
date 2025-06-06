using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using PromptStudio.Data;
using PromptStudio.Core.Services;
using Xunit;
using Xunit.Abstractions;

namespace PromptStudio.Tests.Unit;

/// <summary>
/// Unit tests for CSV functionality
/// Based on the PowerShell test scripts: test_csv_simple.ps1 and test_csv_mcp_functionality.ps1
/// </summary>
public class CsvFunctionalityTests : IDisposable
{    private readonly PromptStudioDbContext _context;
    private readonly IPromptService _promptService;
    private readonly ITestOutputHelper _output;

    public CsvFunctionalityTests(ITestOutputHelper output)
    {
        _output = output;
          var options = new DbContextOptionsBuilder<PromptStudioDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
              _context = new PromptStudioDbContext(options);
        _promptService = new PromptService(_context);
        
        // Ensure database is created
        _context.Database.EnsureCreated();
    }    [Fact]
    public async Task GenerateVariableCsvTemplate_ShouldCreateValidTemplate()
    {
        // Arrange
        var prompt = new PromptTemplate
        {
            Name = "Test Prompt",
            Content = "Generate {{language}} code for {{description}} with {{complexity}} level",
            Variables = new List<PromptVariable>
            {
                new PromptVariable { Name = "language", DefaultValue = "" },
                new PromptVariable { Name = "description", DefaultValue = "" },
                new PromptVariable { Name = "complexity", DefaultValue = "" }
            }
        };

        _context.PromptTemplates.Add(prompt);
        await _context.SaveChangesAsync();

        // Act
        var csvTemplate = _promptService.GenerateVariableCsvTemplate(prompt);

        // Assert
        csvTemplate.Should().NotBeNullOrEmpty();
        csvTemplate.Should().StartWith("language,description,complexity");
        
        var lines = csvTemplate.Split('\n');
        lines.Should().HaveCount(2); // Header + one empty row
        lines[0].Trim().Should().Be("language,description,complexity");
        
        _output.WriteLine($"Generated CSV Template: {csvTemplate}");
    }

    [Theory]
    [InlineData("name,age\nJohn,30\nJane,25", 2)]
    [InlineData("language,code\npython,print('hello')\njavascript,console.log('hello')", 2)]
    [InlineData("single\nvalue", 1)]
    public void ParseCsvData_ShouldHandleVariousFormats(string csvData, int expectedRows)
    {
        // Act
        var rows = ParseCsvData(csvData);

        // Assert
        rows.Should().HaveCount(expectedRows);
        
        _output.WriteLine($"Parsed {rows.Count} rows from CSV data");
        foreach (var row in rows.Take(3)) // Log first 3 rows
        {
            _output.WriteLine($"Row: {string.Join(", ", row)}");
        }
    }

    [Fact]
    public async Task CreateVariableCollectionFromCsv_ShouldSucceed()
    {
        // Arrange
        var collection = new Collection
        {
            Name = "Test Collection",
            Description = "Test collection for CSV functionality"
        };
        _context.Collections.Add(collection);        var prompt = new PromptTemplate
        {
            Name = "Code Generator",
            Content = "Generate {{language}} code: {{code}}",
            Variables = new List<PromptVariable>
            {
                new PromptVariable { Name = "language", DefaultValue = "" },
                new PromptVariable { Name = "code", DefaultValue = "" }
            },
            CollectionId = collection.Id
        };
        _context.PromptTemplates.Add(prompt);
        await _context.SaveChangesAsync();

        var csvData = "language,code\npython,print('test!')\njavascript,console.log('test!')";

        // Act
        var variableCollection = new VariableCollection
        {
            Name = "Test Variables",
            Description = "Generated from CSV",
            PromptTemplateId = prompt.Id,
            CreatedAt = DateTime.UtcNow
        };
        _context.VariableCollections.Add(variableCollection);
        await _context.SaveChangesAsync();        var csvRows = ParseCsvData(csvData);
        var variableSets = new List<Dictionary<string, string>>();
        var headers = csvRows.First();
        
        foreach (var row in csvRows.Skip(1)) // Skip header row
        {
            var variableSet = new Dictionary<string, string>();
            for (int i = 0; i < Math.Min(headers.Count, row.Count); i++)
            {
                variableSet[headers[i]] = row[i];
            }
            variableSets.Add(variableSet);
        }
        
        variableCollection.VariableSets = System.Text.Json.JsonSerializer.Serialize(variableSets);
        await _context.SaveChangesAsync();

        // Assert
        var savedCollection = await _context.VariableCollections
            .FirstAsync(vc => vc.Id == variableCollection.Id);        savedCollection.Should().NotBeNull();
        
        var deserializedVariableSets = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(savedCollection.VariableSets);
        deserializedVariableSets.Should().HaveCount(2); // python and javascript rows
        
        _output.WriteLine($"Created variable collection '{savedCollection.Name}' with {deserializedVariableSets?.Count} variable sets");
    }    [Fact]
    public async Task ValidatePromptVariables_ShouldMatchCsvHeaders()
    {
        // Arrange
        var prompt = new PromptTemplate
        {
            Name = "Template with Variables",
            Content = "Hello {{name}}, you are {{age}} years old and live in {{city}}",
            Variables = new List<PromptVariable>
            {
                new PromptVariable { Name = "name", DefaultValue = "" },
                new PromptVariable { Name = "age", DefaultValue = "" },
                new PromptVariable { Name = "city", DefaultValue = "" }
            }
        };

        var csvData = "name,age,city\nJohn,30,NYC\nJane,25,LA";
        var csvRows = ParseCsvData(csvData);
        var headers = csvRows.FirstOrDefault();        // Act & Assert
        if (headers != null)
        {
            var promptVariables = prompt.Variables.Select(v => v.Name).ToList();
            var csvHeaders = headers.Select(h => h.Trim()).ToList();

            promptVariables.Should().BeEquivalentTo(csvHeaders, 
                "CSV headers should match prompt template variables");
            
            _output.WriteLine($"Prompt variables: {string.Join(", ", promptVariables)}");
            _output.WriteLine($"CSV headers: {string.Join(", ", csvHeaders)}");
        }
    }

    [Fact]
    public void HandleMalformedCsv_ShouldNotThrow()
    {
        // Arrange
        var malformedCsvData = "name,age\nJohn,30,extra\nJane"; // Inconsistent columns

        // Act & Assert
        var action = () => ParseCsvData(malformedCsvData);
        action.Should().NotThrow("Parser should handle malformed CSV gracefully");

        var rows = ParseCsvData(malformedCsvData);
        rows.Should().HaveCount(2); // Should still parse what it can
        
        _output.WriteLine($"Parsed {rows.Count} rows from malformed CSV");
    }

    /// <summary>
    /// Simple CSV parser for testing (mimics the CSV functionality used in the application)
    /// </summary>
    private static List<List<string>> ParseCsvData(string csvData)
    {
        var rows = new List<List<string>>();
        var lines = csvData.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var values = line.Split(',').Select(v => v.Trim()).ToList();
            rows.Add(values);
        }

        return rows;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
