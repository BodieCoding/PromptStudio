using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Enhanced PromptLibrary with enterprise-grade features for scalability and multi-tenancy
/// Libraries provide organized structure for prompt templates and workflows within a PromptLab
/// </summary>
public class PromptLibrary : AuditableEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// The PromptLab this library belongs to (using Guid for scalability)
    /// </summary>
    public Guid PromptLabId { get; set; }
    
    /// <summary>
    /// Library category for better organization and filtering
    /// </summary>
    public LibraryCategory Category { get; set; } = LibraryCategory.General;
    
    /// <summary>
    /// Library color for visual identification (hex color code)
    /// </summary>
    [StringLength(7)]
    public string? Color { get; set; } = "#1976d2";
    
    /// <summary>
    /// Icon identifier for visual representation
    /// </summary>
    [StringLength(50)]
    public string? Icon { get; set; } = "library_books";
    
    /// <summary>
    /// Tags for categorization and search (stored as JSON array for better querying)
    /// Format: ["tag1", "tag2", "tag3"]
    /// </summary>
    [StringLength(1000)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Display order within the prompt lab (using double for flexible ordering)
    /// </summary>
    public double SortOrder { get; set; } = 0.0;
    
    /// <summary>
    /// Whether this library is pinned/favorited for quick access
    /// </summary>
    public bool IsPinned { get; set; } = false;
    
    /// <summary>
    /// Library visibility and sharing settings
    /// </summary>
    public LibraryVisibility Visibility { get; set; } = LibraryVisibility.Private;
    
    /// <summary>
    /// Whether this library requires approval for template modifications
    /// </summary>
    public bool RequiresApproval { get; set; } = false;
    
    /// <summary>
    /// Library status for lifecycle management
    /// </summary>
    public LibraryStatus Status { get; set; } = LibraryStatus.Active;
    
    /// <summary>
    /// Template count cache for performance (updated via database triggers)
    /// </summary>
    public int TemplateCount { get; set; } = 0;
    
    /// <summary>
    /// Last activity timestamp for sorting and analytics
    /// </summary>
    public DateTime? LastActivityAt { get; set; }
    
    // Navigation properties
    public virtual PromptLab PromptLab { get; set; } = null!;
    public virtual ICollection<PromptTemplate> PromptTemplates { get; set; } = new List<PromptTemplate>();
    public virtual ICollection<LibraryPermission> Permissions { get; set; } = new List<LibraryPermission>();
}
