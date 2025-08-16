namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of cross-variable validation (e.g., dependencies between variables)
/// </summary>
public class CrossVariableValidationResult
{
    /// <summary>
    /// Gets or sets whether this cross-variable validation passed
    /// </summary>
    public bool IsValid { get; set; } = true;

    /// <summary>
    /// Gets or sets the rule that was validated
    /// </summary>
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the variables involved in this validation
    /// </summary>
    public List<string> InvolvedVariables { get; set; } = [];

    /// <summary>
    /// Gets or sets the validation message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level
    /// </summary>
    public string Severity { get; set; } = "Error";
}
