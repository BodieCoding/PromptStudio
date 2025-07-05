namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines hierarchical permission levels for enterprise access control and authorization systems.
/// 
/// <para><strong>Business Context:</strong></para>
/// Permission levels establish systematic access control hierarchies that support collaborative workflows,
/// content governance, and security policies within enterprise AI environments. Hierarchical permissions
/// enable appropriate delegation of authority while maintaining security boundaries and audit capabilities.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Permission levels integrate with role-based access control (RBAC) systems, authorization middleware,
/// and audit logging frameworks to enforce enterprise security policies. Each level has specific
/// capabilities and inherits permissions from lower levels in the hierarchy.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Systematic authority delegation and access control
/// - Security policy enforcement with hierarchical inheritance
/// - Audit compliance with granular permission tracking
/// - Collaborative workflow support with appropriate boundaries
/// </summary>
/// <remarks>
/// <para><strong>Permission Hierarchy:</strong></para>
/// Owner (highest) → Admin → Write → Read (lowest)
/// Higher levels inherit all capabilities of lower levels plus additional privileges.
/// 
/// <para><strong>Capability Matrix:</strong></para>
/// - Read: Content consumption, discovery, and execution
/// - Write: Content modification, creation, and collaboration
/// - Admin: User management, permission assignment, configuration
/// - Owner: Full control, transfer rights, deletion capabilities
/// 
/// <para><strong>Security Principles:</strong></para>
/// - Principle of least privilege: Grant minimum necessary permissions
/// - Hierarchical inheritance: Higher levels include lower-level capabilities
/// - Audit trail: All permission exercises are logged for compliance
/// - Separation of duties: Critical operations require appropriate permission levels
/// </remarks>
/// <example>
/// A template contributor has Write permissions to modify content,
/// while a team lead has Admin permissions to manage team access.
/// </example>
public enum PermissionLevel
{
    /// <summary>
    /// Basic consumption permissions for content access, discovery, and execution.
    /// Enables users to view templates, execute workflows, and access documentation
    /// without modification capabilities, suitable for content consumers and end users.
    /// </summary>
    Read = 0,
    
    /// <summary>
    /// Content modification permissions including creation, editing, and collaborative development.
    /// Enables users to modify existing content, create new templates, and participate
    /// in collaborative development workflows while respecting governance policies.
    /// </summary>
    Write = 1,
    
    /// <summary>
    /// Administrative permissions for user management, configuration, and policy enforcement.
    /// Enables management of team access, permission assignment, policy configuration,
    /// and administrative functions while maintaining content governance boundaries.
    /// </summary>
    Admin = 2,
    
    /// <summary>
    /// Complete control permissions including ownership transfer and deletion capabilities.
    /// Provides full authority over the resource including the ability to transfer ownership,
    /// delete content, and modify fundamental access controls and governance policies.
    /// </summary>
    Owner = 3
}

