using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Execution instance of a workflow with complete traceability and analytics
/// Enhanced to support enterprise requirements and template traceability
/// </summary>
public class FlowExecution : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Version of the workflow that was executed
    /// </summary>
    [Required]
    [StringLength(20)]
    public string FlowVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Input variables provided for this execution (JSON)
    /// </summary>
    public string InputVariables { get; set; } = "{}";
    
    /// <summary>
    /// Final output result from the workflow (JSON)
    /// </summary>
    public string OutputResult { get; set; } = "{}";
    
    /// <summary>
    /// Execution context and metadata (JSON)
    /// </summary>
    public string ExecutionContext { get; set; } = "{}";
    
    /// <summary>
    /// User or system that initiated this execution
    /// </summary>
    [StringLength(100)]
    public string? ExecutedBy { get; set; }
    
    /// <summary>
    /// Execution environment (development, staging, production)
    /// </summary>
    [StringLength(50)]
    public string Environment { get; set; } = "production";
    
    // Performance Metrics
    /// <summary>
    /// Total execution time in milliseconds
    /// </summary>
    public int TotalExecutionTime { get; set; }
    
    /// <summary>
    /// Total cost for this execution
    /// </summary>
    public decimal TotalCost { get; set; }
    
    /// <summary>
    /// Total tokens consumed across all nodes
    /// </summary>
    public long TotalTokens { get; set; }
      // Quality and Status
    /// <summary>
    /// Overall execution status
    /// </summary>
    public FlowExecutionStatus Status { get; set; }
    
    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Overall quality score for this execution (0.0 - 1.0)
    /// </summary>
    public decimal? QualityScore { get; set; }
    
    // Experimentation and A/B Testing
    /// <summary>
    /// Experiment ID if this execution was part of an A/B test
    /// </summary>
    public Guid? ExperimentId { get; set; }
    
    /// <summary>
    /// Variant ID if this execution used a specific workflow variant
    /// </summary>
    public Guid? VariantId { get; set; }
    public virtual FlowVariant? Variant { get; set; }
    
    // User Feedback
    /// <summary>
    /// User rating for this execution (1-5 stars)
    /// </summary>
    public int? UserRating { get; set; }
    
    /// <summary>
    /// User feedback comments
    /// </summary>
    public string? UserFeedback { get; set; }
    
    /// <summary>
    /// When user provided feedback
    /// </summary>
    public DateTime? FeedbackAt { get; set; }

    // Navigation properties
    public virtual ICollection<NodeExecution> NodeExecutions { get; set; } = new List<NodeExecution>();
    public virtual ICollection<ExecutionMetric> Metrics { get; set; } = new List<ExecutionMetric>();
}