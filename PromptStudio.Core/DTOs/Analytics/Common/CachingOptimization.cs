namespace PromptStudio.Core.DTOs.Analytics;

public class CachingOptimization
{
    public string CacheType { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public double CurrentHitRate { get; set; }
    public double ProjectedHitRate { get; set; }
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal ExpectedSavings { get; set; }
}
