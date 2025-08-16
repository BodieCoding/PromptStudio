namespace PromptStudio.Core.DTOs.Analytics;

public class UsageAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public long ActiveUsers { get; set; }
    public long Sessions { get; set; }
    public long Actions { get; set; }
    public double EngagementScore { get; set; }
}
