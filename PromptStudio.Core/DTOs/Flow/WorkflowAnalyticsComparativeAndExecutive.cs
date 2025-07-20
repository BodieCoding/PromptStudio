using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents comparative analysis results against benchmarks and previous periods.
/// </summary>
public class ComparativeAnalysisResult
{
    /// <summary>
    /// Gets or sets comparison with previous time periods.
    /// </summary>
    /// <value>Analysis comparing current period with historical periods.</value>
    public HistoricalComparison? HistoricalComparison { get; set; }

    /// <summary>
    /// Gets or sets comparison with industry benchmarks.
    /// </summary>
    /// <value>Analysis comparing performance against industry standards.</value>
    public IndustryBenchmarkComparison? IndustryBenchmarkComparison { get; set; }

    /// <summary>
    /// Gets or sets comparison with internal targets.
    /// </summary>
    /// <value>Analysis comparing performance against internal goals and targets.</value>
    public InternalTargetComparison? InternalTargetComparison { get; set; }

    /// <summary>
    /// Gets or sets peer comparison analysis.
    /// </summary>
    /// <value>Analysis comparing performance with peer organizations or systems.</value>
    public PeerComparison? PeerComparison { get; set; }

    /// <summary>
    /// Gets or sets year-over-year comparison.
    /// </summary>
    /// <value>Analysis comparing current performance with same period last year.</value>
    public YearOverYearComparison? YearOverYearComparison { get; set; }
}

/// <summary>
/// Represents historical comparison analysis.
/// </summary>
public class HistoricalComparison
{
    /// <summary>
    /// Gets or sets the comparison periods analyzed.
    /// </summary>
    /// <value>List of time periods included in the historical comparison.</value>
    public List<ComparisonPeriod>? ComparisonPeriods { get; set; }

    /// <summary>
    /// Gets or sets performance trends over time.
    /// </summary>
    /// <value>Trends showing how performance has changed over the analyzed periods.</value>
    public List<HistoricalTrendPoint>? PerformanceTrends { get; set; }

    /// <summary>
    /// Gets or sets key performance changes identified.
    /// </summary>
    /// <value>Significant changes in performance metrics over time.</value>
    public List<PerformanceChange>? KeyPerformanceChanges { get; set; }

    /// <summary>
    /// Gets or sets the overall historical trend direction.
    /// </summary>
    /// <value>Overall direction of performance change over the historical period.</value>
    public PerformanceTrend OverallTrend { get; set; }
}

/// <summary>
/// Represents a comparison period for temporal analysis.
/// </summary>
public class ComparisonPeriod
{
    /// <summary>
    /// Gets or sets the period identifier.
    /// </summary>
    /// <value>Unique identifier for this comparison period.</value>
    public string PeriodId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the period name for identification.
    /// </summary>
    /// <value>Human-readable name for this period (e.g., "Q1 2024", "Last Month").</value>
    public string PeriodName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the period.
    /// </summary>
    /// <value>Start date and time of this comparison period.</value>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the period.
    /// </summary>
    /// <value>End date and time of this comparison period.</value>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the key metrics for this period.
    /// </summary>
    /// <value>Dictionary of metric names and their values for this period.</value>
    public Dictionary<string, double>? KeyMetrics { get; set; }
}

/// <summary>
/// Represents a historical trend point for trend analysis.
/// </summary>
public class HistoricalTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this trend point.
    /// </summary>
    /// <value>Date and time for this historical data point.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the metric values at this point.
    /// </summary>
    /// <value>Dictionary of metric names and their values at this time point.</value>
    public Dictionary<string, double>? MetricValues { get; set; }

    /// <summary>
    /// Gets or sets the period-over-period change percentages.
    /// </summary>
    /// <value>Percentage changes from the previous period for each metric.</value>
    public Dictionary<string, double>? PercentageChanges { get; set; }
}

/// <summary>
/// Represents a significant performance change identified in historical analysis.
/// </summary>
public class PerformanceChange
{
    /// <summary>
    /// Gets or sets the change identifier.
    /// </summary>
    /// <value>Unique identifier for this performance change.</value>
    public string ChangeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the metric that changed.
    /// </summary>
    /// <value>Name of the metric that experienced the change.</value>
    public string MetricName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets when the change occurred.
    /// </summary>
    /// <value>Date and time when the significant change was detected.</value>
    public DateTime ChangeDate { get; set; }

    /// <summary>
    /// Gets or sets the magnitude of the change.
    /// </summary>
    /// <value>Percentage change in the metric value.</value>
    public double ChangeMagnitude { get; set; }

    /// <summary>
    /// Gets or sets the change description.
    /// </summary>
    /// <value>Human-readable description of what changed and its significance.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the potential impact of this change.
    /// </summary>
    /// <value>Assessment of how this change affects overall performance.</value>
    public string ImpactAssessment { get; set; } = string.Empty;
}

/// <summary>
/// Represents comparison with industry benchmarks.
/// </summary>
public class IndustryBenchmarkComparison
{
    /// <summary>
    /// Gets or sets the industry benchmark data.
    /// </summary>
    /// <value>Dictionary of metrics and their industry benchmark values.</value>
    public Dictionary<string, double>? IndustryBenchmarks { get; set; }

    /// <summary>
    /// Gets or sets the current performance against benchmarks.
    /// </summary>
    /// <value>Dictionary showing how current metrics compare to industry standards.</value>
    public Dictionary<string, BenchmarkComparison>? BenchmarkComparisons { get; set; }

    /// <summary>
    /// Gets or sets the overall benchmark performance score.
    /// </summary>
    /// <value>Overall score (0-100) comparing performance to industry benchmarks.</value>
    public double OverallBenchmarkScore { get; set; }

    /// <summary>
    /// Gets or sets metrics where performance exceeds benchmarks.
    /// </summary>
    /// <value>List of metrics where current performance is above industry standards.</value>
    public List<string>? MetricsAboveBenchmark { get; set; }

    /// <summary>
    /// Gets or sets metrics where performance is below benchmarks.
    /// </summary>
    /// <value>List of metrics where current performance is below industry standards.</value>
    public List<string>? MetricsBelowBenchmark { get; set; }
}

/// <summary>
/// Represents a benchmark comparison for a specific metric.
/// </summary>
public class BenchmarkComparison
{
    /// <summary>
    /// Gets or sets the metric name being compared.
    /// </summary>
    /// <value>Name of the metric in the benchmark comparison.</value>
    public string MetricName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current value for this metric.
    /// </summary>
    /// <value>Current performance value for this metric.</value>
    public double CurrentValue { get; set; }

    /// <summary>
    /// Gets or sets the industry benchmark value.
    /// </summary>
    /// <value>Industry standard or benchmark value for this metric.</value>
    public double BenchmarkValue { get; set; }

    /// <summary>
    /// Gets or sets the percentage difference from benchmark.
    /// </summary>
    /// <value>Percentage difference between current value and benchmark (positive = above benchmark).</value>
    public double PercentageDifference { get; set; }

    /// <summary>
    /// Gets or sets the performance ranking.
    /// </summary>
    /// <value>Ranking description (e.g., "Top 25%", "Below Average").</value>
    public string PerformanceRanking { get; set; } = string.Empty;
}

/// <summary>
/// Represents comparison with internal targets and goals.
/// </summary>
public class InternalTargetComparison
{
    /// <summary>
    /// Gets or sets the target achievement results.
    /// </summary>
    /// <value>Dictionary of targets and their achievement status.</value>
    public Dictionary<string, TargetAchievement>? TargetAchievements { get; set; }

    /// <summary>
    /// Gets or sets the overall target achievement score.
    /// </summary>
    /// <value>Overall score (0-100) representing target achievement across all metrics.</value>
    public double OverallAchievementScore { get; set; }

    /// <summary>
    /// Gets or sets targets that were exceeded.
    /// </summary>
    /// <value>List of targets that were surpassed during the analysis period.</value>
    public List<string>? TargetsExceeded { get; set; }

    /// <summary>
    /// Gets or sets targets that were missed.
    /// </summary>
    /// <value>List of targets that were not achieved during the analysis period.</value>
    public List<string>? TargetsMissed { get; set; }

    /// <summary>
    /// Gets or sets improvement recommendations based on target analysis.
    /// </summary>
    /// <value>Recommendations for achieving missed targets and maintaining exceeded ones.</value>
    public List<string>? ImprovementRecommendations { get; set; }
}

/// <summary>
/// Represents achievement status for a specific target.
/// </summary>
public class TargetAchievement
{
    /// <summary>
    /// Gets or sets the target name.
    /// </summary>
    /// <value>Name or description of the target being measured.</value>
    public string TargetName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target value.
    /// </summary>
    /// <value>The goal or target value that was set.</value>
    public double TargetValue { get; set; }

    /// <summary>
    /// Gets or sets the actual achieved value.
    /// </summary>
    /// <value>The actual value achieved during the measurement period.</value>
    public double ActualValue { get; set; }

    /// <summary>
    /// Gets or sets the achievement percentage.
    /// </summary>
    /// <value>Percentage of target achieved (100% = fully achieved).</value>
    public double AchievementPercentage { get; set; }

    /// <summary>
    /// Gets or sets whether the target was achieved.
    /// </summary>
    /// <value>Boolean indicating if the target was met or exceeded.</value>
    public bool IsAchieved { get; set; }

    /// <summary>
    /// Gets or sets the variance from target.
    /// </summary>
    /// <value>Difference between actual and target values.</value>
    public double VarianceFromTarget { get; set; }
}

/// <summary>
/// Represents peer comparison analysis.
/// </summary>
public class PeerComparison
{
    /// <summary>
    /// Gets or sets the peer organizations or systems included in comparison.
    /// </summary>
    /// <value>List of peer identifiers included in the comparison analysis.</value>
    public List<string>? PeerIdentifiers { get; set; }

    /// <summary>
    /// Gets or sets performance metrics comparison with peers.
    /// </summary>
    /// <value>Dictionary of metrics showing comparison with peer averages.</value>
    public Dictionary<string, PeerMetricComparison>? MetricComparisons { get; set; }

    /// <summary>
    /// Gets or sets the overall peer ranking.
    /// </summary>
    /// <value>Ranking position among peers (1 = best performing).</value>
    public int OverallPeerRanking { get; set; }

    /// <summary>
    /// Gets or sets metrics where performance leads peers.
    /// </summary>
    /// <value>List of metrics where performance exceeds peer averages.</value>
    public List<string>? LeadingMetrics { get; set; }

    /// <summary>
    /// Gets or sets metrics where performance lags peers.
    /// </summary>
    /// <value>List of metrics where performance is below peer averages.</value>
    public List<string>? LaggingMetrics { get; set; }
}

/// <summary>
/// Represents a metric comparison with peers.
/// </summary>
public class PeerMetricComparison
{
    /// <summary>
    /// Gets or sets the metric name.
    /// </summary>
    /// <value>Name of the metric being compared with peers.</value>
    public string MetricName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current organization's value.
    /// </summary>
    /// <value>Current performance value for this metric.</value>
    public double CurrentValue { get; set; }

    /// <summary>
    /// Gets or sets the peer average value.
    /// </summary>
    /// <value>Average value among peer organizations for this metric.</value>
    public double PeerAverage { get; set; }

    /// <summary>
    /// Gets or sets the ranking among peers.
    /// </summary>
    /// <value>Ranking position for this metric among all peers.</value>
    public int PeerRanking { get; set; }

    /// <summary>
    /// Gets or sets the percentile ranking.
    /// </summary>
    /// <value>Percentile ranking (0-100) showing relative position among peers.</value>
    public double PercentileRanking { get; set; }
}

/// <summary>
/// Represents year-over-year comparison analysis.
/// </summary>
public class YearOverYearComparison
{
    /// <summary>
    /// Gets or sets year-over-year changes for key metrics.
    /// </summary>
    /// <value>Dictionary of metrics and their year-over-year change data.</value>
    public Dictionary<string, YearOverYearChange>? MetricChanges { get; set; }

    /// <summary>
    /// Gets or sets the overall year-over-year performance trend.
    /// </summary>
    /// <value>Overall trend direction comparing to the same period last year.</value>
    public PerformanceTrend OverallYearOverYearTrend { get; set; }

    /// <summary>
    /// Gets or sets metrics that improved year-over-year.
    /// </summary>
    /// <value>List of metrics showing improvement compared to last year.</value>
    public List<string>? ImprovedMetrics { get; set; }

    /// <summary>
    /// Gets or sets metrics that declined year-over-year.
    /// </summary>
    /// <value>List of metrics showing decline compared to last year.</value>
    public List<string>? DeclinedMetrics { get; set; }

    /// <summary>
    /// Gets or sets seasonal adjustments applied to the comparison.
    /// </summary>
    /// <value>Information about seasonal factors considered in the year-over-year analysis.</value>
    public string? SeasonalAdjustments { get; set; }
}

/// <summary>
/// Represents a year-over-year change for a specific metric.
/// </summary>
public class YearOverYearChange
{
    /// <summary>
    /// Gets or sets the metric name.
    /// </summary>
    /// <value>Name of the metric with year-over-year change.</value>
    public string MetricName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current year value.
    /// </summary>
    /// <value>Value of the metric in the current period.</value>
    public double CurrentYearValue { get; set; }

    /// <summary>
    /// Gets or sets the previous year value.
    /// </summary>
    /// <value>Value of the metric in the same period last year.</value>
    public double PreviousYearValue { get; set; }

    /// <summary>
    /// Gets or sets the absolute change from last year.
    /// </summary>
    /// <value>Absolute difference between current and previous year values.</value>
    public double AbsoluteChange { get; set; }

    /// <summary>
    /// Gets or sets the percentage change from last year.
    /// </summary>
    /// <value>Percentage change from previous year (positive = improvement).</value>
    public double PercentageChange { get; set; }

    /// <summary>
    /// Gets or sets the change significance level.
    /// </summary>
    /// <value>Significance level of the change (Significant, Moderate, Minor).</value>
    public string ChangeSignificance { get; set; } = string.Empty;
}

/// <summary>
/// Represents an executive insight for high-level reporting.
/// </summary>
public class ExecutiveInsight
{
    /// <summary>
    /// Gets or sets the insight identifier.
    /// </summary>
    /// <value>Unique identifier for this executive insight.</value>
    public string InsightId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the insight title for executive consumption.
    /// </summary>
    /// <value>Brief, executive-appropriate title for this insight.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the executive summary of the insight.
    /// </summary>
    /// <value>High-level summary suitable for executive consumption.</value>
    public string ExecutiveSummary { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the business impact of this insight.
    /// </summary>
    /// <value>Description of the business impact and implications.</value>
    public string BusinessImpact { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets recommended executive actions.
    /// </summary>
    /// <value>Specific actions recommended for executive consideration.</value>
    public List<string>? RecommendedActions { get; set; }

    /// <summary>
    /// Gets or sets the confidence level in this insight.
    /// </summary>
    /// <value>Confidence percentage (0-100) in the accuracy of this insight.</value>
    public double ConfidenceLevel { get; set; }

    /// <summary>
    /// Gets or sets the priority level for executive attention.
    /// </summary>
    /// <value>Priority level indicating urgency of executive attention required.</value>
    public RecommendationPriority Priority { get; set; }
}

/// <summary>
/// Represents a critical alert requiring immediate executive attention.
/// </summary>
public class CriticalAlert
{
    /// <summary>
    /// Gets or sets the alert identifier.
    /// </summary>
    /// <value>Unique identifier for this critical alert.</value>
    public string AlertId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the alert title.
    /// </summary>
    /// <value>Brief title describing the critical issue.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level of the alert.
    /// </summary>
    /// <value>Severity level (Critical, High, Medium) of the alert.</value>
    public string SeverityLevel { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the alert description.
    /// </summary>
    /// <value>Detailed description of the critical issue.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets when the alert was triggered.
    /// </summary>
    /// <value>Date and time when the critical condition was detected.</value>
    public DateTime TriggeredAt { get; set; }

    /// <summary>
    /// Gets or sets immediate actions required.
    /// </summary>
    /// <value>List of immediate actions that should be taken to address the alert.</value>
    public List<string>? ImmediateActions { get; set; }

    /// <summary>
    /// Gets or sets the potential business impact.
    /// </summary>
    /// <value>Assessment of the potential impact on business operations.</value>
    public string PotentialImpact { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the escalation path for this alert.
    /// </summary>
    /// <value>Information about who should be notified and when.</value>
    public string EscalationPath { get; set; } = string.Empty;
}

/// <summary>
/// Represents dimensional metrics for multi-dimensional analysis.
/// </summary>
public class DimensionalMetrics
{
    /// <summary>
    /// Gets or sets the dimension value name.
    /// </summary>
    /// <value>Name or identifier of this dimensional value.</value>
    public string DimensionValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the execution count for this dimension value.
    /// </summary>
    /// <value>Number of executions associated with this dimensional value.</value>
    public long ExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the success rate for this dimension value.
    /// </summary>
    /// <value>Success rate percentage (0-100) for this dimensional value.</value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution duration for this dimension value.
    /// </summary>
    /// <value>Average execution time for this dimensional value.</value>
    public TimeSpan AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets additional custom metrics for this dimension value.
    /// </summary>
    /// <value>Dictionary of additional metrics associated with this dimensional value.</value>
    public Dictionary<string, double>? CustomMetrics { get; set; }
}

/// <summary>
/// Represents a dimensional performance entry for dimensional analysis.
/// </summary>
public class DimensionalPerformanceEntry
{
    /// <summary>
    /// Gets or sets the dimension value.
    /// </summary>
    /// <value>The specific value within the dimension being analyzed.</value>
    public string DimensionValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the performance score for this dimensional value.
    /// </summary>
    /// <value>Calculated performance score (0-100) for this dimensional value.</value>
    public double PerformanceScore { get; set; }

    /// <summary>
    /// Gets or sets the ranking among all values in this dimension.
    /// </summary>
    /// <value>Ranking position (1 = best) among all values in this dimension.</value>
    public int Ranking { get; set; }

    /// <summary>
    /// Gets or sets key performance indicators for this dimensional value.
    /// </summary>
    /// <value>Dictionary of KPIs and their values for this dimensional value.</value>
    public Dictionary<string, double>? KeyPerformanceIndicators { get; set; }
}
