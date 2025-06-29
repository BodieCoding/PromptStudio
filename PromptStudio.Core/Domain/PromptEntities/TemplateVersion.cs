using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Version history for prompt templates supporting collaborative development
/// </summary>
public class TemplateVersion : AuditableEntity
{
    /// <summary>
    /// Reference to the prompt template
    /// </summary>
    public Guid PromptTemplateId { get; set; }
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
    
    /// <summary>
    /// Semantic version identifier (e.g., "1.2.0")
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Version display name
    /// </summary>
    [StringLength(100)]
    public string? VersionName { get; set; }
    
    /// <summary>
    /// Snapshot of the template content at this version
    /// </summary>
    [Required]
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Template metadata snapshot (JSON)
    /// </summary>
    public string? MetadataSnapshot { get; set; }
    
    /// <summary>
    /// Variable definitions snapshot (JSON)
    /// </summary>
    public string? VariablesSnapshot { get; set; }
    
    /// <summary>
    /// Change notes for this version
    /// </summary>
    [StringLength(1000)]
    public string? ChangeNotes { get; set; }
    
    /// <summary>
    /// Type of change (major, minor, patch, hotfix)
    /// </summary>
    public VersionChangeType ChangeType { get; set; } = VersionChangeType.Minor;
    
    /// <summary>
    /// Whether this version is marked as stable/release
    /// </summary>
    public bool IsStable { get; set; } = false;
    
    /// <summary>
    /// Whether this is the current active version
    /// </summary>
    public bool IsCurrent { get; set; } = false;
    
    /// <summary>
    /// Parent version (for branching/forking scenarios)
    /// </summary>
    public Guid? ParentVersionId { get; set; }
    public virtual TemplateVersion? ParentVersion { get; set; }
    
    /// <summary>
    /// Size of this version's content
    /// </summary>
    public long ContentSize { get; set; } = 0;
    
    /// <summary>
    /// Content hash for integrity and comparison
    /// </summary>
    [StringLength(64)]
    public string? ContentHash { get; set; }
    
    /// <summary>
    /// Approval status for this version
    /// </summary>
    public VersionApprovalStatus ApprovalStatus { get; set; } = VersionApprovalStatus.Draft;
    
    /// <summary>
    /// Who approved this version
    /// </summary>
    [StringLength(100)]
    public string? ApprovedBy { get; set; }
    
    /// <summary>
    /// When this version was approved
    /// </summary>
    public DateTime? ApprovedAt { get; set; }
    
    /// <summary>
    /// Performance metrics for this version
    /// </summary>
    public decimal? AverageCost { get; set; }
    public int? AverageResponseTime { get; set; }
    public decimal? QualityScore { get; set; }
    public long ExecutionCount { get; set; } = 0;
}