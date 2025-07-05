namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents a single data point in a usage trend time series, capturing key metrics for a specific time period.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Fundamental building block for trend analysis services, time-series analytics, and dashboard visualizations.
/// Used in collections to represent usage patterns over time in analytics and reporting workflows.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Atomic trend data point optimized for time-series collections and chart rendering.
/// Designed for efficient serialization in large time-series datasets and real-time dashboard updates.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Time-series chart data for dashboard visualization</item>
/// <item>Trend analysis and pattern recognition algorithms</item>
/// <item>Performance monitoring over time periods</item>
/// <item>Historical comparison and forecasting inputs</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight data structure optimized for collection operations and JSON serialization.
/// TimeSpan serialization is efficient but may require custom converters in some client scenarios.
/// Ideal for bulk time-series data transmission and caching.</para>
/// </remarks>
public class UsageTrendPoint
{
    /// <summary>
    /// Gets or sets the date/time for this trend data point.
    /// </summary>
    /// <value>A DateTime representing the specific time period this data point covers.</value>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the number of executions recorded during this time period.
    /// </summary>
    /// <value>A non-negative integer representing the execution count for this time point.</value>
    public long ExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the count of unique users active during this time period.
    /// </summary>
    /// <value>A non-negative integer representing distinct user activity for this time point.</value>
    public long UniqueUsers { get; set; }

    /// <summary>
    /// Gets or sets the success rate for executions during this time period.
    /// </summary>
    /// <value>A decimal between 0.0 and 1.0 representing the success rate for this time point.</value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution time during this time period.
    /// </summary>
    /// <value>A TimeSpan representing the mean execution duration for this time point.</value>
    public TimeSpan AverageExecutionTime { get; set; }
}
