using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Flow execution update information for real-time status updates
/// </summary>
public class FlowExecutionUpdate
{
    /// <summary>
    /// Execution ID
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Updated status
    /// </summary>
    public FlowExecutionStatus Status { get; set; }

    /// <summary>
    /// Progress percentage (0-100)
    /// </summary>
    public int Progress { get; set; }

    /// <summary>
    /// Current node being executed
    /// </summary>
    public Guid? CurrentNodeId { get; set; }

    /// <summary>
    /// Current node name
    /// </summary>
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// Number of completed nodes
    /// </summary>
    public int CompletedNodes { get; set; }

    /// <summary>
    /// Total number of nodes
    /// </summary>
    public int TotalNodes { get; set; }

    /// <summary>
    /// Error message if status is failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Estimated time remaining
    /// </summary>
    public TimeSpan? EstimatedTimeRemaining { get; set; }

    /// <summary>
    /// Token usage so far
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Cost so far
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Timestamp of this update
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
