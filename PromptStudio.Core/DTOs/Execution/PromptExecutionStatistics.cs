using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Prompt execution statistics with detailed metrics
/// </summary>
public class PromptExecutionStatistics
{
    /// <summary>
    /// Total number of executions
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Number of successful executions
    /// </summary>
    public long SuccessfulExecutions { get; set; }

    /// <summary>
    /// Number of failed executions
    /// </summary>
    public long FailedExecutions { get; set; }

    /// <summary>
    /// Success rate as a percentage
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Most recent execution timestamp
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// First execution timestamp
    /// </summary>
    public DateTime? FirstExecution { get; set; }

    /// <summary>
    /// Average executions per day
    /// </summary>
    public double AverageExecutionsPerDay { get; set; }

    /// <summary>
    /// Average execution time
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Median execution time
    /// </summary>
    public TimeSpan MedianExecutionTime { get; set; }

    /// <summary>
    /// 95th percentile execution time
    /// </summary>
    public TimeSpan P95ExecutionTime { get; set; }

    /// <summary>
    /// Total token usage across all executions
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Count of variable usage
    /// </summary>
    public Dictionary<string, long> VariableUsageCount { get; set; } = [];

    /// <summary>
    /// Errors categorized by type
    /// </summary>
    public Dictionary<string, long> ErrorsByType { get; set; } = [];

    /// <summary>
    /// Performance metrics by model
    /// </summary>
    public Dictionary<string, double> ModelPerformance { get; set; } = [];

    /// <summary>
    /// Quality metrics
    /// </summary>
    public QualityMetrics? QualityMetrics { get; set; }
}
