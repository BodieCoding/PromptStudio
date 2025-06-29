using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a directed connection between nodes in a visual workflow graph.
/// FlowEdges define the data flow, control flow, and conditional routing pathways
/// that enable complex LLM workflow orchestration and execution control.
/// </summary>
/// <remarks>
/// FlowEdge entities encapsulate the routing logic, conditions, and data transformation
/// rules that govern how information flows between workflow nodes. They support
/// conditional branching, data filtering, transformation, and sophisticated
/// workflow control patterns essential for enterprise LLM applications.
/// </remarks>
public class FlowEdge : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the parent workflow containing this edge.
    /// Establishes the hierarchical relationship and ensures edge isolation within workflows.
    /// </summary>
    /// <value>The GUID of the PromptFlow that contains this edge.</value>
    /// <remarks>
    /// Workflow membership ensures proper isolation and enables workflow-scoped
    /// operations like validation, execution, and version control.
    /// </remarks>
    public Guid FlowId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the parent workflow.
    /// Provides access to workflow-level configuration and execution context.
    /// </summary>
    /// <value>The PromptFlow entity that owns this edge.</value>
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the unique identifier of the source node for this edge.
    /// Defines the originating node where data or control flow begins.
    /// </summary>
    /// <value>The GUID of the FlowNode that serves as the source of this connection.</value>
    /// <remarks>
    /// Source node identification is critical for execution ordering and
    /// dependency resolution during workflow processing.
    /// </remarks>
    public Guid SourceNodeId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the source node.
    /// Provides access to the originating node's configuration and execution state.
    /// </summary>
    /// <value>The FlowNode entity that originates this edge.</value>
    public virtual FlowNode SourceNode { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the unique identifier of the target node for this edge.
    /// Defines the destination node where data or control flow is directed.
    /// </summary>
    /// <value>The GUID of the FlowNode that receives data through this connection.</value>
    /// <remarks>
    /// Target node identification determines execution dependencies and
    /// enables proper workflow traversal during execution.
    /// </remarks>
    public Guid TargetNodeId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the target node.
    /// Provides access to the destination node's configuration and input requirements.
    /// </summary>
    /// <value>The FlowNode entity that receives data through this edge.</value>
    public virtual FlowNode TargetNode { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the source handle identifier specifying which output port connects to this edge.
    /// Enables multiple output ports per node for complex routing scenarios.
    /// </summary>
    /// <value>
    /// A string identifier for the source node's output port (e.g., "output", "success", "error").
    /// Defaults to "output". Maximum length is 50 characters.
    /// </value>
    /// <example>
    /// Examples: "output" (default), "success", "error", "result", "fallback", "validation"
    /// </example>
    /// <remarks>
    /// Handle identification enables sophisticated node designs with multiple
    /// output paths for different execution outcomes or data types.
    /// </remarks>
    [StringLength(50)]
    public string SourceHandle { get; set; } = "output";
    
    /// <summary>
    /// Gets or sets the target handle identifier specifying which input port receives this connection.
    /// Enables multiple input ports per node for complex data aggregation scenarios.
    /// </summary>
    /// <value>
    /// A string identifier for the target node's input port (e.g., "input", "primary", "secondary").
    /// Defaults to "input". Maximum length is 50 characters.
    /// </value>
    /// <example>
    /// Examples: "input" (default), "primary", "secondary", "condition", "data", "context"
    /// </example>
    /// <remarks>
    /// Handle identification supports complex node architectures requiring
    /// multiple data inputs or specialized input types.
    /// </remarks>
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