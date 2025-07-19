namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Quality metrics for analyzing execution performance and results
/// </summary>
public class QualityMetrics
{
    /// <summary>
    /// Average quality score across all items
    /// </summary>
    public double AverageScore { get; set; }

    /// <summary>
    /// Median quality score
    /// </summary>
    public double MedianScore { get; set; }

    /// <summary>
    /// Standard deviation of quality scores
    /// </summary>
    public double StandardDeviation { get; set; }

    /// <summary>
    /// Minimum quality score
    /// </summary>
    public double MinScore { get; set; }

    /// <summary>
    /// Maximum quality score
    /// </summary>
    public double MaxScore { get; set; }

    /// <summary>
    /// Distribution of scores across ranges
    /// </summary>
    public Dictionary<string, int> ScoreDistribution { get; set; } = new();
}