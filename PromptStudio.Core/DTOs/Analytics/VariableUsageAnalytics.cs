namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive analytics data for variable usage patterns, performance metrics, and optimization insights.
/// 
/// <para><strong>Service Layer Integration:</strong></para>
/// This DTO aggregates variable usage data from multiple execution records to provide actionable insights
/// for template optimization, variable efficiency analysis, and performance improvement recommendations.
/// Service layers should use this DTO for analytics dashboards, optimization workflows, and reporting systems.
/// 
/// <para><strong>Data Aggregation:</strong></para>
/// Contains pre-computed analytics from execution logs including usage frequencies, success correlations,
/// performance metrics, and optimization recommendations derived from historical execution data.
/// </summary>
/// <remarks>
/// <para><strong>Usage in Service Layers:</strong></para>
/// - Analytics services use this for generating optimization reports
/// - Template services use this for variable performance insights
/// - Execution services populate this from aggregated execution data
/// - Reporting services format this data for dashboard presentation
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// This DTO contains computed analytics that should be cached and refreshed periodically
/// rather than calculated on every request due to the complex aggregations involved.
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage example
/// var analytics = await variableAnalyticsService.GetUsageAnalyticsAsync(templateId);
/// var underutilizedVars = analytics.UnusedVariables;
/// var optimizationCandidates = analytics.HighPerformingVariables;
/// </code>
/// </example>
public class VariableUsageAnalytics
{
    /// <summary>
    /// Template identifier for which usage analytics are calculated.
    /// Links analytics data to specific template for optimization insights.
    /// </summary>
    public Guid TemplateId { get; set; }
    
    /// <summary>
    /// Usage frequency count for each variable across all template executions.
    /// Key: variable name, Value: number of times the variable was used.
    /// Enables identification of frequently used vs. underutilized variables.
    /// </summary>
    public Dictionary<string, long> VariableUsageCount { get; set; } = new();
    
    /// <summary>
    /// Most commonly used values for each variable to identify patterns and defaults.
    /// Key: variable name, Value: list of most frequent values ordered by usage.
    /// Supports default value optimization and value validation improvements.
    /// </summary>
    public Dictionary<string, List<string>> MostCommonValues { get; set; } = new();
    
    /// <summary>
    /// Success rate correlation for each variable indicating execution quality impact.
    /// Key: variable name, Value: success rate (0.0 to 1.0) when variable is used.
    /// Identifies variables that correlate with successful template executions.
    /// </summary>
    public Dictionary<string, double> VariableSuccessRates { get; set; } = new();
    
    /// <summary>
    /// Performance impact analysis showing execution time correlation with variables.
    /// Key: variable name, Value: average additional execution time when variable is used.
    /// Enables identification of performance-impacting variables for optimization.
    /// </summary>
    public Dictionary<string, TimeSpan> VariablePerformance { get; set; } = new();
    
    /// <summary>
    /// List of variables defined but never used in template executions.
    /// Identifies cleanup opportunities and template complexity reduction possibilities.
    /// Service layers can use this for template optimization recommendations.
    /// </summary>
    public List<string> UnusedVariables { get; set; } = new();
    
    /// <summary>
    /// List of variables that consistently correlate with high-performance executions.
    /// Identifies optimization opportunities and best practice patterns for template design.
    /// Service layers can promote these variables for similar template development.
    /// </summary>
    public List<string> HighPerformingVariables { get; set; } = new();
}
