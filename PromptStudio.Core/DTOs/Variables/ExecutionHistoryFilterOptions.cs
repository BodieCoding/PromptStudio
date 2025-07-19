using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Filter options for execution history queries
/// </summary>
public class ExecutionHistoryFilterOptions
{
    /// <summary>
    /// Gets or sets the collection ID to filter by
    /// </summary>
    public Guid? CollectionId { get; set; }

    /// <summary>
    /// Gets or sets the prompt template ID to filter by
    /// </summary>
    public Guid? PromptTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the lab ID for multi-tenancy filtering
    /// </summary>
    public Guid? LabId { get; set; }

    /// <summary>
    /// Gets or sets the start date for filtering executions
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for filtering executions
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the execution status to filter by
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the minimum execution duration in milliseconds
    /// </summary>
    public int? MinDuration { get; set; }

    /// <summary>
    /// Gets or sets the maximum execution duration in milliseconds
    /// </summary>
    public int? MaxDuration { get; set; }

    /// <summary>
    /// Gets or sets whether to include only successful executions
    /// </summary>
    public bool? SuccessfulOnly { get; set; }

    /// <summary>
    /// Gets or sets whether to include only failed executions
    /// </summary>
    public bool? FailedOnly { get; set; }

    /// <summary>
    /// Gets or sets tags to filter by
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Gets or sets the page number for pagination
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size for pagination
    /// </summary>
    [Range(1, 1000)]
    public int PageSize { get; set; } = 50;

    /// <summary>
    /// Gets or sets the sort field
    /// </summary>
    public string SortBy { get; set; } = "ExecutedAt";

    /// <summary>
    /// Gets or sets the sort direction
    /// </summary>
    public bool SortDescending { get; set; } = true;
}
