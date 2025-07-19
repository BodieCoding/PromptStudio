using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Request for creating a new prompt variable
/// </summary>
public class CreateVariableRequest
{
    /// <summary>
    /// Gets or sets the variable name
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the variable description
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the variable type
    /// </summary>
    [Required]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the default value for the variable
    /// </summary>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets whether the variable is required
    /// </summary>
    public bool IsRequired { get; set; } = true;

    /// <summary>
    /// Gets or sets the validation constraints for the variable
    /// </summary>
    public Dictionary<string, object> ValidationConstraints { get; set; } = new();

    /// <summary>
    /// Gets or sets the template ID this variable is associated with
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets additional metadata for the variable
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();

    /// <summary>
    /// Gets or sets tags for categorizing the variable
    /// </summary>
    public List<string> Tags { get; set; } = new();
}
