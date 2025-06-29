namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Simple DTO for node execution status (references the full NodeExecution entity)
/// Used in FlowExecutionResult for temporary execution tracking
/// </summary>
public class NodeExecutionInfo
{
    /// <summary>
    /// Node ID that was executed
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// When the node execution started
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// When the node execution completed
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Input to this node
    /// </summary>
    public object? Input { get; set; }

    /// <summary>
    /// Output from this node
    /// </summary>
    public object? Output { get; set; }

    /// <summary>
    /// Execution status (pending, running, completed, failed)
    /// </summary>
    public string Status { get; set; } = "pending";

    /// <summary>
    /// Error message if node execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Duration of node execution in milliseconds
    /// </summary>
    public long Duration => EndTime.HasValue ? (long)(EndTime.Value - StartTime).TotalMilliseconds : 0;
}
