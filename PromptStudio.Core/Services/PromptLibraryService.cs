using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Interfaces.Data;
using PromptStudio.Core.Interfaces.Templates;
using PromptStudio.Core.Interfaces.Lab;
using PromptStudio.Core.Interfaces.Library;
using PromptStudio.Core.DTOs.Statistics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace PromptStudio.Core.Services;

/// <summary>
/// Service for managing prompt libraries
/// </summary>
public class PromptLibraryService : IPromptLibraryService
{
    #region Private Fields

    private readonly IPromptStudioDbContext _context;
    private readonly IPromptTemplateService _promptTemplateService;
    private readonly IPromptLabService _promptLabService;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the PromptLibraryService
    /// </summary>
    /// <param name="context">Database context for data access</param>
    /// <param name="promptTemplateService">Prompt template service for template operations</param>
    /// <param name="promptLabService">Prompt lab service for lab operations</param>
    public PromptLibraryService(
        IPromptStudioDbContext context,
        IPromptTemplateService promptTemplateService,
        IPromptLabService promptLabService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _promptTemplateService = promptTemplateService ?? throw new ArgumentNullException(nameof(promptTemplateService));
        _promptLabService = promptLabService ?? throw new ArgumentNullException(nameof(promptLabService));
    }

    #endregion

    #region Library CRUD Operations   

    /// <summary>
    /// Get all prompt libraries
    /// </summary>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <returns>List of prompt libraries with their prompt templates</returns>
    public async Task<List<PromptLibrary>> GetLibrariesAsync(Guid? promptLabId = null)
    {
        var query = _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Apply soft delete filter
            .Include(l => l.PromptTemplates.Where(pt => pt.DeletedAt == null))
                .ThenInclude(pt => pt.Variables)
            .AsQueryable();

        if (promptLabId.HasValue)
        {
            query = query.Where(l => l.PromptLabId == promptLabId.Value);
        }

        return await query.OrderBy(l => l.Name).ToListAsync();
    }

    /// <summary>
    /// Get a prompt library by ID
    /// </summary>
    /// <param name="id">Library ID</param>
    /// <returns>Library with prompt templates, or null if not found</returns>
    public async Task<PromptLibrary?> GetLibraryByIdAsync(Guid id)
    {
        return await _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Apply soft delete filter
            .Include(l => l.PromptTemplates.Where(pt => pt.DeletedAt == null))
                .ThenInclude(pt => pt.Variables)
            .FirstOrDefaultAsync(l => l.Id == id);
    }   
    
    /// <summary>
    /// Create a new prompt library
    /// </summary>
    /// <param name="name">Library name</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="description">Optional library description</param>
    /// <returns>Created library</returns>
    public async Task<PromptLibrary> CreateLibraryAsync(string name, Guid promptLabId, string? description = null)
    {
        // Validate input using the PromptLabService
        var labExists = await _promptLabService.LabExistsAsync(promptLabId);
        if (!labExists)
        {
            throw new ArgumentException($"Prompt lab with ID {promptLabId} not found", nameof(promptLabId));
        }

        var validation = await ValidateLibraryDataAsync(name, promptLabId);
        if (!validation.IsValid)
        {
            throw new ArgumentException(string.Join("; ", validation.Errors));
        }

        var library = new PromptLibrary
        {
            Name = name,
            Description = description ?? "",
            PromptLabId = promptLabId,
            Visibility = LibraryVisibility.Private, // Default to private
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.PromptLibraries.Add(library);
        await _context.SaveChangesAsync();

        return library;
    }

    /// <summary>
    /// Update an existing prompt library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="name">Updated name</param>
    /// <param name="description">Updated description</param>
    /// <param name="promptLabId">Updated prompt lab ID</param>
    /// <returns>Updated library, or null if not found</returns>
    public async Task<PromptLibrary?> UpdateLibraryAsync(Guid libraryId, string name, string? description, Guid? promptLabId = null)
    {
        var library = await _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Apply soft delete filter
            .Include(l => l.PromptTemplates.Where(pt => pt.DeletedAt == null))
            .FirstOrDefaultAsync(l => l.Id == libraryId);

        if (library == null)
        {
            return null;
        }

        // Use existing prompt lab ID if not provided
        var targetPromptLabId = promptLabId ?? library.PromptLabId;

        // Validate the target lab exists using PromptLabService
        var labExists = await _promptLabService.LabExistsAsync(targetPromptLabId);
        if (!labExists)
        {
            throw new ArgumentException($"Prompt lab with ID {targetPromptLabId} not found", nameof(promptLabId));
        }

        // Validate the update
        var validation = await ValidateLibraryDataAsync(name, targetPromptLabId, libraryId);
        if (!validation.IsValid)
        {
            throw new ArgumentException(string.Join("; ", validation.Errors));
        }

        library.Name = name;
        library.Description = description ?? "";
        library.PromptLabId = targetPromptLabId;
        library.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Check if the library still exists
            if (await _context.PromptLibraries.AnyAsync(l => l.Id == libraryId && l.DeletedAt == null))
            {
                throw;
            }
            else
            {
                throw;
            }
        }

        return library;
    }

    /// <summary>
    /// Delete a prompt library by ID (soft delete)
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <returns>True if the library was deleted, false otherwise</returns>
    public async Task<bool> DeleteLibraryAsync(Guid libraryId)
    {
        var library = await _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Only get non-deleted libraries
            .Include(l => l.PromptTemplates.Where(pt => pt.DeletedAt == null))
                .ThenInclude(pt => pt.Variables)
            .FirstOrDefaultAsync(l => l.Id == libraryId);

        if (library == null)
        {
            return false;
        }

        try
        {
            // Soft delete the library and its templates
            library.DeletedAt = DateTime.UtcNow;
            library.UpdatedAt = DateTime.UtcNow;

            // Soft delete all templates in the library
            foreach (var template in library.PromptTemplates)
            {
                template.DeletedAt = DateTime.UtcNow;
                template.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #endregion

    #region Library Discovery    

    /// <summary>
    /// Search prompt libraries by name or description
    /// </summary>
    /// <param name="searchTerm">Search term to match against name or description</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <returns>List of matching prompt libraries</returns>
    public async Task<List<PromptLibrary>> SearchLibrariesAsync(string searchTerm, Guid? promptLabId = null)
    {
        var query = _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Apply soft delete filter
            .Include(l => l.PromptTemplates.Where(pt => pt.DeletedAt == null))
            .AsQueryable();

        if (promptLabId.HasValue)
        {
            query = query.Where(l => l.PromptLabId == promptLabId.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(l =>
                l.Name.Contains(searchTerm) ||
                (l.Description != null && l.Description.Contains(searchTerm)));
        }

        return await query.OrderBy(l => l.Name).ToListAsync();
    }

    /// <summary>
    /// Get libraries by visibility level
    /// </summary>
    /// <param name="visibility">Visibility level to filter by</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <returns>List of libraries matching visibility criteria</returns>
    public async Task<List<PromptLibrary>> GetLibrariesByVisibilityAsync(LibraryVisibility visibility, Guid? promptLabId = null)
    {
        var query = _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Apply soft delete filter
            .Include(l => l.PromptTemplates.Where(pt => pt.DeletedAt == null))
            .Where(l => l.Visibility == visibility)
            .AsQueryable();

        if (promptLabId.HasValue)
        {
            query = query.Where(l => l.PromptLabId == promptLabId.Value);
        }

        return await query.OrderBy(l => l.Name).ToListAsync();
    }

    /// <summary>
    /// Get recently updated libraries
    /// </summary>
    /// <param name="daysBack">Number of days to look back for updates</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <returns>List of recently updated libraries</returns>
    public async Task<List<PromptLibrary>> GetRecentlyUpdatedLibrariesAsync(int daysBack = 7, Guid? promptLabId = null)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-daysBack);

        var query = _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Apply soft delete filter
            .Include(l => l.PromptTemplates.Where(pt => pt.DeletedAt == null))
            .Where(l => l.UpdatedAt >= cutoffDate)
            .AsQueryable();

        if (promptLabId.HasValue)
        {
            query = query.Where(l => l.PromptLabId == promptLabId.Value);
        }

        return await query.OrderByDescending(l => l.UpdatedAt).ToListAsync();
    }

    #endregion

    #region Library Statistics    

    /// <summary>
    /// Get library statistics including template count, execution count, etc.
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <returns>Library statistics</returns>
    public async Task<LibraryStatistics> GetLibraryStatisticsAsync(Guid libraryId)
    {
        var library = await _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Apply soft delete filter
            .Include(l => l.PromptTemplates.Where(pt => pt.DeletedAt == null))
                .ThenInclude(pt => pt.Variables)
            .FirstOrDefaultAsync(l => l.Id == libraryId);

        if (library == null)
        {
            throw new ArgumentException($"Library with ID {libraryId} not found", nameof(libraryId));
        }

        var templateCount = library.PromptTemplates.Count;
        // TODO: Update execution count when PromptExecution is migrated to Guid IDs
        var totalExecutions = 0; // Temporarily disabled due to ID type mismatch
        var uniqueVariables = library.PromptTemplates
            .SelectMany(pt => pt.Variables)
            .Select(v => v.Name)
            .Distinct()
            .Count();
        var lastTemplateCreated = library.PromptTemplates.Any()
            ? library.PromptTemplates.Max(pt => pt.CreatedAt)
            : (DateTime?)null;
        // TODO: Update last execution when PromptExecution relationship is fixed

        return new LibraryStatistics
        {
            LibraryId = library.Id,
            LibraryName = library.Name,
            TotalTemplates = templateCount,
            ActiveTemplates = library.PromptTemplates.Count(pt => pt.Status == TemplateStatus.Published),
            DraftTemplates = library.PromptTemplates.Count(pt => pt.Status == TemplateStatus.Draft),
            ArchivedTemplates = library.PromptTemplates.Count(pt => pt.Status == TemplateStatus.Archived),
            TotalExecutions = totalExecutions,
            SuccessfulExecutions = 0, // Placeholder until execution tracking is implemented
            FailedExecutions = 0, // Placeholder until execution tracking is implemented
            SuccessRate = 0, // Placeholder until execution tracking is implemented
            AverageExecutionTime = TimeSpan.Zero, // Placeholder until execution tracking is implemented
            UniqueUsers = 0, // Placeholder until user tracking is implemented
            CreatedAt = library.CreatedAt,
            LastModified = library.UpdatedAt,
            StatisticsGeneratedAt = DateTime.UtcNow,
            StatisticsPeriod = TimeSpan.FromDays(30), // Default to 30 days
            ExtendedMetrics = new Dictionary<string, object>
            {
                { "UniqueVariableCount", uniqueVariables },
                { "LastTemplateCreated", (object?)lastTemplateCreated ?? DateTime.MinValue },
                { "TemplatesWithExecutions", 0 } // Placeholder until execution tracking is implemented
            }
        };
    }

    /// <summary>
    /// Get template count for a library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <returns>Number of templates in the library</returns>
    public async Task<int> GetTemplateCountAsync(Guid libraryId)
    {
        return await _context.PromptTemplates
            .Where(pt => pt.DeletedAt == null) // Apply soft delete filter
            .CountAsync(pt => pt.PromptLibraryId == libraryId);
    }

    /// <summary>
    /// Get execution count for all templates in a library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <returns>Total number of executions for templates in the library</returns>
    public async Task<int> GetExecutionCountAsync(Guid libraryId)
    {
        // TODO: Update when PromptExecution is migrated to use Guid foreign keys
        // For now, return 0 due to ID type mismatch
        await Task.CompletedTask;
        return 0;
    }

    #endregion

    #region Library Validation    

    /// <summary>
    /// Check if a library name is unique within a prompt lab
    /// </summary>
    /// <param name="name">Library name to check</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="excludeLibraryId">Optional library ID to exclude from uniqueness check</param>
    /// <returns>True if name is unique, false otherwise</returns>
    public async Task<bool> IsLibraryNameUniqueAsync(string name, Guid promptLabId, Guid? excludeLibraryId = null)
    {
        var query = _context.PromptLibraries
            .Where(l => l.DeletedAt == null) // Apply soft delete filter
            .Where(l => l.PromptLabId == promptLabId && l.Name == name);

        if (excludeLibraryId.HasValue)
        {
            query = query.Where(l => l.Id != excludeLibraryId.Value);
        }

        return !await query.AnyAsync();
    }

    /// <summary>
    /// Validate library creation/update data
    /// </summary>
    /// <param name="name">Library name</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="excludeLibraryId">Optional library ID to exclude from validation</param>
    /// <returns>Validation result</returns>
    public async Task<(bool IsValid, List<string> Errors)> ValidateLibraryDataAsync(string name, Guid promptLabId, Guid? excludeLibraryId = null)
    {
        var errors = new List<string>();

        // Validate name
        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("Library name is required");
        }
        else if (name.Length > 100)
        {
            errors.Add("Library name cannot exceed 100 characters");
        }

        // Validate prompt lab exists using PromptLabService
        var promptLabExists = await _promptLabService.LabExistsAsync(promptLabId);
        if (!promptLabExists)
        {
            errors.Add($"Prompt lab with ID {promptLabId} not found");
        }

        // Check uniqueness
        if (!string.IsNullOrWhiteSpace(name) && promptLabExists)
        {
            var isUnique = await IsLibraryNameUniqueAsync(name, promptLabId, excludeLibraryId);
            if (!isUnique)
            {
                errors.Add($"A library with name '{name}' already exists in this prompt lab");
            }
        }

        return (errors.Count == 0, errors);
    }

    #endregion

    #region Import/Export    

    /// <summary>
    /// Import a prompt library from JSON data
    /// </summary>
    /// <param name="jsonContent">JSON content representing the library and its templates</param>
    /// <param name="promptLabId">Target prompt lab ID</param>
    /// <param name="importExecutionHistory">Whether to import execution history</param>
    /// <param name="overwriteExisting">Whether to overwrite existing library with same name</param>
    /// <returns>Imported library</returns>
    public async Task<PromptLibrary?> ImportLibraryFromJsonAsync(string jsonContent, Guid promptLabId, bool importExecutionHistory, bool overwriteExisting)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // TODO: Implement JSON import logic similar to the legacy collection import
        // This would need to be adapted to work with the new PromptLibrary structure
        await Task.CompletedTask; // Satisfy async requirement
        throw new NotImplementedException("JSON import for PromptLibrary not yet implemented");
    }

    /// <summary>
    /// Export a prompt library to JSON
    /// </summary>
    /// <param name="libraryId">Library ID to export</param>
    /// <param name="includeExecutionHistory">Whether to include execution history</param>
    /// <returns>JSON representation of the library</returns>
    public async Task<string> ExportLibraryToJsonAsync(Guid libraryId, bool includeExecutionHistory = false)
    {
        var library = await GetLibraryByIdAsync(libraryId);
        if (library == null)
        {
            throw new ArgumentException($"Library with ID {libraryId} not found", nameof(libraryId));
        }

        // Load executions if needed
        // TODO: Update when PromptExecution relationship is fixed
        if (includeExecutionHistory)
        {
            // Note: Skipping execution loading due to domain model migration in progress
        }

        var exportData = new
        {
            Library = new
            {
                library.Name,
                library.Description,
                library.Visibility,
                library.CreatedAt,
                library.UpdatedAt
            },
            Templates = library.PromptTemplates.Select(pt => new
            {
                pt.Name,
                pt.Description,
                Content = pt.Content?.Content ?? "",
                pt.CreatedAt,
                pt.UpdatedAt,
                Variables = pt.Variables.Select(v => new
                {
                    v.Name,
                    v.Description,
                    v.DefaultValue,
                    v.Type
                }),
                ExecutionHistory = includeExecutionHistory
                    ? new object[] { } // TODO: Implement when execution relationship is fixed
                    : null
            })
        };

        return JsonSerializer.Serialize(exportData, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }

    #endregion
}
