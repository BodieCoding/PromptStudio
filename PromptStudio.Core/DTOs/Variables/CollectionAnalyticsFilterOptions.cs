using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Filter options for collection analytics queries
/// </summary>
public class CollectionAnalyticsFilterOptions
{
    /// <summary>
    /// Gets or sets the collection ID to analyze
    /// </summary>
    public Guid? CollectionId { get; set; }

    /// <summary>
    /// Gets or sets the prompt template ID to filter analytics by
    /// </summary>
    public Guid? PromptTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the lab ID for multi-tenancy filtering
    /// </summary>
    public Guid? LabId { get; set; }

    /// <summary>
    /// Gets or sets the start date for analytics period
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for analytics period
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the aggregation interval (hour, day, week, month)
    /// Default is "day"
    /// </summary>
    public string AggregationInterval { get; set; } = "day";

    /// <summary>
    /// Gets or sets whether to include detailed breakdowns
    /// </summary>
    public bool IncludeDetailedBreakdown { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to include performance trends
    /// </summary>
    public bool IncludePerformanceTrends { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to include error analysis
    /// </summary>
    public bool IncludeErrorAnalysis { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to include usage patterns analysis
    /// </summary>
    public bool IncludeUsagePatterns { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to generate insights and recommendations
    /// </summary>
    public bool GenerateInsights { get; set; } = true;

    /// <summary>
    /// Gets or sets specific metrics to calculate
    /// </summary>
    public List<string> Metrics { get; set; } = [];

    /// <summary>
    /// Gets or sets tags to filter analytics by
    /// </summary>
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// Gets or sets the minimum sample size required for meaningful analytics
    /// Default is 10
    /// </summary>
    [Range(1, int.MaxValue)]
    public int MinimumSampleSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets confidence level for statistical calculations (0.0 to 1.0)
    /// Default is 0.95 (95% confidence)
    /// </summary>
    [Range(0.0, 1.0)]
    public double ConfidenceLevel { get; set; } = 0.95;
}
