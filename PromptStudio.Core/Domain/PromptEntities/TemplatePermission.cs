using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Granular access control for prompt templates
/// Supports enterprise-grade permissions and security models
/// </summary>
public class TemplatePermission : AuditableEntity
{
    /// <summary>
    /// Reference to the prompt template
    /// </summary>
    public Guid TemplateId { get; set; }
    public virtual PromptTemplate Template { get; set; } = null!;
    
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
    /// Specific capabilities granted (JSON array)
    /// Examples: ["execute", "modify_variables", "view_results", "clone"]
    /// </summary>
    [StringLength(500)]
    public string? Capabilities { get; set; }
    
    /// <summary>
    /// Conditions under which this permission applies (JSON)
    /// Examples: time-based, context-based, or usage-limit restrictions
    /// </summary>
    [StringLength(1000)]
    public string? Conditions { get; set; }
    
    /// <summary>
    /// Whether this permission was granted by inheritance or explicitly
    /// </summary>
    public bool IsInherited { get; set; } = false;
    
    /// <summary>
    /// Source of inherited permission (parent library, lab, etc.)
    /// </summary>
    public Guid? InheritedFromId { get; set; }
    
    /// <summary>
    /// Type of entity permission is inherited from
    /// </summary>
    [StringLength(50)]
    public string? InheritedFromType { get; set; }
}
