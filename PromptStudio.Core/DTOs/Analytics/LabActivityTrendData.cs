namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Lab activity trend data - calculated analytics for time-series reporting
/// This represents aggregated data computed from multiple domain entities
/// </summary>
public class LabActivityTrendData
{
    /// <summary>
    /// Time period for this data point
    /// </summary>
    public DateTime Period { get; set; }

    /// <summary>
    /// Number of executions in this period
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Number of unique users in this period
    /// </summary>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Number of templates created in this period
    /// </summary>
    public int TemplatesCreated { get; set; }

    /// <summary>
    /// Number of templates updated in this period
    /// </summary>
    public int TemplatesUpdated { get; set; }

    /// <summary>
    /// Number of libraries created in this period
    /// </summary>
    public int LibrariesCreated { get; set; }

    /// <summary>
    /// Number of workflows executed in this period
    /// </summary>
    public int WorkflowExecutions { get; set; }

    /// <summary>
    /// Success rate for this period
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Token usage for this period (references Analytics TokenUsage)
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Total cost for this period
    /// </summary>
    public decimal? TotalCost { get; set; }
}
