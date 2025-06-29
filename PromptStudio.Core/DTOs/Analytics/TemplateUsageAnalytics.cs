namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Usage analytics for a template
/// </summary>
public class TemplateUsageAnalytics
{
    public Guid TemplateId { get; set; }
    public long TotalExecutions { get; set; }
    public long UniqueUsers { get; set; }
    public DateTime FirstUsed { get; set; }
    public DateTime LastUsed { get; set; }
    public double AverageExecutionTime { get; set; }
    public Dictionary<string, long> UsageByPeriod { get; set; } = new();
    public List<string> MostUsedVariables { get; set; } = new();
}
