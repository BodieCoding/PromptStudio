namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents comprehensive execution options for flow processing, providing enterprise-grade control over execution behavior.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Central configuration DTO for flow execution services, providing fine-grained control over execution behavior,
/// performance tuning, cost management, and monitoring. Used by workflow engines, execution coordinators, and client applications.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Comprehensive execution configuration with optional parameters for performance, monitoring, experimentation, and cost control.
/// Designed for flexible execution control while maintaining backward compatibility through optional properties.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Flow execution configuration and customization</item>
/// <item>A/B testing and experimentation setup</item>
/// <item>Performance tuning and cost optimization</item>
/// <item>Monitoring and observability configuration</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Contains numerous optional configuration properties - only set required options to minimize payload.
/// Dictionary collections should be kept minimal. Consider creating preset configurations for common execution patterns
/// to reduce configuration complexity and improve performance.</para>
/// </remarks>
public class FlowExecutionOptions
{
    /// <summary>
    /// Gets or sets the execution timeout in milliseconds.
    /// </summary>
    /// <value>A positive integer representing the maximum execution time, or null for no timeout.</value>
    public int? Timeout { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to run in debug mode with detailed trace information.
    /// </summary>
    /// <value>True to enable debug mode; otherwise, false.</value>
    public bool Debug { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to only validate the flow without executing it.
    /// </summary>
    /// <value>True to perform validation only; otherwise, false to execute the flow.</value>
    public bool ValidateOnly { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the user identifier for tracking and billing purposes.
    /// </summary>
    /// <value>A string identifying the user executing the flow, or null if not applicable.</value>
    public string? UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the session identifier for grouping related executions.
    /// </summary>
    /// <value>A string identifying the session context, or null if not applicable.</value>
    public string? SessionId { get; set; }
    
    /// <summary>
    /// Gets or sets additional execution context data.
    /// </summary>
    /// <value>A dictionary containing custom context information for the execution.</value>
    public Dictionary<string, object> Context { get; set; } = new();
    
    /// <summary>
    /// Gets or sets the maximum number of nodes to execute concurrently.
    /// </summary>
    /// <value>A positive integer representing the concurrency limit, or null for system default.</value>
    public int? MaxConcurrentNodes { get; set; }
    
    /// <summary>
    /// Gets or sets the number of retry attempts for failed nodes.
    /// </summary>
    /// <value>A non-negative integer representing the retry count.</value>
    public int? RetryAttempts { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the delay between retry attempts.
    /// </summary>
    /// <value>A TimeSpan representing the wait time between retries, or null for immediate retry.</value>
    public TimeSpan? RetryDelay { get; set; }
    
    /// <summary>
    /// Gets or sets the experiment identifier if this execution is part of an A/B test.
    /// </summary>
    /// <value>A GUID identifying the experiment, or null if not part of an experiment.</value>
    public Guid? ExperimentId { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to enable automatic variant selection.
    /// </summary>
    /// <value>True to enable automatic variant selection; otherwise, false.</value>
    public bool EnableVariantSelection { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a specific variant identifier to force (overrides automatic selection).
    /// </summary>
    /// <value>A GUID identifying the variant to force, or null for automatic selection.</value>
    public Guid? ForceVariantId { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to collect detailed execution metrics.
    /// </summary>
    /// <value>True to collect detailed metrics; otherwise, false.</value>
    public bool CollectDetailedMetrics { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to enable distributed tracing.
    /// </summary>
    /// <value>True to enable distributed tracing; otherwise, false.</value>
    public bool EnableTracing { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the list of custom metrics to track during execution.
    /// </summary>
    /// <value>A list of metric names to collect during execution.</value>
    public List<string> CustomMetrics { get; set; } = new();
    
    /// <summary>
    /// Gets or sets the maximum cost threshold for execution (execution will abort if exceeded).
    /// </summary>
    /// <value>A decimal representing the maximum allowed cost, or null for no limit.</value>
    public decimal? MaxCostThreshold { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum token threshold for execution.
    /// </summary>
    /// <value>A positive integer representing the maximum allowed tokens, or null for no limit.</value>
    public int? MaxTokenThreshold { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to estimate costs before execution.
    /// </summary>
    /// <value>True to perform cost estimation; otherwise, false.</value>
    public bool EstimateCosts { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the preferred model provider if multiple options are available.
    /// </summary>
    /// <value>A string identifying the preferred provider, or null for automatic selection.</value>
    public string? PreferredProvider { get; set; }
    
    /// <summary>
    /// Gets or sets the model selection strategy for execution.
    /// </summary>
    /// <value>A ModelSelectionStrategy value determining how models are chosen for execution.</value>
    public ModelSelectionStrategy ModelSelection { get; set; } = ModelSelectionStrategy.Default;
    
    /// <summary>
    /// Gets or sets a value indicating whether to continue execution on non-critical node failures.
    /// </summary>
    /// <value>True to continue on errors; otherwise, false to stop execution.</value>
    public bool ContinueOnError { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to collect partial results even if execution fails.
    /// </summary>
    /// <value>True to collect partial results; otherwise, false.</value>
    public bool CollectPartialResults { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to enable result caching for repeated executions.
    /// </summary>
    /// <value>True to enable caching; otherwise, false.</value>
    public bool EnableCaching { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the cache time-to-live in minutes.
    /// </summary>
    /// <value>A positive integer representing the cache duration, or null for system default.</value>
    public int? CacheTtlMinutes { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to send notifications on completion.
    /// </summary>
    /// <value>True to send completion notifications; otherwise, false.</value>
    public bool NotifyOnCompletion { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether to send notifications on failure.
    /// </summary>
    /// <value>True to send failure notifications; otherwise, false.</value>
    public bool NotifyOnFailure { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the webhook URL for execution notifications.
    /// </summary>
    /// <value>A valid URL for webhook notifications, or null if not configured.</value>
    public string? WebhookUrl { get; set; }
}
