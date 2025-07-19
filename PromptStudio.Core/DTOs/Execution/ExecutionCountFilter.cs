using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Filter options for counting executions
/// </summary>
public class ExecutionCountFilter
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
    /// Filter by tags
    /// </summary>
    public List<string>? Tags { get; set; }
}
