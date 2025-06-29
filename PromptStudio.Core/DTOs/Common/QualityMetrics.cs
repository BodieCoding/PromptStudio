namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Quality metrics
/// </summary>
public class QualityMetrics
{
    public double AverageScore { get; set; }
    public double MedianScore { get; set; }
    public double StandardDeviation { get; set; }
    public double MinScore { get; set; }
    public double MaxScore { get; set; }
    public Dictionary<string, int> ScoreDistribution { get; set; } = new();
}
