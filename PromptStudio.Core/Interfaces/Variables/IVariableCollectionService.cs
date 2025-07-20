using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Variables;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Execution;

namespace PromptStudio.Core.Interfaces.Variables;

/// <summary>
/// Enterprise-grade service interface for comprehensive variable collection management, including CSV processing, 
/// batch operations, data import/export, and advanced analytics for variable collection workflows within 
/// the PromptStudio platform ecosystem.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// Specialized service layer component responsible for variable collection lifecycle management, CSV data processing,
/// batch execution coordination, and advanced analytics. Implements enterprise patterns for multi-tenancy,
/// audit trails, data validation, and performance optimization for large-scale variable collection operations
/// and template execution workflows with comprehensive business rule enforcement.
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// Implements comprehensive variable collection management with strict data validation, CSV processing capabilities,
/// batch execution coordination, and advanced analytics. Supports collection CRUD operations, data import/export,
/// batch processing, and analytics with full audit capabilities, tenant isolation, and performance optimization
/// for high-volume collection operations and data processing scenarios.
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
///   <item>All operations must enforce tenant isolation and data security boundaries</item>
///   <item>CSV processing must include comprehensive data validation and error reporting</item>
///   <item>Implement streaming for large collection operations to optimize memory usage</item>
///   <item>Support transactional batch operations with rollback capabilities</item>
///   <item>Implement comprehensive audit logging for all collection operations</item>
///   <item>Optimize for high-throughput batch processing and analytics operations</item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
///   <item>Integrates with IVariableManagementService for variable validation and processing</item>
///   <item>Coordinates with execution services for batch processing and analytics</item>
///   <item>Works with audit services for comprehensive change tracking and compliance</item>
///   <item>Supports notification services for collection processing status updates</item>
///   <item>Implements Unit of Work pattern for transactional collection operations</item>
///   <item>Uses repository pattern with optimized data access for large collections</item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
///   <item>Mock CSV processing for unit testing collection operations</item>
///   <item>Test large collection handling and memory management thoroughly</item>
///   <item>Verify tenant isolation in multi-tenant collection scenarios</item>
///   <item>Performance test batch operations under high data volume</item>
///   <item>Test collection data integrity and validation error handling</item>
/// </list>
/// </remarks>
public interface IVariableCollectionService
{
    #region Collection CRUD Operations

    /// <summary>
    /// Creates a new variable collection with comprehensive validation and metadata management.
    /// Enforces business rules for collection naming, data validation, and template integration requirements
    /// with full audit trail and tenant isolation capabilities.
    /// </summary>
    /// <param name="createRequest">Collection creation request with validation details</param>
    /// <param name="tenantId">Tenant identifier for multi-tenant isolation</param>
    /// <param name="createdBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Created collection entity with generated metadata and validation results</returns>
    /// <exception cref="ArgumentException">Thrown when validation rules are violated</exception>
    /// <exception cref="InvalidOperationException">Thrown when business rules prevent creation</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Collection names must be unique within template scope</item>
    ///   <item>Variable data must conform to template variable definitions</item>
    ///   <item>CSV data must pass comprehensive validation before storage</item>
    ///   <item>Collection metadata must include creation and modification tracking</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates CSV data structure and content before collection creation</item>
    ///   <item>Creates comprehensive audit trail for collection creation</item>
    ///   <item>Implements data validation with detailed error reporting</item>
    ///   <item>Triggers notifications for collection creation and validation results</item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// var createRequest = new CreateVariableCollectionRequest
    /// {
    ///     Name = "Customer Test Data",
    ///     TemplateId = templateId,
    ///     CsvData = csvContent,
    ///     Description = "Customer data for template testing"
    /// };
    /// var collection = await service.CreateCollectionAsync(createRequest, tenantId, userId);
    /// </code>
    /// </remarks>
    Task<VariableCollection> CreateCollectionAsync(
        CreateVariableCollectionRequest createRequest, 
        Guid tenantId, 
        Guid createdBy, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a variable collection by its unique identifier with comprehensive metadata and statistics.
    /// Includes collection definition, data summary, validation status, and usage analytics for
    /// collection management and optimization workflows.
    /// </summary>
    /// <param name="collectionId">Unique collection identifier</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="includeData">Whether to include full variable data in response</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Collection entity with metadata and optional data, or null if not found</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Only collections within tenant scope are accessible</item>
    ///   <item>Soft-deleted collections are excluded from results</item>
    ///   <item>Data inclusion is controlled by performance considerations</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements caching for frequently accessed collections</item>
    ///   <item>Supports lazy loading for large collection data sets</item>
    ///   <item>Includes collection statistics and validation summaries</item>
    ///   <item>Logs access for usage analytics and optimization</item>
    /// </list>
    /// </remarks>
    Task<VariableCollection?> GetCollectionByIdAsync(
        Guid collectionId, 
        Guid tenantId, 
        bool includeData = false, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves variable collections associated with a specific template with filtering and pagination.
    /// Enables efficient collection discovery and management within template development and testing workflows
    /// with comprehensive filtering and sorting capabilities.
    /// </summary>
    /// <param name="templateId">Template identifier for collection association</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="filters">Optional filtering criteria for collection selection</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated list of collections with metadata and statistics</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Collections are filtered by tenant and template association</item>
    ///   <item>Results include only active (non-deleted) collections</item>
    ///   <item>Pagination limits are enforced for performance optimization</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Optimized queries for large template collection sets</item>
    ///   <item>Supports advanced filtering by status, size, and validation results</item>
    ///   <item>Includes collection usage statistics and health metrics</item>
    ///   <item>Implements efficient pagination with cursor-based navigation</item>
    /// </list>
    /// </remarks>
    Task<PagedResult<VariableCollection>> GetCollectionsByTemplateAsync(
        Guid templateId, 
        Guid tenantId, 
        CollectionFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing variable collection with comprehensive validation and change tracking.
    /// Maintains collection integrity and validates data changes against template requirements
    /// with full audit trail and impact analysis capabilities.
    /// </summary>
    /// <param name="collectionId">Collection identifier to update</param>
    /// <param name="updateRequest">Update request with modified collection data</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="updatedBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Updated collection entity with validation results and change summary</returns>
    /// <exception cref="NotFoundException">Thrown when collection is not found</exception>
    /// <exception cref="ArgumentException">Thrown when validation rules are violated</exception>
    /// <exception cref="InvalidOperationException">Thrown when update conflicts with business rules</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Collection data changes must maintain template compatibility</item>
    ///   <item>Name changes must maintain uniqueness within template scope</item>
    ///   <item>Data updates must pass comprehensive validation</item>
    ///   <item>Changes affecting active executions require confirmation</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates data changes against template requirements</item>
    ///   <item>Creates comprehensive audit trail for change tracking</item>
    ///   <item>Implements transactional updates with rollback capabilities</item>
    ///   <item>Triggers cache invalidation and dependent system notifications</item>
    /// </list>
    /// </remarks>
    Task<CollectionUpdateResult> UpdateCollectionAsync(
        Guid collectionId, 
        UpdateVariableCollectionRequest updateRequest, 
        Guid tenantId, 
        Guid updatedBy, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs soft delete of a variable collection with dependency validation and cleanup procedures.
    /// Ensures data integrity and validates impact on dependent executions and analytics with
    /// comprehensive audit trail and dependency analysis capabilities.
    /// </summary>
    /// <param name="collectionId">Collection identifier to delete</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="deletedBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Operation result with deletion status and impact analysis</returns>
    /// <exception cref="NotFoundException">Thrown when collection is not found</exception>
    /// <exception cref="InvalidOperationException">Thrown when dependencies prevent deletion</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Collections with active execution dependencies cannot be hard deleted</item>
    ///   <item>Soft delete preserves data integrity and execution history</item>
    ///   <item>Deletion impact analysis includes execution and analytics dependencies</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Performs comprehensive dependency analysis before deletion</item>
    ///   <item>Implements soft delete with audit trail preservation</item>
    ///   <item>Triggers cleanup of associated cache entries and temporary data</item>
    ///   <item>Notifies dependent systems of collection removal</item>
    /// </list>
    /// </remarks>
    Task<OperationResult> DeleteCollectionAsync(
        Guid collectionId, 
        Guid tenantId, 
        Guid deletedBy, 
        CancellationToken cancellationToken = default);

    #endregion

    #region CSV Processing & Data Import

    /// <summary>
    /// Generates a CSV template for variable collection creation with comprehensive field definitions.
    /// Creates standardized CSV templates based on template variable definitions with data type information,
    /// validation rules, and example data for collection development workflows.
    /// </summary>
    /// <param name="templateId">Template identifier for CSV template generation</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="options">Optional CSV generation options for customization</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>CSV template content with headers, data types, and example data</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Template Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Includes headers for all template variables</item>
    ///   <item>Provides data type information and validation rules</item>
    ///   <item>Includes example data for user guidance</item>
    ///   <item>Supports customizable template formatting options</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Generates templates based on current template variable definitions</item>
    ///   <item>Includes comprehensive metadata and validation information</item>
    ///   <item>Supports multiple CSV formats and encoding options</item>
    ///   <item>Implements caching for frequently requested templates</item>
    /// </list>
    /// </remarks>
    Task<CsvTemplateResult> GenerateCsvTemplateAsync(
        Guid templateId, 
        Guid tenantId, 
        CsvGenerationOptions? options = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Parses CSV data with comprehensive validation and error reporting capabilities.
    /// Processes CSV content for variable collection creation with advanced validation,
    /// data type conversion, and detailed error reporting for data quality assurance.
    /// </summary>
    /// <param name="csvContent">CSV content to parse and validate</param>
    /// <param name="templateId">Template identifier for validation context</param>
    /// <param name="tenantId">Tenant identifier for tenant-specific validation rules</param>
    /// <param name="options">Optional parsing configuration and validation settings</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive parsing result with validation details and error information</returns>
    /// <exception cref="ArgumentException">Thrown when CSV content is invalid or malformed</exception>
    /// <exception cref="NotFoundException">Thrown when template is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Parsing Capabilities:</strong></para>
    /// <list type="bullet">
    ///   <item>Comprehensive CSV format validation and data type conversion</item>
    ///   <item>Variable-specific validation against template requirements</item>
    ///   <item>Detailed error reporting with row and column information</item>
    ///   <item>Data quality analysis and improvement recommendations</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Supports multiple CSV formats and encoding detection</item>
    ///   <item>Implements streaming for large CSV file processing</item>
    ///   <item>Provides comprehensive validation with actionable error messages</item>
    ///   <item>Includes data statistics and quality metrics</item>
    /// </list>
    /// </remarks>
    Task<CsvParsingResult> ParseCsvDataAsync(
        string csvContent, 
        Guid templateId, 
        Guid tenantId, 
        CsvParsingOptions? options = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Imports variable collection data from multiple sources with validation and processing options.
    /// Supports import from CSV, JSON, and other structured data formats with comprehensive validation,
    /// data transformation, and error handling for collection integration workflows.
    /// </summary>
    /// <param name="importRequest">Import request with data source and processing options</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="importedBy">User identifier for audit and tracking</param>
    /// <param name="progress">Optional progress reporter for long-running import operations</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Import result with processing statistics and validation details</returns>
    /// <exception cref="ArgumentException">Thrown when import data is invalid</exception>
    /// <exception cref="InvalidOperationException">Thrown when import conflicts with business rules</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Import Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Supports multiple data formats (CSV, JSON, Excel)</item>
    ///   <item>Comprehensive data validation and transformation</item>
    ///   <item>Progress tracking for large import operations</item>
    ///   <item>Rollback capabilities for failed imports</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements transactional import with comprehensive error handling</item>
    ///   <item>Supports streaming for large data sets</item>
    ///   <item>Provides detailed import analytics and validation results</item>
    ///   <item>Creates comprehensive audit trail for import operations</item>
    /// </list>
    /// </remarks>
    Task<VariableImportResult> ImportCollectionDataAsync(
        ImportCollectionRequest importRequest, 
        Guid tenantId, 
        Guid importedBy, 
        IProgress<ImportProgress>? progress = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Batch Execution & Processing

    /// <summary>
    /// Executes batch operations on variable collections with comprehensive progress tracking and analytics.
    /// Processes multiple variable sets through template execution workflows with advanced monitoring,
    /// error handling, and performance optimization for large-scale collection processing.
    /// </summary>
    /// <param name="collectionId">Collection identifier for batch execution</param>
    /// <param name="templateId">Template identifier for execution context</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="executedBy">User identifier for audit and tracking</param>
    /// <param name="options">Optional execution configuration and processing settings</param>
    /// <param name="progress">Progress reporter for real-time execution monitoring</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive batch execution result with statistics and individual results</returns>
    /// <exception cref="NotFoundException">Thrown when collection or template is not found</exception>
    /// <exception cref="InvalidOperationException">Thrown when execution conditions are not met</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Execution Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Parallel processing with configurable concurrency limits</item>
    ///   <item>Real-time progress reporting and execution monitoring</item>
    ///   <item>Comprehensive error handling and retry mechanisms</item>
    ///   <item>Detailed execution analytics and performance metrics</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements optimized batch processing with resource management</item>
    ///   <item>Supports cancellation and graceful shutdown of long-running operations</item>
    ///   <item>Provides comprehensive execution audit trail and analytics</item>
    ///   <item>Creates detailed performance metrics for optimization insights</item>
    /// </list>
    /// </remarks>
    Task<BatchExecutionResult> ExecuteBatchAsync(
        Guid collectionId, 
        Guid templateId, 
        Guid tenantId, 
        Guid executedBy, 
        BatchExecutionOptions? options = null, 
        IProgress<BatchExecutionProgress>? progress = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves execution history for variable collections with filtering and analytics capabilities.
    /// Provides comprehensive execution history with performance metrics, success rates, and
    /// trend analysis for collection optimization and operational monitoring workflows.
    /// </summary>
    /// <param name="collectionId">Collection identifier for execution history</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="filters">Optional filtering criteria for execution history</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated execution history with comprehensive metrics and analytics</returns>
    /// <exception cref="NotFoundException">Thrown when collection is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>History Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Comprehensive execution tracking with detailed metrics</item>
    ///   <item>Performance trend analysis and optimization insights</item>
    ///   <item>Success rate tracking and error pattern analysis</item>
    ///   <item>Filtering and search capabilities for operational analysis</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Optimized queries for large execution history datasets</item>
    ///   <item>Implements aggregation for performance metrics and analytics</item>
    ///   <item>Supports exportable execution data for reporting</item>
    ///   <item>Provides historical trend analysis and forecasting capabilities</item>
    /// </list>
    /// </remarks>
    Task<PagedResult<CollectionExecutionHistory>> GetExecutionHistoryAsync(
        Guid collectionId, 
        Guid tenantId, 
        ExecutionHistoryFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Collection Analytics & Optimization

    /// <summary>
    /// Retrieves comprehensive analytics for variable collections with performance and usage insights.
    /// Provides detailed collection analytics including usage patterns, performance metrics, data quality
    /// analysis, and optimization recommendations for collection management and improvement workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="filters">Optional filtering criteria for analytics data</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive collection analytics with performance and optimization insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Analytics Capabilities:</strong></para>
    /// <list type="bullet">
    ///   <item>Collection usage patterns and performance trends</item>
    ///   <item>Data quality analysis and improvement recommendations</item>
    ///   <item>Execution success rates and error pattern analysis</item>
    ///   <item>Resource utilization and cost optimization insights</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Aggregates data from multiple sources for comprehensive insights</item>
    ///   <item>Implements real-time analytics with historical trend analysis</item>
    ///   <item>Supports exportable analytics data for executive reporting</item>
    ///   <item>Provides actionable optimization recommendations</item>
    /// </list>
    /// </remarks>
    Task<CollectionAnalyticsResult> GetCollectionAnalyticsAsync(
        Guid tenantId, 
        CollectionAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates optimization recommendations for variable collections based on usage and performance analysis.
    /// Analyzes collection performance, data quality, and usage patterns to provide actionable optimization
    /// recommendations for improved efficiency, data quality, and operational effectiveness.
    /// </summary>
    /// <param name="collectionId">Collection identifier for optimization analysis</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive optimization recommendations with implementation guidance</returns>
    /// <exception cref="NotFoundException">Thrown when collection is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Optimization Analysis:</strong></para>
    /// <list type="bullet">
    ///   <item>Performance bottleneck identification and resolution strategies</item>
    ///   <item>Data quality improvement recommendations</item>
    ///   <item>Usage pattern optimization and efficiency improvements</item>
    ///   <item>Cost optimization and resource utilization recommendations</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Uses machine learning for pattern recognition and optimization insights</item>
    ///   <item>Provides prioritized recommendations with impact analysis</item>
    ///   <item>Includes implementation guidance and best practice recommendations</item>
    ///   <item>Supports continuous optimization monitoring and tracking</item>
    /// </list>
    /// </remarks>
    Task<CollectionOptimizationResult> GetOptimizationRecommendationsAsync(
        Guid collectionId, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    #endregion
}
