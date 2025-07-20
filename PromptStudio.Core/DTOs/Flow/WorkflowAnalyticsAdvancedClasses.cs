using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents trend forecasts for predictive analytics.
/// </summary>
public class TrendForecasts
{
    /// <summary>
    /// Gets or sets forecasted execution volume trends.
    /// </summary>
    /// <value>Predicted execution volume trends for future periods.</value>
    public List<ExecutionVolumeTrendPoint>? ExecutionVolumeForecasts { get; set; }

    /// <summary>
    /// Gets or sets forecasted performance trends.
    /// </summary>
    /// <value>Predicted performance trends for future periods.</value>
    public List<PerformanceTrendPoint>? PerformanceForecasts { get; set; }

    /// <summary>
    /// Gets or sets forecasted cost trends.
    /// </summary>
    /// <value>Predicted cost trends for future periods.</value>
    public List<CostTrendPoint>? CostForecasts { get; set; }

    /// <summary>
    /// Gets or sets the forecast confidence level.
    /// </summary>
    /// <value>Confidence level percentage (0-100) for the forecasts.</value>
    public double ConfidenceLevel { get; set; }

    /// <summary>
    /// Gets or sets the forecast horizon in days.
    /// </summary>
    /// <value>Number of days into the future that the forecasts cover.</value>
    public int ForecastHorizonDays { get; set; }
}

/// <summary>
/// Represents a seasonal pattern in workflow usage.
/// </summary>
public class SeasonalPattern
{
    /// <summary>
    /// Gets or sets the pattern identifier.
    /// </summary>
    /// <value>Unique identifier for the seasonal pattern.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the pattern name.
    /// </summary>
    /// <value>Descriptive name for the seasonal pattern.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the pattern description.
    /// </summary>
    /// <value>Detailed description of the seasonal pattern.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the pattern frequency.
    /// </summary>
    /// <value>Frequency of the pattern occurrence (Daily, Weekly, Monthly, Yearly).</value>
    public string Frequency { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the pattern strength score.
    /// </summary>
    /// <value>Strength score (0-100) indicating how pronounced the pattern is.</value>
    public double StrengthScore { get; set; }

    /// <summary>
    /// Gets or sets the peak periods for this pattern.
    /// </summary>
    /// <value>Time periods when this pattern reaches its peak.</value>
    public List<string>? PeakPeriods { get; set; }

    /// <summary>
    /// Gets or sets the low periods for this pattern.
    /// </summary>
    /// <value>Time periods when this pattern reaches its lowest point.</value>
    public List<string>? LowPeriods { get; set; }
}

/// <summary>
/// Represents a trend anomaly detected in the analytics.
/// </summary>
public class TrendAnomaly
{
    /// <summary>
    /// Gets or sets the anomaly identifier.
    /// </summary>
    /// <value>Unique identifier for the detected anomaly.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the anomaly was detected.
    /// </summary>
    /// <value>Date and time when the anomaly occurred.</value>
    public DateTime DetectedAt { get; set; }

    /// <summary>
    /// Gets or sets the anomaly type.
    /// </summary>
    /// <value>Type or category of the detected anomaly.</value>
    public string AnomalyType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level of the anomaly.
    /// </summary>
    /// <value>Severity level (Low, Medium, High, Critical) of the anomaly.</value>
    public string SeverityLevel { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the anomaly description.
    /// </summary>
    /// <value>Detailed description of what the anomaly represents.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expected value for comparison.
    /// </summary>
    /// <value>The expected value based on historical trends.</value>
    public double ExpectedValue { get; set; }

    /// <summary>
    /// Gets or sets the actual value that triggered the anomaly.
    /// </summary>
    /// <value>The actual value that deviated from the expected pattern.</value>
    public double ActualValue { get; set; }

    /// <summary>
    /// Gets or sets the deviation percentage.
    /// </summary>
    /// <value>Percentage deviation from the expected value.</value>
    public double DeviationPercentage { get; set; }
}

/// <summary>
/// Represents a user activity entry for behavioral analysis.
/// </summary>
public class UserActivityEntry
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    /// <value>Unique identifier for the user.</value>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user name for identification.
    /// </summary>
    /// <value>Display name or username for identification.</value>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total execution count for this user.
    /// </summary>
    /// <value>Total number of workflow executions by this user.</value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the unique workflows executed by this user.
    /// </summary>
    /// <value>Number of unique workflows executed by this user.</value>
    public int UniqueWorkflowsExecuted { get; set; }

    /// <summary>
    /// Gets or sets the user activity score.
    /// </summary>
    /// <value>Calculated activity score (0-100) for this user.</value>
    public double ActivityScore { get; set; }

    /// <summary>
    /// Gets or sets the last activity timestamp.
    /// </summary>
    /// <value>Date and time of the user's last workflow execution.</value>
    public DateTime LastActivity { get; set; }
}

/// <summary>
/// Represents a user engagement trend point for behavioral analysis.
/// </summary>
public class UserEngagementTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this engagement measurement.
    /// </summary>
    /// <value>Date and time for this engagement data point.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the engagement score at this point.
    /// </summary>
    /// <value>User engagement score (0-100) at this time point.</value>
    public double EngagementScore { get; set; }

    /// <summary>
    /// Gets or sets the session count at this point.
    /// </summary>
    /// <value>Number of user sessions at this time point.</value>
    public int SessionCount { get; set; }

    /// <summary>
    /// Gets or sets the average session duration at this point.
    /// </summary>
    /// <value>Average session duration at this time point.</value>
    public TimeSpan AverageSessionDuration { get; set; }

    /// <summary>
    /// Gets or sets the user retention rate at this point.
    /// </summary>
    /// <value>User retention rate percentage (0-100) at this time point.</value>
    public double RetentionRate { get; set; }
}

/// <summary>
/// Represents a workflow adoption pattern for usage analysis.
/// </summary>
public class WorkflowAdoptionPattern
{
    /// <summary>
    /// Gets or sets the pattern identifier.
    /// </summary>
    /// <value>Unique identifier for the adoption pattern.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow identifier associated with this pattern.
    /// </summary>
    /// <value>Identifier of the workflow being analyzed for adoption patterns.</value>
    public string WorkflowId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow name.
    /// </summary>
    /// <value>Name of the workflow being analyzed.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the adoption rate over time.
    /// </summary>
    /// <value>Rate of adoption by users over the analysis period.</value>
    public double AdoptionRate { get; set; }

    /// <summary>
    /// Gets or sets the time to adoption median.
    /// </summary>
    /// <value>Median time from workflow creation to user adoption.</value>
    public TimeSpan TimeToAdoption { get; set; }

    /// <summary>
    /// Gets or sets the user adoption curve data.
    /// </summary>
    /// <value>Data points showing how adoption has grown over time.</value>
    public List<UserAdoptionDataPoint>? AdoptionCurve { get; set; }
}

/// <summary>
/// Represents a user behavior segment for behavioral analysis.
/// </summary>
public class UserBehaviorSegment
{
    /// <summary>
    /// Gets or sets the segment identifier.
    /// </summary>
    /// <value>Unique identifier for the behavior segment.</value>
    public string SegmentId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the segment name.
    /// </summary>
    /// <value>Descriptive name for the behavior segment.</value>
    public string SegmentName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the segment description.
    /// </summary>
    /// <value>Detailed description of the behavior segment characteristics.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user count in this segment.
    /// </summary>
    /// <value>Number of users classified into this behavior segment.</value>
    public int UserCount { get; set; }

    /// <summary>
    /// Gets or sets the percentage of total users in this segment.
    /// </summary>
    /// <value>Percentage (0-100) of total users that belong to this segment.</value>
    public double PercentageOfTotalUsers { get; set; }

    /// <summary>
    /// Gets or sets the key characteristics of this segment.
    /// </summary>
    /// <value>Dictionary of key behavioral characteristics defining this segment.</value>
    public Dictionary<string, object>? KeyCharacteristics { get; set; }
}

/// <summary>
/// Represents a user adoption data point for adoption curve analysis.
/// </summary>
public class UserAdoptionDataPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this adoption measurement.
    /// </summary>
    /// <value>Date and time for this adoption data point.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the cumulative user count at this point.
    /// </summary>
    /// <value>Cumulative number of users who have adopted the workflow by this time.</value>
    public int CumulativeUserCount { get; set; }

    /// <summary>
    /// Gets or sets the new user count at this point.
    /// </summary>
    /// <value>Number of new users who adopted the workflow at this time point.</value>
    public int NewUserCount { get; set; }

    /// <summary>
    /// Gets or sets the adoption rate at this point.
    /// </summary>
    /// <value>Rate of adoption (users per time unit) at this time point.</value>
    public double AdoptionRate { get; set; }
}
