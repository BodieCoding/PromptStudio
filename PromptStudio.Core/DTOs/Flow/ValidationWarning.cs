namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// A validation warning in a flow
/// </summary>
public class ValidationWarning
{
    /// <summary>
    /// Node ID where the warning occurred (if applicable)
    /// </summary>
    public string? NodeId { get; set; }

    /// <summary>
    /// Human-readable warning message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Warning type for categorization (performance, best_practice, optimization)
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
