namespace PromptStudio.Core.DTOs.Analytics;

public class MemoryOptimization
{
    public double CurrentUtilization { get; set; }
    public double OptimalUtilization { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
    public double ExpectedImprovement { get; set; }
}
