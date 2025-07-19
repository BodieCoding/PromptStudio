using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Result of a batch prompt execution operation
/// </summary>
public class PromptBatchExecutionResult
{
    /// <summary>
    /// Batch execution ID
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// Name of the variable collection used
    /// </summary>
    public string CollectionName { get; set; } = string.Empty;

    /// <summary>
    /// Name of the prompt template executed
    /// </summary>
    public string PromptName { get; set; } = string.Empty;

    /// <summary>
    /// Total number of variable sets processed
    /// </summary>
    public int TotalSets { get; set; }

    /// <summary>
    /// Number of successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Number of failed executions
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Number of skipped executions
    /// </summary>
    public int SkippedExecutions { get; set; }

    /// <summary>
    /// Individual execution results
    /// </summary>
    public List<PromptExecutionBatchItem> Results { get; set; } = new();

    /// <summary>
    /// Overall success rate
    /// </summary>
    public double SuccessRate => TotalSets > 0 ? (double)SuccessfulExecutions / TotalSets : 0;

    /// <summary>
    /// Total execution time
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// <summary>
    /// Average execution time per item
    /// </summary>
    public TimeSpan AverageDuration { get; set; }

    /// <summary>
    /// Total token usage across all executions
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Quality metrics for the batch
    /// </summary>
    public BatchQualityMetrics? QualityMetrics { get; set; }

    /// <summary>
    /// Performance insights
    /// </summary>
    public Dictionary<string, object> PerformanceMetrics { get; set; } = new();
}
