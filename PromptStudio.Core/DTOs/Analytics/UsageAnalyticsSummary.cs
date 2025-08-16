namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Usage analytics summary
/// </summary>
public class UsageAnalyticsSummary
{
    /// <summary>
    /// Total unique users in the period
    /// </summary>
    public long TotalUniqueUsers { get; set; }

    /// <summary>
    /// Total sessions in the period
    /// </summary>
    public long TotalSessions { get; set; }

    /// <summary>
    /// Total actions/interactions
    /// </summary>
    public long TotalActions { get; set; }

    /// <summary>
    /// Average sessions per user
    /// </summary>
    public double AverageSessionsPerUser { get; set; }

    /// <summary>
    /// Average actions per session
    /// </summary>
    public double AverageActionsPerSession { get; set; }

    /// <summary>
    /// Most active day
    /// </summary>
    public DateTime? MostActiveDay { get; set; }

    /// <summary>
    /// Most popular feature
    /// </summary>
    public string? MostPopularFeature { get; set; }

    /// <summary>
    /// User growth rate
    /// </summary>
    public double? UserGrowthRate { get; set; }
}
