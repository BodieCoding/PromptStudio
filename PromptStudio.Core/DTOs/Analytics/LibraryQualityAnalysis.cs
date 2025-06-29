namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Library quality analysis
/// </summary>
public class LibraryQualityAnalysis
{
    public Guid LibraryId { get; set; }
    public double OverallQualityScore { get; set; }
    public Dictionary<string, double> QualityMetrics { get; set; } = new();
    public List<QualityIssue> Issues { get; set; } = new();
    public List<QualityRecommendation> Recommendations { get; set; } = new();
    public Dictionary<string, object> DetailedAnalysis { get; set; } = new();
}
