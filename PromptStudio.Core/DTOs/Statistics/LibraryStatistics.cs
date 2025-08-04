namespace PromptStudio.Core.DTOs.Statistics;

/// <summary>
/// Represents comprehensive statistical information about a prompt library.
/// Provides insights into library usage, performance, and content metrics for operational monitoring.
/// </summary>
/// <remarks>
/// <para><strong>Analytics Context:</strong></para>
/// <para>Aggregates and summarizes key metrics about library operations including template counts,
/// execution patterns, performance indicators, and user engagement statistics for management
/// reporting and optimization purposes.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>Management dashboards and reporting</description></item>
/// <item><description>Library performance monitoring</description></item>
/// <item><description>Resource planning and optimization</description></item>
/// <item><description>User adoption and engagement analysis</description></item>
/// </list>
/// </remarks>
public class LibraryStatistics
{
    /// <summary>
    /// Gets or sets the unique identifier of the library these statistics represent.
    /// </summary>
    /// <value>
    /// The unique identifier of the associated library.
    /// </value>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the library for identification purposes.
    /// </summary>
    /// <value>
    /// The human-readable name of the library.
    /// </value>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total number of prompt templates in the library.
    /// </summary>
    /// <value>
    /// The count of active prompt templates in the library.
    /// </value>
    public int TotalTemplates { get; set; }

    /// <summary>
    /// Gets or sets the number of templates marked as active.
    /// </summary>
    /// <value>
    /// The count of templates available for use.
    /// </value>
    public int ActiveTemplates { get; set; }

    /// <summary>
    /// Gets or sets the number of templates currently in draft status.
    /// </summary>
    /// <value>
    /// The count of templates being developed or reviewed.
    /// </value>
    public int DraftTemplates { get; set; }

    /// <summary>
    /// Gets or sets the number of templates that are archived.
    /// </summary>
    /// <value>
    /// The count of templates no longer in active use.
    /// </value>
    public int ArchivedTemplates { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions across all templates in the library.
    /// </summary>
    /// <value>
    /// The cumulative execution count for all library templates.
    /// </value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of successful executions.
    /// </summary>
    /// <value>
    /// The count of executions that completed without errors.
    /// </value>
    public long SuccessfulExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of failed executions.
    /// </summary>
    /// <value>
    /// The count of executions that encountered errors.
    /// </value>
    public long FailedExecutions { get; set; }

    /// <summary>
    /// Gets or sets the success rate as a percentage.
    /// </summary>
    /// <value>
    /// The percentage of successful executions (0-100).
    /// </value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution time across all templates.
    /// </summary>
    /// <value>
    /// The mean execution duration for library templates.
    /// </value>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the number of unique users who have executed templates from this library.
    /// </summary>
    /// <value>
    /// The count of distinct users who have used the library.
    /// </value>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the library was created.
    /// </summary>
    /// <value>
    /// The timestamp when the library was established.
    /// </value>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the library was last modified.
    /// </summary>
    /// <value>
    /// The timestamp of the most recent library update.
    /// </value>
    public DateTime LastModified { get; set; }

    /// <summary>
    /// Gets or sets the date and time when statistics were last calculated.
    /// </summary>
    /// <value>
    /// The timestamp when these statistics were generated.
    /// </value>
    public DateTime StatisticsGeneratedAt { get; set; }

    /// <summary>
    /// Gets or sets the time period covered by these statistics.
    /// </summary>
    /// <value>
    /// The date range for which statistics were calculated.
    /// </value>
    public TimeSpan StatisticsPeriod { get; set; }

    /// <summary>
    /// Gets or sets additional metadata and custom metrics.
    /// </summary>
    /// <value>
    /// A dictionary containing extended statistical information.
    /// </value>
    public Dictionary<string, object> ExtendedMetrics { get; set; } = new();

    /// <summary>
    /// Gets the template utilization rate as a percentage.
    /// Calculated as the percentage of templates that have been executed.
    /// </summary>
    /// <value>
    /// The percentage of templates with at least one execution.
    /// </value>
    public double TemplateUtilizationRate
    {
        get
        {
            if (ActiveTemplates == 0) return 0;
            var templatesWithExecutions = ExtendedMetrics.TryGetValue("TemplatesWithExecutions", out var value) 
                ? Convert.ToInt32(value) 
                : 0;
            return (double)templatesWithExecutions / ActiveTemplates * 100;
        }
    }

    /// <summary>
    /// Gets the average executions per template.
    /// </summary>
    /// <value>
    /// The mean number of executions across all active templates.
    /// </value>
    public double AverageExecutionsPerTemplate => ActiveTemplates > 0 ? (double)TotalExecutions / ActiveTemplates : 0;

    /// <summary>
    /// Gets the executions per day over the statistics period.
    /// </summary>
    /// <value>
    /// The daily execution rate for the library.
    /// </value>
    public double ExecutionsPerDay => StatisticsPeriod.TotalDays > 0 ? TotalExecutions / StatisticsPeriod.TotalDays : 0;
}
