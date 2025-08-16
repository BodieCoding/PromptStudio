namespace PromptStudio.Core.DTOs.Analytics;

public class WorkflowEfficiencyMetrics
{
    public double AverageCompletionTimeMinutes { get; set; }
    public double SuccessRate { get; set; }
    public int AverageSteps { get; set; }
}
