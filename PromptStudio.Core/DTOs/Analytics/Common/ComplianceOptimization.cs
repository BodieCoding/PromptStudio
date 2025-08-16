namespace PromptStudio.Core.DTOs.Analytics;

public class ComplianceOptimization
{
    public string ComplianceFramework { get; set; } = string.Empty;
    public string CurrentCompliance { get; set; } = string.Empty;
    public string RecommendedActions { get; set; } = string.Empty;
    public string ExpectedOutcome { get; set; } = string.Empty;
}
