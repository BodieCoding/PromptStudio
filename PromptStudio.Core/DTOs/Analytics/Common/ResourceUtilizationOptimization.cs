namespace PromptStudio.Core.DTOs.Analytics;

// Resource and Infrastructure Optimization Classes
public class ResourceUtilizationOptimization
{
    public string ResourceId { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
    public double CurrentUtilization { get; set; }
    public double OptimalUtilization { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
}
