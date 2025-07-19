using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Filtering options for variable collection queries.
/// </summary>
/// <remarks>
/// <para><strong>Usage:</strong></para>
/// Provides flexible filtering capabilities for collection queries, avoiding parameter list code smells
/// and enabling extensible query options.
/// </remarks>
public class CollectionFilterOptions
{
    /// <summary>
    /// Gets or sets the page number for pagination.
    /// </summary>
    /// <value>Page number (1-based) for paginated results.</value>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size for pagination.
    /// </summary>
    /// <value>Number of items per page (max 100).</value>
    public int PageSize { get; set; } = 20;

    /// <summary>
    /// Gets or sets search term for filtering collections by name or description.
    /// </summary>
    /// <value>Search term to match against collection names and descriptions.</value>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Gets or sets whether to include soft-deleted collections.
    /// </summary>
    /// <value><c>true</c> to include deleted collections; otherwise, <c>false</c>.</value>
    public bool IncludeDeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets the creation date range for filtering.
    /// </summary>
    /// <value>Date range for filtering collections by creation date.</value>
    public DateTimeRange? CreatedDateRange { get; set; }

    /// <summary>
    /// Gets or sets the minimum number of rows for filtering.
    /// </summary>
    /// <value>Minimum row count to filter collections by size.</value>
    public int? MinRowCount { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of rows for filtering.
    /// </summary>
    /// <value>Maximum row count to filter collections by size.</value>
    public int? MaxRowCount { get; set; }

    /// <summary>
    /// Gets or sets collection status filter.
    /// </summary>
    /// <value>Status value to filter collections by processing status.</value>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets sort criteria for the results.
    /// </summary>
    /// <value>Field name to sort results by.</value>
    public string SortBy { get; set; } = "CreatedAt";

    /// <summary>
    /// Gets or sets sort direction.
    /// </summary>
    /// <value><c>true</c> for ascending, <c>false</c> for descending.</value>
    public bool SortAscending { get; set; } = false;
}
