using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive filter options DTO for workflow execution history queries and analysis operations.
/// Provides enterprise-grade filtering, sorting, and pagination capabilities for execution history
/// discovery, analysis, and reporting with comprehensive multi-dimensional filtering support.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary filtering DTO for IWorkflowOrchestrationService execution history queries,
/// enabling sophisticated execution history analysis with comprehensive filter criteria,
/// temporal filtering, and performance-based queries for enterprise execution tracking.</para>
/// 
/// <para><strong>Performance Optimization:</strong></para>
/// <para>Optimized for efficient database queries with indexed filter properties and intelligent
/// query optimization. Supports large-scale execution history repositories with advanced filtering
/// and analytical capabilities for enterprise execution monitoring and reporting.</para>
/// 
/// <para><strong>Filter Categories:</strong></para>
/// <list type="bullet">
/// <item>Temporal filtering with date ranges and execution periods</item>
/// <item>Status-based filtering with execution outcomes and states</item>
/// <item>Performance-based filtering with duration and resource metrics</item>
/// <item>User and workflow-based filtering for ownership and workflow tracking</item>
/// </list>
/// </remarks>
public class ExecutionHistoryFilterOptions
{
    /// <summary>
    /// Gets or sets the workflow identifier for filtering by specific workflow.
    /// </summary>
    /// <value>Unique identifier of the workflow to filter execution history by.</value>
    public int? WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the collection of workflow identifiers for multi-workflow filtering.
    /// </summary>
    /// <value>Collection of workflow identifiers to include in execution history results.</value>
    public List<int>? WorkflowIds { get; set; }

    /// <summary>
    /// Gets or sets the execution status filters for status-based queries.
    /// </summary>
    /// <value>Collection of execution statuses to include in the results.</value>
    public List<WorkflowExecutionStatus>? ExecutionStatuses { get; set; }

    /// <summary>
    /// Gets or sets the execution start date range filter for temporal queries.
    /// </summary>
    /// <value>Date range for filtering executions by their start date and time.</value>
    public DateTimeRangeFilter? StartDateRange { get; set; }

    /// <summary>
    /// Gets or sets the execution completion date range filter for temporal queries.
    /// </summary>
    /// <value>Date range for filtering executions by their completion date and time.</value>
    public DateTimeRangeFilter? CompletionDateRange { get; set; }

    /// <summary>
    /// Gets or sets the execution duration range filter for performance-based queries.
    /// </summary>
    /// <value>Duration range for filtering executions by their execution time.</value>
    public TimeSpanRangeFilter? ExecutionDurationRange { get; set; }

    /// <summary>
    /// Gets or sets the user identifier for filtering by execution initiator.
    /// </summary>
    /// <value>Identifier of the user who initiated the workflow executions to filter by.</value>
    public string? InitiatedBy { get; set; }

    /// <summary>
    /// Gets or sets the search text for filtering by execution parameters or results.
    /// </summary>
    /// <value>Text to search across execution parameters, results, and metadata.</value>
    public string? SearchText { get; set; }

    /// <summary>
    /// Gets or sets the minimum success rate filter for performance-based queries.
    /// </summary>
    /// <value>Minimum success rate percentage for workflows to include in results.</value>
    public double? MinimumSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the resource usage range filter for resource-based queries.
    /// </summary>
    /// <value>Range filter for executions based on their resource consumption metrics.</value>
    public ResourceUsageRangeFilter? ResourceUsageRange { get; set; }

    /// <summary>
    /// Gets or sets the cost range filter for financial analysis queries.
    /// </summary>
    /// <value>Range filter for executions based on their execution cost.</value>
    public NumericRangeFilter<decimal>? CostRange { get; set; }

    /// <summary>
    /// Gets or sets the execution environment filter for deployment-based queries.
    /// </summary>
    /// <value>Environment identifier for filtering executions by deployment environment.</value>
    public string? ExecutionEnvironment { get; set; }

    /// <summary>
    /// Gets or sets the execution priority filter for priority-based queries.
    /// </summary>
    /// <value>Collection of execution priorities to include in the results.</value>
    public List<WorkflowPriority>? ExecutionPriorities { get; set; }

    /// <summary>
    /// Gets or sets whether to include executions that encountered errors.
    /// </summary>
    /// <value>True to include error executions; false to exclude; null for no filtering.</value>
    public bool? IncludeErrorExecutions { get; set; }

    /// <summary>
    /// Gets or sets whether to include executions that were cancelled.
    /// </summary>
    /// <value>True to include cancelled executions; false to exclude; null for no filtering.</value>
    public bool? IncludeCancelledExecutions { get; set; }

    /// <summary>
    /// Gets or sets whether to include executions that timed out.
    /// </summary>
    /// <value>True to include timed out executions; false to exclude; null for no filtering.</value>
    public bool? IncludeTimedOutExecutions { get; set; }

    /// <summary>
    /// Gets or sets the retry count range for filtering by execution retry attempts.
    /// </summary>
    /// <value>Range filter for executions based on their retry attempt count.</value>
    public NumericRangeFilter<int>? RetryCountRange { get; set; }

    /// <summary>
    /// Gets or sets the node count range for filtering by workflow complexity.
    /// </summary>
    /// <value>Range filter for executions based on the node count of executed workflows.</value>
    public NumericRangeFilter<int>? NodeCountRange { get; set; }

    /// <summary>
    /// Gets or sets the execution type filter for categorizing executions.
    /// </summary>
    /// <value>Type of execution (manual, scheduled, triggered) to filter by.</value>
    public ExecutionType? ExecutionType { get; set; }

    /// <summary>
    /// Gets or sets the tenant identifier for multi-tenant filtering.
    /// </summary>
    /// <value>Tenant identifier for filtering executions by tenant scope.</value>
    public string? TenantId { get; set; }

    /// <summary>
    /// Gets or sets the tags filter for tag-based execution queries.
    /// </summary>
    /// <value>Collection of tags that executions must be associated with.</value>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets whether to require all tags or any tags for tag filtering.
    /// </summary>
    /// <value>True to require all specified tags; false to require any specified tags.</value>
    public bool RequireAllTags { get; set; } = false;

    /// <summary>
    /// Gets or sets custom filter criteria for advanced filtering scenarios.
    /// </summary>
    /// <value>Dictionary of custom filter properties for specialized filtering needs.</value>
    public Dictionary<string, object>? CustomFilters { get; set; }

    /// <summary>
    /// Gets or sets the page number for paginated results.
    /// </summary>
    /// <value>One-based page number for result pagination.</value>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size for paginated results.
    /// </summary>
    /// <value>Number of items to return per page.</value>
    public int PageSize { get; set; } = 50;

    /// <summary>
    /// Gets or sets the sort field for result ordering.
    /// </summary>
    /// <value>Field name to sort results by (StartedAt, CompletedAt, Duration, etc.).</value>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets the sort direction for result ordering.
    /// </summary>
    /// <value>Direction for sorting results (ascending or descending).</value>
    public SortDirection SortDirection { get; set; } = SortDirection.Descending;

    /// <summary>
    /// Gets or sets additional sort criteria for complex sorting scenarios.
    /// </summary>
    /// <value>Collection of additional sort criteria for multi-field sorting.</value>
    public List<SortCriteria>? AdditionalSortCriteria { get; set; }
}

/// <summary>
/// Represents a resource usage range filter for performance-based queries.
/// </summary>
public class ResourceUsageRangeFilter
{
    /// <summary>
    /// Gets or sets the CPU usage percentage range for filtering.
    /// </summary>
    /// <value>Range filter for CPU usage percentage during execution.</value>
    public NumericRangeFilter<double>? CpuUsageRange { get; set; }

    /// <summary>
    /// Gets or sets the memory usage range for filtering (in bytes).
    /// </summary>
    /// <value>Range filter for memory consumption during execution.</value>
    public NumericRangeFilter<long>? MemoryUsageRange { get; set; }

    /// <summary>
    /// Gets or sets the network usage range for filtering (in bytes).
    /// </summary>
    /// <value>Range filter for network bandwidth consumption during execution.</value>
    public NumericRangeFilter<long>? NetworkUsageRange { get; set; }

    /// <summary>
    /// Gets or sets the storage usage range for filtering (in bytes).
    /// </summary>
    /// <value>Range filter for storage space consumption during execution.</value>
    public NumericRangeFilter<long>? StorageUsageRange { get; set; }
}

/// <summary>
/// Enumeration of execution types for categorizing workflow executions.
/// </summary>
public enum ExecutionType
{
    /// <summary>Manual execution initiated by user action</summary>
    Manual = 0,
    /// <summary>Scheduled execution based on time triggers</summary>
    Scheduled = 1,
    /// <summary>Event-triggered execution based on system events</summary>
    Triggered = 2,
    /// <summary>API-initiated execution from external systems</summary>
    Api = 3,
    /// <summary>Retry execution from previous failure</summary>
    Retry = 4,
    /// <summary>Recovery execution from system restoration</summary>
    Recovery = 5,
    /// <summary>Test execution for validation purposes</summary>
    Test = 6,
    /// <summary>Debug execution for troubleshooting</summary>
    Debug = 7
}
