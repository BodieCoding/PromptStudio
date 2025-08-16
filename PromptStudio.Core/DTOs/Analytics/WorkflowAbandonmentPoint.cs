namespace PromptStudio.Core.DTOs.Analytics;

public class WorkflowAbandonmentPoint
{
    public string Step { get; set; } = string.Empty;
    public double AbandonmentRate { get; set; }
    public List<string> CommonReasons { get; set; } = [];
}
