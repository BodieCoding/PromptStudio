using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.Interfaces.Security;

/// <summary>
/// Service interface for comprehensive security and access control management in enterprise LLMOps environments.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// <para>Core security service operating in the business service layer, responsible for authentication,
/// authorization, role-based access control (RBAC), and security policy enforcement. Integrates with
/// identity providers, supports multi-tenancy, and provides comprehensive audit trails for all
/// security-related operations in prompt engineering workflows.</para>
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// <para>Implementations must enforce principle of least privilege, support hierarchical permission
/// inheritance, maintain secure session management, and provide comprehensive security analytics.
/// All security operations must be auditable and support compliance with enterprise security frameworks.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
/// <item><description>Implement defense-in-depth security patterns with multiple validation layers</description></item>
/// <item><description>Support fine-grained permissions for all PromptStudio resources</description></item>
/// <item><description>Maintain secure audit trails for all authorization decisions</description></item>
/// <item><description>Integrate with enterprise identity providers (SAML, OIDC, LDAP)</description></item>
/// <item><description>Enforce data classification and sensitivity-based access controls</description></item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Integrates with all domain services for consistent authorization enforcement</description></item>
/// <item><description>Coordinates with audit services for comprehensive security logging</description></item>
/// <item><description>Connects to external identity providers for federated authentication</description></item>
/// <item><description>Utilizes caching services for performance-optimized permission lookups</description></item>
/// <item><description>Implements event-driven architecture for real-time security monitoring</description></item>
/// </list>
/// 
/// <para><strong>Security Considerations:</strong></para>
/// <list type="bullet">
/// <item><description>All permission checks must be fail-safe (deny by default)</description></item>
/// <item><description>Sensitive operations require additional verification and logging</description></item>
/// <item><description>Token management follows secure storage and rotation practices</description></item>
/// <item><description>Rate limiting and abuse detection protect against security threats</description></item>
/// </list>
/// </remarks>
public interface ISecurityService
{
    #region Authentication and Authorization

    /// <summary>
    /// Validates if a user has the specified permission for a given resource within organizational context.
    /// Supports hierarchical permission inheritance and tenant-specific access control.
    /// </summary>
    /// <param name="userId">User identifier to check permissions for</param>
    /// <param name="resourceType">Type of resource being accessed (Lab, Library, Template, etc.)</param>
    /// <param name="resourceId">Unique identifier of the specific resource</param>
    /// <param name="permission">Required permission level for the operation</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>True if user has required permission, false otherwise</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are invalid</exception>
    /// <exception cref="SecurityException">Thrown when security validation fails</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Permissions are checked hierarchically (Lab → Library → Template)</description></item>
    /// <item><description>Organization-level permissions override resource-specific permissions</description></item>
    /// <item><description>System administrators have implicit permissions for all resources</description></item>
    /// <item><description>Inactive or suspended users are denied all permissions</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements caching for frequently accessed permissions</description></item>
    /// <item><description>Logs all permission checks for security auditing</description></item>
    /// <item><description>Supports real-time permission revocation and updates</description></item>
    /// <item><description>Optimizes for high-volume permission validation scenarios</description></item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// // Check if user can edit a specific prompt template
    /// var canEdit = await securityService.HasPermissionAsync(
    ///     userId,
    ///     "PromptTemplate",
    ///     templateId,
    ///     PermissionLevel.Edit,
    ///     organizationId,
    ///     cancellationToken
    /// );
    /// </code>
    /// </remarks>
    Task<bool> HasPermissionAsync(
        string userId,
        string resourceType,
        Guid resourceId,
        PermissionLevel permission,
        Guid organizationId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all permissions for a user within a specific organization context.
    /// Supports filtering and pagination for large-scale enterprise deployments.
    /// </summary>
    /// <param name="userId">User identifier to retrieve permissions for</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="resourceType">Optional filter by resource type</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated list of user permissions with resource details</returns>
    /// <exception cref="ArgumentException">Thrown when userId or organizationId is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when requestor lacks permission to view user permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Users can view their own permissions without additional authorization</description></item>
    /// <item><description>Administrators can view permissions for users in their organization</description></item>
    /// <item><description>Results include both direct and inherited permissions</description></item>
    /// <item><description>Inactive permissions are excluded from results</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements efficient query patterns for large permission sets</description></item>
    /// <item><description>Supports real-time permission updates and notifications</description></item>
    /// <item><description>Provides detailed permission source information (role, direct, inherited)</description></item>
    /// <item><description>Optimizes for administrative permission management interfaces</description></item>
    /// </list>
    /// </remarks>
    Task<PagedResult<UserPermission>> GetUserPermissionsAsync(
        string userId,
        Guid organizationId,
        string? resourceType = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Grants specific permission to a user for a resource with comprehensive audit tracking.
    /// Supports temporary permissions and delegation scenarios.
    /// </summary>
    /// <param name="userId">User identifier to grant permission to</param>
    /// <param name="resourceType">Type of resource being accessed</param>
    /// <param name="resourceId">Unique identifier of the specific resource</param>
    /// <param name="permission">Permission level being granted</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="grantedBy">User identifier of the permission grantor</param>
    /// <param name="expiresAt">Optional expiration time for temporary permissions</param>
    /// <param name="reason">Business justification for permission grant</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Permission grant result with audit information</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when grantor lacks permission delegation rights</exception>
    /// <exception cref="SecurityException">Thrown when permission grant violates security policies</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Grantor must have equal or higher permission level to delegate</description></item>
    /// <item><description>Temporary permissions automatically expire and are revoked</description></item>
    /// <item><description>Permission grants are subject to organizational approval workflows</description></item>
    /// <item><description>All permission grants create comprehensive audit trails</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements approval workflows for sensitive permission grants</description></item>
    /// <item><description>Supports notification systems for permission changes</description></item>
    /// <item><description>Maintains detailed audit logs for compliance requirements</description></item>
    /// <item><description>Validates against organizational security policies</description></item>
    /// </list>
    /// </remarks>
    Task<PermissionGrantResult> GrantPermissionAsync(
        string userId,
        string resourceType,
        Guid resourceId,
        PermissionLevel permission,
        Guid organizationId,
        string grantedBy,
        DateTimeOffset? expiresAt = null,
        string? reason = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Revokes specific permission from a user with comprehensive audit tracking.
    /// Supports cascade revocation for dependent permissions.
    /// </summary>
    /// <param name="userId">User identifier to revoke permission from</param>
    /// <param name="resourceType">Type of resource being accessed</param>
    /// <param name="resourceId">Unique identifier of the specific resource</param>
    /// <param name="permission">Permission level being revoked</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="revokedBy">User identifier of the permission revoker</param>
    /// <param name="reason">Business justification for permission revocation</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Permission revocation result with audit information</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when revoker lacks permission management rights</exception>
    /// <exception cref="SecurityException">Thrown when permission revocation violates security policies</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Revoker must have administrative rights for the resource</description></item>
    /// <item><description>Permission revocation may trigger cascade revocation of dependent permissions</description></item>
    /// <item><description>System administrators cannot have core permissions revoked</description></item>
    /// <item><description>All permission revocations create comprehensive audit trails</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements immediate permission cache invalidation</description></item>
    /// <item><description>Supports notification systems for permission changes</description></item>
    /// <item><description>Maintains detailed audit logs for compliance requirements</description></item>
    /// <item><description>Validates impact analysis before revocation execution</description></item>
    /// </list>
    /// </remarks>
    Task<PermissionRevocationResult> RevokePermissionAsync(
        string userId,
        string resourceType,
        Guid resourceId,
        PermissionLevel permission,
        Guid organizationId,
        string revokedBy,
        string? reason = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Role Management

    /// <summary>
    /// Creates a new security role with defined permissions and organizational scope.
    /// Supports hierarchical role inheritance and template-based role creation.
    /// </summary>
    /// <param name="name">Display name for the role, must be unique within organization</param>
    /// <param name="description">Detailed description of role purpose and scope</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="permissions">Set of permissions included in this role</param>
    /// <param name="createdBy">User identifier of the role creator</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Created role with generated identifiers and permission assignments</returns>
    /// <exception cref="ArgumentException">Thrown when role name is invalid or permissions are malformed</exception>
    /// <exception cref="DuplicateRoleNameException">Thrown when role name already exists within organization</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when creator lacks role management permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Role names must be unique within organization scope</description></item>
    /// <item><description>Creator must have role management permissions</description></item>
    /// <item><description>Roles inherit permissions from parent roles in hierarchy</description></item>
    /// <item><description>System roles cannot be modified or deleted</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Validates permission consistency and security policies</description></item>
    /// <item><description>Supports role templates for common organizational patterns</description></item>
    /// <item><description>Maintains audit trails for all role modifications</description></item>
    /// <item><description>Implements permission optimization for role-based access</description></item>
    /// </list>
    /// </remarks>
    Task<SecurityRole> CreateRoleAsync(
        string name,
        string description,
        Guid organizationId,
        IEnumerable<RolePermission> permissions,
        string createdBy,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Assigns a security role to a user within organizational context.
    /// Supports temporary role assignments and delegation scenarios.
    /// </summary>
    /// <param name="userId">User identifier to assign role to</param>
    /// <param name="roleId">Role identifier being assigned</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="assignedBy">User identifier of the role assigner</param>
    /// <param name="expiresAt">Optional expiration time for temporary role assignments</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Role assignment result with audit information</returns>
    /// <exception cref="ArgumentException">Thrown when user or role identifiers are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when assigner lacks role management permissions</exception>
    /// <exception cref="SecurityException">Thrown when role assignment violates security policies</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Assigner must have role management permissions</description></item>
    /// <item><description>Users cannot be assigned roles beyond their clearance level</description></item>
    /// <item><description>Temporary role assignments automatically expire</description></item>
    /// <item><description>Role assignments are subject to approval workflows for sensitive roles</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements immediate permission cache refresh for assigned users</description></item>
    /// <item><description>Supports notification systems for role assignment changes</description></item>
    /// <item><description>Maintains comprehensive audit trails for compliance</description></item>
    /// <item><description>Validates security clearance and organizational policies</description></item>
    /// </list>
    /// </remarks>
    Task<RoleAssignmentResult> AssignRoleAsync(
        string userId,
        Guid roleId,
        Guid organizationId,
        string assignedBy,
        DateTimeOffset? expiresAt = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Security Analytics

    /// <summary>
    /// Retrieves comprehensive security analytics for organizational security monitoring.
    /// Provides insights into access patterns, permission usage, and security anomalies.
    /// </summary>
    /// <param name="organizationId">Organization context for analytics scope</param>
    /// <param name="DateTimeRange">Time range for analytics data collection</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive security analytics with insights and recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when organization ID or date range is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when requestor lacks security analytics access</exception>
    /// <remarks>
    /// <para><strong>Analytics Categories:</strong></para>
    /// <list type="bullet">
    /// <item><description>Access pattern analysis and anomaly detection</description></item>
    /// <item><description>Permission usage statistics and optimization recommendations</description></item>
    /// <item><description>Security incident tracking and trend analysis</description></item>
    /// <item><description>Role effectiveness and assignment pattern analysis</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Aggregates data from multiple security event sources</description></item>
    /// <item><description>Implements machine learning for anomaly detection</description></item>
    /// <item><description>Provides real-time security dashboards and alerting</description></item>
    /// <item><description>Supports compliance reporting and audit trail analysis</description></item>
    /// </list>
    /// </remarks>
    Task<SecurityAnalytics> GetSecurityAnalyticsAsync(
        Guid organizationId,
        DateTimeRange DateTimeRange,
        CancellationToken cancellationToken = default);

    #endregion
}

/// <summary>
/// Represents a user permission with resource and level details.
/// </summary>
public record UserPermission(
    string UserId,
    string ResourceType,
    Guid ResourceId,
    PermissionLevel Permission,
    string Source, // "Direct", "Role", "Inherited"
    DateTimeOffset? ExpiresAt,
    DateTimeOffset GrantedAt,
    string GrantedBy);

/// <summary>
/// Result of a permission grant operation with audit information.
/// </summary>
public record PermissionGrantResult(
    bool Success,
    string? ErrorMessage,
    string AuditId,
    DateTimeOffset GrantedAt,
    bool RequiresApproval);

/// <summary>
/// Result of a permission revocation operation with audit information.
/// </summary>
public record PermissionRevocationResult(
    bool Success,
    string? ErrorMessage,
    string AuditId,
    DateTimeOffset RevokedAt,
    int AffectedDependencies);

/// <summary>
/// Represents a security role with permissions and organizational context.
/// </summary>
public record SecurityRole(
    Guid Id,
    string Name,
    string Description,
    Guid OrganizationId,
    IEnumerable<RolePermission> Permissions,
    DateTimeOffset CreatedAt,
    string CreatedBy,
    bool IsSystemRole);

/// <summary>
/// Represents a permission within a security role.
/// </summary>
public record RolePermission(
    string ResourceType,
    PermissionLevel Permission,
    string? ResourceFilter); // Optional filter for scoped permissions

/// <summary>
/// Result of a role assignment operation with audit information.
/// </summary>
public record RoleAssignmentResult(
    bool Success,
    string? ErrorMessage,
    string AuditId,
    DateTimeOffset AssignedAt,
    DateTimeOffset? ExpiresAt);

/// <summary>
/// Comprehensive security analytics for organizational monitoring.
/// </summary>
public record SecurityAnalytics(
    Guid OrganizationId,
    DateTimeRange DateTimeRange,
    AccessPatternAnalytics AccessPatterns,
    PermissionUsageAnalytics PermissionUsage,
    SecurityIncidentAnalytics SecurityIncidents,
    RoleEffectivenessAnalytics RoleEffectiveness,
    IEnumerable<SecurityRecommendation> Recommendations,
    DateTimeOffset GeneratedAt);

/// <summary>
/// Analytics for user access patterns and anomaly detection.
/// </summary>
public record AccessPatternAnalytics(
    int TotalAccessAttempts,
    int SuccessfulAccesses,
    int FailedAccesses,
    double AverageSessionDuration,
    IEnumerable<AccessAnomaly> DetectedAnomalies,
    IEnumerable<UserAccessPattern> TopUsers);

/// <summary>
/// Analytics for permission usage and optimization opportunities.
/// </summary>
public record PermissionUsageAnalytics(
    int TotalPermissions,
    int ActivePermissions,
    int UnusedPermissions,
    double PermissionUtilizationRate,
    IEnumerable<PermissionUsagePattern> UsagePatterns,
    IEnumerable<PermissionOptimizationOpportunity> OptimizationOpportunities);

/// <summary>
/// Analytics for security incidents and trend analysis.
/// </summary>
public record SecurityIncidentAnalytics(
    int TotalIncidents,
    int ResolvedIncidents,
    int OpenIncidents,
    double AverageResolutionTime,
    IEnumerable<SecurityIncidentTrend> Trends,
    IEnumerable<SecurityThreatPattern> ThreatPatterns);

/// <summary>
/// Analytics for role effectiveness and assignment patterns.
/// </summary>
public record RoleEffectivenessAnalytics(
    int TotalRoles,
    int ActiveRoles,
    double AverageRoleUsage,
    IEnumerable<RoleUsagePattern> RoleUsagePatterns,
    IEnumerable<RoleOptimizationOpportunity> OptimizationOpportunities);

/// <summary>
/// Security recommendation for organizational improvement.
/// </summary>
public record SecurityRecommendation(
    string Type,
    string Title,
    string Description,
    string Impact,
    string Priority,
    IEnumerable<string> ActionItems);

/// <summary>
/// Detected access anomaly requiring investigation.
/// </summary>
public record AccessAnomaly(
    string Type,
    string Description,
    string UserId,
    DateTimeOffset DetectedAt,
    string Severity,
    string RecommendedAction);

/// <summary>
/// User access pattern for behavior analysis.
/// </summary>
public record UserAccessPattern(
    string UserId,
    int AccessCount,
    TimeSpan AverageSessionDuration,
    IEnumerable<string> CommonResourceTypes,
    DateTimeOffset LastAccess);

/// <summary>
/// Permission usage pattern for optimization analysis.
/// </summary>
public record PermissionUsagePattern(
    string ResourceType,
    PermissionLevel Permission,
    int UsageCount,
    double UtilizationRate,
    DateTimeOffset LastUsed);

/// <summary>
/// Permission optimization opportunity for efficiency improvement.
/// </summary>
public record PermissionOptimizationOpportunity(
    string Type,
    string Description,
    string ResourceType,
    int AffectedUsers,
    string RecommendedAction,
    string ExpectedBenefit);

/// <summary>
/// Security incident trend for pattern analysis.
/// </summary>
public record SecurityIncidentTrend(
    string IncidentType,
    int Count,
    double ChangePercentage,
    string Trend,
    DateTimeOffset PeriodStart,
    DateTimeOffset PeriodEnd);

/// <summary>
/// Security threat pattern for proactive defense.
/// </summary>
public record SecurityThreatPattern(
    string ThreatType,
    string Description,
    int Frequency,
    string Severity,
    IEnumerable<string> IndicatorsOfCompromise,
    string MitigationStrategy);

/// <summary>
/// Role usage pattern for role optimization.
/// </summary>
public record RoleUsagePattern(
    Guid RoleId,
    string RoleName,
    int AssignedUsers,
    double UsageFrequency,
    IEnumerable<string> CommonResourceAccess);

/// <summary>
/// Role optimization opportunity for efficiency improvement.
/// </summary>
public record RoleOptimizationOpportunity(
    string Type,
    string Description,
    Guid RoleId,
    string RoleName,
    string RecommendedAction,
    string ExpectedBenefit);
