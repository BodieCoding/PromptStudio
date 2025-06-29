namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Library usage trends
/// </summary>
public class LibraryUsageTrends
{
    public Guid LibraryId { get; set; }
    public Dictionary<DateTime, UsageTrendPoint> TrendData { get; set; } = new();
    public double GrowthRate { get; set; }
    public List<TrendInsight> Insights { get; set; } = new();
    public Dictionary<string, object> Forecasts { get; set; } = new();
}
