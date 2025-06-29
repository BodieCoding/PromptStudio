namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines time-based granularity levels for trend analysis and temporal data aggregation in analytics and reporting systems.
/// 
/// <para><strong>Business Context:</strong></para>
/// TrendGranularity enables stakeholders to analyze performance data, usage patterns, and system
/// metrics at different temporal resolutions. This supports both operational monitoring (hourly/daily)
/// and strategic analysis (weekly/monthly), allowing organizations to identify patterns, optimize
/// performance, and make informed decisions based on appropriate time horizons.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The granularity enum drives data aggregation strategies, query optimization, and visualization
/// rendering within analytics pipelines. Different granularities require different indexing strategies,
/// caching approaches, and computational resources for efficient data processing and presentation.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Flexible temporal analysis for different business needs and time horizons
/// - Optimized data aggregation and query performance for various time scales
/// - Consistent temporal framework across all analytics and reporting features
/// - Scalable approach to handle large datasets with appropriate granularity
/// - User-friendly time-based filtering and visualization options
/// </summary>
/// <remarks>
/// <para><strong>Usage Patterns:</strong></para>
/// - Hourly: Real-time monitoring, operational dashboards, immediate issue detection
/// - Daily: Standard business reporting, day-over-day comparisons, operational analysis
/// - Weekly: Business cycle analysis, weekly performance reviews, trend identification
/// - Monthly: Strategic analysis, business planning, long-term trend evaluation
/// 
/// <para><strong>Data Aggregation Considerations:</strong></para>
/// - Hourly: High volume, minimal aggregation, real-time processing requirements
/// - Daily: Moderate volume, standard aggregation, batch processing suitable
/// - Weekly: Lower volume, significant aggregation, business cycle alignment
/// - Monthly: Lowest volume, maximum aggregation, strategic planning focus
/// 
/// <para><strong>Performance Implications:</strong></para>
/// - Index strategy should align with most common granularity queries
/// - Caching policies may differ based on granularity and data volatility
/// - Pre-aggregated views recommended for frequently accessed granularities
/// - Query optimization varies significantly across different time scales
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Analytics Engine: Temporal aggregation and calculation strategies
/// - Reporting System: Time-based filtering and visualization options
/// - Dashboard Framework: Granularity selection and display optimization
/// - Data Pipeline: ETL processes and temporal data organization
/// - Caching Layer: Granularity-aware cache invalidation and storage
/// </remarks>
/// <example>
/// <code>
/// // Configure analytics query with appropriate granularity
/// var metrics = await analyticsService.GetTrendDataAsync(
///     startDate: DateTime.UtcNow.AddDays(-30),
///     endDate: DateTime.UtcNow,
///     granularity: TrendGranularity.Daily,
///     metrics: new[] { "execution_count", "success_rate", "average_latency" }
/// );
/// 
/// // Adjust visualization based on granularity
/// var chartConfig = granularity switch
/// {
///     TrendGranularity.Hourly => new ChartConfig { MaxDataPoints = 168 }, // 7 days
///     TrendGranularity.Daily => new ChartConfig { MaxDataPoints = 90 },   // 3 months
///     TrendGranularity.Weekly => new ChartConfig { MaxDataPoints = 52 },  // 1 year
///     TrendGranularity.Monthly => new ChartConfig { MaxDataPoints = 24 }, // 2 years
///     _ => throw new ArgumentException("Unsupported granularity")
/// };
/// </code>
/// </example>
public enum TrendGranularity
{
    /// <summary>
    /// Hour-by-hour temporal granularity for real-time monitoring and immediate operational insights.
    /// <value>0 - Data aggregated and analyzed at hourly intervals</value>
    /// </summary>
    /// <remarks>
    /// Ideal for operational monitoring, real-time dashboards, and immediate issue detection.
    /// Generates high-volume data requiring efficient processing and storage strategies.
    /// </remarks>
    Hourly = 0,

    /// <summary>
    /// Day-by-day temporal granularity for standard business reporting and operational analysis.
    /// <value>1 - Data aggregated and analyzed at daily intervals</value>
    /// </summary>
    /// <remarks>
    /// Most common granularity for business reporting, performance tracking, and day-over-day comparisons.
    /// Balances detail level with manageable data volumes for analysis and visualization.
    /// </remarks>
    Daily = 1,

    /// <summary>
    /// Week-by-week temporal granularity for business cycle analysis and medium-term trend identification.
    /// <value>2 - Data aggregated and analyzed at weekly intervals</value>
    /// </summary>
    /// <remarks>
    /// Aligns with business cycles and planning periods, smooths out daily fluctuations
    /// to reveal underlying trends and patterns in system usage and performance.
    /// </remarks>
    Weekly = 2,

    /// <summary>
    /// Month-by-month temporal granularity for strategic analysis and long-term planning.
    /// <value>3 - Data aggregated and analyzed at monthly intervals</value>
    /// </summary>
    /// <remarks>
    /// Supports strategic decision-making, business planning, and long-term trend analysis.
    /// Provides highest level of aggregation for identifying seasonal patterns and growth trends.
    /// </remarks>
    Monthly = 3
}