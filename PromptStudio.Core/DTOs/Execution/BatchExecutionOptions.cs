using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Options for configuring batch execution behavior
/// </summary>
public class BatchExecutionOptions
{
    /// <summary>
    /// Maximum number of concurrent executions
    /// </summary>
    public int MaxConcurrency { get; set; } = 5;

    /// <summary>
    /// Timeout for each individual execution
    /// </summary>
    public TimeSpan ExecutionTimeout { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Whether to continue execution if individual items fail
    /// </summary>
    public bool ContinueOnError { get; set; } = true;

    /// <summary>
    /// Whether to validate variable completeness before execution
    /// </summary>
    public bool ValidateVariables { get; set; } = true;

    /// <summary>
    /// Delay between batch executions to avoid rate limiting
    /// </summary>
    public TimeSpan? DelayBetweenExecutions { get; set; }

    /// <summary>
    /// Whether to collect detailed performance metrics
    /// </summary>
    public bool CollectDetailedMetrics { get; set; } = true;

    /// <summary>
    /// Custom metadata to attach to all executions in the batch
    /// </summary>
    public Dictionary<string, object>? BatchMetadata { get; set; }

    /// <summary>
    /// Priority level for the batch execution
    /// </summary>
    public ExecutionPriority Priority { get; set; } = ExecutionPriority.Normal;

    /// <summary>
    /// Tags to apply to all executions in the batch
    /// </summary>
    public List<string>? Tags { get; set; }
}

/// <summary>
/// Execution priority levels
/// </summary>
public enum ExecutionPriority
{
    Low = 0,
    Normal = 1,
    High = 2,
    Critical = 3
}
