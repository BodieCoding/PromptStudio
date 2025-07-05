using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Templates;
using PromptStudio.Core.DTOs.Variables;

namespace PromptStudio.Core.Interfaces.Templates;

/// <summary>
/// Service interface for comprehensive prompt template lifecycle management within library contexts.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// <para>Template-focused service in the business service layer, responsible for creating, versioning,
/// and managing prompt templates with variable definitions, content validation, and collaborative features.
/// Operates within library contexts and integrates with execution, analytics, and sharing systems.</para>
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// <para>Implementations must maintain template-library relationships, support comprehensive versioning
/// with change tracking, enforce content validation and quality checks, and provide collaborative
/// development features including sharing, reviewing, and approval workflows.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
/// <item><description>Maintain referential integrity with libraries and enforce access control inheritance</description></item>
/// <item><description>Implement comprehensive versioning with branching and merging capabilities</description></item>
/// <item><description>Support real-time collaborative editing with conflict resolution</description></item>
/// <item><description>Provide advanced content validation including syntax and semantic analysis</description></item>
/// <item><description>Handle concurrent access with optimistic concurrency and merge conflict resolution</description></item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Integrates with IPromptLibraryService for library-level operations and organization</description></item>
/// <item><description>Coordinates with IVariableService for variable definition and validation</description></item>
/// <item><description>Connects to execution services for performance tracking and optimization</description></item>
/// <item><description>Utilizes versioning and change management services for content history</description></item>
/// <item><description>Implements notification services for collaborative workflows</description></item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
/// <item><description>Mock implementations should preserve template validation and variable constraints</description></item>
/// <item><description>Integration tests must verify library-template relationships and access control</description></item>
/// <item><description>Performance tests should validate template processing and content analysis at scale</description></item>
/// <item><description>Test collaborative scenarios including versioning conflicts and resolution</description></item>
/// </list>
/// </remarks>
public interface IPromptTemplateService
{
    #region Template Lifecycle Operations

    /// <summary>
    /// Creates a new prompt template within a specified library with comprehensive configuration.
    /// Establishes variable definitions, content validation, and version control infrastructure.
    /// </summary>
    /// <param name="libraryId">Parent library identifier for template context</param>
    /// <param name="name">Template display name, must be unique within library scope</param>
    /// <param name="content">Template content with variable placeholders and formatting</param>
    /// <param name="description">Detailed description of template purpose and usage</param>
    /// <param name="category">Template category for organization and discovery</param>
    /// <param name="userId">Creator identifier for ownership and audit trails</param>
    /// <param name="variables">Variable definitions with types and constraints</param>
    /// <param name="tags">Optional metadata tags for enhanced categorization</param>
    /// <param name="isPublic">Whether template is publicly discoverable</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Created template with generated identifiers and initial version</returns>
    /// <exception cref="ArgumentException">Thrown when name, content, or variables are invalid</exception>
    /// <exception cref="NotFoundException">Thrown when parent library doesn't exist or is inaccessible</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks template creation permissions</exception>
    /// <exception cref="DuplicateTemplateNameException">Thrown when template name exists within library</exception>
    /// <exception cref="ValidationException">Thrown when content or variables fail validation</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Template names must be unique within parent library scope</description></item>
    /// <item><description>Content must pass syntax validation and variable consistency checks</description></item>
    /// <item><description>Variable definitions must be complete and properly typed</description></item>
    /// <item><description>User must have template creation permissions in target library</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Creates initial version (v1.0) with complete audit trail</description></item>
    /// <item><description>Validates content syntax and variable placeholder consistency</description></item>
    /// <item><description>Establishes change tracking and version control infrastructure</description></item>
    /// <item><description>Initializes performance baselines and quality metrics</description></item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// // Create a customer email response template
    /// var template = await templateService.CreateTemplateAsync(
    ///     libraryId,
    ///     "Customer Email Response",
    ///     "Dear {{customerName}}, Thank you for contacting us about {{issue}}. {{response}}",
    ///     "Standardized response template for customer service emails",
    ///     TemplateCategory.CustomerService,
    ///     currentUserId,
    ///     new[] {
    ///         new VariableDefinition("customerName", VariableType.Text, "Customer's full name"),
    ///         new VariableDefinition("issue", VariableType.Text, "Customer's issue or inquiry"),
    ///         new VariableDefinition("response", VariableType.LongText, "Detailed response content")
    ///     },
    ///     new[] { "customer-service", "email", "response" },
    ///     false,
    ///     cancellationToken
    /// );
    /// </code>
    /// </remarks>
    Task<PromptTemplate> CreateTemplateAsync(
        Guid libraryId,
        string name,
        string content,
        string description,
        TemplateCategory category,
        string userId,
        IEnumerable<VariableDefinition>? variables = null,
        IEnumerable<string>? tags = null,
        bool isPublic = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a prompt template by identifier with comprehensive relationship loading.
    /// Supports version-specific loading and access control validation.
    /// </summary>
    /// <param name="templateId">Unique template identifier</param>
    /// <param name="userId">User identifier for access control validation</param>
    /// <param name="version">Specific version to retrieve (optional, defaults to latest)</param>
    /// <param name="includeVariables">Whether to load variable definitions</param>
    /// <param name="includeExecutions">Whether to load recent execution history</param>
    /// <param name="includeAnalytics">Whether to include performance analytics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Template details or null if not found or access denied</returns>
    /// <exception cref="ArgumentException">Thrown when templateId is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks read access</exception>
    /// <exception cref="VersionNotFoundException">Thrown when specified version doesn't exist</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to template or parent library</description></item>
    /// <item><description>Version access controlled based on user permissions and visibility</description></item>
    /// <item><description>Analytics data may be restricted based on user role</description></item>
    /// <item><description>Deleted templates excluded unless user has admin privileges</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements efficient loading with selective relationship inclusion</description></item>
    /// <item><description>Validates access permissions before exposing template content</description></item>
    /// <item><description>Supports content caching for frequently accessed templates</description></item>
    /// <item><description>Provides version-aware loading with fallback strategies</description></item>
    /// </list>
    /// </remarks>
    Task<PromptTemplate?> GetTemplateAsync(
        Guid templateId,
        string userId,
        string? version = null,
        bool includeVariables = true,
        bool includeExecutions = false,
        bool includeAnalytics = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all templates within a library scope with advanced filtering and search capabilities.
    /// Implements access-controlled results with comprehensive discovery features.
    /// </summary>
    /// <param name="libraryId">Parent library identifier for scope filtering</param>
    /// <param name="userId">User identifier for access-controlled results</param>
    /// <param name="category">Optional filter by template category</param>
    /// <param name="tags">Optional filter by tags using AND logic</param>
    /// <param name="searchTerm">Optional text search across names, descriptions, and content</param>
    /// <param name="isPublic">Optional filter by public visibility</param>
    /// <param name="hasExecutions">Optional filter for templates with execution history</param>
    /// <param name="includeArchived">Whether to include archived templates</param>
    /// <param name="sortBy">Sorting criteria for results</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="pageSize">Number of items per page (max 100)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated collection of accessible templates with metadata</returns>
    /// <exception cref="ArgumentException">Thrown when pagination parameters are invalid</exception>
    /// <exception cref="NotFoundException">Thrown when library doesn't exist or is inaccessible</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks library access</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Results filtered by user permissions and template visibility</description></item>
    /// <item><description>Search includes fuzzy matching with relevance scoring</description></item>
    /// <item><description>Tag filtering supports both AND and OR logic options</description></item>
    /// <item><description>Archived templates require special permissions to view</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements full-text search with content indexing</description></item>
    /// <item><description>Supports multiple sorting options with efficient database queries</description></item>
    /// <item><description>Uses result caching for popular search patterns</description></item>
    /// <item><description>Provides faceted search with category and tag aggregations</description></item>
    /// </list>
    /// </remarks>
    Task<PagedResult<PromptTemplate>> GetTemplatesAsync(
        Guid libraryId,
        string userId,
        TemplateCategory? category = null,
        IEnumerable<string>? tags = null,
        string? searchTerm = null,
        bool? isPublic = null,
        bool? hasExecutions = null,
        bool includeArchived = false,
        TemplateSortOption sortBy = TemplateSortOption.Name,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);

    #endregion

    #region Template Content and Variable Management

    /// <summary>
    /// Updates template content with comprehensive validation and version control.
    /// Creates new version automatically and maintains complete change history.
    /// </summary>
    /// <param name="templateId">Unique template identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="content">Updated template content with variables</param>
    /// <param name="changeDescription">Description of changes for version history</param>
    /// <param name="variables">Updated variable definitions (optional)</param>
    /// <param name="majorVersion">Whether this constitutes a major version change</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Updated template with new version information</returns>
    /// <exception cref="ArgumentException">Thrown when templateId is invalid or content is malformed</exception>
    /// <exception cref="NotFoundException">Thrown when template doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks edit permissions</exception>
    /// <exception cref="ValidationException">Thrown when content or variables fail validation</exception>
    /// <exception cref="ConcurrencyException">Thrown when template was modified by another user</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have edit permissions for the template</description></item>
    /// <item><description>Content must pass syntax validation and consistency checks</description></item>
    /// <item><description>Variable changes must maintain backward compatibility unless major version</description></item>
    /// <item><description>Change description required for audit trail and collaboration</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Creates new version with semantic versioning (major.minor.patch)</description></item>
    /// <item><description>Validates variable placeholder consistency in content</description></item>
    /// <item><description>Maintains backward compatibility analysis for existing executions</description></item>
    /// <item><description>Notifies collaborators of significant changes</description></item>
    /// </list>
    /// </remarks>
    Task<PromptTemplate> UpdateTemplateContentAsync(
        Guid templateId,
        string userId,
        string content,
        string changeDescription,
        IEnumerable<VariableDefinition>? variables = null,
        bool majorVersion = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates template metadata including name, description, and organizational information.
    /// Preserves content versioning while updating administrative properties.
    /// </summary>
    /// <param name="templateId">Unique template identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="name">Updated template name (optional)</param>
    /// <param name="description">Updated description (optional)</param>
    /// <param name="category">Updated category classification (optional)</param>
    /// <param name="tags">Updated tag collection (optional)</param>
    /// <param name="isPublic">Updated public visibility setting (optional)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Updated template with new metadata</returns>
    /// <exception cref="ArgumentException">Thrown when templateId is invalid or name violates constraints</exception>
    /// <exception cref="NotFoundException">Thrown when template doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks metadata update permissions</exception>
    /// <exception cref="DuplicateTemplateNameException">Thrown when name conflicts with existing template</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have administrative access to template</description></item>
    /// <item><description>Name changes must maintain uniqueness within library scope</description></item>
    /// <item><description>Category changes affect organization and discovery</description></item>
    /// <item><description>Public visibility changes require special permissions</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Updates search indexes and categorization systems</description></item>
    /// <item><description>Maintains audit trail for all metadata changes</description></item>
    /// <item><description>Validates business rules before applying changes</description></item>
    /// <item><description>Updates related caches and discovery mechanisms</description></item>
    /// </list>
    /// </remarks>
    Task<PromptTemplate> UpdateTemplateMetadataAsync(
        Guid templateId,
        string userId,
        string? name = null,
        string? description = null,
        TemplateCategory? category = null,
        IEnumerable<string>? tags = null,
        bool? isPublic = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyzes template content for quality, performance, and optimization opportunities.
    /// Provides comprehensive feedback including variable usage, content complexity, and improvement suggestions.
    /// </summary>
    /// <param name="templateId">Unique template identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="version">Specific version to analyze (optional, defaults to latest)</param>
    /// <param name="includePerformanceMetrics">Whether to include execution performance analysis</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive analysis results with recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when templateId is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when template doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks analysis permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to template for basic analysis</description></item>
    /// <item><description>Performance metrics require execution history access</description></item>
    /// <item><description>Analysis results cached to avoid expensive re-computation</description></item>
    /// <item><description>Sensitive analysis data filtered based on user permissions</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements natural language processing for content quality assessment</description></item>
    /// <item><description>Analyzes variable usage patterns and optimization opportunities</description></item>
    /// <item><description>Provides actionable recommendations for improvement</description></item>
    /// <item><description>Supports comparative analysis against similar templates</description></item>
    /// </list>
    /// </remarks>
    Task<TemplateAnalysisResult> AnalyzeTemplateAsync(
        Guid templateId,
        string userId,
        string? version = null,
        bool includePerformanceMetrics = false,
        CancellationToken cancellationToken = default);

    #endregion

    #region Versioning and History

    /// <summary>
    /// Retrieves complete version history for a template with change details and metadata.
    /// Supports filtering and pagination for templates with extensive revision history.
    /// </summary>
    /// <param name="templateId">Unique template identifier</param>
    /// <param name="userId">User identifier for access control validation</param>
    /// <param name="includeContent">Whether to include full content for each version</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="pageSize">Number of versions per page (max 50)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated version history with change details</returns>
    /// <exception cref="ArgumentException">Thrown when templateId is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when template doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks version history access</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to template for version history</description></item>
    /// <item><description>Content inclusion may be restricted for sensitive templates</description></item>
    /// <item><description>Deleted versions excluded unless user has admin privileges</description></item>
    /// <item><description>Version access may be restricted based on creation date and permissions</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements efficient queries with version-specific indexing</description></item>
    /// <item><description>Supports delta compression for storage optimization</description></item>
    /// <item><description>Provides change highlighting and difference visualization</description></item>
    /// <item><description>Includes authorship and collaboration information</description></item>
    /// </list>
    /// </remarks>
    Task<PagedResult<TemplateVersion>> GetTemplateVersionHistoryAsync(
        Guid templateId,
        string userId,
        bool includeContent = false,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Compares two template versions highlighting differences in content, variables, and metadata.
    /// Provides detailed analysis of changes and their potential impact.
    /// </summary>
    /// <param name="templateId">Unique template identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="fromVersion">Source version for comparison</param>
    /// <param name="toVersion">Target version for comparison</param>
    /// <param name="includeVariableAnalysis">Whether to analyze variable definition changes</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Detailed comparison results with change analysis</returns>
    /// <exception cref="ArgumentException">Thrown when templateId or versions are invalid</exception>
    /// <exception cref="NotFoundException">Thrown when template or versions don't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks comparison permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to both versions being compared</description></item>
    /// <item><description>Comparison results may be cached for performance optimization</description></item>
    /// <item><description>Variable analysis requires understanding of type compatibility</description></item>
    /// <item><description>Comparison includes impact assessment for existing executions</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements advanced diff algorithms for content comparison</description></item>
    /// <item><description>Provides semantic analysis of variable and content changes</description></item>
    /// <item><description>Highlights breaking changes and compatibility issues</description></item>
    /// <item><description>Supports side-by-side and unified diff views</description></item>
    /// </list>
    /// </remarks>
    Task<TemplateComparisonResult> CompareTemplateVersionsAsync(
        Guid templateId,
        string userId,
        string fromVersion,
        string toVersion,
        bool includeVariableAnalysis = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reverts template to a previous version creating a new version with restored content.
    /// Maintains complete audit trail while enabling safe rollback operations.
    /// </summary>
    /// <param name="templateId">Unique template identifier</param>
    /// <param name="userId">User identifier for authorization validation</param>
    /// <param name="targetVersion">Version to revert to</param>
    /// <param name="revertReason">Reason for reversion for audit purposes</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>New template version with reverted content</returns>
    /// <exception cref="ArgumentException">Thrown when templateId or targetVersion is invalid</exception>
    /// <exception cref="NotFoundException">Thrown when template or target version doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks revert permissions</exception>
    /// <exception cref="InvalidOperationException">Thrown when revert would cause compatibility issues</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have administrative edit permissions</description></item>
    /// <item><description>Revert reason required for audit trail and compliance</description></item>
    /// <item><description>Creates new version rather than modifying history</description></item>
    /// <item><description>Validates compatibility with existing executions and integrations</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Creates comprehensive audit log for revert operation</description></item>
    /// <item><description>Validates content and variable compatibility before revert</description></item>
    /// <item><description>Notifies stakeholders of revert operation and reasons</description></item>
    /// <item><description>Maintains referential integrity with dependent templates</description></item>
    /// </list>
    /// </remarks>
    Task<PromptTemplate> RevertTemplateVersionAsync(
        Guid templateId,
        string userId,
        string targetVersion,
        string revertReason,
        CancellationToken cancellationToken = default);

    #endregion

    #region Template Discovery and Recommendations

    /// <summary>
    /// Searches for templates across accessible libraries with advanced filtering and ranking.
    /// Implements intelligent discovery with relevance scoring and personalization.
    /// </summary>
    /// <param name="userId">User identifier for personalized and access-controlled results</param>
    /// <param name="searchTerm">Search query with support for natural language and keywords</param>
    /// <param name="categories">Optional filter by multiple categories</param>
    /// <param name="tags">Optional filter by tags with AND/OR logic</param>
    /// <param name="libraries">Optional scope to specific libraries</param>
    /// <param name="includePublic">Whether to include publicly accessible templates</param>
    /// <param name="sortBy">Sorting criteria with relevance scoring</param>
    /// <param name="pageNumber">Page number for pagination (1-based)</param>
    /// <param name="pageSize">Number of results per page (max 50)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Ranked search results with relevance scores and metadata</returns>
    /// <exception cref="ArgumentException">Thrown when search parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks search permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Results filtered by user access permissions and library visibility</description></item>
    /// <item><description>Search includes semantic matching and relevance scoring</description></item>
    /// <item><description>Public templates included based on organization policies</description></item>
    /// <item><description>Personalization based on user history and preferences</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements full-text search with natural language processing</description></item>
    /// <item><description>Uses machine learning for relevance ranking and personalization</description></item>
    /// <item><description>Supports faceted search with dynamic filtering options</description></item>
    /// <item><description>Provides search analytics and query optimization</description></item>
    /// </list>
    /// </remarks>
    Task<PagedResult<TemplateSearchResult>> SearchTemplatesAsync(
        string userId,
        string searchTerm,
        IEnumerable<TemplateCategory>? categories = null,
        IEnumerable<string>? tags = null,
        IEnumerable<Guid>? libraries = null,
        bool includePublic = true,
        TemplateSortOption sortBy = TemplateSortOption.Relevance,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Provides personalized template recommendations based on user activity and preferences.
    /// Implements machine learning algorithms for intelligent content discovery.
    /// </summary>
    /// <param name="userId">User identifier for personalized recommendations</param>
    /// <param name="context">Optional context for situational recommendations</param>
    /// <param name="maxRecommendations">Maximum number of recommendations to return</param>
    /// <param name="includeSimilar">Whether to include templates similar to recently used ones</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Personalized template recommendations with confidence scores</returns>
    /// <exception cref="ArgumentException">Thrown when userId is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks recommendation access</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Recommendations based on user activity, preferences, and team patterns</description></item>
    /// <item><description>Results filtered by access permissions and availability</description></item>
    /// <item><description>Context-aware recommendations for specific use cases</description></item>
    /// <item><description>Recommendations cached and refreshed based on activity patterns</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Uses machine learning models for preference prediction</description></item>
    /// <item><description>Implements collaborative filtering with privacy protection</description></item>
    /// <item><description>Provides explanation for recommendation reasoning</description></item>
    /// <item><description>Supports feedback collection for model improvement</description></item>
    /// </list>
    /// </remarks>
    Task<IEnumerable<TemplateRecommendation>> GetTemplateRecommendationsAsync(
        string userId,
        RecommendationContext? context = null,
        int maxRecommendations = 10,
        bool includeSimilar = true,
        CancellationToken cancellationToken = default);

    #endregion
}
