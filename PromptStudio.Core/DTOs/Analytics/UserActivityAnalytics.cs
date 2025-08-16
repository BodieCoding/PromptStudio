namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// User activity analytics
/// </summary>
public class UserActivityAnalytics
{
    /// <summary>
    /// Daily active users
    /// </summary>
    public double AverageDailyActiveUsers { get; set; }

    /// <summary>
    /// Weekly active users
    /// </summary>
    public long WeeklyActiveUsers { get; set; }

    /// <summary>
    /// Monthly active users
    /// </summary>
    public long MonthlyActiveUsers { get; set; }

    /// <summary>
    /// New users in the period
    /// </summary>
    public long NewUsers { get; set; }

    /// <summary>
    /// Returning users
    /// </summary>
    public long ReturningUsers { get; set; }

    /// <summary>
    /// User activity distribution
    /// </summary>
    public Dictionary<string, long> ActivityDistribution { get; set; } = [];

    /// <summary>
    /// Top users by activity
    /// </summary>
    public List<TopUserActivity>? TopUsers { get; set; }
}
