using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a compliance audit record
/// </summary>
public class ComplianceAudit : AuditableEntity
{
    /// <summary>
    /// Audit name
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Audit type
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string AuditType { get; set; } = string.Empty;

    /// <summary>
    /// Audit scope
    /// </summary>
    public string? Scope { get; set; }

    /// <summary>
    /// Audit findings in JSON format
    /// </summary>
    public string? Findings { get; set; }

    /// <summary>
    /// Audit status
    /// </summary>
    public AuditStatus Status { get; set; } = AuditStatus.InProgress;

    /// <summary>
    /// Audit completion date
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Audit metadata
    /// </summary>
    public string? Metadata { get; set; }
}
