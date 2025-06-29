using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents granular access control and security permissions for prompt libraries in enterprise environments.
/// 
/// <para><strong>Business Context:</strong></para>
/// LibraryPermission enables fine-grained security and collaboration controls for prompt library access,
/// supporting enterprise governance requirements with role-based access control, delegation capabilities,
/// and time-limited permissions. It facilitates secure collaboration while maintaining strict access
/// boundaries and audit trails essential for compliance and intellectual property protection.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity provides comprehensive permission management with principal identification, capability-based
/// access control, delegation chains, and expiration handling. It supports multi-tenant environments
/// with tenant-scoped permissions and integrates with enterprise identity systems for authentication
/// and authorization workflows with full audit trail support.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Fine-grained access control for prompt library resources
/// - Enterprise-grade security with delegation and time-limited access
/// - Compliance-ready audit trails and permission tracking
/// - Flexible capability-based authorization system
/// - Multi-tenant security with proper isolation
/// - Integration-ready for enterprise identity systems
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Role-Based Access Control (RBAC): Principal types and permission levels
/// - Capability-Based Security: Fine-grained capability grants
/// - Delegation Pattern: Permission delegation with depth control
/// - Multi-tenancy: Tenant-scoped permission isolation
/// - Audit Trail: Comprehensive permission change tracking
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index LibraryId and PrincipalId for efficient permission lookups
/// - Consider caching permission matrices for high-frequency operations
/// - Capabilities stored as JSON for flexible querying and extension
/// - Delegation chains should be limited to prevent performance issues
/// - Expired permissions should be cleaned up regularly for optimal performance
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Identity Management: Principal authentication and role assignment
/// - Authorization Engine: Permission evaluation and enforcement
/// - Audit System: Security event logging and compliance reporting
/// - Workflow Engine: Automated permission provisioning and deprovisioning
/// - Admin Interface: Permission management and delegation workflows
/// </remarks>
/// <example>
/// <code>
/// // Granting read access to a library
/// var permission = new LibraryPermission
/// {
///     LibraryId = libraryId,
///     PrincipalId = "user@company.com",
///     PrincipalType = PrincipalType.User,
///     Permission = PermissionLevel.Read,
///     Capabilities = JsonSerializer.Serialize(new[] { "view_templates", "execute_templates" }),
///     GrantReason = "Project collaboration access",
///     TenantId = currentTenantId
/// };
/// 
/// // Granting delegable admin access with time limit
/// var adminPermission = new LibraryPermission
/// {
///     LibraryId = libraryId,
///     PrincipalId = "admin@company.com",
///     PrincipalType = PrincipalType.User,
///     Permission = PermissionLevel.Admin,
///     CanDelegate = true,
///     MaxDelegationDepth = 2,
///     ExpiresAt = DateTime.UtcNow.AddDays(30),
///     Capabilities = JsonSerializer.Serialize(new[] { 
///         "manage_templates", "manage_permissions", "view_analytics" 
///     }),
///     TenantId = currentTenantId
/// };
/// 
/// await permissionService.GrantAsync(permission);
/// </code>
/// </example>
public class LibraryPermission : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the prompt library this permission applies to.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links permissions to specific library resources for granular access control,
    /// enabling precise security boundaries and resource-specific authorization
    /// in enterprise environments with multiple prompt libraries and varied access requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key reference to PromptLibrary entity.
    /// Used for permission scope validation and access control enforcement.
    /// </summary>
    /// <value>
    /// The GUID of the PromptLibrary that this permission grants access to.
    /// </value>
    /// <remarks>
    /// Essential for resource-specific access control and security boundary enforcement.
    /// Used extensively in authorization checks and permission lookups.
    /// </remarks>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the associated prompt library.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides direct access to library metadata for permission evaluation
    /// without requiring separate database queries during authorization checks.
    /// </summary>
    /// <value>
    /// The <see cref="PromptLibrary"/> entity that this permission applies to.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Enables efficient permission evaluation with library context.
    /// </remarks>
    public virtual PromptLibrary Library { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the identifier of the principal being granted permission.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Identifies the user, role, group, or service account receiving access rights,
    /// enabling precise identity-based authorization and accountability tracking
    /// essential for enterprise security and compliance requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Principal identifier from the organization's identity system.
    /// Format varies by principal type: email for users, role names for roles, etc.
    /// </summary>
    /// <value>
    /// A string identifying the principal receiving permission.
    /// Cannot be null or empty. Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Critical for authorization decisions and audit trail generation.
    /// Should follow organizational identity standards for consistency.
    /// </remarks>
    /// <example>
    /// Examples: "user@company.com", "role:content-editors", "group:marketing-team", "service:ai-gateway"
    /// </example>
    [Required]
    [StringLength(100)]
    public string PrincipalId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of principal being granted permission.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Categorizes the permission recipient for appropriate authorization logic,
    /// supporting different identity types and access patterns required
    /// in enterprise environments with diverse identity management needs.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based principal classification for authorization engine routing.
    /// Different principal types may have different permission evaluation logic.
    /// </summary>
    /// <value>
    /// A <see cref="PrincipalType"/> enum value indicating the type of permission recipient.
    /// Default is User.
    /// </value>
    /// <remarks>
    /// Used for authorization logic routing and permission inheritance rules.
    /// Different principal types may have different security considerations.
    /// </remarks>
    public PrincipalType PrincipalType { get; set; } = PrincipalType.User;
    
    /// <summary>
    /// Gets or sets the level of access granted to the principal.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Defines the scope of access rights for hierarchical permission management,
    /// enabling role-based access control with clear authorization levels
    /// suitable for enterprise governance and security requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based permission level with hierarchical access rights.
    /// Higher levels typically include permissions from lower levels.
    /// </summary>
    /// <value>
    /// A <see cref="PermissionLevel"/> enum value defining the access scope.
    /// Default is Read for minimal access principles.
    /// </value>
    /// <remarks>
    /// Used for coarse-grained access control before capability-specific checks.
    /// Should follow principle of least privilege in enterprise environments.
    /// </remarks>
    public PermissionLevel Permission { get; set; } = PermissionLevel.Read;
    
    /// <summary>
    /// Gets or sets the optional expiration date for temporary access control.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables time-limited access for temporary collaborations, project-based work,
    /// and security-conscious environments requiring automatic permission expiration
    /// to minimize security exposure and support compliance with access review policies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// UTC timestamp for consistent timezone handling in global deployments.
    /// Expired permissions should be automatically disabled by authorization systems.
    /// </summary>
    /// <value>
    /// A nullable <see cref="DateTime"/> representing when this permission expires in UTC,
    /// or null for permanent permissions that don't expire automatically.
    /// </value>
    /// <remarks>
    /// Critical for temporal access control and automated security compliance.
    /// Expired permissions should trigger cleanup processes and audit notifications.
    /// </remarks>
    /// <example>
    /// Temporary access: DateTime.UtcNow.AddDays(30) for 30-day contractor access
    /// </example>
    public DateTime? ExpiresAt { get; set; }
    
    /// <summary>
    /// Gets or sets specific capabilities granted beyond the base permission level.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides fine-grained, capability-based access control for complex authorization
    /// scenarios requiring specific action permissions beyond role-based access levels,
    /// supporting enterprise environments with sophisticated security requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON array of capability strings for flexible and extensible permission system.
    /// Capabilities enable action-specific authorization beyond basic CRUD operations.
    /// </summary>
    /// <value>
    /// A JSON string containing an array of capability identifiers,
    /// or null if only basic permission level applies. Maximum length is 500 characters.
    /// </value>
    /// <remarks>
    /// Enables sophisticated authorization scenarios and future capability extension.
    /// Should be validated against known capability definitions for security.
    /// </remarks>
    /// <example>
    /// JSON: ["create_templates", "manage_permissions", "view_analytics", "export_data"]
    /// </example>
    [StringLength(500)]
    public string? Capabilities { get; set; }
    
    /// <summary>
    /// Gets or sets whether this permission can be delegated to other principals.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables permission delegation for flexible collaboration and administrative workflows,
    /// supporting enterprise scenarios where access rights need to be temporarily
    /// or permanently transferred while maintaining security and audit requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag controlling delegation capability in permission hierarchy.
    /// Delegation enables permission distribution without admin intervention.
    /// </summary>
    /// <value>
    /// <c>true</c> if this permission can be delegated to others; otherwise, <c>false</c>.
    /// Default is <c>false</c> for security-first approach.
    /// </value>
    /// <remarks>
    /// Critical for flexible permission management while maintaining security controls.
    /// Should be used carefully with appropriate delegation depth limits.
    /// </remarks>
    public bool CanDelegate { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the maximum delegation depth for permission chain control.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls delegation chain length to prevent uncontrolled permission spread,
    /// supporting enterprise security policies requiring bounded delegation
    /// and clear accountability chains for access rights management.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Integer representing maximum levels of re-delegation allowed.
    /// Prevents infinite delegation chains and maintains permission traceability.
    /// </summary>
    /// <value>
    /// The maximum number of delegation levels allowed from this permission.
    /// Default is 0 (no delegation) for security-conscious defaults.
    /// </value>
    /// <remarks>
    /// Essential for preventing permission sprawl and maintaining security boundaries.
    /// Should be set conservatively based on organizational security policies.
    /// </remarks>
    /// <example>
    /// Values: 0 (no delegation), 1 (direct delegation only), 2 (two-level chain), etc.
    /// </example>
    public int MaxDelegationDepth { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the current delegation depth in the permission chain.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks position in delegation hierarchy for audit trails and delegation
    /// limit enforcement, supporting enterprise requirements for permission
    /// accountability and traceability in complex authorization scenarios.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Integer tracking current position in delegation chain starting from 0.
    /// Used for delegation limit validation and audit trail construction.
    /// </summary>
    /// <value>
    /// The current depth in the delegation chain, where 0 represents the original grant.
    /// Higher numbers indicate further delegation levels.
    /// </value>
    /// <remarks>
    /// Critical for delegation limit enforcement and permission chain tracking.
    /// Used in authorization checks to validate delegation boundaries.
    /// </remarks>
    public int DelegationDepth { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the identifier of the permission this was delegated from.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Maintains delegation chain traceability for audit and accountability purposes,
    /// enabling enterprise compliance requirements and permission origin tracking
    /// essential for security investigations and access review processes.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key reference to parent LibraryPermission in delegation chain.
    /// Null for original (non-delegated) permissions.
    /// </summary>
    /// <value>
    /// The GUID of the LibraryPermission that this permission was delegated from,
    /// or null if this is an original (non-delegated) permission.
    /// </value>
    /// <remarks>
    /// Essential for delegation chain validation and audit trail completeness.
    /// Enables revocation of entire delegation chains when needed.
    /// </remarks>
    public Guid? DelegatedFromId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the parent permission in delegation chain.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides direct access to delegation source for chain validation
    /// and audit trail construction without additional database queries.
    /// </summary>
    /// <value>
    /// The <see cref="LibraryPermission"/> entity this permission was delegated from,
    /// or null if this is an original permission.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Enables efficient delegation chain traversal and validation.
    /// </remarks>
    public virtual LibraryPermission? DelegatedFrom { get; set; }
    
    /// <summary>
    /// Gets or sets the reason for granting this permission.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Documents the business justification for access grants, supporting
    /// enterprise governance requirements, compliance audits, and access
    /// review processes with clear rationale for permission decisions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Freeform text field for business justification and context.
    /// Used for audit trails and permission review documentation.
    /// </summary>
    /// <value>
    /// A text description explaining why this permission was granted,
    /// or null if no specific reason was documented. Maximum length is 500 characters.
    /// </value>
    /// <remarks>
    /// Important for audit compliance and permission review processes.
    /// Should include sufficient detail for future access review decisions.
    /// </remarks>
    /// <example>
    /// Examples: "Project collaboration access", "Temporary contractor support", "Cross-team consultation"
    /// </example>
    [StringLength(500)]
    public string? GrantReason { get; set; }
}
