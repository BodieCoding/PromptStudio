using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace PromptStudio.Core.Services;

/// <summary>
/// Service for managing prompt templates
/// </summary>
public class PromptTemplateService : IPromptTemplateService
{
    #region Private Fields

    private static readonly Regex VariablePattern = new(@"\{\{([^{}]+)\}\}", RegexOptions.Compiled);
    private readonly IPromptStudioDbContext _context;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the PromptTemplateService
    /// </summary>
    /// <param name="context">Database context for data access</param>
    public PromptTemplateService(IPromptStudioDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    #endregion

    #region Template CRUD Operations

    /// <summary>
    /// Get prompt templates, optionally filtered by library
    /// </summary>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <returns>List of prompt templates</returns>
    public async Task<List<PromptTemplate>> GetPromptTemplatesAsync(int? libraryId = null)
    {
        var query = _context.PromptTemplates
            .Include(pt => pt.PromptLibrary)
            .Include(pt => pt.Variables)
            .AsQueryable();

        if (libraryId.HasValue)
        {
            query = query.Where(pt => pt.PromptLibraryId == libraryId.Value);
        }

        return await query.OrderBy(pt => pt.Name).ToListAsync();
    }

    /// <summary>
    /// Get a prompt template by ID
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <returns>Prompt template with related data, or null if not found</returns>
    public async Task<PromptTemplate?> GetPromptTemplateByIdAsync(int id)
    {
        return await _context.PromptTemplates
            .Include(pt => pt.PromptLibrary)
            .Include(pt => pt.Variables)
            .FirstOrDefaultAsync(pt => pt.Id == id);
    }

    /// <summary>
    /// Create a new prompt template
    /// </summary>
    /// <param name="name">Prompt template name</param>
    /// <param name="content">Prompt template content</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="description">Optional prompt template description</param>
    /// <returns>Created prompt template</returns>
    public async Task<PromptTemplate> CreatePromptTemplateAsync(string name, string content, int libraryId, string? description = null)
    {
        // Verify library exists
        var libraryExists = await _context.PromptLibraries.AnyAsync(l => l.Id == libraryId);
        if (!libraryExists)
        {
            throw new ArgumentException($"Library with ID {libraryId} not found", nameof(libraryId));
        }

        // Check if name is unique within the library
        var nameExists = await _context.PromptTemplates
            .AnyAsync(pt => pt.PromptLibraryId == libraryId && pt.Name == name);
        if (nameExists)
        {
            throw new ArgumentException($"A template with name '{name}' already exists in this library", nameof(name));
        }

        var template = new PromptTemplate
        {
            Name = name,
            Description = description ?? "",
            Content = content,
            PromptLibraryId = libraryId,
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
    /// <param name="libraryId">Updated library ID</param>
    /// <param name="description">Updated description</param>
    /// <returns>Updated prompt template, or null if not found</returns>
    public async Task<PromptTemplate?> UpdatePromptTemplateAsync(int promptTemplateId, string name, string content, int libraryId, string? description)
    {
        var existingPrompt = await _context.PromptTemplates
            .Include(p => p.Variables)
            .FirstOrDefaultAsync(p => p.Id == promptTemplateId);

        if (existingPrompt == null)
        {
            return null;
        }

        // Verify library exists
        var libraryExists = await _context.PromptLibraries.AnyAsync(l => l.Id == libraryId);
        if (!libraryExists)
        {
            throw new ArgumentException($"Library with ID {libraryId} not found", nameof(libraryId));
        }

        // Check if name is unique within the library (excluding current template)
        var nameExists = await _context.PromptTemplates
            .AnyAsync(pt => pt.PromptLibraryId == libraryId && pt.Name == name && pt.Id != promptTemplateId);
        if (nameExists)
        {
            throw new ArgumentException($"A template with name '{name}' already exists in this library", nameof(name));
        }

        existingPrompt.Name = name;
        existingPrompt.Description = description ?? "";
        existingPrompt.Content = content;
        existingPrompt.PromptLibraryId = libraryId;
        existingPrompt.UpdatedAt = DateTime.UtcNow;

        var newVariableNames = ExtractVariableNames(content);

        // Remove variables that are no longer needed
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
            if (!currentVariableNames.Contains(variableName) && !variablesToRemove.Any(v => v.Name == variableName))
            {
                existingPrompt.Variables.Add(new PromptVariable
                {
                    Name = variableName,
                    Type = VariableType.Text,
                    PromptTemplateId = existingPrompt.Id,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return await GetPromptTemplateByIdAsync(promptTemplateId);
    }

    /// <summary>
    /// Delete a prompt template by ID
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <returns>True if the prompt template was deleted, false otherwise</returns>
    public async Task<bool> DeletePromptTemplateAsync(int promptTemplateId)
    {
        var promptTemplate = await _context.PromptTemplates
            .Include(pt => pt.Variables)
            .Include(pt => pt.Executions)
            .FirstOrDefaultAsync(pt => pt.Id == promptTemplateId);

        if (promptTemplate == null)
        {
            return false;
        }

        try
        {
            _context.PromptTemplates.Remove(promptTemplate);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion

    #region Variable Management

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

    #endregion

    #region Template Discovery

    /// <summary>
    /// Search prompt templates by name or content
    /// </summary>
    /// <param name="searchTerm">Search term to match against name or content</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <returns>List of matching prompt templates</returns>
    public async Task<List<PromptTemplate>> SearchPromptTemplatesAsync(string searchTerm, int? libraryId = null)
    {
        var query = _context.PromptTemplates
            .Include(pt => pt.PromptLibrary)
            .Include(pt => pt.Variables)
            .AsQueryable();

        if (libraryId.HasValue)
        {
            query = query.Where(pt => pt.PromptLibraryId == libraryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(pt => 
                pt.Name.Contains(searchTerm) || 
                pt.Description.Contains(searchTerm) ||
                pt.Content.Contains(searchTerm));
        }

        return await query.OrderBy(pt => pt.Name).ToListAsync();
    }

    /// <summary>
    /// Get prompt templates by tags
    /// </summary>
    /// <param name="tags">Tags to filter by</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <returns>List of matching prompt templates</returns>
    public async Task<List<PromptTemplate>> GetPromptTemplatesByTagsAsync(List<string> tags, int? libraryId = null)
    {
        // Note: This assumes a Tags property or related entity exists on PromptTemplate
        // For now, we'll return an empty list as this feature isn't implemented yet
        // TODO: Implement tagging system when Tags are added to the domain model
        
        var query = _context.PromptTemplates
            .Include(pt => pt.PromptLibrary)
            .Include(pt => pt.Variables)
            .AsQueryable();

        if (libraryId.HasValue)
        {
            query = query.Where(pt => pt.PromptLibraryId == libraryId.Value);
        }

        // TODO: Add tag filtering when tagging system is implemented
        // For now, return all templates in the library
        return await query.OrderBy(pt => pt.Name).ToListAsync();
    }

    #endregion

    #region Template Validation

    /// <summary>
    /// Validate a prompt template's content and structure
    /// </summary>
    /// <param name="content">Template content to validate</param>
    /// <returns>Validation result with any errors or warnings</returns>
    public (bool IsValid, List<string> Errors, List<string> Warnings) ValidateTemplateContent(string content)
    {
        var errors = new List<string>();
        var warnings = new List<string>();

        if (string.IsNullOrWhiteSpace(content))
        {
            errors.Add("Template content cannot be empty");
            return (false, errors, warnings);
        }

        // Check for malformed variable syntax
        var openBraces = content.Count(c => c == '{');
        var closeBraces = content.Count(c => c == '}');
        
        if (openBraces != closeBraces)
        {
            errors.Add("Mismatched curly braces in template content");
        }

        // Check for incomplete variable declarations
        var incompleteVariables = Regex.Matches(content, @"\{[^{}]*\}(?!\})")
            .Where(m => !m.Value.StartsWith("{{") || !m.Value.EndsWith("}}"))
            .ToList();

        if (incompleteVariables.Any())
        {
            errors.Add("Found incomplete variable declarations. Variables must be wrapped in double braces: {{variableName}}");
        }

        // Check for empty variable names
        var emptyVariables = Regex.Matches(content, @"\{\{\s*\}\}")
            .ToList();

        if (emptyVariables.Any())
        {
            errors.Add("Found empty variable declarations. Variable names cannot be empty: {{}}");
        }

        // Check for duplicate variable names
        var variableNames = ExtractVariableNames(content);
        var duplicates = variableNames.GroupBy(v => v)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicates.Any())
        {
            warnings.Add($"Found duplicate variable names: {string.Join(", ", duplicates)}");
        }

        // Check for very long variable names
        var longVariableNames = variableNames.Where(v => v.Length > 50).ToList();
        if (longVariableNames.Any())
        {
            warnings.Add($"Found very long variable names (>50 characters): {string.Join(", ", longVariableNames)}");
        }

        // Check if content is very long
        if (content.Length > 10000)
        {
            warnings.Add("Template content is very long (>10,000 characters). Consider breaking it into smaller templates.");
        }

        return (errors.Count == 0, errors, warnings);
    }

    /// <summary>
    /// Check if a template name is unique within a library
    /// </summary>
    /// <param name="name">Template name to check</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="excludeTemplateId">Optional template ID to exclude from uniqueness check</param>
    /// <returns>True if name is unique, false otherwise</returns>
    public async Task<bool> IsTemplateNameUniqueAsync(string name, int libraryId, int? excludeTemplateId = null)
    {
        var query = _context.PromptTemplates
            .Where(pt => pt.PromptLibraryId == libraryId && pt.Name == name);

        if (excludeTemplateId.HasValue)
        {
            query = query.Where(pt => pt.Id != excludeTemplateId.Value);
        }

        return !await query.AnyAsync();
    }

    #endregion
}
