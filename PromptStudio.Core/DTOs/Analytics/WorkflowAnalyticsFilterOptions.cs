namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Filter options for workflow analytics queries
/// </summary>
public class WorkflowAnalyticsFilterOptions
{
    /// <summary>
    /// Filter by specific workflow IDs
    /// </summary>
    public List<Guid>? WorkflowIds { get; set; }

    /// <summary>
    /// Filter by workflow status
    /// </summary>
    public List<string>? WorkflowStatuses { get; set; }

    /// <summary>
    /// Filter by workflow creators
    /// </summary>
    public List<Guid>? CreatorIds { get; set; }

    /// <summary>
    /// Filter by execution success rate range
    /// </summary>
    public DoubleRange? SuccessRateRange { get; set; }

    /// <summary>
    /// Filter by execution duration range (in minutes)
    /// </summary>
    public DoubleRange? DurationRange { get; set; }

    /// <summary>
    /// Filter by complexity score range
    /// </summary>
    public DoubleRange? ComplexityRange { get; set; }

    /// <summary>
    /// Include archived workflows
    /// </summary>
    public bool IncludeArchived { get; set; } = false;

    /// <summary>
    /// Group results by specific dimensions
    /// </summary>
    public List<string>? GroupBy { get; set; }

    /// <summary>
    /// Include bottleneck analysis
    /// </summary>
    public bool IncludeBottleneckAnalysis { get; set; } = false;

    /// <summary>
    /// Include efficiency metrics
    /// </summary>
    public bool IncludeEfficiencyMetrics { get; set; } = false;

    /// <summary>
    /// Custom filters as key-value pairs
    /// </summary>
    public Dictionary<string, object>? CustomFilters { get; set; }
}
