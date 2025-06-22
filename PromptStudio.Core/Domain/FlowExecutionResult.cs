namespace PromptStudio.Core.Domain;

/// <summary>
/// Result of executing a prompt flow
/// </summary>
public class FlowExecutionResult
{
    /// <summary>
    /// Whether the execution was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The execution ID for tracking
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Final output from the flow
    /// </summary>
    public object? Output { get; set; }

    /// <summary>
    /// Execution time in milliseconds
    /// </summary>
    public long ExecutionTime { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// List of individual node executions
    /// </summary>
    public List<NodeExecution> NodeExecutions { get; set; } = new();
}

/// <summary>
/// Represents execution of a single node in the flow
/// </summary>
public class NodeExecution
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
