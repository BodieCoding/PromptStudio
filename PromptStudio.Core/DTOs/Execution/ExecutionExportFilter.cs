using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Filter options for exporting execution data
/// </summary>
public class ExecutionExportFilter
{
    /// <summary>
    /// Date range for executions to export
    /// </summary>
    public DateTimeRange? TimeRange { get; set; }

    /// <summary>
    /// Filter by prompt template IDs
    /// </summary>
    public List<Guid>? TemplateIds { get; set; }

    /// <summary>
    /// Filter by user IDs
    /// </summary>
    public List<Guid>? UserIds { get; set; }

    /// <summary>
    /// Filter by AI providers
    /// </summary>
    public List<string>? AiProviders { get; set; }

    /// <summary>
    /// Filter by models
    /// </summary>
    public List<string>? Models { get; set; }

    /// <summary>
    /// Filter by execution status
    /// </summary>
    public List<string>? Statuses { get; set; }

    /// <summary>
    /// Filter by tags
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Minimum quality score
    /// </summary>
    public double? MinQualityScore { get; set; }

    /// <summary>
    /// Maximum quality score
    /// </summary>
    public double? MaxQualityScore { get; set; }

    /// <summary>
    /// Include soft-deleted executions
    /// </summary>
    public bool IncludeDeleted { get; set; } = false;

    /// <summary>
    /// Fields to include in the export
    /// </summary>
    public List<string>? IncludeFields { get; set; }

    /// <summary>
    /// Fields to exclude from the export
    /// </summary>
    public List<string>? ExcludeFields { get; set; }

    /// <summary>
    /// Maximum number of records to export (for performance)
    /// </summary>
    public int? MaxRecords { get; set; }

    /// <summary>
    /// Sort field for the export
    /// </summary>
    public string SortBy { get; set; } = "ExecutedAt";

    /// <summary>
    /// Sort direction
    /// </summary>
    public string SortDirection { get; set; } = "desc";

    /// <summary>
    /// Whether to include variable values in the export
    /// </summary>
    public bool IncludeVariableValues { get; set; } = true;

    /// <summary>
    /// Whether to include full response content
    /// </summary>
    public bool IncludeResponseContent { get; set; } = true;

    /// <summary>
    /// Whether to include performance metrics
    /// </summary>
    public bool IncludePerformanceMetrics { get; set; } = true;

    /// <summary>
    /// Whether to anonymize personal data
    /// </summary>
    public bool AnonymizeData { get; set; } = false;

    /// <summary>
    /// Custom filters as key-value pairs
    /// </summary>
    public Dictionary<string, object>? CustomFilters { get; set; }
}
