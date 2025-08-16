namespace PromptStudio.Core.Domain.VariableEntities;

/// <summary>
/// Represents the result of variable value validation.
/// </summary>
public class VariableValidationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the validation passed.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the error message if validation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets any warnings generated during validation.
    /// </summary>
    public string? Warning { get; set; }
}
