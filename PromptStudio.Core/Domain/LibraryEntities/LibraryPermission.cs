using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Granular access control for prompt libraries
/// Enables fine-grained security and collaboration controls
/// </summary>
public class LibraryPermission : AuditableEntity
{
    /// <summary>
    /// Reference to the prompt library
    /// </summary>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Tenant ID for multi-tenancy support
    /// Ensures permissions are scoped to the correct tenant
    /// </summary>
    public Guid TenantId { get; set; }
    public virtual PromptLibrary Library { get; set; } = null!;
    
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
    /// Optional expiration date for temporary access
    /// </summary>
    public DateTime? ExpiresAt { get; set; }
    
    /// <summary>
    /// Specific capabilities granted (JSON array)
    /// Examples: ["create_templates", "manage_permissions", "view_analytics"]
    /// </summary>
    [StringLength(500)]
    public string? Capabilities { get; set; }
    
    /// <summary>
    /// Whether this permission can be delegated to others
    /// </summary>
    public bool CanDelegate { get; set; } = false;
    
    /// <summary>
    /// Maximum delegation depth (how many levels this can be re-delegated)
    /// </summary>
    public int MaxDelegationDepth { get; set; } = 0;
    
    /// <summary>
    /// Current delegation depth (0 = original grant)
    /// </summary>
    public int DelegationDepth { get; set; } = 0;
    
    /// <summary>
    /// ID of the permission this was delegated from (if applicable)
    /// </summary>
    public Guid? DelegatedFromId { get; set; }
    public virtual LibraryPermission? DelegatedFrom { get; set; }
    
    /// <summary>
    /// Reason for granting this permission
    /// </summary>
    [StringLength(500)]
    public string? GrantReason { get; set; }
}
