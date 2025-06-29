using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Tracks the association between workflows and prompt templates for traceability
/// </summary>
public class WorkflowTemplateUsage : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    public Guid TemplateId { get; set; }
    public virtual PromptTemplate Template { get; set; } = null!;
    
    public Guid NodeId { get; set; }
    public virtual FlowNode Node { get; set; } = null!;
    
    /// <summary>
    /// Version of the template when it was added to the workflow
    /// </summary>
    [Required]
    [StringLength(20)]
    public string TemplateVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Snapshot of template content when it was added
    /// </summary>
    public string? TemplateSnapshot { get; set; }
    
    /// <summary>
    /// Role of the template in this node context
    /// </summary>
    [StringLength(50)]
    public string NodeRole { get; set; } = "primary"; // primary, fallback, validation
    
    /// <summary>
    /// Whether this template usage is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Configuration for how the template is used in this context (JSON)
    /// </summary>
    public string? UsageConfiguration { get; set; }
    
    // Performance in this workflow context
    public long ExecutionCount { get; set; } = 0;
    public decimal AverageCost { get; set; } = 0;
    public int AverageExecutionTime { get; set; } = 0;
    public decimal? QualityScore { get; set; }
}
