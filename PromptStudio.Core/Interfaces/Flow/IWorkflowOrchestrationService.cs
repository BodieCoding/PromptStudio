using PromptStudio.Core.Domain.FlowEntities;
using PromptStudio.Core.DTOs.Flow;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces.Flow;

/// <summary>
/// Enterprise-grade service interface for comprehensive workflow orchestration and execution management, including
/// workflow definition, validation, execution coordination, and advanced analytics within the PromptStudio 
/// workflow automation ecosystem.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// Advanced orchestration service layer component responsible for workflow lifecycle management, execution coordination,
/// node orchestration, and comprehensive workflow analytics. Implements enterprise patterns for multi-tenancy,
/// audit trails, distributed execution, and scalable workflow management with real-time monitoring and advanced
/// analytics capabilities for production workflow automation and LLMOps orchestration workflows.
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// Implements comprehensive workflow orchestration with strict performance requirements, real-time monitoring,
/// distributed execution capabilities, and advanced analytics. Supports workflow CRUD operations, execution coordination,
/// node management, and workflow analytics with full audit capabilities, tenant isolation, and performance optimization
/// for high-throughput workflow execution scenarios and enterprise automation workflows.
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
///   <item>All operations must enforce tenant isolation and workflow security boundaries</item>
///   <item>Workflow execution must support distributed processing and fault tolerance</item>
///   <item>Implement comprehensive workflow validation and dependency management</item>
///   <item>Support workflow versioning and rollback capabilities for production safety</item>
///   <item>Implement comprehensive audit logging for all workflow operations</item>
///   <item>Optimize for high-throughput workflow execution and concurrent processing</item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
///   <item>Integrates with IPromptExecutionService for template execution coordination</item>
///   <item>Coordinates with IVariableManagementService for workflow variable resolution</item>
///   <item>Works with audit services for comprehensive workflow tracking and compliance</item>
///   <item>Supports notification services for workflow status and completion alerts</item>
///   <item>Implements Unit of Work pattern for transactional workflow operations</item>
///   <item>Uses distributed caching for workflow execution optimization</item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
///   <item>Mock workflow nodes and execution dependencies for unit testing</item>
///   <item>Test workflow resilience and error handling thoroughly</item>
///   <item>Verify tenant isolation in multi-tenant workflow scenarios</item>
///   <item>Performance test workflow execution under high concurrency</item>
///   <item>Test workflow versioning and rollback capabilities</item>
/// </list>
/// </remarks>
public interface IWorkflowOrchestrationService
{
    #region Workflow CRUD Operations

    /// <summary>
    /// Creates a new workflow with comprehensive validation and metadata management.
    /// Enforces business rules for workflow definition, node validation, and dependency management
    /// with full audit trail and tenant isolation capabilities.
    /// </summary>
    /// <param name="createRequest">Workflow creation request with definition and validation details</param>
    /// <param name="tenantId">Tenant identifier for multi-tenant isolation</param>
    /// <param name="createdBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Created workflow entity with generated metadata and validation results</returns>
    /// <exception cref="ArgumentException">Thrown when validation rules are violated</exception>
    /// <exception cref="InvalidOperationException">Thrown when business rules prevent creation</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Workflow names must be unique within tenant and category scope</item>
    ///   <item>Workflow definition must pass comprehensive validation</item>
    ///   <item>Node dependencies must form valid directed acyclic graph (DAG)</item>
    ///   <item>All referenced templates and variables must be accessible</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates workflow definition and node dependencies comprehensively</item>
    ///   <item>Creates comprehensive audit trail for workflow creation</item>
    ///   <item>Implements workflow versioning with metadata tracking</item>
    ///   <item>Triggers notifications for workflow creation and validation results</item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// var createRequest = new CreateWorkflowRequest
    /// {
    ///     Name = "Customer Onboarding Flow",
    ///     Description = "Automated customer onboarding workflow",
    ///     Category = WorkflowCategory.CustomerEngagement,
    ///     Nodes = nodeDefinitions,
    ///     Edges = edgeDefinitions
    /// };
    /// var workflow = await service.CreateWorkflowAsync(createRequest, tenantId, userId);
    /// </code>
    /// </remarks>
    Task<PromptFlow> CreateWorkflowAsync(
        CreateWorkflowRequest createRequest, 
        Guid tenantId, 
        Guid createdBy, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a workflow by its unique identifier with comprehensive metadata and execution statistics.
    /// Includes workflow definition, node configuration, execution history summary, and performance metrics
    /// for workflow management and optimization workflows.
    /// </summary>
    /// <param name="workflowId">Unique workflow identifier</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="includeDefinition">Whether to include full workflow definition in response</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Workflow entity with metadata and optional definition, or null if not found</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Only workflows within tenant scope are accessible</item>
    ///   <item>Soft-deleted workflows are excluded from results</item>
    ///   <item>Definition inclusion is controlled by performance considerations</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements caching for frequently accessed workflows</item>
    ///   <item>Supports lazy loading for large workflow definitions</item>
    ///   <item>Includes workflow execution statistics and health metrics</item>
    ///   <item>Logs access for usage analytics and optimization</item>
    /// </list>
    /// </remarks>
    Task<PromptFlow?> GetWorkflowByIdAsync(
        Guid workflowId, 
        Guid tenantId, 
        bool includeDefinition = false, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves workflows within a tenant scope with filtering and pagination capabilities.
    /// Enables efficient workflow discovery and management with comprehensive filtering,
    /// sorting, and search capabilities for workflow development and operational workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for workflow scope</param>
    /// <param name="filters">Optional filtering criteria for workflow selection</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated list of workflows with metadata and statistics</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Workflows are filtered by tenant scope and permissions</item>
    ///   <item>Results include only active (non-deleted) workflows</item>
    ///   <item>Pagination limits are enforced for performance optimization</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Optimized queries for large workflow collections</item>
    ///   <item>Supports advanced filtering by category, status, and execution metrics</item>
    ///   <item>Includes workflow usage statistics and performance indicators</item>
    ///   <item>Implements efficient pagination with cursor-based navigation</item>
    /// </list>
    /// </remarks>
    Task<PagedResult<PromptFlow>> GetWorkflowsAsync(
        Guid tenantId, 
        WorkflowFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing workflow with comprehensive validation and change tracking.
    /// Maintains workflow integrity and validates changes against dependency requirements
    /// with full audit trail and impact analysis capabilities.
    /// </summary>
    /// <param name="workflowId">Workflow identifier to update</param>
    /// <param name="updateRequest">Update request with modified workflow data</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="updatedBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Updated workflow entity with validation results and change summary</returns>
    /// <exception cref="NotFoundException">Thrown when workflow is not found</exception>
    /// <exception cref="ArgumentException">Thrown when validation rules are violated</exception>
    /// <exception cref="InvalidOperationException">Thrown when update conflicts with business rules</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Workflow definition changes must maintain DAG structure</item>
    ///   <item>Name changes must maintain uniqueness within tenant scope</item>
    ///   <item>Node updates must pass comprehensive validation</item>
    ///   <item>Changes affecting active executions require confirmation</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates workflow changes against dependency requirements</item>
    ///   <item>Creates comprehensive audit trail for change tracking</item>
    ///   <item>Implements workflow versioning with rollback capabilities</item>
    ///   <item>Triggers cache invalidation and dependent system notifications</item>
    /// </list>
    /// </remarks>
    Task<WorkflowUpdateResult> UpdateWorkflowAsync(
        Guid workflowId, 
        UpdateWorkflowRequest updateRequest, 
        Guid tenantId, 
        Guid updatedBy, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs soft delete of a workflow with dependency validation and cleanup procedures.
    /// Ensures data integrity and validates impact on dependent executions and other workflows
    /// with comprehensive audit trail and dependency analysis capabilities.
    /// </summary>
    /// <param name="workflowId">Workflow identifier to delete</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="deletedBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Operation result with deletion status and impact analysis</returns>
    /// <exception cref="NotFoundException">Thrown when workflow is not found</exception>
    /// <exception cref="InvalidOperationException">Thrown when dependencies prevent deletion</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Workflows with active execution dependencies cannot be hard deleted</item>
    ///   <item>Soft delete preserves data integrity and execution history</item>
    ///   <item>Deletion impact analysis includes execution and dependency workflows</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Performs comprehensive dependency analysis before deletion</item>
    ///   <item>Implements soft delete with audit trail preservation</item>
    ///   <item>Triggers cleanup of associated cache entries and temporary data</item>
    ///   <item>Notifies dependent systems of workflow removal</item>
    /// </list>
    /// </remarks>
    Task<OperationResult> DeleteWorkflowAsync(
        Guid workflowId, 
        Guid tenantId, 
        Guid deletedBy, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Workflow Validation & Analysis

    /// <summary>
    /// Validates workflow definition with comprehensive analysis and dependency checking.
    /// Performs thorough validation of workflow structure, node configuration, variable dependencies,
    /// and execution feasibility to ensure successful workflow execution and operational reliability.
    /// </summary>
    /// <param name="workflowDefinition">Workflow definition to validate</param>
    /// <param name="tenantId">Tenant identifier for validation context</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive validation result with detailed error and warning information</returns>
    /// <exception cref="ArgumentException">Thrown when workflow definition is malformed</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Validation Checks:</strong></para>
    /// <list type="bullet">
    ///   <item>Workflow structure and DAG validation</item>
    ///   <item>Node configuration and dependency validation</item>
    ///   <item>Variable resolution and template availability validation</item>
    ///   <item>Execution feasibility and resource requirement analysis</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Performs comprehensive dependency analysis and validation</item>
    ///   <item>Provides detailed validation results with actionable feedback</item>
    ///   <item>Includes execution cost estimation and resource requirements</item>
    ///   <item>Supports validation caching for frequently validated workflows</item>
    /// </list>
    /// </remarks>
    Task<FlowValidationResult> ValidateWorkflowAsync(
        WorkflowDefinition workflowDefinition, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyzes workflow complexity and provides optimization recommendations.
    /// Performs comprehensive workflow analysis including performance assessment, optimization opportunities,
    /// and structural improvement recommendations for workflow efficiency and maintainability enhancement.
    /// </summary>
    /// <param name="workflowId">Workflow identifier for analysis</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive workflow analysis with optimization recommendations</returns>
    /// <exception cref="NotFoundException">Thrown when workflow is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Analysis Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Workflow complexity assessment and performance analysis</item>
    ///   <item>Optimization opportunity identification and recommendations</item>
    ///   <item>Structural improvement suggestions for maintainability</item>
    ///   <item>Resource utilization analysis and cost optimization insights</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Uses advanced algorithms for workflow complexity analysis</item>
    ///   <item>Provides prioritized optimization recommendations with impact analysis</item>
    ///   <item>Includes best practice recommendations and design pattern suggestions</item>
    ///   <item>Supports continuous analysis monitoring and improvement tracking</item>
    /// </list>
    /// </remarks>
    Task<WorkflowAnalysisResult> AnalyzeWorkflowAsync(
        Guid workflowId, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Workflow Execution & Orchestration

    /// <summary>
    /// Executes a workflow with comprehensive orchestration and monitoring capabilities.
    /// Performs complete workflow execution with node orchestration, dependency management,
    /// real-time monitoring, and comprehensive result aggregation for production workflow automation.
    /// </summary>
    /// <param name="workflowId">Workflow identifier for execution</param>
    /// <param name="inputVariables">Input variables for workflow execution</param>
    /// <param name="tenantId">Tenant identifier for access control and isolation</param>
    /// <param name="executedBy">User identifier for audit and tracking</param>
    /// <param name="options">Optional execution configuration and processing settings</param>
    /// <param name="progress">Progress reporter for real-time execution monitoring</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive workflow execution result with node execution details</returns>
    /// <exception cref="NotFoundException">Thrown when workflow is not found</exception>
    /// <exception cref="InvalidOperationException">Thrown when execution conditions are not met</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Execution Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Distributed node execution with dependency orchestration</item>
    ///   <item>Real-time progress reporting and execution monitoring</item>
    ///   <item>Comprehensive error handling and retry mechanisms</item>
    ///   <item>Detailed execution analytics and performance metrics</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements optimized workflow execution with resource management</item>
    ///   <item>Supports cancellation and graceful shutdown of long-running workflows</item>
    ///   <item>Provides comprehensive execution audit trail and analytics</item>
    ///   <item>Creates detailed performance metrics for optimization insights</item>
    /// </list>
    /// </remarks>
    Task<FlowExecutionResult> ExecuteWorkflowAsync(
        Guid workflowId, 
        Dictionary<string, object> inputVariables, 
        Guid tenantId, 
        Guid executedBy, 
        FlowExecutionOptions? options = null, 
        IProgress<WorkflowExecutionProgress>? progress = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Monitors active workflow execution with real-time status and progress tracking.
    /// Provides comprehensive monitoring capabilities for ongoing workflow execution operations
    /// with detailed status information, progress metrics, and operational control capabilities.
    /// </summary>
    /// <param name="executionId">Workflow execution identifier for monitoring</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Real-time execution status with progress metrics and operational details</returns>
    /// <exception cref="NotFoundException">Thrown when execution is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Monitoring Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Real-time execution status and progress tracking</item>
    ///   <item>Node-level execution monitoring and performance metrics</item>
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
    Task<WorkflowExecutionStatus> GetExecutionStatusAsync(
        Guid executionId, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels an active workflow execution with graceful shutdown and cleanup procedures.
    /// Provides controlled cancellation of ongoing workflow execution operations with proper resource cleanup,
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
    Task<CancellationResult> CancelWorkflowExecutionAsync(
        Guid executionId, 
        Guid tenantId, 
        Guid cancelledBy, 
        string? reason = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Workflow Analytics & Optimization

    /// <summary>
    /// Retrieves comprehensive workflow execution history with filtering and analytics capabilities.
    /// Provides detailed execution history with performance metrics, success rates, and trend analysis
    /// for workflow optimization, operational monitoring, and business intelligence workflows.
    /// </summary>
    /// <param name="workflowId">Workflow identifier for execution history</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="filters">Optional filtering criteria for execution history</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated execution history with comprehensive metrics and analytics</returns>
    /// <exception cref="NotFoundException">Thrown when workflow is not found</exception>
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
    Task<PagedResult<WorkflowExecutionHistory>> GetWorkflowExecutionHistoryAsync(
        Guid workflowId, 
        Guid tenantId, 
        ExecutionHistoryFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves comprehensive workflow analytics with performance and optimization insights.
    /// Provides detailed workflow analytics including usage patterns, performance metrics, cost analysis,
    /// and optimization recommendations for operational efficiency and business intelligence workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="filters">Optional filtering criteria for analytics data</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive workflow analytics with performance and optimization insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Analytics Capabilities:</strong></para>
    /// <list type="bullet">
    ///   <item>Workflow usage patterns and performance trends</item>
    ///   <item>Cost analysis and resource utilization optimization</item>
    ///   <item>Success rate analysis and error pattern identification</item>
    ///   <item>Workflow performance comparison and benchmarking</item>
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
    Task<WorkflowAnalyticsResult> GetWorkflowAnalyticsAsync(
        Guid tenantId, 
        WorkflowAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates optimization recommendations for workflows based on execution and performance analysis.
    /// Analyzes workflow performance, resource utilization, and execution patterns to provide actionable
    /// optimization recommendations for improved efficiency, cost optimization, and operational effectiveness.
    /// </summary>
    /// <param name="workflowId">Workflow identifier for optimization analysis</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive optimization recommendations with implementation guidance</returns>
    /// <exception cref="NotFoundException">Thrown when workflow is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Optimization Analysis:</strong></para>
    /// <list type="bullet">
    ///   <item>Performance bottleneck identification and resolution strategies</item>
    ///   <item>Resource utilization optimization and cost reduction recommendations</item>
    ///   <item>Execution pattern optimization and efficiency improvements</item>
    ///   <item>Structural optimization and best practice recommendations</item>
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
    Task<WorkflowOptimizationResult> GetOptimizationRecommendationsAsync(
        Guid workflowId, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    #endregion
}
