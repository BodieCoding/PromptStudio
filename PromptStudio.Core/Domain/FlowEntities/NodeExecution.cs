using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Execution record for individual nodes within a workflow execution
/// Provides detailed tracking and debugging capabilities for workflow performance
/// </summary>
public class NodeExecution : AuditableEntity
{
    /// <summary>
    /// Reference to the parent flow execution
    /// </summary>
    public Guid FlowExecutionId { get; set; }
    public virtual FlowExecution FlowExecution { get; set; } = null!;
    
    /// <summary>
    /// Reference to the node that was executed
    /// </summary>
    public Guid NodeId { get; set; }
    public virtual FlowNode Node { get; set; } = null!;
    
    /// <summary>
    /// Node key for identification within the workflow
    /// </summary>
    [Required]
    [StringLength(50)]
    public string NodeKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Type of node that was executed
    /// </summary>
    public FlowNodeType NodeType { get; set; }
    
    /// <summary>
    /// Execution sequence order within the flow
    /// </summary>
    public int ExecutionOrder { get; set; }
    
    /// <summary>
    /// Execution timing
    /// </summary>
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Execution duration in milliseconds
    /// </summary>
    public int? DurationMs { get; set; }
    
    /// <summary>
    /// Input data provided to this node (JSON)
    /// </summary>
    public string InputData { get; set; } = "{}";
    
    /// <summary>
    /// Output data produced by this node (JSON)
    /// </summary>
    public string? OutputData { get; set; }
    
    /// <summary>
    /// Execution status of this node
    /// </summary>
    public NodeExecutionStatus Status { get; set; } = NodeExecutionStatus.Pending;
    
    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Stack trace for debugging failures
    /// </summary>
    public string? ErrorStackTrace { get; set; }
    
    /// <summary>
    /// Number of retry attempts made
    /// </summary>
    public int RetryCount { get; set; } = 0;
    
    /// <summary>
    /// Cost incurred by this node execution
    /// </summary>
    public decimal? Cost { get; set; }
    
    /// <summary>
    /// Token usage for AI-based nodes
    /// </summary>
    public int? TokensUsed { get; set; }
    
    /// <summary>
    /// AI provider used (if applicable)
    /// </summary>
    [StringLength(50)]
    public string? AiProvider { get; set; }
    
    /// <summary>
    /// AI model used (if applicable)
    /// </summary>
    [StringLength(100)]
    public string? AiModel { get; set; }
    
    /// <summary>
    /// Template used for this execution (if applicable)
    /// </summary>
    public Guid? PromptTemplateId { get; set; }
    public virtual PromptTemplate? PromptTemplate { get; set; }
    
    /// <summary>
    /// Template version used
    /// </summary>
    [StringLength(20)]
    public string? TemplateVersion { get; set; }
    
    /// <summary>
    /// Quality score for this execution (0.0 - 1.0)
    /// </summary>
    public decimal? QualityScore { get; set; }
    
    /// <summary>
    /// Confidence level of the execution result (0.0 - 1.0)
    /// </summary>
    public decimal? ConfidenceLevel { get; set; }
    
    /// <summary>
    /// Performance metrics (JSON)
    /// </summary>
    public string? PerformanceMetrics { get; set; }
    
    /// <summary>
    /// Debug information (JSON)
    /// </summary>
    public string? DebugInfo { get; set; }
    
    /// <summary>
    /// Cache hit information
    /// </summary>
    public bool CacheHit { get; set; } = false;
    
    /// <summary>
    /// Cache key used (if applicable)
    /// </summary>
    [StringLength(256)]
    public string? CacheKey { get; set; }
    
    /// <summary>
    /// Edge traversed to reach this node
    /// </summary>
    public Guid? IncomingEdgeId { get; set; }
    public virtual FlowEdge? IncomingEdge { get; set; }
    
    /// <summary>
    /// Edges traversed from this node
    /// </summary>
    public virtual ICollection<EdgeTraversal> OutgoingTraversals { get; set; } = new List<EdgeTraversal>();
}
