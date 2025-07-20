namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Enumeration of analytics dimensions for multi-dimensional analysis across all analytics systems.
/// </summary>
public enum AnalyticsDimension
{
    /// <summary>Time-based dimension</summary>
    Time = 0,
    /// <summary>Workflow-based dimension</summary>
    Workflow = 1,
    /// <summary>User-based dimension</summary>
    User = 2,
    /// <summary>Category-based dimension</summary>
    Category = 3,
    /// <summary>Priority-based dimension</summary>
    Priority = 4,
    /// <summary>Environment-based dimension</summary>
    Environment = 5,
    /// <summary>Execution type dimension</summary>
    ExecutionType = 6,
    /// <summary>Status-based dimension</summary>
    Status = 7,
    /// <summary>Tag-based dimension</summary>
    Tag = 8,
    /// <summary>Project-based dimension</summary>
    Project = 9,
    /// <summary>Organizational unit dimension</summary>
    OrganizationalUnit = 10,
    /// <summary>Geography-based dimension</summary>
    Geography = 11
}

/// <summary>
/// Enumeration of recommendation priority levels for optimization planning.
/// </summary>
public enum RecommendationPriority
{
    /// <summary>Low priority recommendation</summary>
    Low = 0,
    /// <summary>Medium priority recommendation</summary>
    Medium = 1,
    /// <summary>High priority recommendation</summary>
    High = 2,
    /// <summary>Critical priority recommendation</summary>
    Critical = 3
}

/// <summary>
/// Enumeration of analytics recommendation categories for classification.
/// </summary>
public enum AnalyticsRecommendationCategory
{
    /// <summary>Performance optimization recommendation</summary>
    PerformanceOptimization = 0,
    /// <summary>Resource utilization recommendation</summary>
    ResourceOptimization = 1,
    /// <summary>Cost reduction recommendation</summary>
    CostOptimization = 2,
    /// <summary>User experience improvement recommendation</summary>
    UserExperienceImprovement = 3,
    /// <summary>Reliability enhancement recommendation</summary>
    ReliabilityEnhancement = 4,
    /// <summary>Scalability improvement recommendation</summary>
    ScalabilityImprovement = 5,
    /// <summary>Process optimization recommendation</summary>
    ProcessOptimization = 6
}

/// <summary>
/// Enumeration of performance trend indicators for analytics insights.
/// </summary>
public enum PerformanceTrend
{
    /// <summary>Performance is improving over time</summary>
    Improving = 0,
    /// <summary>Performance is stable over time</summary>
    Stable = 1,
    /// <summary>Performance is declining over time</summary>
    Declining = 2,
    /// <summary>Performance shows high volatility</summary>
    Volatile = 3,
    /// <summary>Insufficient data for trend determination</summary>
    InsufficientData = 4
}

/// <summary>
/// Represents the potential impact of implementing an analytics recommendation.
/// </summary>
public class RecommendationImpact
{
    /// <summary>
    /// Gets or sets the expected performance improvement percentage.
    /// </summary>
    /// <value>Percentage improvement in performance expected from implementing the recommendation.</value>
    public double? PerformanceImprovement { get; set; }

    /// <summary>
    /// Gets or sets the expected cost reduction percentage.
    /// </summary>
    /// <value>Percentage reduction in costs expected from implementing the recommendation.</value>
    public double? CostReduction { get; set; }

    /// <summary>
    /// Gets or sets the expected efficiency gain percentage.
    /// </summary>
    /// <value>Percentage improvement in efficiency expected from implementing the recommendation.</value>
    public double? EfficiencyGain { get; set; }

    /// <summary>
    /// Gets or sets the expected user satisfaction improvement.
    /// </summary>
    /// <value>Scale improvement in user satisfaction (1-10 scale).</value>
    public double? UserSatisfactionImprovement { get; set; }

    /// <summary>
    /// Gets or sets the estimated implementation effort in hours.
    /// </summary>
    /// <value>Estimated number of hours required to implement the recommendation.</value>
    public int? ImplementationEffortHours { get; set; }

    /// <summary>
    /// Gets or sets the expected return on investment percentage.
    /// </summary>
    /// <value>Expected ROI percentage from implementing the recommendation.</value>
    public double? ExpectedRoi { get; set; }
}
