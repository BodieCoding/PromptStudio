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
    /// Gets or sets a value indicating whether this template requires approval before being published.
    /// Controls the governance workflow for template lifecycle management.
    /// </summary>
    /// <value>
    /// <c>true</c> if the template must go through an approval process before publication;
    /// <c>false</c> if it can be published directly. Defaults to <c>false</c>.
    /// </value>
    /// <remarks>
    /// Approval requirements are typically configured at the organizational level
    /// for sensitive use cases, production environments, or regulated industries.
    /// </remarks>
    public bool RequiresApproval { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the category of this template for organizational and discovery purposes.
    /// Enables systematic organization and filtering of templates within libraries.
    /// </summary>
    /// <value>
    /// The TemplateCategory enum value indicating the template's functional domain
    /// (e.g., General, CustomerService, ContentGeneration, CodeReview, etc.).
    /// Defaults to General.
    /// </value>
    /// <remarks>
    /// Categories facilitate template discovery, permissions management,
    /// and organizational structure within prompt libraries.
    /// </remarks>
    public TemplateCategory Category { get; set; } = TemplateCategory.General;
    
    /// <summary>
    /// Gets or sets the license type governing sharing and reuse policies for this template.
    /// Defines the legal and usage terms for template distribution and modification.
    /// </summary>
    /// <value>
    /// A license identifier or description (e.g., "MIT", "Apache-2.0", "Proprietary", "CC-BY-4.0").
    /// Optional field with maximum length of 50 characters.
    /// </value>
    /// <example>
    /// Examples: "MIT", "Apache-2.0", "Proprietary", "CC-BY-4.0", "Internal-Use-Only"
    /// </example>
    /// <remarks>
    /// License information is crucial for template sharing between organizations
    /// and compliance with intellectual property requirements.
    /// </remarks>
    [StringLength(50)]
    public string? License { get; set; }
    
    /// <summary>
    /// Gets or sets searchable tags for enhanced template discovery and categorization.
    /// Enables flexible, user-defined classification beyond formal categories.
    /// </summary>
    /// <value>
    /// A JSON array of tag strings for search and discovery purposes.
    /// Optional field with maximum length of 1000 characters.
    /// </value>
    /// <example>
    /// ["customer-support", "empathy", "multilingual", "urgent-response"]
    /// </example>
    /// <remarks>
    /// Tags provide flexible metadata for advanced search, recommendation systems,
    /// and user-driven organization of template collections.
    /// </remarks>
    [StringLength(1000)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Gets or sets the expected output language for this template's generated content.
    /// Helps with template selection and language-specific optimization.
    /// </summary>
    /// <value>
    /// An ISO 639-1 language code (e.g., "en", "es", "fr", "de").
    /// Defaults to "en" (English). Maximum length is 10 characters.
    /// </value>
    /// <example>
    /// Examples: "en" (English), "es" (Spanish), "fr" (French), "zh" (Chinese), "multi" (multilingual)
    /// </example>
    /// <remarks>
    /// Language specification enables automatic model selection, regional compliance,
    /// and user experience optimization for international deployments.
    /// </remarks>
    [StringLength(10)]
    public string? OutputLanguage { get; set; } = "en";
    
    /// <summary>
    /// Gets or sets the recommended AI providers optimized for this template.
    /// Guides provider selection for optimal performance and cost efficiency.
    /// </summary>
    /// <value>
    /// A comma-separated list of provider names or identifiers.
    /// Optional field with maximum length of 200 characters.
    /// </value>
    /// <example>
    /// Examples: "openai,anthropic", "azure-openai", "bedrock", "openai-gpt4,openai-gpt3.5"
    /// </example>
    /// <remarks>
    /// Provider recommendations are based on template testing, performance analysis,
    /// and cost optimization across different LLM providers and models.
    /// </remarks>
    [StringLength(200)]
    public string? RecommendedProviders { get; set; }
    
    // Performance and usage metrics (updated via background processes)
    
    /// <summary>
    /// Gets or sets the total number of times this template has been executed.
    /// Tracks template usage for analytics and optimization purposes.
    /// </summary>
    /// <value>
    /// The cumulative execution count across all environments and users. Defaults to 0.
    /// </value>
    /// <remarks>
    /// Updated automatically by execution services. High execution counts indicate
    /// valuable templates that may warrant additional optimization investment.
    /// </remarks>
    public long ExecutionCount { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the average cost per execution for this template.
    /// Enables cost tracking and optimization for resource-intensive templates.
    /// </summary>
    /// <value>
    /// The average monetary cost per execution in the system's base currency.
    /// Includes provider API costs, compute overhead, and processing fees. Defaults to 0.
    /// </value>
    /// <remarks>
    /// Critical for budget management and cost optimization decisions.
    /// Templates with high costs may be candidates for prompt engineering or provider switching.
    /// </remarks>
    public decimal AverageCost { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the average response time for template executions in milliseconds.
    /// Tracks performance characteristics for latency-sensitive applications.
    /// </summary>
    /// <value>
    /// The average execution duration from request to response in milliseconds. Defaults to 0.
    /// </value>
    /// <remarks>
    /// Response time metrics help identify performance bottlenecks and guide
    /// optimization efforts for time-critical use cases.
    /// </remarks>
    public int AverageResponseTimeMs { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the quality score for this template based on evaluation metrics.
    /// Provides a quantitative measure of template effectiveness and reliability.
    /// </summary>
    /// <value>
    /// A quality score typically ranging from 0.0 to 1.0 or 0 to 100, depending on scoring system.
    /// Null if quality assessment has not been performed.
    /// </value>
    /// <remarks>
    /// Quality scores may be derived from automated testing, user feedback,
    /// expert evaluation, or A/B testing results.
    /// </remarks>
    public decimal? QualityScore { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp of the most recent execution of this template.
    /// Helps identify active versus dormant templates for maintenance purposes.
    /// </summary>
    /// <value>
    /// The UTC timestamp of the last execution, or null if never executed.
    /// </value>
    /// <remarks>
    /// Used for lifecycle management, identifying unused templates,
    /// and prioritizing optimization efforts on actively used templates.
    /// </remarks>
    public DateTime? LastExecutedAt { get; set; }
    
    /// <summary>
    /// Gets or sets a content hash for deduplication and change detection.
    /// Enables efficient comparison and versioning of template content.
    /// </summary>
    /// <value>
    /// A cryptographic hash (e.g., SHA-256) of the template content for integrity checking.
    /// Optional field with maximum length of 64 characters.
    /// </value>
    /// <remarks>
    /// Content hashes enable deduplication of similar templates, change detection
    /// for version control, and integrity verification for critical templates.
    /// </remarks>
    [StringLength(64)]
    public string? ContentHash { get; set; }
    
    /// <summary>
    /// Gets or sets the size category of this template for resource allocation and optimization.
    /// Helps the system allocate appropriate resources and set execution parameters.
    /// </summary>
    /// <value>
    /// The TemplateSize enum value indicating the template's complexity and resource requirements
    /// (Small, Medium, Large, ExtraLarge). Defaults to Small.
    /// </value>
    /// <remarks>
    /// Size categorization affects timeout settings, resource allocation,
    /// and pricing calculations for template execution.
    /// </remarks>
    public TemplateSize Size { get; set; } = TemplateSize.Small;
    
    // Navigation properties
    
    /// <summary>
    /// Gets or sets the navigation property to the template's content and structure.
    /// Provides access to the actual prompt text, variables, and formatting.
    /// </summary>
    /// <value>The PromptContent entity containing the template's executable content.</value>
    /// <remarks>
    /// Content is separated into its own entity to enable efficient querying
    /// and potential content versioning strategies.
    /// </remarks>
    public virtual PromptContent Content { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the collection of variables defined in this template.
    /// Represents the parameterizable elements that make the template reusable.
    /// </summary>
    /// <value>A collection of PromptVariable entities defining template parameters.</value>
    /// <remarks>
    /// Variables enable template reusability by allowing dynamic content substitution.
    /// Each variable defines type, validation rules, and default values.
    /// </remarks>
    public virtual ICollection<PromptVariable> Variables { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of execution records for this template.
    /// Provides detailed history of all template execution attempts and outcomes.
    /// </summary>
    /// <value>A collection of PromptExecution entities representing execution history.</value>
    /// <remarks>
    /// Execution history is essential for performance analysis, debugging,
    /// quality assessment, and compliance auditing.
    /// </remarks>
    public virtual ICollection<PromptExecution> Executions { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of variable collections associated with this template.
    /// Represents curated sets of variable values for batch testing and execution.
    /// </summary>
    /// <value>A collection of VariableCollection entities for batch operations.</value>
    /// <remarks>
    /// Variable collections enable batch testing, A/B testing, and systematic
    /// evaluation of template performance across different input scenarios.
    /// </remarks>
    public virtual ICollection<VariableCollection> VariableCollections { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of version history records for this template.
    /// Tracks the evolution of the template over time for audit and rollback purposes.
    /// </summary>
    /// <value>A collection of TemplateVersion entities representing version history.</value>
    /// <remarks>
    /// Version tracking enables rollback capabilities, change impact analysis,
    /// and compliance with version control requirements.
    /// </remarks>
    public virtual ICollection<TemplateVersion> Versions { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of permission records governing access to this template.
    /// Defines who can view, execute, modify, or manage the template.
    /// </summary>
    /// <value>A collection of TemplatePermission entities defining access control.</value>
    /// <remarks>
    /// Granular permissions enable secure collaboration and governance
    /// in multi-tenant and team-based environments.
    /// </remarks>
    public virtual ICollection<TemplatePermission> Permissions { get; set; } = [];
}
