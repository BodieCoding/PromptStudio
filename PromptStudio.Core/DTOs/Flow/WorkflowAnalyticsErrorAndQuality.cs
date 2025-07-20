using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents workflow popularity analytics for usage insights.
/// </summary>
public class WorkflowPopularityAnalytics
{
    /// <summary>
    /// Gets or sets the most popular workflows by execution count.
    /// </summary>
    /// <value>Collection of workflows ranked by execution frequency.</value>
    public List<WorkflowPopularityEntry>? MostPopularWorkflows { get; set; }

    /// <summary>
    /// Gets or sets the trending workflows showing growth in popularity.
    /// </summary>
    /// <value>Collection of workflows showing increasing usage trends.</value>
    public List<WorkflowPopularityEntry>? TrendingWorkflows { get; set; }

    /// <summary>
    /// Gets or sets the declining workflows showing decrease in popularity.
    /// </summary>
    /// <value>Collection of workflows showing declining usage trends.</value>
    public List<WorkflowPopularityEntry>? DecliningWorkflows { get; set; }

    /// <summary>
    /// Gets or sets the underutilized workflows with low adoption.
    /// </summary>
    /// <value>Collection of workflows with low usage that may need attention.</value>
    public List<WorkflowPopularityEntry>? UnderutilizedWorkflows { get; set; }
}

/// <summary>
/// Represents a workflow popularity entry for ranking analysis.
/// </summary>
public class WorkflowPopularityEntry
{
    /// <summary>
    /// Gets or sets the workflow identifier.
    /// </summary>
    /// <value>Unique identifier for the workflow.</value>
    public string WorkflowId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow name.
    /// </summary>
    /// <value>Human-readable name of the workflow.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total execution count.
    /// </summary>
    /// <value>Total number of executions for this workflow.</value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the unique user count.
    /// </summary>
    /// <value>Number of unique users who have executed this workflow.</value>
    public int UniqueUserCount { get; set; }

    /// <summary>
    /// Gets or sets the popularity score.
    /// </summary>
    /// <value>Calculated popularity score (0-100) for this workflow.</value>
    public double PopularityScore { get; set; }

    /// <summary>
    /// Gets or sets the popularity trend direction.
    /// </summary>
    /// <value>Direction of the popularity trend (Rising, Stable, Declining).</value>
    public string PopularityTrend { get; set; } = string.Empty;
}

/// <summary>
/// Represents error analytics results for reliability insights.
/// </summary>
public class ErrorAnalyticsResult
{
    /// <summary>
    /// Gets or sets the overall error rate percentage.
    /// </summary>
    /// <value>Overall error rate across all workflows in the analysis period.</value>
    public double OverallErrorRate { get; set; }

    /// <summary>
    /// Gets or sets the error breakdown by category.
    /// </summary>
    /// <value>Dictionary of error categories and their respective counts.</value>
    public Dictionary<string, int>? ErrorBreakdownByCategory { get; set; }

    /// <summary>
    /// Gets or sets the most error-prone workflows.
    /// </summary>
    /// <value>Collection of workflows with the highest error rates.</value>
    public List<WorkflowErrorEntry>? MostErrorProneWorkflows { get; set; }

    /// <summary>
    /// Gets or sets the common error patterns identified.
    /// </summary>
    /// <value>Collection of common error patterns and their frequencies.</value>
    public List<ErrorPattern>? CommonErrorPatterns { get; set; }

    /// <summary>
    /// Gets or sets the error resolution metrics.
    /// </summary>
    /// <value>Metrics related to error resolution times and success rates.</value>
    public ErrorResolutionMetrics? ResolutionMetrics { get; set; }
}

/// <summary>
/// Represents a workflow error entry for error analysis.
/// </summary>
public class WorkflowErrorEntry
{
    /// <summary>
    /// Gets or sets the workflow identifier.
    /// </summary>
    /// <value>Unique identifier for the workflow with errors.</value>
    public string WorkflowId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow name.
    /// </summary>
    /// <value>Human-readable name of the workflow.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error rate for this workflow.
    /// </summary>
    /// <value>Error rate percentage (0-100) for this workflow.</value>
    public double ErrorRate { get; set; }

    /// <summary>
    /// Gets or sets the total error count.
    /// </summary>
    /// <value>Total number of errors for this workflow.</value>
    public int TotalErrors { get; set; }

    /// <summary>
    /// Gets or sets the most common error types for this workflow.
    /// </summary>
    /// <value>Dictionary of error types and their frequencies.</value>
    public Dictionary<string, int>? CommonErrorTypes { get; set; }
}

/// <summary>
/// Represents an error pattern for pattern analysis.
/// </summary>
public class ErrorPattern
{
    /// <summary>
    /// Gets or sets the pattern identifier.
    /// </summary>
    /// <value>Unique identifier for the error pattern.</value>
    public string PatternId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the pattern description.
    /// </summary>
    /// <value>Description of the error pattern characteristics.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the frequency of this pattern.
    /// </summary>
    /// <value>Number of times this error pattern has been observed.</value>
    public int Frequency { get; set; }

    /// <summary>
    /// Gets or sets the affected workflows count.
    /// </summary>
    /// <value>Number of workflows affected by this error pattern.</value>
    public int AffectedWorkflowsCount { get; set; }

    /// <summary>
    /// Gets or sets potential solutions for this error pattern.
    /// </summary>
    /// <value>List of potential solutions or mitigations for this error pattern.</value>
    public List<string>? PotentialSolutions { get; set; }
}

/// <summary>
/// Represents error resolution metrics for reliability analysis.
/// </summary>
public class ErrorResolutionMetrics
{
    /// <summary>
    /// Gets or sets the average resolution time for errors.
    /// </summary>
    /// <value>Average time taken to resolve errors.</value>
    public TimeSpan AverageResolutionTime { get; set; }

    /// <summary>
    /// Gets or sets the first-time resolution rate.
    /// </summary>
    /// <value>Percentage of errors resolved on the first attempt.</value>
    public double FirstTimeResolutionRate { get; set; }

    /// <summary>
    /// Gets or sets the resolution success rate.
    /// </summary>
    /// <value>Percentage of errors that are successfully resolved.</value>
    public double ResolutionSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the escalation rate for complex errors.
    /// </summary>
    /// <value>Percentage of errors that require escalation.</value>
    public double EscalationRate { get; set; }
}

/// <summary>
/// Represents quality analytics results for workflow assessment.
/// </summary>
public class QualityAnalyticsResult
{
    /// <summary>
    /// Gets or sets the overall quality score across all workflows.
    /// </summary>
    /// <value>Calculated overall quality score (0-100) for the workflow system.</value>
    public double OverallQualityScore { get; set; }

    /// <summary>
    /// Gets or sets quality metrics by workflow.
    /// </summary>
    /// <value>Dictionary of workflow IDs and their respective quality scores.</value>
    public Dictionary<string, double>? QualityScoresByWorkflow { get; set; }

    /// <summary>
    /// Gets or sets the highest quality workflows.
    /// </summary>
    /// <value>Collection of workflows with the highest quality scores.</value>
    public List<WorkflowQualityEntry>? HighestQualityWorkflows { get; set; }

    /// <summary>
    /// Gets or sets the workflows needing quality improvement.
    /// </summary>
    /// <value>Collection of workflows with quality issues requiring attention.</value>
    public List<WorkflowQualityEntry>? QualityImprovementNeeded { get; set; }

    /// <summary>
    /// Gets or sets the quality trend over time.
    /// </summary>
    /// <value>Quality trend data points showing quality changes over time.</value>
    public List<QualityTrendPoint>? QualityTrend { get; set; }
}

/// <summary>
/// Represents a workflow quality entry for quality analysis.
/// </summary>
public class WorkflowQualityEntry
{
    /// <summary>
    /// Gets or sets the workflow identifier.
    /// </summary>
    /// <value>Unique identifier for the workflow being assessed.</value>
    public string WorkflowId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow name.
    /// </summary>
    /// <value>Human-readable name of the workflow.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quality score for this workflow.
    /// </summary>
    /// <value>Calculated quality score (0-100) for this workflow.</value>
    public double QualityScore { get; set; }

    /// <summary>
    /// Gets or sets the quality factors breakdown.
    /// </summary>
    /// <value>Dictionary of quality factors and their individual scores.</value>
    public Dictionary<string, double>? QualityFactors { get; set; }

    /// <summary>
    /// Gets or sets quality improvement recommendations.
    /// </summary>
    /// <value>List of specific recommendations for improving workflow quality.</value>
    public List<string>? ImprovementRecommendations { get; set; }
}

/// <summary>
/// Represents a quality trend point for temporal quality analysis.
/// </summary>
public class QualityTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this quality measurement.
    /// </summary>
    /// <value>Date and time for this quality data point.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the quality score at this point.
    /// </summary>
    /// <value>Quality score (0-100) at this time point.</value>
    public double QualityScore { get; set; }

    /// <summary>
    /// Gets or sets the number of quality issues at this point.
    /// </summary>
    /// <value>Number of identified quality issues at this time point.</value>
    public int QualityIssueCount { get; set; }

    /// <summary>
    /// Gets or sets the quality improvement rate at this point.
    /// </summary>
    /// <value>Rate of quality improvement at this time point.</value>
    public double ImprovementRate { get; set; }
}
