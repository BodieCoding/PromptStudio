namespace PromptStudio.Core.DTOs.Analytics;

public class WorkflowSimplificationRecommendation
{
    public string WorkflowName { get; set; } = string.Empty;
    public int CurrentSteps { get; set; }
    public int RecommendedSteps { get; set; }
    public string SimplificationActions { get; set; } = string.Empty;
    public double EfficiencyGain { get; set; }
}
