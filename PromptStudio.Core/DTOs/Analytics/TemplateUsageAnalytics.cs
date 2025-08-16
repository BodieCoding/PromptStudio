namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents comprehensive usage analytics for a template, providing insights into adoption, performance, and user patterns.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary analytics DTO for template usage monitoring, adoption tracking, and optimization services.
/// Used by analytics dashboards, template recommendation engines, and governance services for usage-based decisions.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Aggregated usage data with time-series information and variable usage patterns.
/// Optimized for efficient serialization while maintaining rich analytical detail for template insights.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Template popularity and adoption tracking</item>
/// <item>Usage trend analysis and forecasting</item>
/// <item>Variable optimization recommendations</item>
/// <item>Template lifecycle management decisions</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Contains time-series data that may require pagination for long-running templates.
/// Dictionary collections are optimized for JSON serialization. Consider caching for dashboard scenarios
/// where template analytics are frequently accessed.</para>
/// </remarks>
public class TemplateUsageAnalytics
{
    /// <summary>
    /// Gets or sets the unique identifier of the template being analyzed.
    /// </summary>
    /// <value>A valid GUID representing the template entity.</value>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions across all time periods.
    /// </summary>
    /// <value>A non-negative integer representing the cumulative execution count.</value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the count of unique users who have executed this template.
    /// </summary>
    /// <value>A non-negative integer representing distinct user count based on user identification.</value>
    public long UniqueUsers { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the first recorded usage of this template.
    /// </summary>
    /// <value>A DateTime representing the initial adoption date.</value>
    public DateTime FirstUsed { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the most recent usage of this template.
    /// </summary>
    /// <value>A DateTime representing the latest execution time.</value>
    public DateTime LastUsed { get; set; }

    /// <summary>
    /// Gets or sets the average execution time across all template runs.
    /// </summary>
    /// <value>A decimal representing the mean execution time in seconds.</value>
    public double AverageExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the usage statistics grouped by time period.
    /// </summary>
    /// <value>A dictionary mapping period identifiers (e.g., "2024-01", "week-1") to execution counts.</value>
    public Dictionary<string, long> UsageByPeriod { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of most frequently used variables in this template.
    /// </summary>
    /// <value>A list of variable names ordered by usage frequency, providing optimization insights.</value>
    public List<string> MostUsedVariables { get; set; } = [];
}
