using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Options for model validation.
/// </summary>
public class ModelValidationOptions
{
    /// <summary>
    /// Gets or sets whether to validate connectivity.
    /// </summary>
    public bool ValidateConnectivity { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to validate capabilities.
    /// </summary>
    public bool ValidateCapabilities { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to validate performance.
    /// </summary>
    public bool ValidatePerformance { get; set; } = false;

    /// <summary>
    /// Gets or sets timeout for validation operations.
    /// </summary>
    public TimeSpan? ValidationTimeout { get; set; }

    /// <summary>
    /// Gets or sets required capabilities to validate.
    /// </summary>
    public List<string>? RequiredCapabilities { get; set; }
}

/// <summary>
/// Result of model validation operation.
/// </summary>
public class ModelValidationResult : OperationResult
{
    /// <summary>
    /// Gets or sets whether the model is valid and ready.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets connectivity validation result.
    /// </summary>
    public ValidationCheck? ConnectivityCheck { get; set; }

    /// <summary>
    /// Gets or sets capabilities validation result.
    /// </summary>
    public ValidationCheck? CapabilitiesCheck { get; set; }

    /// <summary>
    /// Gets or sets performance validation result.
    /// </summary>
    public ValidationCheck? PerformanceCheck { get; set; }

    /// <summary>
    /// Gets or sets detailed validation results.
    /// </summary>
    public Dictionary<string, ValidationCheck>? DetailedResults { get; set; }

    /// <summary>
    /// Gets or sets validation timestamp.
    /// </summary>
    public DateTime ValidatedAt { get; set; }
}

/// <summary>
/// Individual validation check result.
/// </summary>
public class ValidationCheck
{
    /// <summary>
    /// Gets or sets whether the check passed.
    /// </summary>
    public bool Passed { get; set; }

    /// <summary>
    /// Gets or sets the check description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets error message if check failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets check duration.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Gets or sets additional check details.
    /// </summary>
    public Dictionary<string, object>? Details { get; set; }
}

/// <summary>
/// Options for model execution.
/// </summary>
public class ModelExecutionOptions
{
    /// <summary>
    /// Gets or sets the timeout for execution.
    /// </summary>
    public TimeSpan? ExecutionTimeout { get; set; }

    /// <summary>
    /// Gets or sets the preferred provider ID.
    /// </summary>
    public Guid? PreferredProviderId { get; set; }

    /// <summary>
    /// Gets or sets whether to enable caching.
    /// </summary>
    public bool EnableCaching { get; set; } = false;

    /// <summary>
    /// Gets or sets retry configuration.
    /// </summary>
    public RetryOptions? RetryOptions { get; set; }

    /// <summary>
    /// Gets or sets execution priority.
    /// </summary>
    public ExecutionPriority Priority { get; set; } = ExecutionPriority.Normal;

    /// <summary>
    /// Gets or sets custom execution properties.
    /// </summary>
    public Dictionary<string, object>? CustomProperties { get; set; }
}

/// <summary>
/// Retry configuration for model execution.
/// </summary>
public class RetryOptions
{
    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Gets or sets the base delay between retries.
    /// </summary>
    public TimeSpan BaseDelay { get; set; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Gets or sets whether to use exponential backoff.
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;

    /// <summary>
    /// Gets or sets error conditions that should trigger retry.
    /// </summary>
    public List<string>? RetryConditions { get; set; }
}

/// <summary>
/// Execution priority levels.
/// </summary>
public enum ExecutionPriority
{
    /// <summary>Low priority execution</summary>
    Low = 0,
    /// <summary>Normal priority execution</summary>
    Normal = 1,
    /// <summary>High priority execution</summary>
    High = 2,
    /// <summary>Critical priority execution</summary>
    Critical = 3
}
