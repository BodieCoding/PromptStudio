namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Adoption and retention analytics
/// </summary>
public class AdoptionRetentionAnalytics
{
    /// <summary>
    /// Feature adoption timeline
    /// </summary>
    public Dictionary<string, List<AdoptionTimePoint>> AdoptionTimeline { get; set; } = [];

    /// <summary>
    /// User retention rates by cohort
    /// </summary>
    public List<RetentionCohort> RetentionCohorts { get; set; } = [];

    /// <summary>
    /// Churn analysis
    /// </summary>
    public ChurnAnalysis ChurnAnalysis { get; set; } = new();
}
