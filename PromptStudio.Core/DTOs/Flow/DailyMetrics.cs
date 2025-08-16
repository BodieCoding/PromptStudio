namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Daily execution metrics
/// </summary>
public class DailyMetrics
{
    public DateTime Date { get; set; }
    public long Executions { get; set; }
    public decimal Cost { get; set; }
    public double AverageTime { get; set; }
    public decimal SuccessRate { get; set; }
}
