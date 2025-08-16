namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Progress reporting for batch execution operations
/// </summary>
public class BatchExecutionProgress
{
    /// <summary>
    /// Total number of items to process
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Number of items completed
    /// </summary>
    public int CompletedItems { get; set; }

    /// <summary>
    /// Number of items that failed
    /// </summary>
    public int FailedItems { get; set; }

    /// <summary>
    /// Number of items currently being processed
    /// </summary>
    public int InProgressItems { get; set; }

    /// <summary>
    /// Percentage completion (0-100)
    /// </summary>
    public double PercentComplete => TotalItems > 0 ? (double)CompletedItems / TotalItems * 100 : 0;

    /// <summary>
    /// Current item being processed (optional)
    /// </summary>
    public string? CurrentItem { get; set; }

    /// <summary>
    /// Estimated time remaining
    /// </summary>
    public TimeSpan? EstimatedTimeRemaining { get; set; }

    /// <summary>
    /// Elapsed time since batch started
    /// </summary>
    public TimeSpan ElapsedTime { get; set; }

    /// <summary>
    /// Average time per item
    /// </summary>
    public TimeSpan? AverageTimePerItem { get; set; }

    /// <summary>
    /// Current throughput (items per second)
    /// </summary>
    public double? Throughput { get; set; }

    /// <summary>
    /// Any errors encountered during processing
    /// </summary>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// Additional status information
    /// </summary>
    public string? StatusMessage { get; set; }

    /// <summary>
    /// Batch execution ID for tracking
    /// </summary>
    public Guid? BatchId { get; set; }
}
