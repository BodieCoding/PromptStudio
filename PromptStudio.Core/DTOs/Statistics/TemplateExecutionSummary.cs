namespace PromptStudio.Core.DTOs.Statistics;

/// <summary>
/// Represents comprehensive execution summary information for individual template performance analysis.
/// Provides detailed metrics and insights specific to a single prompt template's execution history and performance.
/// </summary>
/// <remarks>
/// <para><strong>Template-Focused Analytics:</strong></para>
/// <para>Specialized summary container that provides comprehensive execution analysis for individual
/// prompt templates including usage patterns, performance metrics, error analysis, and optimization insights.
/// Essential for template performance optimization, quality assessment, and usage trend analysis.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>Template performance dashboard and individual template analytics</description></item>
/// <item><description>Template optimization and quality improvement processes</description></item>
/// <item><description>Template usage reporting and ROI analysis</description></item>
/// <item><description>Template comparison and benchmarking for library curation</description></item>
/// </list>
/// 
/// <para><strong>Analysis Depth:</strong></para>
/// <para>Provides both high-level summary metrics and detailed execution breakdowns,
/// enabling both quick overview assessment and deep-dive analysis for template optimization
/// and quality management decisions.</para>
/// </remarks>
public class TemplateExecutionSummary
{
    /// <summary>
    /// Gets or sets the unique identifier of the template this summary represents.
    /// Links the summary to the specific template for tracking and reporting.
    /// </summary>
    /// <value>
    /// The template identifier. Must be positive.
    /// </value>
    public int TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the name of the template for display and reporting purposes.
    /// Provides human-readable identification for the summary context.
    /// </summary>
    /// <value>
    /// The template name, or null if not specified.
    /// </value>
    public string? TemplateName { get; set; }

    /// <summary>
    /// Gets or sets the description of the template for context and categorization.
    /// Provides additional context for understanding template purpose and use cases.
    /// </summary>
    /// <value>
    /// The template description, or null if not provided.
    /// </value>
    public string? TemplateDescription { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the collection containing this template.
    /// Provides organizational context for the template within the library structure.
    /// </summary>
    /// <value>
    /// The collection identifier, or null if template is not in a collection.
    /// </value>
    public int? CollectionId { get; set; }

    /// <summary>
    /// Gets or sets the name of the collection containing this template.
    /// Provides human-readable organizational context for reporting.
    /// </summary>
    /// <value>
    /// The collection name, or null if template is not in a collection.
    /// </value>
    public string? CollectionName { get; set; }

    /// <summary>
    /// Gets or sets the total number of times this template has been executed.
    /// Provides the baseline count for all other metrics and popularity assessment.
    /// </summary>
    /// <value>
    /// The total execution count. Must be non-negative.
    /// </value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of successful executions for this template.
    /// Represents executions that completed without errors for quality assessment.
    /// </summary>
    /// <value>
    /// The successful execution count. Must be non-negative and not exceed TotalExecutions.
    /// </value>
    public long SuccessfulExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of failed executions for this template.
    /// Represents executions that encountered errors for quality and reliability analysis.
    /// </summary>
    /// <value>
    /// The failed execution count. Must be non-negative.
    /// </value>
    public long FailedExecutions { get; set; }

    /// <summary>
    /// Gets the success rate as a percentage of total executions.
    /// Calculated property providing quality metrics for template assessment.
    /// </summary>
    /// <value>
    /// The success rate percentage (0-100), or 0 if no executions recorded.
    /// </value>
    public double SuccessRate => TotalExecutions > 0 
        ? (double)SuccessfulExecutions / TotalExecutions * 100 
        : 0;

    /// <summary>
    /// Gets the failure rate as a percentage of total executions.
    /// Calculated property providing error analysis metrics for template optimization.
    /// </summary>
    /// <value>
    /// The failure rate percentage (0-100), or 0 if no executions recorded.
    /// </value>
    public double FailureRate => TotalExecutions > 0 
        ? (double)FailedExecutions / TotalExecutions * 100 
        : 0;

    /// <summary>
    /// Gets or sets the average execution duration for this template.
    /// Provides performance baseline for template optimization and comparison.
    /// </summary>
    /// <value>
    /// The average execution duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the fastest recorded execution duration for this template.
    /// Represents the optimal performance potential for benchmarking purposes.
    /// </summary>
    /// <value>
    /// The minimum execution duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? FastestExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the slowest recorded execution duration for this template.
    /// Identifies performance bottlenecks and outlier scenarios for optimization.
    /// </summary>
    /// <value>
    /// The maximum execution duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? SlowestExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the median execution duration for this template.
    /// Provides robust central tendency measure less affected by performance outliers.
    /// </summary>
    /// <value>
    /// The median execution duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? MedianExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the number of unique users who have executed this template.
    /// Indicates template adoption and user engagement levels.
    /// </summary>
    /// <value>
    /// The unique user count. Must be non-negative.
    /// </value>
    public long UniqueUsers { get; set; }

    /// <summary>
    /// Gets or sets the average number of executions per user for this template.
    /// Provides user engagement depth metrics and template utility assessment.
    /// </summary>
    /// <value>
    /// The average executions per user, or 0 if no users recorded.
    /// </value>
    public double AverageExecutionsPerUser => UniqueUsers > 0 
        ? (double)TotalExecutions / UniqueUsers 
        : 0;

    /// <summary>
    /// Gets or sets the timestamp of the first execution of this template.
    /// Provides template lifecycle information and adoption timeline context.
    /// </summary>
    /// <value>
    /// The first execution timestamp, or null if no executions recorded.
    /// </value>
    public DateTime? FirstExecuted { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the most recent execution of this template.
    /// Indicates current usage activity and template relevance.
    /// </summary>
    /// <value>
    /// The last execution timestamp, or null if no executions recorded.
    /// </value>
    public DateTime? LastExecuted { get; set; }

    /// <summary>
    /// Gets the time span between first and last execution.
    /// Calculated property providing template usage lifespan information.
    /// </summary>
    /// <value>
    /// The usage lifespan, or null if insufficient execution data.
    /// </value>
    public TimeSpan? UsageLifespan => FirstExecuted.HasValue && LastExecuted.HasValue 
        ? LastExecuted.Value - FirstExecuted.Value 
        : null;

    /// <summary>
    /// Gets or sets the collection of execution counts by time period.
    /// Provides usage trend analysis and seasonal pattern identification.
    /// </summary>
    /// <value>
    /// A dictionary mapping time period labels to execution counts.
    /// </value>
    public Dictionary<string, long> ExecutionsByTimePeriod { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of most common error types encountered.
    /// Provides error pattern analysis for template improvement opportunities.
    /// </summary>
    /// <value>
    /// A dictionary mapping error types to occurrence counts.
    /// </value>
    public Dictionary<string, long> CommonErrorTypes { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of variable usage statistics for this template.
    /// Provides insights into variable utilization patterns and optimization opportunities.
    /// </summary>
    /// <value>
    /// A dictionary mapping variable names to usage statistics.
    /// </value>
    public Dictionary<string, VariableUsageStats> VariableUsage { get; set; } = [];

    /// <summary>
    /// Gets or sets the average input size for template executions in characters.
    /// Provides context for template complexity and resource utilization analysis.
    /// </summary>
    /// <value>
    /// The average input size in characters. Must be non-negative.
    /// </value>
    public double AverageInputSize { get; set; }

    /// <summary>
    /// Gets or sets the average output size for template executions in characters.
    /// Provides context for template productivity and resource utilization analysis.
    /// </summary>
    /// <value>
    /// The average output size in characters. Must be non-negative.
    /// </value>
    public double AverageOutputSize { get; set; }

    /// <summary>
    /// Gets or sets the template quality score based on various performance metrics.
    /// Provides overall template assessment for curation and optimization decisions.
    /// </summary>
    /// <value>
    /// The quality score (0-100), or null if not calculated.
    /// </value>
    public double? QualityScore { get; set; }

    /// <summary>
    /// Gets or sets the popularity rank of this template within its collection.
    /// Provides relative popularity assessment for content curation decisions.
    /// </summary>
    /// <value>
    /// The popularity rank within collection, or null if not ranked.
    /// </value>
    public int? PopularityRankInCollection { get; set; }

    /// <summary>
    /// Gets or sets the popularity rank of this template within the entire library.
    /// Provides global popularity assessment for feature promotion and optimization.
    /// </summary>
    /// <value>
    /// The popularity rank within library, or null if not ranked.
    /// </value>
    public int? PopularityRankInLibrary { get; set; }

    /// <summary>
    /// Gets or sets additional template-specific metrics and custom analytics.
    /// Provides extensible storage for domain-specific template measurements.
    /// </summary>
    /// <value>
    /// A dictionary containing template-specific metrics and analytics data.
    /// </value>
    public Dictionary<string, object> AdditionalMetrics { get; set; } = [];

    /// <summary>
    /// Gets or sets the timestamp when this summary was last updated.
    /// Provides freshness information for data validity and cache management.
    /// </summary>
    /// <value>
    /// The last update timestamp, or null if not tracked.
    /// </value>
    public DateTime? LastUpdated { get; set; }

    /// <summary>
    /// Gets or sets recommendations for template improvement based on analysis.
    /// Provides actionable insights for template optimization and quality enhancement.
    /// </summary>
    /// <value>
    /// A list of improvement recommendations, empty if no recommendations available.
    /// </value>
    public List<string> ImprovementRecommendations { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the TemplateExecutionSummary class with default values.
    /// Creates an empty summary instance ready for data population.
    /// </summary>
    public TemplateExecutionSummary()
    {
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the TemplateExecutionSummary class for a specific template.
    /// Creates a summary instance with defined template context.
    /// </summary>
    /// <param name="templateId">The unique identifier of the template</param>
    /// <param name="templateName">The name of the template</param>
    public TemplateExecutionSummary(int templateId, string templateName)
    {
        TemplateId = templateId;
        TemplateName = templateName;
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds execution data for a specific time period.
    /// Provides fluent API for accumulating temporal usage patterns.
    /// </summary>
    /// <param name="timePeriod">The time period label (e.g., "2024-01", "Week 1")</param>
    /// <param name="executionCount">The number of executions in this period</param>
    /// <returns>This TemplateExecutionSummary instance for method chaining</returns>
    public TemplateExecutionSummary AddExecutionsByPeriod(string timePeriod, long executionCount)
    {
        ExecutionsByTimePeriod[timePeriod] = executionCount;
        return this;
    }

    /// <summary>
    /// Adds error type data to the summary.
    /// Provides fluent API for accumulating error pattern analysis.
    /// </summary>
    /// <param name="errorType">The error type or category</param>
    /// <param name="count">The number of occurrences</param>
    /// <returns>This TemplateExecutionSummary instance for method chaining</returns>
    public TemplateExecutionSummary AddErrorType(string errorType, long count)
    {
        CommonErrorTypes[errorType] = count;
        return this;
    }

    /// <summary>
    /// Adds variable usage statistics to the summary.
    /// Provides fluent API for accumulating variable utilization data.
    /// </summary>
    /// <param name="variableName">The variable name</param>
    /// <param name="usageStats">The usage statistics for this variable</param>
    /// <returns>This TemplateExecutionSummary instance for method chaining</returns>
    public TemplateExecutionSummary AddVariableUsage(string variableName, VariableUsageStats usageStats)
    {
        VariableUsage[variableName] = usageStats;
        return this;
    }

    /// <summary>
    /// Adds a custom metric to the summary.
    /// Provides fluent API for accumulating template-specific analytics.
    /// </summary>
    /// <param name="metricName">The metric name</param>
    /// <param name="value">The metric value</param>
    /// <returns>This TemplateExecutionSummary instance for method chaining</returns>
    public TemplateExecutionSummary AddMetric(string metricName, object value)
    {
        AdditionalMetrics[metricName] = value;
        return this;
    }

    /// <summary>
    /// Adds an improvement recommendation to the summary.
    /// Provides fluent API for accumulating optimization suggestions.
    /// </summary>
    /// <param name="recommendation">The improvement recommendation text</param>
    /// <returns>This TemplateExecutionSummary instance for method chaining</returns>
    public TemplateExecutionSummary AddRecommendation(string recommendation)
    {
        ImprovementRecommendations.Add(recommendation);
        return this;
    }

    /// <summary>
    /// Updates the last modified timestamp to the current time.
    /// Marks the summary as recently updated for freshness tracking.
    /// </summary>
    /// <returns>This TemplateExecutionSummary instance for method chaining</returns>
    public TemplateExecutionSummary MarkUpdated()
    {
        LastUpdated = DateTime.UtcNow;
        return this;
    }

    /// <summary>
    /// Calculates the template quality score based on available metrics.
    /// Updates the QualityScore property with a calculated assessment.
    /// </summary>
    /// <returns>This TemplateExecutionSummary instance for method chaining</returns>
    public TemplateExecutionSummary CalculateQualityScore()
    {
        if (TotalExecutions == 0)
        {
            QualityScore = 0;
            return this;
        }

        // Quality score based on success rate (40%), usage frequency (30%), and performance (30%)
        var successComponent = SuccessRate * 0.4;
        
        var usageComponent = Math.Min(Math.Log10(TotalExecutions + 1) / Math.Log10(1000) * 100, 100) * 0.3;
        
        var performanceComponent = 0.0;
        if (AverageExecutionDuration.HasValue)
        {
            // Better performance = lower duration, scored inversely
            var avgSeconds = AverageExecutionDuration.Value.TotalSeconds;
            performanceComponent = Math.Max(0, 100 - (avgSeconds / 10)) * 0.3;
        }

        QualityScore = Math.Round(successComponent + usageComponent + performanceComponent, 2);
        return this;
    }

    /// <summary>
    /// Validates the summary data for consistency and correctness.
    /// Ensures all counts and calculations are logically valid.
    /// </summary>
    /// <returns>A list of validation error messages, empty if data is valid</returns>
    public List<string> Validate()
    {
        var errors = new List<string>();

        if (TemplateId <= 0)
            errors.Add("TemplateId must be positive");

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

        if (UniqueUsers > TotalExecutions)
            errors.Add("UniqueUsers cannot exceed TotalExecutions");

        if (AverageInputSize < 0)
            errors.Add("AverageInputSize cannot be negative");

        if (AverageOutputSize < 0)
            errors.Add("AverageOutputSize cannot be negative");

        if (QualityScore.HasValue && (QualityScore < 0 || QualityScore > 100))
            errors.Add("QualityScore must be between 0 and 100");

        if (FirstExecuted.HasValue && LastExecuted.HasValue && LastExecuted < FirstExecuted)
            errors.Add("LastExecuted cannot be before FirstExecuted");

        return errors;
    }

    /// <summary>
    /// Creates a comprehensive summary report of the template execution data.
    /// Provides formatted text summary suitable for template analysis and reporting.
    /// </summary>
    /// <returns>A formatted string containing template execution summary and metrics</returns>
    public string CreateSummaryReport()
    {
        var usageLifespanText = UsageLifespan?.Days.ToString() ?? "Unknown";
        var qualityScoreText = QualityScore?.ToString("F1") ?? "Not calculated";
        var collectionText = !string.IsNullOrEmpty(CollectionName) ? $" (Collection: {CollectionName})" : "";
        
        return $"Template Execution Summary:\n" +
               $"  Template: {TemplateName ?? "Unknown"} (ID: {TemplateId}){collectionText}\n" +
               $"  Total Executions: {TotalExecutions:N0}\n" +
               $"  Success Rate: {SuccessRate:F1}% ({SuccessfulExecutions:N0} successful, {FailedExecutions:N0} failed)\n" +
               $"  Unique Users: {UniqueUsers:N0} (avg {AverageExecutionsPerUser:F1} executions/user)\n" +
               $"  Performance: Avg {AverageExecutionDuration?.TotalMilliseconds:F0} ms " +
               $"(Range: {FastestExecutionDuration?.TotalMilliseconds:F0} - {SlowestExecutionDuration?.TotalMilliseconds:F0} ms)\n" +
               $"  Usage Period: {FirstExecuted:yyyy-MM-dd} to {LastExecuted:yyyy-MM-dd} ({usageLifespanText} days)\n" +
               $"  Content Size: Avg input {AverageInputSize:F0} chars, avg output {AverageOutputSize:F0} chars\n" +
               $"  Quality Score: {qualityScoreText}/100\n" +
               $"  Popularity: #{PopularityRankInCollection} in collection, #{PopularityRankInLibrary} in library\n" +
               $"  Error Types: {CommonErrorTypes.Count} different types, {CommonErrorTypes.Values.Sum():N0} total errors\n" +
               $"  Variables: {VariableUsage.Count} variables tracked\n" +
               $"  Recommendations: {ImprovementRecommendations.Count} improvement suggestions\n" +
               $"  Last Updated: {LastUpdated:yyyy-MM-dd HH:mm:ss} UTC";
    }

    /// <summary>
    /// Returns a string representation of the template execution summary for debugging and logging.
    /// Provides essential template information in a readable format.
    /// </summary>
    /// <returns>String representation including template context and key metrics</returns>
    public override string ToString()
    {
        return $"TemplateExecutionSummary: {TemplateName} ({TemplateId}) - " +
               $"{TotalExecutions:N0} executions, {SuccessRate:F1}% success, Quality: {QualityScore:F1}/100";
    }
}

/// <summary>
/// Represents usage statistics for a specific variable within a template.
/// Provides detailed metrics about variable utilization patterns and effectiveness.
/// </summary>
public class VariableUsageStats
{
    /// <summary>
    /// Gets or sets the name of the variable.
    /// </summary>
    public string VariableName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of times this variable was used in executions.
    /// </summary>
    public long UsageCount { get; set; }

    /// <summary>
    /// Gets or sets the number of unique values provided for this variable.
    /// </summary>
    public long UniqueValueCount { get; set; }

    /// <summary>
    /// Gets or sets the most frequently used value for this variable.
    /// </summary>
    public string? MostCommonValue { get; set; }

    /// <summary>
    /// Gets or sets the frequency of the most common value as a percentage.
    /// </summary>
    public double MostCommonValueFrequency { get; set; }

    /// <summary>
    /// Gets or sets the average length of values provided for this variable.
    /// </summary>
    public double AverageValueLength { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this variable is required for template execution.
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// Gets or sets the data type of this variable.
    /// </summary>
    public string? DataType { get; set; }
}
