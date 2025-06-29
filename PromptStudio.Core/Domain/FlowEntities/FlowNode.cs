using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Individual node/component within a visual workflow
/// Represents discrete processing units in the workflow graph
/// </summary>
public class FlowNode : AuditableEntity
{
    /// <summary>
    /// Reference to the parent workflow
    /// </summary>
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// User-friendly identifier for the node within the workflow
    /// Must be unique within the workflow scope
    /// </summary>
    [Required]
    [StringLength(50)]
    public string NodeKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Type of node determining its behavior and capabilities
    /// </summary>
    public FlowNodeType NodeType { get; set; }
    
    /// <summary>
    /// Node-specific configuration data (JSON)
    /// Contains parameters, settings, and behavior configuration
    /// </summary>
    public string NodeData { get; set; } = "{}";
    
    /// <summary>
    /// Human-readable display name for the node
    /// </summary>
    [StringLength(100)]
    public string? DisplayName { get; set; }
    
    /// <summary>
    /// Optional description of the node's purpose
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }
    
    // Visual Layout and Positioning
    /// <summary>
    /// X-coordinate position in the visual builder
    /// </summary>
    public double PositionX { get; set; }
    
    /// <summary>
    /// Y-coordinate position in the visual builder
    /// </summary>
    public double PositionY { get; set; }
    
    /// <summary>
    /// Node width in the visual builder
    /// </summary>
    public double Width { get; set; } = 200;
    
    /// <summary>
    /// Node height in the visual builder
    /// </summary>
    public double Height { get; set; } = 100;
    
    /// <summary>
    /// Visual styling information (JSON)
    /// </summary>
    public string? StyleData { get; set; }
    
    // Template Association (CRITICAL for template-execution traceability)
    /// <summary>
    /// PromptTemplate used by this node (if applicable)
    /// </summary>
    public Guid? PromptTemplateId { get; set; }
    public virtual PromptTemplate? PromptTemplate { get; set; }
    
    /// <summary>
    /// Version of the template when it was added to this node
    /// </summary>
    [StringLength(20)]
    public string? TemplateVersion { get; set; }
    
    /// <summary>
    /// Role of the template in this node context
    /// </summary>
    [StringLength(50)]
    public string? TemplateRole { get; set; } = "primary"; // primary, fallback, validation
    
    // Execution Configuration
    /// <summary>
    /// Maximum execution timeout for this node (seconds)
    /// </summary>
    public int? TimeoutSeconds { get; set; }
    
    /// <summary>
    /// Maximum retry attempts on failure
    /// </summary>
    public int MaxRetries { get; set; } = 0;
    
    /// <summary>
    /// Whether this node can be executed in parallel with others
    /// </summary>
    public bool AllowParallelExecution { get; set; } = true;
    
    /// <summary>
    /// Priority level for execution scheduling (1 = highest, 10 = lowest)
    /// </summary>
    public int Priority { get; set; } = 5;
    
    // Performance Tracking
    /// <summary>
    /// Total number of times this node has been executed
    /// </summary>
    public long ExecutionCount { get; set; } = 0;
    
    /// <summary>
    /// Average execution time for this node (milliseconds)
    /// </summary>
    public int AverageExecutionTime { get; set; } = 0;
    
    /// <summary>
    /// Average cost per execution for this node
    /// </summary>
    public decimal AverageCost { get; set; } = 0;
    
    /// <summary>
    /// Success rate percentage (0.0 - 1.0)
    /// </summary>
    public decimal? SuccessRate { get; set; }
    
    /// <summary>
    /// Last execution timestamp
    /// </summary>
    public DateTime? LastExecutedAt { get; set; }
    
    // Validation and Quality
    /// <summary>
    /// Whether this node is currently enabled for execution
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Node validation status
    /// </summary>
    public NodeValidationStatus ValidationStatus { get; set; } = NodeValidationStatus.Valid;
    
    /// <summary>
    /// Validation messages or warnings (JSON array)
    /// </summary>
    public string? ValidationMessages { get; set; }
    
    // Navigation Properties
    public virtual ICollection<FlowEdge> IncomingEdges { get; set; } = new List<FlowEdge>();
    public virtual ICollection<FlowEdge> OutgoingEdges { get; set; } = new List<FlowEdge>();
    public virtual ICollection<NodeExecution> Executions { get; set; } = new List<NodeExecution>();
}