namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Feature utilization analytics
/// </summary>
public class FeatureUtilizationAnalytics
{
    /// <summary>
    /// Usage statistics for each feature
    /// </summary>
    public Dictionary<string, FeatureUsageStats> FeatureUsage { get; set; } = [];

    /// <summary>
    /// Feature adoption rates
    /// </summary>
    public Dictionary<string, double> AdoptionRates { get; set; } = [];

    /// <summary>
    /// Feature engagement scores
    /// </summary>
    public Dictionary<string, double> EngagementScores { get; set; } = [];

    /// <summary>
    /// Unused or underutilized features
    /// </summary>
    public List<string> UnderutilizedFeatures { get; set; } = [];
}
