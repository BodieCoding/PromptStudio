namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Represents a target for sharing templates or libraries
/// This class is used to specify who can access shared content, such as users, teams, or groups.
/// It includes properties for the target ID, type, and an optional message.
/// </summary>
public class ShareTarget
{
    /// <summary>
    /// ID of the user or team to share with
    /// </summary>
    public string TargetId { get; set; } = string.Empty;

    /// <summary>
    /// Type of target (User, Team, Group)
    /// </summary>
    public string TargetType { get; set; } = "User"; // Default to User

    /// <summary>
    /// Optional message for the share notification
    /// </summary>
    public string? Message { get; set; }
}
