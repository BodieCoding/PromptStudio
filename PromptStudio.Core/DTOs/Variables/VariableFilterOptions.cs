using System.ComponentModel.DataAnnotations;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Filter options for variable queries
/// </summary>
public class VariableFilterOptions
{
    /// <summary>
    /// Gets or sets the variable name pattern to filter by
    /// </summary>
    public string? NamePattern { get; set; }

    /// <summary>
    /// Gets or sets the variable type to filter by
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets whether to include only required variables
    /// </summary>
    public bool? RequiredOnly { get; set; }

    /// <summary>
    /// Gets or sets tags to filter by
    /// </summary>
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// Gets or sets the template ID to filter variables by
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the creation date range filter
    /// </summary>
    public DateTimeRange? CreatedDateRange { get; set; }

    /// <summary>
    /// Gets or sets the modification date range filter
    /// </summary>
    public DateTimeRange? ModifiedDateRange { get; set; }

    /// <summary>
    /// Gets or sets whether to include variables with default values only
    /// </summary>
    public bool? HasDefaultValue { get; set; }

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
    public string SortBy { get; set; } = "Name";

    /// <summary>
    /// Gets or sets the sort direction
    /// </summary>
    public bool SortDescending { get; set; } = false;
}
