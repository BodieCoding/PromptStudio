using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Enhanced PromptTemplate with enterprise features, versioning, and performance optimization
/// Templates are reusable prompts with variables, supporting collaborative development
/// </summary>
public class PromptTemplate : AuditableEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// The library this template belongs to
    /// </summary>
    public Guid PromptLibraryId { get; set; }
    public virtual PromptLibrary PromptLibrary { get; set; } = null!;
    
    /// <summary>
    /// Semantic version of the template (e.g., "1.2.0")
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// Base template for inheritance/forking scenarios
    /// </summary>
    public Guid? BaseTemplateId { get; set; }
    public virtual PromptTemplate? BaseTemplate { get; set; }
    
    /// <summary>
    /// Template status for approval workflows
    /// </summary>
    public TemplateStatus Status { get; set; } = TemplateStatus.Draft;
    
    /// <summary>
    /// Whether this template requires approval before publishing
    /// </summary>
    public bool RequiresApproval { get; set; } = false;
    
    /// <summary>
    /// Template category for organization
    /// </summary>
    public TemplateCategory Category { get; set; } = TemplateCategory.General;
    
    /// <summary>
    /// License type for sharing and reuse policies
    /// </summary>
    [StringLength(50)]
    public string? License { get; set; }
    
    /// <summary>
    /// Tags for discovery and categorization (JSON array)
    /// </summary>
    [StringLength(1000)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Expected language for the prompt output
    /// </summary>
    [StringLength(10)]
    public string? OutputLanguage { get; set; } = "en";
    
    /// <summary>
    /// Recommended AI providers for this template
    /// </summary>
    [StringLength(200)]
    public string? RecommendedProviders { get; set; }
    
    // Performance and usage metrics (updated via background processes)
    public long ExecutionCount { get; set; } = 0;
    public decimal AverageCost { get; set; } = 0;
    public int AverageResponseTimeMs { get; set; } = 0;
    public decimal? QualityScore { get; set; }
    public DateTime? LastExecutedAt { get; set; }
    
    /// <summary>
    /// Content hash for deduplication and change detection
    /// </summary>
    [StringLength(64)]
    public string? ContentHash { get; set; }
    
    /// <summary>
    /// Size category for resource allocation
    /// </summary>
    public TemplateSize Size { get; set; } = TemplateSize.Small;
    
    // Navigation properties
    public virtual PromptContent Content { get; set; } = null!;
    public virtual ICollection<PromptVariable> Variables { get; set; } = new List<PromptVariable>();
    public virtual ICollection<PromptExecution> Executions { get; set; } = new List<PromptExecution>();
    public virtual ICollection<VariableCollection> VariableCollections { get; set; } = new List<VariableCollection>();
    public virtual ICollection<TemplateVersion> Versions { get; set; } = new List<TemplateVersion>();
    public virtual ICollection<TemplatePermission> Permissions { get; set; } = new List<TemplatePermission>();
}
