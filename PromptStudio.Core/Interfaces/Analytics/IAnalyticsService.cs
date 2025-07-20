using PromptStudio.Core.DTOs.Analytics;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.Interfaces.Analytics;

/// <summary>
/// Enterprise-grade service interface for comprehensive analytics and business intelligence across the PromptStudio
/// platform, including usage analytics, performance metrics, cost analysis, and predictive insights for 
/// operational optimization and strategic decision support workflows.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// Cross-cutting analytics service layer component responsible for data aggregation, analysis, and insights generation
/// across all platform domains including labs, libraries, templates, executions, and workflows. Implements enterprise
/// patterns for multi-tenancy, data security, real-time analytics, and scalable data processing with advanced
/// machine learning capabilities for predictive analytics and optimization recommendations.
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// Implements comprehensive analytics capabilities with strict performance requirements, real-time data processing,
/// predictive modeling, and advanced visualization support. Supports analytics across all platform domains with
/// full audit capabilities, tenant isolation, and performance optimization for high-volume data processing
/// scenarios and enterprise business intelligence workflows.
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
///   <item>All operations must enforce tenant isolation and data security boundaries</item>
///   <item>Analytics must support real-time processing and historical trend analysis</item>
///   <item>Implement comprehensive data aggregation and statistical analysis capabilities</item>
///   <item>Support predictive analytics and machine learning for optimization insights</item>
///   <item>Implement comprehensive audit logging for all analytics operations</item>
///   <item>Optimize for high-volume data processing and concurrent analytics operations</item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
///   <item>Integrates with all domain services for comprehensive data collection</item>
///   <item>Coordinates with audit services for historical data analysis and compliance</item>
///   <item>Works with notification services for analytics alerts and insights delivery</item>
///   <item>Supports export services for business intelligence and reporting workflows</item>
///   <item>Implements event sourcing patterns for real-time analytics processing</item>
///   <item>Uses distributed caching for analytics performance optimization</item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
///   <item>Mock data sources and aggregation services for unit testing analytics</item>
///   <item>Test analytics accuracy and performance under high data volume</item>
///   <item>Verify tenant isolation in multi-tenant analytics scenarios</item>
///   <item>Performance test analytics processing under concurrent load</item>
///   <item>Test predictive analytics accuracy and model performance</item>
/// </list>
/// </remarks>
public interface IAnalyticsService
{
    #region Platform-Wide Analytics

    /// <summary>
    /// Retrieves comprehensive platform analytics with usage patterns, performance metrics, and operational insights.
    /// Provides executive-level analytics dashboard data including user engagement, system performance,
    /// resource utilization, and business metrics for strategic decision support and operational optimization.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="timeRange">Time range for analytics data aggregation</param>
    /// <param name="filters">Optional filtering criteria for analytics data</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive platform analytics with business intelligence insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Analytics Capabilities:</strong></para>
    /// <list type="bullet">
    ///   <item>Platform usage patterns and user engagement metrics</item>
    ///   <item>System performance and resource utilization analysis</item>
    ///   <item>Business metrics and operational KPIs</item>
    ///   <item>Cost analysis and resource optimization insights</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Aggregates data from all platform domains for comprehensive insights</item>
    ///   <item>Implements real-time analytics with historical trend analysis</item>
    ///   <item>Supports customizable dashboards and visualization configurations</item>
    ///   <item>Provides executive reporting and business intelligence capabilities</item>
    /// </list>
    /// </remarks>
    Task<PlatformAnalyticsResult> GetPlatformAnalyticsAsync(
        Guid tenantId, 
        AnalyticsTimeRange timeRange, 
        PlatformAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves comprehensive usage analytics across all platform domains with detailed metrics and insights.
    /// Provides detailed usage analysis including user activity patterns, feature utilization, adoption metrics,
    /// and engagement insights for product optimization and user experience enhancement workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="timeRange">Time range for usage analytics aggregation</param>
    /// <param name="filters">Optional filtering criteria for usage analytics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive usage analytics with detailed engagement metrics</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Usage Analytics:</strong></para>
    /// <list type="bullet">
    ///   <item>User activity patterns and engagement metrics</item>
    ///   <item>Feature utilization and adoption analysis</item>
    ///   <item>Workflow efficiency and optimization insights</item>
    ///   <item>User journey analysis and experience optimization</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements comprehensive user behavior tracking and analysis</item>
    ///   <item>Supports cohort analysis and user segmentation</item>
    ///   <item>Provides feature adoption and usage trend analysis</item>
    ///   <item>Includes user experience optimization recommendations</item>
    /// </list>
    /// </remarks>
    Task<UsageAnalyticsResult> GetUsageAnalyticsAsync(
        Guid tenantId, 
        AnalyticsTimeRange timeRange, 
        UsageAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves comprehensive performance analytics with system metrics, optimization insights, and benchmarking data.
    /// Provides detailed performance analysis including response times, throughput metrics, resource utilization,
    /// and optimization recommendations for system performance enhancement and capacity planning workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="timeRange">Time range for performance analytics aggregation</param>
    /// <param name="filters">Optional filtering criteria for performance analytics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive performance analytics with optimization insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Performance Analytics:</strong></para>
    /// <list type="bullet">
    ///   <item>System response times and throughput metrics</item>
    ///   <item>Resource utilization and capacity analysis</item>
    ///   <item>Performance bottleneck identification and resolution</item>
    ///   <item>Optimization recommendations and capacity planning</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements real-time performance monitoring and analysis</item>
    ///   <item>Supports performance benchmarking and trend analysis</item>
    ///   <item>Provides automated performance optimization recommendations</item>
    ///   <item>Includes predictive capacity planning and scaling insights</item>
    /// </list>
    /// </remarks>
    Task<PerformanceAnalyticsResult> GetPerformanceAnalyticsAsync(
        Guid tenantId, 
        AnalyticsTimeRange timeRange, 
        PerformanceAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Domain-Specific Analytics

    /// <summary>
    /// Retrieves comprehensive lab analytics with experiment metrics, collaboration insights, and optimization data.
    /// Provides detailed lab analysis including experiment success rates, collaboration patterns, resource utilization,
    /// and innovation metrics for lab optimization and research productivity enhancement workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="timeRange">Time range for lab analytics aggregation</param>
    /// <param name="filters">Optional filtering criteria for lab analytics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive lab analytics with experiment and collaboration insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Lab Analytics:</strong></para>
    /// <list type="bullet">
    ///   <item>Experiment success rates and outcome analysis</item>
    ///   <item>Collaboration patterns and team productivity metrics</item>
    ///   <item>Resource utilization and cost optimization insights</item>
    ///   <item>Innovation metrics and research effectiveness analysis</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements experiment tracking and outcome analysis</item>
    ///   <item>Supports collaboration analytics and team performance insights</item>
    ///   <item>Provides resource optimization and cost analysis for labs</item>
    ///   <item>Includes innovation metrics and research productivity recommendations</item>
    /// </list>
    /// </remarks>
    Task<LabAnalyticsResult> GetLabAnalyticsAsync(
        Guid tenantId, 
        AnalyticsTimeRange timeRange, 
        LabAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves comprehensive template analytics with usage patterns, performance metrics, and optimization insights.
    /// Provides detailed template analysis including execution success rates, performance benchmarks, usage patterns,
    /// and optimization recommendations for template development and maintenance workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="timeRange">Time range for template analytics aggregation</param>
    /// <param name="filters">Optional filtering criteria for template analytics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive template analytics with performance and optimization insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Template Analytics:</strong></para>
    /// <list type="bullet">
    ///   <item>Template execution success rates and performance metrics</item>
    ///   <item>Usage patterns and adoption analysis</item>
    ///   <item>Variable effectiveness and optimization insights</item>
    ///   <item>Template quality assessment and improvement recommendations</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements template performance tracking and analysis</item>
    ///   <item>Supports usage pattern analysis and adoption metrics</item>
    ///   <item>Provides template optimization and quality enhancement insights</item>
    ///   <item>Includes best practice recommendations and design pattern analysis</item>
    /// </list>
    /// </remarks>
    Task<TemplateAnalyticsResult> GetTemplateAnalyticsAsync(
        Guid tenantId, 
        AnalyticsTimeRange timeRange, 
        TemplateAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves comprehensive workflow analytics with execution metrics, efficiency insights, and optimization data.
    /// Provides detailed workflow analysis including execution success rates, performance benchmarks, bottleneck analysis,
    /// and optimization recommendations for workflow development and operational efficiency workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="timeRange">Time range for workflow analytics aggregation</param>
    /// <param name="filters">Optional filtering criteria for workflow analytics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive workflow analytics with execution and optimization insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Workflow Analytics:</strong></para>
    /// <list type="bullet">
    ///   <item>Workflow execution success rates and performance metrics</item>
    ///   <item>Bottleneck identification and efficiency analysis</item>
    ///   <item>Resource utilization and cost optimization insights</item>
    ///   <item>Workflow optimization and design improvement recommendations</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements workflow execution tracking and performance analysis</item>
    ///   <item>Supports bottleneck identification and efficiency optimization</item>
    ///   <item>Provides workflow design and structure optimization insights</item>
    ///   <item>Includes automation opportunities and process improvement recommendations</item>
    /// </list>
    /// </remarks>
    Task<WorkflowAnalyticsResult> GetWorkflowAnalyticsAsync(
        Guid tenantId, 
        AnalyticsTimeRange timeRange, 
        WorkflowAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Cost Analysis & Financial Analytics

    /// <summary>
    /// Retrieves comprehensive cost analytics with resource utilization, expense tracking, and optimization insights.
    /// Provides detailed cost analysis including AI model usage costs, resource consumption, billing analysis,
    /// and cost optimization recommendations for financial efficiency and budget management workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="timeRange">Time range for cost analytics aggregation</param>
    /// <param name="filters">Optional filtering criteria for cost analytics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive cost analytics with optimization and budgeting insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Cost Analytics:</strong></para>
    /// <list type="bullet">
    ///   <item>AI model usage costs and resource consumption analysis</item>
    ///   <item>Billing breakdown and expense categorization</item>
    ///   <item>Cost optimization opportunities and recommendations</item>
    ///   <item>Budget tracking and forecasting insights</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements detailed cost tracking and categorization</item>
    ///   <item>Supports cost optimization analysis and recommendations</item>
    ///   <item>Provides budget forecasting and variance analysis</item>
    ///   <item>Includes ROI analysis and value optimization insights</item>
    /// </list>
    /// </remarks>
    Task<CostAnalyticsResult> GetCostAnalyticsAsync(
        Guid tenantId, 
        AnalyticsTimeRange timeRange, 
        CostAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves resource utilization analytics with capacity planning and optimization insights.
    /// Provides detailed resource analysis including compute utilization, storage consumption, network usage,
    /// and capacity planning recommendations for infrastructure optimization and cost management workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="timeRange">Time range for resource analytics aggregation</param>
    /// <param name="filters">Optional filtering criteria for resource analytics</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive resource analytics with capacity and optimization insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Resource Analytics:</strong></para>
    /// <list type="bullet">
    ///   <item>Compute utilization and performance metrics</item>
    ///   <item>Storage consumption and growth analysis</item>
    ///   <item>Network usage and bandwidth optimization</item>
    ///   <item>Capacity planning and scaling recommendations</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements comprehensive resource monitoring and analysis</item>
    ///   <item>Supports capacity planning and scaling optimization</item>
    ///   <item>Provides resource optimization and cost reduction insights</item>
    ///   <item>Includes predictive capacity planning and growth forecasting</item>
    /// </list>
    /// </remarks>
    Task<ResourceAnalyticsResult> GetResourceAnalyticsAsync(
        Guid tenantId, 
        AnalyticsTimeRange timeRange, 
        ResourceAnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Predictive Analytics & Insights

    /// <summary>
    /// Generates predictive analytics and forecasting insights based on historical data and machine learning models.
    /// Provides intelligent forecasting including usage predictions, performance trends, cost projections,
    /// and optimization recommendations for strategic planning and proactive optimization workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="predictionType">Type of prediction analysis to perform</param>
    /// <param name="timeHorizon">Time horizon for prediction analysis</param>
    /// <param name="options">Optional prediction configuration and modeling parameters</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive predictive analytics with forecasting and optimization insights</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <exception cref="ArgumentException">Thrown when prediction parameters are invalid</exception>
    /// <remarks>
    /// <para><strong>Predictive Capabilities:</strong></para>
    /// <list type="bullet">
    ///   <item>Usage growth forecasting and trend prediction</item>
    ///   <item>Performance trend analysis and capacity planning</item>
    ///   <item>Cost forecasting and budget planning insights</item>
    ///   <item>Optimization opportunities and recommendation priorities</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Uses machine learning models for accurate prediction analysis</item>
    ///   <item>Supports multiple prediction types and time horizons</item>
    ///   <item>Provides confidence intervals and uncertainty analysis</item>
    ///   <item>Includes actionable insights and strategic recommendations</item>
    /// </list>
    /// </remarks>
    Task<PredictiveAnalyticsResult> GetPredictiveAnalyticsAsync(
        Guid tenantId, 
        PredictionType predictionType, 
        PredictionTimeHorizon timeHorizon, 
        PredictiveAnalyticsOptions? options = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates comprehensive optimization recommendations based on analytics data and machine learning insights.
    /// Provides intelligent optimization suggestions including performance improvements, cost reductions,
    /// efficiency enhancements, and strategic recommendations for platform optimization and business growth.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="optimizationScope">Scope of optimization analysis (platform, domain, or specific entity)</param>
    /// <param name="options">Optional optimization configuration and analysis parameters</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive optimization recommendations with implementation guidance</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <exception cref="ArgumentException">Thrown when optimization parameters are invalid</exception>
    /// <remarks>
    /// <para><strong>Optimization Analysis:</strong></para>
    /// <list type="bullet">
    ///   <item>Performance optimization opportunities and implementation strategies</item>
    ///   <item>Cost reduction recommendations and efficiency improvements</item>
    ///   <item>Process optimization and workflow enhancement suggestions</item>
    ///   <item>Strategic recommendations for platform growth and scaling</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Uses advanced algorithms for optimization opportunity identification</item>
    ///   <item>Provides prioritized recommendations with impact analysis</item>
    ///   <item>Includes implementation guidance and best practice recommendations</item>
    ///   <item>Supports continuous optimization monitoring and improvement tracking</item>
    /// </list>
    /// </remarks>
    Task<OptimizationRecommendationsResult> GetOptimizationRecommendationsAsync(
        Guid tenantId, 
        OptimizationScope optimizationScope, 
        OptimizationAnalysisOptions? options = null, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Analytics Export & Reporting

    /// <summary>
    /// Exports analytics data in various formats for external analysis and reporting workflows.
    /// Provides comprehensive data export capabilities including CSV, JSON, Excel formats with
    /// customizable data selection, formatting options, and scheduled export capabilities.
    /// </summary>
    /// <param name="exportRequest">Export configuration with data selection and formatting options</param>
    /// <param name="tenantId">Tenant identifier for export scope</param>
    /// <param name="exportedBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Export result with download information and export metadata</returns>
    /// <exception cref="ArgumentException">Thrown when export configuration is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Export Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Multiple export formats (CSV, JSON, Excel, PDF)</item>
    ///   <item>Customizable data selection and filtering</item>
    ///   <item>Scheduled and on-demand export capabilities</item>
    ///   <item>Secure export with access control and audit trails</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements secure data export with comprehensive access control</item>
    ///   <item>Supports large dataset export with streaming and pagination</item>
    ///   <item>Provides export progress tracking and status monitoring</item>
    ///   <item>Creates detailed export audit trail for compliance</item>
    /// </list>
    /// </remarks>
    Task<AnalyticsExportResult> ExportAnalyticsDataAsync(
        AnalyticsExportRequest exportRequest, 
        Guid tenantId, 
        Guid exportedBy, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates comprehensive analytics reports with visualizations and business intelligence insights.
    /// Provides automated report generation including executive dashboards, operational reports,
    /// compliance documentation, and custom analytics reports for stakeholder communication workflows.
    /// </summary>
    /// <param name="reportRequest">Report configuration with template and customization options</param>
    /// <param name="tenantId">Tenant identifier for report scope</param>
    /// <param name="generatedBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Report generation result with download information and report metadata</returns>
    /// <exception cref="ArgumentException">Thrown when report configuration is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Report Features:</strong></para>
    /// <list type="bullet">
    ///   <item>Pre-built report templates for common analytics scenarios</item>
    ///   <item>Customizable visualizations and dashboard components</item>
    ///   <item>Automated report generation and distribution</item>
    ///   <item>Interactive reports with drill-down capabilities</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements comprehensive report generation with visualization libraries</item>
    ///   <item>Supports scheduled report generation and automated distribution</item>
    ///   <item>Provides interactive report capabilities with real-time data</item>
    ///   <item>Creates detailed report audit trail and access tracking</item>
    /// </list>
    /// </remarks>
    Task<AnalyticsReportResult> GenerateAnalyticsReportAsync(
        AnalyticsReportRequest reportRequest, 
        Guid tenantId, 
        Guid generatedBy, 
        CancellationToken cancellationToken = default);

    #endregion
}
