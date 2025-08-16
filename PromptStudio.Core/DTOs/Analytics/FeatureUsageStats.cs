namespace PromptStudio.Core.DTOs.Analytics;

public class FeatureUsageStats
{
    public long TotalUsage { get; set; }
    public long UniqueUsers { get; set; }
    public double AverageUsagePerUser { get; set; }
    public DateTime LastUsed { get; set; }
}
