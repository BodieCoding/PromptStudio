namespace PromptStudio.Core.DTOs.Analytics;

public class NetworkCostOptimization
{
    public string NetworkComponent { get; set; } = string.Empty;
    public decimal DataTransferCost { get; set; }
    public decimal OptimizedDataTransferCost { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}
