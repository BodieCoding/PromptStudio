using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Lab;

namespace PromptStudio.Core.Interfaces.Lab;

/// <summary>
/// Service interface for comprehensive prompt lab lifecycle management in enterprise LLMOps environments.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// <para>Core lab management service operating in the business service layer, responsible for lab creation,
/// configuration, governance, and tenant isolation. Integrates with identity management, resource allocation,
/// and compliance systems to provide complete workspace management for prompt engineering teams.</para>
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// <para>Implementations must enforce tenant isolation, support soft deletion with audit trails,
/// handle concurrent access through optimistic concurrency control, and maintain comprehensive
/// security boundaries between different organizational units and projects.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
/// <item><description>Enforce strict tenant isolation for multi-tenant deployments</description></item>
/// <item><description>Implement comprehensive audit logging for all lab operations</description></item>
/// <item><description>Support both synchronous and asynchronous operations with proper cancellation</description></item>
/// <item><description>Handle optimistic concurrency conflicts through row versioning</description></item>
/// <item><description>Validate business rules and organizational constraints</description></item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Integrates with IPromptLibraryService for library management within labs</description></item>
/// <item><description>Coordinates with IWorkflowService for workflow lifecycle management</description></item>
/// <item><description>Connects to identity services for user and team authorization</description></item>
/// <item><description>Utilizes Unit of Work pattern for transactional consistency</description></item>
/// <item><description>Implements Repository pattern for data access abstraction</description></item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
/// <item><description>Mock implementations should preserve business rule validation</description></item>
/// <item><description>Integration tests must verify tenant isolation and security boundaries</description></item>
/// <item><description>Performance tests should validate concurrent access patterns</description></item>
/// <item><description>Test error scenarios including resource constraints and validation failures</description></item>
/// </list>
/// </remarks>
public interface IPromptLabManagementService
{
    #region Lab Lifecycle Operations

    /// <summary>
    /// Creates a new prompt lab with comprehensive configuration and governance setup.
    /// Establishes tenant boundaries, initializes default policies, and sets up audit trails.
    /// </summary>
    /// <param name="name">The display name for the lab, must be unique within the tenant scope</param>
    /// <param name="description">Optional detailed description of the lab's purpose and scope</param>
    /// <param name="visibility">Visibility level controlling access and discoverability</param>
    /// <param name="organizationId">Organization identifier for enterprise multi-tenancy</param>
    /// <param name="ownerId">User identifier for the lab owner with administrative privileges</param>
    /// <param name="tags">Optional metadata tags for categorization and discovery</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>The created lab with generated identifiers and initial configuration</returns>
    /// <exception cref="ArgumentException">Thrown when name is null, empty, or violates naming constraints</exception>
    /// <exception cref="DuplicateLabNameException">Thrown when lab name already exists within tenant scope</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks lab creation permissions</exception>
    /// <exception cref="ResourceQuotaExceededException">Thrown when organization lab quota is exceeded</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Lab names must be unique within organization scope</description></item>
    /// <item><description>Owner must have lab creation permissions within organization</description></item>
    /// <item><description>Organization must not exceed maximum lab quota limits</description></item>
    /// <item><description>Lab ID will be auto-generated using friendly URL-safe format</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Creates default library structure and initial governance policies</description></item>
    /// <item><description>Establishes audit trail and change tracking infrastructure</description></item>
    /// <item><description>Initializes resource allocation and quota management</description></item>
    /// <item><description>Sets up default access control and permission templates</description></item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// // Create a customer service lab for prompt engineering team
    /// var lab = await labService.CreateLabAsync(
    ///     "Customer Service AI",
    ///     "Prompt templates and workflows for customer support automation",
    ///     LabVisibility.Team,
    ///     organizationId,
    ///     currentUserId,
    ///     new[] { "customer-service", "support", "automation" },
    ///     cancellationToken
    /// );
    /// </code>
    /// </remarks>
    Task<PromptLab> CreateLabAsync(
        string name,
        string? description,
        LabVisibility visibility,
        Guid organizationId,
        string ownerId,
        IEnumerable<string>? tags = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a prompt lab by its unique identifier with comprehensive relationship loading.
    /// Supports tenant isolation and access control validation.
    /// </summary>
    /// <param name="labId">Unique lab identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="userId">User identifier for access control validation</param>
    /// <param name="includeLibraries">Whether to load associated prompt libraries</param>
    /// <param name="includeWorkflows">Whether to load associated workflows</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Lab details or null if not found or access denied</returns>
    /// <exception cref="ArgumentException">Thrown when labId is empty or invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks read access to lab</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to lab or organization</description></item>
    /// <item><description>Soft-deleted labs are excluded unless user has admin privileges</description></item>
    /// <item><description>Tenant isolation prevents cross-organization access</description></item>
    /// <item><description>Related entities respect their own access control rules</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements lazy loading for related entities to optimize performance</description></item>
    /// <item><description>Validates access permissions before loading sensitive data</description></item>
    /// <item><description>Uses read-optimized queries with appropriate caching strategies</description></item>
    /// <item><description>Supports partial loading to minimize data transfer</description></item>
    /// </list>
    /// </remarks>
    Task<PromptLab?> GetLabAsync(
        Guid labId,
        Guid organizationId,
        string userId,
        bool includeLibraries = false,
        bool includeWorkflows = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all labs accessible to a user within an organization scope.
    /// Supports filtering, pagination, and sorting for enterprise-scale deployments.
    /// </summary>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="userId">User identifier for access-controlled results</param>
    /// <param name="visibility">Optional filter by visibility level</param>
    /// <param name="tags">Optional filter by tags using AND logic</param>
    /// <param name="includeArchived">Whether to include archived/soft-deleted labs</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="pageSize">Number of items per page (max 100)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated collection of accessible labs with metadata</returns>
    /// <exception cref="ArgumentException">Thrown when pagination parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks organization access</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Results filtered by user's access permissions and visibility rules</description></item>
    /// <item><description>Page size limited to prevent performance issues</description></item>
    /// <item><description>Archived labs require special permissions to view</description></item>
    /// <item><description>Tag filtering uses AND logic for precise matching</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements efficient pagination with database-level limiting</description></item>
    /// <item><description>Uses indexed queries for optimal performance at scale</description></item>
    /// <item><description>Supports result caching for frequently accessed lab lists</description></item>
    /// <item><description>Returns minimal lab information for list views</description></item>
    /// </list>
    /// </remarks>
    Task<PagedResult<PromptLab>> GetLabsAsync(
        Guid organizationId,
        string userId,
        LabVisibility? visibility = null,
        IEnumerable<string>? tags = null,
        bool includeArchived = false,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);

    #endregion

    #region Lab Configuration and Governance

    /// <summary>
    /// Updates lab configuration including metadata, visibility, and governance settings.
    /// Maintains audit trails and validates business constraints.
    /// </summary>
    /// <param name="labId">Unique lab identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="name">Updated lab name (optional)</param>
    /// <param name="description">Updated description (optional)</param>
    /// <param name="visibility">Updated visibility level (optional)</param>
    /// <param name="tags">Updated tag collection (optional)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Updated lab with new configuration</returns>
    /// <exception cref="ArgumentException">Thrown when labId is invalid or name violates constraints</exception>
    /// <exception cref="NotFoundException">Thrown when lab doesn't exist or is inaccessible</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks update permissions</exception>
    /// <exception cref="ConcurrencyException">Thrown when lab was modified by another user</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have administrative access to lab</description></item>
    /// <item><description>Name changes must maintain uniqueness within organization</description></item>
    /// <item><description>Visibility changes may require additional permissions</description></item>
    /// <item><description>Changes are logged for compliance and audit purposes</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Uses optimistic concurrency control to prevent data conflicts</description></item>
    /// <item><description>Validates permissions before applying any changes</description></item>
    /// <item><description>Creates comprehensive audit log entries for all modifications</description></item>
    /// <item><description>Updates related entities and caches as needed</description></item>
    /// </list>
    /// </remarks>
    Task<PromptLab> UpdateLabAsync(
        Guid labId,
        Guid organizationId,
        string userId,
        string? name = null,
        string? description = null,
        LabVisibility? visibility = null,
        IEnumerable<string>? tags = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Archives a lab using soft deletion while preserving all data for compliance.
    /// Maintains referential integrity and provides recovery capabilities.
    /// </summary>
    /// <param name="labId">Unique lab identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="reason">Reason for archival for audit purposes</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Task representing the asynchronous archival operation</returns>
    /// <exception cref="ArgumentException">Thrown when labId is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when lab doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks delete permissions</exception>
    /// <exception cref="InvalidOperationException">Thrown when lab has active dependencies</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have administrative delete permissions</description></item>
    /// <item><description>Active workflows and executions must be completed or cancelled</description></item>
    /// <item><description>Archive operation is logged for compliance tracking</description></item>
    /// <item><description>Related libraries and templates are also soft-deleted</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements cascading soft deletion for all related entities</description></item>
    /// <item><description>Preserves all data for compliance and potential recovery</description></item>
    /// <item><description>Updates indexes and caches to reflect archived status</description></item>
    /// <item><description>Handles cleanup of active sessions and temporary resources</description></item>
    /// </list>
    /// </remarks>
    Task ArchiveLabAsync(
        Guid labId,
        Guid organizationId,
        string userId,
        string reason,
        CancellationToken cancellationToken = default);

    #endregion

    #region Validation and Quality Assurance

    /// <summary>
    /// Performs comprehensive validation of lab environment including all libraries,
    /// templates, workflows, and governance compliance.
    /// </summary>
    /// <param name="labId">Unique lab identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="includePerformanceAnalysis">Whether to include performance metrics and optimization suggestions</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive validation results with detailed breakdown</returns>
    /// <exception cref="ArgumentException">Thrown when labId is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when lab doesn't exist or is inaccessible</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks validation permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to lab and its components</description></item>
    /// <item><description>Validation includes compliance checks and quality gates</description></item>
    /// <item><description>Performance analysis requires additional computational resources</description></item>
    /// <item><description>Results are cached to avoid expensive re-validation</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Orchestrates validation across multiple service dependencies</description></item>
    /// <item><description>Implements parallel validation for performance optimization</description></item>
    /// <item><description>Provides detailed error reporting with actionable recommendations</description></item>
    /// <item><description>Supports incremental validation for large lab environments</description></item>
    /// </list>
    /// </remarks>
    Task<LabValidationResult> ValidateLabAsync(
        Guid labId,
        Guid organizationId,
        string userId,
        bool includePerformanceAnalysis = false,
        CancellationToken cancellationToken = default);

    #endregion
}
