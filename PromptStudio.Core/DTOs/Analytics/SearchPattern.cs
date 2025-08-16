namespace PromptStudio.Core.DTOs.Analytics;

public class SearchPattern
{
    public string Query { get; set; } = string.Empty;
    public long Count { get; set; }
    public double SuccessRate { get; set; }
}
