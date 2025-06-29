using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a reusable prompt template with enterprise-grade features including versioning,
/// collaboration, approval workflows, and comprehensive performance tracking.
/// PromptTemplates are the core assets in LLMOps, enabling structured prompt development,
/// testing, optimization, and deployment across teams and applications.
/// </summary>
/// <remarks>
/// PromptTemplate supports advanced features like variable substitution, content versioning,
/// approval workflows, performance analytics, and collaborative development. Templates can
/// inherit from base templates, track execution metrics, and integrate with workflow engines
/// for sophisticated LLM application development.
/// </remarks>
public class PromptTemplate : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the prompt template.
    /// Provides a human-readable identifier for the template within its library.
    /// </summary>
    /// <value>
    /// A descriptive name for the template that should be unique within its library.
    /// Required field with maximum length of 100 characters.
    /// </value>
    /// <example>
    /// Examples: "Customer Support Response", "Code Review Checklist", "Content Summarization"
    /// </example>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional description explaining the template's purpose and usage.
    /// Provides context for template discovery and appropriate usage.
    /// </summary>
    /// <value>
    /// A detailed description of what the template does, when to use it, and any special considerations.
    /// Optional field with maximum length of 500 characters.
    /// </value>
    /// <example>
    /// "Generates empathetic customer support responses based on inquiry type and customer history"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the prompt library containing this template.
    /// Establishes the organizational hierarchy and access control context.
    /// </summary>
    /// <value>The GUID of the PromptLibrary that owns this template.</value>
    /// <remarks>
    /// Library membership determines access permissions, visibility scope,
    /// and organizational categorization for the template.
    /// </remarks>
    public Guid PromptLibraryId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the parent prompt library.
    /// Provides access to library-level configuration, permissions, and metadata.
    /// </summary>
    /// <value>The PromptLibrary entity that contains this template.</value>
    public virtual PromptLibrary PromptLibrary { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the semantic version of this template following semver conventions.
    /// Enables version tracking, compatibility checking, and rollback capabilities.
    /// </summary>
    /// <value>
    /// A semantic version string (e.g., "1.2.0", "2.0.0-beta.1").
    /// Required field with maximum length of 20 characters. Defaults to "1.0.0".
    /// </value>
    /// <example>
    /// Examples: "1.0.0" (initial), "1.1.0" (new features), "2.0.0" (breaking changes), "1.0.1" (bug fixes)
    /// </example>
    /// <remarks>
    /// Version changes should follow semantic versioning principles:
    /// MAJOR.MINOR.PATCH where MAJOR = breaking changes, MINOR = new features, PATCH = bug fixes.
    /// </remarks>
    [Required]
    [StringLength(20)]
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// Gets or sets the unique identifier of the base template for inheritance scenarios.
    /// Enables template forking, inheritance, and derivative template relationships.
    /// </summary>
    /// <value>
    /// The GUID of the PromptTemplate that serves as the base for this template.
    /// Null if this template is not derived from another template.
    /// </value>
    /// <remarks>
    /// Base template relationships enable template evolution tracking,
    /// change impact analysis, and inheritance-based template development workflows.
    /// </remarks>
    public Guid? BaseTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the base template.
    /// Provides access to the template from which this one was derived.
    /// </summary>
    /// <value>The base PromptTemplate entity, or null if this template is not derived.</value>
    public virtual PromptTemplate? BaseTemplate { get; set; }
    
    /// <summary>
    /// Gets or sets the current status of this template in the approval workflow.
    /// Determines the template's readiness for production use and visibility.
    /// </summary>
    /// <value>
    /// The TemplateStatus indicating the template's lifecycle stage (Draft, Review, Published, etc.).
    /// Defaults to Draft.
    /// </value>
    /// <remarks>
    /// Status controls template visibility, executability, and approval workflow progression.
    /// Only Published templates are typically available for production use.
    /// </remarks>
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
