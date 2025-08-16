namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents comprehensive variable usage analytics, providing insights into variable performance, patterns, and optimization opportunities.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary analytics DTO for template optimization services, variable performance monitoring, and template design recommendations.
/// Used by analytics services for optimization insights, template editors for variable suggestions, and governance services for cleanup recommendations.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Aggregated variable usage data with performance correlations and optimization insights.
/// Contains computed analytics from execution logs including usage frequencies, success correlations, and performance metrics.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Template optimization and variable cleanup recommendations</item>
/// <item>Variable performance analysis and bottleneck identification</item>
/// <item>Template design best practices and pattern identification</item>
/// <item>Variable usage trend analysis and forecasting</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Contains pre-computed analytics that should be cached and refreshed periodically rather than calculated on every request.
/// Dictionary collections may grow large for templates with many variables - consider pagination for detailed views.
/// TimeSpan serialization may require custom converters in some scenarios.</para>
/// </remarks>
public class VariableUsageAnalytics
{
    /// <summary>
    /// Gets or sets the unique identifier of the template for which analytics are calculated.
    /// </summary>
    /// <value>A valid GUID representing the template entity being analyzed.</value>
    public Guid TemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the usage frequency count for each variable across all template executions.
    /// </summary>
    /// <value>A dictionary mapping variable names to their usage counts, enabling identification of frequently used vs. underutilized variables.</value>
    public Dictionary<string, long> VariableUsageCount { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the most commonly used values for each variable to identify patterns and defaults.
    /// </summary>
    /// <value>A dictionary mapping variable names to lists of their most frequent values, ordered by usage frequency.</value>
    public Dictionary<string, List<string>> MostCommonValues { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the success rate correlation for each variable indicating execution quality impact.
    /// </summary>
    /// <value>A dictionary mapping variable names to success rates (0.0 to 1.0) when the variable is used.</value>
    public Dictionary<string, double> VariableSuccessRates { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the performance impact analysis showing execution time correlation with variables.
    /// </summary>
    /// <value>A dictionary mapping variable names to average additional execution time when the variable is used.</value>
    public Dictionary<string, TimeSpan> VariablePerformance { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the list of variables defined but never used in template executions.
    /// </summary>
    /// <value>A list of variable names that are defined but unused, identifying cleanup opportunities.</value>
    public List<string> UnusedVariables { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the list of variables that consistently correlate with high-performance executions.
    /// </summary>
    /// <value>A list of variable names that contribute to optimal template performance, useful for optimization recommendations.</value>
    public List<string> HighPerformingVariables { get; set; } = [];
}
