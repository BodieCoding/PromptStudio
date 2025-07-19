using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Library execution statistics with comprehensive metrics
/// </summary>
public class LibraryExecutionStatistics
{
    /// <summary>
    /// Library ID
    /// </summary>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Library name
    /// </summary>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Total number of executions across all templates in the library
    /// </summary>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Number of successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Number of failed executions
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Success rate as a percentage
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Average execution duration across all templates
    /// </summary>
    public TimeSpan? AverageExecutionDuration { get; set; }

    /// <summary>
    /// Total token usage across all executions
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total cost across all executions
    /// </summary>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Most active template in the library
    /// </summary>
    public PromptTemplateExecutionSummary? MostActiveTemplate { get; set; }

    /// <summary>
    /// Last execution date across all templates
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Number of unique templates that have been executed
    /// </summary>
    public int ActiveTemplates { get; set; }

    /// <summary>
    /// Total number of templates in the library
    /// </summary>
    public int TotalTemplates { get; set; }

    /// <summary>
    /// Statistics calculation period start
    /// </summary>
    public DateTime? PeriodStart { get; set; }

    /// <summary>
    /// Statistics calculation period end
    /// </summary>
    public DateTime? PeriodEnd { get; set; }
}
