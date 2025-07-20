using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.Domain.FlowEntities;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive result DTO for workflow update operations with detailed change tracking.
/// Provides enterprise-grade workflow modification results with comprehensive change summaries,
/// validation feedback, and audit trail information for workflow evolution and maintenance.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary result DTO for IWorkflowOrchestrationService update operations, providing
/// comprehensive feedback about workflow modifications with change summaries, validation results,
/// and audit trail information for enterprise workflow management and evolution tracking.</para>
/// 
/// <para><strong>Change Tracking:</strong></para>
/// <para>Detailed change tracking with before/after comparisons, property-level modifications,
/// and comprehensive audit information. Optimized for enterprise change management workflows
/// and compliance requirements with detailed modification documentation.</para>
/// 
/// <para><strong>Validation Feedback:</strong></para>
/// <list type="bullet">
/// <item>Comprehensive validation results with detailed error information</item>
/// <item>Warning notifications for potential issues or recommendations</item>
/// <item>Success confirmations with change summaries</item>
/// <item>Rollback information for failed update operations</item>
/// </list>
/// </remarks>
public class WorkflowUpdateResult : OperationResult
{
    /// <summary>
    /// Gets or sets the updated workflow identifier.
    /// </summary>
    /// <value>The unique identifier of the workflow that was updated.</value>
    public int WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the workflow version after the update operation.
    /// </summary>
    /// <value>The new version number assigned to the workflow after successful update.</value>
    public long NewVersion { get; set; }

    /// <summary>
    /// Gets or sets the workflow version before the update operation.
    /// </summary>
    /// <value>The previous version number of the workflow before the update.</value>
    public long PreviousVersion { get; set; }

    /// <summary>
    /// Gets or sets the updated workflow name for reference and management.
    /// </summary>
    /// <value>The current name of the workflow after the update operation.</value>
    public string? WorkflowName { get; set; }

    /// <summary>
    /// Gets or sets the comprehensive change summary for the update operation.
    /// </summary>
    /// <value>Detailed summary of all changes made during the workflow update.</value>
    public WorkflowChangeSummary? ChangeSummary { get; set; }

    /// <summary>
    /// Gets or sets the validation results for the updated workflow structure.
    /// </summary>
    /// <value>Collection of validation results indicating structural integrity and compliance.</value>
    public List<ValidationResult>? ValidationResults { get; set; }

    /// <summary>
    /// Gets or sets warnings generated during the update operation.
    /// </summary>
    /// <value>Collection of warning messages for potential issues or recommendations.</value>
    public List<string>? Warnings { get; set; }

    /// <summary>
    /// Gets or sets the backup version information created during the update.
    /// </summary>
    /// <value>Information about the backup version created before the update operation.</value>
    public BackupVersionInfo? BackupVersion { get; set; }

    /// <summary>
    /// Gets or sets the execution impact analysis for the updated workflow.
    /// </summary>
    /// <value>Analysis of how the updates might affect workflow execution and performance.</value>
    public ExecutionImpactAnalysis? ExecutionImpact { get; set; }

    /// <summary>
    /// Gets or sets the affected workflow executions due to the update.
    /// </summary>
    /// <value>Information about ongoing or scheduled executions affected by the workflow changes.</value>
    public List<AffectedExecution>? AffectedExecutions { get; set; }

    /// <summary>
    /// Gets or sets the rollback information if the update operation was reversed.
    /// </summary>
    /// <value>Information about rollback operations performed due to update failures.</value>
    public RollbackInfo? RollbackInfo { get; set; }

    /// <summary>
    /// Gets or sets the performance metrics for the update operation.
    /// </summary>
    /// <value>Performance metrics and timing information for the update process.</value>
    public UpdatePerformanceMetrics? PerformanceMetrics { get; set; }

    /// <summary>
    /// Gets or sets the audit trail information for compliance and tracking.
    /// </summary>
    /// <value>Comprehensive audit trail for the workflow update operation.</value>
    public AuditTrailInfo? AuditTrail { get; set; }

    /// <summary>
    /// Gets or sets the compliance validation results for regulatory requirements.
    /// </summary>
    /// <value>Validation results for compliance with organizational and regulatory standards.</value>
    public ComplianceValidation? ComplianceValidation { get; set; }
}

/// <summary>
/// Represents a comprehensive change summary for workflow update operations.
/// </summary>
public class WorkflowChangeSummary
{
    /// <summary>
    /// Gets or sets the total number of changes made during the update.
    /// </summary>
    /// <value>Total count of modifications made to the workflow.</value>
    public int TotalChanges { get; set; }

    /// <summary>
    /// Gets or sets the summary of property changes made to the workflow.
    /// </summary>
    /// <value>Collection of property-level changes with before/after values.</value>
    public List<PropertyChange>? PropertyChanges { get; set; }

    /// <summary>
    /// Gets or sets the summary of node changes made to the workflow.
    /// </summary>
    /// <value>Summary of node additions, modifications, and deletions.</value>
    public NodeChangeSummary? NodeChanges { get; set; }

    /// <summary>
    /// Gets or sets the summary of edge changes made to the workflow.
    /// </summary>
    /// <value>Summary of edge additions, modifications, and deletions.</value>
    public EdgeChangeSummary? EdgeChanges { get; set; }

    /// <summary>
    /// Gets or sets the summary of configuration changes made to the workflow.
    /// </summary>
    /// <value>Summary of configuration and metadata changes.</value>
    public ConfigurationChangeSummary? ConfigurationChanges { get; set; }
}

/// <summary>
/// Represents a property-level change with before and after values.
/// </summary>
public class PropertyChange
{
    /// <summary>
    /// Gets or sets the name of the property that was changed.
    /// </summary>
    /// <value>The property name that was modified during the update.</value>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value before the change.
    /// </summary>
    /// <value>The property value before the modification.</value>
    public object? OldValue { get; set; }

    /// <summary>
    /// Gets or sets the value after the change.
    /// </summary>
    /// <value>The property value after the modification.</value>
    public object? NewValue { get; set; }

    /// <summary>
    /// Gets or sets the change type for the property modification.
    /// </summary>
    /// <value>The type of change performed on the property.</value>
    public ChangeType ChangeType { get; set; }
}

/// <summary>
/// Represents a summary of node changes during workflow updates.
/// </summary>
public class NodeChangeSummary
{
    /// <summary>
    /// Gets or sets the count of nodes added to the workflow.
    /// </summary>
    /// <value>Number of new nodes added during the update.</value>
    public int NodesAdded { get; set; }

    /// <summary>
    /// Gets or sets the count of nodes modified in the workflow.
    /// </summary>
    /// <value>Number of existing nodes modified during the update.</value>
    public int NodesModified { get; set; }

    /// <summary>
    /// Gets or sets the count of nodes deleted from the workflow.
    /// </summary>
    /// <value>Number of nodes removed during the update.</value>
    public int NodesDeleted { get; set; }

    /// <summary>
    /// Gets or sets detailed information about specific node changes.
    /// </summary>
    /// <value>Collection of detailed node change information.</value>
    public List<NodeChangeDetail>? DetailedChanges { get; set; }
}

/// <summary>
/// Represents a summary of edge changes during workflow updates.
/// </summary>
public class EdgeChangeSummary
{
    /// <summary>
    /// Gets or sets the count of edges added to the workflow.
    /// </summary>
    /// <value>Number of new edges added during the update.</value>
    public int EdgesAdded { get; set; }

    /// <summary>
    /// Gets or sets the count of edges modified in the workflow.
    /// </summary>
    /// <value>Number of existing edges modified during the update.</value>
    public int EdgesModified { get; set; }

    /// <summary>
    /// Gets or sets the count of edges deleted from the workflow.
    /// </summary>
    /// <value>Number of edges removed during the update.</value>
    public int EdgesDeleted { get; set; }

    /// <summary>
    /// Gets or sets detailed information about specific edge changes.
    /// </summary>
    /// <value>Collection of detailed edge change information.</value>
    public List<EdgeChangeDetail>? DetailedChanges { get; set; }
}

/// <summary>
/// Represents a summary of configuration changes during workflow updates.
/// </summary>
public class ConfigurationChangeSummary
{
    /// <summary>
    /// Gets or sets whether execution settings were modified.
    /// </summary>
    /// <value>True if execution configuration was changed; otherwise, false.</value>
    public bool ExecutionSettingsChanged { get; set; }

    /// <summary>
    /// Gets or sets whether variable definitions were modified.
    /// </summary>
    /// <value>True if variable definitions were changed; otherwise, false.</value>
    public bool VariableDefinitionsChanged { get; set; }

    /// <summary>
    /// Gets or sets whether security settings were modified.
    /// </summary>
    /// <value>True if security configuration was changed; otherwise, false.</value>
    public bool SecuritySettingsChanged { get; set; }

    /// <summary>
    /// Gets or sets the specific configuration changes made.
    /// </summary>
    /// <value>Collection of specific configuration modifications.</value>
    public List<PropertyChange>? SpecificChanges { get; set; }
}

/// <summary>
/// Represents detailed information about a node change.
/// </summary>
public class NodeChangeDetail
{
    /// <summary>
    /// Gets or sets the node identifier that was changed.
    /// </summary>
    /// <value>The unique identifier of the modified node.</value>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the node name for identification.
    /// </summary>
    /// <value>The name of the modified node.</value>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the operation performed on the node.
    /// </summary>
    /// <value>The type of operation performed (add, update, delete, etc.).</value>
    public NodeOperationType Operation { get; set; }

    /// <summary>
    /// Gets or sets the specific property changes for the node.
    /// </summary>
    /// <value>Collection of property-level changes for the node.</value>
    public List<PropertyChange>? PropertyChanges { get; set; }
}

/// <summary>
/// Represents detailed information about an edge change.
/// </summary>
public class EdgeChangeDetail
{
    /// <summary>
    /// Gets or sets the edge identifier that was changed.
    /// </summary>
    /// <value>The unique identifier of the modified edge.</value>
    public string EdgeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source node identifier for the edge.
    /// </summary>
    /// <value>The identifier of the source node for the edge.</value>
    public string SourceNodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target node identifier for the edge.
    /// </summary>
    /// <value>The identifier of the target node for the edge.</value>
    public string TargetNodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the operation performed on the edge.
    /// </summary>
    /// <value>The type of operation performed (add, update, delete, etc.).</value>
    public EdgeOperationType Operation { get; set; }

    /// <summary>
    /// Gets or sets the specific property changes for the edge.
    /// </summary>
    /// <value>Collection of property-level changes for the edge.</value>
    public List<PropertyChange>? PropertyChanges { get; set; }
}

/// <summary>
/// Enumeration of change types for property modifications.
/// </summary>
public enum ChangeType
{
    /// <summary>Property was added</summary>
    Added = 0,
    /// <summary>Property was modified</summary>
    Modified = 1,
    /// <summary>Property was deleted</summary>
    Deleted = 2,
    /// <summary>Property was renamed</summary>
    Renamed = 3
}
