namespace PromptStudio.Core.DTOs.Lab;

/// <summary>
/// Lab validation result
/// </summary>
public class LabValidationResult
{
    /// <summary>
    /// Whether the lab is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Validation errors (critical issues)
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Validation warnings (non-critical issues)
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Validation recommendations
    /// </summary>
    public List<string> Recommendations { get; set; } = new();

    /// <summary>
    /// Library validation results
    /// </summary>
    public List<LibraryValidationSummary> LibraryValidations { get; set; } = new();

    /// <summary>
    /// Workflow validation results
    /// </summary>
    public List<WorkflowValidationSummary> WorkflowValidations { get; set; } = new();
}
