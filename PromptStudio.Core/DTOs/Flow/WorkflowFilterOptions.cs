using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive filter options DTO for workflow queries and management operations.
/// Provides enterprise-grade filtering, sorting, and pagination capabilities for
/// workflow discovery, management, and analytics with multi-tenant support.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary filtering DTO for IWorkflowOrchestrationService workflow queries,
/// enabling sophisticated workflow discovery with comprehensive filter criteria,
/// sorting options, and pagination support for enterprise workflow management.</para>
/// 
/// <para><strong>Performance Optimization:</strong></para>
/// <para>Optimized for efficient database queries with indexed filter properties
/// and intelligent query optimization. Supports large-scale workflow repositories
/// with advanced filtering and search capabilities for enterprise scenarios.</para>
/// 
/// <para><strong>Query Patterns:</strong></para>
/// <list type="bullet">
/// <item>Text search across names, descriptions, and tags</item>
/// <item>Category-based filtering with hierarchical support</item>
/// <item>Status and execution state filtering</item>
/// <item>Date range filtering for temporal queries</item>
/// <item>Multi-criteria combination with logical operators</item>
/// </list>
/// </remarks>
public class WorkflowFilterOptions
{
    /// <summary>
    /// Gets or sets the search text for workflow name and description filtering.
    /// </summary>
    /// <value>Text to search across workflow names, descriptions, and metadata.</value>
    public string? SearchText { get; set; }

    /// <summary>
    /// Gets or sets the workflow categories for category-based filtering.
    /// </summary>
    /// <value>Collection of workflow categories to include in results.</value>
    public List<WorkflowCategory>? Categories { get; set; }

    /// <summary>
    /// Gets or sets the workflow statuses for status-based filtering.
    /// </summary>
    /// <value>Collection of workflow statuses to include in results.</value>
    public List<WorkflowStatus>? Statuses { get; set; }

    /// <summary>
    /// Gets or sets the workflow tags for tag-based filtering.
    /// </summary>
    /// <value>Collection of tags that workflows must contain for inclusion.</value>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets whether to require all tags or any tags for filtering.
    /// </summary>
    /// <value>True to require all specified tags; false to require any specified tags.</value>
    public bool RequireAllTags { get; set; } = false;

    /// <summary>
    /// Gets or sets the workflow owner identifier for ownership-based filtering.
    /// </summary>
    /// <value>Identifier of the user who owns the workflows to include in results.</value>
    public string? OwnerId { get; set; }

    /// <summary>
    /// Gets or sets whether to include only enabled workflows.
    /// </summary>
    /// <value>True to include only enabled workflows; false to include all workflows; null for no filtering.</value>
    public bool? IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets the creation date range filter for temporal queries.
    /// </summary>
    /// <value>Date range for filtering workflows by creation date.</value>
    public DateTimeRangeFilter? CreatedDateRange { get; set; }

    /// <summary>
    /// Gets or sets the last modified date range filter for temporal queries.
    /// </summary>
    /// <value>Date range for filtering workflows by last modification date.</value>
    public DateTimeRangeFilter? ModifiedDateRange { get; set; }

    /// <summary>
    /// Gets or sets the last executed date range filter for execution-based queries.
    /// </summary>
    /// <value>Date range for filtering workflows by last execution date.</value>
    public DateTimeRangeFilter? LastExecutedDateRange { get; set; }

    /// <summary>
    /// Gets or sets the execution frequency range for activity-based filtering.
    /// </summary>
    /// <value>Range filter for workflows based on their execution frequency metrics.</value>
    public NumericRangeFilter<int>? ExecutionFrequencyRange { get; set; }

    /// <summary>
    /// Gets or sets the success rate range for performance-based filtering.
    /// </summary>
    /// <value>Range filter for workflows based on their execution success rate percentage.</value>
    public NumericRangeFilter<double>? SuccessRateRange { get; set; }

    /// <summary>
    /// Gets or sets the average execution duration range for performance filtering.
    /// </summary>
    /// <value>Range filter for workflows based on their average execution duration.</value>
    public TimeSpanRangeFilter? AverageExecutionDurationRange { get; set; }

    /// <summary>
    /// Gets or sets the workflow complexity range for structural filtering.
    /// </summary>
    /// <value>Range filter for workflows based on their node count complexity.</value>
    public NumericRangeFilter<int>? ComplexityRange { get; set; }

    /// <summary>
    /// Gets or sets whether to include archived workflows in results.
    /// </summary>
    /// <value>True to include archived workflows; false to exclude archived workflows; null for no filtering.</value>
    public bool? IncludeArchived { get; set; }

    /// <summary>
    /// Gets or sets whether to include template workflows in results.
    /// </summary>
    /// <value>True to include template workflows; false to exclude template workflows; null for no filtering.</value>
    public bool? IncludeTemplates { get; set; }

    /// <summary>
    /// Gets or sets the minimum node count for structural filtering.
    /// </summary>
    /// <value>Minimum number of nodes that workflows must contain.</value>
    public int? MinimumNodeCount { get; set; }

    /// <summary>
    /// Gets or sets the maximum node count for structural filtering.
    /// </summary>
    /// <value>Maximum number of nodes that workflows can contain.</value>
    public int? MaximumNodeCount { get; set; }

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
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// Gets or sets the sort field for result ordering.
    /// </summary>
    /// <value>Field name to sort results by.</value>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets the sort direction for result ordering.
    /// </summary>
    /// <value>Direction for sorting results (ascending or descending).</value>
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}

/// <summary>
/// Enumeration of supported workflow statuses for filtering and management.
/// </summary>
public enum WorkflowStatus
{
    /// <summary>Draft workflow under development</summary>
    Draft = 0,
    /// <summary>Active workflow available for execution</summary>
    Active = 1,
    /// <summary>Paused workflow temporarily disabled</summary>
    Paused = 2,
    /// <summary>Disabled workflow not available for execution</summary>
    Disabled = 3,
    /// <summary>Archived workflow preserved for historical reference</summary>
    Archived = 4,
    /// <summary>Deleted workflow marked for removal</summary>
    Deleted = 5
}

/// <summary>
/// Represents a date and time range filter for temporal queries.
/// </summary>
public class DateTimeRangeFilter
{
    /// <summary>
    /// Gets or sets the start date and time for the range filter.
    /// </summary>
    /// <value>Starting date and time for the range (inclusive).</value>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date and time for the range filter.
    /// </summary>
    /// <value>Ending date and time for the range (inclusive).</value>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets whether to include the start date boundary.
    /// </summary>
    /// <value>True to include start date; otherwise, false.</value>
    public bool IncludeStartDate { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to include the end date boundary.
    /// </summary>
    /// <value>True to include end date; otherwise, false.</value>
    public bool IncludeEndDate { get; set; } = true;
}

/// <summary>
/// Represents a numeric range filter for quantitative queries.
/// </summary>
/// <typeparam name="T">The numeric type for the range filter.</typeparam>
public class NumericRangeFilter<T> where T : struct, IComparable<T>
{
    /// <summary>
    /// Gets or sets the minimum value for the range filter.
    /// </summary>
    /// <value>Minimum value for the range (inclusive by default).</value>
    public T? MinValue { get; set; }

    /// <summary>
    /// Gets or sets the maximum value for the range filter.
    /// </summary>
    /// <value>Maximum value for the range (inclusive by default).</value>
    public T? MaxValue { get; set; }

    /// <summary>
    /// Gets or sets whether to include the minimum value boundary.
    /// </summary>
    /// <value>True to include minimum value; otherwise, false.</value>
    public bool IncludeMinValue { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to include the maximum value boundary.
    /// </summary>
    /// <value>True to include maximum value; otherwise, false.</value>
    public bool IncludeMaxValue { get; set; } = true;
}

/// <summary>
/// Represents a time span range filter for duration-based queries.
/// </summary>
public class TimeSpanRangeFilter
{
    /// <summary>
    /// Gets or sets the minimum time span for the range filter.
    /// </summary>
    /// <value>Minimum time span for the range (inclusive by default).</value>
    public TimeSpan? MinDuration { get; set; }

    /// <summary>
    /// Gets or sets the maximum time span for the range filter.
    /// </summary>
    /// <value>Maximum time span for the range (inclusive by default).</value>
    public TimeSpan? MaxDuration { get; set; }

    /// <summary>
    /// Gets or sets whether to include the minimum duration boundary.
    /// </summary>
    /// <value>True to include minimum duration; otherwise, false.</value>
    public bool IncludeMinDuration { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to include the maximum duration boundary.
    /// </summary>
    /// <value>True to include maximum duration; otherwise, false.</value>
    public bool IncludeMaxDuration { get; set; } = true;
}

/// <summary>
/// Represents sort criteria for complex multi-field sorting scenarios.
/// </summary>
public class SortCriteria
{
    /// <summary>
    /// Gets or sets the field name to sort by.
    /// </summary>
    /// <value>Name of the field for sorting.</value>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sort direction for this field.
    /// </summary>
    /// <value>Direction for sorting this field.</value>
    public SortDirection Direction { get; set; } = SortDirection.Ascending;

    /// <summary>
    /// Gets or sets the sort priority for multi-field sorting.
    /// </summary>
    /// <value>Priority level for this sort criteria (lower numbers have higher priority).</value>
    public int Priority { get; set; } = 0;
}
