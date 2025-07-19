using System.ComponentModel.DataAnnotations;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Domain.GovernanceEntities;

/// <summary>
/// Represents a governance policy for the platform
/// </summary>
public class GovernancePolicy : AuditableEntity
{
    /// <summary>
    /// Unique identifier for the policy
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Policy name
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Policy description
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Policy category
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Policy rules in JSON format
    /// </summary>
    [Required]
    public string Rules { get; set; } = string.Empty;

    /// <summary>
    /// Policy severity level
    /// </summary>
    public PolicySeverity Severity { get; set; } = PolicySeverity.Medium;

    /// <summary>
    /// Whether the policy is active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Policy version
    /// </summary>
    public string Version { get; set; } = "1.0.0";

    /// <summary>
    /// Policy metadata
    /// </summary>
    public string? Metadata { get; set; }
}

/// <summary>
/// Policy severity levels
/// </summary>
public enum PolicySeverity
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}

/// <summary>
/// Represents a compliance audit record
/// </summary>
public class ComplianceAudit : AuditableEntity
{
    /// <summary>
    /// Unique identifier for the audit
    /// </summary>
    public Guid Id { get; set; }

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

/// <summary>
/// Audit status enumeration
/// </summary>
public enum AuditStatus
{
    Planned = 0,
    InProgress = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4
}
