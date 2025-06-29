namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Node validation result
/// </summary>
public class NodeValidationResult
{
    /// <summary>
    /// Node ID
    /// </summary>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Node name
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Whether the node is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Node-specific errors
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Node-specific warnings
    /// </summary>
    public List<string> Warnings { get; set; } = new();
}
