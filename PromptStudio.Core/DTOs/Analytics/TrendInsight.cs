namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Trend insight for analytics
/// </summary>
public class TrendInsight
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public double Confidence { get; set; }
    public Dictionary<string, object> Data { get; set; } = new();
    public List<string> Recommendations { get; set; } = new();
}
