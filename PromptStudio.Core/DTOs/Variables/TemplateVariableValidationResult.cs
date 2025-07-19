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
    public Dictionary<string, VariableValidationResult> VariableResults { get; set; } = new();

    /// <summary>
    /// Gets or sets any missing required variables
    /// </summary>
    public List<string> MissingRequiredVariables { get; set; } = new();

    /// <summary>
    /// Gets or sets any unexpected variables not defined in the template
    /// </summary>
    public List<string> UnexpectedVariables { get; set; } = new();

    /// <summary>
    /// Gets or sets cross-variable validation results
    /// </summary>
    public List<CrossVariableValidationResult> CrossVariableResults { get; set; } = new();

    /// <summary>
    /// Gets or sets general validation messages
    /// </summary>
    public List<string> Messages { get; set; } = new();

    /// <summary>
    /// Gets or sets validation warnings
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Gets or sets validation errors
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Gets or sets the validation timestamp
    /// </summary>
    public DateTime ValidatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets performance metrics for the validation operation
    /// </summary>
    public ValidationPerformanceMetrics Performance { get; set; } = new();
}

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
    public List<string> InvolvedVariables { get; set; } = new();

    /// <summary>
    /// Gets or sets the validation message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level
    /// </summary>
    public string Severity { get; set; } = "Error";
}

/// <summary>
/// Performance metrics for validation operations
/// </summary>
public class ValidationPerformanceMetrics
{
    /// <summary>
    /// Gets or sets the total validation duration in milliseconds
    /// </summary>
    public long DurationMs { get; set; }

    /// <summary>
    /// Gets or sets the number of variables validated
    /// </summary>
    public int VariablesValidated { get; set; }

    /// <summary>
    /// Gets or sets the number of validation rules applied
    /// </summary>
    public int RulesApplied { get; set; }

    /// <summary>
    /// Gets or sets memory usage during validation in bytes
    /// </summary>
    public long MemoryUsageBytes { get; set; }
}
