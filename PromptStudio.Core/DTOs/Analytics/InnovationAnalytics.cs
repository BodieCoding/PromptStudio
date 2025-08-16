namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Innovation analytics
/// </summary>
public class InnovationAnalytics
{
    /// <summary>
    /// Innovation index (0-100)
    /// </summary>
    public double InnovationIndex { get; set; }

    /// <summary>
    /// Number of breakthrough discoveries
    /// </summary>
    public long BreakthroughCount { get; set; }

    /// <summary>
    /// Novel approach adoption rate
    /// </summary>
    public double NovelApproachAdoptionRate { get; set; }
}
