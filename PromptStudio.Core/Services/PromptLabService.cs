using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Interfaces.Data;
using PromptStudio.Core.Interfaces.Lab;
using PromptStudio.Core.DTOs.Lab;
using PromptStudio.Core.DTOs.Common;

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

    public async Task<IEnumerable<PromptLab>> GetLabsAsync(string? userId = null, Guid? tenantId = null, bool includeDeleted = false)
    {
        var query = _context.PromptLabs.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(lab => lab.DeletedAt == null);
        }

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query.OrderByDescending(lab => lab.UpdatedAt).ToListAsync();
    }

    public async Task<PromptLab?> GetLabByIdAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false, bool includeRelated = true)
    {
        var query = _context.PromptLabs.AsQueryable();

        if (includeRelated)
        {
            query = query.Include(l => l.PromptLibraries);
        }

        query = query.Where(lab => lab.Id == labId);

        if (!includeDeleted)
        {
            query = query.Where(lab => lab.DeletedAt == null);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<PromptLab> CreateLabAsync(string name, string? description = null, LabVisibility visibility = LabVisibility.Private, List<string>? tags = null, Guid? tenantId = null, string? createdBy = null)
    {
        var lab = new PromptLab
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Visibility = visibility,
            Tags = tags != null ? System.Text.Json.JsonSerializer.Serialize(tags) : null,
            OrganizationId = tenantId,
            Owner = createdBy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Status = LabStatus.Active
        };

        _context.PromptLabs.Add(lab);
        await _context.SaveChangesAsync();
        return lab;
    }

    public async Task<PromptLab?> UpdateLabAsync(Guid labId, string name, string? description = null, LabVisibility? visibility = null, List<string>? tags = null, Guid? tenantId = null, string? updatedBy = null)
    {
        var lab = await _context.PromptLabs
            .Where(l => l.Id == labId && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (lab == null) return null;

        lab.Name = name;
        lab.Description = description;
        if (visibility.HasValue) lab.Visibility = visibility.Value;
        lab.Tags = tags != null ? System.Text.Json.JsonSerializer.Serialize(tags) : null;
        lab.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return lab;
    }

    public async Task<bool> DeleteLabAsync(Guid labId, Guid? tenantId = null, string? deletedBy = null)
    {
        var lab = await _context.PromptLabs
            .Where(l => l.Id == labId && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (lab == null) return false;

        lab.DeletedAt = DateTime.UtcNow;
        lab.UpdatedAt = DateTime.UtcNow;
        lab.Status = LabStatus.Deleted;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> PermanentlyDeleteLabAsync(Guid labId, Guid? tenantId = null)
    {
        var lab = await _context.PromptLabs
            .Where(l => l.Id == labId)
            .FirstOrDefaultAsync();

        if (lab == null) return false;

        _context.PromptLabs.Remove(lab);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RestoreLabAsync(Guid labId, Guid? tenantId = null, string? restoredBy = null)
    {
        var lab = await _context.PromptLabs
            .Where(l => l.Id == labId && l.DeletedAt != null)
            .FirstOrDefaultAsync();

        if (lab == null) return false;

        lab.DeletedAt = null;
        lab.UpdatedAt = DateTime.UtcNow;
        lab.Status = LabStatus.Active;

        await _context.SaveChangesAsync();
        return true;
    }

    #endregion

    #region Lab Discovery

    public async Task<IEnumerable<PromptLab>> SearchLabsAsync(string searchTerm, string? userId = null, Guid? tenantId = null, bool includeDeleted = false)
    {
        var query = _context.PromptLabs.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(lab => lab.DeletedAt == null);
        }

        query = query.Where(lab => lab.Name.Contains(searchTerm) || 
                                  (lab.Description != null && lab.Description.Contains(searchTerm)));

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query.OrderByDescending(lab => lab.UpdatedAt).ToListAsync();
    }

    public async Task<IEnumerable<PromptLab>> GetLabsByVisibilityAsync(LabVisibility visibility, string? userId = null, Guid? tenantId = null, bool includeDeleted = false)
    {
        var query = _context.PromptLabs.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(lab => lab.DeletedAt == null);
        }

        query = query.Where(lab => lab.Visibility == visibility);

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query.OrderByDescending(lab => lab.UpdatedAt).ToListAsync();
    }

    public async Task<IEnumerable<PromptLab>> GetLabsByTagsAsync(List<string> tags, string? userId = null, Guid? tenantId = null, bool includeDeleted = false)
    {
        var query = _context.PromptLabs.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(lab => lab.DeletedAt == null);
        }

        // TODO: Implement proper tag filtering when tag implementation is finalized
        
        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query.OrderByDescending(lab => lab.UpdatedAt).ToListAsync();
    }

    public async Task<IEnumerable<PromptLab>> GetRecentlyUpdatedLabsAsync(int daysBack = 7, string? userId = null, Guid? tenantId = null, int limit = 50, bool includeDeleted = false)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-daysBack);
        var query = _context.PromptLabs.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(lab => lab.DeletedAt == null);
        }

        query = query.Where(lab => lab.UpdatedAt >= cutoffDate);

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query.OrderByDescending(lab => lab.UpdatedAt).Take(limit).ToListAsync();
    }

    public async Task<IEnumerable<PromptLab>> GetMostActiveLabsAsync(string? userId = null, Guid? tenantId = null, int limit = 10, int daysBack = 30)
    {
        // TODO: Implement based on activity metrics
        var query = _context.PromptLabs.AsQueryable();

        query = query.Where(lab => lab.DeletedAt == null);

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query.OrderByDescending(lab => lab.UpdatedAt).Take(limit).ToListAsync();
    }

    public async Task<PagedResult<PromptLab>> GetLabsPagedAsync(int pageNumber, int pageSize, string? userId = null, Guid? tenantId = null, bool includeDeleted = false)
    {
        var query = _context.PromptLabs.AsQueryable();

        if (!includeDeleted)
        {
            query = query.Where(lab => lab.DeletedAt == null);
        }

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(lab => lab.Owner == userId);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        var totalItems = await query.CountAsync();
        var items = await query
            .OrderByDescending(lab => lab.UpdatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PromptLab>
        {
            Items = items,
            TotalCount = totalItems,
            Skip = (pageNumber - 1) * pageSize,
            Take = pageSize
        };
    }

    #endregion

    #region Lab Analytics

    public async Task<LabStatistics> GetLabStatisticsAsync(Guid labId, Guid? tenantId = null, int daysBack = 30)
    {
        var lab = await _context.PromptLabs
            .Include(l => l.PromptLibraries)
            .Where(l => l.Id == labId && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (lab == null)
        {
            return new LabStatistics { LabId = labId, LabName = "Not Found" };
        }

        var stats = new LabStatistics
        {
            LabId = labId,
            LabName = lab.Name,
            TotalLibraries = lab.PromptLibraries.Count,
            ActiveLibraries = lab.PromptLibraries.Count(lib => lib.DeletedAt == null)
        };

        // TODO: Complete implementation when all relationships are established
        return stats;
    }

    public async Task<int> GetLibraryCountAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false)
    {
        var query = _context.PromptLibraries.Where(lib => lib.PromptLabId == labId);

        if (!includeDeleted)
        {
            query = query.Where(lib => lib.DeletedAt == null);
        }

        return await query.CountAsync();
    }

    public Task<int> GetWorkflowCountAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false)
    {
        // TODO: Implement when workflow relationships are established
        return Task.FromResult(0);
    }

    public async Task<int> GetTotalTemplateCountAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false)
    {
        var query = _context.PromptTemplates
            .Where(template => template.PromptLibrary.PromptLabId == labId);

        if (!includeDeleted)
        {
            query = query.Where(template => template.DeletedAt == null && 
                                          template.PromptLibrary.DeletedAt == null);
        }

        return await query.CountAsync();
    }

    public Task<int> GetTotalExecutionCountAsync(Guid labId, Guid? tenantId = null, int daysBack = 30)
    {
        // TODO: Implement when execution relationships are established
        return Task.FromResult(0);
    }

    public Task<List<LabActivityTrendData>> GetLabActivityTrendsAsync(Guid labId, Guid? tenantId = null, int daysBack = 30, TrendGranularity granularity = TrendGranularity.Daily)
    {
        // TODO: Implement activity trend analysis
        return Task.FromResult(new List<LabActivityTrendData>());
    }

    public Task<List<LibraryPerformanceSummary>> GetTopPerformingLibrariesAsync(Guid labId, Guid? tenantId = null, int limit = 10, int daysBack = 30)
    {
        // TODO: Implement performance analysis
        return Task.FromResult(new List<LibraryPerformanceSummary>());
    }

    #endregion

    #region Lab Collaboration and Permissions

    public Task<List<LabMemberDto>> GetLabMembersAsync(Guid labId, Guid? tenantId = null)
    {
        // TODO: Implement when member system is established
        return Task.FromResult(new List<LabMemberDto>());
    }

    public Task<LabMemberDto> AddLabMemberAsync(Guid labId, string userId, LabMemberRole role, Guid? tenantId = null, string? addedBy = null)
    {
        // TODO: Implement when member system is established
        throw new NotImplementedException("Member system not yet implemented");
    }

    public Task<LabMemberDto?> UpdateLabMemberRoleAsync(Guid labId, string userId, LabMemberRole role, Guid? tenantId = null, string? updatedBy = null)
    {
        // TODO: Implement when member system is established
        return Task.FromResult<LabMemberDto?>(null);
    }

    public Task<bool> RemoveLabMemberAsync(Guid labId, string userId, Guid? tenantId = null, string? removedBy = null)
    {
        // TODO: Implement when member system is established
        return Task.FromResult(false);
    }

    public async Task<bool> HasLabAccessAsync(Guid labId, string userId, LabMemberRole requiredRole = LabMemberRole.Viewer, Guid? tenantId = null)
    {
        var lab = await _context.PromptLabs
            .Where(l => l.Id == labId && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (lab == null) return false;

        // Owner has full access
        if (lab.Owner == userId) return true;

        // Check visibility-based access
        return lab.Visibility switch
        {
            LabVisibility.Public => requiredRole == LabMemberRole.Viewer,
            LabVisibility.Internal => requiredRole == LabMemberRole.Viewer,
            _ => false
        };
    }

    public async Task<LabMemberRole?> GetUserLabRoleAsync(Guid labId, string userId, Guid? tenantId = null)
    {
        var lab = await _context.PromptLabs
            .Where(l => l.Id == labId && l.DeletedAt == null)
            .FirstOrDefaultAsync();

        if (lab == null) return null;

        if (lab.Owner == userId) return LabMemberRole.Owner;

        // TODO: Implement proper role system
        return null;
    }

    #endregion

    #region Lab Import/Export

    public Task<string> ExportLabAsync(Guid labId, bool includeLibraries = true, bool includeWorkflows = true, bool includeExecutionHistory = false, bool includePermissions = false, Guid? tenantId = null)
    {
        // TODO: Implement export functionality
        throw new NotImplementedException("Export functionality not yet implemented");
    }

    public Task<PromptLab?> ImportLabAsync(string jsonContent, bool importLibraries = true, bool importWorkflows = true, bool importExecutionHistory = false, bool importPermissions = false, bool overwriteExisting = false, Guid? tenantId = null, string? importedBy = null)
    {
        // TODO: Implement import functionality
        throw new NotImplementedException("Import functionality not yet implemented");
    }

    public Task<PromptLab> CloneLabAsync(Guid sourceLabId, string newName, bool includeLibraries = true, bool includeWorkflows = true, bool includeExecutionHistory = false, Guid? tenantId = null, string? clonedBy = null)
    {
        // TODO: Implement clone functionality
        throw new NotImplementedException("Clone functionality not yet implemented");
    }

    #endregion

    #region Lab Validation

    public Task<LabValidationResult> ValidateLabAsync(PromptLab lab)
    {
        // TODO: Implement validation
        return Task.FromResult(new LabValidationResult { IsValid = true });
    }

    public async Task<bool> IsLabNameUniqueAsync(string name, Guid? tenantId = null, Guid? excludeLabId = null)
    {
        var query = _context.PromptLabs
            .Where(lab => lab.Name == name && lab.DeletedAt == null);

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        if (excludeLabId.HasValue)
        {
            query = query.Where(lab => lab.Id != excludeLabId.Value);
        }

        return !await query.AnyAsync();
    }

    public async Task<bool> LabExistsAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false)
    {
        var query = _context.PromptLabs
            .Where(lab => lab.Id == labId);

        if (!includeDeleted)
        {
            query = query.Where(lab => lab.DeletedAt == null);
        }

        if (tenantId.HasValue)
        {
            query = query.Where(lab => lab.OrganizationId == tenantId.Value);
        }

        return await query.AnyAsync();
    }

    #endregion
}
