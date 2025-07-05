using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Library;

namespace PromptStudio.Core.Interfaces.Library;

/// <summary>
/// Service interface for comprehensive prompt library lifecycle management within lab environments.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// <para>Library-focused service in the business service layer, responsible for organizing prompt templates
/// into logical collections with categorization, access control, and collaborative features. Operates within
/// the context of prompt labs and integrates with template management, versioning, and sharing systems.</para>
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// <para>Implementations must maintain hierarchical organization within labs, support collaborative
/// development workflows, enforce access control at library level, and provide comprehensive
/// categorization and discovery capabilities for prompt template collections.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
/// <item><description>Maintain parent-child relationships with labs and ensure referential integrity</description></item>
/// <item><description>Implement collaborative features including sharing and permission management</description></item>
/// <item><description>Support hierarchical categorization and flexible tagging systems</description></item>
/// <item><description>Provide efficient search and discovery mechanisms for large template collections</description></item>
/// <item><description>Handle concurrent access with optimistic concurrency control</description></item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Integrates with IPromptLabManagementService for lab-level operations</description></item>
/// <item><description>Coordinates with IPromptTemplateService for template management within libraries</description></item>
/// <item><description>Connects to categorization and tagging services for organization</description></item>
/// <item><description>Utilizes sharing and collaboration services for multi-user workflows</description></item>
/// <item><description>Implements event-driven architecture for real-time collaboration</description></item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
/// <item><description>Mock implementations should preserve library hierarchy and access control</description></item>
/// <item><description>Integration tests must verify lab-library relationships and constraints</description></item>
/// <item><description>Performance tests should validate search and categorization at scale</description></item>
/// <item><description>Test collaboration scenarios including sharing and permission inheritance</description></item>
/// </list>
/// </remarks>
public interface IPromptLibraryService
{
    #region Library Lifecycle Operations

    /// <summary>
    /// Creates a new prompt library within a specified lab with comprehensive configuration.
    /// Establishes categorization, access control, and initial template organization structure.
    /// </summary>
    /// <param name="labId">Parent lab identifier for library context</param>
    /// <param name="name">Library display name, must be unique within lab scope</param>
    /// <param name="description">Detailed description of library purpose and content scope</param>
    /// <param name="category">Library category for organization and discovery</param>
    /// <param name="visibility">Access level controlling who can view and use library</param>
    /// <param name="userId">Creator identifier for ownership and audit trails</param>
    /// <param name="tags">Optional metadata tags for enhanced categorization</param>
    /// <param name="color">Optional color theme for visual organization</param>
    /// <param name="icon">Optional icon identifier for visual representation</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Created library with generated identifiers and configuration</returns>
    /// <exception cref="ArgumentException">Thrown when name is invalid or violates naming constraints</exception>
    /// <exception cref="NotFoundException">Thrown when parent lab doesn't exist or is inaccessible</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks library creation permissions</exception>
    /// <exception cref="DuplicateLibraryNameException">Thrown when library name exists within lab scope</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Library names must be unique within parent lab scope</description></item>
    /// <item><description>User must have library creation permissions in target lab</description></item>
    /// <item><description>Category must be valid and appropriate for lab context</description></item>
    /// <item><description>Default permissions inherited from lab-level settings</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Creates initial folder structure and organization templates</description></item>
    /// <item><description>Establishes audit trails and change tracking for library</description></item>
    /// <item><description>Initializes access control with lab-inherited permissions</description></item>
    /// <item><description>Sets up collaboration features and sharing capabilities</description></item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// // Create a customer service library for support templates
    /// var library = await libraryService.CreateLibraryAsync(
    ///     labId,
    ///     "Customer Support Templates",
    ///     "Standardized prompts for customer service scenarios",
    ///     LibraryCategory.CustomerService,
    ///     LibraryVisibility.Team,
    ///     currentUserId,
    ///     new[] { "support", "customer-service", "templates" },
    ///     "#1976d2",
    ///     "support_agent",
    ///     cancellationToken
    /// );
    /// </code>
    /// </remarks>
    Task<PromptLibrary> CreateLibraryAsync(
        Guid labId,
        string name,
        string description,
        LibraryCategory category,
        LibraryVisibility visibility,
        string userId,
        IEnumerable<string>? tags = null,
        string? color = null,
        string? icon = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a prompt library by identifier with comprehensive relationship loading.
    /// Supports access control validation and tenant isolation.
    /// </summary>
    /// <param name="libraryId">Unique library identifier</param>
    /// <param name="userId">User identifier for access control validation</param>
    /// <param name="includeTemplates">Whether to load associated prompt templates</param>
    /// <param name="includeStatistics">Whether to include usage and performance statistics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Library details or null if not found or access denied</returns>
    /// <exception cref="ArgumentException">Thrown when libraryId is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks read access</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to library or parent lab</description></item>
    /// <item><description>Archived libraries excluded unless user has admin privileges</description></item>
    /// <item><description>Template loading respects individual template permissions</description></item>
    /// <item><description>Statistics require additional computational resources</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements efficient loading strategies for related entities</description></item>
    /// <item><description>Validates access permissions before exposing sensitive data</description></item>
    /// <item><description>Supports partial loading for performance optimization</description></item>
    /// <item><description>Uses caching for frequently accessed library metadata</description></item>
    /// </list>
    /// </remarks>
    Task<PromptLibrary?> GetLibraryAsync(
        Guid libraryId,
        string userId,
        bool includeTemplates = false,
        bool includeStatistics = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all libraries within a lab scope with filtering and pagination support.
    /// Implements access-controlled results based on user permissions and visibility settings.
    /// </summary>
    /// <param name="labId">Parent lab identifier for scope filtering</param>
    /// <param name="userId">User identifier for access-controlled results</param>
    /// <param name="category">Optional filter by library category</param>
    /// <param name="visibility">Optional filter by visibility level</param>
    /// <param name="tags">Optional filter by tags using AND logic</param>
    /// <param name="searchTerm">Optional text search across names and descriptions</param>
    /// <param name="includeArchived">Whether to include archived libraries</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="pageSize">Number of items per page (max 100)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated collection of accessible libraries</returns>
    /// <exception cref="ArgumentException">Thrown when pagination parameters are invalid</exception>
    /// <exception cref="NotFoundException">Thrown when lab doesn't exist or is inaccessible</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks lab access</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Results filtered by user permissions and library visibility</description></item>
    /// <item><description>Search includes fuzzy matching on names and descriptions</description></item>
    /// <item><description>Tag filtering uses AND logic for precise matching</description></item>
    /// <item><description>Archived libraries require special permissions</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements efficient database queries with proper indexing</description></item>
    /// <item><description>Supports result caching for frequently accessed library lists</description></item>
    /// <item><description>Uses full-text search capabilities for enhanced discovery</description></item>
    /// <item><description>Provides sorting options for various organizational needs</description></item>
    /// </list>
    /// </remarks>
    Task<PagedResult<PromptLibrary>> GetLibrariesAsync(
        Guid labId,
        string userId,
        LibraryCategory? category = null,
        LibraryVisibility? visibility = null,
        IEnumerable<string>? tags = null,
        string? searchTerm = null,
        bool includeArchived = false,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);

    #endregion

    #region Library Organization and Categorization

    /// <summary>
    /// Updates library configuration including metadata, categorization, and organizational settings.
    /// Maintains audit trails and validates business constraints.
    /// </summary>
    /// <param name="libraryId">Unique library identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="name">Updated library name (optional)</param>
    /// <param name="description">Updated description (optional)</param>
    /// <param name="category">Updated category classification (optional)</param>
    /// <param name="visibility">Updated visibility level (optional)</param>
    /// <param name="tags">Updated tag collection (optional)</param>
    /// <param name="color">Updated color theme (optional)</param>
    /// <param name="icon">Updated icon identifier (optional)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Updated library with new configuration</returns>
    /// <exception cref="ArgumentException">Thrown when libraryId is invalid or name violates constraints</exception>
    /// <exception cref="NotFoundException">Thrown when library doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks update permissions</exception>
    /// <exception cref="ConcurrencyException">Thrown when library was modified by another user</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have administrative access to library</description></item>
    /// <item><description>Name changes must maintain uniqueness within lab scope</description></item>
    /// <item><description>Category changes may affect template organization</description></item>
    /// <item><description>All changes logged for audit and compliance purposes</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Uses optimistic concurrency control for conflict prevention</description></item>
    /// <item><description>Validates all business rules before applying changes</description></item>
    /// <item><description>Updates related caches and search indexes</description></item>
    /// <item><description>Notifies subscribers of library configuration changes</description></item>
    /// </list>
    /// </remarks>
    Task<PromptLibrary> UpdateLibraryAsync(
        Guid libraryId,
        string userId,
        string? name = null,
        string? description = null,
        LibraryCategory? category = null,
        LibraryVisibility? visibility = null,
        IEnumerable<string>? tags = null,
        string? color = null,
        string? icon = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reorganizes library content including template ordering and folder structure.
    /// Supports bulk operations for efficient library management.
    /// </summary>
    /// <param name="libraryId">Unique library identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="templateOrdering">New ordering for templates within library</param>
    /// <param name="pinned">Templates to pin for prioritized access</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Task representing the asynchronous reorganization operation</returns>
    /// <exception cref="ArgumentException">Thrown when libraryId is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when library doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks organization permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have administrative or organization permissions</description></item>
    /// <item><description>Template ordering affects display and discovery</description></item>
    /// <item><description>Pinned templates have priority in user interfaces</description></item>
    /// <item><description>Changes are reflected in search results and recommendations</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements bulk update operations for efficiency</description></item>
    /// <item><description>Maintains referential integrity during reorganization</description></item>
    /// <item><description>Updates search indexes and discovery algorithms</description></item>
    /// <item><description>Provides rollback capabilities for failed operations</description></item>
    /// </list>
    /// </remarks>
    Task ReorganizeLibraryAsync(
        Guid libraryId,
        string userId,
        IEnumerable<Guid>? templateOrdering = null,
        IEnumerable<Guid>? pinned = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Sharing and Collaboration

    /// <summary>
    /// Shares a library with specified users or groups with configured access levels.
    /// Implements fine-grained permission control and collaboration features.
    /// </summary>
    /// <param name="libraryId">Unique library identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="shareTargets">Collection of users or groups to share with</param>
    /// <param name="permissions">Access permissions to grant</param>
    /// <param name="message">Optional message to include with sharing notification</param>
    /// <param name="expirationDate">Optional expiration date for shared access</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Sharing results with success status and recipient details</returns>
    /// <exception cref="ArgumentException">Thrown when libraryId is invalid or targets are malformed</exception>
    /// <exception cref="NotFoundException">Thrown when library doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks sharing permissions</exception>
    /// <exception cref="InvalidOperationException">Thrown when sharing violates security policies</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have sharing permissions for the library</description></item>
    /// <item><description>Share targets must be valid users or groups within organization</description></item>
    /// <item><description>Permissions cannot exceed sharer's own access level</description></item>
    /// <item><description>Sharing may be restricted by organization policies</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Sends notifications to share recipients through configured channels</description></item>
    /// <item><description>Maintains audit trail of all sharing activities</description></item>
    /// <item><description>Implements permission inheritance and conflict resolution</description></item>
    /// <item><description>Supports bulk sharing operations for efficiency</description></item>
    /// </list>
    /// </remarks>
    Task<LibrarySharingResult> ShareLibraryAsync(
        Guid libraryId,
        string userId,
        IEnumerable<ShareTarget> shareTargets,
        LibraryPermission permissions,
        string? message = null,
        DateTime? expirationDate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Exports library content in various formats for backup, migration, or sharing purposes.
    /// Supports filtering and customization of export content.
    /// </summary>
    /// <param name="libraryId">Unique library identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="exportOptions">Configuration for export format and content</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Export results with generated content and metadata</returns>
    /// <exception cref="ArgumentException">Thrown when libraryId is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when library doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks export permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to library and templates</description></item>
    /// <item><description>Export may exclude sensitive information based on permissions</description></item>
    /// <item><description>Large libraries may require streaming or chunked exports</description></item>
    /// <item><description>Export operations are logged for compliance purposes</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Supports multiple export formats (JSON, CSV, ZIP archives)</description></item>
    /// <item><description>Implements streaming for large library exports</description></item>
    /// <item><description>Validates content before export to ensure consistency</description></item>
    /// <item><description>Provides progress tracking for long-running exports</description></item>
    /// </list>
    /// </remarks>
    Task<LibraryExportResult> ExportLibraryAsync(
        Guid libraryId,
        string userId,
        LibraryExportOptions exportOptions,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Imports library content from external sources with validation and conflict resolution.
    /// Supports various import formats and merge strategies.
    /// </summary>
    /// <param name="labId">Target lab identifier for imported library</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="importOptions">Configuration for import source and behavior</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Import results with created library and processing details</returns>
    /// <exception cref="ArgumentException">Thrown when import data is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when target lab doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks import permissions</exception>
    /// <exception cref="ValidationException">Thrown when imported content fails validation</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have library creation permissions in target lab</description></item>
    /// <item><description>Imported content must pass validation and security checks</description></item>
    /// <item><description>Name conflicts resolved according to specified strategy</description></item>
    /// <item><description>Import operation maintains referential integrity</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Validates import content before applying any changes</description></item>
    /// <item><description>Implements rollback capabilities for failed imports</description></item>
    /// <item><description>Provides detailed progress and error reporting</description></item>
    /// <item><description>Supports incremental imports for large datasets</description></item>
    /// </list>
    /// </remarks>
    Task<LibraryImportResult> ImportLibraryAsync(
        Guid labId,
        string userId,
        LibraryImportOptions importOptions,
        CancellationToken cancellationToken = default);

    #endregion

    #region Validation and Analytics

    /// <summary>
    /// Performs comprehensive validation of library structure, content, and compliance.
    /// Evaluates template quality, categorization accuracy, and governance adherence.
    /// </summary>
    /// <param name="libraryId">Unique library identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="includeTemplateAnalysis">Whether to perform deep template content analysis</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive validation results with recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when libraryId is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when library doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks validation permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to library and templates</description></item>
    /// <item><description>Validation includes compliance with organizational standards</description></item>
    /// <item><description>Template analysis requires additional computational resources</description></item>
    /// <item><description>Results cached to avoid expensive re-validation</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Orchestrates validation across template and content services</description></item>
    /// <item><description>Implements parallel validation for performance optimization</description></item>
    /// <item><description>Provides actionable recommendations for improvement</description></item>
    /// <item><description>Supports incremental validation for large libraries</description></item>
    /// </list>
    /// </remarks>
    Task<LibraryValidationResult> ValidateLibraryAsync(
        Guid libraryId,
        string userId,
        bool includeTemplateAnalysis = false,
        CancellationToken cancellationToken = default);

    #endregion
}
