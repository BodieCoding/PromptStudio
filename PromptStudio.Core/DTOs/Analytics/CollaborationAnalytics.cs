namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Collaboration analytics
/// </summary>
public class CollaborationAnalytics
{
    /// <summary>
    /// Average team size per lab
    /// </summary>
    public double AverageTeamSize { get; set; }

    /// <summary>
    /// Collaboration frequency metrics
    /// </summary>
    public Dictionary<string, double> CollaborationFrequency { get; set; } = [];

    /// <summary>
    /// Collaboration efficiency score (0-100)
    /// </summary>
    public double CollaborationEfficiencyScore { get; set; }
}
