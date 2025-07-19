using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Lab;

/// <summary>
/// Library performance summary for lab analytics
/// </summary>
public class LibraryPerformanceSummary
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
    /// Number of templates in the library
    /// </summary>
    public int TemplateCount { get; set; }

    /// <summary>
    /// Number of executions
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Success rate
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Token usage
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Total cost
    /// </summary>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Performance score (calculated metric)
    /// </summary>
    public double PerformanceScore { get; set; }

    /// <summary>
    /// Last execution date
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
