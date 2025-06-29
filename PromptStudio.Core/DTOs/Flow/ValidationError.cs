namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// A validation error in a flow
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Node ID where the error occurred (if applicable)
    /// </summary>
    public string? NodeId { get; set; }

    /// <summary>
    /// Human-readable error message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Error type for categorization (connection, data, logic, syntax)
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
