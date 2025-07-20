using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.Domain.FlowEntities;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive workflow definition DTO with complete structure and metadata information.
/// Provides enterprise-grade workflow representation with comprehensive node definitions,
/// edge configurations, and execution metadata for workflow management and orchestration.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary workflow representation DTO for IWorkflowOrchestrationService, providing
/// complete workflow structure with nodes, edges, variables, and configuration for
/// enterprise workflow execution, analysis, and management operations.</para>
/// 
/// <para><strong>Data Structure:</strong></para>
/// <para>Complete workflow definition with comprehensive metadata, execution statistics,
/// and structural information. Optimized for efficient serialization while maintaining
/// rich workflow representation capabilities for enterprise workflow management scenarios.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Workflow retrieval and display operations</item>
/// <item>Workflow analysis and validation processes</item>
/// <item>Workflow export and import operations</item>
/// <item>Workflow execution planning and optimization</item>
/// </list>
/// </remarks>
public class WorkflowDefinition
{
    /// <summary>
    /// Gets or sets the unique workflow identifier.
    /// </summary>
    /// <value>The unique identifier for the workflow within the system.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the workflow name for identification and management.
    /// </summary>
    /// <value>A descriptive name that uniquely identifies the workflow within tenant scope.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow description for documentation and management.
    /// </summary>
    /// <value>A detailed description explaining the workflow purpose and functionality.</value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the workflow category for organization and filtering.
    /// </summary>
    /// <value>A workflow category enumeration for classification and organization.</value>
    public WorkflowCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the current workflow status for state management.
    /// </summary>
    /// <value>The current status of the workflow (active, paused, disabled, etc.).</value>
    public WorkflowStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the workflow priority for execution scheduling.
    /// </summary>
    /// <value>A priority level for workflow execution scheduling and resource allocation.</value>
    public WorkflowPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the workflow tags for enhanced searchability and organization.
    /// </summary>
    /// <value>A collection of tags for workflow classification and search optimization.</value>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Gets or sets the workflow node definitions with comprehensive configuration.
    /// </summary>
    /// <value>A collection of node definitions that define workflow execution logic.</value>
    public List<WorkflowNodeDefinition> Nodes { get; set; } = new();

    /// <summary>
    /// Gets or sets the workflow edge definitions for node connections and flow control.
    /// </summary>
    /// <value>A collection of edge definitions that define workflow execution flow.</value>
    public List<WorkflowEdgeDefinition> Edges { get; set; } = new();

    /// <summary>
    /// Gets or sets the workflow input variable definitions for parameterization.
    /// </summary>
    /// <value>A collection of input variable definitions for workflow parameterization.</value>
    public List<WorkflowVariableDefinition> InputVariables { get; set; } = new();

    /// <summary>
    /// Gets or sets the workflow output variable definitions for result handling.
    /// </summary>
    /// <value>A collection of output variable definitions for workflow result management.</value>
    public List<WorkflowVariableDefinition> OutputVariables { get; set; } = new();

    /// <summary>
    /// Gets or sets the workflow configuration settings for execution control.
    /// </summary>
    /// <value>Configuration settings that control workflow execution behavior.</value>
    public WorkflowConfiguration Configuration { get; set; } = new();

    /// <summary>
    /// Gets or sets whether the workflow is enabled for execution.
    /// </summary>
    /// <value>True if the workflow is available for execution; otherwise, false.</value>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets whether the workflow is a template for creating new workflows.
    /// </summary>
    /// <value>True if the workflow serves as a template; otherwise, false.</value>
    public bool IsTemplate { get; set; }

    /// <summary>
    /// Gets or sets the workflow version for change tracking and management.
    /// </summary>
    /// <value>The current version number of the workflow for change tracking.</value>
    public long Version { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp for audit and tracking.
    /// </summary>
    /// <value>The date and time when the workflow was created.</value>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the creation user identifier for audit and tracking.
    /// </summary>
    /// <value>The identifier of the user who created the workflow.</value>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last modification timestamp for audit and tracking.
    /// </summary>
    /// <value>The date and time when the workflow was last modified.</value>
    public DateTime ModifiedAt { get; set; }

    /// <summary>
    /// Gets or sets the last modification user identifier for audit and tracking.
    /// </summary>
    /// <value>The identifier of the user who last modified the workflow.</value>
    public string ModifiedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the execution statistics for the workflow.
    /// </summary>
    /// <value>Statistical information about workflow executions and performance.</value>
    public WorkflowExecutionStatistics? ExecutionStatistics { get; set; }

    /// <summary>
    /// Gets or sets the validation status of the workflow structure.
    /// </summary>
    /// <value>Information about the structural validation and integrity of the workflow.</value>
    public WorkflowValidationStatus? ValidationStatus { get; set; }

    /// <summary>
    /// Gets or sets the estimated execution metrics for the workflow.
    /// </summary>
    /// <value>Estimated performance and resource usage metrics for workflow execution.</value>
    public WorkflowExecutionEstimates? ExecutionEstimates { get; set; }

    /// <summary>
    /// Gets or sets the dependencies information for the workflow.
    /// </summary>
    /// <value>Information about external dependencies and requirements for workflow execution.</value>
    public WorkflowDependencies? Dependencies { get; set; }

    /// <summary>
    /// Gets or sets the tenant identifier for multi-tenant isolation.
    /// </summary>
    /// <value>The tenant identifier for multi-tenant data isolation.</value>
    public string? TenantId { get; set; }

    /// <summary>
    /// Gets or sets custom properties for workflow-specific metadata.
    /// </summary>
    /// <value>Dictionary of custom properties for workflow-specific information.</value>
    public Dictionary<string, object>? CustomProperties { get; set; }
}

/// <summary>
/// Represents execution statistics for workflow performance tracking.
/// </summary>
public class WorkflowExecutionStatistics
{
    /// <summary>
    /// Gets or sets the total number of workflow executions.
    /// </summary>
    /// <value>Total count of workflow execution attempts.</value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of successful workflow executions.
    /// </summary>
    /// <value>Count of successfully completed workflow executions.</value>
    public long SuccessfulExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of failed workflow executions.
    /// </summary>
    /// <value>Count of failed workflow executions.</value>
    public long FailedExecutions { get; set; }

    /// <summary>
    /// Gets or sets the success rate percentage for workflow executions.
    /// </summary>
    /// <value>Percentage of successful executions relative to total executions.</value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution duration for the workflow.
    /// </summary>
    /// <value>Average time taken for workflow execution completion.</value>
    public TimeSpan AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the minimum execution duration recorded.
    /// </summary>
    /// <value>Shortest execution duration recorded for the workflow.</value>
    public TimeSpan MinExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the maximum execution duration recorded.
    /// </summary>
    /// <value>Longest execution duration recorded for the workflow.</value>
    public TimeSpan MaxExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last successful execution.
    /// </summary>
    /// <value>Date and time of the most recent successful workflow execution.</value>
    public DateTime? LastSuccessfulExecution { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last failed execution.
    /// </summary>
    /// <value>Date and time of the most recent failed workflow execution.</value>
    public DateTime? LastFailedExecution { get; set; }

    /// <summary>
    /// Gets or sets the average resource usage during workflow executions.
    /// </summary>
    /// <value>Statistical information about resource consumption during executions.</value>
    public ResourceUsageStatistics? AverageResourceUsage { get; set; }
}

/// <summary>
/// Represents validation status information for workflow structure integrity.
/// </summary>
public class WorkflowValidationStatus
{
    /// <summary>
    /// Gets or sets whether the workflow structure is valid.
    /// </summary>
    /// <value>True if the workflow structure is valid; otherwise, false.</value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last validation check.
    /// </summary>
    /// <value>Date and time when the workflow was last validated.</value>
    public DateTime LastValidatedAt { get; set; }

    /// <summary>
    /// Gets or sets validation errors found in the workflow structure.
    /// </summary>
    /// <value>Collection of validation errors that prevent workflow execution.</value>
    public List<ValidationError>? ValidationErrors { get; set; }

    /// <summary>
    /// Gets or sets validation warnings for the workflow structure.
    /// </summary>
    /// <value>Collection of validation warnings that may affect workflow performance.</value>
    public List<ValidationWarning>? ValidationWarnings { get; set; }

    /// <summary>
    /// Gets or sets the validation score for the workflow quality.
    /// </summary>
    /// <value>Numeric score representing the overall quality and validity of the workflow.</value>
    public double ValidationScore { get; set; }
}

/// <summary>
/// Represents execution estimates for workflow performance planning.
/// </summary>
public class WorkflowExecutionEstimates
{
    /// <summary>
    /// Gets or sets the estimated execution duration for the workflow.
    /// </summary>
    /// <value>Estimated time required for workflow execution completion.</value>
    public TimeSpan EstimatedDuration { get; set; }

    /// <summary>
    /// Gets or sets the estimated resource requirements for execution.
    /// </summary>
    /// <value>Estimated computational and memory resources required for execution.</value>
    public ResourceRequirements? EstimatedResourceRequirements { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost for workflow execution.
    /// </summary>
    /// <value>Estimated financial cost for workflow execution including resource usage.</value>
    public decimal EstimatedCost { get; set; }

    /// <summary>
    /// Gets or sets the confidence level for the execution estimates.
    /// </summary>
    /// <value>Confidence percentage for the accuracy of the execution estimates.</value>
    public double ConfidenceLevel { get; set; }
}

/// <summary>
/// Represents dependency information for workflow execution requirements.
/// </summary>
public class WorkflowDependencies
{
    /// <summary>
    /// Gets or sets template dependencies required for workflow execution.
    /// </summary>
    /// <value>Collection of template identifiers that the workflow depends on.</value>
    public List<int>? TemplateDependencies { get; set; }

    /// <summary>
    /// Gets or sets external service dependencies for workflow execution.
    /// </summary>
    /// <value>Collection of external services that the workflow requires for execution.</value>
    public List<ExternalServiceDependency>? ExternalServiceDependencies { get; set; }

    /// <summary>
    /// Gets or sets data source dependencies for workflow execution.
    /// </summary>
    /// <value>Collection of data sources that the workflow requires for execution.</value>
    public List<DataSourceDependency>? DataSourceDependencies { get; set; }

    /// <summary>
    /// Gets or sets library and package dependencies for workflow execution.
    /// </summary>
    /// <value>Collection of libraries and packages required for workflow execution.</value>
    public List<LibraryDependency>? LibraryDependencies { get; set; }
}

/// <summary>
/// Represents resource usage statistics for workflow execution monitoring.
/// </summary>
public class ResourceUsageStatistics
{
    /// <summary>
    /// Gets or sets the average CPU usage during workflow execution.
    /// </summary>
    /// <value>Average CPU utilization percentage during workflow executions.</value>
    public double AverageCpuUsage { get; set; }

    /// <summary>
    /// Gets or sets the average memory usage during workflow execution.
    /// </summary>
    /// <value>Average memory consumption in bytes during workflow executions.</value>
    public long AverageMemoryUsage { get; set; }

    /// <summary>
    /// Gets or sets the average network usage during workflow execution.
    /// </summary>
    /// <value>Average network bandwidth consumption during workflow executions.</value>
    public long AverageNetworkUsage { get; set; }

    /// <summary>
    /// Gets or sets the average storage usage during workflow execution.
    /// </summary>
    /// <value>Average storage space consumption during workflow executions.</value>
    public long AverageStorageUsage { get; set; }
}
