using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive request DTO for updating existing workflows with validation and change tracking.
/// Provides enterprise-grade workflow modification capabilities with comprehensive validation,
/// change tracking, and audit trail support for workflow evolution and maintenance.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary workflow update DTO for IWorkflowOrchestrationService, enabling comprehensive
/// workflow modifications with node updates, edge restructuring, and metadata changes.
/// Supports partial updates, change tracking, and enterprise workflow management patterns.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Structured workflow update definition with comprehensive validation attributes and change tracking.
/// Optimized for efficient serialization while maintaining rich workflow modification capabilities
/// for enterprise workflow management and evolution scenarios.</para>
/// 
/// <para><strong>Update Patterns:</strong></para>
/// <list type="bullet">
/// <item>Partial updates with selective property modification</item>
/// <item>Complete workflow structure replacement</item>
/// <item>Incremental node and edge modifications</item>
/// <item>Metadata and configuration updates</item>
/// </list>
/// </remarks>
public class UpdateWorkflowRequest
{
    /// <summary>
    /// Gets or sets the workflow identifier for the update operation.
    /// </summary>
    /// <value>The unique identifier of the workflow to be updated.</value>
    [Required]
    public int WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow name for identification and management.
    /// </summary>
    /// <value>A descriptive name that uniquely identifies the workflow within tenant scope.</value>
    [StringLength(200, MinimumLength = 3)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow description for documentation and management.
    /// </summary>
    /// <value>A detailed description explaining the workflow purpose and functionality.</value>
    [StringLength(2000)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow category for organization and filtering.
    /// </summary>
    /// <value>A workflow category enumeration for classification and organization.</value>
    public WorkflowCategory? Category { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow tags for enhanced searchability and organization.
    /// </summary>
    /// <value>A collection of tags for workflow classification and search optimization.</value>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow node definitions with comprehensive configuration.
    /// </summary>
    /// <value>A collection of node definitions that define workflow execution logic.</value>
    public List<WorkflowNodeDefinition>? Nodes { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow edge definitions for node connections and flow control.
    /// </summary>
    /// <value>A collection of edge definitions that define workflow execution flow.</value>
    public List<WorkflowEdgeDefinition>? Edges { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow input variable definitions for parameterization.
    /// </summary>
    /// <value>A collection of input variable definitions for workflow parameterization.</value>
    public List<WorkflowVariableDefinition>? InputVariables { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow configuration settings for execution control.
    /// </summary>
    /// <value>Configuration settings that control workflow execution behavior.</value>
    public WorkflowConfiguration? Configuration { get; set; }

    /// <summary>
    /// Gets or sets whether the workflow should be enabled for execution.
    /// </summary>
    /// <value>True if the workflow should be enabled for execution; otherwise, false; null for no change.</value>
    public bool? IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow priority for execution scheduling.
    /// </summary>
    /// <value>A priority level for workflow execution scheduling and resource allocation.</value>
    public WorkflowPriority? Priority { get; set; }

    /// <summary>
    /// Gets or sets the expected version for optimistic concurrency control.
    /// </summary>
    /// <value>The expected version number to prevent concurrent modification conflicts.</value>
    public long? ExpectedVersion { get; set; }

    /// <summary>
    /// Gets or sets the reason for the workflow update for audit trail documentation.
    /// </summary>
    /// <value>A description of why the workflow is being updated for change tracking.</value>
    [StringLength(500)]
    public string? UpdateReason { get; set; }

    /// <summary>
    /// Gets or sets whether to validate the workflow structure before updating.
    /// </summary>
    /// <value>True to perform comprehensive validation; false to skip validation; default is true.</value>
    public bool ValidateStructure { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to preserve existing execution history during updates.
    /// </summary>
    /// <value>True to maintain execution history; false to clear history; default is true.</value>
    public bool PreserveExecutionHistory { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to create a backup version before updating.
    /// </summary>
    /// <value>True to create a backup version; false to skip backup; default is true.</value>
    public bool CreateBackup { get; set; } = true;

    /// <summary>
    /// Gets or sets specific properties to update for partial update operations.
    /// </summary>
    /// <value>Collection of property names to update, used for selective partial updates.</value>
    public List<string>? PropertiesToUpdate { get; set; }

    /// <summary>
    /// Gets or sets node operations for granular node management during updates.
    /// </summary>
    /// <value>Collection of node operations (add, update, delete) for precise workflow modifications.</value>
    public List<NodeUpdateOperation>? NodeOperations { get; set; }

    /// <summary>
    /// Gets or sets edge operations for granular edge management during updates.
    /// </summary>
    /// <value>Collection of edge operations (add, update, delete) for precise workflow modifications.</value>
    public List<EdgeUpdateOperation>? EdgeOperations { get; set; }

    /// <summary>
    /// Gets or sets custom update properties for specialized update scenarios.
    /// </summary>
    /// <value>Dictionary of custom update properties for workflow-specific modifications.</value>
    public Dictionary<string, object>? CustomUpdateProperties { get; set; }
}

/// <summary>
/// Represents a node update operation for granular workflow modifications.
/// </summary>
public class NodeUpdateOperation
{
    /// <summary>
    /// Gets or sets the operation type for the node modification.
    /// </summary>
    /// <value>The type of operation to perform on the node.</value>
    [Required]
    public NodeOperationType Operation { get; set; }

    /// <summary>
    /// Gets or sets the node identifier for the operation.
    /// </summary>
    /// <value>The unique identifier of the node to be modified.</value>
    public string? NodeId { get; set; }

    /// <summary>
    /// Gets or sets the node definition for add and update operations.
    /// </summary>
    /// <value>The node definition containing updated configuration and properties.</value>
    public WorkflowNodeDefinition? NodeDefinition { get; set; }

    /// <summary>
    /// Gets or sets the operation sequence for ordered execution.
    /// </summary>
    /// <value>Sequence number for controlling operation execution order.</value>
    public int Sequence { get; set; }
}

/// <summary>
/// Represents an edge update operation for granular workflow modifications.
/// </summary>
public class EdgeUpdateOperation
{
    /// <summary>
    /// Gets or sets the operation type for the edge modification.
    /// </summary>
    /// <value>The type of operation to perform on the edge.</value>
    [Required]
    public EdgeOperationType Operation { get; set; }

    /// <summary>
    /// Gets or sets the edge identifier for the operation.
    /// </summary>
    /// <value>The unique identifier of the edge to be modified.</value>
    public string? EdgeId { get; set; }

    /// <summary>
    /// Gets or sets the edge definition for add and update operations.
    /// </summary>
    /// <value>The edge definition containing updated configuration and properties.</value>
    public WorkflowEdgeDefinition? EdgeDefinition { get; set; }

    /// <summary>
    /// Gets or sets the operation sequence for ordered execution.
    /// </summary>
    /// <value>Sequence number for controlling operation execution order.</value>
    public int Sequence { get; set; }
}

/// <summary>
/// Enumeration of node operation types for workflow modifications.
/// </summary>
public enum NodeOperationType
{
    /// <summary>Add a new node to the workflow</summary>
    Add = 0,
    /// <summary>Update an existing node in the workflow</summary>
    Update = 1,
    /// <summary>Delete an existing node from the workflow</summary>
    Delete = 2,
    /// <summary>Move an existing node to a different position</summary>
    Move = 3,
    /// <summary>Duplicate an existing node with new configuration</summary>
    Duplicate = 4
}

/// <summary>
/// Enumeration of edge operation types for workflow modifications.
/// </summary>
public enum EdgeOperationType
{
    /// <summary>Add a new edge to the workflow</summary>
    Add = 0,
    /// <summary>Update an existing edge in the workflow</summary>
    Update = 1,
    /// <summary>Delete an existing edge from the workflow</summary>
    Delete = 2,
    /// <summary>Redirect an existing edge to different nodes</summary>
    Redirect = 3
}
