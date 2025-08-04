namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents retry policy configuration for workflow execution.
/// </summary>
public class RetryPolicy
{
    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;

    /// <summary>
    /// Gets or sets the initial delay between retry attempts.
    /// </summary>
    public TimeSpan InitialDelay { get; set; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Gets or sets the maximum delay between retry attempts.
    /// </summary>
    public TimeSpan MaxDelay { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Gets or sets the backoff multiplier for exponential backoff.
    /// </summary>
    public double BackoffMultiplier { get; set; } = 2.0;

    /// <summary>
    /// Gets or sets whether to use exponential backoff.
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;

    /// <summary>
    /// Gets or sets the exceptions that should trigger retries.
    /// </summary>
    public List<string>? RetriableExceptions { get; set; }
}

/// <summary>
/// Represents concurrency settings for workflow execution.
/// </summary>
public class ConcurrencySettings
{
    /// <summary>
    /// Gets or sets the maximum degree of parallelism.
    /// </summary>
    public int MaxDegreeOfParallelism { get; set; } = Environment.ProcessorCount;

    /// <summary>
    /// Gets or sets whether to use bounded parallelism.
    /// </summary>
    public bool UseBoundedParallelism { get; set; } = true;

    /// <summary>
    /// Gets or sets the task scheduler type to use.
    /// </summary>
    public string? TaskSchedulerType { get; set; }

    /// <summary>
    /// Gets or sets the thread pool settings.
    /// </summary>
    public Dictionary<string, object>? ThreadPoolSettings { get; set; }
}

/// <summary>
/// Represents resource consumption metrics.
/// </summary>
public class ResourceConsumption
{
    /// <summary>
    /// Gets or sets the CPU usage percentage.
    /// </summary>
    public double CpuUsagePercentage { get; set; }

    /// <summary>
    /// Gets or sets the memory usage in bytes.
    /// </summary>
    public long MemoryUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the network usage in bytes.
    /// </summary>
    public long NetworkUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the storage usage in bytes.
    /// </summary>
    public long StorageUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when metrics were captured.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents retry information for workflow execution.
/// </summary>
public class ExecutionRetryInfo
{
    /// <summary>
    /// Gets or sets the total number of retry attempts made.
    /// </summary>
    public int TotalRetryAttempts { get; set; }

    /// <summary>
    /// Gets or sets the retry attempts for individual nodes.
    /// </summary>
    public List<NodeRetryInfo>? NodeRetryAttempts { get; set; }

    /// <summary>
    /// Gets or sets the final retry outcome.
    /// </summary>
    public RetryOutcome FinalOutcome { get; set; }

    /// <summary>
    /// Gets or sets the total time spent on retries.
    /// </summary>
    public TimeSpan TotalRetryTime { get; set; }

    /// <summary>
    /// Gets or sets the retry policy that was applied.
    /// </summary>
    public RetryPolicy? AppliedRetryPolicy { get; set; }
}

/// <summary>
/// Represents retry information for a specific node.
/// </summary>
public class NodeRetryInfo
{
    /// <summary>
    /// Gets or sets the node identifier.
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of retry attempts for this node.
    /// </summary>
    public int RetryAttempts { get; set; }

    /// <summary>
    /// Gets or sets the timestamps of retry attempts.
    /// </summary>
    public List<DateTime>? RetryTimestamps { get; set; }

    /// <summary>
    /// Gets or sets the errors that triggered retries.
    /// </summary>
    public List<ExecutionError>? RetryTriggerErrors { get; set; }

    /// <summary>
    /// Gets or sets the final outcome after retries.
    /// </summary>
    public RetryOutcome Outcome { get; set; }
}

/// <summary>
/// Enumeration of retry outcomes.
/// </summary>
public enum RetryOutcome
{
    /// <summary>
    /// Retry was successful and execution completed.
    /// </summary>
    Success = 0,

    /// <summary>
    /// Retry failed and maximum attempts were exceeded.
    /// </summary>
    MaxAttemptsExceeded = 1,

    /// <summary>
    /// Retry was cancelled by user or system.
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Retry timed out before completion.
    /// </summary>
    TimedOut = 3,

    /// <summary>
    /// Non-retriable error occurred during retry.
    /// </summary>
    NonRetriableError = 4
}

/// <summary>
/// Represents cancellation information for workflow execution.
/// </summary>
public class ExecutionCancellationInfo
{
    /// <summary>
    /// Gets or sets when the cancellation was requested.
    /// </summary>
    public DateTime CancellationRequestedAt { get; set; }

    /// <summary>
    /// Gets or sets when the cancellation was completed.
    /// </summary>
    public DateTime? CancellationCompletedAt { get; set; }

    /// <summary>
    /// Gets or sets who requested the cancellation.
    /// </summary>
    public string RequestedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the reason for cancellation.
    /// </summary>
    public string? CancellationReason { get; set; }

    /// <summary>
    /// Gets or sets whether the cancellation was graceful.
    /// </summary>
    public bool IsGracefulCancellation { get; set; } = true;

    /// <summary>
    /// Gets or sets the cleanup actions performed during cancellation.
    /// </summary>
    public List<string>? CleanupActions { get; set; }
}

/// <summary>
/// Represents execution environment information.
/// </summary>
public class ExecutionEnvironmentInfo
{
    /// <summary>
    /// Gets or sets the machine name where execution occurred.
    /// </summary>
    public string? MachineName { get; set; }

    /// <summary>
    /// Gets or sets the operating system information.
    /// </summary>
    public string? OperatingSystem { get; set; }

    /// <summary>
    /// Gets or sets the runtime version.
    /// </summary>
    public string? RuntimeVersion { get; set; }

    /// <summary>
    /// Gets or sets the application version.
    /// </summary>
    public string? ApplicationVersion { get; set; }

    /// <summary>
    /// Gets or sets the execution region or location.
    /// </summary>
    public string? ExecutionRegion { get; set; }

    /// <summary>
    /// Gets or sets additional environment variables.
    /// </summary>
    public Dictionary<string, string>? EnvironmentVariables { get; set; }

    /// <summary>
    /// Gets or sets the infrastructure details.
    /// </summary>
    public Dictionary<string, object>? InfrastructureDetails { get; set; }
}

/// <summary>
/// Represents audit trail information for compliance.
/// </summary>
public class AuditTrailInfo
{
    /// <summary>
    /// Gets or sets the audit events that occurred during execution.
    /// </summary>
    public List<AuditEvent>? AuditEvents { get; set; }

    /// <summary>
    /// Gets or sets the compliance information.
    /// </summary>
    public Dictionary<string, object>? ComplianceInfo { get; set; }

    /// <summary>
    /// Gets or sets the security context information.
    /// </summary>
    public Dictionary<string, object>? SecurityContext { get; set; }
}

/// <summary>
/// Represents an individual audit event.
/// </summary>
public class AuditEvent
{
    /// <summary>
    /// Gets or sets the event timestamp.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the event type.
    /// </summary>
    public string EventType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the event description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user or system that triggered the event.
    /// </summary>
    public string TriggeredBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional event details.
    /// </summary>
    public Dictionary<string, object>? EventDetails { get; set; }
}

/// <summary>
/// Represents cost information for workflow execution.
/// </summary>
public class ExecutionCostInfo
{
    /// <summary>
    /// Gets or sets the total estimated cost.
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Gets or sets the currency for cost calculations.
    /// </summary>
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Gets or sets the cost breakdown by category.
    /// </summary>
    public Dictionary<string, decimal>? CostBreakdown { get; set; }

    /// <summary>
    /// Gets or sets the resource usage costs.
    /// </summary>
    public Dictionary<string, decimal>? ResourceCosts { get; set; }

    /// <summary>
    /// Gets or sets API usage costs.
    /// </summary>
    public decimal? ApiUsageCost { get; set; }

    /// <summary>
    /// Gets or sets compute costs.
    /// </summary>
    public decimal? ComputeCost { get; set; }

    /// <summary>
    /// Gets or sets storage costs.
    /// </summary>
    public decimal? StorageCost { get; set; }
}

/// <summary>
/// Represents quality metrics for workflow execution.
/// </summary>
public class ExecutionQualityMetrics
{
    /// <summary>
    /// Gets or sets the overall quality score.
    /// </summary>
    public double OverallQualityScore { get; set; }

    /// <summary>
    /// Gets or sets the accuracy metrics.
    /// </summary>
    public Dictionary<string, double>? AccuracyMetrics { get; set; }

    /// <summary>
    /// Gets or sets the performance quality indicators.
    /// </summary>
    public Dictionary<string, double>? PerformanceIndicators { get; set; }

    /// <summary>
    /// Gets or sets the reliability metrics.
    /// </summary>
    public Dictionary<string, double>? ReliabilityMetrics { get; set; }

    /// <summary>
    /// Gets or sets the compliance score.
    /// </summary>
    public double? ComplianceScore { get; set; }

    /// <summary>
    /// Gets or sets quality assessment details.
    /// </summary>
    public Dictionary<string, object>? QualityAssessment { get; set; }
}
