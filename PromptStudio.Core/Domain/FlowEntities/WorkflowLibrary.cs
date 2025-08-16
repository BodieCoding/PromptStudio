using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents an organizational container for workflow management within PromptLabs, providing hierarchical structure and governance for complex workflow ecosystems.
/// 
/// <para><strong>Business Context:</strong></para>
/// WorkflowLibrary enables teams to organize, manage, and govern collections of related workflows
/// within a structured hierarchy. This supports enterprise workflow management, access control,
/// collaboration, and lifecycle governance while maintaining clear organizational boundaries
/// and facilitating workflow discovery and reuse across teams and projects.
/// 
/// <para><strong>Technical Context:</strong></para>
/// WorkflowLibrary serves as a container entity within the workflow management system, providing
/// organizational metadata, access control, and governance capabilities with enterprise-grade
/// audit trails, multi-tenancy support, and comprehensive permission management for scalable
/// workflow organization and collaboration.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Hierarchical workflow organization for complex enterprise environments
/// - Granular access control and permission management for workflow security
/// - Visual identification and categorization for enhanced user experience
/// - Governance and approval workflows for enterprise compliance
/// - Performance optimization through caching and organizational metadata
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Container Pattern: Organizational grouping of related workflows
/// - Access Control Pattern: Permission-based security and collaboration
/// - Cache Aside Pattern: Performance optimization through metadata caching
/// - Multi-tenancy: Organizational isolation and data security
/// - Audit Trail: Complete library lifecycle and change tracking
/// 
/// <para><strong>Organizational Hierarchy:</strong></para>
/// - PromptLab contains multiple WorkflowLibraries
/// - WorkflowLibrary contains multiple PromptFlows
/// - Categories provide cross-cutting organizational taxonomy
/// - Permissions enable fine-grained access control
/// - Tags support flexible categorization and discovery
/// 
/// <para><strong>Access Control Framework:</strong></para>
/// - Library-level permissions for read, write, and administrative access
/// - Inheritance model for workflow permissions within the library
/// - Approval workflows for controlled change management
/// - Visibility settings for public, private, and shared libraries
/// - Integration with organizational authentication and authorization
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index on PromptLabId and WorkflowCategoryId for efficient queries
/// - Cache WorkflowCount for performance optimization
/// - Consider denormalizing frequently accessed metadata
/// - Optimize permission queries for large-scale access control
/// - Monitor library size for performance and usability
/// 
/// <para><strong>Integration Points:</strong></para>
/// - PromptLab: Parent organizational container
/// - WorkflowCategory: Flexible categorization system
/// - PromptFlow: Contained workflow entities
/// - Permission System: Access control and collaboration
/// - Search Engine: Discovery and filtering capabilities
/// - Analytics Platform: Usage tracking and optimization insights
/// </remarks>
/// <example>
/// <code>
/// // Create a new workflow library with categorization
/// var workflowLibrary = new WorkflowLibrary
/// {
///     Name = "Customer Service Automation",
///     Description = "Standardized workflows for customer service scenarios",
///     PromptLabId = customerServiceLab.Id,
///     WorkflowCategoryId = automationCategory.Id,
///     Color = "#2196F3",
///     Icon = "support_agent",
///     Tags = JsonSerializer.Serialize(new[] { "customer-service", "automation", "support" }),
///     Visibility = LibraryVisibility.TeamShared,
///     RequiresApproval = true,
///     DefaultTimeoutSeconds = 300,
///     DefaultCostLimit = 5.00m
/// };
/// 
/// // Add workflows to the library
/// var workflows = new[]
/// {
///     CreateTicketClassificationWorkflow(),
///     CreateResponseGenerationWorkflow(),
///     CreateEscalationWorkflow()
/// };
/// 
/// foreach (var workflow in workflows)
/// {
///     workflow.WorkflowLibraryId = workflowLibrary.Id;
///     workflowLibrary.PromptFlows.Add(workflow);
/// }
/// 
/// // Set up permissions for team access
/// workflowLibrary.Permissions.Add(new WorkflowLibraryPermission
/// {
///     UserId = teamLeadId,
///     Permission = LibraryPermissionType.Admin
/// });
/// 
/// await repository.AddAsync(workflowLibrary);
/// </code>
/// </example>
public class WorkflowLibrary : AuditableEntity
{
    /// <summary>
    /// Human-readable name for the workflow library used in organizational interfaces and navigation.
    /// <value>String up to 100 characters identifying the library purpose and scope</value>
    /// </summary>
    /// <remarks>
    /// Should be descriptive and unique within the PromptLab to facilitate
    /// library identification, navigation, and team communication.
    /// </remarks>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Comprehensive description explaining the library's purpose, scope, and contained workflow types.
    /// <value>String up to 500 characters detailing library context and objectives</value>
    /// </summary>
    /// <remarks>
    /// Provides context for team members, documents library scope,
    /// and supports discovery through search and browsing interfaces.
    /// </remarks>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Reference to the parent PromptLab that contains this workflow library.
    /// <value>Guid identifier linking this library to its parent organizational container</value>
    /// </summary>
    /// <remarks>
    /// Establishes organizational hierarchy and access control inheritance.
    /// Used for scoping queries and enforcing organizational boundaries.
    /// </remarks>
    public Guid PromptLabId { get; set; }

    /// <summary>
    /// Navigation property to the parent PromptLab entity for organizational context.
    /// <value>PromptLab entity representing the parent organizational container</value>
    /// </summary>
    /// <remarks>
    /// Provides access to lab-level settings, permissions, and organizational metadata
    /// for comprehensive library management and access control.
    /// </remarks>
    public virtual PromptLab PromptLab { get; set; } = null!;
    
    /// <summary>
    /// Reference to the workflow category for flexible library organization and discovery.
    /// <value>Guid identifier linking this library to its organizational category</value>
    /// </summary>
    /// <remarks>
    /// Enables flexible library categorization through the enterprise category system,
    /// supporting hierarchical organization and custom taxonomies for workflow management.
    /// </remarks>
    public Guid WorkflowCategoryId { get; set; }

    /// <summary>
    /// Navigation property to the workflow category for library organization and metadata.
    /// <value>WorkflowCategory entity providing categorization and organizational metadata</value>
    /// </summary>
    /// <remarks>
    /// Provides access to category hierarchy, descriptions, and visual properties
    /// for enhanced library organization and user experience.
    /// </remarks>
    public virtual WorkflowCategory WorkflowCategory { get; set; } = null!;
    
    /// <summary>
    /// Visual color identifier for library representation in user interfaces.
    /// <value>Hex color code string (e.g., "#1976d2") for visual identification</value>
    /// </summary>
    /// <remarks>
    /// Enhances user experience through visual differentiation and brand consistency.
    /// Should follow accessibility guidelines for color contrast and usability.
    /// </remarks>
    [StringLength(7)]
    public string? Color { get; set; } = "#1976d2";
    
    /// <summary>
    /// Icon identifier for visual representation in user interfaces and navigation.
    /// <value>String identifier for icon selection from available icon libraries</value>
    /// </summary>
    /// <remarks>
    /// Provides intuitive visual identification and improves user experience
    /// through consistent iconography and visual design patterns.
    /// </remarks>
    [StringLength(50)]
    public string? Icon { get; set; } = "account_tree";
    
    /// <summary>
    /// Flexible tagging system for library categorization, search, and discovery.
    /// <value>JSON array of tag strings for metadata and filtering capabilities</value>
    /// </summary>
    /// <remarks>
    /// Supports cross-cutting concerns, custom categorization, and enhanced
    /// search capabilities beyond formal category hierarchies.
    /// </remarks>
    [StringLength(1000)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Numeric ordering value for custom library arrangement within the parent lab.
    /// <value>Double precision number controlling display order and organization</value>
    /// </summary>
    /// <remarks>
    /// Enables user-customizable library ordering for improved workflow
    /// and enhanced user experience in organizational interfaces.
    /// </remarks>
    public double SortOrder { get; set; } = 0.0;
    
    /// <summary>
    /// Flag indicating whether this library is pinned for quick access and visibility.
    /// <value>Boolean indicating pinned status for enhanced accessibility</value>
    /// </summary>
    /// <remarks>
    /// Supports user personalization and quick access to frequently used
    /// libraries through enhanced interface prominence and navigation shortcuts.
    /// </remarks>
    public bool IsPinned { get; set; } = false;
    
    /// <summary>
    /// Access control setting determining library visibility and sharing scope.
    /// <value>LibraryVisibility enum controlling access and sharing permissions</value>
    /// </summary>
    /// <remarks>
    /// Controls library discoverability and access patterns, supporting
    /// organizational security requirements and collaboration models.
    /// </remarks>
    public LibraryVisibility Visibility { get; set; } = LibraryVisibility.Private;
    
    /// <summary>
    /// Governance flag requiring approval workflows for library and workflow modifications.
    /// <value>Boolean indicating whether changes require formal approval processes</value>
    /// </summary>
    /// <remarks>
    /// Supports enterprise governance requirements, change control, and
    /// quality assurance through formal approval and review processes.
    /// </remarks>
    public bool RequiresApproval { get; set; } = false;
    
    /// <summary>
    /// Current lifecycle status of the library for operational management.
    /// <value>LibraryStatus enum indicating current operational state</value>
    /// </summary>
    /// <remarks>
    /// Controls library availability, operational behavior, and lifecycle
    /// management through status-based access control and workflow automation.
    /// </remarks>
    public LibraryStatus Status { get; set; } = LibraryStatus.Active;
    
    /// <summary>
    /// Cached count of workflows contained in this library for performance optimization.
    /// <value>Integer representing the current number of workflows in the library</value>
    /// </summary>
    /// <remarks>
    /// Denormalized value updated through triggers or application logic
    /// to avoid expensive count queries in list and summary interfaces.
    /// </remarks>
    public int WorkflowCount { get; set; } = 0;
    
    /// <summary>
    /// Timestamp of the most recent activity within the library for sorting and analytics.
    /// <value>DateTime in UTC representing the last workflow creation, modification, or execution</value>
    /// </summary>
    /// <remarks>
    /// Used for activity-based sorting, analytics, and identifying
    /// active versus dormant libraries for management and optimization.
    /// </remarks>
    public DateTime? LastActivityAt { get; set; }
    
    /// <summary>
    /// Default execution timeout for workflows within this library for resource management.
    /// <value>Integer representing default timeout in seconds for workflow executions</value>
    /// </summary>
    /// <remarks>
    /// Provides library-level resource control and prevents runaway executions
    /// while allowing workflow-specific overrides for specialized requirements.
    /// </remarks>
    public int? DefaultTimeoutSeconds { get; set; }
    
    /// <summary>
    /// Default cost limit for workflow executions within this library for budget control.
    /// <value>Decimal representing maximum allowed cost per workflow execution</value>
    /// </summary>
    /// <remarks>
    /// Supports budget management and cost control at the library level
    /// while maintaining flexibility for workflow-specific cost requirements.
    /// </remarks>
    public decimal? DefaultCostLimit { get; set; }

    // Navigation Properties
    /// <summary>
    /// Collection of workflows contained within this library for organizational management.
    /// <value>ICollection of PromptFlow entities representing all workflows in the library</value>
    /// </summary>
    /// <remarks>
    /// Primary organizational relationship for workflow management, discovery,
    /// and execution within the library context and access control framework.
    /// </remarks>
    public virtual ICollection<PromptFlow> PromptFlows { get; set; } = [];

    /// <summary>
    /// Collection of access permissions for granular library security and collaboration.
    /// <value>ICollection of WorkflowLibraryPermission entities defining user and role access</value>
    /// </summary>
    /// <remarks>
    /// Enables fine-grained access control, team collaboration, and security
    /// management through explicit permission assignments and role-based access.
    /// </remarks>
    public virtual ICollection<WorkflowLibraryPermission> Permissions { get; set; } = [];
}

