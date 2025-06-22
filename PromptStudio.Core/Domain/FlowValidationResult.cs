namespace PromptStudio.Core.Domain;

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

/// <summary>
/// A validation error in a flow
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Node ID where the error occurred (if applicable)
    /// </summary>
    public string? NodeId { get; set; }

    /// <summary>
    /// Human-readable error message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Error type for categorization (connection, data, logic, syntax)
    /// </summary>
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// A validation warning in a flow
/// </summary>
public class ValidationWarning
{
    /// <summary>
    /// Node ID where the warning occurred (if applicable)
    /// </summary>
    public string? NodeId { get; set; }

    /// <summary>
    /// Human-readable warning message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Warning type for categorization (performance, best_practice, optimization)
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
