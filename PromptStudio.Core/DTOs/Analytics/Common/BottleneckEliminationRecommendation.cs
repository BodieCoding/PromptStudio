namespace PromptStudio.Core.DTOs.Analytics;

public class BottleneckEliminationRecommendation
{
    public string BottleneckLocation { get; set; } = string.Empty;
    public string BottleneckType { get; set; } = string.Empty;
    public string RecommendedSolution { get; set; } = string.Empty;
    public double ExpectedImprovement { get; set; }
}
