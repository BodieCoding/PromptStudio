namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the execution status of a workflow node during processing.
/// Provides detailed state tracking for node execution lifecycle management.
/// </summary>
/// <remarks>
/// This enumeration supports comprehensive workflow monitoring and debugging
/// by providing granular status information for each stage of node execution.
/// </remarks>
public enum NodeExecutionStatus
{
    /// <summary>
    /// The node is waiting to be executed.
    /// Initial state when a node is queued but not yet started.
    /// </summary>
    Pending = 0,
    
    /// <summary>
    /// The node is currently being executed.
    /// Indicates active processing is in progress.
    /// </summary>
    Running = 1,
    
    /// <summary>
    /// The node execution completed successfully.
    /// All processing finished without errors and produced valid output.
    /// </summary>
    Completed = 2,
    
    /// <summary>
    /// The node execution failed due to an error.
    /// Processing encountered an unrecoverable error condition.
    /// </summary>
    Failed = 3,
    
    /// <summary>
    /// The node execution was cancelled before completion.
    /// Processing was intentionally stopped by user or system action.
    /// </summary>
    Cancelled = 4,
    
    /// <summary>
    /// The node execution exceeded its timeout limit.
    /// Processing was terminated due to duration constraints.
    /// </summary>
    TimedOut = 5,
    
    /// <summary>
    /// The node is waiting for retry after a failed attempt.
    /// Indicates a temporary failure state before retry execution.
    /// </summary>
    Retrying = 6,
    
    /// <summary>
    /// The node is temporarily paused.
    /// Execution can be resumed from the current state.
    /// </summary>
    Paused = 7,
    
    /// <summary>
    /// The node was skipped during execution.
    /// Processing bypassed this node based on conditional logic.
    /// </summary>
    Skipped = 8
}
