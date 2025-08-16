namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Cohort analytics
/// </summary>
public class CohortAnalytics
{
    /// <summary>
    /// Cohort definition
    /// </summary>
    public string CohortDefinition { get; set; } = string.Empty;

    /// <summary>
    /// Cohort data matrix
    /// </summary>
    public List<CohortData> CohortMatrix { get; set; } = [];

    /// <summary>
    /// Cohort insights
    /// </summary>
    public List<string> Insights { get; set; } = [];
}
