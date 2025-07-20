using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive workflow analytics result DTO with detailed metrics and business intelligence insights.
/// Provides enterprise-grade workflow analytics capabilities with comprehensive performance metrics,
/// trend analysis, resource utilization insights, and business intelligence for workflow optimization.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary analytics result DTO for IWorkflowOrchestrationService analytics operations,
/// providing comprehensive workflow insights with performance metrics, trend analysis,
/// resource utilization patterns, and business intelligence for enterprise workflow optimization.</para>
/// 
/// <para><strong>Analytics Scope:</strong></para>
/// <para>Multi-dimensional workflow analytics including performance analysis, resource optimization,
/// cost analysis, user behavior patterns, and business intelligence insights. Designed for enterprise
/// decision-making and continuous workflow improvement initiatives.</para>
/// 
/// <para><strong>Insight Categories:</strong></para>
/// <list type="bullet">
/// <item>Performance metrics and execution analysis</item>
/// <item>Resource utilization and cost optimization</item>
/// <item>Trend analysis and predictive insights</item>
/// <item>Business intelligence and operational metrics</item>
/// </list>
/// </remarks>
public class WorkflowAnalyticsResult
{
    /// <summary>
    /// Gets or sets the analytics generation timestamp for tracking and versioning.
    /// </summary>
    /// <value>The date and time when the workflow analytics were generated.</value>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the analysis period covered by the analytics.
    /// </summary>
    /// <value>Date range representing the period covered by the analytics analysis.</value>
    public DateTimeRangeFilter AnalysisPeriod { get; set; } = new();

    /// <summary>
    /// Gets or sets the aggregation level used for the analytics.
    /// </summary>
    /// <value>Level of data aggregation applied to the analytics results.</value>
    public AnalyticsAggregationLevel AggregationLevel { get; set; }

    /// <summary>
    /// Gets or sets the total number of workflows included in the analysis.
    /// </summary>
    /// <value>Count of unique workflows included in the analytics analysis.</value>
    public int TotalWorkflowsAnalyzed { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions included in the analysis.
    /// </summary>
    /// <value>Count of workflow executions included in the analytics analysis.</value>
    public long TotalExecutionsAnalyzed { get; set; }

    /// <summary>
    /// Gets or sets the overall performance metrics for all workflows.
    /// </summary>
    /// <value>Aggregate performance metrics across all workflows in the analysis.</value>
    public WorkflowPerformanceMetrics? OverallPerformanceMetrics { get; set; }

    /// <summary>
    /// Gets or sets the resource utilization analytics across all workflows.
    /// </summary>
    /// <value>Comprehensive resource utilization analysis and optimization insights.</value>
    public ResourceUtilizationAnalytics? ResourceUtilizationAnalytics { get; set; }

    /// <summary>
    /// Gets or sets the cost analysis results for financial optimization.
    /// </summary>
    /// <value>Detailed cost analysis with optimization opportunities and trends.</value>
    public PromptStudio.Core.DTOs.Analytics.CostAnalyticsResult? CostAnalytics { get; set; }

    /// <summary>
    /// Gets or sets the trend analysis results for predictive insights.
    /// </summary>
    /// <value>Comprehensive trend analysis with forecasting and pattern recognition.</value>
    public TrendAnalysisResult? TrendAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the user activity analytics for behavioral insights.
    /// </summary>
    /// <value>Analysis of user behavior patterns and workflow usage statistics.</value>
    public UserActivityAnalytics? UserActivityAnalytics { get; set; }

    /// <summary>
    /// Gets or sets the workflow popularity rankings and usage patterns.
    /// </summary>
    /// <value>Analysis of workflow popularity, usage frequency, and adoption patterns.</value>
    public WorkflowPopularityAnalytics? PopularityAnalytics { get; set; }

    /// <summary>
    /// Gets or sets the error and failure analysis for reliability insights.
    /// </summary>
    /// <value>Comprehensive error analysis with failure patterns and reliability metrics.</value>
    public ErrorAnalyticsResult? ErrorAnalytics { get; set; }

    /// <summary>
    /// Gets or sets the quality metrics analysis for workflow assessment.
    /// </summary>
    /// <value>Quality assessment metrics and improvement recommendations for workflows.</value>
    public QualityAnalyticsResult? QualityAnalytics { get; set; }

    /// <summary>
    /// Gets or sets the category-based analytics for organizational insights.
    /// </summary>
    /// <value>Analysis of workflow performance and usage by category classification.</value>
    public CategoryAnalyticsResult? CategoryAnalytics { get; set; }

    /// <summary>
    /// Gets or sets the geographic distribution analytics for location-based insights.
    /// </summary>
    /// <value>Geographic analysis of workflow usage and performance by location.</value>
    public GeographicAnalyticsResult? GeographicAnalytics { get; set; }

    /// <summary>
    /// Gets or sets the comparative analysis against benchmarks and previous periods.
    /// </summary>
    /// <value>Comparative analysis with benchmarks, industry standards, and historical data.</value>
    public ComparativeAnalysisResult? ComparativeAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the optimization recommendations based on analytics insights.
    /// </summary>
    /// <value>Actionable optimization recommendations derived from analytics analysis.</value>
    public List<AnalyticsOptimizationRecommendation>? OptimizationRecommendations { get; set; }

    /// <summary>
    /// Gets or sets the key performance indicators summary.
    /// </summary>
    /// <value>Summary of key performance indicators and executive-level metrics.</value>
    public KpiSummary? KpiSummary { get; set; }

    /// <summary>
    /// Gets or sets the dimensional analysis results for multi-dimensional insights.
    /// </summary>
    /// <value>Multi-dimensional analysis results organized by analytical dimensions.</value>
    public Dictionary<AnalyticsDimension, DimensionalAnalysisResult>? DimensionalAnalysis { get; set; }

    /// <summary>
    /// Gets or sets additional custom analytics metrics and insights.
    /// </summary>
    /// <value>Dictionary of custom analytics properties for organization-specific metrics.</value>
    public Dictionary<string, object>? CustomAnalyticsMetrics { get; set; }
}

/// <summary>
/// Represents comprehensive performance metrics for workflow analytics.
/// </summary>
public class WorkflowPerformanceMetrics
{
    /// <summary>
    /// Gets or sets the overall success rate percentage.
    /// </summary>
    /// <value>Overall success rate percentage across all analyzed workflows.</value>
    public double OverallSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution duration across all workflows.
    /// </summary>
    /// <value>Average execution time across all workflow executions in the analysis.</value>
    public TimeSpan AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the median execution duration for statistical analysis.
    /// </summary>
    /// <value>Median execution time providing central tendency insights.</value>
    public TimeSpan MedianExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the fastest execution time recorded.
    /// </summary>
    /// <value>Shortest execution duration among all analyzed workflows.</value>
    public TimeSpan FastestExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the slowest execution time recorded.
    /// </summary>
    /// <value>Longest execution duration among all analyzed workflows.</value>
    public TimeSpan SlowestExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the overall throughput rate.
    /// </summary>
    /// <value>Average number of workflow executions completed per unit time.</value>
    public double OverallThroughput { get; set; }

    /// <summary>
    /// Gets or sets the execution consistency score.
    /// </summary>
    /// <value>Score representing the consistency of execution times and outcomes.</value>
    public double ExecutionConsistencyScore { get; set; }

    /// <summary>
    /// Gets or sets the performance trend indicator.
    /// </summary>
    /// <value>Indicator showing whether performance is improving, declining, or stable.</value>
    public PerformanceTrend PerformanceTrend { get; set; }

    /// <summary>
    /// Gets or sets percentile distribution of execution durations.
    /// </summary>
    /// <value>Dictionary of percentile levels and their corresponding execution duration values.</value>
    public Dictionary<double, TimeSpan>? PercentileDistribution { get; set; }

    /// <summary>
    /// Gets or sets the top performing workflows by success rate.
    /// </summary>
    /// <value>Collection of workflows with the highest success rates.</value>
    public List<WorkflowPerformanceEntry>? TopPerformingWorkflows { get; set; }

    /// <summary>
    /// Gets or sets the underperforming workflows requiring attention.
    /// </summary>
    /// <value>Collection of workflows with performance issues requiring optimization.</value>
    public List<WorkflowPerformanceEntry>? UnderperformingWorkflows { get; set; }
}

/// <summary>
/// Represents resource utilization analytics for optimization insights.
/// </summary>
public class ResourceUtilizationAnalytics
{
    /// <summary>
    /// Gets or sets the overall resource efficiency score.
    /// </summary>
    /// <value>Calculated efficiency score based on resource utilization across all workflows.</value>
    public double OverallResourceEfficiency { get; set; }

    /// <summary>
    /// Gets or sets the average CPU utilization percentage.
    /// </summary>
    /// <value>Average CPU utilization across all workflow executions.</value>
    public double AverageCpuUtilization { get; set; }

    /// <summary>
    /// Gets or sets the peak CPU utilization recorded.
    /// </summary>
    /// <value>Highest CPU utilization percentage recorded during the analysis period.</value>
    public double PeakCpuUtilization { get; set; }

    /// <summary>
    /// Gets or sets the average memory utilization in bytes.
    /// </summary>
    /// <value>Average memory consumption across all workflow executions.</value>
    public long AverageMemoryUtilization { get; set; }

    /// <summary>
    /// Gets or sets the peak memory utilization recorded.
    /// </summary>
    /// <value>Highest memory consumption recorded during the analysis period.</value>
    public long PeakMemoryUtilization { get; set; }

    /// <summary>
    /// Gets or sets the total network bandwidth consumed.
    /// </summary>
    /// <value>Total network data transferred across all workflow executions.</value>
    public long TotalNetworkUtilization { get; set; }

    /// <summary>
    /// Gets or sets the average storage utilization.
    /// </summary>
    /// <value>Average storage space consumption across all workflow executions.</value>
    public long AverageStorageUtilization { get; set; }

    /// <summary>
    /// Gets or sets the resource utilization trend over time.
    /// </summary>
    /// <value>Trend analysis showing how resource utilization changes over time.</value>
    public List<ResourceUtilizationTrendPoint>? UtilizationTrend { get; set; }

    /// <summary>
    /// Gets or sets the most resource-intensive workflows.
    /// </summary>
    /// <value>Collection of workflows with the highest resource consumption.</value>
    public List<ResourceIntensiveWorkflow>? MostResourceIntensiveWorkflows { get; set; }

    /// <summary>
    /// Gets or sets resource optimization opportunities.
    /// </summary>
    /// <value>Collection of identified opportunities for resource optimization.</value>
    public List<ResourceOptimizationOpportunity>? OptimizationOpportunities { get; set; }
}

/// <summary>
/// Represents a trend analysis result with forecasting and pattern recognition.
/// </summary>
public class TrendAnalysisResult
{
    /// <summary>
    /// Gets or sets the execution volume trends over time.
    /// </summary>
    /// <value>Analysis of workflow execution volume trends and patterns.</value>
    public List<ExecutionVolumeTrendPoint>? ExecutionVolumeTrends { get; set; }

    /// <summary>
    /// Gets or sets the performance trends over time.
    /// </summary>
    /// <value>Analysis of workflow performance trends including success rates and durations.</value>
    public List<PerformanceTrendPoint>? PerformanceTrends { get; set; }

    /// <summary>
    /// Gets or sets the cost trends over time for financial analysis.
    /// </summary>
    /// <value>Analysis of workflow execution cost trends and financial patterns.</value>
    public List<CostTrendPoint>? CostTrends { get; set; }

    /// <summary>
    /// Gets or sets the user activity trends over time.
    /// </summary>
    /// <value>Analysis of user activity and engagement trends with workflows.</value>
    public List<UserActivityTrendPoint>? UserActivityTrends { get; set; }

    /// <summary>
    /// Gets or sets the error rate trends over time for reliability analysis.
    /// </summary>
    /// <value>Analysis of error rate trends and reliability patterns.</value>
    public List<ErrorRateTrendPoint>? ErrorRateTrends { get; set; }

    /// <summary>
    /// Gets or sets forecasted values for future planning.
    /// </summary>
    /// <value>Predictive forecasts based on historical trend analysis.</value>
    public TrendForecasts? Forecasts { get; set; }

    /// <summary>
    /// Gets or sets identified seasonal patterns in workflow usage.
    /// </summary>
    /// <value>Analysis of seasonal and cyclical patterns in workflow execution.</value>
    public List<SeasonalPattern>? SeasonalPatterns { get; set; }

    /// <summary>
    /// Gets or sets anomaly detection results for unusual patterns.
    /// </summary>
    /// <value>Detection and analysis of anomalous patterns in workflow behavior.</value>
    public List<TrendAnomaly>? DetectedAnomalies { get; set; }
}

/// <summary>
/// Represents user activity analytics for behavioral insights.
/// </summary>
public class UserActivityAnalytics
{
    /// <summary>
    /// Gets or sets the total number of active users during the analysis period.
    /// </summary>
    /// <value>Count of unique users who executed workflows during the analysis period.</value>
    public int TotalActiveUsers { get; set; }

    /// <summary>
    /// Gets or sets the average executions per user.
    /// </summary>
    /// <value>Average number of workflow executions per active user.</value>
    public double AverageExecutionsPerUser { get; set; }

    /// <summary>
    /// Gets or sets the most active users by execution count.
    /// </summary>
    /// <value>Collection of users with the highest workflow execution activity.</value>
    public List<UserActivityEntry>? MostActiveUsers { get; set; }

    /// <summary>
    /// Gets or sets the user engagement trends over time.
    /// </summary>
    /// <value>Analysis of user engagement patterns and trends over the analysis period.</value>
    public List<UserEngagementTrendPoint>? EngagementTrends { get; set; }

    /// <summary>
    /// Gets or sets the workflow adoption patterns by users.
    /// </summary>
    /// <value>Analysis of how users adopt and utilize different workflows.</value>
    public List<WorkflowAdoptionPattern>? AdoptionPatterns { get; set; }

    /// <summary>
    /// Gets or sets the user behavior segmentation analysis.
    /// </summary>
    /// <value>Segmentation of users based on their workflow usage behavior patterns.</value>
    public List<UserBehaviorSegment>? BehaviorSegments { get; set; }
}

/// <summary>
/// Represents an analytics optimization recommendation based on insights.
/// </summary>
public class AnalyticsOptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation identifier for tracking.
    /// </summary>
    /// <value>Unique identifier for the analytics-based optimization recommendation.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation category for classification.
    /// </summary>
    /// <value>Category classification for the type of optimization recommended.</value>
    public AnalyticsRecommendationCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the recommendation title for identification.
    /// </summary>
    /// <value>Brief title describing the analytics-based optimization recommendation.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation description with detailed guidance.
    /// </summary>
    /// <value>Detailed description of the recommended optimization based on analytics insights.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority level for the recommendation.
    /// </summary>
    /// <value>Priority level indicating the importance of implementing this recommendation.</value>
    public RecommendationPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the potential impact of implementing the recommendation.
    /// </summary>
    /// <value>Quantified impact assessment based on analytics analysis.</value>
    public RecommendationImpact? PotentialImpact { get; set; }

    /// <summary>
    /// Gets or sets the confidence level in the recommendation.
    /// </summary>
    /// <value>Confidence percentage based on the strength of supporting analytics data.</value>
    public double ConfidenceLevel { get; set; }

    /// <summary>
    /// Gets or sets the supporting analytics evidence.
    /// </summary>
    /// <value>Analytics data and metrics that support this recommendation.</value>
    public Dictionary<string, object>? SupportingEvidence { get; set; }

    /// <summary>
    /// Gets or sets the recommended implementation steps.
    /// </summary>
    /// <value>Specific steps for implementing the analytics-based recommendation.</value>
    public List<string>? ImplementationSteps { get; set; }
}

/// <summary>
/// Represents a key performance indicators summary for executive reporting.
/// </summary>
public class KpiSummary
{
    /// <summary>
    /// Gets or sets the overall system health score.
    /// </summary>
    /// <value>Composite score representing the overall health of the workflow system.</value>
    public double SystemHealthScore { get; set; }

    /// <summary>
    /// Gets or sets the business value delivered score.
    /// </summary>
    /// <value>Score representing the business value delivered by workflow executions.</value>
    public double BusinessValueScore { get; set; }

    /// <summary>
    /// Gets or sets the operational efficiency score.
    /// </summary>
    /// <value>Score representing the operational efficiency of workflow operations.</value>
    public double OperationalEfficiencyScore { get; set; }

    /// <summary>
    /// Gets or sets the user satisfaction score.
    /// </summary>
    /// <value>Score representing user satisfaction with workflow performance.</value>
    public double UserSatisfactionScore { get; set; }

    /// <summary>
    /// Gets or sets the cost effectiveness score.
    /// </summary>
    /// <value>Score representing the cost effectiveness of workflow operations.</value>
    public double CostEffectivenessScore { get; set; }

    /// <summary>
    /// Gets or sets the innovation index score.
    /// </summary>
    /// <value>Score representing the level of innovation in workflow development and usage.</value>
    public double InnovationIndexScore { get; set; }

    /// <summary>
    /// Gets or sets executive summary insights.
    /// </summary>
    /// <value>Collection of key insights formatted for executive consumption.</value>
    public List<ExecutiveInsight>? ExecutiveInsights { get; set; }

    /// <summary>
    /// Gets or sets critical alerts requiring immediate attention.
    /// </summary>
    /// <value>Collection of critical issues identified through analytics analysis.</value>
    public List<CriticalAlert>? CriticalAlerts { get; set; }
}

/// <summary>
/// Represents dimensional analysis results for multi-dimensional insights.
/// </summary>
public class DimensionalAnalysisResult
{
    /// <summary>
    /// Gets or sets the dimension being analyzed.
    /// </summary>
    /// <value>The analytical dimension for this analysis result.</value>
    public AnalyticsDimension Dimension { get; set; }

    /// <summary>
    /// Gets or sets the dimensional breakdown results.
    /// </summary>
    /// <value>Results broken down by dimension values with associated metrics.</value>
    public Dictionary<string, DimensionalMetrics>? DimensionalBreakdown { get; set; }

    /// <summary>
    /// Gets or sets the top performing dimension values.
    /// </summary>
    /// <value>Collection of dimension values with the best performance metrics.</value>
    public List<DimensionalPerformanceEntry>? TopPerformingValues { get; set; }

    /// <summary>
    /// Gets or sets insights specific to this dimension.
    /// </summary>
    /// <value>Analytical insights specific to this dimensional analysis.</value>
    public List<string>? DimensionalInsights { get; set; }

    /// <summary>
    /// Gets or sets correlation analysis with other dimensions.
    /// </summary>
    /// <value>Analysis of correlations between this dimension and other analytical dimensions.</value>
    public Dictionary<AnalyticsDimension, double>? DimensionalCorrelations { get; set; }
}
