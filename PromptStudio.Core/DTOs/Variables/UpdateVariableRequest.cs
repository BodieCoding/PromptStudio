using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Request for updating an existing prompt variable
/// </summary>
public class UpdateVariableRequest
{
    /// <summary>
    /// Gets or sets the updated variable name
    /// </summary>
    [StringLength(100)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the updated variable description
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the updated variable type
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the updated default value for the variable
    /// </summary>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets whether the variable is required
    /// </summary>
    public bool? IsRequired { get; set; }

    /// <summary>
    /// Gets or sets the updated validation constraints for the variable
    /// </summary>
    public Dictionary<string, object>? ValidationConstraints { get; set; }

    /// <summary>
    /// Gets or sets additional metadata for the variable
    /// </summary>
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Gets or sets updated tags for categorizing the variable
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the version number for optimistic concurrency
    /// </summary>
    public int? Version { get; set; }
}
