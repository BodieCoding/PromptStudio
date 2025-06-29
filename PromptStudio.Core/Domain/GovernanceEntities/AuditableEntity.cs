using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Base entity providing enterprise-grade audit, multi-tenancy, and concurrency support
/// All domain entities should inherit from this for consistent behavior
/// </summary>
public abstract class AuditableEntity
{
    /// <summary>
    /// Unique identifier using Guid for global uniqueness and security
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Organization/Tenant ID for multi-tenant data isolation
    /// Critical for enterprise deployments and compliance
    /// </summary>
    public Guid? OrganizationId { get; set; }
    
    /// <summary>
    /// Audit trail: when the entity was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Audit trail: when the entity was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Soft delete timestamp for enterprise compliance and data recovery
    /// Null means the entity is active
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    
    /// <summary>
    /// Audit trail: who created the entity (user ID or system identifier)
    /// </summary>
    [StringLength(100)]
    public string? CreatedBy { get; set; }
    
    /// <summary>
    /// Audit trail: who last updated the entity (user ID or system identifier)
    /// </summary>
    [StringLength(100)]
    public string? UpdatedBy { get; set; }
    
    /// <summary>
    /// Row version for optimistic concurrency control
    /// Essential for collaborative editing scenarios
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }
    
    /// <summary>
    /// Data classification level for security and compliance
    /// </summary>
    public DataClassification Classification { get; set; } = DataClassification.Internal;
    
    /// <summary>
    /// Indicates if this entity is currently active (not soft deleted)
    /// </summary>
    public bool IsActive => DeletedAt == null;
}
