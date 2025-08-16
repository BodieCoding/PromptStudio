using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Prompt template execution summary with performance metrics
/// </summary>
public class PromptTemplateExecutionSummary
{
    /// <summary>
    /// Template ID
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Template name
    /// </summary>
    public string TemplateName { get; set; } = string.Empty;

    /// <summary>
    /// Total execution count for this template
    /// </summary>
    public long ExecutionCount { get; set; }

    /// <summary>
    /// Most recent execution timestamp
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Success rate as a percentage
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Average execution time
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Token usage statistics
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Average quality score
    /// </summary>
    public double? QualityScore { get; set; }

    /// <summary>
    /// Additional performance metrics
    /// </summary>
    public Dictionary<string, object> PerformanceMetrics { get; set; } = [];
}
