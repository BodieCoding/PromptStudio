using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a member's association with a PromptLab, managing access control and collaborative relationships.
/// 
/// <para><strong>Business Context:</strong></para>
/// LabMembers define the access control and collaboration model for PromptLabs,
/// establishing who can participate in prompt engineering activities, their level of access,
/// and their role within the lab's governance structure. This enables organizations
/// to implement proper security boundaries while facilitating team collaboration.
/// 
/// <para><strong>Technical Context:</strong></para>
/// LabMember serves as a many-to-many relationship entity between users and labs,
/// with enterprise features including audit trails, soft deletion, multi-tenancy support,
/// and role-based access control. Each membership relationship is tracked with full
/// lifecycle management and accountability.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Role-based access control for lab resources and operations
/// - Complete audit trail of membership changes and access grants
/// - Secure team collaboration with proper governance
/// - Multi-tenant isolation for enterprise security
/// - Soft deletion for compliance and data recovery
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Association Entity: Rich many-to-many relationship with additional properties
/// - Audit Trail: Comprehensive tracking through AuditableEntity and role history
/// - Soft Delete: Membership history preservation for compliance
/// - Multi-tenancy: Organizational isolation through tenant boundaries
/// 
/// <para><strong>Security Considerations:</strong></para>
/// - User ID references external identity management system
/// - Role-based permissions control access to lab resources
/// - Audit trails support security monitoring and compliance
/// - Soft deletion preserves security investigation capabilities
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index on (LabId, UserId) for membership lookups
/// - Index on UserId for user's lab enumeration
/// - Consider caching active memberships for frequent access checks
/// - Soft delete filter performance on large member datasets
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Identity Management: User authentication and profile integration
/// - Authorization System: Role-based access control enforcement
/// - Activity Tracking: User activity and engagement analytics
/// - Notification System: Membership and role change notifications
/// </remarks>
/// <example>
/// <code>
/// // Adding a new member to a lab
/// var member = new LabMember
/// {
///     LabId = labId,
///     UserId = "user123@company.com",
///     Role = LabMemberRole.Contributor,
///     JoinedAt = DateTime.UtcNow,
///     AddedBy = "admin@company.com",
///     OrganizationId = currentTenantId
/// };
/// 
/// // Updating member role
/// member.Role = LabMemberRole.Admin;
/// member.RoleUpdatedAt = DateTime.UtcNow;
/// member.RoleUpdatedBy = "owner@company.com";
/// member.UpdatedAt = DateTime.UtcNow;
/// member.UpdatedBy = "owner@company.com";
/// 
/// await memberService.UpdateAsync(member);
/// </code>
/// </example>
public class LabMember : AuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier of the PromptLab this member belongs to.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Establishes the lab context for the membership relationship,
    /// enabling proper access control and resource scoping within
    /// the organization's lab structure and governance policies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key reference to PromptLab entity.
    /// Required for all membership relationships.
    /// Used in composite indexes for performance optimization.
    /// </summary>
    /// <value>
    /// A valid Guid that references an existing PromptLab.
    /// Cannot be empty or default Guid value.
    /// </value>
    /// <remarks>
    /// Used extensively in access control queries and permission checks.
    /// Forms part of composite unique constraint with UserId.
    /// </remarks>
    /// <example>
    /// Typically set when creating membership: labId = existingLab.Id
    /// </example>
    public Guid LabId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who is a member of the lab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links the membership to a specific user in the organization's identity system,
    /// enabling personalized access control, activity tracking, and collaboration
    /// workflows within the prompt engineering environment.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// References external identity management system (Azure AD, Auth0, etc.).
    /// Required field with maximum length for integration compatibility.
    /// Used in access control and user activity queries.
    /// </summary>
    /// <value>
    /// A user identifier string (email, user ID, or similar).
    /// Cannot be null or empty. Maximum length is 200 characters.
    /// </value>
    /// <remarks>
    /// Format depends on identity provider (e.g., email, GUID, username).
    /// Forms part of composite unique constraint with LabId.
    /// Future versions may include proper User entity relationships.
    /// </remarks>
    /// <example>
    /// Examples: "john.doe@company.com", "user-123", "jdoe"
    /// </example>
    [Required]
    [StringLength(200)]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role that defines the member's permissions within the lab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Implements role-based access control (RBAC) for lab resources,
    /// enabling organizations to grant appropriate permissions while
    /// maintaining security boundaries and operational governance.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based role definition with predefined permission sets.
    /// Used by authorization middleware for access control decisions.
    /// Role changes are tracked through audit trail and role update fields.
    /// </summary>
    /// <value>
    /// A <see cref="LabMemberRole"/> enum value defining access level.
    /// Default is typically set by business rules during member creation.
    /// </value>
    /// <remarks>
    /// Role changes should update RoleUpdatedAt and RoleUpdatedBy fields.
    /// Higher privilege roles typically inherit lower privilege permissions.
    /// Role definitions should align with organizational access control policies.
    /// </remarks>
    /// <example>
    /// Common progression: Viewer → Contributor → Admin → Owner
    /// </example>
    public LabMemberRole Role { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the member first joined the lab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides membership history and tenure tracking for analytics,
    /// governance reporting, and member engagement analysis,
    /// supporting team management and collaboration insights.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Immutable timestamp set once during member creation.
    /// Used for reporting, analytics, and membership lifecycle tracking.
    /// Should be in UTC for consistent timezone handling.
    /// </summary>
    /// <value>
    /// UTC timestamp of when the membership was created.
    /// Typically set to current UTC time during member creation.
    /// </value>
    /// <remarks>
    /// Distinct from CreatedAt to track actual membership start vs. entity creation.
    /// Useful for membership analytics and tenure calculations.
    /// Should remain unchanged even if membership is temporarily deactivated.
    /// </remarks>
    /// <example>
    /// JoinedAt = DateTime.UtcNow; // Set during initial membership creation
    /// </example>
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the identifier of who added this member to the lab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides accountability and audit trail for membership decisions,
    /// supporting security monitoring, compliance reporting, and
    /// governance workflows in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// References the user who initiated the membership creation.
    /// Optional field allowing for system-initiated memberships.
    /// Used in audit reports and security investigations.
    /// </summary>
    /// <value>
    /// User identifier of the member creator (email, user ID, or similar).
    /// Can be null for system-initiated memberships. Maximum length is 200 characters.
    /// </value>
    /// <remarks>
    /// Distinct from CreatedBy which tracks entity-level creation.
    /// Important for understanding membership approval workflows.
    /// May reference admin users, lab owners, or automated systems.
    /// </remarks>
    /// <example>
    /// Examples: "admin@company.com", "lab-owner-123", "system-automation"
    /// </example>
    [StringLength(200)]
    public string? AddedBy { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the member's role was last changed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks role evolution and permission changes for security auditing,
    /// compliance reporting, and access control governance,
    /// providing visibility into privilege escalation and access management.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Updated whenever the Role property changes.
    /// Null indicates the role has never been changed since joining.
    /// Used for security monitoring and access control auditing.
    /// </summary>
    /// <value>
    /// UTC timestamp of the most recent role change.
    /// Null if the role has never been updated since creation.
    /// </value>
    /// <remarks>
    /// Should be updated atomically with Role changes.
    /// Important for detecting unauthorized privilege escalation.
    /// Used in security reports and compliance auditing.
    /// </remarks>
    /// <example>
    /// // Updated when changing roles
    /// member.Role = LabMemberRole.Admin;
    /// member.RoleUpdatedAt = DateTime.UtcNow;
    /// </example>
    public DateTime? RoleUpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of who last updated the member's role.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides accountability for role changes and permission modifications,
    /// supporting security investigations, compliance auditing, and
    /// governance oversight of access control decisions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Updated whenever the Role property changes.
    /// References the user who initiated the role modification.
    /// Used in security monitoring and audit trail reconstruction.
    /// </summary>
    /// <value>
    /// User identifier of who modified the role (email, user ID, or similar).
    /// Null if the role has never been updated. Maximum length is 200 characters.
    /// </value>
    /// <remarks>
    /// Should be updated atomically with Role and RoleUpdatedAt changes.
    /// Critical for understanding authorization decision accountability.
    /// May reference different users than AddedBy for role evolution tracking.
    /// </remarks>
    /// <example>
    /// Examples: "lab-admin@company.com", "security-officer-456"
    /// </example>
    [StringLength(200)]
    public string? RoleUpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the member's last recorded activity in the lab.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables engagement analytics, inactive member identification,
    /// and collaboration insights for team management and lab optimization,
    /// supporting member retention and productivity analysis.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Updated by application logic when member performs lab activities.
    /// Used for analytics, member engagement scoring, and cleanup policies.
    /// Should be updated for meaningful activities (not just authentication).
    /// </summary>
    /// <value>
    /// UTC timestamp of the last meaningful activity.
    /// Null if no activity has been recorded since joining.
    /// </value>
    /// <remarks>
    /// Activity definition should be consistent across the application.
    /// Used for identifying inactive members and engagement analytics.
    /// Consider batching updates for performance in high-activity scenarios.
    /// </remarks>
    /// <example>
    /// Activities: template creation, execution, library access, collaboration
    /// </example>
    public DateTime? LastActivity { get; set; }

    // Navigation properties

    /// <summary>
    /// Gets or sets the PromptLab this member belongs to.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides navigation to the lab context for the membership,
    /// enabling access to lab resources, settings, and governance policies
    /// through object relationship mapping.
    /// </summary>
    /// <value>
    /// The PromptLab entity this member belongs to.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework relationships.
    /// Use Include() in queries when lab details are needed.
    /// </remarks>
    public virtual PromptLab? Lab { get; set; }
}
