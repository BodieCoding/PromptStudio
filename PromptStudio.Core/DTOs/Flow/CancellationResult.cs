using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive cancellation result DTO for workflow execution termination operations.
/// Provides enterprise-grade cancellation feedback with detailed status information,
/// cleanup results, and resource recovery metrics for workflow execution management.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary cancellation result DTO for IWorkflowOrchestrationService cancellation operations,
/// providing comprehensive feedback about workflow execution termination with cleanup status,
/// resource recovery information, and cancellation impact analysis for enterprise workflow management.</para>
/// 
/// <para><strong>Cancellation Scope:</strong></para>
/// <para>Comprehensive cancellation tracking including execution termination status, resource cleanup,
/// data consistency verification, and recovery recommendations. Designed for enterprise workflow
/// management with detailed cancellation impact analysis and resource management.</para>
/// 
/// <para><strong>Result Categories:</strong></para>
/// <list type="bullet">
/// <item>Cancellation success status and timing information</item>
/// <item>Resource cleanup and recovery status</item>
/// <item>Data consistency and rollback information</item>
/// <item>Impact analysis and recovery recommendations</item>
/// </list>
/// </remarks>
public class CancellationResult : OperationResult
{
    /// <summary>
    /// Gets or sets the execution identifier that was cancelled.
    /// </summary>
    /// <value>The unique identifier of the workflow execution that was cancelled.</value>
    public string ExecutionId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow identifier for the cancelled execution.
    /// </summary>
    /// <value>The unique identifier of the workflow whose execution was cancelled.</value>
    public int WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the workflow name for identification and reporting.
    /// </summary>
    /// <value>The name of the workflow whose execution was cancelled.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cancellation request timestamp for audit and tracking.
    /// </summary>
    /// <value>The date and time when the cancellation was requested.</value>
    public DateTime CancellationRequestedAt { get; set; }

    /// <summary>
    /// Gets or sets the cancellation completion timestamp for performance tracking.
    /// </summary>
    /// <value>The date and time when the cancellation was completed.</value>
    public DateTime CancellationCompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the total time taken to complete the cancellation process.
    /// </summary>
    /// <value>The duration required to complete the workflow execution cancellation.</value>
    public TimeSpan CancellationDuration { get; set; }

    /// <summary>
    /// Gets or sets the cancellation reason for audit and analysis purposes.
    /// </summary>
    /// <value>The reason provided for cancelling the workflow execution.</value>
    public string? CancellationReason { get; set; }

    /// <summary>
    /// Gets or sets the user identifier who requested the cancellation.
    /// </summary>
    /// <value>The identifier of the user who requested the workflow execution cancellation.</value>
    public string? CancelledBy { get; set; }

    /// <summary>
    /// Gets or sets the execution status at the time of cancellation.
    /// </summary>
    /// <value>The execution status when the cancellation was initiated.</value>
    public WorkflowExecutionStatus ExecutionStatusAtCancellation { get; set; }

    /// <summary>
    /// Gets or sets the progress percentage at the time of cancellation.
    /// </summary>
    /// <value>The workflow execution progress percentage when cancellation was initiated.</value>
    public double ProgressPercentageAtCancellation { get; set; }

    /// <summary>
    /// Gets or sets information about nodes that were executing at cancellation time.
    /// </summary>
    /// <value>Collection of nodes that were actively executing when cancellation occurred.</value>
    public List<NodeCancellationInfo>? NodesBeingExecuted { get; set; }

    /// <summary>
    /// Gets or sets information about resource cleanup operations performed.
    /// </summary>
    /// <value>Details about resource cleanup operations performed during cancellation.</value>
    public ResourceCleanupInfo? ResourceCleanup { get; set; }

    /// <summary>
    /// Gets or sets information about data rollback operations performed.
    /// </summary>
    /// <value>Details about data rollback operations performed during cancellation.</value>
    public DataRollbackInfo? DataRollback { get; set; }

    /// <summary>
    /// Gets or sets the partial results that were available at cancellation time.
    /// </summary>
    /// <value>Partial workflow results that were available when cancellation occurred.</value>
    public Dictionary<string, object>? PartialResults { get; set; }

    /// <summary>
    /// Gets or sets information about any cleanup errors encountered.
    /// </summary>
    /// <value>Collection of errors encountered during the cancellation cleanup process.</value>
    public List<CleanupError>? CleanupErrors { get; set; }

    /// <summary>
    /// Gets or sets warnings generated during the cancellation process.
    /// </summary>
    /// <value>Collection of warnings encountered during workflow execution cancellation.</value>
    public new List<string>? Warnings { get; set; }

    /// <summary>
    /// Gets or sets recommendations for handling the cancelled execution.
    /// </summary>
    /// <value>Recommendations for follow-up actions after workflow execution cancellation.</value>
    public List<PostCancellationRecommendation>? Recommendations { get; set; }

    /// <summary>
    /// Gets or sets the impact analysis for the cancelled execution.
    /// </summary>
    /// <value>Analysis of the impact caused by cancelling the workflow execution.</value>
    public CancellationImpactAnalysis? ImpactAnalysis { get; set; }

    /// <summary>
    /// Gets or sets recovery information for potential execution resumption.
    /// </summary>
    /// <value>Information about potential recovery options for the cancelled execution.</value>
    public CancellationRecoveryInfo? RecoveryInfo { get; set; }

    /// <summary>
    /// Gets or sets custom cancellation properties for workflow-specific information.
    /// </summary>
    /// <value>Dictionary of custom properties for workflow-specific cancellation information.</value>
    public Dictionary<string, object>? CustomCancellationProperties { get; set; }
}

/// <summary>
/// Represents information about a node that was cancelled during workflow execution.
/// </summary>
public class NodeCancellationInfo
{
    /// <summary>
    /// Gets or sets the node identifier that was cancelled.
    /// </summary>
    /// <value>The unique identifier of the node that was cancelled.</value>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node name for identification and reporting.
    /// </summary>
    /// <value>The name of the node that was cancelled.</value>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node status at the time of cancellation.
    /// </summary>
    /// <value>The execution status of the node when cancellation occurred.</value>
    public NodeExecutionStatus StatusAtCancellation { get; set; }

    /// <summary>
    /// Gets or sets the time when the node execution started.
    /// </summary>
    /// <value>The date and time when the cancelled node execution started.</value>
    public DateTime NodeStartedAt { get; set; }

    /// <summary>
    /// Gets or sets the duration the node had been executing before cancellation.
    /// </summary>
    /// <value>The time the node had been executing before cancellation occurred.</value>
    public TimeSpan ExecutionDurationBeforeCancellation { get; set; }

    /// <summary>
    /// Gets or sets whether the node was successfully cancelled.
    /// </summary>
    /// <value>True if the node was successfully cancelled; otherwise, false.</value>
    public bool WasSuccessfullyCancelled { get; set; }

    /// <summary>
    /// Gets or sets any partial results from the cancelled node.
    /// </summary>
    /// <value>Partial results that were available from the node before cancellation.</value>
    public object? PartialResult { get; set; }

    /// <summary>
    /// Gets or sets any errors encountered while cancelling the node.
    /// </summary>
    /// <value>Error information if the node cancellation encountered issues.</value>
    public string? CancellationError { get; set; }
}

/// <summary>
/// Represents information about resource cleanup operations during cancellation.
/// </summary>
public class ResourceCleanupInfo
{
    /// <summary>
    /// Gets or sets whether resource cleanup was successful.
    /// </summary>
    /// <value>True if all resource cleanup operations were successful; otherwise, false.</value>
    public bool CleanupSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the time taken to complete resource cleanup.
    /// </summary>
    /// <value>The duration required to complete resource cleanup operations.</value>
    public TimeSpan CleanupDuration { get; set; }

    /// <summary>
    /// Gets or sets details about memory cleanup operations.
    /// </summary>
    /// <value>Information about memory resource cleanup during cancellation.</value>
    public ResourceCleanupDetail? MemoryCleanup { get; set; }

    /// <summary>
    /// Gets or sets details about file system cleanup operations.
    /// </summary>
    /// <value>Information about file system resource cleanup during cancellation.</value>
    public ResourceCleanupDetail? FileSystemCleanup { get; set; }

    /// <summary>
    /// Gets or sets details about network connection cleanup operations.
    /// </summary>
    /// <value>Information about network resource cleanup during cancellation.</value>
    public ResourceCleanupDetail? NetworkCleanup { get; set; }

    /// <summary>
    /// Gets or sets details about database connection cleanup operations.
    /// </summary>
    /// <value>Information about database resource cleanup during cancellation.</value>
    public ResourceCleanupDetail? DatabaseCleanup { get; set; }

    /// <summary>
    /// Gets or sets the total number of resources that required cleanup.
    /// </summary>
    /// <value>Total count of resources that required cleanup during cancellation.</value>
    public int TotalResourcesRequiringCleanup { get; set; }

    /// <summary>
    /// Gets or sets the number of resources that were successfully cleaned up.
    /// </summary>
    /// <value>Count of resources that were successfully cleaned up during cancellation.</value>
    public int ResourcesSuccessfullyCleanedUp { get; set; }

    /// <summary>
    /// Gets or sets the number of resources that failed cleanup.
    /// </summary>
    /// <value>Count of resources that failed cleanup operations during cancellation.</value>
    public int ResourcesFailedCleanup { get; set; }
}

/// <summary>
/// Represents detailed information about a specific resource cleanup operation.
/// </summary>
public class ResourceCleanupDetail
{
    /// <summary>
    /// Gets or sets the type of resource that was cleaned up.
    /// </summary>
    /// <value>The type or category of resource that underwent cleanup.</value>
    public string ResourceType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the cleanup was successful for this resource type.
    /// </summary>
    /// <value>True if cleanup was successful for this resource type; otherwise, false.</value>
    public bool CleanupSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the number of individual resources cleaned up.
    /// </summary>
    /// <value>Count of individual resources of this type that were cleaned up.</value>
    public int ResourceCount { get; set; }

    /// <summary>
    /// Gets or sets the time taken for cleanup of this resource type.
    /// </summary>
    /// <value>Duration required to clean up resources of this type.</value>
    public TimeSpan CleanupDuration { get; set; }

    /// <summary>
    /// Gets or sets any errors encountered during cleanup of this resource type.
    /// </summary>
    /// <value>Error information for cleanup operations of this resource type.</value>
    public string? CleanupError { get; set; }
}

/// <summary>
/// Represents information about data rollback operations during cancellation.
/// </summary>
public class DataRollbackInfo
{
    /// <summary>
    /// Gets or sets whether data rollback was performed.
    /// </summary>
    /// <value>True if data rollback operations were performed; otherwise, false.</value>
    public bool RollbackPerformed { get; set; }

    /// <summary>
    /// Gets or sets whether the data rollback was successful.
    /// </summary>
    /// <value>True if all data rollback operations were successful; otherwise, false.</value>
    public bool RollbackSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the time taken to complete data rollback operations.
    /// </summary>
    /// <value>The duration required to complete data rollback operations.</value>
    public TimeSpan RollbackDuration { get; set; }

    /// <summary>
    /// Gets or sets the number of data operations that were rolled back.
    /// </summary>
    /// <value>Count of data operations that were successfully rolled back.</value>
    public int DataOperationsRolledBack { get; set; }

    /// <summary>
    /// Gets or sets the number of data operations that failed rollback.
    /// </summary>
    /// <value>Count of data operations that could not be rolled back.</value>
    public int DataOperationsFailedRollback { get; set; }

    /// <summary>
    /// Gets or sets detailed rollback information for specific data stores.
    /// </summary>
    /// <value>Collection of detailed rollback information for individual data stores.</value>
    public List<DataStoreRollbackDetail>? DataStoreRollbacks { get; set; }

    /// <summary>
    /// Gets or sets any errors encountered during data rollback operations.
    /// </summary>
    /// <value>Collection of errors encountered during data rollback operations.</value>
    public List<string>? RollbackErrors { get; set; }
}

/// <summary>
/// Represents detailed rollback information for a specific data store.
/// </summary>
public class DataStoreRollbackDetail
{
    /// <summary>
    /// Gets or sets the name or identifier of the data store.
    /// </summary>
    /// <value>The name or identifier of the data store that underwent rollback.</value>
    public string DataStoreName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of data store (database, file, cache, etc.).
    /// </summary>
    /// <value>The type or category of the data store.</value>
    public string DataStoreType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether rollback was successful for this data store.
    /// </summary>
    /// <value>True if rollback was successful for this data store; otherwise, false.</value>
    public bool RollbackSuccessful { get; set; }

    /// <summary>
    /// Gets or sets the number of records or items that were rolled back.
    /// </summary>
    /// <value>Count of records or items that were rolled back in this data store.</value>
    public long RecordsRolledBack { get; set; }

    /// <summary>
    /// Gets or sets the time taken for rollback of this data store.
    /// </summary>
    /// <value>Duration required to complete rollback for this data store.</value>
    public TimeSpan RollbackDuration { get; set; }

    /// <summary>
    /// Gets or sets any errors encountered during rollback of this data store.
    /// </summary>
    /// <value>Error information for rollback operations of this data store.</value>
    public string? RollbackError { get; set; }
}

/// <summary>
/// Represents an error encountered during cleanup operations.
/// </summary>
public class CleanupError
{
    /// <summary>
    /// Gets or sets the error code for categorization and handling.
    /// </summary>
    /// <value>A code identifying the type of cleanup error encountered.</value>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error message describing the cleanup issue.
    /// </summary>
    /// <value>A descriptive message explaining the cleanup error.</value>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the component or resource that encountered the cleanup error.
    /// </summary>
    /// <value>The component or resource where the cleanup error occurred.</value>
    public string? Component { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the cleanup error occurred.
    /// </summary>
    /// <value>The date and time when the cleanup error was encountered.</value>
    public DateTime ErrorTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the severity level of the cleanup error.
    /// </summary>
    /// <value>The severity level indicating the impact of the cleanup error.</value>
    public ErrorSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets additional details about the cleanup error.
    /// </summary>
    /// <value>Dictionary of additional properties providing more context about the error.</value>
    public Dictionary<string, object>? AdditionalDetails { get; set; }
}

/// <summary>
/// Represents a recommendation for post-cancellation actions.
/// </summary>
public class PostCancellationRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation type for categorization.
    /// </summary>
    /// <value>The category or type of the post-cancellation recommendation.</value>
    public PostCancellationRecommendationType Type { get; set; }

    /// <summary>
    /// Gets or sets the recommendation title for identification.
    /// </summary>
    /// <value>A brief title describing the post-cancellation recommendation.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation description with detailed guidance.
    /// </summary>
    /// <value>Detailed description of the recommended post-cancellation action.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority level of the recommendation.
    /// </summary>
    /// <value>Priority level indicating the importance of following this recommendation.</value>
    public RecommendationPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the recommended timeframe for implementing the recommendation.
    /// </summary>
    /// <value>Suggested timeframe for implementing the post-cancellation recommendation.</value>
    public string? RecommendedTimeframe { get; set; }

    /// <summary>
    /// Gets or sets specific action steps for the recommendation.
    /// </summary>
    /// <value>Collection of specific steps to implement the recommendation.</value>
    public List<string>? ActionSteps { get; set; }
}

/// <summary>
/// Represents impact analysis information for workflow cancellation.
/// </summary>
public class CancellationImpactAnalysis
{
    /// <summary>
    /// Gets or sets the overall impact severity of the cancellation.
    /// </summary>
    /// <value>Severity level representing the overall impact of the workflow cancellation.</value>
    public ImpactSeverity OverallImpactSeverity { get; set; }

    /// <summary>
    /// Gets or sets the estimated time savings from cancelling the execution.
    /// </summary>
    /// <value>Estimated time that was saved by cancelling the workflow execution.</value>
    public TimeSpan? EstimatedTimeSavings { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost savings from cancelling the execution.
    /// </summary>
    /// <value>Estimated cost that was saved by cancelling the workflow execution.</value>
    public decimal? EstimatedCostSavings { get; set; }

    /// <summary>
    /// Gets or sets information about affected downstream processes.
    /// </summary>
    /// <value>Collection of downstream processes that may be affected by the cancellation.</value>
    public List<AffectedProcess>? AffectedDownstreamProcesses { get; set; }

    /// <summary>
    /// Gets or sets information about data consistency implications.
    /// </summary>
    /// <value>Analysis of data consistency implications resulting from the cancellation.</value>
    public DataConsistencyImpact? DataConsistencyImpact { get; set; }

    /// <summary>
    /// Gets or sets information about resource recovery implications.
    /// </summary>
    /// <value>Analysis of resource recovery implications resulting from the cancellation.</value>
    public ResourceRecoveryImpact? ResourceRecoveryImpact { get; set; }
}

/// <summary>
/// Represents recovery information for potentially resuming cancelled execution.
/// </summary>
public class CancellationRecoveryInfo
{
    /// <summary>
    /// Gets or sets whether the cancelled execution can be resumed.
    /// </summary>
    /// <value>True if the workflow execution can potentially be resumed; otherwise, false.</value>
    public bool CanBeResumed { get; set; }

    /// <summary>
    /// Gets or sets whether the workflow can be restarted from the beginning.
    /// </summary>
    /// <value>True if the workflow can be restarted from the beginning; otherwise, false.</value>
    public bool CanBeRestarted { get; set; }

    /// <summary>
    /// Gets or sets checkpoint information available for resumption.
    /// </summary>
    /// <value>Information about available checkpoints that could be used for resumption.</value>
    public List<AvailableCheckpoint>? AvailableCheckpoints { get; set; }

    /// <summary>
    /// Gets or sets the estimated effort required for recovery operations.
    /// </summary>
    /// <value>Estimated time and effort required to recover from the cancellation.</value>
    public RecoveryEffortEstimate? RecoveryEffortEstimate { get; set; }

    /// <summary>
    /// Gets or sets recommendations for recovery actions.
    /// </summary>
    /// <value>Collection of recommendations for recovering from the workflow cancellation.</value>
    public List<RecoveryRecommendation>? RecoveryRecommendations { get; set; }

    /// <summary>
    /// Gets or sets the expiration time for recovery options.
    /// </summary>
    /// <value>Date and time when recovery options expire and are no longer available.</value>
    public DateTime? RecoveryOptionsExpireAt { get; set; }
}

/// <summary>
/// Represents an affected downstream process due to workflow cancellation.
/// </summary>
public class AffectedProcess
{
    /// <summary>
    /// Gets or sets the process identifier.
    /// </summary>
    public string ProcessId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the process name.
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the impact level on this process.
    /// </summary>
    public ImpactSeverity ImpactLevel { get; set; }

    /// <summary>
    /// Gets or sets the description of how this process is affected.
    /// </summary>
    public string? ImpactDescription { get; set; }
}

/// <summary>
/// Represents data consistency impact information.
/// </summary>
public class DataConsistencyImpact
{
    /// <summary>
    /// Gets or sets whether data consistency is compromised.
    /// </summary>
    public bool IsCompromised { get; set; }

    /// <summary>
    /// Gets or sets the severity of data consistency impact.
    /// </summary>
    public ImpactSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the description of data consistency issues.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the affected data sources.
    /// </summary>
    public List<string>? AffectedDataSources { get; set; }
}

/// <summary>
/// Represents resource recovery impact information.
/// </summary>
public class ResourceRecoveryImpact
{
    /// <summary>
    /// Gets or sets whether resource recovery is needed.
    /// </summary>
    public bool RecoveryNeeded { get; set; }

    /// <summary>
    /// Gets or sets the estimated recovery time.
    /// </summary>
    public TimeSpan? EstimatedRecoveryTime { get; set; }

    /// <summary>
    /// Gets or sets the affected resource types.
    /// </summary>
    public List<string>? AffectedResourceTypes { get; set; }
}

/// <summary>
/// Represents an available checkpoint for workflow resumption.
/// </summary>
public class AvailableCheckpoint
{
    /// <summary>
    /// Gets or sets the checkpoint identifier.
    /// </summary>
    public string CheckpointId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the checkpoint timestamp.
    /// </summary>
    public DateTime CheckpointTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the checkpoint description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the completion percentage at this checkpoint.
    /// </summary>
    public double CompletionPercentage { get; set; }
}

/// <summary>
/// Represents recovery effort estimation.
/// </summary>
public class RecoveryEffortEstimate
{
    /// <summary>
    /// Gets or sets the estimated time required for recovery.
    /// </summary>
    public TimeSpan EstimatedTime { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost for recovery.
    /// </summary>
    public decimal? EstimatedCost { get; set; }

    /// <summary>
    /// Gets or sets the complexity level of recovery.
    /// </summary>
    public string? ComplexityLevel { get; set; }
}

/// <summary>
/// Represents a recovery recommendation.
/// </summary>
public class RecoveryRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority of this recommendation.
    /// </summary>
    public RecommendationPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets specific steps for this recommendation.
    /// </summary>
    public List<string>? Steps { get; set; }
}

/// <summary>
/// Enumeration of post-cancellation recommendation types.
/// </summary>
public enum PostCancellationRecommendationType
{
    /// <summary>Data cleanup recommendation</summary>
    DataCleanup = 0,
    /// <summary>Resource recovery recommendation</summary>
    ResourceRecovery = 1,
    /// <summary>Workflow restart recommendation</summary>
    WorkflowRestart = 2,
    /// <summary>Monitoring and alerting recommendation</summary>
    MonitoringAndAlerting = 3,
    /// <summary>Documentation and audit recommendation</summary>
    DocumentationAndAudit = 4,
    /// <summary>Process improvement recommendation</summary>
    ProcessImprovement = 5
}


/// <summary>
/// Enumeration of impact severity levels for cancellation analysis.
/// </summary>
public enum ImpactSeverity
{
    /// <summary>Minimal impact</summary>
    Minimal = 0,
    /// <summary>Low impact</summary>
    Low = 1,
    /// <summary>Medium impact</summary>
    Medium = 2,
    /// <summary>High impact</summary>
    High = 3,
    /// <summary>Critical impact</summary>
    Critical = 4
}
