using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Enhanced execution statistics
/// </summary>
public class EnhancedExecutionStatistics
{
    public long TotalExecutions { get; set; }
    public long SuccessfulExecutions { get; set; }
    public long FailedExecutions { get; set; }
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;
    public DateTime? LastExecution { get; set; }
    public DateTime? FirstExecution { get; set; }
    public double AverageExecutionsPerDay { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
    public TimeSpan MedianExecutionTime { get; set; }
    public TimeSpan P95ExecutionTime { get; set; }
    public TokenUsageInfo? TotalTokenUsage { get; set; }
    public Dictionary<string, long> VariableUsageCount { get; set; } = new();
    public Dictionary<string, long> ErrorsByType { get; set; } = new();
    public Dictionary<string, double> ModelPerformance { get; set; } = new();
    public QualityMetrics? QualityMetrics { get; set; }
}
