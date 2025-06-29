namespace PromptStudio.Core.Domain;

/// <summary>
/// Records edge traversal events during workflow execution for comprehensive audit trails and debugging.
/// Captures the complete context of how data flows through workflow connections, including
/// timing, conditions, data transformation, and error states.
/// </summary>
/// <remarks>
/// EdgeTraversal provides detailed tracking of workflow execution paths, enabling
/// sophisticated debugging, performance analysis, and compliance auditing. Each traversal
/// represents a single movement of data/control from one node to another through a
/// specific edge connection.
/// </remarks>
public class EdgeTraversal : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the node execution that initiated this traversal.
    /// Links this traversal to the specific node execution context that triggered the edge transition.
    /// </summary>
    /// <value>The GUID of the NodeExecution that initiated this edge traversal.</value>
    /// <remarks>
    /// This creates a direct link to the source node's execution context, enabling
    /// reconstruction of the complete execution flow and dependency analysis.
    /// </remarks>
    public Guid NodeExecutionId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the node execution that initiated this traversal.
    /// Provides access to the full execution context of the source node.
    /// </summary>
    /// <value>The NodeExecution entity that triggered this edge traversal.</value>
    public virtual NodeExecution NodeExecution { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the unique identifier of the edge that was traversed.
    /// References the workflow edge definition that defines the connection rules and behavior.
    /// </summary>
    /// <value>The GUID of the FlowEdge that was traversed during execution.</value>
    /// <remarks>
    /// Links to the edge definition which contains routing rules, conditions,
    /// and transformation logic that governed this traversal.
    /// </remarks>
    public Guid EdgeId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the edge that was traversed.
    /// Provides access to the edge definition including routing rules and conditions.
    /// </summary>
    /// <value>The FlowEdge entity that defines the traversed connection.</value>
    public virtual FlowEdge Edge { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the precise timestamp when this edge traversal occurred.
    /// Enables accurate timing analysis and sequencing of workflow execution events.
    /// </summary>
    /// <value>
    /// The UTC timestamp of when the edge traversal was executed. 
    /// Defaults to the current UTC time when the entity is created.
    /// </value>
    /// <remarks>
    /// High-precision timing data is crucial for performance analysis,
    /// bottleneck identification, and execution flow reconstruction.
    /// </remarks>
    public DateTime TraversalTime { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the result of condition evaluation that determined this traversal path.
    /// Contains the evaluated condition result when the edge has conditional routing logic.
    /// </summary>
    /// <value>
    /// The string representation of the condition evaluation result (e.g., "true", "score > 0.8").
    /// Null if the edge has no conditional logic or uses default routing.
    /// </value>
    /// <example>
    /// Examples: "true", "false", "score >= 0.75", "output.confidence > threshold",
    /// "classification == 'approved'", "retry_count < 3"
    /// </example>
    /// <remarks>
    /// Critical for understanding why a particular path was taken in complex workflows
    /// with branching logic and conditional routing.
    /// </remarks>
    public string? ConditionResult { get; set; }
    
    /// <summary>
    /// Gets or sets the data payload that was passed along this edge during traversal.
    /// Contains the serialized JSON representation of data flowing through the connection.
    /// </summary>
    /// <value>
    /// JSON-serialized data that was transmitted along this edge. Can include transformed
    /// outputs, metadata, context variables, or any workflow-specific payload.
    /// Null if no data was passed or if data passing is disabled for this edge.
    /// </value>
    /// <example>
    /// Examples: {"result": "success", "score": 0.95}, {"tokens": 150, "model": "gpt-4"},
    /// {"error": null, "output": "Generated content here"}
    /// </example>
    /// <remarks>
    /// Enables complete data flow reconstruction and debugging of data transformation
    /// issues. Large payloads should be truncated or summarized to avoid storage bloat.
    /// </remarks>
    public string? TraversalData { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the edge traversal completed successfully.
    /// Tracks the success or failure state of the traversal operation.
    /// </summary>
    /// <value>
    /// <c>true</c> if the edge traversal completed without errors; <c>false</c> if
    /// the traversal encountered errors or was aborted. Defaults to <c>true</c>.
    /// </value>
    /// <remarks>
    /// Essential for error tracking and workflow reliability analysis.
    /// Failed traversals help identify problematic connections and edge cases.
    /// </remarks>
    public bool Success { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the error message describing any failure that occurred during traversal.
    /// Provides detailed error information when Success is false.
    /// </summary>
    /// <value>
    /// A descriptive error message explaining why the traversal failed.
    /// Null when Success is true or no specific error details are available.
    /// </value>
    /// <example>
    /// Examples: "Condition evaluation failed: undefined variable 'score'",
    /// "Data transformation error: invalid JSON format",
    /// "Target node rejected input: validation failed"
    /// </example>
    /// <remarks>
    /// Critical for debugging workflow execution issues and identifying
    /// configuration problems or runtime errors in edge logic.
    /// </remarks>
    public string? ErrorMessage { get; set; }
}
