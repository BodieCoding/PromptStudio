namespace PromptStudio.Core.DTOs.Statistics;

/// <summary>
/// Represents comprehensive execution statistics for prompt operations.
/// Provides detailed metrics and performance data for prompt execution analysis and reporting.
/// </summary>
/// <remarks>
/// <para><strong>Analytics Foundation:</strong></para>
/// <para>Core statistics container that aggregates execution data across different dimensions
/// including success rates, performance metrics, error patterns, and usage trends.
/// Essential for performance monitoring, optimization, and business intelligence reporting.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>Performance dashboard and monitoring systems</description></item>
/// <item><description>Business intelligence and reporting applications</description></item>
/// <item><description>System optimization and capacity planning</description></item>
/// <item><description>User behavior analysis and usage pattern detection</description></item>
/// </list>
/// 
/// <para><strong>Data Aggregation:</strong></para>
/// <para>Supports multiple aggregation levels from individual prompt executions to
/// system-wide statistics, with configurable time ranges and filtering options
/// for comprehensive analytics coverage.</para>
/// </remarks>
public class ExecutionStatistics
{
    /// <summary>
    /// Gets or sets the total number of executions recorded in this statistics period.
    /// Provides the baseline count for all other metrics and calculations.
    /// </summary>
    /// <value>
    /// The total execution count. Must be non-negative.
    /// </value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of successful executions.
    /// Represents executions that completed without errors or failures.
    /// </summary>
    /// <value>
    /// The count of successful executions. Must be non-negative and not exceed TotalExecutions.
    /// </value>
    public long SuccessfulExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of failed executions.
    /// Represents executions that encountered errors or could not complete successfully.
    /// </summary>
    /// <value>
    /// The count of failed executions. Must be non-negative.
    /// </value>
    public long FailedExecutions { get; set; }

    /// <summary>
    /// Gets the success rate as a percentage of total executions.
    /// Calculated property providing success ratio for performance assessment.
    /// </summary>
    /// <value>
    /// The success rate percentage (0-100), or 0 if no executions recorded.
    /// </value>
    public double SuccessRate => TotalExecutions > 0 
        ? (double)SuccessfulExecutions / TotalExecutions * 100 
        : 0;

    /// <summary>
    /// Gets the failure rate as a percentage of total executions.
    /// Calculated property providing failure ratio for error analysis.
    /// </summary>
    /// <value>
    /// The failure rate percentage (0-100), or 0 if no executions recorded.
    /// </value>
    public double FailureRate => TotalExecutions > 0 
        ? (double)FailedExecutions / TotalExecutions * 100 
        : 0;

    /// <summary>
    /// Gets or sets the average execution duration across all executions.
    /// Provides performance baseline for execution time analysis.
    /// </summary>
    /// <value>
    /// The average execution duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the minimum execution duration recorded.
    /// Represents the fastest execution time for performance optimization insights.
    /// </summary>
    /// <value>
    /// The minimum execution duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? MinExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the maximum execution duration recorded.
    /// Represents the slowest execution time for performance bottleneck identification.
    /// </summary>
    /// <value>
    /// The maximum execution duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? MaxExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the median execution duration.
    /// Provides robust central tendency measure less affected by outliers.
    /// </summary>
    /// <value>
    /// The median execution duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? MedianExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the 95th percentile execution duration.
    /// Indicates that 95% of executions complete within this time.
    /// </summary>
    /// <value>
    /// The 95th percentile execution duration, or null if insufficient data.
    /// </value>
    public TimeSpan? P95ExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the total number of unique users who performed executions.
    /// Provides user engagement and adoption metrics.
    /// </summary>
    /// <value>
    /// The count of unique users. Must be non-negative.
    /// </value>
    public long UniqueUsers { get; set; }

    /// <summary>
    /// Gets or sets the total number of unique prompt templates executed.
    /// Indicates prompt diversity and usage patterns.
    /// </summary>
    /// <value>
    /// The count of unique prompt templates. Must be non-negative.
    /// </value>
    public long UniquePromptTemplates { get; set; }

    /// <summary>
    /// Gets or sets the average number of executions per user.
    /// Provides user engagement depth metrics.
    /// </summary>
    /// <value>
    /// The average executions per user, or 0 if no users recorded.
    /// </value>
    public double AverageExecutionsPerUser => UniqueUsers > 0 
        ? (double)TotalExecutions / UniqueUsers 
        : 0;

    /// <summary>
    /// Gets or sets the average number of executions per prompt template.
    /// Indicates template popularity and usage distribution.
    /// </summary>
    /// <value>
    /// The average executions per template, or 0 if no templates recorded.
    /// </value>
    public double AverageExecutionsPerTemplate => UniquePromptTemplates > 0 
        ? (double)TotalExecutions / UniquePromptTemplates 
        : 0;

    /// <summary>
    /// Gets or sets the total data transfer volume for all executions.
    /// Includes input and output data for resource utilization analysis.
    /// </summary>
    /// <value>
    /// The total data volume in bytes. Must be non-negative.
    /// </value>
    public long TotalDataVolume { get; set; }

    /// <summary>
    /// Gets or sets the average data transfer per execution.
    /// Provides resource utilization insights per operation.
    /// </summary>
    /// <value>
    /// The average data volume per execution in bytes.
    /// </value>
    public double AverageDataVolumePerExecution => TotalExecutions > 0 
        ? (double)TotalDataVolume / TotalExecutions 
        : 0;

    /// <summary>
    /// Gets or sets the start time of the statistics period.
    /// Defines the beginning of the data collection timeframe.
    /// </summary>
    /// <value>
    /// The start timestamp for this statistics period.
    /// </value>
    public DateTime PeriodStart { get; set; }

    /// <summary>
    /// Gets or sets the end time of the statistics period.
    /// Defines the conclusion of the data collection timeframe.
    /// </summary>
    /// <value>
    /// The end timestamp for this statistics period.
    /// </value>
    public DateTime PeriodEnd { get; set; }

    /// <summary>
    /// Gets the duration of the statistics period.
    /// Calculated property providing the timespan covered by these statistics.
    /// </summary>
    /// <value>
    /// The duration of the statistics period.
    /// </value>
    public TimeSpan PeriodDuration => PeriodEnd - PeriodStart;

    /// <summary>
    /// Gets or sets the throughput rate as executions per hour.
    /// Provides system load and capacity utilization metrics.
    /// </summary>
    /// <value>
    /// The executions per hour rate, or 0 if period duration is zero.
    /// </value>
    public double ExecutionsPerHour => PeriodDuration.TotalHours > 0 
        ? TotalExecutions / PeriodDuration.TotalHours 
        : 0;

    /// <summary>
    /// Gets or sets the collection of error categories and their occurrence counts.
    /// Provides detailed error analysis for troubleshooting and improvement.
    /// </summary>
    /// <value>
    /// A dictionary mapping error categories to occurrence counts.
    /// </value>
    public Dictionary<string, long> ErrorCategoryCounts { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of execution duration distributions.
    /// Provides detailed performance analysis across different time buckets.
    /// </summary>
    /// <value>
    /// A dictionary mapping duration ranges to execution counts.
    /// </value>
    public Dictionary<string, long> DurationDistribution { get; set; } = [];

    /// <summary>
    /// Gets or sets additional metadata and custom metrics.
    /// Provides extensible storage for domain-specific analytics.
    /// </summary>
    /// <value>
    /// A dictionary containing custom metrics and metadata.
    /// </value>
    public Dictionary<string, object> AdditionalMetrics { get; set; } = [];

    /// <summary>
    /// Gets or sets the timestamp when these statistics were last updated.
    /// Provides freshness information for data validity assessment.
    /// </summary>
    /// <value>
    /// The last update timestamp, or null if not tracked.
    /// </value>
    public DateTime? LastUpdated { get; set; }

    /// <summary>
    /// Initializes a new instance of the ExecutionStatistics class with default values.
    /// Creates an empty statistics instance ready for data population.
    /// </summary>
    public ExecutionStatistics()
    {
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the ExecutionStatistics class for a specific period.
    /// Creates a statistics instance with defined time boundaries.
    /// </summary>
    /// <param name="periodStart">The start of the statistics period</param>
    /// <param name="periodEnd">The end of the statistics period</param>
    public ExecutionStatistics(DateTime periodStart, DateTime periodEnd)
    {
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds an error category count to the statistics.
    /// Provides fluent API for accumulating error data.
    /// </summary>
    /// <param name="category">The error category name</param>
    /// <param name="count">The number of occurrences</param>
    /// <returns>This ExecutionStatistics instance for method chaining</returns>
    public ExecutionStatistics AddErrorCategory(string category, long count)
    {
        ErrorCategoryCounts[category] = ErrorCategoryCounts.GetValueOrDefault(category, 0) + count;
        return this;
    }

    /// <summary>
    /// Adds a duration distribution bucket to the statistics.
    /// Provides fluent API for accumulating performance distribution data.
    /// </summary>
    /// <param name="durationRange">The duration range label</param>
    /// <param name="count">The number of executions in this range</param>
    /// <returns>This ExecutionStatistics instance for method chaining</returns>
    public ExecutionStatistics AddDurationBucket(string durationRange, long count)
    {
        DurationDistribution[durationRange] = DurationDistribution.GetValueOrDefault(durationRange, 0) + count;
        return this;
    }

    /// <summary>
    /// Adds a custom metric to the statistics.
    /// Provides fluent API for accumulating domain-specific analytics.
    /// </summary>
    /// <param name="metricName">The metric name</param>
    /// <param name="value">The metric value</param>
    /// <returns>This ExecutionStatistics instance for method chaining</returns>
    public ExecutionStatistics AddMetric(string metricName, object value)
    {
        AdditionalMetrics[metricName] = value;
        return this;
    }

    /// <summary>
    /// Updates the last modified timestamp to the current time.
    /// Marks the statistics as recently updated for freshness tracking.
    /// </summary>
    /// <returns>This ExecutionStatistics instance for method chaining</returns>
    public ExecutionStatistics MarkUpdated()
    {
        LastUpdated = DateTime.UtcNow;
        return this;
    }

    /// <summary>
    /// Validates the statistical data for consistency and correctness.
    /// Ensures all counts and calculations are logically valid.
    /// </summary>
    /// <returns>A list of validation error messages, empty if data is valid</returns>
    public List<string> Validate()
    {
        var errors = new List<string>();

        if (TotalExecutions < 0)
            errors.Add("TotalExecutions cannot be negative");

        if (SuccessfulExecutions < 0)
            errors.Add("SuccessfulExecutions cannot be negative");

        if (FailedExecutions < 0)
            errors.Add("FailedExecutions cannot be negative");

        if (SuccessfulExecutions + FailedExecutions > TotalExecutions)
            errors.Add("Sum of successful and failed executions cannot exceed total executions");

        if (UniqueUsers < 0)
            errors.Add("UniqueUsers cannot be negative");

        if (UniquePromptTemplates < 0)
            errors.Add("UniquePromptTemplates cannot be negative");

        if (TotalDataVolume < 0)
            errors.Add("TotalDataVolume cannot be negative");

        if (PeriodEnd < PeriodStart)
            errors.Add("PeriodEnd cannot be before PeriodStart");

        return errors;
    }

    /// <summary>
    /// Creates a summary report of the execution statistics.
    /// Provides formatted text summary suitable for logging and reporting.
    /// </summary>
    /// <returns>A formatted string containing key statistics and metrics</returns>
    public string CreateSummaryReport()
    {
        return $"Execution Statistics Summary:\n" +
               $"  Period: {PeriodStart:yyyy-MM-dd} to {PeriodEnd:yyyy-MM-dd}\n" +
               $"  Total Executions: {TotalExecutions:N0}\n" +
               $"  Success Rate: {SuccessRate:F1}% ({SuccessfulExecutions:N0} successful)\n" +
               $"  Failure Rate: {FailureRate:F1}% ({FailedExecutions:N0} failed)\n" +
               $"  Average Duration: {AverageExecutionDuration?.TotalMilliseconds:F2} ms\n" +
               $"  Throughput: {ExecutionsPerHour:F2} executions/hour\n" +
               $"  Unique Users: {UniqueUsers:N0}\n" +
               $"  Unique Templates: {UniquePromptTemplates:N0}\n" +
               $"  Data Volume: {TotalDataVolume / 1024.0 / 1024.0:F2} MB\n" +
               $"  Last Updated: {LastUpdated:yyyy-MM-dd HH:mm:ss} UTC";
    }

    /// <summary>
    /// Returns a string representation of the execution statistics for debugging and logging.
    /// Provides essential statistics information in a readable format.
    /// </summary>
    /// <returns>String representation including key metrics and time period</returns>
    public override string ToString()
    {
        return $"ExecutionStatistics: {TotalExecutions:N0} executions ({SuccessRate:F1}% success) " +
               $"from {PeriodStart:yyyy-MM-dd} to {PeriodEnd:yyyy-MM-dd}";
    }
}
