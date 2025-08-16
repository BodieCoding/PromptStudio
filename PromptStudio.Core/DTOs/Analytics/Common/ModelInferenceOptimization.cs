namespace PromptStudio.Core.DTOs.Analytics;

public class ModelInferenceOptimization
{
    public string ModelId { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public double CurrentLatency { get; set; }
    public double ProjectedLatency { get; set; }
    public decimal CostImpact { get; set; }
    public string RecommendedConfiguration { get; set; } = string.Empty;
}
