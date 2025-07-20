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
    public List<LabRecommendation> Recommendations { get; set; } = new();
}

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
    public List<string> KeyAchievements { get; set; } = new();
}

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
    public Dictionary<string, double> SuccessRateByType { get; set; } = new();

    /// <summary>
    /// Average experiment duration
    /// </summary>
    public TimeSpan AverageExperimentDuration { get; set; }
}

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
    public Dictionary<string, double> CollaborationFrequency { get; set; } = new();

    /// <summary>
    /// Collaboration efficiency score (0-100)
    /// </summary>
    public double CollaborationEfficiencyScore { get; set; }
}

/// <summary>
/// Lab resource utilization analytics
/// </summary>
public class LabResourceAnalytics
{
    /// <summary>
    /// Compute resource utilization percentage
    /// </summary>
    public double ComputeUtilization { get; set; }

    /// <summary>
    /// Storage utilization percentage
    /// </summary>
    public double StorageUtilization { get; set; }

    /// <summary>
    /// Cost breakdown by resource type
    /// </summary>
    public Dictionary<string, decimal> CostBreakdown { get; set; } = new();
}

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

/// <summary>
/// Lab recommendation
/// </summary>
public class LabRecommendation
{
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ExpectedBenefit { get; set; }
}
