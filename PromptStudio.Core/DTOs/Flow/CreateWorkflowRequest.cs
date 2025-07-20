using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.Domain.FlowEntities;
using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive request DTO for creating new workflows with validation and metadata.
/// Provides enterprise-grade workflow creation capabilities with comprehensive validation,
/// metadata management, and audit trail support for workflow development workflows.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary workflow creation DTO for IWorkflowOrchestrationService, enabling comprehensive
/// workflow definition with node configuration, edge definitions, and metadata management.
/// Supports validation, audit trails, and enterprise workflow development patterns.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Structured workflow definition with comprehensive validation attributes and metadata.
/// Optimized for efficient serialization while maintaining rich workflow definition capabilities
/// for enterprise workflow development and automation scenarios.</para>
/// 
/// <para><strong>Validation Rules:</strong></para>
/// <list type="bullet">
/// <item>Workflow names must be unique within tenant scope</item>
/// <item>Node definitions must form valid DAG structure</item>
/// <item>All referenced templates and variables must be accessible</item>
/// <item>Edge definitions must reference valid node IDs</item>
/// </list>
/// </remarks>
public class CreateWorkflowRequest
{
    /// <summary>
    /// Gets or sets the workflow name for identification and management.
    /// </summary>
    /// <value>A descriptive name that uniquely identifies the workflow within tenant scope.</value>
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow description for documentation and management.
    /// </summary>
    /// <value>A detailed description explaining the workflow purpose and functionality.</value>
    [StringLength(2000)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the workflow category for organization and filtering.
    /// </summary>
    /// <value>A workflow category enumeration for classification and organization.</value>
    [Required]
    public WorkflowCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the workflow tags for enhanced searchability and organization.
    /// </summary>
    /// <value>A collection of tags for workflow classification and search optimization.</value>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Gets or sets the workflow node definitions with comprehensive configuration.
    /// </summary>
    /// <value>A collection of node definitions that define workflow execution logic.</value>
    [Required]
    [MinLength(1)]
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
    /// Gets or sets the workflow configuration settings for execution control.
    /// </summary>
    /// <value>Configuration settings that control workflow execution behavior.</value>
    public WorkflowConfiguration Configuration { get; set; } = new();

    /// <summary>
    /// Gets or sets whether the workflow should be enabled for execution immediately.
    /// </summary>
    /// <value>True if the workflow should be enabled for execution; otherwise, false.</value>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the workflow priority for execution scheduling.
    /// </summary>
    /// <value>A priority level for workflow execution scheduling and resource allocation.</value>
    public WorkflowPriority Priority { get; set; } = WorkflowPriority.Normal;
}

/// <summary>
/// Represents a workflow node definition for comprehensive workflow orchestration.
/// </summary>
public class WorkflowNodeDefinition
{
    /// <summary>
    /// Gets or sets the unique node identifier within the workflow.
    /// </summary>
    /// <value>A unique identifier for the node within the workflow context.</value>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node name for identification and management.
    /// </summary>
    /// <value>A descriptive name for the workflow node.</value>
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node type for processing and execution logic.
    /// </summary>
    /// <value>The type of node that determines its execution behavior.</value>
    [Required]
    public WorkflowNodeType Type { get; set; }

    /// <summary>
    /// Gets or sets the node configuration with execution parameters.
    /// </summary>
    /// <value>Configuration data specific to the node type and execution requirements.</value>
    public Dictionary<string, object> Configuration { get; set; } = new();

    /// <summary>
    /// Gets or sets the node position for visual workflow representation.
    /// </summary>
    /// <value>Position coordinates for visual workflow design and management.</value>
    public NodePosition? Position { get; set; }

    /// <summary>
    /// Gets or sets whether the node is enabled for execution.
    /// </summary>
    /// <value>True if the node should be executed; otherwise, false.</value>
    public bool IsEnabled { get; set; } = true;
}

/// <summary>
/// Represents a workflow edge definition for node connection and flow control.
/// </summary>
public class WorkflowEdgeDefinition
{
    /// <summary>
    /// Gets or sets the unique edge identifier within the workflow.
    /// </summary>
    /// <value>A unique identifier for the edge within the workflow context.</value>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source node identifier for the connection.
    /// </summary>
    /// <value>The identifier of the node that initiates this connection.</value>
    [Required]
    public string SourceNodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target node identifier for the connection.
    /// </summary>
    /// <value>The identifier of the node that receives this connection.</value>
    [Required]
    public string TargetNodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the edge condition for conditional flow control.
    /// </summary>
    /// <value>Optional condition that must be met for this edge to be traversed.</value>
    public string? Condition { get; set; }

    /// <summary>
    /// Gets or sets the edge priority for multiple outgoing connections.
    /// </summary>
    /// <value>Priority level for edge evaluation when multiple paths are available.</value>
    public int Priority { get; set; } = 0;
}

/// <summary>
/// Represents a workflow variable definition for parameterization and data flow.
/// </summary>
public class WorkflowVariableDefinition
{
    /// <summary>
    /// Gets or sets the variable name for identification and reference.
    /// </summary>
    /// <value>A unique name for the variable within the workflow scope.</value>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the variable type for validation and processing.
    /// </summary>
    /// <value>The data type of the variable for proper validation and handling.</value>
    [Required]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the variable description for documentation.
    /// </summary>
    /// <value>A description explaining the variable purpose and usage.</value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the default value for the variable.
    /// </summary>
    /// <value>Optional default value used when the variable is not provided.</value>
    public object? DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets whether the variable is required for workflow execution.
    /// </summary>
    /// <value>True if the variable must be provided; otherwise, false.</value>
    public bool IsRequired { get; set; }

    /// <summary>
    /// Gets or sets validation rules for the variable value.
    /// </summary>
    /// <value>Optional validation configuration for variable value verification.</value>
    public VariableValidationRules? ValidationRules { get; set; }
}

/// <summary>
/// Represents workflow configuration settings for execution control and optimization.
/// </summary>
public class WorkflowConfiguration
{
    /// <summary>
    /// Gets or sets the maximum execution timeout for the workflow.
    /// </summary>
    /// <value>Maximum time allowed for workflow execution before timeout.</value>
    public TimeSpan? ExecutionTimeout { get; set; }

    /// <summary>
    /// Gets or sets the retry policy for failed workflow executions.
    /// </summary>
    /// <value>Retry configuration for handling workflow execution failures.</value>
    public RetryPolicy? RetryPolicy { get; set; }

    /// <summary>
    /// Gets or sets the concurrency settings for parallel node execution.
    /// </summary>
    /// <value>Configuration for managing concurrent node execution within the workflow.</value>
    public ConcurrencySettings? ConcurrencySettings { get; set; }

    /// <summary>
    /// Gets or sets whether to enable detailed execution logging.
    /// </summary>
    /// <value>True to enable comprehensive logging; otherwise, false.</value>
    public bool EnableDetailedLogging { get; set; }

    /// <summary>
    /// Gets or sets custom configuration properties for workflow execution.
    /// </summary>
    /// <value>Additional configuration properties for workflow customization.</value>
    public Dictionary<string, object> CustomProperties { get; set; } = new();
}

/// <summary>
/// Represents node position information for visual workflow representation.
/// </summary>
public class NodePosition
{
    /// <summary>
    /// Gets or sets the X coordinate for node positioning.
    /// </summary>
    /// <value>The horizontal position coordinate for the node.</value>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate for node positioning.
    /// </summary>
    /// <value>The vertical position coordinate for the node.</value>
    public double Y { get; set; }
}

/// <summary>
/// Represents validation rules for workflow variables.
/// </summary>
public class VariableValidationRules
{
    /// <summary>
    /// Gets or sets the minimum value for numeric variables.
    /// </summary>
    /// <value>Optional minimum value constraint for numeric validation.</value>
    public object? MinValue { get; set; }

    /// <summary>
    /// Gets or sets the maximum value for numeric variables.
    /// </summary>
    /// <value>Optional maximum value constraint for numeric validation.</value>
    public object? MaxValue { get; set; }

    /// <summary>
    /// Gets or sets the regular expression pattern for string validation.
    /// </summary>
    /// <value>Optional regex pattern for string value validation.</value>
    public string? Pattern { get; set; }

    /// <summary>
    /// Gets or sets the allowed values for enumeration validation.
    /// </summary>
    /// <value>Optional list of allowed values for enumeration validation.</value>
    public List<object>? AllowedValues { get; set; }
}

/// <summary>
/// Represents retry policy configuration for workflow execution resilience.
/// </summary>
public class RetryPolicy
{
    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// </summary>
    /// <value>Maximum number of times to retry failed workflow executions.</value>
    public int MaxAttempts { get; set; } = 3;

    /// <summary>
    /// Gets or sets the delay between retry attempts.
    /// </summary>
    /// <value>Time delay between consecutive retry attempts.</value>
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Gets or sets the backoff strategy for retry delays.
    /// </summary>
    /// <value>Strategy for calculating retry delays (exponential, linear, etc.).</value>
    public BackoffStrategy BackoffStrategy { get; set; } = BackoffStrategy.Exponential;
}

/// <summary>
/// Represents concurrency settings for parallel workflow execution.
/// </summary>
public class ConcurrencySettings
{
    /// <summary>
    /// Gets or sets the maximum number of parallel nodes.
    /// </summary>
    /// <value>Maximum number of nodes that can execute concurrently.</value>
    public int MaxParallelNodes { get; set; } = 5;

    /// <summary>
    /// Gets or sets whether to enable parallel execution optimization.
    /// </summary>
    /// <value>True to optimize for parallel execution; otherwise, false.</value>
    public bool EnableParallelOptimization { get; set; } = true;
}

/// <summary>
/// Enumeration of supported workflow categories for organization and management.
/// </summary>
public enum WorkflowCategory
{
    /// <summary>General purpose workflows</summary>
    General = 0,
    /// <summary>Customer engagement and communication workflows</summary>
    CustomerEngagement = 1,
    /// <summary>Content generation and management workflows</summary>
    ContentGeneration = 2,
    /// <summary>Data processing and analysis workflows</summary>
    DataProcessing = 3,
    /// <summary>Integration and automation workflows</summary>
    Integration = 4,
    /// <summary>Testing and validation workflows</summary>
    Testing = 5,
    /// <summary>Monitoring and alerting workflows</summary>
    Monitoring = 6
}

/// <summary>
/// Enumeration of supported workflow node types for processing logic.
/// </summary>
public enum WorkflowNodeType
{
    /// <summary>Template execution node for prompt processing</summary>
    TemplateExecution = 0,
    /// <summary>Condition evaluation node for flow control</summary>
    Condition = 1,
    /// <summary>Loop iteration node for repetitive processing</summary>
    Loop = 2,
    /// <summary>Parallel execution node for concurrent processing</summary>
    Parallel = 3,
    /// <summary>Data transformation node for data manipulation</summary>
    DataTransform = 4,
    /// <summary>External API call node for integration</summary>
    ApiCall = 5,
    /// <summary>Delay node for timing control</summary>
    Delay = 6,
    /// <summary>Variable assignment node for data flow</summary>
    VariableAssignment = 7
}

/// <summary>
/// Enumeration of workflow priority levels for execution scheduling.
/// </summary>
public enum WorkflowPriority
{
    /// <summary>Low priority execution</summary>
    Low = 0,
    /// <summary>Normal priority execution</summary>
    Normal = 1,
    /// <summary>High priority execution</summary>
    High = 2,
    /// <summary>Critical priority execution</summary>
    Critical = 3
}

/// <summary>
/// Enumeration of backoff strategies for retry policy configuration.
/// </summary>
public enum BackoffStrategy
{
    /// <summary>Fixed delay between retries</summary>
    Fixed = 0,
    /// <summary>Linear increase in delay</summary>
    Linear = 1,
    /// <summary>Exponential increase in delay</summary>
    Exponential = 2,
    /// <summary>Random jitter with exponential backoff</summary>
    ExponentialWithJitter = 3
}
