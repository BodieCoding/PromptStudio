using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive workflow execution history DTO with detailed execution information and metrics.
/// Provides enterprise-grade execution tracking capabilities with comprehensive execution details,
/// performance metrics, resource utilization, and audit information for workflow execution management.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary execution history DTO for IWorkflowOrchestrationService execution tracking,
/// providing comprehensive workflow execution records with detailed metrics, resource usage,
/// and audit information for enterprise execution monitoring and analysis operations.</para>
/// 
/// <para><strong>Historical Context:</strong></para>
/// <para>Complete execution record including input parameters, execution results, performance metrics,
/// error information, and resource utilization. Designed for enterprise execution audit, analysis,
/// and performance optimization with comprehensive historical data preservation.</para>
/// 
/// <para><strong>Data Categories:</strong></para>
/// <list type="bullet">
/// <item>Execution identification and timing information</item>
/// <item>Input parameters and execution configuration</item>
/// <item>Execution results and output data</item>
/// <item>Performance metrics and resource utilization</item>
/// <item>Error information and diagnostic data</item>
/// </list>
/// </remarks>
public class WorkflowExecutionHistory
{
    /// <summary>
    /// Gets or sets the unique execution identifier for tracking and reference.
    /// </summary>
    /// <value>The unique identifier for this specific workflow execution instance.</value>
    public string ExecutionId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow identifier for the executed workflow.
    /// </summary>
    /// <value>The unique identifier of the workflow that was executed.</value>
    public int WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the workflow name for identification and reporting.
    /// </summary>
    /// <value>The name of the workflow that was executed for context and reporting.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow version that was executed for historical tracking.
    /// </summary>
    /// <value>The version number of the workflow that was executed.</value>
    public long WorkflowVersion { get; set; }

    /// <summary>
    /// Gets or sets the final execution status of the workflow.
    /// </summary>
    /// <value>The final status of the workflow execution (completed, failed, cancelled, etc.).</value>
    public WorkflowExecutionStatus FinalStatus { get; set; }

    /// <summary>
    /// Gets or sets the execution start timestamp for timing analysis.
    /// </summary>
    /// <value>The date and time when the workflow execution started.</value>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the execution completion timestamp for timing analysis.
    /// </summary>
    /// <value>The date and time when the workflow execution completed (if finished).</value>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the total execution duration for performance analysis.
    /// </summary>
    /// <value>The total time taken for the workflow execution from start to completion.</value>
    public TimeSpan? ExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the user identifier who initiated the execution.
    /// </summary>
    /// <value>The identifier of the user who initiated the workflow execution.</value>
    public string InitiatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the execution type categorization for analysis.
    /// </summary>
    /// <value>The type of execution (manual, scheduled, triggered, etc.).</value>
    public ExecutionType ExecutionType { get; set; }

    /// <summary>
    /// Gets or sets the execution priority that was assigned.
    /// </summary>
    /// <value>The priority level assigned to the workflow execution.</value>
    public WorkflowPriority ExecutionPriority { get; set; }

    /// <summary>
    /// Gets or sets the input parameters provided for the execution.
    /// </summary>
    /// <value>The input parameters and values provided for the workflow execution.</value>
    public Dictionary<string, object>? InputParameters { get; set; }

    /// <summary>
    /// Gets or sets the execution results produced by the workflow.
    /// </summary>
    /// <value>The results and output data produced by the workflow execution.</value>
    public Dictionary<string, object>? ExecutionResults { get; set; }

    /// <summary>
    /// Gets or sets the execution configuration used for the workflow.
    /// </summary>
    /// <value>Configuration settings that were applied during workflow execution.</value>
    public ExecutionConfiguration? ExecutionConfiguration { get; set; }

    /// <summary>
    /// Gets or sets the performance metrics for the execution.
    /// </summary>
    /// <value>Comprehensive performance metrics captured during workflow execution.</value>
    public ExecutionPerformanceMetrics? PerformanceMetrics { get; set; }

    /// <summary>
    /// Gets or sets the resource utilization metrics for the execution.
    /// </summary>
    /// <value>Resource utilization metrics including CPU, memory, and network usage.</value>
    public ExecutionResourceMetrics? ResourceMetrics { get; set; }

    /// <summary>
    /// Gets or sets the error information if the execution failed.
    /// </summary>
    /// <value>Detailed error information if the workflow execution encountered failures.</value>
    public ExecutionError? ExecutionError { get; set; }

    /// <summary>
    /// Gets or sets the warning information encountered during execution.
    /// </summary>
    /// <value>Collection of warnings encountered during workflow execution.</value>
    public List<ExecutionWarning>? Warnings { get; set; }

    /// <summary>
    /// Gets or sets the execution progress snapshots for detailed tracking.
    /// </summary>
    /// <value>Collection of progress snapshots captured during workflow execution.</value>
    public List<ExecutionProgressSnapshot>? ProgressSnapshots { get; set; }

    /// <summary>
    /// Gets or sets the node execution details for granular analysis.
    /// </summary>
    /// <value>Detailed execution information for individual workflow nodes.</value>
    public List<NodeExecutionDetail>? NodeExecutionDetails { get; set; }

    /// <summary>
    /// Gets or sets the retry information if retries were attempted.
    /// </summary>
    /// <value>Information about retry attempts made for the workflow execution.</value>
    public ExecutionRetryInfo? RetryInfo { get; set; }

    /// <summary>
    /// Gets or sets the cancellation information if the execution was cancelled.
    /// </summary>
    /// <value>Information about cancellation if the workflow execution was cancelled.</value>
    public ExecutionCancellationInfo? CancellationInfo { get; set; }

    /// <summary>
    /// Gets or sets the checkpoint information for recovery capabilities.
    /// </summary>
    /// <value>Collection of checkpoints created during workflow execution for recovery.</value>
    public List<ExecutionCheckpoint>? Checkpoints { get; set; }

    /// <summary>
    /// Gets or sets the execution environment information for context.
    /// </summary>
    /// <value>Information about the execution environment and infrastructure.</value>
    public ExecutionEnvironmentInfo? EnvironmentInfo { get; set; }

    /// <summary>
    /// Gets or sets the audit trail information for compliance and tracking.
    /// </summary>
    /// <value>Comprehensive audit trail for the workflow execution.</value>
    public AuditTrailInfo? AuditTrail { get; set; }

    /// <summary>
    /// Gets or sets the cost information for financial tracking.
    /// </summary>
    /// <value>Cost information and breakdown for the workflow execution.</value>
    public ExecutionCostInfo? CostInfo { get; set; }

    /// <summary>
    /// Gets or sets the quality metrics for execution assessment.
    /// </summary>
    /// <value>Quality metrics and assessment information for the workflow execution.</value>
    public ExecutionQualityMetrics? QualityMetrics { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with the execution for categorization.
    /// </summary>
    /// <value>Collection of tags associated with the workflow execution.</value>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the tenant identifier for multi-tenant isolation.
    /// </summary>
    /// <value>The tenant identifier for multi-tenant data isolation.</value>
    public string? TenantId { get; set; }

    /// <summary>
    /// Gets or sets custom execution properties for workflow-specific information.
    /// </summary>
    /// <value>Dictionary of custom properties for workflow-specific execution information.</value>
    public Dictionary<string, object>? CustomExecutionProperties { get; set; }
}

/// <summary>
/// Represents configuration settings applied during workflow execution.
/// </summary>
public class ExecutionConfiguration
{
    /// <summary>
    /// Gets or sets the execution timeout that was applied.
    /// </summary>
    /// <value>The timeout duration that was configured for the execution.</value>
    public TimeSpan? ExecutionTimeout { get; set; }

    /// <summary>
    /// Gets or sets the retry policy that was applied.
    /// </summary>
    /// <value>The retry policy configuration used during execution.</value>
    public RetryPolicy? RetryPolicy { get; set; }

    /// <summary>
    /// Gets or sets the concurrency settings that were applied.
    /// </summary>
    /// <value>The concurrency configuration used during execution.</value>
    public ConcurrencySettings? ConcurrencySettings { get; set; }

    /// <summary>
    /// Gets or sets whether detailed logging was enabled.
    /// </summary>
    /// <value>True if detailed logging was enabled during execution; otherwise, false.</value>
    public bool DetailedLoggingEnabled { get; set; }

    /// <summary>
    /// Gets or sets the logging level that was applied.
    /// </summary>
    /// <value>The logging level configuration used during execution.</value>
    public string? LoggingLevel { get; set; }

    /// <summary>
    /// Gets or sets custom configuration properties that were applied.
    /// </summary>
    /// <value>Dictionary of custom configuration properties used during execution.</value>
    public Dictionary<string, object>? CustomConfiguration { get; set; }
}

/// <summary>
/// Represents comprehensive performance metrics for workflow execution analysis.
/// </summary>
public class ExecutionPerformanceMetrics
{
    /// <summary>
    /// Gets or sets the total number of nodes that were executed.
    /// </summary>
    /// <value>Total count of nodes that were executed during the workflow.</value>
    public int TotalNodesExecuted { get; set; }

    /// <summary>
    /// Gets or sets the number of nodes that completed successfully.
    /// </summary>
    /// <value>Count of nodes that completed execution successfully.</value>
    public int NodesCompletedSuccessfully { get; set; }

    /// <summary>
    /// Gets or sets the number of nodes that failed execution.
    /// </summary>
    /// <value>Count of nodes that failed during execution.</value>
    public int NodesFailedExecution { get; set; }

    /// <summary>
    /// Gets or sets the number of nodes that were skipped.
    /// </summary>
    /// <value>Count of nodes that were skipped during conditional execution.</value>
    public int NodesSkipped { get; set; }

    /// <summary>
    /// Gets or sets the average node execution time.
    /// </summary>
    /// <value>Average time taken for node execution across all nodes.</value>
    public TimeSpan AverageNodeExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the fastest node execution time recorded.
    /// </summary>
    /// <value>The shortest execution time recorded for any node in the workflow.</value>
    public TimeSpan FastestNodeExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the slowest node execution time recorded.
    /// </summary>
    /// <value>The longest execution time recorded for any node in the workflow.</value>
    public TimeSpan SlowestNodeExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the node throughput for performance assessment.
    /// </summary>
    /// <value>Average number of nodes processed per unit of time.</value>
    public double NodeThroughput { get; set; }

    /// <summary>
    /// Gets or sets the overall execution efficiency score.
    /// </summary>
    /// <value>Calculated efficiency score based on execution performance metrics.</value>
    public double ExecutionEfficiencyScore { get; set; }

    /// <summary>
    /// Gets or sets the parallelization factor achieved.
    /// </summary>
    /// <value>Measure of parallel execution efficiency achieved during workflow execution.</value>
    public double ParallelizationFactor { get; set; }

    /// <summary>
    /// Gets or sets the queue time before execution started.
    /// </summary>
    /// <value>Time spent waiting in queue before execution began.</value>
    public TimeSpan? QueueTime { get; set; }

    /// <summary>
    /// Gets or sets the initialization time for execution startup.
    /// </summary>
    /// <value>Time taken for execution initialization and startup processes.</value>
    public TimeSpan? InitializationTime { get; set; }

    /// <summary>
    /// Gets or sets the cleanup time after execution completion.
    /// </summary>
    /// <value>Time taken for cleanup operations after execution completion.</value>
    public TimeSpan? CleanupTime { get; set; }
}

/// <summary>
/// Represents resource utilization metrics during workflow execution.
/// </summary>
public class ExecutionResourceMetrics
{
    /// <summary>
    /// Gets or sets the peak CPU usage percentage during execution.
    /// </summary>
    /// <value>Maximum CPU utilization percentage reached during workflow execution.</value>
    public double PeakCpuUsagePercentage { get; set; }

    /// <summary>
    /// Gets or sets the average CPU usage percentage during execution.
    /// </summary>
    /// <value>Average CPU utilization percentage during workflow execution.</value>
    public double AverageCpuUsagePercentage { get; set; }

    /// <summary>
    /// Gets or sets the peak memory usage in bytes during execution.
    /// </summary>
    /// <value>Maximum memory consumption in bytes during workflow execution.</value>
    public long PeakMemoryUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the average memory usage in bytes during execution.
    /// </summary>
    /// <value>Average memory consumption in bytes during workflow execution.</value>
    public long AverageMemoryUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the total network bytes transferred during execution.
    /// </summary>
    /// <value>Total network data transferred in bytes during workflow execution.</value>
    public long TotalNetworkBytesTransferred { get; set; }

    /// <summary>
    /// Gets or sets the peak network bandwidth usage during execution.
    /// </summary>
    /// <value>Maximum network bandwidth utilization during workflow execution.</value>
    public long PeakNetworkBandwidthUsage { get; set; }

    /// <summary>
    /// Gets or sets the total storage space used during execution.
    /// </summary>
    /// <value>Total storage space consumed in bytes during workflow execution.</value>
    public long TotalStorageSpaceUsed { get; set; }

    /// <summary>
    /// Gets or sets the peak storage usage during execution.
    /// </summary>
    /// <value>Maximum storage space consumption during workflow execution.</value>
    public long PeakStorageUsage { get; set; }

    /// <summary>
    /// Gets or sets the resource efficiency score for the execution.
    /// </summary>
    /// <value>Calculated efficiency score based on resource utilization metrics.</value>
    public double ResourceEfficiencyScore { get; set; }

    /// <summary>
    /// Gets or sets detailed resource usage samples captured during execution.
    /// </summary>
    /// <value>Collection of resource usage samples taken at intervals during execution.</value>
    public List<ResourceUsageSample>? ResourceUsageSamples { get; set; }
}

/// <summary>
/// Represents a progress snapshot captured during workflow execution.
/// </summary>
public class ExecutionProgressSnapshot
{
    /// <summary>
    /// Gets or sets the timestamp when the progress snapshot was captured.
    /// </summary>
    /// <value>Date and time when the progress snapshot was recorded.</value>
    public DateTime SnapshotTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the execution progress percentage at snapshot time.
    /// </summary>
    /// <value>Overall execution progress percentage when the snapshot was captured.</value>
    public double ProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets the execution status at snapshot time.
    /// </summary>
    /// <value>Execution status when the progress snapshot was captured.</value>
    public WorkflowExecutionStatus ExecutionStatus { get; set; }

    /// <summary>
    /// Gets or sets the nodes completed at snapshot time.
    /// </summary>
    /// <value>Count of nodes completed when the snapshot was captured.</value>
    public int NodesCompleted { get; set; }

    /// <summary>
    /// Gets or sets the currently executing node at snapshot time.
    /// </summary>
    /// <value>Information about the node being executed when the snapshot was captured.</value>
    public string? CurrentlyExecutingNode { get; set; }

    /// <summary>
    /// Gets or sets the estimated completion time at snapshot time.
    /// </summary>
    /// <value>Estimated completion time calculated when the snapshot was captured.</value>
    public DateTime? EstimatedCompletionTime { get; set; }

    /// <summary>
    /// Gets or sets the resource utilization at snapshot time.
    /// </summary>
    /// <value>Resource utilization metrics when the snapshot was captured.</value>
    public ResourceConsumption? ResourceUtilization { get; set; }
}

/// <summary>
/// Represents detailed execution information for an individual workflow node.
/// </summary>
public class NodeExecutionDetail
{
    /// <summary>
    /// Gets or sets the node identifier for the execution detail.
    /// </summary>
    /// <value>The unique identifier of the node for execution tracking.</value>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node name for identification and reporting.
    /// </summary>
    /// <value>The name of the node for execution detail tracking.</value>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node type for processing categorization.
    /// </summary>
    /// <value>The type of node that was executed.</value>
    public WorkflowNodeType NodeType { get; set; }

    /// <summary>
    /// Gets or sets the final execution status of the node.
    /// </summary>
    /// <value>The final execution status of the individual node.</value>
    public NodeExecutionStatus FinalStatus { get; set; }

    /// <summary>
    /// Gets or sets the node execution start timestamp.
    /// </summary>
    /// <value>The date and time when the node execution started.</value>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the node execution completion timestamp.
    /// </summary>
    /// <value>The date and time when the node execution completed (if finished).</value>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the node execution duration.
    /// </summary>
    /// <value>The total time taken for the node execution.</value>
    public TimeSpan? ExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the node input parameters.
    /// </summary>
    /// <value>Input parameters provided to the node for execution.</value>
    public Dictionary<string, object>? InputParameters { get; set; }

    /// <summary>
    /// Gets or sets the node execution result.
    /// </summary>
    /// <value>The result produced by the node execution (if completed successfully).</value>
    public object? ExecutionResult { get; set; }

    /// <summary>
    /// Gets or sets error information if the node execution failed.
    /// </summary>
    /// <value>Error information if the node execution encountered failures.</value>
    public ExecutionError? ExecutionError { get; set; }

    /// <summary>
    /// Gets or sets the retry attempts made for the node.
    /// </summary>
    /// <value>Count of retry attempts made for the node execution.</value>
    public int RetryAttempts { get; set; }

    /// <summary>
    /// Gets or sets the resource consumption for the node execution.
    /// </summary>
    /// <value>Resource consumption metrics for the individual node execution.</value>
    public ResourceConsumption? ResourceConsumption { get; set; }

    /// <summary>
    /// Gets or sets custom node execution properties.
    /// </summary>
    /// <value>Dictionary of custom properties for node-specific execution information.</value>
    public Dictionary<string, object>? CustomNodeProperties { get; set; }
}

/// <summary>
/// Represents a resource usage sample captured at a specific point in time.
/// </summary>
public class ResourceUsageSample
{
    /// <summary>
    /// Gets or sets the timestamp when the sample was captured.
    /// </summary>
    /// <value>Date and time when the resource usage sample was recorded.</value>
    public DateTime SampleTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the CPU usage percentage at sample time.
    /// </summary>
    /// <value>CPU utilization percentage when the sample was captured.</value>
    public double CpuUsagePercentage { get; set; }

    /// <summary>
    /// Gets or sets the memory usage in bytes at sample time.
    /// </summary>
    /// <value>Memory consumption in bytes when the sample was captured.</value>
    public long MemoryUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the network usage in bytes per second at sample time.
    /// </summary>
    /// <value>Network bandwidth consumption when the sample was captured.</value>
    public long NetworkUsageBytesPerSecond { get; set; }

    /// <summary>
    /// Gets or sets the storage usage in bytes at sample time.
    /// </summary>
    /// <value>Storage space consumption when the sample was captured.</value>
    public long StorageUsageBytes { get; set; }
}
