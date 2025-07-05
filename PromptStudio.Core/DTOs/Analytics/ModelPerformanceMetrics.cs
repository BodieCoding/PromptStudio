namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents performance metrics for an AI model, providing comprehensive statistics for monitoring and optimization.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Used in analytics services for model performance monitoring, benchmarking, and optimization decisions.
/// Essential for multi-model LLMOps environments where performance comparison drives routing decisions.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Aggregated performance data suitable for real-time dashboards and periodic reports.
/// Contains computed metrics rather than raw execution data for efficient transmission.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Model comparison and benchmarking analytics</item>
/// <item>Performance trend analysis over time</item>
/// <item>Cost optimization through token usage tracking</item>
/// <item>SLA monitoring and alerting</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight DTO optimized for frequent API calls. TimeSpan serialization may require custom converters
/// in some JSON serialization scenarios. Consider caching for dashboard scenarios.</para>
/// </remarks>
public class ModelPerformanceMetrics
{
    /// <summary>
    /// Gets or sets the name/identifier of the AI model being measured.
    /// </summary>
    /// <value>A non-empty string identifying the model (e.g., "gpt-4", "claude-3-opus").</value>
    public string ModelName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total number of executions recorded for this model.
    /// </summary>
    /// <value>A positive integer representing the execution count over the measurement period.</value>
    public long ExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the success rate as a decimal percentage (0.0 to 1.0).
    /// </summary>
    /// <value>A decimal between 0.0 and 1.0, where 1.0 represents 100% success rate.</value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average response time for successful executions.
    /// </summary>
    /// <value>A TimeSpan representing the mean response time across all successful executions.</value>
    public TimeSpan AverageResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the total token usage across all executions for this model.
    /// </summary>
    /// <value>A positive integer representing the cumulative token consumption.</value>
    public long TotalTokenUsage { get; set; }
}
