using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Flow execution statistics
/// </summary>
public class FlowExecutionStatistics
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
    /// Success rate as a percentage
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Average execution duration
    /// </summary>
    public TimeSpan? AverageExecutionDuration { get; set; }

    /// <summary>
    /// Fastest execution time
    /// </summary>
    public TimeSpan? FastestExecution { get; set; }

    /// <summary>
    /// Slowest execution time
    /// </summary>
    public TimeSpan? SlowestExecution { get; set; }

    /// <summary>
    /// Total token usage across all executions
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total cost across all executions
    /// </summary>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Last execution date
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Statistics calculation period start
    /// </summary>
    public DateTime? PeriodStart { get; set; }

    /// <summary>
    /// Statistics calculation period end
    /// </summary>
    public DateTime? PeriodEnd { get; set; }
}
