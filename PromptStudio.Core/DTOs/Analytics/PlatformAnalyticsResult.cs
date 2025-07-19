using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive platform analytics result with executive-level insights
/// </summary>
public class PlatformAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Overall platform health score (0-100)
    /// </summary>
    public double HealthScore { get; set; }

    /// <summary>
    /// Executive summary with key insights
    /// </summary>
    public PlatformAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// User engagement analytics
    /// </summary>
    public UserEngagementAnalytics UserEngagement { get; set; } = new();

    /// <summary>
    /// System performance analytics
    /// </summary>
    public SystemPerformanceAnalytics Performance { get; set; } = new();

    /// <summary>
    /// Cost and resource utilization analytics
    /// </summary>
    public ResourceUtilizationAnalytics ResourceUtilization { get; set; } = new();

    /// <summary>
    /// Business metrics and KPIs
    /// </summary>
    public BusinessMetricsAnalytics BusinessMetrics { get; set; } = new();

    /// <summary>
    /// Usage patterns and trends
    /// </summary>
    public UsagePatternAnalytics UsagePatterns { get; set; } = new();

    /// <summary>
    /// Quality and reliability metrics
    /// </summary>
    public QualityAnalytics Quality { get; set; } = new();

    /// <summary>
    /// Predictive insights and forecasts
    /// </summary>
    public PredictiveAnalytics? PredictiveInsights { get; set; }

    /// <summary>
    /// Recommendations for optimization
    /// </summary>
    public List<AnalyticsRecommendation> Recommendations { get; set; } = new();

    /// <summary>
    /// Time-series data points
    /// </summary>
    public List<PlatformAnalyticsTimePoint>? TimeSeries { get; set; }

    /// <summary>
    /// Alerts and notable events
    /// </summary>
    public List<AnalyticsAlert> Alerts { get; set; } = new();
}

/// <summary>
/// Executive summary of platform analytics
/// </summary>
public class PlatformAnalyticsSummary
{
    /// <summary>
    /// Total active users in the period
    /// </summary>
    public long TotalActiveUsers { get; set; }

    /// <summary>
    /// Total executions across the platform
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Total cost for the period
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Average response time across all operations
    /// </summary>
    public double AverageResponseTimeMs { get; set; }

    /// <summary>
    /// Overall success rate
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Growth rate compared to previous period
    /// </summary>
    public double? GrowthRate { get; set; }

    /// <summary>
    /// Key achievements in this period
    /// </summary>
    public List<string> KeyAchievements { get; set; } = new();

    /// <summary>
    /// Areas requiring attention
    /// </summary>
    public List<string> AreasOfConcern { get; set; } = new();
}

/// <summary>
/// User engagement analytics
/// </summary>
public class UserEngagementAnalytics
{
    /// <summary>
    /// Daily active users
    /// </summary>
    public double AverageDailyActiveUsers { get; set; }

    /// <summary>
    /// Monthly active users
    /// </summary>
    public long MonthlyActiveUsers { get; set; }

    /// <summary>
    /// User retention rate
    /// </summary>
    public double RetentionRate { get; set; }

    /// <summary>
    /// Average session duration in minutes
    /// </summary>
    public double AverageSessionDurationMinutes { get; set; }

    /// <summary>
    /// Feature adoption rates
    /// </summary>
    public Dictionary<string, double> FeatureAdoptionRates { get; set; } = new();

    /// <summary>
    /// User activity distribution
    /// </summary>
    public Dictionary<string, long> ActivityDistribution { get; set; } = new();
}

/// <summary>
/// System performance analytics
/// </summary>
public class SystemPerformanceAnalytics
{
    /// <summary>
    /// Average response time in milliseconds
    /// </summary>
    public double AverageResponseTimeMs { get; set; }

    /// <summary>
    /// 95th percentile response time
    /// </summary>
    public double P95ResponseTimeMs { get; set; }

    /// <summary>
    /// System uptime percentage
    /// </summary>
    public double UptimePercentage { get; set; }

    /// <summary>
    /// Throughput (requests per second)
    /// </summary>
    public double ThroughputRps { get; set; }

    /// <summary>
    /// Error rate percentage
    /// </summary>
    public double ErrorRate { get; set; }

    /// <summary>
    /// Resource utilization metrics
    /// </summary>
    public Dictionary<string, double> ResourceUtilization { get; set; } = new();
}

/// <summary>
/// Resource utilization analytics
/// </summary>
public class ResourceUtilizationAnalytics
{
    /// <summary>
    /// Total cost for the period
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Cost breakdown by category
    /// </summary>
    public Dictionary<string, decimal> CostBreakdown { get; set; } = new();

    /// <summary>
    /// Cost per user
    /// </summary>
    public decimal CostPerUser { get; set; }

    /// <summary>
    /// Cost per execution
    /// </summary>
    public decimal CostPerExecution { get; set; }

    /// <summary>
    /// Cost trends over time
    /// </summary>
    public List<CostTrendPoint>? CostTrends { get; set; }

    /// <summary>
    /// Resource efficiency score (0-100)
    /// </summary>
    public double EfficiencyScore { get; set; }
}

/// <summary>
/// Business metrics analytics
/// </summary>
public class BusinessMetricsAnalytics
{
    /// <summary>
    /// Return on investment metrics
    /// </summary>
    public Dictionary<string, decimal> ROIMetrics { get; set; } = new();

    /// <summary>
    /// Productivity metrics
    /// </summary>
    public Dictionary<string, double> ProductivityMetrics { get; set; } = new();

    /// <summary>
    /// Customer satisfaction metrics
    /// </summary>
    public Dictionary<string, double> SatisfactionMetrics { get; set; } = new();

    /// <summary>
    /// Business impact scores
    /// </summary>
    public Dictionary<string, double> ImpactScores { get; set; } = new();
}

/// <summary>
/// Usage pattern analytics
/// </summary>
public class UsagePatternAnalytics
{
    /// <summary>
    /// Peak usage hours
    /// </summary>
    public List<int> PeakHours { get; set; } = new();

    /// <summary>
    /// Usage by day of week
    /// </summary>
    public Dictionary<string, long> UsageByDayOfWeek { get; set; } = new();

    /// <summary>
    /// Geographic usage distribution
    /// </summary>
    public Dictionary<string, long>? GeographicDistribution { get; set; }

    /// <summary>
    /// Seasonal patterns identified
    /// </summary>
    public List<string> SeasonalPatterns { get; set; } = new();
}

/// <summary>
/// Quality analytics
/// </summary>
public class QualityAnalytics
{
    /// <summary>
    /// Overall quality score
    /// </summary>
    public double OverallQualityScore { get; set; }

    /// <summary>
    /// Quality trends over time
    /// </summary>
    public List<QualityTrendPoint>? QualityTrends { get; set; }

    /// <summary>
    /// Quality issues identified
    /// </summary>
    public List<QualityIssue> QualityIssues { get; set; } = new();

    /// <summary>
    /// Quality recommendations
    /// </summary>
    public List<QualityRecommendation> QualityRecommendations { get; set; } = new();
}

/// <summary>
/// Predictive analytics and forecasts
/// </summary>
public class PredictiveAnalytics
{
    /// <summary>
    /// Usage forecasts
    /// </summary>
    public List<UsageForecastPoint>? UsageForecasts { get; set; }

    /// <summary>
    /// Cost forecasts
    /// </summary>
    public List<CostForecastPoint>? CostForecasts { get; set; }

    /// <summary>
    /// Capacity planning recommendations
    /// </summary>
    public List<CapacityRecommendation> CapacityRecommendations { get; set; } = new();

    /// <summary>
    /// Risk assessments
    /// </summary>
    public List<RiskAssessment> RiskAssessments { get; set; } = new();
}

/// <summary>
/// Analytics recommendation
/// </summary>
public class AnalyticsRecommendation
{
    /// <summary>
    /// Recommendation type
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Priority level (high, medium, low)
    /// </summary>
    public string Priority { get; set; } = "medium";

    /// <summary>
    /// Recommendation title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Expected impact
    /// </summary>
    public string? ExpectedImpact { get; set; }

    /// <summary>
    /// Implementation effort
    /// </summary>
    public string? ImplementationEffort { get; set; }
}

/// <summary>
/// Platform analytics time point
/// </summary>
public class PlatformAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public long ActiveUsers { get; set; }
    public long Executions { get; set; }
    public decimal Cost { get; set; }
    public double AverageResponseTimeMs { get; set; }
    public double SuccessRate { get; set; }
}

/// <summary>
/// Analytics alert
/// </summary>
public class AnalyticsAlert
{
    /// <summary>
    /// Alert severity (info, warning, critical)
    /// </summary>
    public string Severity { get; set; } = "info";

    /// <summary>
    /// Alert title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Alert message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// When the alert was triggered
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Alert category
    /// </summary>
    public string Category { get; set; } = string.Empty;
}

/// <summary>
/// Usage forecast point
/// </summary>
public class UsageForecastPoint
{
    public DateTime Timestamp { get; set; }
    public long PredictedExecutions { get; set; }
    public long PredictedUsers { get; set; }
    public double ConfidenceLevel { get; set; }
}

/// <summary>
/// Cost forecast point
/// </summary>
public class CostForecastPoint
{
    public DateTime Timestamp { get; set; }
    public decimal PredictedCost { get; set; }
    public decimal CostLowerBound { get; set; }
    public decimal CostUpperBound { get; set; }
    public double ConfidenceLevel { get; set; }
}

/// <summary>
/// Capacity recommendation
/// </summary>
public class CapacityRecommendation
{
    /// <summary>
    /// Resource type
    /// </summary>
    public string ResourceType { get; set; } = string.Empty;

    /// <summary>
    /// Current capacity
    /// </summary>
    public double CurrentCapacity { get; set; }

    /// <summary>
    /// Recommended capacity
    /// </summary>
    public double RecommendedCapacity { get; set; }

    /// <summary>
    /// Reason for recommendation
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Timeline for implementation
    /// </summary>
    public string Timeline { get; set; } = string.Empty;
}

/// <summary>
/// Risk assessment
/// </summary>
public class RiskAssessment
{
    /// <summary>
    /// Risk type
    /// </summary>
    public string RiskType { get; set; } = string.Empty;

    /// <summary>
    /// Risk level (low, medium, high, critical)
    /// </summary>
    public string RiskLevel { get; set; } = "medium";

    /// <summary>
    /// Risk description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Probability of occurrence (0-1)
    /// </summary>
    public double Probability { get; set; }

    /// <summary>
    /// Potential impact
    /// </summary>
    public string Impact { get; set; } = string.Empty;

    /// <summary>
    /// Mitigation strategies
    /// </summary>
    public List<string> MitigationStrategies { get; set; } = new();
}
