namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents comprehensive performance metrics for a template, including latency, success rates, and quality indicators.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Core performance monitoring DTO used by analytics services, SLA monitoring systems, and optimization engines.
/// Essential for template quality assurance, performance alerting, and automatic scaling decisions in LLMOps environments.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Statistical performance data with percentile calculations and error categorization.
/// Designed for efficient metric aggregation and real-time monitoring dashboard updates.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Real-time performance monitoring and alerting</item>
/// <item>SLA compliance tracking and reporting</item>
/// <item>Template optimization and performance tuning</item>
/// <item>Comparative performance analysis across templates</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Optimized for frequent updates in monitoring scenarios. Percentile calculations (P95, P99) are pre-computed
/// for efficiency. Dictionary collections should be monitored for size to prevent payload inflation in high-error scenarios.</para>
/// </remarks>
public class TemplatePerformanceMetrics
{
    /// <summary>
    /// Gets or sets the unique identifier of the template being measured.
    /// </summary>
    /// <value>A valid GUID representing the template entity.</value>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the success rate as a decimal percentage (0.0 to 1.0).
    /// </summary>
    /// <value>A decimal between 0.0 and 1.0, where 1.0 represents 100% success rate.</value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average response latency across all executions.
    /// </summary>
    /// <value>A decimal representing the mean latency in milliseconds.</value>
    public double AverageLatency { get; set; }

    /// <summary>
    /// Gets or sets the 95th percentile latency for performance monitoring.
    /// </summary>
    /// <value>A decimal representing the P95 latency in milliseconds, indicating that 95% of requests complete faster.</value>
    public double P95Latency { get; set; }

    /// <summary>
    /// Gets or sets the 99th percentile latency for outlier detection.
    /// </summary>
    /// <value>A decimal representing the P99 latency in milliseconds, useful for identifying performance outliers.</value>
    public double P99Latency { get; set; }

    /// <summary>
    /// Gets or sets the total count of errors recorded for this template.
    /// </summary>
    /// <value>A non-negative integer representing the cumulative error count.</value>
    public long ErrorCount { get; set; }

    /// <summary>
    /// Gets or sets the error distribution categorized by error type.
    /// </summary>
    /// <value>A dictionary mapping error type names to their occurrence counts, useful for error pattern analysis.</value>
    public Dictionary<string, long> ErrorsByType { get; set; } = [];

    /// <summary>
    /// Gets or sets the overall quality score for this template.
    /// </summary>
    /// <value>A decimal score (typically 0.0 to 1.0) representing template quality based on multiple factors.</value>
    public double QualityScore { get; set; }

    /// <summary>
    /// Gets or sets the performance trend indicators over time.
    /// </summary>
    /// <value>A dictionary mapping metric names to trend values, enabling performance trajectory analysis.</value>
    public Dictionary<string, double> PerformanceTrends { get; set; } = [];
}
