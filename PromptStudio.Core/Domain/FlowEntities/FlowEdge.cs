using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a connection/edge between nodes in a workflow
/// Defines the flow of data and control through the workflow graph
/// </summary>
public class FlowEdge : AuditableEntity
{
    /// <summary>
    /// Reference to the parent workflow
    /// </summary>
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Source node for this edge
    /// </summary>
    public Guid SourceNodeId { get; set; }
    public virtual FlowNode SourceNode { get; set; } = null!;
    
    /// <summary>
    /// Target node for this edge
    /// </summary>
    public Guid TargetNodeId { get; set; }
    public virtual FlowNode TargetNode { get; set; } = null!;
    
    /// <summary>
    /// Source handle/port identifier
    /// Identifies which output port of the source node this edge connects from
    /// </summary>
    [StringLength(50)]
    public string SourceHandle { get; set; } = "output";
    
    /// <summary>
    /// Target handle/port identifier
    /// Identifies which input port of the target node this edge connects to
    /// </summary>
    [StringLength(50)]
    public string TargetHandle { get; set; } = "input";
    
    /// <summary>
    /// Conditional logic for this edge (JSON)
    /// Defines when this edge should be traversed during execution
    /// </summary>
    public string? Condition { get; set; }
    
    /// <summary>
    /// Whether this is the default path for conditional nodes
    /// Used when no other conditions are met
    /// </summary>
    public bool IsDefault { get; set; } = false;
    
    /// <summary>
    /// Edge label for display purposes
    /// </summary>
    [StringLength(100)]
    public string? Label { get; set; }
    
    /// <summary>
    /// Edge style configuration (JSON)
    /// Contains visual styling information for the workflow builder
    /// </summary>
    public string? Style { get; set; }
    
    /// <summary>
    /// Priority order when multiple edges from the same source exist
    /// </summary>
    public int Priority { get; set; } = 0;
    
    /// <summary>
    /// Whether this edge is currently enabled for execution
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Data transformation rules applied when traversing this edge (JSON)
    /// </summary>
    public string? TransformationRules { get; set; }
    
    /// <summary>
    /// Edge type for different connection behaviors
    /// </summary>
    public EdgeType Type { get; set; } = EdgeType.Normal;
    
    // Analytics and Performance Tracking
    /// <summary>
    /// Number of times this edge has been traversed during executions
    /// </summary>
    public long TraversalCount { get; set; } = 0;
    
    /// <summary>
    /// Success rate of executions that traversed this edge (0.0 - 1.0)
    /// </summary>
    public decimal? SuccessRate { get; set; }
    
    /// <summary>
    /// Average execution time for traversing this edge (milliseconds)
    /// </summary>
    public int? AverageTraversalTime { get; set; }
    
    /// <summary>
    /// Last time this edge was traversed
    /// </summary>
    public DateTime? LastTraversedAt { get; set; }
    
    /// <summary>
    /// Condition evaluation success rate (for conditional edges)
    /// </summary>
    public decimal? ConditionSuccessRate { get; set; }
    
    // Validation and Quality
    /// <summary>
    /// Edge validation status
    /// </summary>
    public EdgeValidationStatus ValidationStatus { get; set; } = EdgeValidationStatus.Valid;
    
    /// <summary>
    /// Validation messages or warnings (JSON array)
    /// </summary>
    public string? ValidationMessages { get; set; }
}