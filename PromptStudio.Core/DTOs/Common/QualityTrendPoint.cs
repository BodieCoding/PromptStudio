namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Quality trend data point for time-series analysis
/// </summary>
public class QualityTrendPoint
{
    /// <summary>
    /// Timestamp for this quality data point
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Average quality score for this time period
    /// </summary>
    public double AverageQualityScore { get; set; }

    /// <summary>
    /// Median quality score for this time period
    /// </summary>
    public double MedianQualityScore { get; set; }

    /// <summary>
    /// Number of scored executions in this time period
    /// </summary>
    public long ScoredExecutions { get; set; }

    /// <summary>
    /// Quality score variance from previous period
    /// </summary>
    public double? QualityVariance { get; set; }

    /// <summary>
    /// Percentage change from previous period
    /// </summary>
    public double? PercentageChange { get; set; }

    /// <summary>
    /// Quality score distribution statistics
    /// </summary>
    public Dictionary<string, double>? ScoreDistribution { get; set; }
}
