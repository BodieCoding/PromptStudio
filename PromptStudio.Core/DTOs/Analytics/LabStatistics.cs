namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive lab performance statistics and operational metrics for dashboard and reporting services.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by analytics services to provide complete lab health assessment, performance monitoring,
/// and operational insights for management dashboards. Aggregates data from multiple domain entities
/// to deliver executive-level lab performance overview with key performance indicators and trends.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains pre-computed lab-wide statistics including content inventory, execution metrics,
/// user engagement data, and financial indicators. Provides both current state snapshots and
/// calculated performance indicators for comprehensive lab assessment and comparison analysis.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Executive dashboard services use this for lab performance overviews
/// - Comparison services analyze multiple labs using these standardized metrics
/// - Monitoring services track lab health and usage trends over time
/// - Reporting services generate executive summaries and KPI reports
/// - Capacity planning services use these metrics for resource allocation decisions
/// 
/// <para><strong>Calculated Properties:</strong></para>
/// SuccessRate is computed from execution data to ensure consistency
/// Service layers should use calculated properties rather than recomputing metrics
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// Statistics are pre-aggregated for dashboard performance
/// Consider caching these results with appropriate refresh intervals
/// MostActiveLibrary provides drill-down capability without full library enumeration
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for lab comparison and monitoring
/// var labStats = await analyticsService.GetLabStatisticsAsync(labId);
/// var utilizationScore = labStats.SuccessRate * (labStats.ActiveTemplates / Math.Max(1, labStats.TotalTemplates));
/// if (labStats.SuccessRate &lt; 90.0) {
///     await alertService.NotifyLabQualityIssueAsync(labId, labStats.SuccessRate);
/// }
/// </code>
/// </example>
public class LabStatistics
{
    /// <summary>
    /// Unique identifier of the lab for which statistics are calculated.
    /// Used for correlation with other analytics data and drill-down operations.
    /// </summary>
    public Guid LabId { get; set; }

    /// <summary>
    /// Human-readable name of the lab for dashboard display and reporting.
    /// Provides context for statistics interpretation and user interface presentation.
    /// </summary>
    public string LabName { get; set; } = string.Empty;

    /// <summary>
    /// Total count of libraries in the lab including both active and archived libraries.
    /// Provides overall lab content scope and organizational structure assessment.
    /// Service layers can use this for capacity and organization analysis.
    /// </summary>
    public int TotalLibraries { get; set; }

    /// <summary>
    /// Count of currently active libraries excluding archived or deleted libraries.
    /// Indicates operational library inventory and active content organization.
    /// Critical metric for current operational capacity and content availability.
    /// </summary>
    public int ActiveLibraries { get; set; }

    /// <summary>
    /// Total count of templates across all libraries including archived templates.
    /// Provides comprehensive view of lab content inventory and development history.
    /// Service layers use this for content volume analysis and historical tracking.
    /// </summary>
    public int TotalTemplates { get; set; }

    /// <summary>
    /// Count of currently active templates available for execution and development.
    /// Indicates operational template inventory and immediate content availability.
    /// Key metric for lab productivity assessment and content utilization analysis.
    /// </summary>
    public int ActiveTemplates { get; set; }

    /// <summary>
    /// Total count of workflows defined in the lab including inactive workflows.
    /// Provides comprehensive view of process automation scope and workflow development.
    /// Service layers use this for automation coverage analysis and workflow inventory.
    /// </summary>
    public int TotalWorkflows { get; set; }

    /// <summary>
    /// Count of currently active workflows available for execution.
    /// Indicates operational workflow capacity and process automation readiness.
    /// Critical metric for automation utilization and operational efficiency assessment.
    /// </summary>
    public int ActiveWorkflows { get; set; }

    /// <summary>
    /// Total number of executions performed across all lab templates and workflows.
    /// Comprehensive usage indicator for lab activity level and operational volume.
    /// Service layers use this for usage trend analysis and capacity planning.
    /// </summary>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Count of executions that completed successfully without errors.
    /// Indicates lab operational quality and template reliability performance.
    /// Used with TotalExecutions for success rate calculation and quality assessment.
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Count of executions that failed due to errors or operational issues.
    /// Indicates areas requiring attention for lab operational improvement.
    /// Service layers can analyze this for quality improvement and troubleshooting.
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Calculated success rate percentage for lab execution quality assessment.
    /// Computed as (SuccessfulExecutions / TotalExecutions) × 100.
    /// Critical KPI for lab operational health and quality monitoring.
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Timestamp of the most recent execution activity in the lab.
    /// Indicates lab usage recency and operational activity level.
    /// Service layers use this for activity monitoring and engagement analysis.
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Timestamp of the most recent activity including executions, updates, and modifications.
    /// Broader activity indicator including content development and administrative actions.
    /// Used for comprehensive lab engagement and productivity tracking.
    /// </summary>
    public DateTime? LastActivity { get; set; }

    /// <summary>
    /// Count of distinct users who have accessed or used the lab.
    /// Indicates lab adoption breadth and user engagement scope.
    /// Service layers use this for adoption analysis and user community assessment.
    /// </summary>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Count of users with formal membership and access rights to the lab.
    /// Indicates lab team size and collaboration scope.
    /// Used for team management and access control analysis.
    /// </summary>
    public int MemberCount { get; set; }

    /// <summary>
    /// Aggregated token consumption across all lab executions.
    /// Null when no token-consuming activities have occurred.
    /// Service layers use this for cost analysis and resource utilization tracking.
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total operational costs incurred by the lab across all activities.
    /// Includes AI model costs, infrastructure costs, and operational expenses.
    /// Null when cost data is unavailable. Critical for budget tracking and cost optimization.
    /// </summary>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Performance summary of the highest-activity library within the lab.
    /// Provides drill-down insights into lab activity concentration and top-performing content.
    /// Service layers can use this for library performance comparison and optimization focus.
    /// </summary>
    public LibraryPerformanceSummary? MostActiveLibrary { get; set; }

    /// <summary>
    /// Lab creation timestamp for age calculation and lifecycle analysis.
    /// Enables maturity assessment and long-term trend analysis.
    /// Service layers use this for lab lifecycle and development timeline tracking.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Most recent lab configuration or metadata update timestamp.
    /// Indicates lab maintenance activity and configuration management.
    /// Used for administrative activity tracking and governance compliance.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}