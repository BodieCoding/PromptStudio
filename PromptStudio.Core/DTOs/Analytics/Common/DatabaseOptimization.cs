namespace PromptStudio.Core.DTOs.Analytics;

public class DatabaseOptimization
{
    public string DatabaseType { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public string RecommendedIndexes { get; set; } = string.Empty;
    public string QueryOptimizations { get; set; } = string.Empty;
    public double ExpectedPerformanceGain { get; set; }
}
