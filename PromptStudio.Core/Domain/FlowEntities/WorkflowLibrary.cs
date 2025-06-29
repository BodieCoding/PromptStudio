using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Organizational structure for workflow management within PromptLabs
/// Provides hierarchical organization for complex workflow ecosystems
/// </summary>
public class WorkflowLibrary : AuditableEntity
{
    /// <summary>
    /// Display name of the workflow library
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed description of the workflow library's purpose
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// The PromptLab this workflow library belongs to
    /// </summary>
    public Guid PromptLabId { get; set; }
    public virtual PromptLab PromptLab { get; set; } = null!;
    
    /// <summary>
    /// Library category for better organization and filtering
    /// </summary>
    public WorkflowCategory Category { get; set; } = WorkflowCategory.General;
    
    /// <summary>
    /// Library color for visual identification (hex color code)
    /// </summary>
    [StringLength(7)]
    public string? Color { get; set; } = "#1976d2";
    
    /// <summary>
    /// Icon identifier for visual representation
    /// </summary>
    [StringLength(50)]
    public string? Icon { get; set; } = "account_tree";
    
    /// <summary>
    /// Tags for categorization and search (stored as JSON array)
    /// </summary>
    [StringLength(1000)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Display order within the prompt lab
    /// </summary>
    public double SortOrder { get; set; } = 0.0;
    
    /// <summary>
    /// Whether this library is pinned for quick access
    /// </summary>
    public bool IsPinned { get; set; } = false;
    
    /// <summary>
    /// Library visibility and sharing settings
    /// </summary>
    public LibraryVisibility Visibility { get; set; } = LibraryVisibility.Private;
    
    /// <summary>
    /// Whether workflows in this library require approval for modifications
    /// </summary>
    public bool RequiresApproval { get; set; } = false;
    
    /// <summary>
    /// Library status for lifecycle management
    /// </summary>
    public LibraryStatus Status { get; set; } = LibraryStatus.Active;
    
    /// <summary>
    /// Workflow count cache for performance
    /// </summary>
    public int WorkflowCount { get; set; } = 0;
    
    /// <summary>
    /// Last activity timestamp for sorting and analytics
    /// </summary>
    public DateTime? LastActivityAt { get; set; }
    
    /// <summary>
    /// Default execution timeout for workflows in this library (seconds)
    /// </summary>
    public int? DefaultTimeoutSeconds { get; set; }
    
    /// <summary>
    /// Default cost limit for workflow executions in this library
    /// </summary>
    public decimal? DefaultCostLimit { get; set; }
    
    // Navigation properties
    public virtual ICollection<PromptFlow> PromptFlows { get; set; } = new List<PromptFlow>();
    public virtual ICollection<WorkflowLibraryPermission> Permissions { get; set; } = new List<WorkflowLibraryPermission>();
}

/// <summary>
/// Workflow categories for comprehensive organization
/// </summary>
public enum WorkflowCategory
{
    General = 0,
    DataProcessing = 1,
    ContentGeneration = 2,
    Analysis = 3,
    Automation = 4,
    Integration = 5,
    Testing = 6,
    Monitoring = 7,
    Reporting = 8,
    CustomerService = 9,
    Marketing = 10,
    Development = 11,
    Operations = 12,
    Research = 13,
    Training = 14,
    Compliance = 15,
    Security = 16,
    Finance = 17,
    HumanResources = 18,
    Quality = 19
}

/// <summary>
/// Workflow approval and execution status
/// </summary>
public enum WorkflowStatus
{
    Draft = 0,
    UnderReview = 1,
    Approved = 2,
    Published = 3,
    Running = 4,
    Paused = 5,
    Stopped = 6,
    Error = 7,
    Deprecated = 8,
    Archived = 9
}
