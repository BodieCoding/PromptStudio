using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive lab analytics result with experiment metrics and collaboration insights
/// </summary>
public class LabAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Overall lab analytics summary
    /// </summary>
    public LabAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// Experiment analytics
    /// </summary>
    public ExperimentAnalytics Experiments { get; set; } = new();

    /// <summary>
    /// Collaboration analytics
    /// </summary>
    public CollaborationAnalytics Collaboration { get; set; } = new();

    /// <summary>
    /// Resource utilization analytics
    /// </summary>
    public LabResourceAnalytics ResourceUtilization { get; set; } = new();

    /// <summary>
    /// Innovation metrics
    /// </summary>
    public InnovationAnalytics Innovation { get; set; } = new();

    /// <summary>
    /// Time-series data for lab activities
    /// </summary>
    public List<LabAnalyticsTimePoint>? TimeSeries { get; set; }

    /// <summary>
    /// Lab optimization recommendations
    /// </summary>
    public List<LabRecommendation> Recommendations { get; set; } = [];
}
