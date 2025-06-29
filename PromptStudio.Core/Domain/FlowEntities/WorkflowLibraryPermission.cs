using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Granular access control for workflow libraries
/// Supports enterprise-grade workflow security and collaboration
/// </summary>
public class WorkflowLibraryPermission : AuditableEntity
{
    /// <summary>
    /// Reference to the workflow library
    /// </summary>
    public Guid WorkflowLibraryId { get; set; }
    public virtual WorkflowLibrary WorkflowLibrary { get; set; } = null!;
    
    /// <summary>
    /// Principal (user, role, or service account) being granted permission
    /// </summary>
    [Required]
    [StringLength(100)]
    public string PrincipalId { get; set; } = string.Empty;
    
    /// <summary>
    /// Type of principal (User, Role, Group, ServiceAccount)
    /// </summary>
    public PrincipalType PrincipalType { get; set; } = PrincipalType.User;
    
    /// <summary>
    /// Level of access granted
    /// </summary>
    public PermissionLevel Permission { get; set; } = PermissionLevel.Read;
    
    /// <summary>
    /// Optional permission expiration for temporary access
    /// </summary>
    public DateTime? ExpiresAt { get; set; }
    
    /// <summary>
    /// Specific workflow capabilities granted (JSON array)
    /// Examples: ["create_workflows", "execute_workflows", "manage_versions", "view_analytics"]
    /// </summary>
    [StringLength(500)]
    public string? Capabilities { get; set; }
    
    /// <summary>
    /// Whether this permission allows delegation to others
    /// </summary>
    public bool CanDelegate { get; set; } = false;
    
    /// <summary>
    /// Maximum execution cost limit for workflows in this library
    /// </summary>
    public decimal? CostLimit { get; set; }
    
    /// <summary>
    /// Maximum execution time limit (seconds)
    /// </summary>
    public int? TimeLimit { get; set; }
    
    /// <summary>
    /// Workflow execution quota (executions per time period)
    /// </summary>
    public int? ExecutionQuota { get; set; }
    
    /// <summary>
    /// Quota reset period (hours)
    /// </summary>
    public int QuotaPeriodHours { get; set; } = 24;
    
    /// <summary>
    /// Allowed execution contexts (JSON array)
    /// Examples: ["development", "staging", "production"]
    /// </summary>
    [StringLength(200)]
    public string? AllowedContexts { get; set; }
    
    /// <summary>
    /// Reason for granting this permission
    /// </summary>
    [StringLength(500)]
    public string? GrantReason { get; set; }
    
    /// <summary>
    /// Who approved this permission grant
    /// </summary>
    [StringLength(100)]
    public string? ApprovedBy { get; set; }
    
    /// <summary>
    /// When this permission was approved
    /// </summary>
    public DateTime? ApprovedAt { get; set; }
}
