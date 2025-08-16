namespace PromptStudio.Core.DTOs.Analytics;

public class LoadBalancingOptimization
{
    public string LoadBalancerType { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public double ExpectedPerformanceGain { get; set; }
}
