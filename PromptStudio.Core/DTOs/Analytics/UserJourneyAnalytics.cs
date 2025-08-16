namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// User journey analytics
/// </summary>
public class UserJourneyAnalytics
{
    /// <summary>
    /// Common user paths
    /// </summary>
    public List<UserPath> CommonPaths { get; set; } = [];

    /// <summary>
    /// Journey touchpoints
    /// </summary>
    public List<JourneyTouchpoint> Touchpoints { get; set; } = [];

    /// <summary>
    /// Journey insights
    /// </summary>
    public List<JourneyInsight> Insights { get; set; } = [];
}
