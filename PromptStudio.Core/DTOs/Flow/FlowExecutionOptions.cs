namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Enhanced options for flow execution with enterprise features
/// </summary>
public class FlowExecutionOptions
{
    /// <summary>
    /// Execution timeout in milliseconds
    /// </summary>
    public int? Timeout { get; set; }

    /// <summary>
    /// Whether to run in debug mode with detailed trace
    /// </summary> 
    public bool Debug { get; set; } = false;

    /// <summary>
    /// Whether to only validate the flow without executing
    /// </summary>
    public bool ValidateOnly { get; set; } = false;
    
    // Execution context
    /// <summary>
    /// User ID for tracking and billing
    /// </summary>
    public string? UserId { get; set; }
    
    /// <summary>
    /// Session ID for grouping related executions
    /// </summary>
    public string? SessionId { get; set; }
    
    /// <summary>
    /// Additional execution context
    /// </summary>
    public Dictionary<string, object> Context { get; set; } = new();
    
    // Performance options
    /// <summary>
    /// Maximum number of nodes to execute concurrently
    /// </summary>
    public int? MaxConcurrentNodes { get; set; }
    
    /// <summary>
    /// Number of retry attempts for failed nodes
    /// </summary>
    public int? RetryAttempts { get; set; } = 0;
    
    /// <summary>
    /// Delay between retry attempts
    /// </summary>
    public TimeSpan? RetryDelay { get; set; }
    
    // Experimentation
    /// <summary>
    /// Experiment ID if this execution is part of an A/B test
    /// </summary>
    public Guid? ExperimentId { get; set; }
    
    /// <summary>
    /// Whether to enable automatic variant selection
    /// </summary>
    public bool EnableVariantSelection { get; set; } = false;
    
    /// <summary>
    /// Force a specific variant (overrides automatic selection)
    /// </summary>
    public Guid? ForceVariantId { get; set; }
    
    // Analytics and monitoring
    /// <summary>
    /// Whether to collect detailed execution metrics
    /// </summary>
    public bool CollectDetailedMetrics { get; set; } = true;
    
    /// <summary>
    /// Whether to enable distributed tracing
    /// </summary>
    public bool EnableTracing { get; set; } = false;
    
    /// <summary>
    /// Custom metrics to track during execution
    /// </summary>
    public List<string> CustomMetrics { get; set; } = new();
    
    // Cost control
    /// <summary>
    /// Maximum cost threshold for execution (execution will abort if exceeded)
    /// </summary>
    public decimal? MaxCostThreshold { get; set; }
    
    /// <summary>
    /// Maximum token threshold for execution
    /// </summary>
    public int? MaxTokenThreshold { get; set; }
    
    /// <summary>
    /// Whether to estimate costs before execution
    /// </summary>
    public bool EstimateCosts { get; set; } = false;
    
    // Model preferences
    /// <summary>
    /// Preferred model provider (if multiple options available)
    /// </summary>
    public string? PreferredProvider { get; set; }
    
    /// <summary>
    /// Model selection strategy (fastest, cheapest, best_quality)
    /// </summary>
    public ModelSelectionStrategy ModelSelection { get; set; } = ModelSelectionStrategy.Default;
    
    // Error handling
    /// <summary>
    /// Whether to continue execution on non-critical node failures
    /// </summary>
    public bool ContinueOnError { get; set; } = false;
    
    /// <summary>
    /// Whether to collect partial results even if execution fails
    /// </summary>
    public bool CollectPartialResults { get; set; } = true;
    
    // Caching
    /// <summary>
    /// Whether to enable result caching for repeated executions
    /// </summary>
    public bool EnableCaching { get; set; } = false;
    
    /// <summary>
    /// Cache TTL in minutes
    /// </summary>
    public int? CacheTtlMinutes { get; set; }
    
    // Notifications
    /// <summary>
    /// Whether to send notifications on completion
    /// </summary>
    public bool NotifyOnCompletion { get; set; } = false;
    
    /// <summary>
    /// Whether to send notifications on failure
    /// </summary>
    public bool NotifyOnFailure { get; set; } = false;
    
    /// <summary>
    /// Webhook URL for execution notifications
    /// </summary>
    public string? WebhookUrl { get; set; }
}
