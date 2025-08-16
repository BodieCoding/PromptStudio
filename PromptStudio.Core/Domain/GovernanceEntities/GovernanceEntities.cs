using System.ComponentModel.DataAnnotations;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a governance policy for the platform
/// </summary>
public class GovernancePolicy : AuditableEntity
{
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
    /// Policy version
    /// </summary>
    public string Version { get; set; } = "1.0.0";

    /// <summary>
    /// Policy metadata
    /// </summary>
    public string? Metadata { get; set; }
}
