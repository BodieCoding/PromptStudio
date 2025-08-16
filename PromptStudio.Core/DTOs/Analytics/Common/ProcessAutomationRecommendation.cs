namespace PromptStudio.Core.DTOs.Analytics;

// Process and Workflow Optimization Classes
public class ProcessAutomationRecommendation
{
    public string ProcessName { get; set; } = string.Empty;
    public string CurrentState { get; set; } = string.Empty;
    public string RecommendedAutomation { get; set; } = string.Empty;
    public double TimesSavings { get; set; }
    public decimal CostSavings { get; set; }
}
