namespace PromptStudio.Core.DTOs.Analytics;

public class IntegrationOptimization
{
    public string IntegrationType { get; set; } = string.Empty;
    public string CurrentImplementation { get; set; } = string.Empty;
    public string RecommendedApproach { get; set; } = string.Empty;
    public double ExpectedPerformanceGain { get; set; }
}
