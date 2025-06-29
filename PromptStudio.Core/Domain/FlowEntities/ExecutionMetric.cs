using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Custom metrics for detailed workflow analytics and performance monitoring.
/// Stores key-value metrics collected during workflow execution to enable
/// comprehensive analysis, debugging, and optimization of LLM workflows.
/// </summary>
/// <remarks>
/// ExecutionMetric provides a flexible schema for capturing various types of
/// execution data including performance counters, quality scores, resource usage,
/// and custom business metrics. Supports multiple data types through the MetricType
/// field and enables rich analytics through proper categorization and units.
/// </remarks>
public class ExecutionMetric : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the associated workflow execution.
    /// Links this metric to a specific FlowExecution instance for proper grouping
    /// and analysis of execution performance.
    /// </summary>
    /// <value>The GUID of the FlowExecution that generated this metric.</value>
    public Guid FlowExecutionId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the associated workflow execution.
    /// Provides access to the full execution context and related data.
    /// </summary>
    /// <value>The FlowExecution entity that owns this metric.</value>
    public virtual FlowExecution FlowExecution { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the name of the metric being recorded.
    /// Should follow a consistent naming convention for easy querying and analysis.
    /// </summary>
    /// <value>
    /// A descriptive name for the metric (e.g., "response_time_ms", "token_count", 
    /// "quality_score", "error_rate"). Maximum length is 50 characters.
    /// </value>
    /// <example>
    /// Examples: "execution_duration_ms", "tokens_consumed", "quality_score", 
    /// "model_confidence", "cache_hit_rate", "memory_usage_mb"
    /// </example>
    [Required]
    [StringLength(50)]
    public string MetricName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the value of the metric as a string representation.
    /// The actual data type is determined by the MetricType property.
    /// </summary>
    /// <value>
    /// The metric value stored as a string. For non-string types, this contains
    /// the serialized representation (e.g., "123.45" for numbers, "true" for booleans,
    /// JSON for complex objects).
    /// </value>
    /// <remarks>
    /// String storage provides flexibility for different data types while maintaining
    /// query capabilities. Applications should parse the value according to MetricType.
    /// </remarks>
    [Required]
    public string MetricValue { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the data type of the metric value for proper interpretation and processing.
    /// Enables type-safe parsing and appropriate analytics operations.
    /// </summary>
    /// <value>
    /// The type of data stored in MetricValue. Supported values: "string", "number", 
    /// "boolean", "json". Defaults to "string". Maximum length is 20 characters.
    /// </value>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description><c>string</c> - Text values, names, descriptions</description></item>
    /// <item><description><c>number</c> - Numeric values (integers, decimals)</description></item>
    /// <item><description><c>boolean</c> - True/false values (stored as "true"/"false")</description></item>
    /// <item><description><c>json</c> - Complex objects serialized as JSON</description></item>
    /// </list>
    /// </remarks>
    [StringLength(20)]
    public string MetricType { get; set; } = "string";
    
    /// <summary>
    /// Gets or sets the unit of measurement for numeric metrics.
    /// Provides context for interpretation and enables proper aggregation and comparison.
    /// </summary>
    /// <value>
    /// The unit of measurement (e.g., "ms", "seconds", "bytes", "MB", "tokens", "%").
    /// Optional field, maximum length is 50 characters. Null for non-numeric metrics.
    /// </value>
    /// <example>
    /// Examples: "ms" (milliseconds), "tokens", "MB" (megabytes), "%" (percentage),
    /// "requests/sec", "USD", "score" (0-100)
    /// </example>
    [StringLength(50)]
    public string? MetricUnit { get; set; }
    
    /// <summary>
    /// Gets or sets an optional human-readable description of what this metric represents.
    /// Provides additional context for metric interpretation and usage.
    /// </summary>
    /// <value>
    /// A descriptive explanation of the metric's purpose, calculation method, or significance.
    /// Optional field, maximum length is 200 characters.
    /// </value>
    /// <example>
    /// Examples: "Time taken for LLM API response", "Quality score from 0-100 based on rubric",
    /// "Memory consumed during template processing", "Number of retries due to rate limits"
    /// </example>
    [StringLength(200)]
    public string? Description { get; set; }
}