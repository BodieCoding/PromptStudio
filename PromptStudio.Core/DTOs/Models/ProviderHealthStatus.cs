using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Provider health status information.
/// </summary>
public class ProviderHealthStatus
{
    /// <summary>
    /// Gets or sets the provider ID.
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Gets or sets the overall health status.
    /// </summary>
    public HealthStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the last health check timestamp.
    /// </summary>
    public DateTime LastCheckAt { get; set; }

    /// <summary>
    /// Gets or sets connectivity status.
    /// </summary>
    public HealthCheckResult? Connectivity { get; set; }

    /// <summary>
    /// Gets or sets availability status.
    /// </summary>
    public HealthCheckResult? Availability { get; set; }

    /// <summary>
    /// Gets or sets performance metrics.
    /// </summary>
    public HealthCheckResult? Performance { get; set; }

    /// <summary>
    /// Gets or sets capacity status.
    /// </summary>
    public HealthCheckResult? Capacity { get; set; }

    /// <summary>
    /// Gets or sets detailed health metrics.
    /// </summary>
    public Dictionary<string, object>? HealthMetrics { get; set; }

    /// <summary>
    /// Gets or sets any health issues or warnings.
    /// </summary>
    public List<HealthIssue>? Issues { get; set; }
}

/// <summary>
/// Health status levels.
/// </summary>
public enum HealthStatus
{
    /// <summary>Provider is healthy and operating normally</summary>
    Healthy = 0,
    /// <summary>Provider has minor issues but is operational</summary>
    Warning = 1,
    /// <summary>Provider has significant issues affecting performance</summary>
    Degraded = 2,
    /// <summary>Provider is unhealthy and not operational</summary>
    Unhealthy = 3,
    /// <summary>Provider status is unknown</summary>
    Unknown = 4
}

/// <summary>
/// Individual health check result.
/// </summary>
public class HealthCheckResult
{
    /// <summary>
    /// Gets or sets the check status.
    /// </summary>
    public HealthStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the response time for this check.
    /// </summary>
    public TimeSpan ResponseTime { get; set; }

    /// <summary>
    /// Gets or sets error message if check failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets additional check details.
    /// </summary>
    public Dictionary<string, object>? Details { get; set; }
}

/// <summary>
/// Health issue information.
/// </summary>
public class HealthIssue
{
    /// <summary>
    /// Gets or sets the issue severity.
    /// </summary>
    public HealthIssueSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the issue description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issue category.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets when the issue was first detected.
    /// </summary>
    public DateTime DetectedAt { get; set; }

    /// <summary>
    /// Gets or sets recommended actions for the issue.
    /// </summary>
    public List<string>? RecommendedActions { get; set; }
}

/// <summary>
/// Health issue severity levels.
/// </summary>
public enum HealthIssueSeverity
{
    /// <summary>Informational issue</summary>
    Info = 0,
    /// <summary>Warning level issue</summary>
    Warning = 1,
    /// <summary>Error level issue</summary>
    Error = 2,
    /// <summary>Critical issue</summary>
    Critical = 3
}

/// <summary>
/// Filter options for provider analytics.
/// </summary>
public class ProviderAnalyticsFilterOptions
{
    /// <summary>
    /// Gets or sets the date range for analytics.
    /// </summary>
    public DateRange? DateRange { get; set; }

    /// <summary>
    /// Gets or sets specific provider IDs to include.
    /// </summary>
    public List<Guid>? ProviderIds { get; set; }

    /// <summary>
    /// Gets or sets metrics to include in the analytics.
    /// </summary>
    public List<string>? IncludeMetrics { get; set; }

    /// <summary>
    /// Gets or sets the granularity of the analytics data.
    /// </summary>
    public AnalyticsGranularity Granularity { get; set; } = AnalyticsGranularity.Daily;
}

/// <summary>
/// Date range specification.
/// </summary>
public class DateRange
{
    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime EndDate { get; set; }
}

/// <summary>
/// Analytics granularity options.
/// </summary>
public enum AnalyticsGranularity
{
    /// <summary>Hourly analytics data</summary>
    Hourly = 0,
    /// <summary>Daily analytics data</summary>
    Daily = 1,
    /// <summary>Weekly analytics data</summary>
    Weekly = 2,
    /// <summary>Monthly analytics data</summary>
    Monthly = 3
}

/// <summary>
/// Provider analytics result.
/// </summary>
public class ProviderAnalyticsResult : OperationResult
{
    /// <summary>
    /// Gets or sets the analytics data for each provider.
    /// </summary>
    public Dictionary<Guid, ProviderAnalyticsData>? ProviderAnalytics { get; set; }

    /// <summary>
    /// Gets or sets aggregate analytics across all providers.
    /// </summary>
    public AggregateAnalyticsData? AggregateData { get; set; }

    /// <summary>
    /// Gets or sets the analytics generation timestamp.
    /// </summary>
    public DateTime GeneratedAt { get; set; }

    /// <summary>
    /// Gets or sets the date range covered by the analytics.
    /// </summary>
    public DateRange? CoveredPeriod { get; set; }
}

/// <summary>
/// Analytics data for a specific provider.
/// </summary>
public class ProviderAnalyticsData
{
    /// <summary>
    /// Gets or sets the provider ID.
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Gets or sets usage metrics.
    /// </summary>
    public Dictionary<string, object>? UsageMetrics { get; set; }

    /// <summary>
    /// Gets or sets performance metrics.
    /// </summary>
    public Dictionary<string, object>? PerformanceMetrics { get; set; }

    /// <summary>
    /// Gets or sets cost metrics.
    /// </summary>
    public Dictionary<string, object>? CostMetrics { get; set; }

    /// <summary>
    /// Gets or sets time-series data points.
    /// </summary>
    public List<AnalyticsDataPoint>? TimeSeriesData { get; set; }
}

/// <summary>
/// Individual analytics data point.
/// </summary>
public class AnalyticsDataPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this data point.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the metrics for this data point.
    /// </summary>
    public Dictionary<string, object>? Metrics { get; set; }
}

/// <summary>
/// Aggregate analytics data across providers.
/// </summary>
public class AggregateAnalyticsData
{
    /// <summary>
    /// Gets or sets total requests across all providers.
    /// </summary>
    public long TotalRequests { get; set; }

    /// <summary>
    /// Gets or sets total cost across all providers.
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Gets or sets average response time across all providers.
    /// </summary>
    public double AverageResponseTime { get; set; }

    /// <summary>
    /// Gets or sets overall success rate.
    /// </summary>
    public double OverallSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets additional aggregate metrics.
    /// </summary>
    public Dictionary<string, object>? AdditionalMetrics { get; set; }
}

/// <summary>
/// Provider optimization result.
/// </summary>
public class ProviderOptimizationResult : OperationResult
{
    /// <summary>
    /// Gets or sets optimization recommendations.
    /// </summary>
    public List<OptimizationRecommendation>? Recommendations { get; set; }

    /// <summary>
    /// Gets or sets current performance analysis.
    /// </summary>
    public PerformanceAnalysis? CurrentPerformance { get; set; }

    /// <summary>
    /// Gets or sets potential improvements.
    /// </summary>
    public Dictionary<string, ImpactEstimate>? PotentialImprovements { get; set; }

    /// <summary>
    /// Gets or sets the analysis timestamp.
    /// </summary>
    public DateTime AnalyzedAt { get; set; }
}

/// <summary>
/// Individual optimization recommendation.
/// </summary>
public class OptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation type.
    /// </summary>
    public RecommendationType Type { get; set; }

    /// <summary>
    /// Gets or sets the recommendation title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority of this recommendation.
    /// </summary>
    public RecommendationPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the estimated impact.
    /// </summary>
    public ImpactEstimate? EstimatedImpact { get; set; }

    /// <summary>
    /// Gets or sets implementation steps.
    /// </summary>
    public List<string>? ImplementationSteps { get; set; }
}

/// <summary>
/// Types of optimization recommendations.
/// </summary>
public enum RecommendationType
{
    /// <summary>Performance optimization</summary>
    Performance = 0,
    /// <summary>Cost optimization</summary>
    Cost = 1,
    /// <summary>Configuration optimization</summary>
    Configuration = 2,
    /// <summary>Capacity optimization</summary>
    Capacity = 3,
    /// <summary>Security optimization</summary>
    Security = 4
}

/// <summary>
/// Impact estimate for recommendations.
/// </summary>
public class ImpactEstimate
{
    /// <summary>
    /// Gets or sets the estimated performance improvement percentage.
    /// </summary>
    public double? PerformanceImprovement { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost reduction percentage.
    /// </summary>
    public double? CostReduction { get; set; }

    /// <summary>
    /// Gets or sets the implementation effort level.
    /// </summary>
    public ImplementationEffort EffortLevel { get; set; }

    /// <summary>
    /// Gets or sets additional impact details.
    /// </summary>
    public Dictionary<string, object>? AdditionalDetails { get; set; }
}

/// <summary>
/// Implementation effort levels.
/// </summary>
public enum ImplementationEffort
{
    /// <summary>Low effort - simple configuration change</summary>
    Low = 0,
    /// <summary>Medium effort - moderate changes required</summary>
    Medium = 1,
    /// <summary>High effort - significant changes required</summary>
    High = 2,
    /// <summary>Very high effort - major restructuring required</summary>
    VeryHigh = 3
}

/// <summary>
/// Current performance analysis.
/// </summary>
public class PerformanceAnalysis
{
    /// <summary>
    /// Gets or sets the current performance rating.
    /// </summary>
    public PerformanceRating CurrentRating { get; set; }

    /// <summary>
    /// Gets or sets identified performance bottlenecks.
    /// </summary>
    public List<PerformanceBottleneck>? Bottlenecks { get; set; }

    /// <summary>
    /// Gets or sets performance trends.
    /// </summary>
    public Dictionary<string, TrendAnalysis>? Trends { get; set; }
}

/// <summary>
/// Performance rating levels.
/// </summary>
public enum PerformanceRating
{
    /// <summary>Excellent performance</summary>
    Excellent = 0,
    /// <summary>Good performance</summary>
    Good = 1,
    /// <summary>Fair performance</summary>
    Fair = 2,
    /// <summary>Poor performance</summary>
    Poor = 3,
    /// <summary>Critical performance issues</summary>
    Critical = 4
}

/// <summary>
/// Performance bottleneck information.
/// </summary>
public class PerformanceBottleneck
{
    /// <summary>
    /// Gets or sets the bottleneck area.
    /// </summary>
    public string Area { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity of the bottleneck.
    /// </summary>
    public BottleneckSeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the bottleneck description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets suggested solutions.
    /// </summary>
    public List<string>? SuggestedSolutions { get; set; }
}

/// <summary>
/// Bottleneck severity levels.
/// </summary>
public enum BottleneckSeverity
{
    /// <summary>Minor bottleneck</summary>
    Minor = 0,
    /// <summary>Moderate bottleneck</summary>
    Moderate = 1,
    /// <summary>Major bottleneck</summary>
    Major = 2,
    /// <summary>Critical bottleneck</summary>
    Critical = 3
}

/// <summary>
/// Trend analysis information.
/// </summary>
public class TrendAnalysis
{
    /// <summary>
    /// Gets or sets the trend direction.
    /// </summary>
    public TrendDirection Direction { get; set; }

    /// <summary>
    /// Gets or sets the trend magnitude.
    /// </summary>
    public double Magnitude { get; set; }

    /// <summary>
    /// Gets or sets trend confidence level.
    /// </summary>
    public double Confidence { get; set; }

    /// <summary>
    /// Gets or sets the trend description.
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Trend direction options.
/// </summary>
public enum TrendDirection
{
    /// <summary>Improving trend</summary>
    Improving = 0,
    /// <summary>Stable trend</summary>
    Stable = 1,
    /// <summary>Declining trend</summary>
    Declining = 2,
    /// <summary>Volatile trend</summary>
    Volatile = 3
}
