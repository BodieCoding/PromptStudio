using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace PromptStudio.Core.Services;

/// <summary>
/// Implementation of the prompt service for managing prompts and variables
/// This is a domain service that implements business logic for prompt processing
/// </summary>
public class PromptService : IPromptService
{
    private static readonly Regex VariablePattern = new(@"\{\{([^{}]+)\}\}", RegexOptions.Compiled);
    private readonly PromptStudioDbContext _context;

    /// <summary>
    /// Initializes a new instance of the PromptService
    /// </summary>
    /// <param name="context">Database context for data access</param>
    public PromptService(PromptStudioDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Resolves a prompt template by substituting variables with provided values
    /// </summary>
    /// <param name="template">The prompt template to resolve</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <returns>The resolved prompt with variables substituted</returns>
    public string ResolvePrompt(PromptTemplate template, Dictionary<string, string> variableValues)
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
            return $"\"{variable?.DefaultValue ?? "sample_" + name}\"";
        }));

        var sampleRow2 = string.Join(",", variableNames.Select(name =>
        {
            var variable = template.Variables.FirstOrDefault(v => v.Name == name);
            return $"\"{variable?.DefaultValue ?? "example_" + name}\"";
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

        // Parse header
        var headers = ParseCsvLine(lines[0]);

        // Validate headers contain expected variables
        var missingVariables = expectedVariables.Except(headers, StringComparer.OrdinalIgnoreCase).ToList();
        if (missingVariables.Any())
        {
            throw new ArgumentException($"CSV is missing required columns: {string.Join(", ", missingVariables)}");
        }

        // Parse data rows
        for (int i = 1; i < lines.Length; i++)
        {
            var values = ParseCsvLine(lines[i]);
            if (values.Length != headers.Length)
            {
                throw new ArgumentException($"Row {i + 1} has {values.Length} values but expected {headers.Length}");
            }

            var variableSet = new Dictionary<string, string>();
            for (int j = 0; j < headers.Length; j++)
            {
                variableSet[headers[j]] = values[j];
            }
            result.Add(variableSet);
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
                    results.Add((variableSet, string.Empty, false, "Missing required variables"));
                    continue;
                }

                var resolvedPrompt = ResolvePrompt(template, variableSet);
                results.Add((variableSet, resolvedPrompt, true, null));
            }
            catch (Exception ex)
            {
                results.Add((variableSet, string.Empty, false, ex.Message));
            }
        }

        return results;
    }

    private string[] ParseCsvLine(string line)
    {
        // Simple CSV parser - handles quoted fields
        var result = new List<string>();
        var current = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote
                    current.Append('"');
                    i++; // Skip next quote
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }        result.Add(current.ToString());
        return result.ToArray();
    }

    // Database Operations    /// <summary>
    /// Get all collections
    /// </summary>
    /// <returns>List of all collections with their prompt templates</returns>
    public async Task<List<Collection>> GetCollectionsAsync()
    {
        return await _context.Collections
            .Include(c => c.PromptTemplates)
                .ThenInclude(pt => pt.Variables)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Get a collection by ID
    /// </summary>
    /// <param name="id">Collection ID</param>
    /// <returns>Collection with prompt templates, or null if not found</returns>
    public async Task<Collection?> GetCollectionByIdAsync(int id)
    {
        return await _context.Collections
            .Include(c => c.PromptTemplates)
                .ThenInclude(pt => pt.Variables)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Get prompt templates, optionally filtered by collection
    /// </summary>
    /// <param name="collectionId">Optional collection ID to filter by</param>
    /// <returns>List of prompt templates</returns>
    public async Task<List<PromptTemplate>> GetPromptTemplatesAsync(int? collectionId = null)
    {
        var query = _context.PromptTemplates
            .Include(pt => pt.Collection)
            .Include(pt => pt.Variables)
            .AsQueryable();

        if (collectionId.HasValue)
        {
            query = query.Where(pt => pt.CollectionId == collectionId.Value);
        }

        return await query
            .OrderBy(pt => pt.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Get a prompt template by ID
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <returns>Prompt template with related data, or null if not found</returns>
    public async Task<PromptTemplate?> GetPromptTemplateByIdAsync(int id)
    {
        return await _context.PromptTemplates
            .Include(pt => pt.Collection)
            .Include(pt => pt.Variables)
            .FirstOrDefaultAsync(pt => pt.Id == id);
    }

    /// <summary>
    /// Execute a prompt template with provided variables
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <param name="variables">Variables as JSON string</param>
    /// <returns>Execution result with resolved prompt</returns>
    public async Task<object> ExecutePromptTemplateAsync(int id, string variables)
    {
        var template = await GetPromptTemplateByIdAsync(id);
        if (template == null)
        {
            return new { success = false, error = $"Prompt template with ID {id} not found" };
        }

        try
        {
            var variableValues = JsonSerializer.Deserialize<Dictionary<string, string>>(variables) 
                ?? new Dictionary<string, string>();

            if (!ValidateVariables(template, variableValues))
            {
                return new { success = false, error = "Missing required variables" };
            }

            var resolvedPrompt = ResolvePrompt(template, variableValues);

            // Store execution record
            var execution = new PromptExecution
            {
                PromptTemplateId = template.Id,
                ResolvedPrompt = resolvedPrompt,
                VariableValues = variables,
                ExecutedAt = DateTime.UtcNow,
                AiProvider = "MCP",
                Model = "N/A"
            };

            _context.PromptExecutions.Add(execution);
            await _context.SaveChangesAsync();

            return new
            {
                success = true,
                executionId = execution.Id,
                resolvedPrompt = resolvedPrompt,
                variables = variableValues,
                template = new
                {
                    id = template.Id,
                    name = template.Name,
                    collectionId = template.CollectionId,
                    collectionName = template.Collection?.Name
                }
            };
        }
        catch (JsonException)
        {
            return new { success = false, error = "Invalid JSON format for variables" };
        }
        catch (Exception ex)
        {
            return new { success = false, error = ex.Message };
        }
    }

    /// <summary>
    /// Get execution history for prompts
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <returns>List of prompt executions</returns>
    public async Task<List<PromptExecution>> GetExecutionHistoryAsync(int? promptId = null, int limit = 50)
    {
        var query = _context.PromptExecutions
            .Include(e => e.PromptTemplate)
                .ThenInclude(pt => pt!.Collection)
            .AsQueryable();

        if (promptId.HasValue)
        {
            query = query.Where(e => e.PromptTemplateId == promptId.Value);
        }

        return await query
            .OrderByDescending(e => e.ExecutedAt)
            .Take(limit)
            .ToListAsync();
    }

    /// <summary>
    /// Get execution history for prompts with pagination
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <returns>List of prompt executions</returns>
    public async Task<List<PromptExecution>> GetExecutionHistoryAsync(int pageNumber, int pageSize, int? promptId = null)
    {
        var query = _context.PromptExecutions
            .Include(e => e.PromptTemplate)
                .ThenInclude(pt => pt!.Collection)
            .AsQueryable();

        if (promptId.HasValue)
        {
            query = query.Where(e => e.PromptTemplateId == promptId.Value);
        }

        return await query
            .OrderByDescending(e => e.ExecutedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Get total count of executions
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <returns>Total count of executions</returns>
    public async Task<int> GetTotalExecutionsCountAsync(int? promptId = null)
    {
        var query = _context.PromptExecutions.AsQueryable();

        if (promptId.HasValue)
        {
            query = query.Where(e => e.PromptTemplateId == promptId.Value);
        }
        return await query.CountAsync();
    }

    /// <summary>
    /// Create a new collection
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="description">Optional collection description</param>
    /// <returns>Created collection</returns>
    public async Task<Collection> CreateCollectionAsync(string name, string? description = null)
    {
        var collection = new Collection
        {
            Name = name,
            Description = description ?? "",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Collections.Add(collection);
        await _context.SaveChangesAsync();

        return collection;
    }

    /// <summary>
    /// Update an existing collection
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="name">Updated name</param>
    /// <param name="description">Updated description</param>
    /// <returns>Updated collection, or null if not found</returns>
    public async Task<Collection?> UpdateCollectionAsync(int collectionId, string name, string? description)
    {
        var collection = await _context.Collections
            .Include(c => c.PromptTemplates) // Include PromptTemplates if they are needed by the caller after update
            .FirstOrDefaultAsync(c => c.Id == collectionId);

        if (collection == null)
        {
            return null; // Collection not found
        }

        collection.Name = name;
        collection.Description = description ?? ""; // Handle null description
        collection.UpdatedAt = DateTime.UtcNow;

        try
        {
            // _context.Collections.Update(collection); // EF Core tracks changes, explicit Update not always needed if entity is tracked
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Check if the entity still exists. If not, it was deleted by another user.
            if (!await _context.Collections.AnyAsync(e => e.Id == collectionId))
            {
                return null; // Indicate it was not found (effectively deleted)
            }
            else
            {
                throw; // Re-throw if it still exists but there's another concurrency issue
            }
        }
        return collection;
    }

    /// <summary>
    /// Create a new prompt template
    /// </summary>
    /// <param name="name">Prompt template name</param>
    /// <param name="content">Prompt template content</param>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="description">Optional prompt template description</param>
    /// <returns>Created prompt template</returns>
    public async Task<PromptTemplate> CreatePromptTemplateAsync(string name, string content, int collectionId, string? description = null)
    {
        // Verify collection exists
        var collectionExists = await _context.Collections.AnyAsync(c => c.Id == collectionId);
        if (!collectionExists)
        {
            throw new ArgumentException($"Collection with ID {collectionId} not found", nameof(collectionId));
        }

        var template = new PromptTemplate
        {
            Name = name,
            Description = description ?? "",
            Content = content,
            CollectionId = collectionId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.PromptTemplates.Add(template);
        await _context.SaveChangesAsync();

        // Extract and create variables
        var variableNames = ExtractVariableNames(content);
        foreach (var variableName in variableNames)
        {
            var variable = new PromptVariable
            {
                Name = variableName,
                Type = VariableType.Text,
                PromptTemplateId = template.Id,
                CreatedAt = DateTime.UtcNow
            };
            _context.PromptVariables.Add(variable);
        }

        await _context.SaveChangesAsync();

        // Reload with all relationships
        return await GetPromptTemplateByIdAsync(template.Id) ?? template;
    }

    /// <summary>
    /// Update an existing prompt template
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <param name="name">Updated name</param>
    /// <param name="content">Updated content</param>
    /// <param name="collectionId">Updated collection ID</param>
    /// <param name="description">Updated description</param>
    /// <returns>Updated prompt template, or null if not found</returns>
    public async Task<PromptTemplate?> UpdatePromptTemplateAsync(int promptTemplateId, string name, string content, int collectionId, string? description)
    {
        var existingPrompt = await _context.PromptTemplates
            .Include(p => p.Variables)
            .FirstOrDefaultAsync(p => p.Id == promptTemplateId);

        if (existingPrompt == null)
        {
            return null;
        }

        // Verify collection exists
        var collectionExists = await _context.Collections.AnyAsync(c => c.Id == collectionId);
        if (!collectionExists)
        {
            throw new ArgumentException($"Collection with ID {collectionId} not found", nameof(collectionId));
        }

        existingPrompt.Name = name;
        existingPrompt.Description = description ?? "";
        existingPrompt.Content = content;
        existingPrompt.CollectionId = collectionId; // Ensure CollectionId is updated
        existingPrompt.UpdatedAt = DateTime.UtcNow;

        var newVariableNames = ExtractVariableNames(content);

        var variablesToRemove = existingPrompt.Variables
            .Where(v => !newVariableNames.Contains(v.Name))
            .ToList();
        if (variablesToRemove.Any())
        {
            _context.PromptVariables.RemoveRange(variablesToRemove);
        }

        var currentVariableNames = existingPrompt.Variables.Select(v => v.Name).ToList();
        foreach (var variableName in newVariableNames)
        {
            // Add only if it's not current and not in the list to be removed (in case of name reuse after case change etc.)
            if (!currentVariableNames.Contains(variableName) && !variablesToRemove.Any(v => v.Name == variableName))
            {
                existingPrompt.Variables.Add(new PromptVariable
                {
                    Name = variableName,
                    Type = VariableType.Text, // Default type
                    PromptTemplateId = existingPrompt.Id,
                    CreatedAt = DateTime.UtcNow // Should be UpdatedAt for consistency, or not set here
                });
            }
        }
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle concurrency if necessary, or rethrow
            throw;
        }
        return await GetPromptTemplateByIdAsync(promptTemplateId); // Return the reloaded entity
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
            .OrderByDescending(vc => vc.CreatedAt)
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
        // Verify prompt template exists
        var template = await GetPromptTemplateByIdAsync(promptId);
        if (template == null)
        {
            throw new ArgumentException($"Prompt template with ID {promptId} not found", nameof(promptId));
        }

        // Extract variable names from template
        var expectedVariables = ExtractVariableNames(template.Content);

        // Parse CSV data
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
    /// Execute batch processing with a variable collection
    /// </summary>
    /// <param name="collectionId">Variable collection ID</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>Batch execution result</returns>
    public async Task<object> ExecuteBatchAsync(int collectionId, int promptId)
    {
        var collection = await _context.VariableCollections
            .FirstOrDefaultAsync(vc => vc.Id == collectionId);

        if (collection == null)
        {
            throw new ArgumentException($"Variable collection with ID {collectionId} not found", nameof(collectionId));
        }

        var template = await GetPromptTemplateByIdAsync(promptId);
        if (template == null)
        {
            throw new ArgumentException($"Prompt template with ID {promptId} not found", nameof(promptId));
        }

        // Parse variable sets from JSON
        var variableSets = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(collection.VariableSets)
            ?? new List<Dictionary<string, string>>();

        // Execute batch
        var results = BatchExecute(template, variableSets);

        // Store successful executions in database
        var executions = results.Where(r => r.Success).Select(result => new PromptExecution
        {
            PromptTemplateId = promptId,
            ResolvedPrompt = result.ResolvedPrompt,
            VariableValues = JsonSerializer.Serialize(result.Variables),
            ExecutedAt = DateTime.UtcNow,
            AiProvider = "MCP Batch",
            Model = "N/A"
        }).ToList();

        if (executions.Any())
        {
            _context.PromptExecutions.AddRange(executions);
            await _context.SaveChangesAsync();
        }

        return new
        {
            CollectionName = collection.Name,
            PromptName = template.Name,
            TotalSets = variableSets.Count,
            SuccessfulExecutions = results.Count(r => r.Success),
            FailedExecutions = results.Count(r => !r.Success),
            Results = results
        };
    }

    /// <summary>
    /// Save a list of prompt executions to the database
    /// </summary>
    /// <param name="executions">List of prompt executions</param>
    /// <returns>List of saved prompt executions</returns>
    public async Task<List<PromptExecution>> SavePromptExecutionsAsync(List<PromptExecution> executions)
    {
        if (executions == null || executions.Count == 0)
        {
            return executions ?? [];
        }

        _context.PromptExecutions.AddRange(executions);
        await _context.SaveChangesAsync();
        return executions; // IDs will be populated by EF Core after SaveChangesAsync
    }

    /// <summary>
    /// Delete a prompt template by ID
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <returns>True if the prompt template was deleted, false otherwise</returns>
    public async Task<bool> DeletePromptTemplateAsync(int promptTemplateId)
    {
        var promptTemplate = await _context.PromptTemplates
            .Include(p => p.Variables) // Include children if they need manual removal or logging
            .Include(p => p.Executions)
            .Include(p => p.VariableCollections)
            .FirstOrDefaultAsync(p => p.Id == promptTemplateId);

        if (promptTemplate != null)
        {
            // EF Core with proper cascade delete configuration should handle related entities.
            // If not, or if specific logic is needed:
            // _context.PromptVariables.RemoveRange(promptTemplate.Variables);
            // _context.PromptExecutions.RemoveRange(promptTemplate.Executions);
            // _context.VariableCollections.RemoveRange(promptTemplate.VariableCollections);
            _context.PromptTemplates.Remove(promptTemplate);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Delete a collection by ID
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <returns>True if the collection was deleted, false otherwise</returns>
    public async Task<bool> DeleteCollectionAsync(int collectionId)
    {
        var collection = await _context.Collections
            .Include(c => c.PromptTemplates)
            .FirstOrDefaultAsync(c => c.Id == collectionId);

        if (collection == null)
        {
            return false; // Collection not found
        }

        _context.Collections.Remove(collection);
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            return false; 
        }
    }

    // Helper DTOs for deserialization within the service, mirroring those in ImportModel.
    // Consider moving these to a shared DTOs location if used elsewhere, or keep them internal to the service method if not.
    // For this example, I'm redeclaring simplified versions. Ensure they match your JSON structure.
    private class ServiceImportData { public ServiceCollectionData? Collection { get; set; } }
    private class ServiceAlternativeImportData { public ServiceCollectionMetadata? Collection { get; set; } public List<ServicePromptData>? Prompts { get; set; } }
    private class ServiceCollectionMetadata { public string? Name { get; set; } public string? Description { get; set; } }
    private class ServiceCollectionData { public string? Name { get; set; } public string? Description { get; set; } public List<ServicePromptData>? Prompts { get; set; } }
    private class ServicePromptData { public string? Name { get; set; } public string? Description { get; set; } public string? Content { get; set; } public List<ServiceVariableData>? Variables { get; set; } public List<ServiceExecutionData>? ExecutionHistory { get; set; } }
    private class ServiceVariableData { public string? Name { get; set; } public string? Description { get; set; } public string? DefaultValue { get; set; } public string? Type { get; set; } }
    private class ServiceExecutionData { public DateTime ExecutedAt { get; set; } public string? VariableValues { get; set; } public string? ResolvedPrompt { get; set; } }

    public async Task<Collection?> ImportCollectionFromJsonAsync(string jsonContent, bool importExecutionHistory, bool overwriteExisting)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        ServiceCollectionData? collectionData = null;
        List<ServicePromptData>? promptsData = null;

        // Attempt to deserialize standard format
        var importData = JsonSerializer.Deserialize<ServiceImportData>(jsonContent, options);
        if (importData?.Collection != null)
        {
            collectionData = importData.Collection;
            promptsData = collectionData.Prompts;
        }
        else // Attempt to deserialize alternative format
        {
            var altFormat = JsonSerializer.Deserialize<ServiceAlternativeImportData>(jsonContent, options);
            if (altFormat?.Collection != null)
            {
                collectionData = new ServiceCollectionData
                {
                    Name = altFormat.Collection.Name,
                    Description = altFormat.Collection.Description,
                    Prompts = altFormat.Prompts
                };
                promptsData = altFormat.Prompts;
            }
        }

        if (collectionData == null)
        {
            // If data is still null, the format is unrecognized or essential parts are missing.
            return null; // Or throw ArgumentException("Invalid import file format.")
        }

        var collectionName = collectionData.Name ?? "Untitled Collection";
        var existingCollection = await _context.Collections
            .Include(c => c.PromptTemplates)
                .ThenInclude(pt => pt.Variables)
            .Include(c => c.PromptTemplates)
                .ThenInclude(pt => pt.Executions)
            .FirstOrDefaultAsync(c => c.Name == collectionName);

        Collection targetCollection;

        if (existingCollection != null)
        {
            if (overwriteExisting)
            {
                // A more robust delete would be a separate service call, e.g., DeleteCollectionAsync(existingCollection.Id)
                _context.Collections.Remove(existingCollection);
                await _context.SaveChangesAsync(); // Commit deletion before adding new

                targetCollection = new Collection { Name = collectionName }; // Re-create
            }
            else
            {
                // Find a unique name
                var baseName = collectionName;
                var counter = 1;
                while (await _context.Collections.AnyAsync(c => c.Name == collectionName))
                {
                    collectionName = $"{baseName} ({counter++})";
                }
                targetCollection = new Collection { Name = collectionName };
            }
        }
        else
        {
            targetCollection = new Collection { Name = collectionName };
        }

        targetCollection.Description = collectionData.Description ?? "";
        targetCollection.CreatedAt = DateTime.UtcNow;
        targetCollection.UpdatedAt = DateTime.UtcNow;
        targetCollection.PromptTemplates = new List<PromptTemplate>(); // Initialize navigation property

        if (existingCollection == null || overwriteExisting) // Add if it's new or if we overwrote
        {
            _context.Collections.Add(targetCollection);
        }
        // If !OverwriteExisting and existingCollection != null, targetCollection is a new entity to be added.
        // If existingCollection != null and overwriteExisting, targetCollection is also a new entity.
        // The logic above already adds targetCollection if it's truly new or replacing an old one.
        // If we were updating an existing one (not overwriting), we wouldn't Add, just modify.

        // Save to get targetCollection.Id if it's new
        await _context.SaveChangesAsync();


        if (promptsData != null)
        {
            foreach (var promptData in promptsData)
            {
                var prompt = new PromptTemplate
                {
                    Name = promptData.Name ?? "Untitled Prompt",
                    Description = promptData.Description,
                    Content = promptData.Content ?? "",
                    CollectionId = targetCollection.Id, // Assign foreign key
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Variables = new List<PromptVariable>(),
                    Executions = new List<PromptExecution>()
                };

                var variableNames = ExtractVariableNames(prompt.Content); // Use existing service method
                foreach (var variableName in variableNames)
                {
                    var importedVarData = promptData.Variables?.FirstOrDefault(v => v.Name == variableName);
                    prompt.Variables.Add(new PromptVariable
                    {
                        Name = variableName,
                        Description = importedVarData?.Description,
                        DefaultValue = importedVarData?.DefaultValue,
                        Type = Enum.TryParse<VariableType>(importedVarData?.Type ?? "Text", true, out var type) ? type : VariableType.Text,
                        CreatedAt = DateTime.UtcNow,
                        PromptTemplateId = prompt.Id // Will be set by EF if relationship is configured
                    });
                }

                if (importExecutionHistory && promptData.ExecutionHistory != null)
                {
                    foreach (var executionData in promptData.ExecutionHistory)
                    {
                        prompt.Executions.Add(new PromptExecution
                        {
                            VariableValues = executionData.VariableValues,
                            ResolvedPrompt = executionData.ResolvedPrompt ?? "",
                            ExecutedAt = executionData.ExecutedAt,
                            AiProvider = "Imported",
                            Model = "N/A",
                            PromptTemplateId = prompt.Id // Will be set by EF
                        });
                    }
                }
                // Add the fully constructed prompt to the context (or to the collection's navigation property if already tracked)
                _context.PromptTemplates.Add(prompt);
            }
        }        await _context.SaveChangesAsync(); // Save all prompts, variables, executions
        return await GetCollectionByIdAsync(targetCollection.Id); // Return the re-fetched collection with all includes
    }
}
