namespace PromptStudio.Core.DTOs.Analytics;

public class AutoScalingOptimization
{
    public string ResourceType { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal ExpectedSavings { get; set; }
}
