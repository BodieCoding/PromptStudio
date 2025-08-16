namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of template variable validation for a complete variable set
/// </summary>
public class TemplateVariableValidationResult
{
    /// <summary>
    /// Gets or sets whether the overall validation was successful
    /// </summary>
    public bool IsValid { get; set; } = true;

    /// <summary>
    /// Gets or sets the template ID that was validated against
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets validation results for individual variables
    /// </summary>
    public Dictionary<string, VariableValidationResult> VariableResults { get; set; } = [];

    /// <summary>
    /// Gets or sets any missing required variables
    /// </summary>
    public List<string> MissingRequiredVariables { get; set; } = [];

    /// <summary>
    /// Gets or sets any unexpected variables not defined in the template
    /// </summary>
    public List<string> UnexpectedVariables { get; set; } = [];

    /// <summary>
    /// Gets or sets cross-variable validation results
    /// </summary>
    public List<CrossVariableValidationResult> CrossVariableResults { get; set; } = [];

    /// <summary>
    /// Gets or sets general validation messages
    /// </summary>
    public List<string> Messages { get; set; } = [];

    /// <summary>
    /// Gets or sets validation warnings
    /// </summary>
    public List<string> Warnings { get; set; } = [];

    /// <summary>
    /// Gets or sets validation errors
    /// </summary>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// Gets or sets the validation timestamp
    /// </summary>
    public DateTime ValidatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets performance metrics for the validation operation
    /// </summary>
    public ValidationPerformanceMetrics Performance { get; set; } = new();
}
