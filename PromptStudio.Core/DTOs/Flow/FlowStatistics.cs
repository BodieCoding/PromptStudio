namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Flow execution statistics for analytics and monitoring
/// </summary>
public class FlowStatistics
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
    /// Number of cancelled executions
    /// </summary>
    public int CancelledExecutions { get; set; }

    /// <summary>
    /// Success rate as percentage
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Average execution time in milliseconds
    /// </summary>
    public double AverageExecutionTime { get; set; }

    /// <summary>
    /// Minimum execution time in milliseconds
    /// </summary>
    public double MinExecutionTime { get; set; }

    /// <summary>
    /// Maximum execution time in milliseconds
    /// </summary>
    public double MaxExecutionTime { get; set; }

    /// <summary>
    /// Last execution date
    /// </summary>
    public DateTime? LastExecutionDate { get; set; }

    /// <summary>
    /// First execution date
    /// </summary>
    public DateTime? FirstExecutionDate { get; set; }

    /// <summary>
    /// Statistics calculation date
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;
}
