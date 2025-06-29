using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;

namespace PromptStudio.Core.Services;

/// <summary>
/// Service for managing prompt lab operations
/// </summary>
public class PromptLabService : IPromptLabService
{
    private readonly IPromptStudioDbContext _context;

    public PromptLabService(IPromptStudioDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    #region Lab CRUD Operations

    /// <summary>
    /// Get all prompt labs for a user/tenant
    /// </summary>
    public async Task<IEnumerable<PromptLab>> GetLabsAsync(string? userId = null, Guid? tenantId = null)
    {
        var query = _context.PromptLabs.AsQueryable();

        // Apply soft delete filter
        query = query.Where(lab => lab.DeletedAt == null);

        // Apply user filter if provided
        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        // Apply tenant filter if provided
        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query
            .OrderByDescending(lab => lab.UpdatedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Get a prompt lab by ID
    /// </summary>
    public async Task<PromptLab?> GetLabByIdAsync(Guid labId)
    {
        return await _context.PromptLabs
            .Where(lab => lab.Id == labId && lab.DeletedAt == null)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Create a new prompt lab
    /// </summary>
    public async Task<PromptLab> CreateLabAsync(PromptLab lab)
    {
        ArgumentNullException.ThrowIfNull(lab);

        // Ensure timestamps are set
        lab.CreatedAt = DateTime.UtcNow;
        lab.UpdatedAt = DateTime.UtcNow;
        lab.DeletedAt = null; // Ensure not soft deleted

        _context.PromptLabs.Add(lab);
        await _context.SaveChangesAsync();

        return lab;
    }

    /// <summary>
    /// Update an existing prompt lab
    /// </summary>
    public async Task<PromptLab?> UpdateLabAsync(PromptLab lab)
    {
        ArgumentNullException.ThrowIfNull(lab);

        var existingLab = await _context.PromptLabs
            .Where(l => l.Id == lab.Id && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (existingLab == null)
        {
            return null;
        }

        // Update properties
        existingLab.Name = lab.Name;
        existingLab.Description = lab.Description;
        existingLab.LabId = lab.LabId;
        existingLab.Owner = lab.Owner;
        existingLab.OrganizationId = lab.OrganizationId;
        existingLab.Status = lab.Status;
        existingLab.Tags = lab.Tags;
        existingLab.Visibility = lab.Visibility;
        existingLab.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingLab;
    }

    /// <summary>
    /// Delete a prompt lab (soft delete)
    /// </summary>
    public async Task<bool> DeleteLabAsync(Guid labId)
    {
        var lab = await _context.PromptLabs
            .Where(l => l.Id == labId && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (lab == null)
        {
            return false;
        }

        // Soft delete
        lab.DeletedAt = DateTime.UtcNow;
        lab.UpdatedAt = DateTime.UtcNow;
        lab.Status = LabStatus.Deleted;

        await _context.SaveChangesAsync();
        return true;
    }

    #endregion

    #region Lab Discovery

    /// <summary>
    /// Search prompt labs by name or description
    /// </summary>
    public async Task<IEnumerable<PromptLab>> SearchLabsAsync(string searchTerm, string? userId = null, Guid? tenantId = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(searchTerm);

        var query = _context.PromptLabs.AsQueryable();

        // Apply soft delete filter
        query = query.Where(lab => lab.DeletedAt == null);

        // Apply search filter
        query = query.Where(lab =>
            lab.Name.Contains(searchTerm) ||
            (lab.Description != null && lab.Description.Contains(searchTerm)) ||
            lab.LabId.Contains(searchTerm));

        // Apply user filter if provided
        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        // Apply tenant filter if provided
        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query
            .OrderByDescending(lab => lab.UpdatedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Get labs by visibility level
    /// </summary>
    public async Task<IEnumerable<PromptLab>> GetLabsByVisibilityAsync(LabVisibility visibility, string? userId = null, Guid? tenantId = null)
    {
        var query = _context.PromptLabs.AsQueryable();

        // Apply soft delete filter
        query = query.Where(lab => lab.DeletedAt == null);

        // Apply visibility filter
        query = query.Where(lab => lab.Visibility == visibility);

        // Apply user filter if provided
        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        // Apply tenant filter if provided
        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query
            .OrderByDescending(lab => lab.UpdatedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Get recently updated labs
    /// </summary>
    public async Task<IEnumerable<PromptLab>> GetRecentlyUpdatedLabsAsync(int daysBack = 7, string? userId = null, Guid? tenantId = null)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-daysBack);
        var query = _context.PromptLabs.AsQueryable();

        // Apply soft delete filter
        query = query.Where(lab => lab.DeletedAt == null);

        // Apply date filter
        query = query.Where(lab => lab.UpdatedAt >= cutoffDate);

        // Apply user filter if provided
        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        // Apply tenant filter if provided
        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query
            .OrderByDescending(lab => lab.UpdatedAt)
            .ToListAsync();
    }

    #endregion

    #region Lab Analytics

    /// <summary>
    /// Get lab statistics
    /// </summary>
    public async Task<LabStatistics> GetLabStatisticsAsync(Guid labId)
    {
        var lab = await _context.PromptLabs
            .Include(l => l.PromptLibraries)
            .ThenInclude(lib => lib.PromptTemplates)
            .Where(l => l.Id == labId && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (lab == null)
        {
            return new LabStatistics();
        }
        var stats = new LabStatistics
        {
            LibraryCount = lab.PromptLibraries.Count(lib => lib.DeletedAt == null),
            WorkflowLibraryCount = 0, // TODO: Add proper workflow library identification when domain is finalized
            TotalWorkflowCount = 0, // TODO: Implement when PromptFlow->PromptLab relationship is clarified
            LastActivity = lab.UpdatedAt
        };

        // Get template counts and execution counts
        var activeLibraries = lab.PromptLibraries.Where(lib => lib.DeletedAt == null);
        var activeTemplates = activeLibraries.SelectMany(lib => lib.PromptTemplates.Where(t => t.DeletedAt == null));

        stats.TotalTemplateCount = activeTemplates.Count();        // Get execution counts from database for better performance
        // TODO: Update this when PromptExecution is migrated to use Guid foreign keys
        // For now, skipping execution count due to ID type mismatch
        var activeTemplatesList = activeTemplates.ToList();
        if (activeTemplatesList.Any())
        {
            // Note: Execution count skipped until PromptExecution uses Guid foreign keys
            stats.TotalExecutionCount = 0; // TODO: Implement when migration is complete
            stats.LastTemplateCreated = activeTemplatesList.Max(t => t.CreatedAt);
        }
        var flowIds = new List<Guid>(); // TODO: Get flows for lab when domain relationship is clarified
        if (flowIds.Any())
        {
            stats.TotalFlowExecutionCount = await _context.FlowExecutions
                .Where(exec => flowIds.Contains(exec.FlowId))
                .CountAsync();

            // TODO: Implement when flow relationship is clarified
            // stats.LastWorkflowCreated = lab.PromptFlows.Where(f => f.DeletedAt == null).Max(f => f.CreatedAt);
        }

        // Calculate averages
        if (stats.LibraryCount > 0)
        {
            stats.AverageTemplatesPerLibrary = (double)stats.TotalTemplateCount / stats.LibraryCount;
            stats.AverageWorkflowsPerLibrary = stats.WorkflowLibraryCount > 0
                ? (double)stats.TotalWorkflowCount / stats.WorkflowLibraryCount
                : 0;
        }

        return stats;
    }

    /// <summary>
    /// Get library count for a lab
    /// </summary>
    public async Task<int> GetLibraryCountAsync(Guid labId)
    {
        return await _context.PromptLibraries
            .Where(lib => lib.PromptLabId == labId && lib.DeletedAt == null)
            .CountAsync();
    }

    /// <summary>
    /// Get workflow count for a lab
    /// </summary>
    public async Task<int> GetWorkflowCountAsync(Guid labId)
    {
        // Query flows through WorkflowLibrary relationship
        return await _context.PromptFlows
            .Where(flow => flow.WorkflowLibrary.PromptLabId == labId &&
                          flow.DeletedAt == null &&
                          flow.WorkflowLibrary.DeletedAt == null)
            .CountAsync();
    }

    /// <summary>
    /// Get total template count across all libraries in a lab
    /// </summary>
    public async Task<int> GetTotalTemplateCountAsync(Guid labId)
    {
        return await _context.PromptTemplates
            .Where(template => template.PromptLibrary.PromptLabId == labId &&
                              template.DeletedAt == null &&
                              template.PromptLibrary.DeletedAt == null)
            .CountAsync();
    }

    /// <summary>
    /// Get total execution count across all templates in a lab
    /// </summary>
    public async Task<int> GetTotalExecutionCountAsync(Guid labId)
    {
        return await _context.PromptExecutions
            .Where(exec => exec.PromptTemplate.PromptLibrary.PromptLabId == labId)
            .CountAsync();
    }

    #endregion

    #region Lab Validation

    /// <summary>
    /// Check if a lab name is unique for a user/tenant
    /// </summary>
    public async Task<bool> IsLabNameUniqueAsync(string name, string userId, Guid tenantId, Guid? excludeLabId = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        var query = _context.PromptLabs
            .Where(lab => lab.Name == name &&
                         lab.Owner == userId &&
                         lab.OrganizationId == tenantId &&
                         lab.DeletedAt == null);

        if (excludeLabId.HasValue)
        {
            query = query.Where(lab => lab.Id != excludeLabId.Value);
        }

        return !await query.AnyAsync();
    }

    /// <summary>
    /// Validate lab data
    /// </summary>
    public async Task<(bool IsValid, List<string> Errors)> ValidateLabDataAsync(PromptLab lab, bool isUpdate = false)
    {
        ArgumentNullException.ThrowIfNull(lab);

        var errors = new List<string>();

        // Validate required fields
        if (string.IsNullOrWhiteSpace(lab.Name))
        {
            errors.Add("Lab name is required");
        }

        if (string.IsNullOrWhiteSpace(lab.LabId))
        {
            errors.Add("Lab ID is required");
        }
        else if (!System.Text.RegularExpressions.Regex.IsMatch(lab.LabId, @"^[a-z][a-z0-9-]*[a-z0-9]$"))
        {
            errors.Add("Lab ID must be lowercase, start with a letter, and contain only letters, numbers, and hyphens");
        }

        if (string.IsNullOrWhiteSpace(lab.Owner))
        {
            errors.Add("Lab owner is required");
        }

        // Check lab ID uniqueness (within organization/tenant scope)
        if (!string.IsNullOrWhiteSpace(lab.LabId) && !string.IsNullOrWhiteSpace(lab.Owner))
        {
            var existingLabWithId = await _context.PromptLabs
                .Where(l => l.LabId == lab.LabId &&
                           l.OrganizationId == lab.OrganizationId &&
                           l.DeletedAt == null)
                .FirstOrDefaultAsync();

            if (existingLabWithId != null && (!isUpdate || existingLabWithId.Id != lab.Id))
            {
                errors.Add("Lab ID already exists in this organization");
            }
        }

        // Check name uniqueness (within owner scope)
        if (!string.IsNullOrWhiteSpace(lab.Name) && !string.IsNullOrWhiteSpace(lab.Owner) && lab.OrganizationId.HasValue)
        {
            var isNameUnique = await IsLabNameUniqueAsync(lab.Name, lab.Owner, lab.OrganizationId.Value, isUpdate ? lab.Id : null);
            if (!isNameUnique)
            {
                errors.Add("Lab name already exists for this user");
            }
        }

        return (errors.Count == 0, errors);
    }

    /// <summary>
    /// Check if a lab exists
    /// </summary>
    public async Task<bool> LabExistsAsync(Guid labId)
    {
        return await _context.PromptLabs
            .Where(lab => lab.Id == labId && lab.DeletedAt == null)
            .AnyAsync();
    }

    #endregion

    #region Lab Permissions

    /// <summary>
    /// Get labs accessible to a user
    /// </summary>
    public async Task<IEnumerable<PromptLab>> GetAccessibleLabsAsync(string userId, Guid tenantId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        // For now, return labs owned by user or with appropriate visibility
        // This will be enhanced when proper permission system is implemented
        return await _context.PromptLabs
            .Where(lab => lab.DeletedAt == null &&
                         lab.OrganizationId == tenantId &&
                         (lab.Owner == userId ||
                          lab.Visibility == LabVisibility.Internal ||
                          lab.Visibility == LabVisibility.Public))
            .OrderByDescending(lab => lab.UpdatedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Check if user has access to a lab
    /// </summary>
    public async Task<bool> HasLabAccessAsync(Guid labId, string userId, PermissionLevel requiredPermission = PermissionLevel.Read)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        var lab = await _context.PromptLabs
            .Where(l => l.Id == labId && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (lab == null)
        {
            return false;
        }

        // Owner has full access
        if (lab.Owner == userId)
        {
            return true;
        }

        // Check visibility-based access
        return lab.Visibility switch
        {
            LabVisibility.Public => requiredPermission == PermissionLevel.Read,
            LabVisibility.Internal => requiredPermission == PermissionLevel.Read, // Assuming user is in same org
            LabVisibility.Private => false,
            LabVisibility.TeamShared => false, // TODO: Implement team-based access
            _ => false
        };
    }

    /// <summary>
    /// Grant permission to a lab (placeholder for future implementation)
    /// </summary>
    public async Task<bool> GrantLabPermissionAsync(Guid labId, string principalId, PrincipalType principalType, PermissionLevel permission)
    {
        // TODO: Implement when permission system is ready
        // For now, return false to indicate not implemented
        await Task.CompletedTask;
        return false;
    }   
    
    /// <summary>
    /// Revoke permission from a lab (placeholder for future implementation)
    /// </summary>
    public async Task<bool> RevokeLabPermissionAsync(Guid labId, string principalId)
    {
        // TODO: Implement when permission system is ready
        // For now, return false to indicate not implemented
        await Task.CompletedTask;
        return false;
    }

    #endregion
}
