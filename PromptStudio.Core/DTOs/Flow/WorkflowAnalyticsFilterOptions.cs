using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive filter options DTO for workflow analytics queries and reporting operations.
/// Provides enterprise-grade filtering capabilities for workflow analytics with comprehensive
/// temporal, performance, and categorical filtering support for advanced analytics and reporting.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary filtering DTO for IWorkflowOrchestrationService analytics operations,
/// enabling sophisticated workflow analytics with comprehensive filter criteria,
/// temporal analysis, and performance-based queries for enterprise analytics and reporting.</para>
/// 
/// <para><strong>Analytics Scope:</strong></para>
/// <para>Comprehensive analytics filtering including temporal analysis, performance metrics,
/// resource utilization, cost analysis, and business intelligence queries. Designed for enterprise
/// workflow analytics with advanced filtering and analytical capabilities.</para>
/// 
/// <para><strong>Filter Dimensions:</strong></para>
/// <list type="bullet">
/// <item>Temporal filtering with customizable date ranges and periods</item>
/// <item>Performance filtering with execution metrics and success rates</item>
/// <item>Resource filtering with utilization and cost parameters</item>
/// <item>Business filtering with workflows, users, and organizational units</item>
/// </list>
/// </remarks>
public class WorkflowAnalyticsFilterOptions
{
    /// <summary>
    /// Gets or sets the analysis time range for temporal filtering.
    /// </summary>
    /// <value>Date range for filtering analytics data by time period.</value>
    public DateTimeRangeFilter? AnalysisTimeRange { get; set; }

    /// <summary>
    /// Gets or sets the workflow identifiers for specific workflow analytics.
    /// </summary>
    /// <value>Collection of workflow identifiers to include in analytics analysis.</value>
    public List<int>? WorkflowIds { get; set; }

    /// <summary>
    /// Gets or sets the workflow categories for category-based analytics.
    /// </summary>
    /// <value>Collection of workflow categories to include in analytics analysis.</value>
    public List<WorkflowCategory>? WorkflowCategories { get; set; }

    /// <summary>
    /// Gets or sets the user identifiers for user-based analytics filtering.
    /// </summary>
    /// <value>Collection of user identifiers for filtering analytics by user activity.</value>
    public List<string>? UserIds { get; set; }

    /// <summary>
    /// Gets or sets the execution status filters for status-based analytics.
    /// </summary>
    /// <value>Collection of execution statuses to include in analytics analysis.</value>
    public List<WorkflowExecutionStatus>? ExecutionStatuses { get; set; }

    /// <summary>
    /// Gets or sets the minimum execution count for workflow inclusion.
    /// </summary>
    /// <value>Minimum number of executions required for workflows to be included in analytics.</value>
    public int? MinimumExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the success rate range filter for performance analytics.
    /// </summary>
    /// <value>Range filter for workflows based on their execution success rate percentage.</value>
    public NumericRangeFilter<double>? SuccessRateRange { get; set; }

    /// <summary>
    /// Gets or sets the execution duration range for performance analytics.
    /// </summary>
    /// <value>Duration range for filtering workflows by their average execution time.</value>
    public TimeSpanRangeFilter? ExecutionDurationRange { get; set; }

    /// <summary>
    /// Gets or sets the resource utilization range for resource analytics.
    /// </summary>
    /// <value>Range filter for workflows based on their resource consumption metrics.</value>
    public ResourceUtilizationRangeFilter? ResourceUtilizationRange { get; set; }

    /// <summary>
    /// Gets or sets the cost range filter for financial analytics.
    /// </summary>
    /// <value>Range filter for workflows based on their execution cost.</value>
    public NumericRangeFilter<decimal>? CostRange { get; set; }

    /// <summary>
    /// Gets or sets the complexity range filter for structural analytics.
    /// </summary>
    /// <value>Range filter for workflows based on their structural complexity metrics.</value>
    public NumericRangeFilter<int>? ComplexityRange { get; set; }

    /// <summary>
    /// Gets or sets the execution frequency range for activity analytics.
    /// </summary>
    /// <value>Range filter for workflows based on their execution frequency patterns.</value>
    public NumericRangeFilter<double>? ExecutionFrequencyRange { get; set; }

    /// <summary>
    /// Gets or sets the error rate range filter for reliability analytics.
    /// </summary>
    /// <value>Range filter for workflows based on their error rate percentage.</value>
    public NumericRangeFilter<double>? ErrorRateRange { get; set; }

    /// <summary>
    /// Gets or sets the priority levels for priority-based analytics.
    /// </summary>
    /// <value>Collection of workflow priorities to include in analytics analysis.</value>
    public List<WorkflowPriority>? PriorityLevels { get; set; }

    /// <summary>
    /// Gets or sets the execution types for execution type analytics.
    /// </summary>
    /// <value>Collection of execution types to include in analytics analysis.</value>
    public List<ExecutionType>? ExecutionTypes { get; set; }

    /// <summary>
    /// Gets or sets the environment filters for environment-based analytics.
    /// </summary>
    /// <value>Collection of execution environments to include in analytics analysis.</value>
    public List<string>? ExecutionEnvironments { get; set; }

    /// <summary>
    /// Gets or sets the organizational unit filters for organizational analytics.
    /// </summary>
    /// <value>Collection of organizational units for filtering analytics by organizational structure.</value>
    public List<string>? OrganizationalUnits { get; set; }

    /// <summary>
    /// Gets or sets the project identifiers for project-based analytics.
    /// </summary>
    /// <value>Collection of project identifiers for filtering analytics by project scope.</value>
    public List<string>? ProjectIds { get; set; }

    /// <summary>
    /// Gets or sets the tag filters for tag-based analytics.
    /// </summary>
    /// <value>Collection of tags for filtering workflows and executions by tag associations.</value>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets whether to require all tags or any tags for tag filtering.
    /// </summary>
    /// <value>True to require all specified tags; false to require any specified tags.</value>
    public bool RequireAllTags { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to include archived workflows in analytics.
    /// </summary>
    /// <value>True to include archived workflows; false to exclude; null for no filtering.</value>
    public bool? IncludeArchivedWorkflows { get; set; }

    /// <summary>
    /// Gets or sets whether to include template workflows in analytics.
    /// </summary>
    /// <value>True to include template workflows; false to exclude; null for no filtering.</value>
    public bool? IncludeTemplateWorkflows { get; set; }

    /// <summary>
    /// Gets or sets the analytics aggregation level for data grouping.
    /// </summary>
    /// <value>Level of data aggregation for analytics results (hourly, daily, weekly, monthly).</value>
    public AnalyticsAggregationLevel AggregationLevel { get; set; } = AnalyticsAggregationLevel.Daily;

    /// <summary>
    /// Gets or sets the analytics metrics to include in the analysis.
    /// </summary>
    /// <value>Collection of specific metrics to include in the analytics results.</value>
    public List<AnalyticsMetricType>? MetricsToInclude { get; set; }

    /// <summary>
    /// Gets or sets the analytics dimensions for multi-dimensional analysis.
    /// </summary>
    /// <value>Collection of dimensions for grouping and analyzing analytics data.</value>
    public List<AnalyticsDimension>? AnalyticsDimensions { get; set; }

    /// <summary>
    /// Gets or sets the comparison period for trend analysis.
    /// </summary>
    /// <value>Previous time period to compare current analytics results against.</value>
    public DateTimeRangeFilter? ComparisonPeriod { get; set; }

    /// <summary>
    /// Gets or sets whether to include trend analysis in results.
    /// </summary>
    /// <value>True to include trend analysis; false to exclude trend calculations.</value>
    public bool IncludeTrendAnalysis { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to include benchmark comparisons.
    /// </summary>
    /// <value>True to include benchmark comparisons; false to exclude benchmarking.</value>
    public bool IncludeBenchmarkComparisons { get; set; } = false;

    /// <summary>
    /// Gets or sets the percentile levels for statistical analysis.
    /// </summary>
    /// <value>Collection of percentile levels to calculate for distribution analysis.</value>
    public List<double>? PercentileLevels { get; set; }

    /// <summary>
    /// Gets or sets the tenant identifier for multi-tenant analytics filtering.
    /// </summary>
    /// <value>Tenant identifier for filtering analytics by tenant scope.</value>
    public string? TenantId { get; set; }

    /// <summary>
    /// Gets or sets custom filter criteria for advanced analytics scenarios.
    /// </summary>
    /// <value>Dictionary of custom filter properties for specialized analytics filtering needs.</value>
    public Dictionary<string, object>? CustomFilters { get; set; }

    /// <summary>
    /// Gets or sets the result limit for analytics data volume control.
    /// </summary>
    /// <value>Maximum number of results to return in analytics queries.</value>
    public int? ResultLimit { get; set; }

    /// <summary>
    /// Gets or sets the sort criteria for analytics result ordering.
    /// </summary>
    /// <value>Sort criteria for ordering analytics results by specific metrics or dimensions.</value>
    public List<AnalyticsSortCriteria>? SortCriteria { get; set; }
}

/// <summary>
/// Represents resource utilization range filter for resource-based analytics.
/// </summary>
public class ResourceUtilizationRangeFilter
{
    /// <summary>
    /// Gets or sets the CPU utilization range for filtering.
    /// </summary>
    /// <value>Range filter for average CPU utilization percentage.</value>
    public NumericRangeFilter<double>? CpuUtilizationRange { get; set; }

    /// <summary>
    /// Gets or sets the memory utilization range for filtering.
    /// </summary>
    /// <value>Range filter for average memory utilization percentage.</value>
    public NumericRangeFilter<double>? MemoryUtilizationRange { get; set; }

    /// <summary>
    /// Gets or sets the network utilization range for filtering.
    /// </summary>
    /// <value>Range filter for average network bandwidth utilization.</value>
    public NumericRangeFilter<long>? NetworkUtilizationRange { get; set; }

    /// <summary>
    /// Gets or sets the storage utilization range for filtering.
    /// </summary>
    /// <value>Range filter for average storage space utilization.</value>
    public NumericRangeFilter<long>? StorageUtilizationRange { get; set; }

    /// <summary>
    /// Gets or sets the resource efficiency range for filtering.
    /// </summary>
    /// <value>Range filter for calculated resource efficiency scores.</value>
    public NumericRangeFilter<double>? ResourceEfficiencyRange { get; set; }
}

/// <summary>
/// Represents sort criteria for analytics result ordering.
/// </summary>
public class AnalyticsSortCriteria
{
    /// <summary>
    /// Gets or sets the field or metric to sort by.
    /// </summary>
    /// <value>Name of the field or metric for sorting analytics results.</value>
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sort direction for the field.
    /// </summary>
    /// <value>Direction for sorting the specified field (ascending or descending).</value>
    public SortDirection SortDirection { get; set; } = SortDirection.Descending;

    /// <summary>
    /// Gets or sets the sort priority for multi-field sorting.
    /// </summary>
    /// <value>Priority level for this sort criteria (lower numbers have higher priority).</value>
    public int SortPriority { get; set; } = 0;
}

/// <summary>
/// Enumeration of analytics aggregation levels for temporal data grouping.
/// </summary>
public enum AnalyticsAggregationLevel
{
    /// <summary>Hourly data aggregation</summary>
    Hourly = 0,
    /// <summary>Daily data aggregation</summary>
    Daily = 1,
    /// <summary>Weekly data aggregation</summary>
    Weekly = 2,
    /// <summary>Monthly data aggregation</summary>
    Monthly = 3,
    /// <summary>Quarterly data aggregation</summary>
    Quarterly = 4,
    /// <summary>Yearly data aggregation</summary>
    Yearly = 5,
    /// <summary>Real-time data without aggregation</summary>
    RealTime = 6
}

/// <summary>
/// Enumeration of analytics metric types for selective metric inclusion.
/// </summary>
public enum AnalyticsMetricType
{
    /// <summary>Execution count metrics</summary>
    ExecutionCount = 0,
    /// <summary>Success rate metrics</summary>
    SuccessRate = 1,
    /// <summary>Average execution duration metrics</summary>
    AverageExecutionDuration = 2,
    /// <summary>Resource utilization metrics</summary>
    ResourceUtilization = 3,
    /// <summary>Cost metrics</summary>
    Cost = 4,
    /// <summary>Error rate metrics</summary>
    ErrorRate = 5,
    /// <summary>Throughput metrics</summary>
    Throughput = 6,
    /// <summary>User activity metrics</summary>
    UserActivity = 7,
    /// <summary>Workflow popularity metrics</summary>
    WorkflowPopularity = 8,
    /// <summary>Performance trend metrics</summary>
    PerformanceTrends = 9,
    /// <summary>Quality score metrics</summary>
    QualityScore = 10,
    /// <summary>Efficiency metrics</summary>
    Efficiency = 11
}
