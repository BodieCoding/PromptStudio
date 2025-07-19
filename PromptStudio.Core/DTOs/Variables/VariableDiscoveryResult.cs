namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of variable discovery and extraction from template content
/// </summary>
public class VariableDiscoveryResult
{
    /// <summary>
    /// Gets or sets the discovered variable names
    /// </summary>
    public List<string> VariableNames { get; set; } = new();

    /// <summary>
    /// Gets or sets detailed information about discovered variables
    /// </summary>
    public List<DiscoveredVariable> Variables { get; set; } = new();

    /// <summary>
    /// Gets or sets any parsing warnings or issues
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Gets or sets any parsing errors
    /// </summary>
    public List<string> Errors { get; set; } = new();

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
    public List<VariablePosition> Positions { get; set; } = new();

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
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Position information for a variable reference
/// </summary>
public class VariablePosition
{
    /// <summary>
    /// Gets or sets the line number (1-based)
    /// </summary>
    public int Line { get; set; }

    /// <summary>
    /// Gets or sets the column number (1-based)
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Gets or sets the character index in the content
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the length of the variable reference
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets the full text of the variable reference
    /// </summary>
    public string ReferenceText { get; set; } = string.Empty;
}
