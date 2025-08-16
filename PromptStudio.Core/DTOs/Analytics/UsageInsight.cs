namespace PromptStudio.Core.DTOs.Analytics;

public class UsageInsight
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public List<string> Recommendations { get; set; } = [];
}
