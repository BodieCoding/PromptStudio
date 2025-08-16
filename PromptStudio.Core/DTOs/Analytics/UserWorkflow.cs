namespace PromptStudio.Core.DTOs.Analytics;

public class UserWorkflow
{
    public string WorkflowName { get; set; } = string.Empty;
    public List<string> Steps { get; set; } = [];
    public long Usage { get; set; }
    public double SuccessRate { get; set; }
}
