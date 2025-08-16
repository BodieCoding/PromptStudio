namespace PromptStudio.Core.DTOs.Analytics;

public class DeploymentOptimization
{
    public string DeploymentProcess { get; set; } = string.Empty;
    public string CurrentApproach { get; set; } = string.Empty;
    public string RecommendedApproach { get; set; } = string.Empty;
    public double DeploymentTimeReduction { get; set; }
}
