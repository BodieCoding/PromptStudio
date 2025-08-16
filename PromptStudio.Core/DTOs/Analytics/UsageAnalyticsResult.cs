using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive usage analytics result with detailed user engagement metrics
/// </summary>
public class UsageAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Overall usage summary
    /// </summary>
    public UsageAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// User activity analytics
    /// </summary>
    public UserActivityAnalytics UserActivity { get; set; } = new();

    /// <summary>
    /// Feature utilization analytics
    /// </summary>
    public FeatureUtilizationAnalytics FeatureUtilization { get; set; } = new();

    /// <summary>
    /// Session analytics
    /// </summary>
    public SessionAnalytics Sessions { get; set; } = new();

    /// <summary>
    /// Content usage analytics
    /// </summary>
    public ContentUsageAnalytics ContentUsage { get; set; } = new();

    /// <summary>
    /// User segmentation analysis
    /// </summary>
    public UserSegmentationAnalytics UserSegmentation { get; set; } = new();

    /// <summary>
    /// Workflow analytics
    /// </summary>
    public WorkflowAnalytics Workflows { get; set; } = new();

    /// <summary>
    /// Adoption and retention metrics
    /// </summary>
    public AdoptionRetentionAnalytics AdoptionRetention { get; set; } = new();

    /// <summary>
    /// Cohort analysis data (if requested)
    /// </summary>
    public CohortAnalytics? CohortAnalysis { get; set; }

    /// <summary>
    /// Funnel analysis data (if requested)
    /// </summary>
    public FunnelAnalytics? FunnelAnalysis { get; set; }

    /// <summary>
    /// User journey analysis (if requested)
    /// </summary>
    public UserJourneyAnalytics? UserJourney { get; set; }

    /// <summary>
    /// Time-series usage data
    /// </summary>
    public List<UsageAnalyticsTimePoint>? TimeSeries { get; set; }

    /// <summary>
    /// Usage insights and recommendations
    /// </summary>
    public List<UsageInsight> Insights { get; set; } = [];
}
