namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Information about a discovered variable
/// </summary>
public class DiscoveredVariable
{
    /// <summary>
    /// Gets or sets the variable name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the inferred or specified type
    /// </summary>
    public string? InferredType { get; set; }

    /// <summary>
    /// Gets or sets the positions where this variable appears in the template
    /// </summary>
    public List<VariablePosition> Positions { get; set; } = [];

    /// <summary>
    /// Gets or sets whether this variable appears to be required
    /// </summary>
    public bool IsRequired { get; set; } = true;

    /// <summary>
    /// Gets or sets any suggested default value
    /// </summary>
    public string? SuggestedDefault { get; set; }

    /// <summary>
    /// Gets or sets additional metadata about the variable
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = [];
}
