namespace PromptStudio.Core.DTOs.Analytics;

public class ComputeCostOptimization
{
    public string ComputeResource { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal CurrentCost { get; set; }
    public decimal OptimizedCost { get; set; }
}
