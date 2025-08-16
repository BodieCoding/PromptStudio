namespace PromptStudio.Core.DTOs.Analytics;

public class ApiDesignOptimization
{
    public string ApiName { get; set; } = string.Empty;
    public string CurrentDesign { get; set; } = string.Empty;
    public string RecommendedDesign { get; set; } = string.Empty;
    public double EfficiencyGain { get; set; }
}
