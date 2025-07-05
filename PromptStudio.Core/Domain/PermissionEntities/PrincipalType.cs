namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the types of security principals supported in enterprise identity and access management systems.
/// 
/// <para><strong>Business Context:</strong></para>
/// Principal types enable comprehensive identity management for diverse organizational structures including
/// individual users, role-based access, team collaboration, group permissions, and automated system access.
/// This supports complex enterprise scenarios with both human and machine identities requiring AI system access.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Principal types integrate with identity providers (Active Directory, Azure AD, LDAP), authentication
/// systems, and authorization frameworks to provide unified access control across diverse identity sources.
/// Each type has specific authentication requirements and permission inheritance characteristics.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Comprehensive identity management for complex organizations
/// - Unified access control across diverse identity sources
/// - Support for both human and machine identity scenarios
/// - Flexible permission assignment and inheritance models
/// </summary>
/// <remarks>
/// <para><strong>Identity Categories:</strong></para>
/// - Individual: User accounts for personal access and accountability
/// - Organizational: Role, Team, Group for structured organizational access
/// - System: ServiceAccount for automated and programmatic access
/// 
/// <para><strong>Permission Models:</strong></para>
/// - Direct assignment: Permissions granted directly to principal
/// - Role-based: Permissions inherited through role membership
/// - Group-based: Permissions inherited through group membership
/// - Service-based: Permissions for automated system integration
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// Principal types determine authentication methods, permission inheritance,
/// audit logging requirements, and access token characteristics for enterprise integration.
/// </remarks>
/// <example>
/// A user has direct access, inherits permissions from their team,
/// while a CI/CD system uses a service account for automated operations.
/// </example>
public enum PrincipalType
{
    /// <summary>
    /// Individual user account representing a specific person with direct access rights.
    /// Supports personal authentication, individual audit trails, and direct permission
    /// assignment for user-specific access control and accountability.
    /// </summary>
    User = 0,
    
    /// <summary>
    /// Functional role definition providing permission templates for similar job functions.
    /// Enables role-based access control (RBAC) where users inherit permissions through
    /// role assignment, supporting scalable permission management across organizations.
    /// </summary>
    Role = 1,
    
    /// <summary>
    /// Organizational team providing collaborative access and shared responsibilities.
    /// Supports team-based collaboration with shared resources, collective ownership,
    /// and collaborative workflow management within organizational structures.
    /// </summary>
    Team = 2,
    
    /// <summary>
    /// Administrative group for organizing multiple principals with similar access needs.
    /// Enables efficient permission management for large user populations through
    /// group-based access control with hierarchical permission inheritance.
    /// </summary>
    Group = 3,
    
    /// <summary>
    /// System service account for automated processes and programmatic access.
    /// Supports machine-to-machine authentication, API access, automated workflows,
    /// and integration scenarios requiring non-interactive system authentication.
    /// </summary>
    ServiceAccount = 4
}

