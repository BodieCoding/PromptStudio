using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a flexible category system for organizing prompt workflows in enterprise environments.
/// 
/// <para><strong>Business Context:</strong></para>
/// WorkflowCategory enables both system-defined and user-defined categorization schemes,
/// supporting diverse organizational needs and custom taxonomies while maintaining
/// consistency through predefined categories for common workflow types. This flexible approach
/// accommodates enterprise requirements for both standardization and customization in
/// complex workflow ecosystems.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity supports hierarchical categorization with parent-child relationships,
/// system vs. user-defined categories, and multi-tenant isolation. It provides
/// comprehensive category management with ordering, descriptions, and validation
/// while maintaining performance through proper indexing and workflow organization.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Flexible categorization supporting both standard and custom workflow categories
/// - Hierarchical organization for complex workflow taxonomies
/// - Multi-tenant category isolation and management
/// - User-friendly category creation and workflow organization
/// - System-provided categories for common workflow patterns
/// - Enterprise-ready with audit trails and governance
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Composite Pattern: Hierarchical category structures with parent-child relationships
/// - Strategy Pattern: Different category types (system vs. user-defined)
/// - Multi-tenancy: Tenant-specific category isolation
/// - Soft Delete: Category preservation for workflow data integrity
/// - Audit Trail: Complete category change tracking
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index Name and ParentId for efficient category tree queries
/// - Consider materialized path or nested sets for deep hierarchies
/// - Cache frequently accessed category trees for performance
/// - System categories can be cached globally across tenants
/// - Soft delete preserves referential integrity while allowing cleanup
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Workflow Management: Category assignment and filtering
/// - User Interface: Dynamic category selection and hierarchy display
/// - Search System: Category-based filtering and faceted search
/// - Admin Interface: Category management and hierarchy maintenance
/// - Analytics Platform: Usage tracking by workflow category
/// </remarks>
/// <example>
/// <code>
/// // Creating a system-defined workflow category
/// var systemCategory = new WorkflowCategory
/// {
///     Name = "Customer Service",
///     Description = "Workflows for customer service automation and support scenarios",
///     IsSystemDefined = true,
///     CategoryType = CategoryType.Functional,
///     DisplayOrder = 40,
///     IconName = "customer-service",
///     Color = "#2563eb"
/// };
/// 
/// // Creating a user-defined workflow category
/// var customCategory = new WorkflowCategory
/// {
///     Name = "ACME Manufacturing Processes",
///     Description = "Company-specific manufacturing workflow automation and quality control",
///     IsSystemDefined = false,
///     CategoryType = CategoryType.OrganizationSpecific,
///     ParentCategoryId = systemCategory.Id,
///     CreatedBy = "workflow-admin@acme.com",
///     TenantId = currentTenantId
/// };
/// 
/// // Creating subcategory for specialized workflows
/// var subCategory = new WorkflowCategory
/// {
///     Name = "Quality Control Automation",
///     Description = "Automated quality assurance and testing workflows",
///     ParentCategoryId = customCategory.Id,
///     IsSystemDefined = false,
///     TenantId = currentTenantId
/// };
/// 
/// await categoryService.CreateAsync(customCategory);
/// </code>
/// </example>
public class WorkflowCategory : AuditableEntity
{
    /// <summary>
    /// Gets or sets the display name of the workflow category.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides user-friendly identification for workflow category selection and display,
    /// enabling intuitive navigation and organization of prompt workflows
    /// across diverse enterprise environments with clear, meaningful labels.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Required field with length constraints for UI compatibility.
    /// Should be unique within the same parent category and tenant scope.
    /// </summary>
    /// <value>
    /// A descriptive name for the workflow category that users will see in interfaces.
    /// Cannot be null or empty. Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Used extensively in user interfaces and should be concise yet descriptive.
    /// Consider internationalization requirements for global deployments.
    /// </remarks>
    /// <example>
    /// Examples: "Customer Service", "Data Processing", "Content Generation", "Quality Assurance"
    /// </example>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional description explaining the workflow category's purpose and scope.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides context and guidance for category usage, helping users understand
    /// the intended scope and select appropriate categories for their prompt workflows
    /// in complex organizational structures with multiple workflow types.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional field for detailed category explanation and usage guidelines.
    /// Used in tooltips, help systems, and category management interfaces.
    /// </summary>
    /// <value>
    /// A detailed description of the workflow category's purpose and intended use.
    /// Can be null. Maximum length is 500 characters.
    /// </value>
    /// <remarks>
    /// Helpful for category selection guidance and administrative documentation.
    /// Should provide clear criteria for when to use this category.
    /// </remarks>
    /// <example>
    /// "Workflows designed for automated customer service operations, including ticket routing, response generation, and escalation procedures"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the parent category for hierarchical organization.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables hierarchical category structures for complex organizational taxonomies,
    /// supporting nested categorization schemes that reflect enterprise organizational
    /// structures and business process hierarchies for workflow management.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Self-referencing foreign key for tree structure implementation.
    /// Null for root-level categories. Supports unlimited nesting depth.
    /// </summary>
    /// <value>
    /// The GUID of the parent WorkflowCategory, or null for top-level categories.
    /// </value>
    /// <remarks>
    /// Enables sophisticated category hierarchies but consider performance implications for deep trees.
    /// Circular references must be prevented through validation logic.
    /// </remarks>
    public Guid? ParentCategoryId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the parent workflow category.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides direct access to parent category information for hierarchy
    /// navigation and breadcrumb construction without additional queries.
    /// </summary>
    /// <value>
    /// The parent <see cref="WorkflowCategory"/> entity, or null for root categories.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Enables efficient hierarchy traversal and parent information access.
    /// </remarks>
    public virtual WorkflowCategory? ParentCategory { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of child workflow categories under this category.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to subcategories for hierarchy display and navigation,
    /// enabling complete category tree management and traversal for workflow organization.
    /// </summary>
    /// <value>
    /// A collection of child <see cref="WorkflowCategory"/> entities.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Supports hierarchical category management and tree operations.
    /// </remarks>
    public virtual ICollection<WorkflowCategory> ChildCategories { get; set; } = [];
    
    /// <summary>
    /// Gets or sets whether this is a system-defined workflow category.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Distinguishes between standard system categories and user-created custom categories,
    /// enabling different management policies and protecting system categories
    /// from accidental modification while allowing user customization for workflows.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag controlling category mutability and management permissions.
    /// System categories typically cannot be deleted or significantly modified.
    /// </summary>
    /// <value>
    /// <c>true</c> if this is a system-provided category; <c>false</c> for user-created categories.
    /// Default is <c>false</c> for user-created categories.
    /// </value>
    /// <remarks>
    /// System categories provide consistency across tenants and should be protected from modification.
    /// User categories enable organizational flexibility and workflow customization.
    /// </remarks>
    public bool IsSystemDefined { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the type classification of this workflow category.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Categorizes the workflow category itself for administrative purposes and UI organization,
    /// enabling different treatment and filtering of categories based on their
    /// intended use and organizational scope in workflow management.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based category classification for administrative and UI purposes.
    /// Used for category filtering, grouping, and management workflows.
    /// </summary>
    /// <value>
    /// A <see cref="CategoryType"/> enum value indicating the category classification.
    /// Default is General for uncategorized workflow categories.
    /// </value>
    /// <remarks>
    /// Helps organize categories themselves and provides context for category management.
    /// Used in administrative interfaces for category filtering and organization.
    /// </remarks>
    public WorkflowCategoryType CategoryType { get; set; } = WorkflowCategoryType.General;
    
    /// <summary>
    /// Gets or sets the display order for workflow category sorting within the same parent.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls category presentation order in user interfaces, enabling
    /// logical organization and prioritization of workflow categories based on
    /// usage frequency or business importance.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Integer used for sorting categories within the same hierarchical level.
    /// Lower numbers appear first in sorted lists.
    /// </summary>
    /// <value>
    /// An integer representing the sort order within sibling categories.
    /// Default is 0. Lower values appear first in sorted displays.
    /// </value>
    /// <remarks>
    /// Enables custom ordering beyond alphabetical sorting.
    /// Should be managed consistently within category groups.
    /// </remarks>
    public int DisplayOrder { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets an optional icon name for visual representation of the workflow category.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides visual cues for workflow category recognition and improved user experience,
    /// enabling quick identification and enhanced usability in complex
    /// category hierarchies and workflow selection interfaces.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Icon identifier from the application's icon library or font.
    /// Used for UI enhancement and visual category identification.
    /// </summary>
    /// <value>
    /// A string identifying an icon from the application's icon set,
    /// or null if no specific icon is assigned. Maximum length is 50 characters.
    /// </value>
    /// <remarks>
    /// Enhances user experience through visual category identification.
    /// Should reference icons available in the application's design system.
    /// </remarks>
    /// <example>
    /// Examples: "workflow", "automation", "data-processing", "customer-service", "analytics"
    /// </example>
    [StringLength(50)]
    public string? IconName { get; set; }
    
    /// <summary>
    /// Gets or sets an optional color code for workflow category theming.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables color-coded category organization for improved visual navigation
    /// and user experience, supporting quick recognition and categorization
    /// in complex enterprise environments with multiple workflow types.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Hex color code for UI theming and visual organization.
    /// Used for category badges, highlights, and visual grouping.
    /// </summary>
    /// <value>
    /// A hex color code (e.g., "#2563eb") for category theming,
    /// or null for default styling. Maximum length is 7 characters.
    /// </value>
    /// <remarks>
    /// Enhances visual organization and category recognition.
    /// Should follow accessibility guidelines for color contrast.
    /// </remarks>
    /// <example>
    /// Examples: "#2563eb" (blue), "#dc2626" (red), "#059669" (green), "#7c3aed" (purple)
    /// </example>
    [StringLength(7)]
    public string? Color { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of prompt flows associated with this workflow category.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to all workflows using this category for management,
    /// reporting, and analysis purposes without separate queries.
    /// </summary>
    /// <value>
    /// A collection of <see cref="PromptFlow"/> entities using this category.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Enables efficient querying of workflows by category.
    /// </remarks>
    public virtual ICollection<PromptFlow> PromptFlows { get; set; } = [];
}
