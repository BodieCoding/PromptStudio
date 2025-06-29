namespace PromptStudio.Core.DTOs.Analytics;

public class ModelPerformanceMetrics
{
    public string ModelName { get; set; } = string.Empty;
    public long ExecutionCount { get; set; }
    public double SuccessRate { get; set; }
    public TimeSpan AverageResponseTime { get; set; }
    public long TotalTokenUsage { get; set; }
}
