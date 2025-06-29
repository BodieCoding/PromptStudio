// Enhanced PromptLab with Guid identifier and enterprise-scale considerations
using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the top-level organizational unit for prompt engineering work in enterprise LLMOps.
/// PromptLabs provide complete isolation, ownership, governance, and collaboration boundaries
/// for prompt development teams, similar to research labs, GitHub organizations, or GCP projects.
/// Each lab contains libraries, templates, workflows, and associated governance policies.
/// </summary>
/// <remarks>
/// PromptLab serves as the primary tenant boundary in multi-tenant LLMOps environments,
/// providing security isolation, resource management, and collaborative workspaces.
/// Labs enable organizations to separate different projects, teams, or business units
/// while maintaining centralized governance and analytics capabilities.
/// </remarks>
public class PromptLab
{
    /// <summary>
    /// Gets or sets the unique identifier for this PromptLab.
    /// Provides globally unique identification for distributed systems and security.
    /// </summary>
    /// <value>
    /// A GUID that uniquely identifies this lab across all systems and environments.
    /// Auto-generated on creation to ensure global uniqueness.
    /// </value>
    /// <remarks>
    /// GUID-based identifiers enable secure, distributed operation and prevent
    /// enumeration attacks while supporting database sharding and replication.
    /// </remarks>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Gets or sets the human-readable name of the PromptLab.
    /// Provides a descriptive identifier for display and organizational purposes.
    /// </summary>
    /// <value>
    /// A descriptive name that should be meaningful to users and administrators.
    /// Required field with maximum length of 100 characters.
    /// </value>
    /// <example>
    /// Examples: "Customer Service AI", "Marketing Content Generation", "Legal Document Review"
    /// </example>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional description explaining the lab's purpose and scope.
    /// Provides context for lab discovery and understanding of its intended use.
    /// </summary>
    /// <value>
    /// A detailed description of the lab's objectives, team, and intended use cases.
    /// Optional field with maximum length of 500 characters.
    /// </value>
    /// <example>
    /// "Collaborative workspace for developing and testing AI-powered customer service responses, escalation procedures, and multilingual support templates"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the URL-friendly unique identifier for this lab.
    /// Provides human-readable URLs and API endpoints while maintaining GUID-based internal references.
    /// </summary>
    /// <value>
    /// A lowercase, hyphen-separated identifier that must be unique across the platform.
    /// Required field with length between 3-50 characters, following specific format rules.
    /// </value>
    /// <example>
    /// Examples: "customer-support-ai", "marketing-content-gen", "legal-document-review"
    /// </example>
    /// <remarks>
    /// Lab IDs enable friendly URLs (e.g., /labs/customer-support-ai) while maintaining
    /// security through GUID-based internal operations. Format restrictions ensure
    /// URL compatibility and consistent naming conventions.
    /// </remarks>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [RegularExpression(@"^[a-z][a-z0-9-]*[a-z0-9]$", 
        ErrorMessage = "Lab ID must be lowercase, start with a letter, and contain only letters, numbers, and hyphens")]
    public string LabId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the lab owner for ownership and access control.
    /// Will be expanded to support user and team entities in future enterprise implementations.
    /// </summary>
    /// <value>
    /// An identifier for the lab owner (user ID, email, or team identifier).
    /// Will become a foreign key reference to User/Team entities in enterprise versions.
    /// Required field with maximum length of 100 characters.
    /// </value>
    /// <remarks>
    /// Owner identification is critical for access control, governance, and administrative operations.
    /// Future versions will implement proper user management with role-based access control.
    /// </remarks>
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