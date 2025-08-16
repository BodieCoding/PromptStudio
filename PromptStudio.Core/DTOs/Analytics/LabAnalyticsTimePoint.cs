namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Lab analytics time point
/// </summary>
public class LabAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public long ActiveLabs { get; set; }
    public long ActiveExperiments { get; set; }
    public double AverageSuccessRate { get; set; }
    public double CollaborationScore { get; set; }
}
