using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents an edge/connection between nodes in a workflow
/// </summary>
public class FlowEdge : AuditableEntity
{
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
    /// </summary>
    [StringLength(50)]
    public string SourceHandle { get; set; } = "output";
    
    /// <summary>
    /// Target handle/port identifier
    /// </summary>
    [StringLength(50)]
    public string TargetHandle { get; set; } = "input";
    
    /// <summary>
    /// Conditional logic for this edge (JSON)
    /// </summary>
    public string? Condition { get; set; }
    
    /// <summary>
    /// Whether this is the default path for conditional nodes
    /// </summary>
    public bool IsDefault { get; set; } = false;
    
    /// <summary>
    /// Edge label for display purposes
    /// </summary>
    [StringLength(100)]
    public string? Label { get; set; }
    
    /// <summary>
    /// Edge style configuration (JSON)
    /// </summary>
    public string? Style { get; set; }
    
    // Analytics
    /// <summary>
    /// Number of times this edge has been traversed during executions
    /// </summary>
    public long TraversalCount { get; set; } = 0;
    
    /// <summary>
    /// Success rate of executions that traversed this edge
    /// </summary>
    public decimal? SuccessRate { get; set; }
}