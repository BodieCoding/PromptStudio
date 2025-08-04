namespace PromptStudio.Core.DTOs.Statistics;

/// <summary>
/// Placeholder DTO for execution trend analysis
/// </summary>
public class ExecutionTrend
{
    public DateTime Period { get; set; }
    public int ExecutionCount { get; set; }
    public int UniqueUsers { get; set; }
    public double SuccessRate { get; set; }
}
