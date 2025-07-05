namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Time-series analytics data for lab activity trends and operational monitoring dashboards.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by analytics services to provide time-series data for trend analysis, operational dashboards,
/// and performance monitoring. Enables systematic tracking of lab productivity, user engagement,
/// and operational health across configurable time periods for executive reporting and optimization insights.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Aggregates multiple domain entity activities into time-bucketed analytics suitable for charting,
/// trending, and comparative analysis. Contains pre-computed metrics from execution logs, user activities,
/// and content creation events for efficient dashboard rendering and report generation.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Analytics services use this for trend dashboard population
/// - Reporting services consume this for executive summaries and KPI tracking
/// - Monitoring services analyze trends for operational alerts and capacity planning
/// - Performance services track productivity metrics and usage patterns
/// - Business intelligence services aggregate this for strategic insights
/// 
/// <para><strong>Time Series Considerations:</strong></para>
/// Period represents the time bucket (hourly, daily, weekly, monthly based on TrendGranularity)
/// Data should be ordered chronologically for proper trend visualization
/// Missing periods should be handled gracefully in service layer aggregations
/// 
/// <para><strong>Performance Optimization:</strong></para>
/// Pre-computed aggregations reduce real-time calculation overhead
/// Consider caching for frequently accessed time ranges
/// Token usage and cost data may be nullable for periods without activity
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for trend analysis
/// var trendData = await analyticsService.GetLabActivityTrendAsync(labId, startDate, endDate, TrendGranularity.Daily);
/// var growthRate = analyticsService.CalculateGrowthRate(trendData.Select(d => d.ExecutionCount));
/// var peakUsage = trendData.OrderByDescending(d => d.UniqueUsers).First();
/// </code>
/// </example>
public class LabActivityTrendData
{
    /// <summary>
    /// Time period boundary for this aggregated data point.
    /// Represents the start of the time bucket (hour, day, week, month) based on requested granularity.
    /// Service layers should use this for chronological ordering and time-series visualization.
    /// </summary>
    public DateTime Period { get; set; }

    /// <summary>
    /// Total number of template and workflow executions during this time period.
    /// Includes all execution types for comprehensive activity measurement.
    /// Used by analytics services for usage trend analysis and capacity planning.
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Count of distinct users who performed activities during this time period.
    /// Enables user engagement analysis and adoption trend tracking.
    /// Service layers can use this for user activity heatmaps and engagement metrics.
    /// </summary>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Number of new templates created during this time period.
    /// Indicates content creation productivity and innovation velocity.
    /// Analytics services use this for content growth analysis and productivity insights.
    /// </summary>
    public int TemplatesCreated { get; set; }

    /// <summary>
    /// Number of existing templates modified during this time period.
    /// Reflects ongoing content maintenance and improvement activities.
    /// Service layers can analyze this for template lifecycle and maintenance patterns.
    /// </summary>
    public int TemplatesUpdated { get; set; }

    /// <summary>
    /// Number of new libraries created during this time period.
    /// Indicates organizational growth and content structure expansion.
    /// Used by analytics services for organizational development tracking.
    /// </summary>
    public int LibrariesCreated { get; set; }

    /// <summary>
    /// Total number of workflow executions during this time period.
    /// Provides insights into process automation adoption and workflow utilization.
    /// Service layers use this for workflow performance and adoption analysis.
    /// </summary>
    public int WorkflowExecutions { get; set; }

    /// <summary>
    /// Success rate percentage for all executions during this time period.
    /// Calculated as successful executions divided by total executions (0.0-1.0 range).
    /// Critical metric for quality monitoring and operational health assessment.
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Aggregated token consumption metrics for this time period.
    /// Null when no token-consuming activities occurred during the period.
    /// Service layers use this for cost analysis and resource utilization tracking.
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Total operational cost incurred during this time period.
    /// Includes AI model costs, infrastructure costs, and operational expenses.
    /// Null when cost data is unavailable or no billable activities occurred.
    /// </summary>
    public decimal? TotalCost { get; set; }
}
