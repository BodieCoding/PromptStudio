namespace PromptStudio.Core.Domain;

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
    /// Optional message with details about the sharing operation
    /// </summary>
    public string? Message { get; set; }
}
