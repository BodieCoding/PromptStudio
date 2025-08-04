using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace PromptStudio.Core.Services;

/// <summary>
/// Service for handling prompt variables, CSV operations, and variable collections
/// </summary>
public class VariableService : IVariableService
{
    private static readonly Regex VariablePattern = new(@"\{\{([^{}]+)\}\}", RegexOptions.Compiled);
    private readonly IPromptStudioDbContext _context;

    public VariableService(IPromptStudioDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Extracts variable names from a prompt template content
    /// </summary>
    /// <param name="promptContent">The content to extract variables from</param>
    /// <returns>List of variable names found in the content</returns>
    public List<string> ExtractVariableNames(string promptContent)
    {
        var variables = new List<string>();
        var startIndex = 0;

        while (true)
        {
            var start = promptContent.IndexOf("{{", startIndex);
            if (start == -1) break;

            var end = promptContent.IndexOf("}}", start + 2);
            if (end == -1) break;

            var variableName = promptContent.Substring(start + 2, end - start - 2).Trim();
            if (!string.IsNullOrEmpty(variableName) && !variables.Contains(variableName))
            {
                variables.Add(variableName);
            }

            startIndex = end + 2;
        }

        return variables;
    }

    /// <summary>
    /// Validates that all required variables have values
    /// </summary>
    /// <param name="template">The prompt template to validate</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <returns>True if all required variables have values, false otherwise</returns>
    public bool ValidateVariables(PromptTemplate template, Dictionary<string, string> variableValues)
    {
        foreach (var variable in template.Variables)
        {
            if (!variableValues.ContainsKey(variable.Name) && string.IsNullOrEmpty(variable.DefaultValue))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Generates a sample CSV template for a prompt's variables
    /// </summary>
    /// <param name="template">The prompt template to generate CSV for</param>
    /// <returns>CSV content with headers for all variables</returns>
    public string GenerateVariableCsvTemplate(PromptTemplate template)
    {
        var variableNames = ExtractVariableNames(template.Content);
        if (!variableNames.Any())
        {
            return "No variables found in this prompt template.";
        }

        // Create CSV header
        var csv = string.Join(",", variableNames) + "\n";

        // Add sample rows with default values or examples
        var sampleRow1 = string.Join(",", variableNames.Select(name =>
        {
            var variable = template.Variables.FirstOrDefault(v => v.Name == name);
            return variable?.DefaultValue ?? $"sample_{name}_1";
        }));

        var sampleRow2 = string.Join(",", variableNames.Select(name =>
        {
            var variable = template.Variables.FirstOrDefault(v => v.Name == name);
            return variable?.DefaultValue ?? $"sample_{name}_2";
        }));

        csv += sampleRow1 + "\n";
        csv += sampleRow2 + "\n";

        return csv;
    }

    /// <summary>
    /// Parses CSV content into variable sets
    /// </summary>
    /// <param name="csvContent">The CSV content to parse</param>
    /// <param name="expectedVariables">List of expected variable names</param>
    /// <returns>List of dictionaries containing variable values for each row</returns>
    public List<Dictionary<string, string>> ParseVariableCsv(string csvContent, List<string> expectedVariables)
    {
        var result = new List<Dictionary<string, string>>();
        var lines = csvContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length < 2) // Need at least header + 1 data row
        {
            throw new ArgumentException("CSV must contain at least a header row and one data row.");
        }

        // Parse header row
        var headers = ParseCsvLine(lines[0]);

        // Validate headers contain expected variables
        var missingVariables = expectedVariables.Where(v => !headers.Contains(v, StringComparer.OrdinalIgnoreCase)).ToList();
        if (missingVariables.Any())
        {
            throw new ArgumentException($"CSV is missing required variables: {string.Join(", ", missingVariables)}");
        }

        // Parse data rows
        for (int i = 1; i < lines.Length; i++)
        {
            var values = ParseCsvLine(lines[i]);
            if (values.Length != headers.Length)
            {
                throw new ArgumentException($"Row {i + 1} has {values.Length} values but header has {headers.Length} columns.");
            }

            var row = new Dictionary<string, string>();
            for (int j = 0; j < headers.Length; j++)
            {
                row[headers[j]] = values[j];
            }
            result.Add(row);
        }

        return result;
    }

    /// <summary>
    /// Batch executes a prompt template against multiple variable sets
    /// </summary>
    /// <param name="template">The prompt template to execute</param>
    /// <param name="variableSets">List of variable sets to use</param>
    /// <returns>List of execution results with variables, resolved prompts, and any errors</returns>
    public List<(Dictionary<string, string> Variables, string ResolvedPrompt, bool Success, string? Error)> BatchExecute(
        PromptTemplate template, List<Dictionary<string, string>> variableSets)
    {
        var results = new List<(Dictionary<string, string>, string, bool, string?)>();

        foreach (var variableSet in variableSets)
        {
            try
            {
                if (!ValidateVariables(template, variableSet))
                {
                    results.Add((variableSet, "", false, "Missing required variables"));
                    continue;
                }

                var resolvedPrompt = ResolvePrompt(template, variableSet);
                results.Add((variableSet, resolvedPrompt, true, null));
            }
            catch (Exception ex)
            {
                results.Add((variableSet, "", false, ex.Message));
            }
        }

        return results;
    }

    /// <summary>
    /// Get variable collections for a prompt template
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>List of variable collections</returns>
    public async Task<List<VariableCollection>> GetVariableCollectionsAsync(int promptId)
    {
        return await _context.VariableCollections
            .Where(vc => vc.PromptTemplateId == promptId)
            .OrderBy(vc => vc.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Create a variable collection from CSV data
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="csvData">CSV data with variables</param>
    /// <param name="description">Optional collection description</param>
    /// <returns>Created variable collection</returns>
    public async Task<VariableCollection> CreateVariableCollectionAsync(string name, int promptId, string csvData, string? description = null)
    {
        var template = await _context.PromptTemplates
            .Include(pt => pt.Variables)
            .FirstOrDefaultAsync(pt => pt.Id == promptId);

        if (template == null)
        {
            throw new ArgumentException($"Prompt template with ID {promptId} not found");
        }

        var expectedVariables = ExtractVariableNames(template.Content);
        var variableSets = ParseVariableCsv(csvData, expectedVariables);

        var collection = new VariableCollection
        {
            Name = name,
            Description = description ?? "",
            PromptTemplateId = promptId,
            VariableSets = JsonSerializer.Serialize(variableSets),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.VariableCollections.Add(collection);
        await _context.SaveChangesAsync();

        return collection;
    }

    /// <summary>
    /// Generate a CSV template for a prompt template
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <returns>CSV template content</returns>
    public async Task<string> GenerateCsvTemplateAsync(int templateId)
    {
        var template = await _context.PromptTemplates
            .Include(pt => pt.Variables)
            .FirstOrDefaultAsync(pt => pt.Id == templateId);

        if (template == null)
        {
            throw new ArgumentException($"Prompt template with ID {templateId} not found");
        }

        return GenerateVariableCsvTemplate(template);
    }

    /// <summary>
    /// List variable collections for a prompt template
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>List of variable collections</returns>
    public async Task<List<VariableCollection>> ListVariableCollectionsAsync(int promptId)
    {
        return await GetVariableCollectionsAsync(promptId);
    }

    #region Private Helper Methods

    /// <summary>
    /// Resolves a prompt template by substituting variables with provided values
    /// </summary>
    /// <param name="template">The prompt template to resolve</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <returns>The resolved prompt with variables substituted</returns>
    private string ResolvePrompt(PromptTemplate template, Dictionary<string, string> variableValues)
    {
        var resolvedContent = template.Content;

        foreach (var variable in template.Variables)
        {
            var placeholder = $"{{{{{variable.Name}}}}}";
            var value = variableValues.GetValueOrDefault(variable.Name, variable.DefaultValue ?? "");
            resolvedContent = resolvedContent.Replace(placeholder, value);
        }

        return resolvedContent;
    }

    /// <summary>
    /// Parses a CSV line handling quoted fields
    /// </summary>
    /// <param name="line">The CSV line to parse</param>
    /// <returns>Array of parsed values</returns>
    private string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        bool inQuotes = false;
        bool escaped = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (escaped)
            {
                current.Append(c);
                escaped = false;
            }
            else if (c == '\\')
            {
                escaped = true;
            }
            else if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString().Trim());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        result.Add(current.ToString().Trim());
        return result.ToArray();
    }

    #endregion
}
