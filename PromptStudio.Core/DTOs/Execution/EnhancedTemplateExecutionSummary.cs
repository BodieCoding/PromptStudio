using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Enhanced template execution summary
/// </summary>
public class EnhancedTemplateExecutionSummary
{
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public long ExecutionCount { get; set; }
    public DateTime? LastExecution { get; set; }
    public double SuccessRate { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
    public TokenUsageInfo? TokenUsage { get; set; }
    public double? QualityScore { get; set; }
    public Dictionary<string, object> PerformanceMetrics { get; set; } = new();
}
