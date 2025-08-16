namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Content usage analytics
/// </summary>
public class ContentUsageAnalytics
{
    /// <summary>
    /// Most accessed templates
    /// </summary>
    public List<ContentUsageItem> MostAccessedTemplates { get; set; } = [];

    /// <summary>
    /// Most accessed libraries
    /// </summary>
    public List<ContentUsageItem> MostAccessedLibraries { get; set; } = [];

    /// <summary>
    /// Content engagement metrics
    /// </summary>
    public Dictionary<string, ContentEngagementMetrics> ContentEngagement { get; set; } = [];

    /// <summary>
    /// Search patterns and queries
    /// </summary>
    public List<SearchPattern>? SearchPatterns { get; set; }
}
