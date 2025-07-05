namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Defines the execution status states for individual nodes within a flow execution.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Status enumeration used by flow execution engines, monitoring services, and progress tracking systems.
/// Essential for real-time execution monitoring, error handling, and workflow orchestration in complex flow scenarios.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Enumeration values representing the complete lifecycle of node execution states.
/// Designed for efficient status communication and consistent state management across execution services.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Real-time execution monitoring and progress tracking</item>
/// <item>Error handling and retry logic implementation</item>
/// <item>Execution audit trails and state persistence</item>
/// <item>Workflow orchestration and dependency management</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight enumeration optimized for frequent status updates and state transitions.
/// Status changes should be logged for audit purposes. Consider status persistence for long-running executions.</para>
/// </remarks>
public enum NodeExecutionStatus
{
    /// <summary>
    /// The node is waiting to be executed.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// The node is currently executing.
    /// </summary>
    Running = 1,

    /// <summary>
    /// The node executed successfully and completed.
    /// </summary>
    Success = 2,

    /// <summary>
    /// The node execution failed with an error.
    /// </summary>
    Failed = 3,

    /// <summary>
    /// The node execution was cancelled before completion.
    /// </summary>
    Cancelled = 4,

    /// <summary>
    /// The node execution exceeded the timeout limit.
    /// </summary>
    Timeout = 5,

    /// <summary>
    /// The node was skipped due to conditional logic or flow control.
    /// </summary>
    Skipped = 6,

    /// <summary>
    /// The node is in the process of retrying after a previous failure.
    /// </summary>
    Retrying = 7
}
