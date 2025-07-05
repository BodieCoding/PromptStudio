using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents granular access control and permission management for workflow libraries, enabling enterprise-grade security, collaboration, and resource governance.
/// 
/// <para><strong>Business Context:</strong></para>
/// WorkflowLibraryPermission provides fine-grained access control for workflow libraries, enabling
/// organizations to implement secure collaboration, role-based access, and resource governance
/// while maintaining compliance requirements and operational security. This supports enterprise
/// scenarios where different users, teams, and systems require varying levels of access to
/// workflow libraries with specific capabilities and resource constraints.
/// 
/// <para><strong>Technical Context:</strong></para>
/// WorkflowLibraryPermission implements a capability-based access control system with temporal
/// constraints, resource limits, and delegation capabilities within the enterprise security
/// framework. The entity supports multi-principal types, quota management, and audit trails
/// for comprehensive security governance and compliance tracking.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Fine-grained access control with capability-based permissions
/// - Resource governance through cost limits, time constraints, and execution quotas
/// - Temporal access control with permission expiration and approval workflows
/// - Enterprise compliance through comprehensive audit trails and delegation controls
/// - Multi-principal support for users, roles, groups, and service accounts
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Capability Pattern: Fine-grained permission control through specific capabilities
/// - Resource Governor Pattern: Cost, time, and quota limits for resource management
/// - Temporal Security Pattern: Time-bound permissions with expiration controls
/// - Delegation Pattern: Permission delegation with governance and audit trails
/// - Multi-Principal Pattern: Support for users, roles, groups, and service accounts
/// 
/// <para><strong>Access Control Model:</strong></para>
/// - Principal-based permissions supporting multiple identity types
/// - Capability-driven access control for specific workflow operations
/// - Resource limits for cost, execution time, and usage quotas
/// - Context restrictions for environment-specific access control
/// - Approval workflows for permission grants and modifications
/// 
/// <para><strong>Resource Governance:</strong></para>
/// - Cost limits prevent budget overruns and unauthorized usage
/// - Time limits control execution duration and resource consumption
/// - Execution quotas manage usage patterns and prevent abuse
/// - Context restrictions limit access to appropriate environments
/// - Delegation controls enable secure permission sharing
/// 
/// <para><strong>Security Considerations:</strong></para>
/// - Principle of least privilege through capability-based access
/// - Temporal security with automatic permission expiration
/// - Audit trails for all permission grants, modifications, and usage
/// - Resource limits prevent system abuse and cost overruns
/// - Approval workflows ensure proper authorization governance
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Identity Provider: Principal authentication and authorization
/// - Resource Monitor: Cost, time, and quota tracking and enforcement
/// - Approval System: Permission grant and modification workflows
/// - Audit Platform: Security event logging and compliance reporting
/// - Execution Engine: Permission validation and capability enforcement
/// </remarks>
/// <example>
/// <code>
/// // Grant comprehensive access to a team lead
/// var teamLeadPermission = new WorkflowLibraryPermission
/// {
///     WorkflowLibraryId = automationLibrary.Id,
///     PrincipalId = teamLeadUser.Id,
///     PrincipalType = PrincipalType.User,
///     Permission = PermissionLevel.Admin,
///     Capabilities = JsonSerializer.Serialize(new[]
///     {
///         "create_workflows", "execute_workflows", "manage_versions",
///         "view_analytics", "manage_permissions", "approve_changes"
///     }),
///     CanDelegate = true,
///     CostLimit = 100.00m,
///     ExecutionQuota = 1000,
///     AllowedContexts = JsonSerializer.Serialize(new[] { "development", "staging", "production" }),
///     GrantReason = "Team lead role with full library management capabilities",
///     ApprovedBy = departmentManager.Id,
///     ApprovedAt = DateTime.UtcNow
/// };
/// 
/// // Grant limited access to a developer
/// var developerPermission = new WorkflowLibraryPermission
/// {
///     WorkflowLibraryId = automationLibrary.Id,
///     PrincipalId = developerUser.Id,
///     PrincipalType = PrincipalType.User,
///     Permission = PermissionLevel.Write,
///     Capabilities = JsonSerializer.Serialize(new[] { "create_workflows", "execute_workflows" }),
///     CostLimit = 25.00m,
///     TimeLimit = 300, // 5 minutes max
///     ExecutionQuota = 100,
///     AllowedContexts = JsonSerializer.Serialize(new[] { "development" }),
///     ExpiresAt = DateTime.UtcNow.AddMonths(6),
///     GrantReason = "Developer access for workflow development and testing"
/// };
/// 
/// // Grant service account access for automation
/// var servicePermission = new WorkflowLibraryPermission
/// {
///     WorkflowLibraryId = automationLibrary.Id,
///     PrincipalId = "automation-service-001",
///     PrincipalType = PrincipalType.ServiceAccount,
///     Permission = PermissionLevel.Execute,
///     Capabilities = JsonSerializer.Serialize(new[] { "execute_workflows" }),
///     CostLimit = 500.00m,
///     ExecutionQuota = 10000,
///     QuotaPeriodHours = 24,
///     AllowedContexts = JsonSerializer.Serialize(new[] { "production" }),
///     GrantReason = "Production automation service access"
/// };
/// 
/// await repository.AddRangeAsync(new[] { teamLeadPermission, developerPermission, servicePermission });
/// </code>
/// </example>
public class WorkflowLibraryPermission : AuditableEntity
{
    /// <summary>
    /// Reference to the workflow library for which permissions are being granted.
    /// <value>Guid identifier linking this permission to the target workflow library</value>
    /// </summary>
    /// <remarks>
    /// Establishes the scope of access control and enables efficient permission
    /// queries and library-specific security policy enforcement.
    /// </remarks>
    public Guid WorkflowLibraryId { get; set; }

    /// <summary>
    /// Navigation property to the target workflow library for permission scope resolution.
    /// <value>WorkflowLibrary entity representing the access control target</value>
    /// </summary>
    /// <remarks>
    /// Provides access to library metadata, configuration, and hierarchical
    /// permission inheritance for comprehensive access control evaluation.
    /// </remarks>
    public virtual WorkflowLibrary WorkflowLibrary { get; set; } = null!;
    
    /// <summary>
    /// Unique identifier of the principal (user, role, group, or service account) receiving permissions.
    /// <value>String identifier for the security principal being granted access</value>
    /// </summary>
    /// <remarks>
    /// Supports multiple identity provider formats and principal types for
    /// flexible integration with enterprise authentication and authorization systems.
    /// </remarks>
    [Required]
    [StringLength(100)]
    public string PrincipalId { get; set; } = string.Empty;
    
    /// <summary>
    /// Classification of the principal type for appropriate permission processing and validation.
    /// <value>PrincipalType enum indicating the nature of the security principal</value>
    /// </summary>
    /// <remarks>
    /// Determines permission inheritance rules, delegation capabilities, and
    /// appropriate validation mechanisms for different principal categories.
    /// </remarks>
    public PrincipalType PrincipalType { get; set; } = PrincipalType.User;
    
    /// <summary>
    /// Base level of access granted to the principal for library operations.
    /// <value>PermissionLevel enum defining the fundamental access scope</value>
    /// </summary>
    /// <remarks>
    /// Provides the foundation permission level that can be further refined
    /// through specific capabilities and resource constraints for granular control.
    /// </remarks>
    public PermissionLevel Permission { get; set; } = PermissionLevel.Read;
    
    /// <summary>
    /// Optional expiration timestamp for temporal access control and automatic permission cleanup.
    /// <value>DateTime in UTC when the permission automatically expires, null for permanent access</value>
    /// </summary>
    /// <remarks>
    /// Supports temporary access grants, contractor permissions, and time-bound
    /// security policies with automatic cleanup and compliance requirements.
    /// </remarks>
    public DateTime? ExpiresAt { get; set; }
    
    /// <summary>
    /// Specific workflow capabilities granted beyond the base permission level.
    /// <value>JSON array of capability strings defining granular operation permissions</value>
    /// </summary>
    /// <remarks>
    /// Enables fine-grained permission control through capability-based access,
    /// supporting custom workflows and specialized operational requirements.
    /// Examples: ["create_workflows", "execute_workflows", "manage_versions", "view_analytics"]
    /// </remarks>
    [StringLength(500)]
    public string? Capabilities { get; set; }
    
    /// <summary>
    /// Flag indicating whether the principal can delegate their permissions to other users.
    /// <value>Boolean controlling delegation rights for permission sharing and team management</value>
    /// </summary>
    /// <remarks>
    /// Supports team leadership scenarios and managed delegation while maintaining
    /// security governance and audit trails for all permission transfers.
    /// </remarks>
    public bool CanDelegate { get; set; } = false;
    
    /// <summary>
    /// Maximum financial cost limit for workflow executions under this permission.
    /// <value>Decimal representing the maximum allowed cost per execution or total cost limit</value>
    /// </summary>
    /// <remarks>
    /// Provides budget control and prevents cost overruns while enabling
    /// flexible resource allocation and usage-based access management.
    /// </remarks>
    public decimal? CostLimit { get; set; }
    
    /// <summary>
    /// Maximum execution duration allowed for workflows under this permission.
    /// <value>Integer representing maximum execution time in seconds</value>
    /// </summary>
    /// <remarks>
    /// Controls resource consumption and prevents long-running processes
    /// while supporting operational requirements and system performance management.
    /// </remarks>
    public int? TimeLimit { get; set; }
    
    /// <summary>
    /// Maximum number of workflow executions allowed within the quota period.
    /// <value>Integer representing the execution count limit for usage governance</value>
    /// </summary>
    /// <remarks>
    /// Implements usage-based access control and prevents system abuse
    /// while supporting fair resource allocation and capacity management.
    /// </remarks>
    public int? ExecutionQuota { get; set; }
    
    /// <summary>
    /// Time period in hours for execution quota reset and usage calculation.
    /// <value>Integer representing the quota reset period in hours</value>
    /// </summary>
    /// <remarks>
    /// Defines the sliding window for quota enforcement and usage tracking,
    /// supporting flexible usage patterns and operational requirements.
    /// </remarks>
    public int QuotaPeriodHours { get; set; } = 24;
    
    /// <summary>
    /// Execution contexts where this permission is valid and can be exercised.
    /// <value>JSON array of context strings defining environment-specific access restrictions</value>
    /// </summary>
    /// <remarks>
    /// Enables environment-specific access control for development, staging,
    /// and production environments with appropriate security boundaries.
    /// Examples: ["development", "staging", "production"]
    /// </remarks>
    [StringLength(200)]
    public string? AllowedContexts { get; set; }
    
    /// <summary>
    /// Business justification and rationale for granting this permission.
    /// <value>String documenting the reason and business need for the permission grant</value>
    /// </summary>
    /// <remarks>
    /// Supports compliance requirements, audit trails, and governance
    /// by documenting the business justification for access grants.
    /// </remarks>
    [StringLength(500)]
    public string? GrantReason { get; set; }
    
    /// <summary>
    /// Identifier of the authority who approved this permission grant.
    /// <value>String identifier of the approving user, manager, or automated system</value>
    /// </summary>
    /// <remarks>
    /// Establishes accountability and approval chains for permission grants,
    /// supporting enterprise governance and compliance requirements.
    /// </remarks>
    [StringLength(100)]
    public string? ApprovedBy { get; set; }
    
    /// <summary>
    /// Timestamp when this permission was formally approved and became effective.
    /// <value>DateTime in UTC recording the approval moment for audit and compliance tracking</value>
    /// </summary>
    /// <remarks>
    /// Provides audit trail for permission lifecycle and supports compliance
    /// reporting and governance review processes for access management.
    /// </remarks>
    public DateTime? ApprovedAt { get; set; }
}
