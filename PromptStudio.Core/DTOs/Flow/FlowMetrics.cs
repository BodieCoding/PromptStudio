namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Basic flow metrics for analytics and reporting
/// </summary>
public class FlowMetrics
{
    /// <summary>
    /// Flow ID
    /// </summary>
    public Guid FlowId { get; set; }

    /// <summary>
    /// Total number of executions
    /// </summary>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Number of successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Number of failed executions
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Success rate as percentage
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Number of cancelled executions
    /// </summary>
    public int CancelledExecutions { get; set; }

    /// <summary>
    /// Date range for these metrics
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// End date for these metrics
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Last updated timestamp
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
