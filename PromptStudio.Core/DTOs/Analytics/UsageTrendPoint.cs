namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Usage trend data point
/// </summary>
public class UsageTrendPoint
{
    public DateTime Date { get; set; }
    public long ExecutionCount { get; set; }
    public long UniqueUsers { get; set; }
    public double SuccessRate { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
}
