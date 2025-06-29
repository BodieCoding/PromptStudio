namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Node execution result
/// </summary>
public class NodeExecutionResult
{
    /// <summary>
    /// Node execution ID
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Node ID that was executed
    /// </summary>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Node name
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Node type
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// Whether the node execution was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Node execution start time
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Node execution completion time
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Node execution duration
    /// </summary>
    public TimeSpan? Duration => CompletedAt?.Subtract(StartedAt);

    /// <summary>
    /// Input data provided to the node
    /// </summary>
    public Dictionary<string, object> InputData { get; set; } = new();

    /// <summary>
    /// Output data produced by the node
    /// </summary>
    public Dictionary<string, object> OutputData { get; set; } = new();

    /// <summary>
    /// Node execution status
    /// </summary>
    public NodeExecutionStatus Status { get; set; }

    /// <summary>
    /// Token usage for this node
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Cost for this node execution
    /// </summary>
    public decimal? Cost { get; set; }
}
