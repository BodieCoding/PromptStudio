namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the execution lifecycle states of workflow instances during runtime processing.
/// 
/// <para><strong>Business Context:</strong></para>
/// Flow execution status provides visibility into workflow processing states, enabling
/// operational monitoring, SLA tracking, and business process management. Status tracking
/// supports automated notifications, escalation procedures, and performance analytics
/// critical for enterprise workflow operations.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Execution status integrates with workflow engines, monitoring systems, and notification
/// services to provide real-time operational visibility. Status transitions trigger
/// automated actions, logging, and metrics collection for comprehensive workflow observability.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Real-time workflow execution visibility
/// - Automated monitoring and alerting capabilities
/// - SLA compliance tracking and reporting
/// - Operational insights for process optimization
/// </summary>
/// <remarks>
/// <para><strong>Status Lifecycle:</strong></para>
/// Typical flow: Pending → Running → Completed/Failed/Cancelled
/// Partial completion indicates some nodes succeeded while others failed.
/// 
/// <para><strong>Monitoring Integration:</strong></para>
/// - Status changes trigger monitoring events
/// - Failed and TimedOut statuses require attention
/// - Performance metrics tracked by status durations
/// - SLA compliance measured against execution times
/// 
/// <para><strong>Error Handling:</strong></para>
/// - Failed status should include error details and recovery options
/// - PartiallyCompleted provides granular failure analysis
/// - TimedOut enables investigation of performance issues
/// - Cancelled supports graceful workflow termination
/// </remarks>
/// <example>
/// A document processing workflow: Pending (queued) → Running (processing) → 
/// Completed (success) or Failed (error) with detailed error information.
/// </example>
public enum FlowExecutionStatus
{
    /// <summary>
    /// Initial status when workflow execution is queued but not yet started.
    /// Indicates the workflow is waiting for available execution resources
    /// or dependencies to be satisfied before processing begins.
    /// </summary>
    Pending = 0,
    
    /// <summary>
    /// Active execution status indicating the workflow is currently processing.
    /// Workflow nodes are being executed according to the defined flow logic
    /// and execution is progressing through the workflow graph.
    /// </summary>
    Running = 1,
    
    /// <summary>
    /// Successful completion status indicating all workflow nodes executed successfully.
    /// The workflow has finished processing and produced the expected results
    /// without encountering errors or exceptions.
    /// </summary>
    Completed = 2,
    
    /// <summary>
    /// Failure status indicating the workflow encountered unrecoverable errors.
    /// Execution was terminated due to exceptions, validation failures, or
    /// other error conditions that prevented successful completion.
    /// </summary>
    Failed = 3,
    
    /// <summary>
    /// Cancelled status indicating the workflow was manually terminated before completion.
    /// User or system initiated cancellation stopped execution, typically for
    /// resource management, priority changes, or business requirement changes.
    /// </summary>
    Cancelled = 4,
    
    /// <summary>
    /// Timeout status indicating the workflow exceeded maximum allowed execution time.
    /// Execution was terminated due to SLA constraints, resource limits, or
    /// performance thresholds to prevent resource exhaustion.
    /// </summary>
    TimedOut = 5,
    
    /// <summary>
    /// Partial completion status indicating some workflow nodes succeeded while others failed.
    /// Provides granular visibility into workflow execution where partial results
    /// may still be valuable despite overall workflow incompletion.
    /// </summary>
    PartiallyCompleted = 6
}
