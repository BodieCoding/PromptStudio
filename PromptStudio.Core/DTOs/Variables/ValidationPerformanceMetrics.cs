namespace PromptStudio.Core.DTOs.Variables;

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
