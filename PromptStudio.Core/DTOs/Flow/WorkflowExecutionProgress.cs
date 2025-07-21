using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive workflow execution progress DTO with detailed status and metrics tracking.
/// Provides enterprise-grade execution monitoring capabilities with comprehensive progress tracking,
/// performance metrics, and real-time status updates for workflow execution management.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary execution progress DTO for IWorkflowOrchestrationService execution monitoring,
/// providing real-time workflow execution status with detailed progress metrics, node-level
/// execution information, and performance tracking for enterprise workflow management.</para>
/// 
/// <para><strong>Monitoring Scope:</strong></para>
/// <para>Comprehensive execution tracking including overall progress, node-level execution status,
/// resource utilization, performance metrics, and error handling. Designed for real-time
/// monitoring and management of enterprise workflow executions.</para>
/// 
/// <para><strong>Progress Tracking:</strong></para>
/// <list type="bullet">
/// <item>Overall workflow execution progress and status</item>
/// <item>Node-level execution tracking and timing</item>
/// <item>Resource utilization monitoring and metrics</item>
/// <item>Error tracking and recovery status</item>
/// </list>
/// </remarks>
public class WorkflowExecutionProgress
{
    /// <summary>
    /// Gets or sets the execution identifier for tracking and management.
    /// </summary>
    /// <value>The unique identifier for the workflow execution instance.</value>
    public string ExecutionId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow identifier for the executing workflow.
    /// </summary>
    /// <value>The unique identifier of the workflow being executed.</value>
    public int WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the workflow name for identification and reporting.
    /// </summary>
    /// <value>The name of the workflow being executed for context and reporting.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current execution status of the workflow.
    /// </summary>
    /// <value>The current status of the workflow execution (running, paused, completed, etc.).</value>
    public WorkflowExecutionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the overall progress percentage of the execution.
    /// </summary>
    /// <value>Percentage of workflow execution completion (0.0 to 100.0).</value>
    public double ProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets the execution start timestamp for tracking and metrics.
    /// </summary>
    /// <value>The date and time when the workflow execution started.</value>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the estimated completion timestamp for planning and scheduling.
    /// </summary>
    /// <value>The estimated date and time when the workflow execution will complete.</value>
    public DateTime? EstimatedCompletionAt { get; set; }

    /// <summary>
    /// Gets or sets the actual completion timestamp when execution finishes.
    /// </summary>
    /// <value>The actual date and time when the workflow execution completed (if finished).</value>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the elapsed execution time for performance tracking.
    /// </summary>
    /// <value>The total time elapsed since the workflow execution started.</value>
    public TimeSpan ElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets the estimated remaining time for completion planning.
    /// </summary>
    /// <value>The estimated time remaining for workflow execution completion.</value>
    public TimeSpan? EstimatedRemainingTime { get; set; }

    /// <summary>
    /// Gets or sets the currently executing node information.
    /// </summary>
    /// <value>Information about the node currently being executed in the workflow.</value>
    public CurrentExecutionNode? CurrentNode { get; set; }

    /// <summary>
    /// Gets or sets the execution progress for individual nodes.
    /// </summary>
    /// <value>Collection of execution progress information for each node in the workflow.</value>
    public List<NodeExecutionProgress>? NodeProgress { get; set; }

    /// <summary>
    /// Gets or sets the execution metrics for performance monitoring.
    /// </summary>
    /// <value>Real-time metrics about workflow execution performance and resource usage.</value>
    public ExecutionMetrics? ExecutionMetrics { get; set; }

    /// <summary>
    /// Gets or sets the error information if execution encounters issues.
    /// </summary>
    /// <value>Information about errors encountered during workflow execution.</value>
    public ExecutionError? LastError { get; set; }

    /// <summary>
    /// Gets or sets the warning information for execution monitoring.
    /// </summary>
    /// <value>Collection of warnings encountered during workflow execution.</value>
    public List<ExecutionWarning>? Warnings { get; set; }

    /// <summary>
    /// Gets or sets the execution input parameters for context and audit.
    /// </summary>
    /// <value>Input parameters provided for the workflow execution.</value>
    public Dictionary<string, object>? InputParameters { get; set; }

    /// <summary>
    /// Gets or sets the partial execution results as they become available.
    /// </summary>
    /// <value>Partial results from completed nodes during workflow execution.</value>
    public Dictionary<string, object>? PartialResults { get; set; }

    /// <summary>
    /// Gets or sets the execution context information for debugging and audit.
    /// </summary>
    /// <value>Contextual information about the execution environment and state.</value>
    public ExecutionContext? ExecutionContext { get; set; }

    /// <summary>
    /// Gets or sets the resource utilization information for monitoring and optimization.
    /// </summary>
    /// <value>Current resource utilization metrics for the workflow execution.</value>
    public ResourceUtilization? ResourceUtilization { get; set; }

    /// <summary>
    /// Gets or sets the checkpoint information for recovery and resumption.
    /// </summary>
    /// <value>Checkpoint information for workflow execution recovery and resumption capabilities.</value>
    public ExecutionCheckpoint? LastCheckpoint { get; set; }

    /// <summary>
    /// Gets or sets custom progress properties for workflow-specific tracking.
    /// </summary>
    /// <value>Dictionary of custom progress properties for workflow-specific monitoring needs.</value>
    public Dictionary<string, object>? CustomProgressProperties { get; set; }
}

/// <summary>
/// Represents information about the currently executing node in a workflow.
/// </summary>
public class CurrentExecutionNode
{
    /// <summary>
    /// Gets or sets the node identifier currently being executed.
    /// </summary>
    /// <value>The unique identifier of the node currently being executed.</value>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node name for identification and reporting.
    /// </summary>
    /// <value>The name of the node currently being executed.</value>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node type for processing and display purposes.
    /// </summary>
    /// <value>The type of node currently being executed.</value>
    public WorkflowNodeType NodeType { get; set; }

    /// <summary>
    /// Gets or sets the node execution start timestamp.
    /// </summary>
    /// <value>The date and time when the current node execution started.</value>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the estimated completion time for the current node.
    /// </summary>
    /// <value>The estimated date and time when the current node execution will complete.</value>
    public DateTime? EstimatedCompletionAt { get; set; }

    /// <summary>
    /// Gets or sets the progress percentage for the current node execution.
    /// </summary>
    /// <value>Percentage of completion for the current node execution (0.0 to 100.0).</value>
    public double NodeProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets the current activity description within the node.
    /// </summary>
    /// <value>Description of the current activity or operation within the executing node.</value>
    public string? CurrentActivity { get; set; }
}

/// <summary>
/// Represents execution progress information for an individual workflow node.
/// </summary>
public class NodeExecutionProgress
{
    /// <summary>
    /// Gets or sets the node identifier for the execution progress.
    /// </summary>
    /// <value>The unique identifier of the node for execution tracking.</value>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node name for identification and reporting.
    /// </summary>
    /// <value>The name of the node for execution progress tracking.</value>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the execution status for the individual node.
    /// </summary>
    /// <value>The current execution status of the individual node.</value>
    public NodeExecutionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the execution start timestamp for the node.
    /// </summary>
    /// <value>The date and time when the node execution started.</value>
    public DateTime? StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the execution completion timestamp for the node.
    /// </summary>
    /// <value>The date and time when the node execution completed (if finished).</value>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the execution duration for the node.
    /// </summary>
    /// <value>The total time taken for the node execution.</value>
    public TimeSpan? ExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the node execution result if completed successfully.
    /// </summary>
    /// <value>The result produced by the node execution (if completed successfully).</value>
    public object? ExecutionResult { get; set; }

    /// <summary>
    /// Gets or sets error information if the node execution failed.
    /// </summary>
    /// <value>Error information if the node execution encountered failures.</value>
    public ExecutionError? Error { get; set; }

    /// <summary>
    /// Gets or sets the retry count for the node execution.
    /// </summary>
    /// <value>Number of retry attempts made for the node execution.</value>
    public int RetryCount { get; set; }

    /// <summary>
    /// Gets or sets the maximum retry attempts allowed for the node.
    /// </summary>
    /// <value>Maximum number of retry attempts allowed for the node execution.</value>
    public int MaxRetryAttempts { get; set; }

    /// <summary>
    /// Gets or sets custom node properties for node-specific tracking.
    /// </summary>
    /// <value>Dictionary of custom properties for node-specific execution tracking.</value>
    public Dictionary<string, object>? CustomNodeProperties { get; set; }
}

/// <summary>
/// Represents real-time execution metrics for workflow performance monitoring.
/// </summary>
public class ExecutionMetrics
{
    /// <summary>
    /// Gets or sets the nodes completed count for progress tracking.
    /// </summary>
    /// <value>Number of nodes that have completed execution successfully.</value>
    public int NodesCompleted { get; set; }

    /// <summary>
    /// Gets or sets the total nodes count for progress calculation.
    /// </summary>
    /// <value>Total number of nodes in the workflow for progress calculation.</value>
    public int TotalNodes { get; set; }

    /// <summary>
    /// Gets or sets the nodes failed count for error tracking.
    /// </summary>
    /// <value>Number of nodes that have failed during execution.</value>
    public int NodesFailed { get; set; }

    /// <summary>
    /// Gets or sets the nodes skipped count for conditional execution tracking.
    /// </summary>
    /// <value>Number of nodes that were skipped during conditional execution.</value>
    public int NodesSkipped { get; set; }

    /// <summary>
    /// Gets or sets the average node execution time for performance tracking.
    /// </summary>
    /// <value>Average execution time per node for performance analysis.</value>
    public TimeSpan AverageNodeExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the throughput metrics for performance evaluation.
    /// </summary>
    /// <value>Throughput metrics indicating nodes processed per unit time.</value>
    public double NodeThroughput { get; set; }

    /// <summary>
    /// Gets or sets the current resource consumption metrics.
    /// </summary>
    /// <value>Current resource consumption including CPU, memory, and network usage.</value>
    public ResourceConsumption? CurrentResourceConsumption { get; set; }

    /// <summary>
    /// Gets or sets the cumulative resource consumption metrics.
    /// </summary>
    /// <value>Cumulative resource consumption since execution started.</value>
    public ResourceConsumption? CumulativeResourceConsumption { get; set; }
}

/// <summary>
/// Enumeration of node execution statuses for individual node tracking.
/// </summary>
public enum NodeExecutionStatus
{
    /// <summary>Node is pending execution</summary>
    Pending = 0,
    /// <summary>Node is currently executing</summary>
    Running = 1,
    /// <summary>Node execution completed successfully</summary>
    Completed = 2,
    /// <summary>Node execution failed</summary>
    Failed = 3,
    /// <summary>Node execution was skipped</summary>
    Skipped = 4,
    /// <summary>Node execution was cancelled</summary>
    Cancelled = 5,
    /// <summary>Node execution is paused</summary>
    Paused = 6,
    /// <summary>Node execution is waiting for dependencies</summary>
    WaitingForDependencies = 7
}

/// <summary>
/// Represents current resource consumption during workflow execution.
/// </summary>
public class ResourceConsumption
{
    /// <summary>
    /// Gets or sets the current CPU usage percentage.
    /// </summary>
    /// <value>Current CPU utilization percentage for the workflow execution.</value>
    public double CpuUsagePercentage { get; set; }

    /// <summary>
    /// Gets or sets the current memory usage in bytes.
    /// </summary>
    /// <value>Current memory consumption in bytes for the workflow execution.</value>
    public long MemoryUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the current network usage in bytes per second.
    /// </summary>
    /// <value>Current network bandwidth consumption for the workflow execution.</value>
    public long NetworkUsageBytesPerSecond { get; set; }

    /// <summary>
    /// Gets or sets the current storage usage in bytes.
    /// </summary>
    /// <value>Current storage space consumption for the workflow execution.</value>
    public long StorageUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the resource consumption was measured.
    /// </summary>
    /// <value>Date and time when the resource consumption metrics were captured.</value>
    public DateTime MeasuredAt { get; set; }
}
