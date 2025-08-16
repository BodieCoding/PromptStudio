namespace PromptStudio.Core.DTOs.Analytics;

public class ParallelProcessingRecommendation
{
    public string ProcessName { get; set; } = string.Empty;
    public string CurrentImplementation { get; set; } = string.Empty;
    public string RecommendedApproach { get; set; } = string.Empty;
    public double ExpectedSpeedup { get; set; }
}
