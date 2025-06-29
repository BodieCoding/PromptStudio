using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// AI-generated suggestions for workflow optimization
/// </summary>
public class WorkflowSuggestion : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Type of suggestion
    /// </summary>
    public SuggestionType Type { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed suggestion data and implementation details (JSON)
    /// </summary>
    public string SuggestionData { get; set; } = "{}";
    
    /// <summary>
    /// Priority level for this suggestion
    /// </summary>
    public SuggestionPriority Priority { get; set; } = SuggestionPriority.Medium;
    
    // AI Context
    /// <summary>
    /// AI model that generated this suggestion
    /// </summary>
    [StringLength(50)]
    public string AiModel { get; set; } = string.Empty;
    
    /// <summary>
    /// AI confidence score for this suggestion (0.0 - 1.0)
    /// </summary>
    public decimal ConfidenceScore { get; set; }
    
    /// <summary>
    /// AI reasoning context and explanation
    /// </summary>
    public string? ReasoningContext { get; set; }
    
    // Expected Impact
    /// <summary>
    /// Expected performance improvement percentage
    /// </summary>
    public decimal? ExpectedPerformanceImpact { get; set; }
    
    /// <summary>
    /// Expected cost impact (positive = cost increase, negative = cost savings)
    /// </summary>
    public decimal? ExpectedCostImpact { get; set; }
    
    /// <summary>
    /// Implementation effort estimate (hours)
    /// </summary>
    public int? ImplementationEffort { get; set; }
    
    // User Response
    /// <summary>
    /// Current status of this suggestion
    /// </summary>
    public SuggestionStatus Status { get; set; } = SuggestionStatus.Pending;
    
    /// <summary>
    /// User feedback on this suggestion
    /// </summary>
    public string? UserFeedback { get; set; }
    
    /// <summary>
    /// When the user responded to this suggestion
    /// </summary>
    public DateTime? RespondedAt { get; set; }
    
    /// <summary>
    /// User who responded to this suggestion
    /// </summary>
    [StringLength(100)]
    public string? RespondedBy { get; set; }
    
    // Post-Implementation Tracking
    /// <summary>
    /// Actual performance impact after implementation
    /// </summary>
    public decimal? ActualPerformanceImpact { get; set; }
    
    /// <summary>
    /// Actual cost impact after implementation
    /// </summary>
    public decimal? ActualCostImpact { get; set; }
    
    /// <summary>
    /// When this suggestion was implemented
    /// </summary>
    public DateTime? ImplementedAt { get; set; }
}
