namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a single execution of a prompt flow
/// </summary>
public class FlowExecution
{
    /// <summary>
    /// Unique identifier for this execution
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID of the flow that was executed
    /// </summary>
    public Guid FlowId { get; set; }

    /// <summary>
    /// JSON serialized input variables
    /// </summary>
    public string InputVariables { get; set; } = "{}";

    /// <summary>
    /// JSON serialized output result
    /// </summary>
    public string? OutputResult { get; set; }

    /// <summary>
    /// Execution time in milliseconds
    /// </summary>
    public long ExecutionTime { get; set; }

    /// <summary>
    /// Execution status (completed, failed, running)
    /// </summary>
    public string Status { get; set; } = "running";

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// When the execution was started
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Navigation property to the flow
    /// </summary>
    public virtual PromptFlow? Flow { get; set; }
}
