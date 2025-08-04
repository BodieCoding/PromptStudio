using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a collection of prompt templates for organizational and categorization purposes.
/// Collections enable grouping related templates together for better organization, management,
/// and access control in enterprise LLMOps environments.
/// </summary>
/// <remarks>
/// <para><strong>Domain Purpose:</strong></para>
/// <para>Collections serve as organizational containers for PromptTemplates, providing logical
/// grouping capabilities that support team collaboration, project organization, and hierarchical
/// template management. They enable bulk operations, shared permissions, and contextual organization
/// of related prompts within enterprise environments.</para>
/// 
/// <para><strong>Business Rules:</strong></para>
/// <list type="bullet">
/// <item><description>Collection names must be unique within the organization scope</description></item>
/// <item><description>Collections can contain zero or more PromptTemplates</description></item>
/// <item><description>PromptTemplates belong to exactly one Collection</description></item>
/// <item><description>Collections support hierarchical organization and inheritance</description></item>
/// <item><description>Access permissions can be applied at the Collection level</description></item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>One-to-many relationship with PromptTemplate entities</description></item>
/// <item><description>Supports cascade operations for bulk template management</description></item>
/// <item><description>Integrates with permission system for access control</description></item>
/// <item><description>Enables batch operations across related templates</description></item>
/// </list>
/// </remarks>
public class Collection : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the collection.
    /// Provides a human-readable identifier for the collection within its scope.
    /// </summary>
    /// <value>
    /// A descriptive name for the collection that should be unique within its scope.
    /// Required field with maximum length of 100 characters.
    /// </value>
    /// <example>
    /// Examples: "Customer Support Templates", "Marketing Content", "Technical Documentation"
    /// </example>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an optional description explaining the collection's purpose and contents.
    /// Provides context for collection discovery and appropriate usage.
    /// </summary>
    /// <value>
    /// A detailed description of what the collection contains and its intended use cases.
    /// Optional field with maximum length of 500 characters.
    /// </value>
    /// <example>
    /// "Standardized templates for customer support interactions including ticket responses, 
    /// escalation procedures, and follow-up communications"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the organizational status of the collection.
    /// Determines visibility, access patterns, and lifecycle management.
    /// </summary>
    /// <value>
    /// A CollectionStatus enumeration value indicating the current state.
    /// Defaults to Active for new collections.
    /// </value>
    /// <remarks>
    /// Status affects collection visibility, template access, and operational capabilities.
    /// Archived collections are read-only, while Draft collections may have limited visibility.
    /// </remarks>
    public CollectionStatus Status { get; set; } = CollectionStatus.Active;

    /// <summary>
    /// Gets or sets optional tags for enhanced categorization and discovery.
    /// Enables flexible taxonomy beyond hierarchical organization.
    /// </summary>
    /// <value>
    /// A comma-separated string of tags for cross-cutting categorization.
    /// Optional field with maximum length of 200 characters.
    /// </value>
    /// <example>
    /// "customer-facing, automated, high-priority, approved"
    /// </example>
    [StringLength(200)]
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets the display order for UI presentation and sorting.
    /// Enables custom ordering within collection hierarchies and listings.
    /// </summary>
    /// <value>
    /// An integer value for sort ordering. Lower values appear first.
    /// Defaults to 0 for new collections.
    /// </value>
    /// <remarks>
    /// Used by UI components for consistent collection ordering.
    /// Collections with the same order value are sorted alphabetically by name.
    /// </remarks>
    public int DisplayOrder { get; set; } = 0;

    /// <summary>
    /// Gets or sets whether this collection is marked as a favorite for quick access.
    /// Enables user-specific prioritization and rapid navigation.
    /// </summary>
    /// <value>
    /// Boolean indicating favorite status for enhanced discoverability.
    /// Defaults to false for new collections.
    /// </value>
    /// <remarks>
    /// Favorite status is typically user-specific in multi-tenant environments.
    /// May be extended to support organization-wide featured collections.
    /// </remarks>
    public bool IsFavorite { get; set; } = false;

    /// <summary>
    /// Gets or sets optional color coding for visual organization and identification.
    /// Supports UI customization and visual categorization patterns.
    /// </summary>
    /// <value>
    /// A hex color code string for UI theming and visual identification.
    /// Optional field with standard hex color format validation.
    /// </value>
    /// <example>
    /// "#FF5733" (orange), "#3498DB" (blue), "#2ECC71" (green)
    /// </example>
    [StringLength(7)] // #RRGGBB format
    public string? ColorCode { get; set; }

    #region Navigation Properties

    /// <summary>
    /// Gets or sets the collection of prompt templates belonging to this collection.
    /// Establishes the primary organizational relationship for template management.
    /// </summary>
    /// <value>
    /// A collection of PromptTemplate entities organized within this collection.
    /// Navigation property supporting Entity Framework relationships.
    /// </value>
    /// <remarks>
    /// <para><strong>Relationship Details:</strong></para>
    /// <list type="bullet">
    /// <item><description>One-to-many relationship (Collection -> PromptTemplates)</description></item>
    /// <item><description>Cascade delete behavior configurable via EF configuration</description></item>
    /// <item><description>Supports lazy loading and explicit include operations</description></item>
    /// <item><description>Enables bulk operations across related templates</description></item>
    /// </list>
    /// 
    /// <para><strong>Usage Patterns:</strong></para>
    /// <list type="bullet">
    /// <item><description>Template discovery and browsing within collections</description></item>
    /// <item><description>Bulk template operations (export, backup, permissions)</description></item>
    /// <item><description>Collection-scoped analytics and reporting</description></item>
    /// <item><description>Hierarchical template organization and management</description></item>
    /// </list>
    /// </remarks>
    public virtual ICollection<PromptTemplate> PromptTemplates { get; set; } = new List<PromptTemplate>();

    #endregion

    #region Computed Properties

    /// <summary>
    /// Gets the total count of prompt templates in this collection.
    /// Provides quick access to collection size without full template loading.
    /// </summary>
    /// <value>
    /// Integer count of templates currently belonging to this collection.
    /// Computed property that may require database query if templates not loaded.
    /// </value>
    /// <remarks>
    /// This property may trigger a database query if PromptTemplates navigation
    /// property is not loaded. Consider using explicit counts in performance-critical scenarios.
    /// </remarks>
    public int TemplateCount => PromptTemplates?.Count ?? 0;

    /// <summary>
    /// Gets a value indicating whether this collection contains any prompt templates.
    /// Provides efficient empty collection detection.
    /// </summary>
    /// <value>
    /// Boolean indicating whether the collection has any associated templates.
    /// Computed property optimized for existence checking.
    /// </value>
    /// <remarks>
    /// More efficient than checking TemplateCount > 0 for existence queries.
    /// May be optimized by Entity Framework depending on query context.
    /// </remarks>
    public bool HasTemplates => PromptTemplates?.Any() ?? false;

    /// <summary>
    /// Gets a formatted display name combining name and template count.
    /// Provides user-friendly representation for UI components.
    /// </summary>
    /// <value>
    /// Formatted string including collection name and template count.
    /// Example: "Customer Support (12 templates)"
    /// </value>
    /// <remarks>
    /// Useful for dropdown lists, navigation menus, and summary displays.
    /// Template count reflects current loaded state, may not reflect database state.
    /// </remarks>
    public string DisplayName => $"{Name} ({TemplateCount} template{(TemplateCount == 1 ? "" : "s")})";

    #endregion

    #region Business Methods

    /// <summary>
    /// Determines whether this collection can be deleted based on business rules.
    /// Validates deletion constraints including template dependencies and status.
    /// </summary>
    /// <returns>True if the collection can be safely deleted, false otherwise</returns>
    /// <remarks>
    /// <para><strong>Deletion Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Empty collections can always be deleted</description></item>
    /// <item><description>Collections with templates require special permissions</description></item>
    /// <item><description>System collections may be protected from deletion</description></item>
    /// <item><description>Archived collections have different deletion rules</description></item>
    /// </list>
    /// </remarks>
    public bool CanBeDeleted()
    {
        // Basic rule: can delete if no templates or if status allows it
        return !HasTemplates || Status == CollectionStatus.Draft;
    }

    /// <summary>
    /// Updates the collection's display order for hierarchical sorting.
    /// Provides controlled ordering updates with validation.
    /// </summary>
    /// <param name="newOrder">The new display order value</param>
    /// <returns>True if the order was updated successfully</returns>
    /// <remarks>
    /// Order values should be unique within the same organizational scope.
    /// Negative values are reserved for system collections.
    /// </remarks>
    public bool UpdateDisplayOrder(int newOrder)
    {
        if (newOrder < 0 && Status != CollectionStatus.System)
        {
            return false; // Negative orders reserved for system collections
        }

        DisplayOrder = newOrder;
        return true;
    }

    /// <summary>
    /// Toggles the favorite status of this collection.
    /// Provides controlled favorite management with state tracking.
    /// </summary>
    /// <returns>The new favorite status after toggling</returns>
    /// <remarks>
    /// In multi-tenant environments, favorite status may be user-specific.
    /// This method provides the basic toggle mechanism for implementation.
    /// </remarks>
    public bool ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
        return IsFavorite;
    }

    #endregion

    /// <summary>
    /// Returns a string representation of the collection for debugging and logging.
    /// Provides essential identifying information in a readable format.
    /// </summary>
    /// <returns>String representation including ID, name, and template count</returns>
    public override string ToString()
    {
        return $"Collection [ID: {Id}] {Name} - {TemplateCount} templates ({Status})";
    }
}
