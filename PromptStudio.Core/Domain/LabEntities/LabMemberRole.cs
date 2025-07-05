namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines hierarchical access control roles for lab members, enabling fine-grained permission management and collaborative governance within PromptLabs.
/// 
/// <para><strong>Business Context:</strong></para>
/// LabMemberRole provides a structured approach to access control and collaboration within PromptLabs,
/// ensuring appropriate permissions for different team members based on their responsibilities
/// and organizational roles. This supports enterprise governance, security compliance, and
/// effective team collaboration while maintaining clear accountability and operational boundaries.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The role hierarchy implements a progressive permission model where higher roles inherit
/// capabilities from lower roles, enabling efficient permission management and clear
/// escalation paths for access control within the lab security framework.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Hierarchical permission structure for clear access control governance
/// - Role-based collaboration enabling appropriate team member participation
/// - Enterprise security compliance through structured access management
/// - Scalable permission model supporting organizations of various sizes
/// - Clear accountability through explicit role assignments and capabilities
/// </summary>
/// <remarks>
/// <para><strong>Permission Hierarchy:</strong></para>
/// Viewer → Executor → Contributor → Manager → Admin → Owner
/// Each role inherits all capabilities from lower-level roles.
/// 
/// <para><strong>Role Capabilities Matrix:</strong></para>
/// - Viewer: Read-only access to all lab content
/// - Executor: + Execute templates and workflows
/// - Contributor: + Create, edit, and manage own content
/// - Manager: + Manage all lab content and workflows
/// - Admin: + Manage lab settings and member roles
/// - Owner: + Full lab ownership and deletion rights
/// 
/// <para><strong>Security Considerations:</strong></para>
/// - Role assignments should follow principle of least privilege
/// - Regular role reviews recommended for access governance
/// - Owner role should be limited to minimize security risk
/// - Role changes should be logged for audit compliance
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Permission System: Role-based access control enforcement
/// - User Interface: Role-appropriate feature visibility
/// - Audit System: Role change tracking and compliance
/// - Notification System: Role-based communication and alerts
/// </remarks>
/// <example>
/// <code>
/// // Assign appropriate roles based on team structure
/// var teamMembers = new[]
/// {
///     new LabMember { UserId = "analyst-001", Role = LabMemberRole.Viewer },
///     new LabMember { UserId = "developer-001", Role = LabMemberRole.Contributor },
///     new LabMember { UserId = "team-lead-001", Role = LabMemberRole.Manager },
///     new LabMember { UserId = "department-head", Role = LabMemberRole.Admin }
/// };
/// 
/// // Check permissions based on role
/// if (member.Role >= LabMemberRole.Contributor)
/// {
///     // Allow content creation and editing
///     await templateService.CreateTemplateAsync(newTemplate);
/// }
/// 
/// if (member.Role >= LabMemberRole.Manager)
/// {
///     // Allow lab management operations
///     await labService.UpdateLabSettingsAsync(labSettings);
/// }
/// </code>
/// </example>
public enum LabMemberRole
{
    /// <summary>
    /// Read-only access to lab content for monitoring and review purposes.
    /// <value>0 - Basic viewing permissions without modification capabilities</value>
    /// </summary>
    /// <remarks>
    /// Ideal for stakeholders, auditors, and team members who need visibility
    /// into lab activities without requiring modification permissions.
    /// </remarks>
    Viewer = 0,

    /// <summary>
    /// Execution permissions for running templates and workflows without modification rights.
    /// <value>1 - Viewer permissions plus template and workflow execution capabilities</value>
    /// </summary>
    /// <remarks>
    /// Suitable for operators, testers, and users who need to run existing
    /// workflows but should not modify lab content or configuration.
    /// </remarks>
    Executor = 1,

    /// <summary>
    /// Content creation and editing permissions for active collaboration and development.
    /// <value>2 - Executor permissions plus content creation and editing capabilities</value>
    /// </summary>
    /// <remarks>
    /// Appropriate for developers, content creators, and team members actively
    /// contributing to lab development and content creation efforts.
    /// </remarks>
    Contributor = 2,

    /// <summary>
    /// Comprehensive content management permissions for operational oversight and coordination.
    /// <value>3 - Contributor permissions plus management of all lab content and workflows</value>
    /// </summary>
    /// <remarks>
    /// Designed for team leads, project managers, and senior contributors who
    /// coordinate lab activities and manage content across the entire lab.
    /// </remarks>
    Manager = 3,

    /// <summary>
    /// Administrative permissions for lab configuration, member management, and governance.
    /// <value>4 - Manager permissions plus lab administration and member role management</value>
    /// </summary>
    /// <remarks>
    /// Reserved for administrators who manage lab settings, member access,
    /// and organizational policies while maintaining operational oversight.
    /// </remarks>
    Admin = 4,

    /// <summary>
    /// Complete ownership with full control including lab lifecycle and deletion rights.
    /// <value>5 - All permissions including lab deletion and ownership transfer</value>
    /// </summary>
    /// <remarks>
    /// Ultimate responsibility role with complete control over lab existence,
    /// ownership transfer, and all administrative functions. Should be carefully assigned.
    /// </remarks>
    Owner = 5
}
