using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Lab;

/// <summary>
/// Lab member information DTO
/// </summary>
public class LabMemberDto
{
    /// <summary>
    /// Lab ID
    /// </summary>
    public Guid LabId { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Member role in the lab
    /// </summary>
    public LabMemberRole Role { get; set; }

    /// <summary>
    /// When the member was added to the lab
    /// </summary>
    public DateTime JoinedAt { get; set; }

    /// <summary>
    /// Who added this member to the lab
    /// </summary>
    public string? AddedBy { get; set; }

    /// <summary>
    /// When the member's role was last updated
    /// </summary>
    public DateTime? RoleUpdatedAt { get; set; }

    /// <summary>
    /// Who last updated the member's role
    /// </summary>
    public string? RoleUpdatedBy { get; set; }

    /// <summary>
    /// Member's last activity in the lab
    /// </summary>
    public DateTime? LastActivity { get; set; }

    /// <summary>
    /// Whether the member is active
    /// </summary>
    public bool IsActive { get; set; } = true;
}
