namespace PromptStudio.Core.DTOs.Analytics;

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
    /// Most recent execution date
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// First execution date in the period
    /// </summary>
    public DateTime? FirstExecution { get; set; }

    /// <summary>
    /// Number of unique users who executed the flow
    /// </summary>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Total token usage across all executions
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total cost across all executions
    /// </summary>
    public decimal? TotalCost { get; set; }
}

/// <summary>
/// Token usage statistics for analytics
/// </summary>
public class TokenUsage
{
    /// <summary>
    /// Number of input tokens
    /// </summary>
    public int InputTokens { get; set; }

    /// <summary>
    /// Number of output tokens
    /// </summary>
    public int OutputTokens { get; set; }

    /// <summary>
    /// Total tokens used
    /// </summary>
    public int TotalTokens => InputTokens + OutputTokens;

    /// <summary>
    /// Cost per input token
    /// </summary>
    public decimal? InputTokenCost { get; set; }

    /// <summary>
    /// Cost per output token
    /// </summary>
    public decimal? OutputTokenCost { get; set; }

    /// <summary>
    /// Total cost for this token usage
    /// </summary>
    public decimal? TotalCost => (InputTokens * (InputTokenCost ?? 0)) + (OutputTokens * (OutputTokenCost ?? 0));
}
