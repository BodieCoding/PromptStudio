using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the top-level organizational unit for prompt engineering work in enterprise LLMOps environments.
/// 
/// <para><strong>Business Context:</strong></para>
/// PromptLabs provide complete isolation, ownership, governance, and collaboration boundaries
/// for prompt development teams, similar to research labs, GitHub organizations, or GCP projects.
/// They enable organizations to separate different projects, teams, or business units while
/// maintaining centralized governance, analytics, and resource management capabilities.
/// 
/// <para><strong>Technical Context:</strong></para>
/// PromptLab serves as the primary tenant boundary in multi-tenant LLMOps environments,
/// providing security isolation through Guid-based identification, soft deletion support,
/// and comprehensive audit trails. Each lab contains libraries, templates, workflows,
/// and associated governance policies with full lifecycle management.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Complete workspace isolation for different teams and projects
/// - Enterprise-grade governance and compliance capabilities
/// - Collaborative development with role-based access control
/// - Comprehensive audit trails and change tracking
/// - Scalable multi-tenant architecture support
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Multi-tenancy: Organizational isolation through tenant boundaries
/// - Soft Delete: Audit-compliant data retention and recovery
/// - Optimistic Concurrency: Row versioning for collaborative editing
/// - Audit Trail: Comprehensive change tracking through AuditableEntity
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index LabId for friendly URL routing performance
/// - Consider partitioning by OrganizationId for large-scale deployments
/// - Tags stored as JSON for flexible querying and categorization
/// - Soft delete preserves data integrity while supporting cleanup policies
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Identity Management: User and team authentication/authorization
/// - Workflow Engine: Prompt template and workflow orchestration
/// - Analytics Platform: Usage metrics and performance tracking
/// - Governance System: Compliance monitoring and policy enforcement
/// - Storage Backend: Multi-tenant data isolation and backup
/// </remarks>
/// <example>
/// <code>
/// // Creating a new prompt lab for customer service
/// var lab = new PromptLab
/// {
///     Name = "Customer Service AI",
///     Description = "AI-powered customer support response development and testing",
///     LabId = "customer-service-ai",
///     Owner = "team-customer-support",
///     OrganizationId = organizationId,
///     Status = LabStatus.Active,
///     Visibility = LabVisibility.Internal,
///     Tags = JsonSerializer.Serialize(new[] { "customer-service", "ai", "support" }),
///     TenantId = currentTenantId
/// };
/// 
/// // Adding libraries and workflows
/// lab.PromptLibraries.Add(new PromptLibrary 
/// { 
///     Name = "Support Templates",
///     LabId = lab.Id 
/// });
/// 
/// await labService.CreateAsync(lab);
/// </code>
/// </example>
public class PromptLab : AuditableEntity
{
    /// <summary>
    /// Gets or sets the human-readable name of the PromptLab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides a descriptive identifier for display and organizational purposes,
    /// enabling easy recognition and management of labs across enterprise environments
    /// with clear identification for users, administrators, and reporting systems.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Required field with maximum length of 100 characters.
    /// Used for display in user interfaces, reports, and administrative functions.
    /// </summary>
    /// <value>
    /// A descriptive name that should be meaningful to users and administrators.
    /// Cannot be null or empty. Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Should be descriptive and reflect the lab's purpose or team.
    /// Used extensively in UI components and administrative interfaces.
    /// </remarks>
    /// <example>
    /// Examples: "Customer Service AI", "Marketing Content Generation", "Legal Document Review"
    /// </example>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional description explaining the lab's purpose and scope.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides context for lab discovery and understanding of its intended use,
    /// supporting team onboarding, collaboration, and governance workflows
    /// by clearly communicating the lab's objectives and scope.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional field with maximum length of 500 characters.
    /// Used for documentation, discovery, and administrative purposes.
    /// </summary>
    /// <value>
    /// A detailed description of the lab's objectives, team, and intended use cases.
    /// Can be null. Maximum length is 500 characters.
    /// </value>
    /// <remarks>
    /// Helps with lab discovery and provides context for new team members.
    /// Should include information about the lab's goals and scope.
    /// </remarks>
    /// <example>
    /// "Collaborative workspace for developing and testing AI-powered customer service responses, escalation procedures, and multilingual support templates"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the URL-friendly unique identifier for this lab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides human-readable URLs and API endpoints while maintaining GUID-based internal references,
    /// enabling user-friendly navigation and integration with external systems
    /// through consistent, readable identifiers.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Required unique identifier with format restrictions for URL compatibility.
    /// Must be lowercase, start with a letter, and contain only letters, numbers, and hyphens.
    /// Length between 3-50 characters.
    /// </summary>
    /// <value>
    /// A lowercase, hyphen-separated identifier that must be unique across the platform.
    /// Cannot be null or empty. Length between 3-50 characters.
    /// </value>
    /// <remarks>
    /// Lab IDs enable friendly URLs (e.g., /labs/customer-support-ai) while maintaining
    /// security through GUID-based internal operations. Format restrictions ensure
    /// URL compatibility and consistent naming conventions.
    /// </remarks>
    /// <example>
    /// Examples: "customer-support-ai", "marketing-content-gen", "legal-document-review"
    /// </example>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [RegularExpression(@"^[a-z][a-z0-9-]*[a-z0-9]$", 
        ErrorMessage = "Lab ID must be lowercase, start with a letter, and contain only letters, numbers, and hyphens")]
    public string LabId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the lab owner for ownership and access control.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Establishes clear ownership and accountability for lab resources and governance,
    /// supporting enterprise access control policies and administrative workflows
    /// with proper delegation and responsibility tracking.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Will be expanded to support user and team entities in future enterprise implementations.
    /// Currently stores user ID, email, or team identifier as string reference.
    /// </summary>
    /// <value>
    /// An identifier for the lab owner (user ID, email, or team identifier).
    /// Can be null for system-managed labs. Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Owner identification is critical for access control, governance, and administrative operations.
    /// Future versions will implement proper user management with role-based access control.
    /// </remarks>
    [StringLength(100)]
    public string? Owner { get; set; }
    
    /// <summary>
    /// Gets or sets the lab status for lifecycle management and operational control.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables lifecycle management and operational control over lab resources,
    /// supporting maintenance windows, project phases, and governance workflows
    /// with clear status tracking and automated policy enforcement.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based status tracking with predefined states for operational clarity.
    /// Used for access control and operational decision-making.
    /// </summary>
    /// <value>
    /// A <see cref="LabStatus"/> enum value indicating the current operational state.
    /// Default is Active.
    /// </value>
    /// <remarks>
    /// Used for operational control and access management.
    /// Inactive or archived labs may have restricted access or functionality.
    /// </remarks>
    public LabStatus Status { get; set; } = LabStatus.Active;
    
    /// <summary>
    /// Gets or sets tags for categorization and discovery in JSON array format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables flexible categorization and discovery of labs across enterprise environments,
    /// supporting search, filtering, and organizational taxonomy with dynamic
    /// tagging strategies that adapt to business needs.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON array format for better querying and indexing performance.
    /// Supports dynamic tag management and flexible categorization schemes.
    /// </summary>
    /// <value>
    /// A JSON string containing an array of tag values for categorization.
    /// Can be null. Maximum length is 1000 characters.
    /// </value>
    /// <remarks>
    /// JSON format enables efficient querying and flexible tag management.
    /// Should follow organizational tagging standards for consistency.
    /// </remarks>
    /// <example>
    /// JSON: ["customer-service", "ai", "support", "multilingual"]
    /// </example>
    [StringLength(1000)]
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets the lab visibility and access control settings.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls lab discoverability and access permissions across organizational boundaries,
    /// supporting enterprise security policies and collaboration models
    /// with granular visibility and access control capabilities.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based visibility control with predefined access levels.
    /// Integrates with authorization systems for access enforcement.
    /// </summary>
    /// <value>
    /// A <see cref="LabVisibility"/> enum value controlling access and discovery.
    /// Default is Private.
    /// </value>
    /// <remarks>
    /// Used for access control and discovery filtering.
    /// Private labs are only visible to members and administrators.
    /// </remarks>
    public LabVisibility Visibility { get; set; } = LabVisibility.Private;
    
    // Navigation properties - all using Guid foreign keys
    /// <summary>
    /// Gets or sets the collection of prompt libraries associated with this lab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides organized access to prompt template collections within the lab,
    /// enabling structured content management and collaborative development workflows.
    /// </summary>
    /// <value>
    /// A collection of <see cref="PromptLibrary"/> entities belonging to this lab.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Supports lazy loading and efficient querying of related libraries.
    /// </remarks>
    public virtual ICollection<PromptLibrary> PromptLibraries { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of prompt flows (workflows) associated with this lab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to workflow definitions and orchestration logic within the lab,
    /// enabling complex prompt processing and automation capabilities.
    /// </summary>
    /// <value>
    /// A collection of <see cref="PromptFlow"/> entities belonging to this lab.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Supports complex workflow orchestration and automation scenarios.
    /// </remarks>
    public virtual ICollection<PromptFlow> PromptFlows { get; set; } = [];
}