using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Enumeration of workflow execution statuses for comprehensive execution state management.
/// Provides enterprise-grade execution status tracking with detailed state information
/// for workflow orchestration, monitoring, and management operations.
/// </summary>
/// <remarks>
/// <para><strong>State Management:</strong></para>
/// <para>Comprehensive execution status enumeration supporting complex workflow execution
/// scenarios including queued execution, running states, completion states, and error conditions.
/// Designed for enterprise workflow orchestration with detailed state tracking and management.</para>
/// 
/// <para><strong>Status Categories:</strong></para>
/// <list type="bullet">
/// <item>Preparation states (Queued, Starting, Validating)</item>
/// <item>Active execution states (Running, Paused, WaitingForInput)</item>
/// <item>Completion states (Completed, Failed, Cancelled)</item>
/// <item>Recovery states (Retrying, Recovering, PartiallyCompleted)</item>
/// </list>
/// 
/// <para><strong>Integration Points:</strong></para>
/// <para>Used across workflow execution tracking, progress monitoring, execution history,
/// and workflow analytics for consistent status representation and management throughout
/// the enterprise workflow orchestration system.</para>
/// </remarks>
public enum WorkflowExecutionStatus
{
    /// <summary>
    /// Workflow execution is queued and waiting to be started.
    /// The execution request has been received and is pending resource allocation.
    /// </summary>
    Queued = 0,

    /// <summary>
    /// Workflow execution is in the starting phase with initialization activities.
    /// Resources are being allocated and the execution environment is being prepared.
    /// </summary>
    Starting = 1,

    /// <summary>
    /// Workflow structure and parameters are being validated before execution.
    /// Pre-execution validation checks are being performed to ensure workflow integrity.
    /// </summary>
    Validating = 2,

    /// <summary>
    /// Workflow execution is actively running with nodes being processed.
    /// The workflow is in active execution with nodes being processed according to the flow.
    /// </summary>
    Running = 3,

    /// <summary>
    /// Workflow execution has been paused and can be resumed later.
    /// Execution has been temporarily halted and can be resumed from the current state.
    /// </summary>
    Paused = 4,

    /// <summary>
    /// Workflow execution is waiting for external input or intervention.
    /// Execution has reached a point requiring external input or user intervention to continue.
    /// </summary>
    WaitingForInput = 5,

    /// <summary>
    /// Workflow execution is waiting for external dependencies to be satisfied.
    /// Execution is paused pending the availability of required external resources or services.
    /// </summary>
    WaitingForDependencies = 6,

    /// <summary>
    /// Workflow execution completed successfully with all nodes processed.
    /// All workflow nodes have been executed successfully and the workflow has completed.
    /// </summary>
    Completed = 7,

    /// <summary>
    /// Workflow execution completed but with some nodes failing or being skipped.
    /// The workflow completed its execution path but some nodes failed or were conditionally skipped.
    /// </summary>
    PartiallyCompleted = 8,

    /// <summary>
    /// Workflow execution failed due to unrecoverable errors.
    /// The workflow encountered errors that prevented successful completion and cannot be recovered.
    /// </summary>
    Failed = 9,

    /// <summary>
    /// Workflow execution was cancelled by user request or system intervention.
    /// The workflow execution was explicitly cancelled and terminated before completion.
    /// </summary>
    Cancelled = 10,

    /// <summary>
    /// Workflow execution is being retried after a failure.
    /// A failed execution is being retried according to the configured retry policy.
    /// </summary>
    Retrying = 11,

    /// <summary>
    /// Workflow execution is in recovery mode after encountering errors.
    /// The workflow is attempting to recover from errors using configured recovery strategies.
    /// </summary>
    Recovering = 12,

    /// <summary>
    /// Workflow execution timed out and was terminated.
    /// The workflow execution exceeded the configured timeout limit and was terminated.
    /// </summary>
    TimedOut = 13,

    /// <summary>
    /// Workflow execution encountered a deadlock condition.
    /// The workflow execution reached a state where no further progress is possible due to dependencies.
    /// </summary>
    Deadlocked = 14,

    /// <summary>
    /// Workflow execution is suspended pending resource availability.
    /// Execution has been suspended due to resource constraints and is waiting for available resources.
    /// </summary>
    Suspended = 15,

    /// <summary>
    /// Workflow execution is in an unknown or undefined state.
    /// The execution status cannot be determined or is in an unexpected state requiring investigation.
    /// </summary>
    Unknown = 16
}
