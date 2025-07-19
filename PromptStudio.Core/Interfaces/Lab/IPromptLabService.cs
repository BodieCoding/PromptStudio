using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Lab;

namespace PromptStudio.Core.Interfaces.Lab;

/// <summary>
/// Interface for prompt lab operations (Updated for Guid-based architecture)
/// </summary>
public interface IPromptLabService
{
    #region Lab CRUD Operations

    /// <summary>
    /// Get all prompt labs for a user/tenant
    /// </summary>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID to filter by</param>
    /// <param name="includeDeleted">Whether to include soft-deleted labs</param>
    /// <returns>List of prompt labs</returns>
    Task<IEnumerable<PromptLab>> GetLabsAsync(string? userId = null, Guid? tenantId = null, bool includeDeleted = false);

    /// <summary>
    /// Get a prompt lab by ID
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="includeDeleted">Whether to include soft-deleted labs</param>
    /// <param name="includeRelated">Whether to include related libraries and workflows</param>
    /// <returns>Prompt lab or null if not found</returns>
    Task<PromptLab?> GetLabByIdAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false, bool includeRelated = true);

    /// <summary>
    /// Create a new prompt lab
    /// </summary>
    /// <param name="name">Lab name</param>
    /// <param name="description">Optional lab description</param>
    /// <param name="visibility">Lab visibility level</param>
    /// <param name="tags">Optional tags</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="createdBy">User ID who created the lab</param>
    /// <returns>Created prompt lab</returns>
    Task<PromptLab> CreateLabAsync(string name, string? description = null, 
        LabVisibility visibility = LabVisibility.Private, List<string>? tags = null, 
        Guid? tenantId = null, string? createdBy = null);

    /// <summary>
    /// Update an existing prompt lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="name">Updated name</param>
    /// <param name="description">Updated description</param>
    /// <param name="visibility">Updated visibility level</param>
    /// <param name="tags">Updated tags</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="updatedBy">User ID who updated the lab</param>
    /// <returns>Updated prompt lab or null if not found</returns>
    Task<PromptLab?> UpdateLabAsync(Guid labId, string name, string? description = null, 
        LabVisibility? visibility = null, List<string>? tags = null, 
        Guid? tenantId = null, string? updatedBy = null);

    /// <summary>
    /// Delete a prompt lab (soft delete)
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="deletedBy">User ID who deleted the lab</param>
    /// <returns>True if deleted, false if not found</returns>
    Task<bool> DeleteLabAsync(Guid labId, Guid? tenantId = null, string? deletedBy = null);

    /// <summary>
    /// Permanently delete a prompt lab (hard delete)
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>True if permanently deleted, false if not found</returns>
    Task<bool> PermanentlyDeleteLabAsync(Guid labId, Guid? tenantId = null);

    /// <summary>
    /// Restore a soft-deleted prompt lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="restoredBy">User ID who restored the lab</param>
    /// <returns>True if restored, false if not found</returns>
    Task<bool> RestoreLabAsync(Guid labId, Guid? tenantId = null, string? restoredBy = null);

    #endregion

    #region Lab Discovery

    /// <summary>
    /// Search prompt labs by name or description
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID to filter by</param>
    /// <param name="includeDeleted">Whether to include soft-deleted labs</param>
    /// <returns>List of matching prompt labs</returns>
    Task<IEnumerable<PromptLab>> SearchLabsAsync(string searchTerm, string? userId = null, 
        Guid? tenantId = null, bool includeDeleted = false);

    /// <summary>
    /// Get labs by visibility level
    /// </summary>
    /// <param name="visibility">Visibility level</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID to filter by</param>
    /// <param name="includeDeleted">Whether to include soft-deleted labs</param>
    /// <returns>List of labs matching visibility criteria</returns>
    Task<IEnumerable<PromptLab>> GetLabsByVisibilityAsync(LabVisibility visibility, string? userId = null, 
        Guid? tenantId = null, bool includeDeleted = false);

    /// <summary>
    /// Get labs by tags
    /// </summary>
    /// <param name="tags">Tags to filter by</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID to filter by</param>
    /// <param name="includeDeleted">Whether to include soft-deleted labs</param>
    /// <returns>List of labs matching tag criteria</returns>
    Task<IEnumerable<PromptLab>> GetLabsByTagsAsync(List<string> tags, string? userId = null, 
        Guid? tenantId = null, bool includeDeleted = false);

    /// <summary>
    /// Get recently updated labs
    /// </summary>
    /// <param name="daysBack">Number of days to look back</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID to filter by</param>
    /// <param name="limit">Maximum number of labs to return</param>
    /// <param name="includeDeleted">Whether to include soft-deleted labs</param>
    /// <returns>List of recently updated labs</returns>
    Task<IEnumerable<PromptLab>> GetRecentlyUpdatedLabsAsync(int daysBack = 7, string? userId = null, 
        Guid? tenantId = null, int limit = 50, bool includeDeleted = false);

    /// <summary>
    /// Get most active labs (by execution count)
    /// </summary>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID to filter by</param>
    /// <param name="limit">Maximum number of labs to return</param>
    /// <param name="daysBack">Number of days to look back for activity stats</param>
    /// <returns>List of most active labs</returns>
    Task<IEnumerable<PromptLab>> GetMostActiveLabsAsync(string? userId = null, Guid? tenantId = null, 
        int limit = 10, int daysBack = 30);

    /// <summary>
    /// Get labs with pagination
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID to filter by</param>
    /// <param name="includeDeleted">Whether to include soft-deleted labs</param>
    /// <returns>Paginated list of labs</returns>
    Task<PagedResult<PromptLab>> GetLabsPagedAsync(int pageNumber, int pageSize, string? userId = null, 
        Guid? tenantId = null, bool includeDeleted = false);

    #endregion

    #region Lab Analytics

    /// <summary>
    /// Get lab statistics
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="daysBack">Number of days to look back for statistics</param>
    /// <returns>Lab statistics</returns>
    Task<LabStatistics> GetLabStatisticsAsync(Guid labId, Guid? tenantId = null, int daysBack = 30);

    /// <summary>
    /// Get library count for a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="includeDeleted">Whether to include soft-deleted libraries</param>
    /// <returns>Number of libraries in the lab</returns>
    Task<int> GetLibraryCountAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false);

    /// <summary>
    /// Get workflow count for a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="includeDeleted">Whether to include soft-deleted workflows</param>
    /// <returns>Number of workflows in the lab</returns>
    Task<int> GetWorkflowCountAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false);

    /// <summary>
    /// Get total template count across all libraries in a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="includeDeleted">Whether to include soft-deleted templates</param>
    /// <returns>Total number of templates in the lab</returns>
    Task<int> GetTotalTemplateCountAsync(Guid labId, Guid? tenantId = null, bool includeDeleted = false);

    /// <summary>
    /// Get total execution count across all templates in a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>Total number of executions in the lab</returns>
    Task<int> GetTotalExecutionCountAsync(Guid labId, Guid? tenantId = null, int daysBack = 30);

    /// <summary>
    /// Get lab activity trends over time
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <param name="granularity">Time granularity for the data</param>
    /// <returns>Activity trend data</returns>
    Task<List<LabActivityTrendData>> GetLabActivityTrendsAsync(Guid labId, Guid? tenantId = null, 
        int daysBack = 30, TrendGranularity granularity = TrendGranularity.Daily);

    /// <summary>
    /// Get top performing libraries in a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="limit">Maximum number of libraries to return</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>List of top performing libraries</returns>
    Task<List<LibraryPerformanceSummary>> GetTopPerformingLibrariesAsync(Guid labId, 
        Guid? tenantId = null, int limit = 10, int daysBack = 30);

    #endregion

    #region Lab Collaboration and Permissions

    /// <summary>
    /// Get lab members (users with access to the lab)
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>List of lab members with their permissions</returns>
    Task<List<LabMemberDto>> GetLabMembersAsync(Guid labId, Guid? tenantId = null);

    /// <summary>
    /// Add a member to a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="role">Member role</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="addedBy">User ID who added the member</param>
    /// <returns>Created lab member</returns>
    Task<LabMemberDto> AddLabMemberAsync(Guid labId, string userId, LabMemberRole role, 
        Guid? tenantId = null, string? addedBy = null);

    /// <summary>
    /// Update a lab member's role
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="role">New role</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="updatedBy">User ID who updated the member</param>
    /// <returns>Updated lab member or null if not found</returns>
    Task<LabMemberDto?> UpdateLabMemberRoleAsync(Guid labId, string userId, LabMemberRole role, 
        Guid? tenantId = null, string? updatedBy = null);

    /// <summary>
    /// Remove a member from a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="removedBy">User ID who removed the member</param>
    /// <returns>True if member was removed, false otherwise</returns>
    Task<bool> RemoveLabMemberAsync(Guid labId, string userId, Guid? tenantId = null, string? removedBy = null);

    /// <summary>
    /// Check if a user has access to a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="requiredRole">Minimum required role</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>True if user has access, false otherwise</returns>
    Task<bool> HasLabAccessAsync(Guid labId, string userId, LabMemberRole requiredRole = LabMemberRole.Viewer, 
        Guid? tenantId = null);

    /// <summary>
    /// Get user's role in a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>User's role or null if not a member</returns>
    Task<LabMemberRole?> GetUserLabRoleAsync(Guid labId, string userId, Guid? tenantId = null);

    #endregion

    #region Lab Import/Export

    /// <summary>
    /// Export a lab to JSON format
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="includeLibraries">Whether to include libraries</param>
    /// <param name="includeWorkflows">Whether to include workflows</param>
    /// <param name="includeExecutionHistory">Whether to include execution history</param>
    /// <param name="includePermissions">Whether to include permission information</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>JSON representation of the lab</returns>
    Task<string> ExportLabAsync(Guid labId, bool includeLibraries = true, bool includeWorkflows = true, 
        bool includeExecutionHistory = false, bool includePermissions = false, Guid? tenantId = null);

    /// <summary>
    /// Import a lab from JSON format
    /// </summary>
    /// <param name="jsonContent">JSON content representing the lab</param>
    /// <param name="importLibraries">Whether to import libraries</param>
    /// <param name="importWorkflows">Whether to import workflows</param>
    /// <param name="importExecutionHistory">Whether to import execution history</param>
    /// <param name="importPermissions">Whether to import permission information</param>
    /// <param name="overwriteExisting">Whether to overwrite existing lab</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="importedBy">User ID who imported the lab</param>
    /// <returns>Imported lab or null if import failed</returns>
    Task<PromptLab?> ImportLabAsync(string jsonContent, bool importLibraries = true, bool importWorkflows = true, 
        bool importExecutionHistory = false, bool importPermissions = false, bool overwriteExisting = false, 
        Guid? tenantId = null, string? importedBy = null);

    /// <summary>
    /// Clone a lab
    /// </summary>
    /// <param name="sourceLabId">Source lab ID</param>
    /// <param name="newName">Name for the cloned lab</param>
    /// <param name="includeLibraries">Whether to include libraries</param>
    /// <param name="includeWorkflows">Whether to include workflows</param>
    /// <param name="includeExecutionHistory">Whether to include execution history</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="clonedBy">User ID who cloned the lab</param>
    /// <returns>Cloned lab</returns>
    Task<PromptLab> CloneLabAsync(Guid sourceLabId, string newName, bool includeLibraries = true, 
        bool includeWorkflows = true, bool includeExecutionHistory = false, 
        Guid? tenantId = null, string? clonedBy = null);

    #endregion

    #region Lab Validation

    /// <summary>
    /// Validate lab data and structure
    /// </summary>
    /// <param name="lab">Lab to validate</param>
    /// <returns>Validation result</returns>
    Task<LabValidationResult> ValidateLabAsync(PromptLab lab);

    /// <summary>
    /// Check if a lab name is unique for a tenant
    /// </summary>
    /// <param name="name">Lab name to check</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="excludeLabId">Optional lab ID to exclude from uniqueness check</param>
    /// <returns>True if name is unique, false otherwise</returns>
    Task<bool> IsLabNameUniqueAsync(string name, Guid? tenantId = null, Guid? excludeLabId = null);

    #endregion
}
