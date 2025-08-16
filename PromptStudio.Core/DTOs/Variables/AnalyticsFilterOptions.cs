using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Filter options for analytics queries
/// </summary>
public class AnalyticsFilterOptions
{
    /// <summary>
    /// Gets or sets the start date for analytics period
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for analytics period
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the template ID to filter analytics by
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the variable ID to filter analytics by
    /// </summary>
    public Guid? VariableId { get; set; }

    /// <summary>
    /// Gets or sets the aggregation interval (hour, day, week, month)
    /// Default is "day"
    /// </summary>
    public string AggregationInterval { get; set; } = "day";

    /// <summary>
    /// Gets or sets specific metrics to calculate
    /// </summary>
    public List<string> Metrics { get; set; } = [];

    /// <summary>
    /// Gets or sets tags to filter analytics by
    /// </summary>
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// Gets or sets whether to include detailed breakdowns
    /// </summary>
    public bool IncludeDetailedBreakdown { get; set; } = false;

    /// <summary>
    /// Gets or sets whether to include usage patterns
    /// </summary>
    public bool IncludeUsagePatterns { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to include performance metrics
    /// </summary>
    public bool IncludePerformanceMetrics { get; set; } = true;

    /// <summary>
    /// Gets or sets the minimum sample size required for meaningful analytics
    /// Default is 10
    /// </summary>
    [Range(1, int.MaxValue)]
    public int MinimumSampleSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets the page number for pagination
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size for pagination
    /// </summary>
    [Range(1, 1000)]
    public int PageSize { get; set; } = 50;
}
