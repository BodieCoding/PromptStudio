// Enhanced PromptLab with Guid identifier and enterprise-scale considerations
using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// A PromptLab is the top-level organizational unit for prompt engineering work.
/// Similar to research labs or GCP projects, it provides isolation, ownership, and governance.
/// PromptLabs contain PromptLibraries, which contain PromptTemplates and PromptFlows.
/// </summary>
public class PromptLab
{
    /// <summary>
    /// Unique identifier for the PromptLab.
    /// Using Guid for global uniqueness, security, and distributed system compatibility.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Lab identifier - unique, URL-friendly identifier (like GitHub repo names)
    /// Format: lowercase, hyphens allowed, 3-50 characters
    /// Examples: my-chatbot-prompts, sales-ai-workflows, content-generation
    /// This provides human-readable URLs while maintaining Guid-based internal references
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [RegularExpression(@"^[a-z][a-z0-9-]*[a-z0-9]$", 
        ErrorMessage = "Lab ID must be lowercase, start with a letter, and contain only letters, numbers, and hyphens")]
    public string LabId { get; set; } = string.Empty;

    /// <summary>
    /// Owner of the lab - will be expanded to support users/teams later
    /// For enterprise scale, this will become a foreign key to User/Team entities
    /// </summary>
    [StringLength(100)]
    public string? Owner { get; set; }
    
    /// <summary>
    /// Organization/Tenant ID for multi-tenant isolation
    /// Critical for enterprise deployments and data isolation
    /// </summary>
    public Guid? OrganizationId { get; set; }
    
    /// <summary>
    /// Lab status for lifecycle management
    /// </summary>
    public LabStatus Status { get; set; } = LabStatus.Active;
    
    /// <summary>
    /// Tags for categorization and discovery
    /// JSON array format for better querying and indexing
    /// </summary>
    [StringLength(1000)]
    public string? Tags { get; set; }

    /// <summary>
    /// Lab visibility and access control
    /// </summary>
    public LabVisibility Visibility { get; set; } = LabVisibility.Private;
    
    /// <summary>
    /// Soft delete timestamp for audit and recovery
    /// Supports enterprise compliance and data governance requirements
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    
    /// <summary>
    /// Row version for optimistic concurrency control
    /// Critical for collaborative editing scenarios
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties - all using Guid foreign keys
    public virtual ICollection<PromptLibrary> PromptLibraries { get; set; } = new List<PromptLibrary>();
    public virtual ICollection<PromptFlow> PromptFlows { get; set; } = new List<PromptFlow>();
}