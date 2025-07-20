namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Filter options for template analytics queries
/// </summary>
public class TemplateAnalyticsFilterOptions
{
    /// <summary>
    /// Filter by specific template IDs
    /// </summary>
    public List<Guid>? TemplateIds { get; set; }

    /// <summary>
    /// Filter by template categories
    /// </summary>
    public List<string>? Categories { get; set; }

    /// <summary>
    /// Filter by template authors
    /// </summary>
    public List<Guid>? AuthorIds { get; set; }

    /// <summary>
    /// Filter by template status
    /// </summary>
    public List<string>? Statuses { get; set; }

    /// <summary>
    /// Filter by success rate range
    /// </summary>
    public DoubleRange? SuccessRateRange { get; set; }

    /// <summary>
    /// Filter by usage count range
    /// </summary>
    public LongRange? UsageCountRange { get; set; }

    /// <summary>
    /// Filter by quality score range
    /// </summary>
    public DoubleRange? QualityScoreRange { get; set; }

    /// <summary>
    /// Include archived templates
    /// </summary>
    public bool IncludeArchived { get; set; } = false;

    /// <summary>
    /// Group results by specific dimensions
    /// </summary>
    public List<string>? GroupBy { get; set; }

    /// <summary>
    /// Include performance metrics
    /// </summary>
    public bool IncludePerformanceMetrics { get; set; } = false;

    /// <summary>
    /// Include variable analysis
    /// </summary>
    public bool IncludeVariableAnalysis { get; set; } = false;

    /// <summary>
    /// Custom filters as key-value pairs
    /// </summary>
    public Dictionary<string, object>? CustomFilters { get; set; }
}

/// <summary>
/// Represents a long range filter
/// </summary>
public class LongRange
{
    public long? Min { get; set; }
    public long? Max { get; set; }
}
