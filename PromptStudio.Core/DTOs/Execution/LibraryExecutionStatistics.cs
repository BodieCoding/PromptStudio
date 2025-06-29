using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
///  Library execution statistics
/// </summary>
public class LibraryExecutionStatistics
{
    public long TotalExecutions { get; set; }
    public int ExecutedTemplatesCount { get; set; }
    public int TotalTemplatesCount { get; set; }
    public double AverageExecutionsPerTemplate { get; set; }
    public DateTime? LastExecution { get; set; }
    public List<TemplateExecutionSummary> TemplateStatistics { get; set; } = new();
    public Dictionary<string, long> ExecutionsByModel { get; set; } = new();
    public Dictionary<string, double> SuccessRatesByModel { get; set; } = new();
    public TokenUsageInfo? TotalTokenUsage { get; set; }
}
