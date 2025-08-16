using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Result of an individual prompt execution within a batch
/// </summary>
public class PromptExecutionBatchItem
{
    /// <summary>
    /// Index in the batch
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Variables used for this execution
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = [];

    /// <summary>
    /// Resolved prompt content
    /// </summary>
    public string ResolvedPrompt { get; set; } = string.Empty;

    /// <summary>
    /// Whether the execution was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Error code for categorization
    /// </summary>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Execution ID if saved to database
    /// </summary>
    public Guid? ExecutionId { get; set; }

    /// <summary>
    /// Execution duration
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Token usage for this execution
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Quality score if available
    /// </summary>
    public double? QualityScore { get; set; }

    /// <summary>
    /// Quality metrics if available
    /// </summary>
    public QualityMetrics? QualityMetrics { get; set; }

    /// <summary>
    /// Retry count if applicable
    /// </summary>
    public int RetryCount { get; set; }
}
