using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents an individual processing node within a visual workflow graph.
/// FlowNodes are the fundamental building blocks of LLM workflows, encapsulating
/// discrete processing units such as prompt execution, data transformation,
/// conditional logic, or external API calls.
/// </summary>
/// <remarks>
/// Each FlowNode maintains its own configuration, visual positioning, execution
/// history, and performance metrics. Nodes are connected via FlowEdges to create
/// complex workflow topologies. The node system supports various types including
/// LLM prompts, transformations, conditions, loops, and integrations.
/// </remarks>
public class FlowNode : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the parent workflow containing this node.
    /// Establishes the hierarchical relationship between workflows and their constituent nodes.
    /// </summary>
    /// <value>The GUID of the PromptFlow that contains this node.</value>
    public Guid FlowId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the parent workflow.
    /// Provides access to workflow-level configuration and metadata.
    /// </summary>
    /// <value>The PromptFlow entity that owns this node.</value>
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the unique identifier key for this node within its workflow scope.
    /// Used for referencing the node in workflow definitions and execution contexts.
    /// </summary>
    /// <value>
    /// A workflow-scoped unique identifier (e.g., "start", "llm-1", "condition-check", "end").
    /// Must be unique within the parent workflow. Maximum length is 50 characters.
    /// </value>
    /// <example>
    /// Examples: "start", "classification-prompt", "quality-check", "retry-logic", "final-output"
    /// </example>
    /// <remarks>
    /// The NodeKey enables human-readable references in workflow configuration
    /// and provides stable identity for node operations independent of database IDs.
    /// </remarks>
    [Required]
    [StringLength(50)]
    public string NodeKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of this node, which determines its behavior and capabilities.
    /// Defines the processing logic and available configuration options for the node.
    /// </summary>
    /// <value>
    /// The FlowNodeType enum value specifying the node's functional category
    /// (e.g., Prompt, Condition, Transform, Input, Output, etc.).
    /// </value>
    /// <remarks>
    /// Node type determines the execution engine, configuration schema, and
    /// available input/output ports for the node.
    /// </remarks>
    public FlowNodeType NodeType { get; set; }
    
    /// <summary>
    /// Gets or sets the node-specific configuration data in JSON format.
    /// Contains all parameters, settings, and behavior configuration for this node type.
    /// </summary>
    /// <value>
    /// A JSON string containing node-specific configuration. Structure varies by NodeType.
    /// Defaults to an empty JSON object "{}".
    /// </value>
    /// <example>
    /// Prompt node: {"model": "gpt-4", "temperature": 0.7, "max_tokens": 1000}
    /// Condition node: {"expression": "output.confidence > 0.8", "true_path": "success"}
    /// Transform node: {"script": "return input.text.toUpperCase();", "language": "javascript"}
    /// </example>
    /// <remarks>
    /// The configuration schema is validated against the node type's requirements
    /// during workflow validation and execution preparation.
    /// </remarks>
    public string NodeData { get; set; } = "{}";
    
    /// <summary>
    /// Gets or sets the human-readable display name for this node in the visual builder.
    /// Provides a user-friendly label for the node in workflow diagrams and editors.
    /// </summary>
    /// <value>
    /// A descriptive name for display purposes. Optional field with maximum length of 100 characters.
    /// Null values will typically display the NodeKey as fallback.
    /// </value>
    /// <example>
    /// Examples: "Initial Classification", "Quality Score Check", "Generate Summary", "Final Review"
    /// </example>
    [StringLength(100)]
    public string? DisplayName { get; set; }
    
    /// <summary>
    /// Gets or sets an optional description explaining the node's purpose and functionality.
    /// Provides detailed context about what this node does within the workflow.
    /// </summary>
    /// <value>
    /// A descriptive explanation of the node's role, processing logic, or business purpose.
    /// Optional field with maximum length of 500 characters.
    /// </value>
    /// <example>
    /// "Classifies incoming requests into priority categories based on urgency keywords and customer tier"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }
    
    // Visual Layout and Positioning
    
    /// <summary>
    /// Gets or sets the X-coordinate position of the node in the visual workflow builder.
    /// Determines horizontal placement in the workflow diagram canvas.
    /// </summary>
    /// <value>The X-coordinate in pixels or logical units within the workflow canvas.</value>
    /// <remarks>
    /// Used by visual workflow editors to maintain node positioning and layout.
    /// Coordinate system origin is typically top-left of the canvas.
    /// </remarks>
    public double PositionX { get; set; }
    
    /// <summary>
    /// Gets or sets the Y-coordinate position of the node in the visual workflow builder.
    /// Determines vertical placement in the workflow diagram canvas.
    /// </summary>
    /// <value>The Y-coordinate in pixels or logical units within the workflow canvas.</value>
    /// <remarks>
    /// Used by visual workflow editors to maintain node positioning and layout.
    /// Coordinate system origin is typically top-left of the canvas.
    /// </remarks>
    public double PositionY { get; set; }
    
    /// <summary>
    /// Gets or sets the width of the node in the visual workflow builder.
    /// Determines the horizontal size of the node representation.
    /// </summary>
    /// <value>The node width in pixels or logical units. Defaults to 200.</value>
    /// <remarks>
    /// Visual sizing helps accommodate different node types and content lengths.
    /// Larger nodes may be needed for complex configurations or detailed labels.
    /// </remarks>
    public double Width { get; set; } = 200;
    
    /// <summary>
    /// Gets or sets the height of the node in the visual workflow builder.
    /// Determines the vertical size of the node representation.
    /// </summary>
    /// <value>The node height in pixels or logical units. Defaults to 100.</value>
    /// <remarks>
    /// Height may vary based on node type, number of input/output ports,
    /// and amount of configuration data to display.
    /// </remarks>
    public double Height { get; set; } = 100;
    
    /// <summary>
    /// Gets or sets visual styling information for custom node appearance.
    /// Contains JSON-formatted style properties for the visual builder.
    /// </summary>
    /// <value>
    /// JSON string containing style properties like colors, borders, fonts, icons.
    /// Null indicates default styling should be used.
    /// </value>
    /// <example>
    /// {"backgroundColor": "#e3f2fd", "borderColor": "#1976d2", "icon": "chat", "textColor": "#000"}
    /// </example>
    /// <remarks>
    /// Enables custom visual themes and helps distinguish different node types
    /// or states in complex workflows.
    /// </remarks>
    public string? StyleData { get; set; }
    
    // Template Association (CRITICAL for template-execution traceability)
    
    /// <summary>
    /// Gets or sets the unique identifier of the PromptTemplate used by this node.
    /// Links the node to a specific prompt template for LLM execution nodes.
    /// </summary>
    /// <value>
    /// The GUID of the PromptTemplate associated with this node.
    /// Null for nodes that don't use prompt templates (e.g., condition, transform nodes).
    /// </value>
    /// <remarks>
    /// Critical for traceability between workflow executions and prompt templates.
    /// Enables tracking of template usage, versioning, and performance analysis.
    /// </remarks>
    public Guid? PromptTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the associated prompt template.
    /// Provides access to the template content, configuration, and metadata.
    /// </summary>
    /// <value>The PromptTemplate entity used by this node, or null if not applicable.</value>
    public virtual PromptTemplate? PromptTemplate { get; set; }
    
    /// <summary>
    /// Gets or sets the version of the template when it was associated with this node.
    /// Captures the template version for audit trails and rollback capabilities.
    /// </summary>
    /// <value>
    /// A version string identifying the specific template version (e.g., "1.0", "2.3-beta").
    /// Null if versioning is not being tracked or template is not used.
    /// Maximum length is 20 characters.
    /// </value>
    /// <remarks>
    /// Essential for understanding which template version was active during
    /// specific workflow executions and for change impact analysis.
    /// </remarks>
    [StringLength(20)]
    public string? TemplateVersion { get; set; }
    
    /// <summary>
    /// Gets or sets the role of the template within this node's execution context.
    /// Defines how the template is used when multiple templates might be involved.
    /// </summary>
    /// <value>
    /// The template's role in execution (e.g., "primary", "fallback", "validation").
    /// Defaults to "primary". Maximum length is 50 characters.
    /// </value>
    /// <example>
    /// Examples: "primary" (main execution), "fallback" (backup option), 
    /// "validation" (quality check), "preprocessing" (input preparation)
    /// </example>
    /// <remarks>
    /// Enables sophisticated template orchestration within nodes that might
    /// use multiple templates for different purposes or fallback scenarios.
    /// </remarks>
    [StringLength(50)]
    public string? TemplateRole { get; set; } = "primary";
    
    // Execution Configuration
    
    /// <summary>
    /// Gets or sets the maximum execution timeout for this node in seconds.
    /// Prevents runaway executions and ensures workflow reliability.
    /// </summary>
    /// <value>
    /// The timeout duration in seconds. Null indicates no specific timeout (uses system default).
    /// Recommended values vary by node type: prompts (30-300s), transforms (5-60s).
    /// </value>
    /// <remarks>
    /// Critical for preventing workflow hangs and managing resource consumption.
    /// Should be tuned based on expected execution complexity and external dependencies.
    /// </remarks>
    public int? TimeoutSeconds { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum number of retry attempts when this node fails.
    /// Provides resilience against transient failures and external service issues.
    /// </summary>
    /// <value>
    /// The number of retry attempts (0 = no retries, 1+ = retry count).
    /// Defaults to 0 (no automatic retries).
    /// </value>
    /// <remarks>
    /// Retry logic should consider the node type and failure characteristics.
    /// LLM nodes may benefit from retries due to rate limiting, while
    /// deterministic transforms typically should not retry.
    /// </remarks>
    public int MaxRetries { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets a value indicating whether this node can execute in parallel with other nodes.
    /// Controls workflow execution parallelization and resource utilization.
    /// </summary>
    /// <value>
    /// <c>true</c> if the node supports parallel execution; <c>false</c> if it must run sequentially.
    /// Defaults to <c>true</c>.
    /// </value>
    /// <remarks>
    /// Nodes with shared resources, stateful operations, or order dependencies
    /// should disable parallel execution. Most LLM nodes can run in parallel.
    /// </remarks>
    public bool AllowParallelExecution { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the execution priority level for workflow scheduling.
    /// Determines the order of execution when multiple nodes are ready to run.
    /// </summary>
    /// <value>
    /// Priority level from 1 (highest priority) to 10 (lowest priority).
    /// Defaults to 5 (medium priority).
    /// </value>
    /// <remarks>
    /// Higher priority nodes (lower numbers) are scheduled for execution first.
    /// Useful for optimizing workflow performance and ensuring critical paths
    /// execute promptly.
    /// </remarks>
    public int Priority { get; set; } = 5;
    
    // Performance Tracking
    
    /// <summary>
    /// Gets or sets the total number of times this node has been executed across all workflow runs.
    /// Provides usage analytics and helps identify frequently used components.
    /// </summary>
    /// <value>
    /// The cumulative execution count since node creation. Defaults to 0.
    /// </value>
    /// <remarks>
    /// Incremented each time the node completes execution (successful or failed).
    /// Used for calculating average performance metrics and identifying usage patterns.
    /// </remarks>
    public long ExecutionCount { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the average execution time for this node in milliseconds.
    /// Enables performance monitoring and bottleneck identification.
    /// </summary>
    /// <value>
    /// The average execution duration in milliseconds across all runs. Defaults to 0.
    /// </value>
    /// <remarks>
    /// Calculated as a rolling average and updated after each execution.
    /// Critical for identifying performance degradation and optimization opportunities.
    /// </remarks>
    public int AverageExecutionTime { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the average cost per execution for this node.
    /// Tracks resource consumption and enables cost optimization analysis.
    /// </summary>
    /// <value>
    /// The average monetary cost per execution in the system's base currency.
    /// Includes API costs, compute costs, and other attributable expenses. Defaults to 0.
    /// </value>
    /// <remarks>
    /// Particularly important for LLM nodes where token usage directly impacts costs.
    /// Enables cost-aware workflow optimization and budget tracking.
    /// </remarks>
    public decimal AverageCost { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the success rate percentage for this node's executions.
    /// Indicates reliability and helps identify problematic components.
    /// </summary>
    /// <value>
    /// The success rate as a decimal (0.0 = 0%, 1.0 = 100%).
    /// Null if no executions have occurred yet.
    /// </value>
    /// <remarks>
    /// Calculated as successful executions divided by total executions.
    /// Low success rates may indicate configuration issues, external dependencies,
    /// or inappropriate usage patterns.
    /// </remarks>
    public decimal? SuccessRate { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp of the most recent execution of this node.
    /// Helps identify unused or rarely used components.
    /// </summary>
    /// <value>
    /// The UTC timestamp of the last execution, or null if never executed.
    /// </value>
    /// <remarks>
    /// Used for identifying stale or deprecated nodes and tracking usage patterns.
    /// Important for workflow maintenance and optimization efforts.
    /// </remarks>
    public DateTime? LastExecutedAt { get; set; }
    
    // Validation and Quality
    
    /// <summary>
    /// Gets or sets a value indicating whether this node is currently enabled for execution.
    /// Provides a mechanism to temporarily disable nodes without removing them.
    /// </summary>
    /// <value>
    /// <c>true</c> if the node can be executed; <c>false</c> if it should be skipped.
    /// Defaults to <c>true</c>.
    /// </value>
    /// <remarks>
    /// Disabled nodes are bypassed during workflow execution but remain in the definition.
    /// Useful for testing, gradual rollouts, or temporary disabling of problematic nodes.
    /// </remarks>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the current validation status of this node's configuration.
    /// Indicates whether the node is properly configured and ready for execution.
    /// </summary>
    /// <value>
    /// The NodeValidationStatus indicating the node's readiness (Valid, Warning, Error, etc.).
    /// Defaults to Valid.
    /// </value>
    /// <remarks>
    /// Validation encompasses configuration completeness, template availability,
    /// dependency satisfaction, and other execution prerequisites.
    /// </remarks>
    public NodeValidationStatus ValidationStatus { get; set; } = NodeValidationStatus.Valid;
    
    /// <summary>
    /// Gets or sets validation messages or warnings associated with this node.
    /// Provides detailed feedback about configuration issues or concerns.
    /// </summary>
    /// <value>
    /// A JSON array of validation messages with severity levels and descriptions.
    /// Null if no validation issues exist.
    /// </value>
    /// <example>
    /// [{"level": "warning", "message": "Template version is outdated"}, 
    ///  {"level": "error", "message": "Required parameter 'model' is missing"}]
    /// </example>
    /// <remarks>
    /// Critical for workflow debugging and ensuring execution readiness.
    /// Messages should be actionable and guide users toward resolution.
    /// </remarks>
    public string? ValidationMessages { get; set; }
    
    // Navigation Properties
    
    /// <summary>
    /// Gets or sets the collection of edges that terminate at this node (incoming connections).
    /// Represents the data flow inputs and control dependencies for this node.
    /// </summary>
    /// <value>A collection of FlowEdge entities that connect to this node as their target.</value>
    /// <remarks>
    /// Used for traversing the workflow graph upstream and analyzing dependencies.
    /// The number and types of incoming edges determine when this node can execute.
    /// </remarks>
    public virtual ICollection<FlowEdge> IncomingEdges { get; set; } = new List<FlowEdge>();
    
    /// <summary>
    /// Gets or sets the collection of edges that originate from this node (outgoing connections).
    /// Represents the data flow outputs and control paths from this node to subsequent nodes.
    /// </summary>
    /// <value>A collection of FlowEdge entities that originate from this node.</value>
    /// <remarks>
    /// Used for traversing the workflow graph downstream and understanding data flow.
    /// Multiple outgoing edges enable branching logic and parallel execution paths.
    /// </remarks>
    public virtual ICollection<FlowEdge> OutgoingEdges { get; set; } = new List<FlowEdge>();
    
    /// <summary>
    /// Gets or sets the collection of execution records for this specific node.
    /// Provides detailed history of all node execution attempts and their outcomes.
    /// </summary>
    /// <value>A collection of NodeExecution entities representing execution history.</value>
    /// <remarks>
    /// Essential for debugging, performance analysis, and audit trails.
    /// Each execution record contains timing, status, input/output data, and error information.
    /// </remarks>
    public virtual ICollection<NodeExecution> Executions { get; set; } = new List<NodeExecution>();
}