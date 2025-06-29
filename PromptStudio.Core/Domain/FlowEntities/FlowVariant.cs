using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Workflow variants for A/B testing and optimization
/// </summary>
public class FlowVariant : AuditableEntity
{
    public Guid BaseFlowId { get; set; }
    public virtual PromptFlow BaseFlow { get; set; } = null!;
    
    [Required]
    [StringLength(100)]
    public string VariantName { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// JSON description of what changed in this variant
    /// </summary>
    public string Changes { get; set; } = "{}";
    
    /// <summary>
    /// Variant configuration and modifications (JSON)
    /// </summary>
    public string VariantData { get; set; } = "{}";
    
    // A/B Testing Configuration
    /// <summary>
    /// Whether this variant is currently active in experiments
    /// </summary>
    public bool VariantIsActive { get; set; } = true;
    
    /// <summary>
    /// Percentage of traffic allocated to this variant (0-100)
    /// </summary>
    public double TrafficPercentage { get; set; } = 0.0;
    
    /// <summary>
    /// Priority for variant selection
    /// </summary>
    public int Priority { get; set; } = 0;
    
    // Performance Metrics
    public long ExecutionCount { get; set; } = 0;
    public decimal AverageCost { get; set; } = 0;
    public int AverageExecutionTime { get; set; } = 0;
    public decimal? QualityScore { get; set; }
    public decimal? ConversionRate { get; set; }
    public decimal? UserSatisfactionScore { get; set; }
    
    // Statistical Analysis
    /// <summary>
    /// Whether results are statistically significant
    /// </summary>
    public bool IsStatisticallySignificant { get; set; } = false;
    
    /// <summary>
    /// Confidence level for statistical significance (0-1)
    /// </summary>
    public decimal? ConfidenceLevel { get; set; }
    
    /// <summary>
    /// P-value for statistical testing
    /// </summary>
    public decimal? PValue { get; set; }
    
    // Navigation properties
    public virtual ICollection<FlowExecution> Executions { get; set; } = new List<FlowExecution>();
}
