namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Experiment analytics
/// </summary>
public class ExperimentAnalytics
{
    /// <summary>
    /// Total number of experiments
    /// </summary>
    public long TotalExperiments { get; set; }

    /// <summary>
    /// Success rate by experiment type
    /// </summary>
    public Dictionary<string, double> SuccessRateByType { get; set; } = [];

    /// <summary>
    /// Average experiment duration
    /// </summary>
    public TimeSpan AverageExperimentDuration { get; set; }
}
