namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Statistics for a prompt library
/// </summary>
public class LibraryStatistics
{
    public long TemplateCount { get; set; }
    public long TotalExecutions { get; set; }
    public long UniqueVariableCount { get; set; }
    public DateTime? LastTemplateCreated { get; set; }
    public DateTime? LastExecution { get; set; }
    public double AverageExecutionsPerTemplate { get; set; }
    public long TotalContentSize { get; set; }
    public double SuccessRate { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
    public TokenUsageInfo? TotalTokenUsage { get; set; }
    public Dictionary<string, long> TemplatesByCategory { get; set; } = new();
    public Dictionary<string, long> ExecutionsByModel { get; set; } = new();
    public List<string> MostUsedVariables { get; set; } = new();
    public QualityMetrics? QualityMetrics { get; set; }
}
