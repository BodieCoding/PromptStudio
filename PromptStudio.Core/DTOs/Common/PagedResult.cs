namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Represents a paginated result set with metadata support for efficient data transfer in service APIs.
/// </summary>
/// <typeparam name="T">The type of items contained in the paged result.</typeparam>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Universal pagination DTO used across all service layers for efficient data transfer and client-side pagination.
/// Essential for API endpoints handling large datasets, providing consistent pagination patterns across the application.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Generic pagination container with skip/take semantics and extensible metadata support.
/// Designed for efficient serialization and client-side pagination controls with minimal payload overhead.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>API endpoint pagination for lists and search results</item>
/// <item>Database query result pagination with total count tracking</item>
/// <item>Client-side data grid and infinite scroll implementations</item>
/// <item>Bulk data transfer with progress tracking via metadata</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Optimized for JSON serialization with minimal overhead. Generic type parameter enables type-safe collections
/// without boxing. Metadata dictionary should be used sparingly to maintain efficient payload sizes in high-frequency scenarios.</para>
/// </remarks>
public class PagedResult<T>
{
    /// <summary>
    /// Gets or sets the collection of items for the current page.
    /// </summary>
    /// <value>A list containing the items for this page, empty if no results are found.</value>
    public List<T> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the number of items to skip from the beginning of the result set.
    /// </summary>
    /// <value>A non-negative integer representing the offset for pagination (0-based indexing).</value>
    public int Skip { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of items to include in this page.
    /// </summary>
    /// <value>A positive integer representing the page size or limit for this result set.</value>
    public int Take { get; set; }

    /// <summary>
    /// Gets or sets the total count of items available across all pages.
    /// </summary>
    /// <value>A non-negative integer representing the complete result set size before pagination.</value>
    public long TotalCount { get; set; }

    /// <summary>
    /// Gets a value indicating whether more items are available beyond the current page.
    /// </summary>
    /// <value>True if additional pages exist; otherwise, false.</value>
    public bool HasMore => Skip + Take < TotalCount;

    /// <summary>
    /// Gets or sets additional metadata associated with the paged result.
    /// </summary>
    /// <value>A dictionary containing optional metadata such as filtering info, sorting parameters, or performance metrics.</value>
    public Dictionary<string, object> Metadata { get; set; } = [];
}
