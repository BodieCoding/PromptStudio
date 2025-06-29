namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Enhanced paged result
/// </summary>
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Skip { get; set; }
    public int Take { get; set; }
    public long TotalCount { get; set; }
    public bool HasMore => Skip + Take < TotalCount;
    public Dictionary<string, object> Metadata { get; set; } = new();
}
