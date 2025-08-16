namespace PromptStudio.Core.DTOs.Analytics;

public class LlmProviderCostOptimization
{
    public string ProviderId { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
    public decimal CurrentCostPerToken { get; set; }
    public decimal OptimizedCostPerToken { get; set; }
    public string AlternativeProvider { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
}
