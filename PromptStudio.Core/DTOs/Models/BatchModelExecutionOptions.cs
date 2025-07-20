using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Options for batch model execution.
/// </summary>
public class BatchModelExecutionOptions
{
    /// <summary>
    /// Gets or sets the maximum number of concurrent executions.
    /// </summary>
    public int MaxConcurrency { get; set; } = 5;

    /// <summary>
    /// Gets or sets whether to continue on individual failures.
    /// </summary>
    public bool ContinueOnError { get; set; } = true;

    /// <summary>
    /// Gets or sets the timeout for the entire batch operation.
    /// </summary>
    public TimeSpan? BatchTimeout { get; set; }

    /// <summary>
    /// Gets or sets execution options to apply to all requests.
    /// </summary>
    public ModelExecutionOptions? DefaultExecutionOptions { get; set; }

    /// <summary>
    /// Gets or sets provider distribution strategy.
    /// </summary>
    public ProviderDistributionStrategy DistributionStrategy { get; set; } = ProviderDistributionStrategy.RoundRobin;

    /// <summary>
    /// Gets or sets custom batch properties.
    /// </summary>
    public Dictionary<string, object>? CustomProperties { get; set; }
}

/// <summary>
/// Provider distribution strategies for batch execution.
/// </summary>
public enum ProviderDistributionStrategy
{
    /// <summary>Distribute requests evenly across providers</summary>
    RoundRobin = 0,
    /// <summary>Use the fastest available provider</summary>
    FastestFirst = 1,
    /// <summary>Use the least loaded provider</summary>
    LeastLoaded = 2,
    /// <summary>Use random provider selection</summary>
    Random = 3,
    /// <summary>Use provider with lowest cost</summary>
    LowestCost = 4
}

/// <summary>
/// Progress information for batch execution.
/// </summary>
public class BatchExecutionProgress
{
    /// <summary>
    /// Gets or sets the total number of requests in the batch.
    /// </summary>
    public int TotalRequests { get; set; }

    /// <summary>
    /// Gets or sets the number of completed requests.
    /// </summary>
    public int CompletedRequests { get; set; }

    /// <summary>
    /// Gets or sets the number of successful requests.
    /// </summary>
    public int SuccessfulRequests { get; set; }

    /// <summary>
    /// Gets or sets the number of failed requests.
    /// </summary>
    public int FailedRequests { get; set; }

    /// <summary>
    /// Gets or sets the current progress percentage.
    /// </summary>
    public double ProgressPercentage => TotalRequests > 0 ? (double)CompletedRequests / TotalRequests * 100 : 0;

    /// <summary>
    /// Gets or sets the elapsed time since batch started.
    /// </summary>
    public TimeSpan ElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets the estimated time remaining.
    /// </summary>
    public TimeSpan? EstimatedTimeRemaining { get; set; }

    /// <summary>
    /// Gets or sets current execution statistics.
    /// </summary>
    public Dictionary<string, object>? Statistics { get; set; }
}

/// <summary>
/// Result of batch model execution operation.
/// </summary>
public class BatchModelExecutionResult : OperationResult
{
    /// <summary>
    /// Gets or sets the individual execution results.
    /// </summary>
    public List<BatchItemResult>? Results { get; set; }

    /// <summary>
    /// Gets or sets the total number of requests processed.
    /// </summary>
    public int TotalRequests { get; set; }

    /// <summary>
    /// Gets or sets the number of successful requests.
    /// </summary>
    public int SuccessfulRequests { get; set; }

    /// <summary>
    /// Gets or sets the number of failed requests.
    /// </summary>
    public int FailedRequests { get; set; }

    /// <summary>
    /// Gets or sets the total execution time.
    /// </summary>
    public TimeSpan TotalExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets provider usage statistics.
    /// </summary>
    public Dictionary<Guid, ProviderUsageStats>? ProviderUsage { get; set; }

    /// <summary>
    /// Gets or sets overall batch statistics.
    /// </summary>
    public BatchExecutionStatistics? BatchStatistics { get; set; }
}

/// <summary>
/// Individual batch item execution result.
/// </summary>
public class BatchItemResult
{
    /// <summary>
    /// Gets or sets the original request index in the batch.
    /// </summary>
    public int RequestIndex { get; set; }

    /// <summary>
    /// Gets or sets whether the execution was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the model response (if successful).
    /// </summary>
    public ModelResponse? Response { get; set; }

    /// <summary>
    /// Gets or sets the error message (if failed).
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the provider used for this request.
    /// </summary>
    public Guid? ProviderId { get; set; }

    /// <summary>
    /// Gets or sets the execution duration.
    /// </summary>
    public TimeSpan ExecutionDuration { get; set; }
}

/// <summary>
/// Provider usage statistics for batch execution.
/// </summary>
public class ProviderUsageStats
{
    /// <summary>
    /// Gets or sets the provider ID.
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Gets or sets the number of requests sent to this provider.
    /// </summary>
    public int RequestsExecuted { get; set; }

    /// <summary>
    /// Gets or sets the number of successful requests.
    /// </summary>
    public int SuccessfulRequests { get; set; }

    /// <summary>
    /// Gets or sets the total execution time for this provider.
    /// </summary>
    public TimeSpan TotalExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the average execution time for this provider.
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }
}

/// <summary>
/// Overall batch execution statistics.
/// </summary>
public class BatchExecutionStatistics
{
    /// <summary>
    /// Gets or sets the average execution time per request.
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the total tokens consumed.
    /// </summary>
    public long TotalTokensConsumed { get; set; }

    /// <summary>
    /// Gets or sets the total cost of the batch.
    /// </summary>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Gets or sets the throughput in requests per second.
    /// </summary>
    public double ThroughputRps { get; set; }

    /// <summary>
    /// Gets or sets additional statistical data.
    /// </summary>
    public Dictionary<string, object>? AdditionalStats { get; set; }
}
