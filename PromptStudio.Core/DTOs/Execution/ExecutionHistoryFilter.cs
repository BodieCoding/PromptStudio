using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Advanced filtering options for execution history queries
/// </summary>
public class ExecutionHistoryFilter
{
    /// <summary>
    /// Filter by prompt template ID
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Filter by user who executed the prompt
    /// </summary>
    public Guid? ExecutedBy { get; set; }

    /// <summary>
    /// Filter by AI provider
    /// </summary>
    public string? AiProvider { get; set; }

    /// <summary>
    /// Filter by model name
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Filter by execution status
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Date range for execution time
    /// </summary>
    public DateTimeRange? ExecutionTimeRange { get; set; }

    /// <summary>
    /// Minimum response time in milliseconds
    /// </summary>
    public int? MinResponseTimeMs { get; set; }

    /// <summary>
    /// Maximum response time in milliseconds
    /// </summary>
    public int? MaxResponseTimeMs { get; set; }

    /// <summary>
    /// Minimum quality score
    /// </summary>
    public double? MinQualityScore { get; set; }

    /// <summary>
    /// Maximum quality score
    /// </summary>
    public double? MaxQualityScore { get; set; }

    /// <summary>
    /// Filter by cost range
    /// </summary>
    public DecimalRange? CostRange { get; set; }

    /// <summary>
    /// Filter by token usage range
    /// </summary>
    public IntRange? TokenUsageRange { get; set; }

    /// <summary>
    /// Filter by tags
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Search in prompt content or response
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// Whether to include soft-deleted executions
    /// </summary>
    public bool IncludeDeleted { get; set; } = false;

    /// <summary>
    /// Filter by variable collection ID
    /// </summary>
    public Guid? VariableCollectionId { get; set; }

    /// <summary>
    /// Filter by execution priority
    /// </summary>
    public ExecutionPriority? Priority { get; set; }

    /// <summary>
    /// Sort field
    /// </summary>
    public string SortBy { get; set; } = "ExecutedAt";

    /// <summary>
    /// Sort direction
    /// </summary>
    public string SortDirection { get; set; } = "desc";
}

/// <summary>
/// Represents a decimal range filter
/// </summary>
public class DecimalRange
{
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}

/// <summary>
/// Represents an integer range filter
/// </summary>
public class IntRange
{
    public int? Min { get; set; }
    public int? Max { get; set; }
}
