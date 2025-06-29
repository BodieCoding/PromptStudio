namespace PromptStudio.Core.DTOs.Flow;

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
    public List<NodeExecutionInfo> NodeExecutions { get; set; } = new();
}
