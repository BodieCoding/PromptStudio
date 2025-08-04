namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Enumeration of flow execution status values.
/// Tracks the current state of flow execution operations for monitoring and control purposes.
/// </summary>
/// <remarks>
/// <para><strong>Execution Lifecycle:</strong></para>
/// <para>Represents the complete lifecycle of flow execution from initialization through completion
/// or termination. Status values enable real-time monitoring, progress tracking, and operational
/// control of long-running flow processes.</para>
/// </remarks>
public enum FlowExecutionStatus
{
    /// <summary>
    /// Flow execution is pending and has not yet started.
    /// Initial state when execution is queued but not yet begun.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Flow execution is currently initializing and preparing to run.
    /// Validation, resource allocation, and setup operations are in progress.
    /// </summary>
    Initializing = 1,

    /// <summary>
    /// Flow execution is actively running and processing nodes.
    /// The flow is executing nodes according to the defined workflow logic.
    /// </summary>
    Running = 2,

    /// <summary>
    /// Flow execution is temporarily paused.
    /// Execution can be resumed from the current state.
    /// </summary>
    Paused = 3,

    /// <summary>
    /// Flow execution is waiting for external dependencies or resources.
    /// Execution will resume automatically when dependencies are available.
    /// </summary>
    Waiting = 4,

    /// <summary>
    /// Flow execution completed successfully.
    /// All nodes executed without errors and produced expected outputs.
    /// </summary>
    Completed = 5,

    /// <summary>
    /// Flow execution failed due to errors.
    /// One or more nodes encountered unrecoverable errors.
    /// </summary>
    Failed = 6,

    /// <summary>
    /// Flow execution was cancelled by user request.
    /// Execution was terminated before natural completion.
    /// </summary>
    Cancelled = 7,

    /// <summary>
    /// Flow execution timed out.
    /// Execution exceeded configured timeout limits.
    /// </summary>
    TimedOut = 8,

    /// <summary>
    /// Flow execution encountered a partial failure.
    /// Some nodes succeeded while others failed, but execution continued.
    /// </summary>
    PartiallySucceeded = 9,

    /// <summary>
    /// Flow execution is in an unknown or indeterminate state.
    /// Used when status cannot be determined or during recovery operations.
    /// </summary>
    Unknown = 10
}
