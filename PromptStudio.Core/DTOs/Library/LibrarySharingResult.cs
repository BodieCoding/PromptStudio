namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Result of sharing a library with other users or teams
/// This class encapsulates the outcome of a sharing operation, including success status, 
/// shared targets, failed targets, and an optional message.
/// It is used to provide feedback on the sharing process, indicating which targets were successfully shared and
/// </summary>
public class LibrarySharingResult
{
    /// <summary>
    /// Indicates if the sharing was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// List of targets that were successfully shared with
    /// </summary>
    public List<ShareTarget> SharedTargets { get; set; } = new List<ShareTarget>();

    /// <summary>
    /// List of targets that failed to share
    /// </summary>
    public List<ShareTarget> FailedTargets { get; set; } = new List<ShareTarget>();

    /// <summary>
    /// List of errors encountered during sharing
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// List of share target results
    /// </summary>
    public List<ShareTargetResult> Results { get; set; } = new();

    /// <summary>
    /// Number of successful shares
    /// </summary>
    public int SuccessfulShares { get; set; }

    /// <summary>
    /// Number of failed shares
    /// </summary>
    public int FailedShares { get; set; }

    /// <summary>
    /// Optional message with details about the sharing operation
    /// </summary>
    public string? Message { get; set; }
}

/// <summary>
/// Share target (user or team)
/// </summary>
public class ShareTarget
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty; // "user" or "team"
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

/// <summary>
/// Share target result
/// </summary>
public class ShareTargetResult
{
    public ShareTarget Target { get; set; } = new();
    public bool Success { get; set; }
    public string? Error { get; set; }
    public List<string> GrantedPermissions { get; set; } = new();
}
