using PromptStudio.Core.Domain.PromptEntities;
using PromptStudio.Core.DTOs.Variables;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.Interfaces.Execution;

/// <summary>
/// Enterprise-grade service interface for comprehensive prompt template execution management, including 
/// single and batch execution, real-time monitoring, performance optimization, and advanced analytics 
/// within the PromptStudio execution ecosystem.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// Core execution service layer component responsible for orchestrating prompt template execution workflows,
/// variable resolution, AI model integration, and comprehensive execution tracking. Implements enterprise
/// patterns for multi-tenancy, audit trails, performance optimization, and scalable execution management
/// with real-time monitoring and advanced analytics capabilities for production LLMOps workflows.
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// Implements comprehensive execution management with strict performance requirements, real-time monitoring,
/// error handling, and scalability optimization. Supports single and batch execution, AI model integration,
/// execution analytics, and operational monitoring with full audit capabilities, tenant isolation, and
/// performance optimization for high-throughput execution scenarios and enterprise workflows.
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
///   <item>All operations must enforce tenant isolation and execution security boundaries</item>
///   <item>Execution must include comprehensive variable validation and error handling</item>
///   <item>Implement circuit breaker patterns for AI model provider resilience</item>
///   <item>Support execution caching and optimization for frequently used templates</item>
///   <item>Implement comprehensive audit logging for all execution operations</item>
///   <item>Optimize for high-throughput batch execution and concurrent processing</item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
///   <item>Integrates with IVariableManagementService for variable resolution and validation</item>
///   <item>Coordinates with AI model providers for prompt execution and response handling</item>
///   <item>Works with audit services for comprehensive execution tracking and compliance</item>
///   <item>Supports notification services for execution status and completion alerts</item>
///   <item>Implements Unit of Work pattern for transactional execution operations</item>
///   <item>Uses caching services for execution optimization and performance enhancement</item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
///   <item>Mock AI model providers for unit testing execution workflows</item>
///   <item>Test execution resilience and error handling thoroughly</item>
///   <item>Verify tenant isolation in multi-tenant execution scenarios</item>
///   <item>Performance test batch execution under high concurrency and data volume</item>
///   <item>Test execution caching and optimization effectiveness</item>
/// </list>
/// </remarks>
public interface IPromptExecutionService
{
    #region Single Execution Operations

    /// <summary>
    /// Executes a single prompt template with comprehensive variable resolution and AI model integration.
    /// Performs template variable resolution, AI model execution, and comprehensive result tracking
    /// with full audit trail and performance monitoring capabilities.
    /// </summary>
    /// <param name="templateId">Template identifier for execution</param>
    /// <param name="variableValues">Dictionary of variable names and values for template resolution</param>
    /// <param name="tenantId">Tenant identifier for access control and isolation</param>
    /// <param name="executedBy">User identifier for audit and tracking</param>
    /// <param name="options">Optional execution configuration and AI model settings</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive execution result with resolved content and performance metrics</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found or not accessible</exception>
    /// <exception cref="ArgumentException">Thrown when variable validation fails</exception>
    /// <exception cref="InvalidOperationException">Thrown when execution conditions are not met</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>All required template variables must have valid values</item>
    ///   <item>Variable values must conform to template type constraints</item>
    ///   <item>Execution requires valid AI model provider configuration</item>
    ///   <item>Template must be in active status for execution</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements variable resolution with comprehensive validation</item>
    ///   <item>Supports multiple AI model providers with failover capabilities</item>
    ///   <item>Creates detailed execution audit trail and performance metrics</item>
    ///   <item>Implements execution caching for performance optimization</item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// var variables = new Dictionary&lt;string, string&gt;
    /// {
    ///     ["customerName"] = "John Doe",
    ///     ["productCategory"] = "Electronics"
    /// };
    /// var result = await service.ExecuteTemplateAsync(templateId, variables, tenantId, userId);
    /// if (result.Success) {
    ///     await ProcessResultAsync(result.ResolvedContent);
    /// }
    /// </code>
    /// </remarks>
    Task<ExecutionResult> ExecuteTemplateAsync(
        Guid templateId, 
        Dictionary<string, string> variableValues, 
        Guid tenantId, 
        Guid executedBy, 
        ExecutionOptions? options = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a prompt template with JSON-formatted variables for API integration scenarios.
    /// Supports JSON variable input format for seamless API integration with comprehensive validation,
    /// execution tracking, and performance monitoring for automated execution workflows.
    /// </summary>
    /// <param name="templateId">Template identifier for execution</param>
    /// <param name="variablesJson">JSON-formatted variable values for template resolution</param>
    /// <param name="tenantId">Tenant identifier for access control and isolation</param>
    /// <param name="executedBy">User identifier for audit and tracking</param>
    /// <param name="options">Optional execution configuration and AI model settings</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive execution result with resolved content and performance metrics</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found or not accessible</exception>
    /// <exception cref="ArgumentException">Thrown when JSON parsing or variable validation fails</exception>
    /// <exception cref="InvalidOperationException">Thrown when execution conditions are not met</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>JSON Format Requirements:</strong></para>
    /// <list type="bullet">
    ///   <item>JSON must contain valid key-value pairs for template variables</item>
    ///   <item>Variable names must match template variable definitions exactly</item>
    ///   <item>Values must be convertible to appropriate data types</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements robust JSON parsing with detailed error reporting</item>
    ///   <item>Validates parsed variables against template requirements</item>
    ///   <item>Supports complex variable types and nested data structures</item>
    ///   <item>Maintains execution audit trail with JSON variable history</item>
    /// </list>
    /// </remarks>
    Task<ExecutionResult> ExecuteTemplateAsync(
        Guid templateId, 
        string variablesJson, 
        Guid tenantId, 
        Guid executedBy, 
        ExecutionOptions? options = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates template execution prerequisites without performing actual execution.
    /// Performs comprehensive validation of template status, variable requirements, AI model configuration,
    /// and execution permissions to ensure successful execution before resource commitment.
    /// </summary>
    /// <param name="templateId">Template identifier for validation</param>
    /// <param name="variableValues">Variable values to validate for execution</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="options">Optional execution configuration for validation</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive validation result with detailed error and warning information</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Validation Checks:</strong></para>
    /// <list type="bullet">
    ///   <item>Template availability and active status validation</item>
    ///   <item>Variable completeness and type constraint validation</item>
    ///   <item>AI model provider availability and configuration validation</item>
    ///   <item>Execution permissions and quota availability validation</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Performs dry-run validation without resource consumption</item>
    ///   <item>Provides detailed validation results with actionable feedback</item>
    ///   <item>Includes execution cost estimation and resource requirements</item>
    ///   <item>Supports validation caching for frequently validated templates</item>
    /// </list>
    /// </remarks>
    Task<ExecutionValidationResult> ValidateExecutionAsync(
        Guid templateId, 
        Dictionary<string, string> variableValues, 
        Guid tenantId, 
        ExecutionOptions? options = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Batch Execution Operations

    /// <summary>
    /// Executes batch operations using variable collections with comprehensive progress tracking and analytics.
    /// Processes multiple variable sets through template execution workflows with advanced monitoring,
    /// parallel processing, error handling, and performance optimization for large-scale execution scenarios.
    /// </summary>
    /// <param name="templateId">Template identifier for batch execution</param>
    /// <param name="collectionId">Variable collection identifier containing execution data</param>
    /// <param name="tenantId">Tenant identifier for access control and isolation</param>
    /// <param name="executedBy">User identifier for audit and tracking</param>
    /// <param name="options">Optional batch execution configuration and processing settings</param>
    /// <param name="progress">Progress reporter for real-time execution monitoring</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive batch execution result with statistics and individual results</returns>
    /// <exception cref="NotFoundException">Thrown when template or collection is not found</exception>
    /// <exception cref="InvalidOperationException">Thrown when batch execution conditions are not met</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Batch Processing Features:</strong></para>
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
        Guid templateId, 
        Guid collectionId, 
        Guid tenantId, 
        Guid executedBy, 
        BatchExecutionOptions? options = null, 
        IProgress<BatchExecutionProgress>? progress = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes batch operations with explicit variable sets for flexible batch processing scenarios.
    /// Processes provided variable sets through template execution workflows with advanced monitoring,
    /// parallel processing, and comprehensive result aggregation for custom batch execution workflows.
    /// </summary>
    /// <param name="templateId">Template identifier for batch execution</param>
    /// <param name="variableSets">Collection of variable sets for execution</param>
    /// <param name="tenantId">Tenant identifier for access control and isolation</param>
    /// <param name="executedBy">User identifier for audit and tracking</param>
    /// <param name="options">Optional batch execution configuration and processing settings</param>
    /// <param name="progress">Progress reporter for real-time execution monitoring</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive batch execution result with individual execution details</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found</exception>
    /// <exception cref="ArgumentException">Thrown when variable sets validation fails</exception>
    /// <exception cref="InvalidOperationException">Thrown when batch execution conditions are not met</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Flexible Batch Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Supports dynamic variable sets without collection storage</item>
    ///   <item>Configurable parallel processing and resource management</item>
    ///   <item>Individual execution result tracking and aggregation</item>
    ///   <item>Comprehensive error handling with detailed failure analysis</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates all variable sets before execution commencement</item>
    ///   <item>Implements memory-efficient processing for large batch operations</item>
    ///   <item>Provides detailed execution metrics and performance analysis</item>
    ///   <item>Supports execution result streaming for memory optimization</item>
    /// </list>
    /// </remarks>
    Task<BatchExecutionResult> ExecuteBatchAsync(
        Guid templateId, 
        IEnumerable<Dictionary<string, string>> variableSets, 
        Guid tenantId, 
        Guid executedBy, 
        BatchExecutionOptions? options = null, 
        IProgress<BatchExecutionProgress>? progress = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Monitors active batch execution operations with real-time status and progress tracking.
    /// Provides comprehensive monitoring capabilities for ongoing batch execution operations
    /// with detailed status information, progress metrics, and operational control capabilities.
    /// </summary>
    /// <param name="executionId">Batch execution identifier for monitoring</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Real-time execution status with progress metrics and operational details</returns>
    /// <exception cref="NotFoundException">Thrown when execution is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Monitoring Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Real-time execution status and progress tracking</item>
    ///   <item>Performance metrics and resource utilization monitoring</item>
    ///   <item>Error tracking and failure analysis capabilities</item>
    ///   <item>Execution control and cancellation capabilities</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Provides real-time status updates with minimal latency</item>
    ///   <item>Implements efficient status querying with caching optimization</item>
    ///   <item>Supports detailed execution analytics and reporting</item>
    ///   <item>Includes execution health monitoring and alerting capabilities</item>
    /// </list>
    /// </remarks>
    Task<BatchExecutionStatus> GetBatchExecutionStatusAsync(
        Guid executionId, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Execution History & Analytics

    /// <summary>
    /// Retrieves comprehensive execution history with filtering and analytics capabilities.
    /// Provides detailed execution history with performance metrics, success rates, and trend analysis
    /// for template optimization, operational monitoring, and business intelligence workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for history scope and access control</param>
    /// <param name="filters">Optional filtering criteria for execution history selection</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated execution history with comprehensive metrics and analytics</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>History Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Comprehensive execution tracking with detailed metrics</item>
    ///   <item>Performance trend analysis and optimization insights</item>
    ///   <item>Success rate tracking and error pattern analysis</item>
    ///   <item>Advanced filtering and search capabilities for operational analysis</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Optimized queries for large execution history datasets</item>
    ///   <item>Implements aggregation for performance metrics and analytics</item>
    ///   <item>Supports exportable execution data for reporting and compliance</item>
    ///   <item>Provides historical trend analysis and forecasting capabilities</item>
    /// </list>
    /// </remarks>
    Task<PagedResult<ExecutionHistoryEntry>> GetExecutionHistoryAsync(
        Guid tenantId, 
        ExecutionHistoryFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves execution history for a specific template with detailed performance analytics.
    /// Provides template-specific execution history with comprehensive performance metrics,
    /// usage patterns, and optimization insights for template development and maintenance workflows.
    /// </summary>
    /// <param name="templateId">Template identifier for execution history</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="filters">Optional filtering criteria for execution history</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated template execution history with performance metrics</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Template Analytics:</strong></para>
    /// <list type="bullet">
    ///   <item>Template-specific performance metrics and usage patterns</item>
    ///   <item>Variable usage analysis and optimization recommendations</item>
    ///   <item>Execution success rates and error pattern analysis</item>
    ///   <item>Performance benchmarking and trend analysis</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Provides template-optimized analytics and performance insights</item>
    ///   <item>Implements caching for frequently accessed template metrics</item>
    ///   <item>Supports detailed template performance reporting</item>
    ///   <item>Includes template optimization recommendations and best practices</item>
    /// </list>
    /// </remarks>
    Task<PagedResult<ExecutionHistoryEntry>> GetTemplateExecutionHistoryAsync(
        Guid templateId, 
        Guid tenantId, 
        ExecutionHistoryFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves comprehensive execution analytics with performance and optimization insights.
    /// Provides detailed execution analytics including usage patterns, performance metrics, cost analysis,
    /// and optimization recommendations for operational efficiency and business intelligence workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="filters">Optional filtering criteria for analytics data</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive execution analytics with performance and optimization insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Analytics Capabilities:</strong></para>
    /// <list type="bullet">
    ///   <item>Execution usage patterns and performance trends</item>
    ///   <item>Cost analysis and resource utilization optimization</item>
    ///   <item>Success rate analysis and error pattern identification</item>
    ///   <item>Template performance comparison and benchmarking</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Aggregates data from multiple sources for comprehensive insights</item>
    ///   <item>Implements real-time analytics with historical trend analysis</item>
    ///   <item>Supports exportable analytics data for executive reporting</item>
    ///   <item>Provides actionable optimization recommendations and cost insights</item>
    /// </list>
    /// </remarks>
    Task<ExecutionAnalyticsResult> GetExecutionAnalyticsAsync(
        Guid tenantId, 
        ExecutionAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Execution Management & Control

    /// <summary>
    /// Cancels an active execution operation with graceful shutdown and cleanup procedures.
    /// Provides controlled cancellation of ongoing execution operations with proper resource cleanup,
    /// state preservation, and audit trail maintenance for operational control and resource management.
    /// </summary>
    /// <param name="executionId">Execution identifier for cancellation</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancelledBy">User identifier for audit and tracking</param>
    /// <param name="reason">Optional cancellation reason for audit trail</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Cancellation result with status and cleanup information</returns>
    /// <exception cref="NotFoundException">Thrown when execution is not found</exception>
    /// <exception cref="InvalidOperationException">Thrown when execution cannot be cancelled</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Cancellation Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Graceful shutdown with proper resource cleanup</item>
    ///   <item>State preservation for partial execution results</item>
    ///   <item>Comprehensive audit trail for cancellation tracking</item>
    ///   <item>Notification integration for stakeholder alerts</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements graceful cancellation with minimal data loss</item>
    ///   <item>Preserves partial execution results for analysis</item>
    ///   <item>Creates detailed cancellation audit trail</item>
    ///   <item>Triggers appropriate cleanup and notification workflows</item>
    /// </list>
    /// </remarks>
    Task<CancellationResult> CancelExecutionAsync(
        Guid executionId, 
        Guid tenantId, 
        Guid cancelledBy, 
        string? reason = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retries a failed execution with optional configuration adjustments and enhanced error handling.
    /// Provides intelligent retry capabilities for failed executions with configuration optimization,
    /// error analysis, and improved execution parameters for successful completion.
    /// </summary>
    /// <param name="executionId">Original execution identifier for retry</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="retriedBy">User identifier for audit and tracking</param>
    /// <param name="retryOptions">Optional retry configuration and parameter adjustments</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Retry execution result with performance and success metrics</returns>
    /// <exception cref="NotFoundException">Thrown when original execution is not found</exception>
    /// <exception cref="InvalidOperationException">Thrown when execution cannot be retried</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Retry Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Intelligent retry with error analysis and parameter optimization</item>
    ///   <item>Enhanced error handling and failure prevention strategies</item>
    ///   <item>Configuration adjustment based on previous failure analysis</item>
    ///   <item>Comprehensive retry audit trail and success tracking</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Analyzes previous failure causes for retry optimization</item>
    ///   <item>Implements intelligent parameter adjustment and error prevention</item>
    ///   <item>Creates detailed retry audit trail with failure analysis</item>
    ///   <item>Provides retry success metrics and improvement recommendations</item>
    /// </list>
    /// </remarks>
    Task<ExecutionResult> RetryExecutionAsync(
        Guid executionId, 
        Guid tenantId, 
        Guid retriedBy, 
        RetryExecutionOptions? retryOptions = null, 
        CancellationToken cancellationToken = default);

    #endregion
}
