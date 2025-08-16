namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Batch quality metrics
/// </summary>
public class BatchQualityMetrics
{
    /// <summary>
    /// Average quality score across the batch
    /// </summary>
    public double AverageQualityScore { get; set; }

    /// <summary>
    /// Standard deviation of quality scores
    /// </summary>
    public double QualityStandardDeviation { get; set; }

    /// <summary>
    /// Minimum quality score in the batch
    /// </summary>
    public double MinQualityScore { get; set; }

    /// <summary>
    /// Maximum quality score in the batch
    /// </summary>
    public double MaxQualityScore { get; set; }

    /// <summary>
    /// Distribution of quality scores by range
    /// </summary>
    public Dictionary<string, int> QualityDistribution { get; set; } = [];
}
