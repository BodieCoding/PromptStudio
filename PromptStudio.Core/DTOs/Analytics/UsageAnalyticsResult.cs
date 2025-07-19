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
    public List<UsageInsight> Insights { get; set; } = new();
}

/// <summary>
/// Usage analytics summary
/// </summary>
public class UsageAnalyticsSummary
{
    /// <summary>
    /// Total unique users in the period
    /// </summary>
    public long TotalUniqueUsers { get; set; }

    /// <summary>
    /// Total sessions in the period
    /// </summary>
    public long TotalSessions { get; set; }

    /// <summary>
    /// Total actions/interactions
    /// </summary>
    public long TotalActions { get; set; }

    /// <summary>
    /// Average sessions per user
    /// </summary>
    public double AverageSessionsPerUser { get; set; }

    /// <summary>
    /// Average actions per session
    /// </summary>
    public double AverageActionsPerSession { get; set; }

    /// <summary>
    /// Most active day
    /// </summary>
    public DateTime? MostActiveDay { get; set; }

    /// <summary>
    /// Most popular feature
    /// </summary>
    public string? MostPopularFeature { get; set; }

    /// <summary>
    /// User growth rate
    /// </summary>
    public double? UserGrowthRate { get; set; }
}

/// <summary>
/// User activity analytics
/// </summary>
public class UserActivityAnalytics
{
    /// <summary>
    /// Daily active users
    /// </summary>
    public double AverageDailyActiveUsers { get; set; }

    /// <summary>
    /// Weekly active users
    /// </summary>
    public long WeeklyActiveUsers { get; set; }

    /// <summary>
    /// Monthly active users
    /// </summary>
    public long MonthlyActiveUsers { get; set; }

    /// <summary>
    /// New users in the period
    /// </summary>
    public long NewUsers { get; set; }

    /// <summary>
    /// Returning users
    /// </summary>
    public long ReturningUsers { get; set; }

    /// <summary>
    /// User activity distribution
    /// </summary>
    public Dictionary<string, long> ActivityDistribution { get; set; } = new();

    /// <summary>
    /// Top users by activity
    /// </summary>
    public List<TopUserActivity>? TopUsers { get; set; }
}

/// <summary>
/// Feature utilization analytics
/// </summary>
public class FeatureUtilizationAnalytics
{
    /// <summary>
    /// Usage statistics for each feature
    /// </summary>
    public Dictionary<string, FeatureUsageStats> FeatureUsage { get; set; } = new();

    /// <summary>
    /// Feature adoption rates
    /// </summary>
    public Dictionary<string, double> AdoptionRates { get; set; } = new();

    /// <summary>
    /// Feature engagement scores
    /// </summary>
    public Dictionary<string, double> EngagementScores { get; set; } = new();

    /// <summary>
    /// Unused or underutilized features
    /// </summary>
    public List<string> UnderutilizedFeatures { get; set; } = new();
}

/// <summary>
/// Session analytics
/// </summary>
public class SessionAnalytics
{
    /// <summary>
    /// Average session duration in minutes
    /// </summary>
    public double AverageSessionDurationMinutes { get; set; }

    /// <summary>
    /// Median session duration in minutes
    /// </summary>
    public double MedianSessionDurationMinutes { get; set; }

    /// <summary>
    /// Session duration distribution
    /// </summary>
    public Dictionary<string, long> DurationDistribution { get; set; } = new();

    /// <summary>
    /// Bounce rate (single-action sessions)
    /// </summary>
    public double BounceRate { get; set; }

    /// <summary>
    /// Peak session hours
    /// </summary>
    public List<int> PeakHours { get; set; } = new();

    /// <summary>
    /// Session patterns by day of week
    /// </summary>
    public Dictionary<string, SessionDayStats> SessionsByDay { get; set; } = new();
}

/// <summary>
/// Content usage analytics
/// </summary>
public class ContentUsageAnalytics
{
    /// <summary>
    /// Most accessed templates
    /// </summary>
    public List<ContentUsageItem> MostAccessedTemplates { get; set; } = new();

    /// <summary>
    /// Most accessed libraries
    /// </summary>
    public List<ContentUsageItem> MostAccessedLibraries { get; set; } = new();

    /// <summary>
    /// Content engagement metrics
    /// </summary>
    public Dictionary<string, ContentEngagementMetrics> ContentEngagement { get; set; } = new();

    /// <summary>
    /// Search patterns and queries
    /// </summary>
    public List<SearchPattern>? SearchPatterns { get; set; }
}

/// <summary>
/// User segmentation analytics
/// </summary>
public class UserSegmentationAnalytics
{
    /// <summary>
    /// User segments by activity level
    /// </summary>
    public Dictionary<string, UserSegment> SegmentsByActivity { get; set; } = new();

    /// <summary>
    /// User segments by role
    /// </summary>
    public Dictionary<string, UserSegment> SegmentsByRole { get; set; } = new();

    /// <summary>
    /// User segments by department
    /// </summary>
    public Dictionary<string, UserSegment> SegmentsByDepartment { get; set; } = new();

    /// <summary>
    /// Power users identification
    /// </summary>
    public List<PowerUser> PowerUsers { get; set; } = new();
}

/// <summary>
/// Workflow analytics
/// </summary>
public class WorkflowAnalytics
{
    /// <summary>
    /// Common user workflows
    /// </summary>
    public List<UserWorkflow> CommonWorkflows { get; set; } = new();

    /// <summary>
    /// Workflow efficiency metrics
    /// </summary>
    public Dictionary<string, WorkflowEfficiencyMetrics> EfficiencyMetrics { get; set; } = new();

    /// <summary>
    /// Workflow abandonment points
    /// </summary>
    public List<WorkflowAbandonmentPoint> AbandonmentPoints { get; set; } = new();
}

/// <summary>
/// Adoption and retention analytics
/// </summary>
public class AdoptionRetentionAnalytics
{
    /// <summary>
    /// Feature adoption timeline
    /// </summary>
    public Dictionary<string, List<AdoptionTimePoint>> AdoptionTimeline { get; set; } = new();

    /// <summary>
    /// User retention rates by cohort
    /// </summary>
    public List<RetentionCohort> RetentionCohorts { get; set; } = new();

    /// <summary>
    /// Churn analysis
    /// </summary>
    public ChurnAnalysis ChurnAnalysis { get; set; } = new();
}

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
    public List<CohortData> CohortMatrix { get; set; } = new();

    /// <summary>
    /// Cohort insights
    /// </summary>
    public List<string> Insights { get; set; } = new();
}

/// <summary>
/// Funnel analytics
/// </summary>
public class FunnelAnalytics
{
    /// <summary>
    /// Funnel steps
    /// </summary>
    public List<FunnelStep> Steps { get; set; } = new();

    /// <summary>
    /// Overall conversion rate
    /// </summary>
    public double OverallConversionRate { get; set; }

    /// <summary>
    /// Drop-off points
    /// </summary>
    public List<FunnelDropOff> DropOffs { get; set; } = new();
}

/// <summary>
/// User journey analytics
/// </summary>
public class UserJourneyAnalytics
{
    /// <summary>
    /// Common user paths
    /// </summary>
    public List<UserPath> CommonPaths { get; set; } = new();

    /// <summary>
    /// Journey touchpoints
    /// </summary>
    public List<JourneyTouchpoint> Touchpoints { get; set; } = new();

    /// <summary>
    /// Journey insights
    /// </summary>
    public List<JourneyInsight> Insights { get; set; } = new();
}

// Supporting classes for the analytics data structures

public class TopUserActivity
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public long ActivityCount { get; set; }
    public TimeSpan TotalActiveTime { get; set; }
}

public class FeatureUsageStats
{
    public long TotalUsage { get; set; }
    public long UniqueUsers { get; set; }
    public double AverageUsagePerUser { get; set; }
    public DateTime LastUsed { get; set; }
}

public class SessionDayStats
{
    public long SessionCount { get; set; }
    public double AverageDurationMinutes { get; set; }
    public double AverageActionsPerSession { get; set; }
}

public class ContentUsageItem
{
    public Guid ContentId { get; set; }
    public string ContentName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long AccessCount { get; set; }
    public long UniqueUsers { get; set; }
}

public class ContentEngagementMetrics
{
    public TimeSpan AverageTimeSpent { get; set; }
    public double InteractionRate { get; set; }
    public double CompletionRate { get; set; }
}

public class SearchPattern
{
    public string Query { get; set; } = string.Empty;
    public long Count { get; set; }
    public double SuccessRate { get; set; }
}

public class UserSegment
{
    public string SegmentName { get; set; } = string.Empty;
    public long UserCount { get; set; }
    public double PercentageOfTotal { get; set; }
    public Dictionary<string, object> Characteristics { get; set; } = new();
}

public class PowerUser
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public long ActivityScore { get; set; }
    public List<string> TopFeatures { get; set; } = new();
}

public class UserWorkflow
{
    public string WorkflowName { get; set; } = string.Empty;
    public List<string> Steps { get; set; } = new();
    public long Usage { get; set; }
    public double SuccessRate { get; set; }
}

public class WorkflowEfficiencyMetrics
{
    public double AverageCompletionTimeMinutes { get; set; }
    public double SuccessRate { get; set; }
    public int AverageSteps { get; set; }
}

public class WorkflowAbandonmentPoint
{
    public string Step { get; set; } = string.Empty;
    public double AbandonmentRate { get; set; }
    public List<string> CommonReasons { get; set; } = new();
}

public class AdoptionTimePoint
{
    public DateTime Date { get; set; }
    public long AdoptionCount { get; set; }
    public double CumulativeAdoptionRate { get; set; }
}

public class RetentionCohort
{
    public DateTime CohortDate { get; set; }
    public long InitialUsers { get; set; }
    public List<double> RetentionRates { get; set; } = new(); // By period
}

public class ChurnAnalysis
{
    public double ChurnRate { get; set; }
    public List<string> ChurnReasons { get; set; } = new();
    public Dictionary<string, double> ChurnBySegment { get; set; } = new();
}

public class CohortData
{
    public DateTime CohortDate { get; set; }
    public long CohortSize { get; set; }
    public List<double> PeriodValues { get; set; } = new();
}

public class FunnelStep
{
    public int StepOrder { get; set; }
    public string StepName { get; set; } = string.Empty;
    public long Users { get; set; }
    public double ConversionRate { get; set; }
    public double DropOffRate { get; set; }
}

public class FunnelDropOff
{
    public string FromStep { get; set; } = string.Empty;
    public string ToStep { get; set; } = string.Empty;
    public double DropOffRate { get; set; }
    public List<string> PossibleReasons { get; set; } = new();
}

public class UserPath
{
    public List<string> Path { get; set; } = new();
    public long Usage { get; set; }
    public double SuccessRate { get; set; }
    public double AverageCompletionTimeMinutes { get; set; }
}

public class JourneyTouchpoint
{
    public string TouchpointName { get; set; } = string.Empty;
    public long Interactions { get; set; }
    public double SatisfactionScore { get; set; }
    public TimeSpan AverageTimeSpent { get; set; }
}

public class JourneyInsight
{
    public string InsightType { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Impact { get; set; } = string.Empty;
}

public class UsageAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public long ActiveUsers { get; set; }
    public long Sessions { get; set; }
    public long Actions { get; set; }
    public double EngagementScore { get; set; }
}

public class UsageInsight
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public List<string> Recommendations { get; set; } = new();
}
