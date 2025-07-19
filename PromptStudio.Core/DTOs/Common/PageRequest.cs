namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Page request parameters for paginated queries
/// </summary>
public class PageRequest
{
    /// <summary>
    /// Page number (0-based)
    /// </summary>
    public int Page { get; set; } = 0;

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int Size { get; set; } = 20;

    /// <summary>
    /// Sort field
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// Sort direction (asc, desc)
    /// </summary>
    public string Direction { get; set; } = "asc";

    /// <summary>
    /// Calculate skip value for database queries
    /// </summary>
    public int Skip => Page * Size;

    /// <summary>
    /// Get take value for database queries
    /// </summary>
    public int Take => Size;
}
