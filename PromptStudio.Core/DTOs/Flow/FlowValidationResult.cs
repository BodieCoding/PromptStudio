namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Result of validating a prompt flow
/// </summary>
public class FlowValidationResult
{
    /// <summary>
    /// Whether the flow is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// List of validation errors
    /// </summary>
    public List<ValidationError> Errors { get; set; } = new();

    /// <summary>
    /// List of validation warnings
    /// </summary>
    public List<ValidationWarning> Warnings { get; set; } = new();
}
