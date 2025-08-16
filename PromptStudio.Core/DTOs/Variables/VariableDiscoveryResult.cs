namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of variable discovery and extraction from template content
/// </summary>
public class VariableDiscoveryResult
{
    /// <summary>
    /// Gets or sets the discovered variable names
    /// </summary>
    public List<string> VariableNames { get; set; } = [];

    /// <summary>
    /// Gets or sets detailed information about discovered variables
    /// </summary>
    public List<DiscoveredVariable> Variables { get; set; } = [];

    /// <summary>
    /// Gets or sets any parsing warnings or issues
    /// </summary>
    public List<string> Warnings { get; set; } = [];

    /// <summary>
    /// Gets or sets any parsing errors
    /// </summary>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// Gets or sets whether the discovery was successful
    /// </summary>
    public bool IsSuccess { get; set; } = true;

    /// <summary>
    /// Gets or sets the total number of variable references found
    /// </summary>
    public int TotalVariableReferences { get; set; }

    /// <summary>
    /// Gets or sets the unique variable count
    /// </summary>
    public int UniqueVariableCount { get; set; }

    /// <summary>
    /// Gets or sets the discovery timestamp
    /// </summary>
    public DateTime DiscoveredAt { get; set; } = DateTime.UtcNow;
}
