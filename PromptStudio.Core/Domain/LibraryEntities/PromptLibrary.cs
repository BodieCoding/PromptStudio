using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents an organized collection of prompt templates within a PromptLab environment.
/// PromptLibraries provide structured organization, access control, and collaborative
/// management for related prompt templates and workflows in enterprise LLMOps scenarios.
/// </summary>
/// <remarks>
/// PromptLibrary serves as an organizational unit that groups related templates,
/// enabling team collaboration, permission management, and systematic prompt development.
/// Libraries support categorization, sharing policies, and integration with workflow
/// engines for comprehensive LLM application development.
/// </remarks>
public class PromptLibrary : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the prompt library.
    /// Provides a human-readable identifier for the library within its lab environment.
    /// </summary>
    /// <value>
    /// A descriptive name that should be unique within the parent PromptLab.
    /// Required field with maximum length of 100 characters.
    /// </value>
    /// <example>
    /// Examples: "Customer Support Templates", "Marketing Content Generation", "Code Review Assistants"
    /// </example>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional description explaining the library's purpose and scope.
    /// Provides context for library discovery and appropriate usage by team members.
    /// </summary>
    /// <value>
    /// A detailed description of the library's contents, intended use cases, and target audience.
    /// Optional field with maximum length of 500 characters.
    /// </value>
    /// <example>
    /// "Collection of tested prompt templates for customer service interactions, including escalation procedures and multilingual support"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the PromptLab containing this library.
    /// Establishes the hierarchical relationship and organizational context.
    /// </summary>
    /// <value>The GUID of the PromptLab that owns this library.</value>
    /// <remarks>
    /// Lab membership determines the broader organizational context, user access patterns,
    /// and integration with lab-level governance and analytics systems.
    /// </remarks>
    public Guid PromptLabId { get; set; }
    
    /// <summary>
    /// Gets or sets the category of this library for organizational and discovery purposes.
    /// Enables systematic classification and filtering of libraries within labs.
    /// </summary>
    /// <value>
    /// The LibraryCategory enum value indicating the library's functional domain
    /// (e.g., General, CustomerService, Marketing, Development, etc.).
    /// Defaults to General.
    /// </value>
    /// <remarks>
    /// Categories facilitate library discovery, permissions management,
    /// and organizational structure within prompt lab environments.
    /// </remarks>
    public LibraryCategory Category { get; set; } = LibraryCategory.General;
    
    /// <summary>
    /// Gets or sets the color identifier for visual representation in user interfaces.
    /// Provides visual distinction and branding for library identification.
    /// </summary>
    /// <value>
    /// A hexadecimal color code (e.g., "#3498db", "#e74c3c") for UI theming.
    /// Optional field with maximum length of 7 characters.
    /// </value>
    /// <example>
    /// Examples: "#3498db" (blue), "#e74c3c" (red), "#2ecc71" (green), "#f39c12" (orange)
    /// </example>
    /// <remarks>
    /// Color coding helps users quickly identify and distinguish between
    /// different libraries in visual interfaces and dashboards.
    /// </remarks>
    [StringLength(7)]
    public string? Color { get; set; } = "#1976d2";
    
    /// <summary>
    /// Gets or sets the icon identifier for visual representation in user interfaces.
    /// Provides semantic visual cues about the library's content or purpose.
    /// </summary>
    /// <value>
    /// An icon name or identifier compatible with the UI framework (e.g., Material Icons, FontAwesome).
    /// Optional field with maximum length of 50 characters. Defaults to "library_books".
    /// </value>
    /// <example>
    /// Examples: "library_books", "support_agent", "campaign", "code", "psychology", "business"
    /// </example>
    /// <remarks>
    /// Icons enhance user experience by providing quick visual identification
    /// of library types and purposes in navigation and selection interfaces.
    /// </remarks>
    [StringLength(50)]
    public string? Icon { get; set; } = "library_books";
    
    /// <summary>
    /// Gets or sets searchable tags for enhanced library discovery and organization.
    /// Enables flexible, user-defined classification beyond formal categories.
    /// </summary>
    /// <value>
    /// A JSON array of tag strings for search and discovery purposes.
    /// Optional field with maximum length of 1000 characters.
    /// </value>
    /// <example>
    /// ["customer-facing", "multilingual", "production-ready", "experimental", "ai-assisted"]
    /// </example>
    /// <remarks>
    /// Tags provide flexible metadata for advanced search, recommendation systems,
    /// and user-driven organization of library collections within labs.
    /// </remarks>
    [StringLength(1000)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Gets or sets the display order of this library within its parent lab.
    /// Determines the sequence in which libraries appear in user interfaces.
    /// </summary>
    /// <value>
    /// A numeric value for ordering (lower values appear first). Uses double for flexible ordering.
    /// Defaults to 0.0.
    /// </value>
    /// <remarks>
    /// Flexible ordering using doubles allows for easy reordering without affecting
    /// other items (e.g., inserting 1.5 between 1.0 and 2.0).
    /// </remarks>
    public double SortOrder { get; set; } = 0.0;
    
    /// <summary>
    /// Gets or sets a value indicating whether this library is pinned for quick access.
    /// Provides user-driven prioritization and quick access to frequently used libraries.
    /// </summary>
    /// <value>
    /// <c>true</c> if the library should appear in quick access areas;
    /// <c>false</c> for normal positioning. Defaults to <c>false</c>.
    /// </value>
    /// <remarks>
    /// Pinned libraries typically appear at the top of lists or in dedicated
    /// quick access sections for improved user productivity.
    /// </remarks>
    public bool IsPinned { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the visibility and sharing settings for this library.
    /// Controls who can discover, access, and interact with the library and its contents.
    /// </summary>
    /// <value>
    /// The LibraryVisibility enum value determining access scope
    /// (Private, Internal, Public, TeamShared). Defaults to Private.
    /// </value>
    /// <remarks>
    /// Visibility settings cascade to contained templates unless explicitly overridden.
    /// Public libraries enable organization-wide sharing and collaboration.
    /// </remarks>
    public LibraryVisibility Visibility { get; set; } = LibraryVisibility.Private;
    
    /// <summary>
    /// Gets or sets a value indicating whether template modifications require approval.
    /// Implements governance controls for critical or production library content.
    /// </summary>
    /// <value>
    /// <c>true</c> if template changes must go through approval workflows;
    /// <c>false</c> for direct modification. Defaults to <c>false</c>.
    /// </value>
    /// <remarks>
    /// Approval requirements are essential for production environments,
    /// regulated industries, or libraries containing critical business logic.
    /// </remarks>
    public bool RequiresApproval { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the current lifecycle status of this library.
    /// Manages library availability and operational state throughout its lifecycle.
    /// </summary>
    /// <value>
    /// The LibraryStatus enum value indicating operational state
    /// (Active, Archived, Deprecated, Maintenance, etc.). Defaults to Active.
    /// </value>
    /// <remarks>
    /// Status controls library availability, discoverability, and operational behavior.
    /// Archived libraries are preserved but hidden from normal operations.
    /// </remarks>
    public LibraryStatus Status { get; set; } = LibraryStatus.Active;
    
    /// <summary>
    /// Gets or sets the cached count of templates in this library for performance optimization.
    /// Provides quick access to template count without expensive join operations.
    /// </summary>
    /// <value>
    /// The number of templates currently contained in this library. Defaults to 0.
    /// Updated automatically via database triggers or background processes.
    /// </value>
    /// <remarks>
    /// Cached counts improve dashboard and listing performance by avoiding
    /// expensive aggregate queries for frequently accessed metrics.
    /// </remarks>
    public int TemplateCount { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the timestamp of the most recent activity in this library.
    /// Tracks library usage for analytics, sorting, and maintenance purposes.
    /// </summary>
    /// <value>
    /// The UTC timestamp of the last template creation, modification, or execution.
    /// Null if no activity has occurred since library creation.
    /// </value>
    /// <remarks>
    /// Activity timestamps enable "recently used" sorting, identify dormant libraries,
    /// and support usage analytics for organizational insights.
    /// </remarks>
    public DateTime? LastActivityAt { get; set; }
    
    // Navigation properties
    
    /// <summary>
    /// Gets or sets the navigation property to the parent prompt lab.
    /// Provides access to lab-level configuration, governance, and user management.
    /// </summary>
    /// <value>The PromptLab entity that contains this library.</value>
    public virtual PromptLab PromptLab { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the collection of prompt templates contained in this library.
    /// Represents the core content and value of the library for LLM operations.
    /// </summary>
    /// <value>A collection of PromptTemplate entities belonging to this library.</value>
    /// <remarks>
    /// Templates within a library share governance settings, permissions,
    /// and organizational context while maintaining individual execution history.
    /// </remarks>
    public virtual ICollection<PromptTemplate> PromptTemplates { get; set; } = new List<PromptTemplate>();
    
    /// <summary>
    /// Gets or sets the collection of permission records governing access to this library.
    /// Defines granular access control for viewing, modifying, and managing library content.
    /// </summary>
    /// <value>A collection of LibraryPermission entities defining access rights.</value>
    /// <remarks>
    /// Granular permissions enable secure collaboration, role-based access control,
    /// and compliance with organizational security requirements.
    /// </remarks>
    public virtual ICollection<LibraryPermission> Permissions { get; set; } = new List<LibraryPermission>();
}
