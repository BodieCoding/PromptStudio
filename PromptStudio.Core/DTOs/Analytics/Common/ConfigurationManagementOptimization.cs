namespace PromptStudio.Core.DTOs.Analytics;

public class ConfigurationManagementOptimization
{
    public string ConfigurationArea { get; set; } = string.Empty;
    public string CurrentApproach { get; set; } = string.Empty;
    public string RecommendedApproach { get; set; } = string.Empty;
    public double EfficiencyGain { get; set; }
}
