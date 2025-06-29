namespace PromptStudio.Core.Domain;

/// <summary>
/// Records edge traversal during workflow execution
/// </summary>
public class EdgeTraversal : AuditableEntity
{
    /// <summary>
    /// Reference to the node execution that initiated this traversal
    /// </summary>
    public Guid NodeExecutionId { get; set; }
    public virtual NodeExecution NodeExecution { get; set; } = null!;
    
    /// <summary>
    /// Edge that was traversed
    /// </summary>
    public Guid EdgeId { get; set; }
    public virtual FlowEdge Edge { get; set; } = null!;
    
    /// <summary>
    /// When the traversal occurred
    /// </summary>
    public DateTime TraversalTime { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Condition evaluation result (if applicable)
    /// </summary>
    public string? ConditionResult { get; set; }
    
    /// <summary>
    /// Data passed along this edge (JSON)
    /// </summary>
    public string? TraversalData { get; set; }
    
    /// <summary>
    /// Whether the traversal was successful
    /// </summary>
    public bool Success { get; set; } = true;
    
    /// <summary>
    /// Error message if traversal failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}
