namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Session analytics
/// </summary>
public class SessionAnalytics
{
    /// <summary>
    /// Average session duration in minutes
    /// </summary>
    public double AverageSessionDurationMinutes { get; set; }

    /// <summary>
    /// Median session duration in minutes
    /// </summary>
    public double MedianSessionDurationMinutes { get; set; }

    /// <summary>
    /// Session duration distribution
    /// </summary>
    public Dictionary<string, long> DurationDistribution { get; set; } = [];

    /// <summary>
    /// Bounce rate (single-action sessions)
    /// </summary>
    public double BounceRate { get; set; }

    /// <summary>
    /// Peak session hours
    /// </summary>
    public List<int> PeakHours { get; set; } = [];

    /// <summary>
    /// Session patterns by day of week
    /// </summary>
    public Dictionary<string, SessionDayStats> SessionsByDay { get; set; } = [];
}
