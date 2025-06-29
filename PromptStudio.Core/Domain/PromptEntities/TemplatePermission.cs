using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents granular access control permissions for prompt templates in enterprise environments.
/// 
/// <para><strong>Business Context:</strong></para>
/// This entity implements enterprise-grade security and access control for prompt templates,
/// enabling organizations to manage who can view, modify, execute, and administer templates
/// across different organizational units, roles, and security contexts. It supports complex
/// permission inheritance, temporary access grants, and fine-grained capability control.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity provides a flexible permission system supporting multiple principal types,
/// inheritance hierarchies, conditional access, and capability-based security. It integrates
/// with organizational identity systems and supports both explicit and inherited permissions
/// for comprehensive access control across complex organizational structures.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Enterprise-grade security and access control for sensitive prompt assets
/// - Flexible permission inheritance and delegation capabilities
/// - Fine-grained capability control for specific operations and features
/// - Temporal access control with expiration and conditional permissions
/// - Comprehensive audit trails for security compliance and governance
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Access Control List (ACL): Explicit permission grants for specific principals
/// - Inheritance Pattern: Hierarchical permission propagation from parent entities
/// - Capability-based Security: Fine-grained operation-level access control
/// - Multi-tenancy: Inherits tenant isolation from AuditableEntity
/// 
/// <para><strong>Security Considerations:</strong></para>
/// - Permissions should be evaluated in order of specificity (explicit before inherited)
/// - Capability arrays should use standardized capability names for consistency
/// - Expired permissions must be automatically excluded from access decisions
/// - Audit logs should track all permission evaluations for security monitoring
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Identity Systems: Principal validation and authentication integration
/// - Authorization Services: Permission evaluation and access control decisions
/// - Audit Systems: Security event logging and compliance reporting
/// - Template Management: Access control for template operations and workflows
/// - Inheritance Engine: Permission propagation from parent entities
/// </remarks>
/// <example>
/// <code>
/// // Granting explicit execution permission to a user
/// var permission = new TemplatePermission
/// {
///     TemplateId = templateId,
///     PrincipalId = "user123",
///     PrincipalType = PrincipalType.User,
///     Permission = PermissionLevel.Execute,
///     Capabilities = JsonSerializer.Serialize(new[] { "execute", "view_results" }),
///     ExpiresAt = DateTime.UtcNow.AddDays(30),
///     TenantId = currentTenantId
/// };
/// 
/// // Granting inherited permission from parent library
/// var inheritedPermission = new TemplatePermission
/// {
///     TemplateId = templateId,
///     PrincipalId = "role:developers",
///     PrincipalType = PrincipalType.Role,
///     Permission = PermissionLevel.Modify,
///     IsInherited = true,
///     InheritedFromId = libraryId,
///     InheritedFromType = "PromptLibrary",
///     TenantId = currentTenantId
/// };
/// 
/// // Checking permissions with capabilities
/// var hasExecutePermission = await permissionService.HasCapabilityAsync(
///     templateId, userId, "execute");
/// </code>
/// </example>
public class TemplatePermission : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the prompt template to which this permission applies.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links the permission to a specific prompt template, enabling precise access control
    /// and security boundary definition for individual templates within enterprise
    /// prompt management systems.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with PromptTemplate entity. Required for all permission records.
    /// Used for permission lookups and access control evaluations.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the unique identifier of the target prompt template.
    /// </value>
    /// <remarks>
    /// This property is required and must reference an existing PromptTemplate.
    /// Used as the primary key for permission lookups and access control decisions.
    /// </remarks>
    public Guid TemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the target prompt template.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to template metadata and configuration for permission
    /// evaluation and context-aware access control decisions.
    /// </summary>
    /// <value>
    /// A <see cref="PromptTemplate"/> instance representing the target template.
    /// </value>
    public virtual PromptTemplate Template { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the identifier of the security principal being granted permission.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Identifies the user, role, group, or service account receiving access rights,
    /// enabling precise access control and delegation capabilities in enterprise
    /// environments with complex organizational structures and security requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Format depends on PrincipalType: user IDs, role names, group identifiers,
    /// or service account keys. Must be resolvable through the organization's identity system.
    /// </summary>
    /// <value>
    /// A string identifying the security principal (e.g., "user123", "role:developers", "group:admins").
    /// Maximum length is 100 characters. Cannot be null or empty.
    /// </value>
    /// <remarks>
    /// Format should be consistent with organizational identity management standards.
    /// Used for principal resolution and permission evaluation.
    /// </remarks>
    [Required]
    [StringLength(100)]
    public string PrincipalId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of security principal receiving the permission.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Categorizes the permission recipient type for appropriate handling by
    /// identity and authorization systems, enabling different evaluation logic
    /// for users, roles, groups, and service accounts in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Determines how the PrincipalId should be resolved and evaluated.
    /// Used by authorization services for appropriate permission checking logic.
    /// </summary>
    /// <value>
    /// A <see cref="PrincipalType"/> enum value indicating the principal type.
    /// Default is User.
    /// </value>
    /// <remarks>
    /// Must match the format and resolution method for the PrincipalId.
    /// Used for identity system integration and permission evaluation routing.
    /// </remarks>
    public PrincipalType PrincipalType { get; set; } = PrincipalType.User;
    
    /// <summary>
    /// Gets or sets the level of access granted to the principal.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Defines the scope of access rights for the principal, providing hierarchical
    /// permission levels that align with organizational security policies and
    /// operational requirements for prompt template management.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Hierarchical permission levels with implicit capabilities. Higher levels
    /// typically include all capabilities of lower levels.
    /// </summary>
    /// <value>
    /// A <see cref="PermissionLevel"/> enum value indicating the access level.
    /// Default is Read.
    /// </value>
    /// <remarks>
    /// Permission levels typically follow a hierarchical model where higher levels
    /// include capabilities of lower levels (e.g., Modify includes Read capabilities).
    /// </remarks>
    public PermissionLevel Permission { get; set; } = PermissionLevel.Read;
    
    /// <summary>
    /// Gets or sets the optional expiration timestamp for temporary access grants.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables temporal access control for temporary collaborations, guest access,
    /// or time-limited project permissions, supporting enterprise security policies
    /// requiring periodic access review and automatic privilege expiration.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Permissions with past expiration dates should be automatically excluded
    /// from access evaluations. Null indicates permanent permission (subject to other conditions).
    /// </summary>
    /// <value>
    /// A nullable <see cref="DateTime"/> representing the permission expiration time in UTC,
    /// or null for permanent permissions.
    /// </value>
    /// <remarks>
    /// Expired permissions should be automatically excluded from access control decisions.
    /// Regular cleanup processes should remove or archive expired permissions.
    /// </remarks>
    public DateTime? ExpiresAt { get; set; }
    
    /// <summary>
    /// Gets or sets specific capabilities granted to the principal in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides fine-grained capability control beyond basic permission levels,
    /// enabling precise access control for specific operations and features
    /// in enterprise environments with complex functional requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON array of capability strings. Common capabilities include operations
    /// like "execute", "modify_variables", "view_results", "clone", "delete".
    /// </summary>
    /// <value>
    /// A JSON array of capability strings, or null to use default capabilities for the permission level.
    /// Maximum length is 500 characters.
    /// </value>
    /// <example>
    /// ["execute", "modify_variables", "view_results", "clone", "export"]
    /// </example>
    /// <remarks>
    /// Capability names should follow organizational standards for consistency.
    /// Used for fine-grained operation-level access control decisions.
    /// </remarks>
    [StringLength(500)]
    public string? Capabilities { get; set; }
    
    /// <summary>
    /// Gets or sets conditional access requirements in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables sophisticated conditional access policies based on context such as
    /// time of day, network location, device characteristics, or usage quotas,
    /// supporting enterprise zero-trust security models and compliance requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON object containing condition definitions and parameters. Evaluated
    /// at access time to determine if permission should be granted.
    /// </summary>
    /// <value>
    /// A JSON object containing conditional access requirements,
    /// or null for unconditional permissions. Maximum length is 1000 characters.
    /// </value>
    /// <example>
    /// {"timeRestrictions": {"startHour": 9, "endHour": 17}, "networkRestrictions": ["internal"], "usageLimit": 100}
    /// </example>
    /// <remarks>
    /// Conditions are evaluated at access time and may prevent permission use even if not expired.
    /// Should support common enterprise conditional access patterns.
    /// </remarks>
    [StringLength(1000)]
    public string? Conditions { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether this permission was inherited from a parent entity.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Distinguishes between explicit permissions and those inherited from parent
    /// entities like libraries or labs, enabling proper permission precedence
    /// and governance in hierarchical organizational structures.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Inherited permissions typically have lower precedence than explicit permissions.
    /// Used for permission resolution algorithms and inheritance tracking.
    /// </summary>
    /// <value>
    /// <c>true</c> if the permission was inherited from a parent entity; otherwise, <c>false</c>.
    /// Default is <c>false</c>.
    /// </value>
    /// <remarks>
    /// Inherited permissions may be overridden by explicit permissions.
    /// Used for permission precedence evaluation and inheritance tracking.
    /// </remarks>
    public bool IsInherited { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the unique identifier of the entity from which this permission was inherited.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks the source of inherited permissions for audit purposes and enables
    /// permission cascade management when parent entity permissions change
    /// in enterprise hierarchical permission structures.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// References the ID of the parent entity (library, lab, etc.) that granted
    /// this permission through inheritance. Used for permission dependency tracking.
    /// </summary>
    /// <value>
    /// A nullable <see cref="Guid"/> representing the source entity for inherited permissions,
    /// or null for explicit permissions.
    /// </value>
    /// <remarks>
    /// Used for permission cascade updates when parent permissions change.
    /// Essential for maintaining permission inheritance integrity.
    /// </remarks>
    public Guid? InheritedFromId { get; set; }
    
    /// <summary>
    /// Gets or sets the type of entity from which this permission was inherited.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Identifies the type of parent entity for proper permission inheritance
    /// processing and enables type-specific inheritance rules in enterprise
    /// environments with multiple inheritance sources and policies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Common values include "PromptLibrary", "PromptLab", "WorkflowLibrary".
    /// Used for determining appropriate inheritance processing logic.
    /// </summary>
    /// <value>
    /// A string identifying the parent entity type (e.g., "PromptLibrary"),
    /// or null for explicit permissions. Maximum length is 50 characters.
    /// </value>
    /// <remarks>
    /// Should match the actual entity type names for consistency.
    /// Used for type-specific inheritance processing and validation.
    /// </remarks>
    [StringLength(50)]
    public string? InheritedFromType { get; set; }
}
