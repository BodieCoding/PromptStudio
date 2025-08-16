using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Interfaces.Data;
using PromptStudio.Core.Interfaces.Templates;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Templates;
using PromptStudio.Core.Exceptions;

namespace PromptStudio.Core.Services;

/// <summary>
/// Service implementation for comprehensive prompt template lifecycle management within library contexts.
/// Provides template creation, versioning, content validation, and collaborative features.
/// </summary>
public class PromptTemplateService(IPromptStudioDbContext context) : IPromptTemplateService
{
    private readonly IPromptStudioDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    #region Template Lifecycle Operations

    public async Task<PromptTemplate> CreateTemplateAsync(
        Guid libraryId,
        string name,
        string content,
        string description,
        TemplateCategory category,
        string userId,
        IEnumerable<PromptVariable>? variables = null,
        IEnumerable<string>? tags = null,
        bool requiresApproval = false,
        CancellationToken cancellationToken = default)
    {
        
        // Validate library exists
        var library = await _context.PromptLibraries
            .Where(l => l.Id == libraryId && l.DeletedAt == null)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new ResourceNotFoundException($"Library with ID {libraryId} not found or inaccessible");

        // TODO: Validate user has access
        
        // Check for name uniqueness within library
        var existingTemplate = await _context.PromptTemplates
            .Where(t => t.PromptLibraryId == libraryId && 
                       t.Name == name && 
                       t.DeletedAt == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingTemplate != null)
        {
            throw new DuplicateTemplateNameException($"Template with name '{name}' already exists in library");
        }

        // Create new template
        var template = new PromptTemplate
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            PromptLibraryId = libraryId,
            Content = new PromptContent { Content = content },
            Category = category,
            RequiresApproval = requiresApproval,
            Version = "1.0.0",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = userId
        };

        // Add variables if provided
        if (variables != null)
        {
            template.Variables = [.. variables];
        }

        // Add tags if provided (assume tags is a JSON string property)
        if (tags != null)
        {
            template.Tags = System.Text.Json.JsonSerializer.Serialize(tags);
        }

        _context.PromptTemplates.Add(template);
        await _context.SaveChangesAsync(cancellationToken);

        return template;
    }

    public async Task<PromptTemplate?> GetTemplateAsync(
        Guid templateId,
        string userId,
        string? version = null,
        bool includeVariables = true,
        bool includeExecutions = false,
        bool includeAnalytics = false,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement access control validation
        // For now, return the template if found
        
        var query = _context.PromptTemplates.AsQueryable();

        if (includeVariables)
        {
            query = query.Include(t => t.Variables);
        }

        if (includeExecutions)
        {
            query = query.Include(t => t.Executions.Take(10));
        }

        query = query.Where(t => t.Id == templateId && t.DeletedAt == null);

        if (!string.IsNullOrEmpty(version))
        {
            query = query.Where(t => t.Version == version);
        }

        var template = await query.FirstOrDefaultAsync(cancellationToken);

        return template;
    }

    public async Task<PagedResult<PromptTemplate>> GetTemplatesAsync(
        Guid libraryId,
        string userId,
        TemplateCategory? category = null,
        IEnumerable<string>? tags = null,
        string? searchTerm = null,
        bool? requiresApproval = null,
        bool? hasExecutions = null,
        bool includeArchived = false,
        string sortBy = "Name",
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = _context.PromptTemplates
            .Where(t => t.PromptLibraryId == libraryId);

        if (!includeArchived)
        {
            query = query.Where(t => t.DeletedAt == null);
        }

        if (category.HasValue)
        {
            query = query.Where(t => t.Category == category.Value);
        }

        if (requiresApproval.HasValue)
        {
            query = query.Where(t => t.RequiresApproval == requiresApproval.Value);
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(t => t.Name.Contains(searchTerm) ||
                                   (t.Description != null && t.Description.Contains(searchTerm)));
        }

        // TODO: Implement tag filtering when tag structure is clarified
        // TODO: Implement hasExecutions filtering

        // Apply sorting
        query = sortBy.ToLower() switch
        {
            "name" => query.OrderBy(t => t.Name),
            "created" => query.OrderByDescending(t => t.CreatedAt),
            "updated" => query.OrderByDescending(t => t.UpdatedAt),
            _ => query.OrderBy(t => t.Name)
        };

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<PromptTemplate>
        {
            Items = items,
            TotalCount = totalCount,
            Skip = (pageNumber - 1) * pageSize,
            Take = pageSize
        };
    }

    #endregion

    #region Template Content and Variable Management

    public async Task<PromptTemplate> UpdateTemplateContentAsync(
        Guid templateId,
        string userId,
        string content,
        string changeDescription,
        IEnumerable<PromptVariable>? variables = null,
        bool majorVersion = false,
        CancellationToken cancellationToken = default)
    {
        var template = await _context.PromptTemplates
            .Include(t => t.Variables)
            .Where(t => t.Id == templateId && t.DeletedAt == null)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new ResourceNotFoundException($"Template with ID {templateId} not found");

        // TODO: Implement access control validation
        // TODO: Implement changeDescription

        // Update content
        template.Content = new PromptContent { Content = content };

        // Update variables if provided
        if (variables != null)
        {
            template.Variables = variables.ToList();
        }

        // TODO: Version control
        var versionParts = template.Version.Split('.');
        if (majorVersion)
        {
            template.Version = $"{int.Parse(versionParts[0]) + 1}.0.0";
        }
        else
        {
            template.Version = $"{versionParts[0]}.{int.Parse(versionParts[1]) + 1}.0";
        }

        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return template;
    }

    public async Task<PromptTemplate> UpdateTemplateMetadataAsync(
        Guid templateId,
        string userId,
        string? name = null,
        string? description = null,
        TemplateCategory? category = null,
        IEnumerable<string>? tags = null,
        bool? requiresApproval = null,
        CancellationToken cancellationToken = default)
    {
        var template = await _context.PromptTemplates
            .Where(t => t.Id == templateId && t.DeletedAt == null)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new ResourceNotFoundException($"Template with ID {templateId} not found");

        // TODO: Implement access control validation

        // Update metadata fields
        if (!string.IsNullOrEmpty(name))
        {
            // Check for name uniqueness
            var existingTemplate = await _context.PromptTemplates
                .Where(t => t.PromptLibraryId == template.PromptLibraryId &&
                           t.Name == name &&
                           t.Id != templateId &&
                           t.DeletedAt == null)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingTemplate != null)
            {
                throw new DuplicateTemplateNameException($"Template with name '{name}' already exists in library");
            }

            template.Name = name;
        }

        if (description != null)
        {
            template.Description = description;
        }

        if (category.HasValue)
        {
            template.Category = category.Value;
        }

        if (tags != null)
        {
            template.Tags = System.Text.Json.JsonSerializer.Serialize(tags);
        }

        if (requiresApproval.HasValue)
        {
            template.RequiresApproval = requiresApproval.Value;
        }

        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return template;
    }

    public async Task<TemplateAnalysisResult> AnalyzeTemplateAsync(
        Guid templateId,
        string userId,
        string? version = null,
        bool includePerformanceMetrics = false,
        CancellationToken cancellationToken = default)
    {
        var template = await GetTemplateAsync(templateId, userId, version, true, includePerformanceMetrics, false, cancellationToken) ?? throw new ResourceNotFoundException($"Template with ID {templateId} not found");

        // TODO: Implement comprehensive template analysis
        // For now, return a basic analysis result

        return new TemplateAnalysisResult
        {
            ComplexityScore = 0.5, // Placeholder for complexity score
            VariableCount = template.Variables?.Count ?? 0,
            TokenCount = template.Content?.Content?.Length ?? 0, // Simplified token count
            QualityIssues = 
            [
                "No major quality issues detected"
            ],
            OptimizationSuggestions =
            [
                "Consider adding more specific variable descriptions",
                "Template content could benefit from clearer structure"
            ],
            Metrics = new Dictionary<string, object>
            {
                { "variableCount", template.Variables?.Count ?? 0 },
                { "tokenCount", template.Content?.Content?.Length ?? 0 },
                { "executionTime", 100 } // Placeholder for execution time in ms
            }
        };
    }

    #endregion

    #region Versioning and History

    public async Task<PagedResult<TemplateVersion>> GetTemplateVersionHistoryAsync(
        Guid templateId,
        string userId,
        bool includeContent = false,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement proper version history when TemplateVersion entity is available
        // For now, return empty result
        return new PagedResult<TemplateVersion>
        {
            Items = [],
            TotalCount = 0,
            Skip = (pageNumber - 1) * pageSize,
            Take = pageSize
        };
       
    }

    public async Task<TemplateComparisonResult> CompareTemplateVersionsAsync(
        Guid templateId,
        string userId,
        string fromVersion,
        string toVersion,
        bool includeVariableAnalysis = true,
        CancellationToken cancellationToken = default)
    {
        // TODO: Implement version comparison
        return new TemplateComparisonResult
        {
            Version1 = fromVersion,
            Version2 = toVersion,
            ContentDifferences = [],
            VariableChanges = [],
            MetricChanges = []
        };        
    }

    public async Task<PromptTemplate> RevertTemplateVersionAsync(
        Guid templateId,
        string userId,
        string targetVersion,
        string revertReason,
        CancellationToken cancellationToken = default)
    {
        var template = await _context.PromptTemplates
            .Where(t => t.Id == templateId && t.DeletedAt == null)
            .FirstOrDefaultAsync(cancellationToken) ?? throw new ResourceNotFoundException($"Template with ID {templateId} not found");

        // TODO: Implement proper version revert logic
        // For now, just update the version

        template.Version = targetVersion;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return template;
    }

    public async Task<bool> DeleteTemplateAsync(
        Guid templateId,
        string userId,
        string deleteReason,
        CancellationToken cancellationToken = default)
    {
        var template = await _context.PromptTemplates
            .Where(t => t.Id == templateId && t.DeletedAt == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (template == null)
        {
            return false;
        }

        // TODO: Implement access control validation

        // Soft delete
        template.DeletedAt = DateTime.UtcNow;
        template.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    #endregion

    #region Template Discovery and Recommendations

    public async Task<PagedResult<PromptTemplate>> SearchTemplatesAsync(
        string userId,
        string searchTerm,
        TemplateCategory? category = null,
        IEnumerable<string>? tags = null,
        Guid? libraryId = null,
        bool includeInactive = false,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = _context.PromptTemplates.AsQueryable();

        if (!includeInactive)
        {
            query = query.Where(t => t.DeletedAt == null);
        }

        if (libraryId.HasValue)
        {
            query = query.Where(t => t.PromptLibraryId == libraryId.Value);
        }

        if (category.HasValue)
        {
            query = query.Where(t => t.Category == category.Value);
        }

        // Search in name, description, and content
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(t => t.Name.Contains(searchTerm) ||
                                   (t.Description != null && t.Description.Contains(searchTerm)));
            // TODO: Add content search when PromptContent structure is clarified
        }

        // TODO: Implement access control filtering
        // TODO: Implement tag filtering

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(t => t.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<PromptTemplate>
        {
            Items = items,
            TotalCount = totalCount,
            Skip = (pageNumber - 1) * pageSize,
            Take = pageSize
        };
    }

    #endregion
}