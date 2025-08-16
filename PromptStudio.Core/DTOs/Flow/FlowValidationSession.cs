using System.ComponentModel.DataAnnotations;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Flow validation session for caching validation results
/// </summary>
public class FlowValidationSession : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    [Required]
    [StringLength(20)]
    public string FlowVersion { get; set; } = string.Empty;
    
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Full validation result as JSON
    /// </summary>
    public string ValidationData { get; set; } = "{}";
    
    public DateTime ValidatedAt { get; set; }
    
    [StringLength(100)]
    public string? ValidatedBy { get; set; }
    
    public TimeSpan ValidationDuration { get; set; }
    
    /// <summary>
    /// Hash of flow structure for cache invalidation
    /// </summary>
    [StringLength(64)]
    public string FlowHash { get; set; } = string.Empty;
}
