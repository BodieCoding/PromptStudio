namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Lab analytics summary
/// </summary>
public class LabAnalyticsSummary
{
    /// <summary>
    /// Total number of active labs
    /// </summary>
    public long TotalActiveLabs { get; set; }

    /// <summary>
    /// Total number of experiments
    /// </summary>
    public long TotalExperiments { get; set; }

    /// <summary>
    /// Overall success rate of experiments
    /// </summary>
    public double OverallSuccessRate { get; set; }

    /// <summary>
    /// Average collaboration score
    /// </summary>
    public double AverageCollaborationScore { get; set; }

    /// <summary>
    /// Key achievements
    /// </summary>
    public List<string> KeyAchievements { get; set; } = [];
}
