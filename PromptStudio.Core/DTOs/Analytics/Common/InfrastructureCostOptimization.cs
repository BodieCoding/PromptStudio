namespace PromptStudio.Core.DTOs.Analytics;

// Cost Optimization Classes
public class InfrastructureCostOptimization
{
    public string InfrastructureComponent { get; set; } = string.Empty;
    public decimal CurrentCost { get; set; }
    public decimal OptimizedCost { get; set; }
    public decimal PotentialSavings { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}
