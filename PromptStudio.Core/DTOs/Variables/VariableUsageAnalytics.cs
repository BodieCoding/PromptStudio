namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Comprehensive analytics data for variable usage patterns, performance metrics, and optimization insights within template execution workflows.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by analytics services to provide detailed variable performance analysis, usage pattern identification,
/// and optimization recommendations for template development teams. Enables data-driven template optimization,
/// variable efficiency analysis, and systematic improvement of template parameter utilization and effectiveness.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains variable-specific usage statistics, performance metrics, success rate analysis, and optimization
/// insights. Supports both real-time variable performance monitoring and historical trend analysis for
/// comprehensive template optimization and variable management decision support.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>Analytics Service - Variable performance tracking and optimization insights</item>
///   <item>Optimization Service - Data-driven template improvement recommendations</item>
///   <item>Development Service - Variable usage pattern analysis and best practice identification</item>
///   <item>Quality Service - Variable performance assessment and quality monitoring</item>
///   <item>Reporting Service - Variable effectiveness dashboards and team analytics</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Template-specific variable analytics with comprehensive usage metrics</item>
///   <item>Performance data enables optimization decision support and efficiency analysis</item>
///   <item>Usage patterns support template development best practice identification</item>
///   <item>Success rate analysis provides quality assessment and improvement insights</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Template optimization workflows and variable efficiency analysis</item>
///   <item>Development team performance dashboards and usage insights</item>
///   <item>Variable effectiveness assessment and improvement planning</item>
///   <item>Template quality monitoring and performance trend analysis</item>
///   <item>Best practice identification and knowledge sharing initiatives</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Analytics calculations may be computationally intensive for high-usage templates</item>
///   <item>Usage count dictionaries can grow large with extensive variable usage</item>
///   <item>Performance metrics should be cached for dashboard responsiveness</item>
///   <item>Consider aggregation strategies for long-term historical data</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for variable optimization analysis
/// var analytics = await variableAnalyticsService.GetUsageAnalyticsAsync(templateId);
/// var underperformingVars = analytics.VariableSuccessRates.Where(v =&gt; v.Value &lt; 0.9).Select(v =&gt; v.Key);
/// if (analytics.UnusedVariables.Any()) {
///     await optimizationService.RecommendVariableRemovalAsync(templateId, analytics.UnusedVariables);
/// }
/// </code>
/// </example>
public class VariableUsageAnalytics
{
    /// <summary>
    /// Unique identifier of the template for which variable usage analytics are calculated.
    /// Used for correlation with template metadata and enables drill-down analysis capabilities.
    /// Critical for template-specific optimization workflows and performance tracking.
    /// </summary>
    public Guid TemplateId { get; set; }
    
    /// <summary>
    /// Dictionary mapping variable names to their usage frequency counts across all executions.
    /// Provides quantitative usage analysis for variable importance assessment and optimization planning.
    /// High usage counts indicate critical variables, low counts suggest candidates for review or removal.
    /// Used by optimization services for data-driven template improvement recommendations.
    /// </summary>
    public Dictionary<string, long> VariableUsageCount { get; set; } = [];
    
    /// <summary>
    /// Dictionary mapping variable names to collections of their most frequently used values.
    /// Enables pattern analysis, common usage identification, and template optimization insights.
    /// Useful for understanding user behavior, identifying popular configurations, and optimizing defaults.
    /// Service layers can analyze value patterns for template improvement and user experience enhancement.
    /// </summary>
    public Dictionary<string, List<string>> MostCommonValues { get; set; } = [];
    
    /// <summary>
    /// Dictionary mapping variable names to their execution success rates as decimal percentages.
    /// Calculated as successful executions divided by total executions for each variable.
    /// Critical for identifying problematic variables and optimization opportunities.
    /// Success rates below 0.9 typically indicate variables requiring attention or optimization.
    /// </summary>
    public Dictionary<string, double> VariableSuccessRates { get; set; } = [];
    
    /// <summary>
    /// Dictionary mapping variable names to their average execution performance time spans.
    /// Provides performance impact analysis for variables affecting template execution speed.
    /// Used for performance optimization, bottleneck identification, and efficiency improvement.
    /// High performance impact variables may require optimization or caching strategies.
    /// </summary>
    public Dictionary<string, TimeSpan> VariablePerformance { get; set; } = [];
    
    /// <summary>
    /// Collection of variable names that have not been used in any recent executions.
    /// Identifies candidates for removal, cleanup, or deprecation to improve template maintainability.
    /// Unused variables may indicate outdated functionality or missed optimization opportunities.
    /// Service layers can use this for template cleanup and maintenance automation workflows.
    /// </summary>
    public List<string> UnusedVariables { get; set; } = [];
    
    /// <summary>
    /// Collection of variable names demonstrating exceptional performance and success rates.
    /// Identifies best-performing variables for pattern analysis and optimization template creation.
    /// High-performing variables can serve as models for improving other variable implementations.
    /// Used for best practice identification and template optimization strategy development.
    /// </summary>
    public List<string> HighPerformingVariables { get; set; } = [];
}
