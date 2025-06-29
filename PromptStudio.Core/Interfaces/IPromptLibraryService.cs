using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Enterprise-grade service interface for managing prompt libraries with enhanced features
/// Supports multi-tenancy, audit trails, permissions, and advanced analytics
/// </summary>
public interface IPromptLibraryService
{
    #region Library CRUD Operations

    /// <summary>
    /// Get all prompt libraries with tenant isolation
    /// </summary>
    /// <param name="tenantId">Tenant ID for isolation</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <param name="includeDeleted">Whether to include soft-deleted libraries</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of prompt libraries with their prompt templates</returns>
    Task<List<PromptLibrary>> GetLibrariesAsync(Guid tenantId, Guid? promptLabId = null, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a prompt library by ID with tenant validation
    /// </summary>
    /// <param name="id">Library ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="includeDeleted">Whether to include soft-deleted items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Library with prompt templates, or null if not found</returns>
    Task<PromptLibrary?> GetLibraryByIdAsync(Guid id, Guid tenantId, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new prompt library with full audit support
    /// </summary>
    /// <param name="name">Library name</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="createdBy">User creating the library</param>
    /// <param name="description">Optional library description</param>
    /// <param name="visibility">Library visibility level</param>
    /// <param name="tags">Optional tags for categorization</param>
    /// <param name="permissions">Initial permissions</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created library</returns>
    Task<PromptLibrary> CreateLibraryAsync(
        string name, 
        Guid promptLabId, 
        Guid tenantId, 
        Guid createdBy,
        string? description = null,
        LibraryVisibility visibility = LibraryVisibility.Private,
        string? tags = null,
        List<LibraryPermission>? permissions = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing prompt library with audit trail
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="updatedBy">User updating the library</param>
    /// <param name="name">Updated name</param>
    /// <param name="description">Updated description</param>
    /// <param name="promptLabId">Updated prompt lab ID</param>
    /// <param name="visibility">Updated visibility</param>
    /// <param name="tags">Updated tags</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated library, or null if not found</returns>
    Task<PromptLibrary?> UpdateLibraryAsync(
        Guid libraryId, 
        Guid tenantId, 
        Guid updatedBy,
        string? name = null, 
        string? description = null, 
        Guid? promptLabId = null,
        LibraryVisibility? visibility = null,
        string? tags = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft delete a prompt library by ID with audit trail
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="deletedBy">User deleting the library</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the library was deleted, false otherwise</returns>
    Task<bool> SoftDeleteLibraryAsync(Guid libraryId, Guid tenantId, Guid deletedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Permanently delete a prompt library (hard delete)
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if permanently deleted</returns>
    Task<bool> HardDeleteLibraryAsync(Guid libraryId, Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restore a soft-deleted library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="restoredBy">User restoring the library</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if restored successfully</returns>
    Task<bool> RestoreLibraryAsync(Guid libraryId, Guid tenantId, Guid restoredBy, CancellationToken cancellationToken = default);

    #endregion

    #region Library Discovery & Search

    /// <summary>
    /// Search prompt libraries by name or description with advanced filtering
    /// </summary>
    /// <param name="searchTerm">Search term to match against name or description</param>
    /// <param name="tenantId">Tenant ID for isolation</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <param name="visibility">Optional visibility filter</param>
    /// <param name="tags">Optional tags filter</param>
    /// <param name="includeDeleted">Whether to include soft-deleted items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching prompt libraries</returns>
    Task<List<PromptLibrary>> SearchLibrariesAsync(
        string searchTerm, 
        Guid tenantId,
        Guid? promptLabId = null,
        LibraryVisibility? visibility = null,
        List<string>? tags = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get libraries by visibility level with tenant isolation
    /// </summary>
    /// <param name="visibility">Visibility level to filter by</param>
    /// <param name="tenantId">Tenant ID for isolation</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of libraries matching visibility criteria</returns>
    Task<List<PromptLibrary>> GetLibrariesByVisibilityAsync(LibraryVisibility visibility, Guid tenantId, Guid? promptLabId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get recently updated libraries with enhanced filtering
    /// </summary>
    /// <param name="tenantId">Tenant ID for isolation</param>
    /// <param name="timeRange">Time range to look back</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <param name="userId">Optional user ID for user-specific results</param>
    /// <param name="limit">Maximum number of libraries to return</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of recently updated libraries</returns>
    Task<List<PromptLibrary>> GetRecentlyUpdatedLibrariesAsync(
        Guid tenantId,
        TimeSpan? timeRange = null,
        Guid? promptLabId = null,
        Guid? userId = null,
        int limit = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get popular libraries based on usage analytics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="timeRange">Time range for popularity calculation</param>
    /// <param name="promptLabId">Optional lab filter</param>
    /// <param name="limit">Maximum number of libraries</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Popular libraries with metrics</returns>
    Task<List<PromptLibrary>> GetPopularLibrariesAsync(
        Guid tenantId,
        TimeSpan? timeRange = null,
        Guid? promptLabId = null,
        int limit = 10,
        CancellationToken cancellationToken = default);

    #endregion

    #region Library Statistics & Analytics

    /// <summary>
    /// Get comprehensive library statistics including template count, execution count, etc.
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="timeRange">Time range for statistics</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Enhanced library statistics</returns>
    Task<LibraryStatistics> GetLibraryStatisticsAsync(Guid libraryId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get template count for a library with filters
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="includeDeleted">Whether to include soft-deleted templates</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of templates in the library</returns>
    Task<long> GetTemplateCountAsync(Guid libraryId, Guid tenantId, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get execution count for all templates in a library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="timeRange">Optional time range filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Total number of executions for templates in the library</returns>
    Task<long> GetExecutionCountAsync(Guid libraryId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get library performance analytics
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="timeRange">Time range for analysis</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Performance analytics</returns>
    Task<LibraryPerformanceAnalytics> GetLibraryPerformanceAsync(Guid libraryId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get usage trends for a library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="granularity">Time granularity (hour, day, week, month)</param>
    /// <param name="timeRange">Time range for trends</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Usage trend data</returns>
    Task<LibraryUsageTrends> GetLibraryUsageTrendsAsync(
        Guid libraryId, 
        Guid tenantId,
        string granularity = "day",
        TimeSpan? timeRange = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Library Validation & Quality

    /// <summary>
    /// Check if a library name is unique within a prompt lab and tenant
    /// </summary>
    /// <param name="name">Library name to check</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="excludeLibraryId">Optional library ID to exclude from uniqueness check</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if name is unique, false otherwise</returns>
    Task<bool> IsLibraryNameUniqueAsync(string name, Guid promptLabId, Guid tenantId, Guid? excludeLibraryId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Validate library creation/update data with comprehensive checks
    /// </summary>
    /// <param name="name">Library name</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="excludeLibraryId">Optional library ID to exclude from validation</param>
    /// <param name="additionalData">Additional validation data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Enhanced validation result</returns>
    Task<LibraryValidationResult> ValidateLibraryDataAsync(
        string name, 
        Guid promptLabId, 
        Guid tenantId,
        Guid? excludeLibraryId = null,
        Dictionary<string, object>? additionalData = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyze library quality and provide optimization suggestions
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Quality analysis result</returns>
    Task<LibraryQualityAnalysis> AnalyzeLibraryQualityAsync(Guid libraryId, Guid tenantId, CancellationToken cancellationToken = default);

    #endregion

    #region Permissions & Access Control

    /// <summary>
    /// Get library permissions for a user
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User permissions for the library</returns>  
    Task<LibraryPermission> GetLibraryPermissionAsync(
        Guid libraryId, 
        Guid userId, 
        Guid tenantId, 
        string permission,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Update library permissions
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="updatedBy">User updating permissions</param>
    /// <param name="permissions">New permissions</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if updated successfully</returns>
    Task<bool> UpdateLibraryPermissionsAsync(
        Guid libraryId, 
        Guid tenantId, 
        Guid updatedBy,
        List<LibraryPermission> permissions,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if user has specific permission on library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="permission">Permission to check</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if user has permission</returns>
    Task<bool> HasPermissionAsync(Guid libraryId, Guid userId, Guid tenantId, string permission, CancellationToken cancellationToken = default);

    #endregion

    #region Import/Export & Migration

    /// <summary>
    /// Import a prompt library from JSON data with enhanced options
    /// </summary>
    /// <param name="jsonContent">JSON content representing the library and its templates</param>
    /// <param name="promptLabId">Target prompt lab ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="importedBy">User performing the import</param>
    /// <param name="importOptions">Import configuration options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Import result with detailed information</returns>
    Task<LibraryImportResult> ImportLibraryFromJsonAsync(
        string jsonContent, 
        Guid promptLabId, 
        Guid tenantId, 
        Guid importedBy,
        LibraryImportOptions? importOptions = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Export a prompt library to JSON with enhanced options
    /// </summary>
    /// <param name="libraryId">Library ID to export</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="exportOptions">Export configuration options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Export result with JSON content</returns>
    Task<LibraryExportResult> ExportLibraryToJsonAsync(
        Guid libraryId, 
        Guid tenantId,
        LibraryExportOptions? exportOptions = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Clone a library to a new library
    /// </summary>
    /// <param name="sourceLibraryId">Source library ID</param>
    /// <param name="targetLibraryName">New library name</param>
    /// <param name="targetPromptLabId">Target lab ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="clonedBy">User performing the clone</param>
    /// <param name="cloneOptions">Clone options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Cloned library</returns>
    Task<PromptLibrary> CloneLibraryAsync(
        Guid sourceLibraryId,
        string targetLibraryName,
        Guid targetPromptLabId,
        Guid tenantId,
        Guid clonedBy,
        LibraryCloneOptions? cloneOptions = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Collaboration & Sharing

    /// <summary>
    /// Share library with other users or teams
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="sharedBy">User sharing the library</param>
    /// <param name="shareTargets">Users/teams to share with</param>
    /// <param name="permissions">Permissions to grant</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Sharing result</returns>
    Task<LibrarySharingResult> ShareLibraryAsync(
        Guid libraryId,
        Guid tenantId,
        Guid sharedBy,
        List<ShareTarget> shareTargets,
        List<string> permissions,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get libraries shared with a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Shared libraries</returns>
    Task<List<PromptLibrary>> GetSharedLibrariesAsync(Guid userId, Guid tenantId, CancellationToken cancellationToken = default);

    #endregion
}

/// <summary>
/// Result of sharing a library with other users or teams
/// This class encapsulates the outcome of a sharing operation, including success status, 
/// shared targets, failed targets, and an optional message.
/// It is used to provide feedback on the sharing process, indicating which targets were successfully shared and
/// </summary>
public class LibrarySharingResult
{
    /// <summary>
    /// Indicates if the sharing was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// List of targets that were successfully shared with
    /// </summary>
    public List<ShareTarget> SharedTargets { get; set; } = new List<ShareTarget>();

    /// <summary>
    /// List of targets that failed to share
    /// </summary>
    public List<ShareTarget> FailedTargets { get; set; } = new List<ShareTarget>();

    /// <summary>
    /// Optional message with details about the sharing operation
    /// </summary>
    public string? Message { get; set; }
}

/// <summary>
/// Options for cloning a prompt library
/// This class allows customization of the cloning process, such as whether to clone permissions and templates,
/// </summary>
public class LibraryCloneOptions
{
    /// <summary>
    /// Whether to clone permissions from the source library
    /// </summary>
    public bool ClonePermissions { get; set; } = true;

    /// <summary>
    /// Whether to clone templates from the source library
    /// </summary>
    public bool CloneTemplates { get; set; } = true;

    /// <summary>
    /// Optional custom metadata to include in the cloned library
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }
}

/// <summary>
/// Options for exporting a prompt library to JSON
/// This class allows customization of the export process, such as whether to include permissions and templates,
/// </summary>
public class LibraryExportOptions
{
    /// <summary>
    /// Whether to include permissions in the export
    /// </summary>
    public bool IncludePermissions { get; set; } = true;

    /// <summary>
    /// Whether to include templates in the export
    /// </summary>
    public bool IncludeTemplates { get; set; } = true;

    /// <summary>
    /// Optional custom metadata to include in the export
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }
}
/// <summary>
/// Result of exporting a prompt library to JSON
/// This class encapsulates the outcome of an export operation, including success status,
/// JSON content of the exported library, and an optional message.
/// it is used to provide feedback on the export process, indicating whether the export was successful
/// </summary>
public class LibraryExportResult
{
    /// <summary>
    /// Indicates if the export was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// JSON content of the exported library
    /// </summary>
    public string? JsonContent { get; set; }

    /// <summary>
    /// Optional message with details about the export operation
    /// </summary>
    public string? Message { get; set; }
}