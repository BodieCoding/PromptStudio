namespace PromptStudio.Core.DTOs.Analytics;

public class ContentUsageItem
{
    public Guid ContentId { get; set; }
    public string ContentName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long AccessCount { get; set; }
    public long UniqueUsers { get; set; }
}
