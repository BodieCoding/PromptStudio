namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents comprehensive performance analytics for a library, including execution metrics, template performance, and model insights.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Core analytics DTO for library performance monitoring, optimization services, and operational dashboards.
/// Used by performance monitoring systems, cost optimization engines, and capacity planning services.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Aggregated performance data with nested model and template metrics.
/// Designed for efficient analysis while maintaining detailed breakdown capabilities for optimization workflows.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Library performance monitoring and optimization</item>
/// <item>Model performance comparison and selection</item>
/// <item>Template efficiency analysis and improvement</item>
/// <item>Cost optimization and resource allocation</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Contains nested performance objects and collections that may grow large for active libraries.
/// Consider caching for dashboard scenarios and pagination for detailed views. Custom metrics should be bounded
/// to prevent payload inflation in performance monitoring scenarios.</para>
/// </remarks>
public class LibraryPerformanceAnalytics
{
    /// <summary>
    /// Gets or sets the unique identifier of the library being analyzed.
    /// </summary>
    /// <value>A valid GUID representing the library entity.</value>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the overall success rate across all library executions.
    /// </summary>
    /// <value>A decimal between 0.0 and 1.0 representing the aggregate success rate for the library.</value>
    public double OverallSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average response time across all library executions.
    /// </summary>
    /// <value>A TimeSpan representing the mean execution time for all templates in the library.</value>
    public TimeSpan AverageResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions recorded for this library.
    /// </summary>
    /// <value>A non-negative integer representing the cumulative execution count across all templates.</value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the performance metrics breakdown by individual template.
    /// </summary>
    /// <value>A dictionary mapping template identifiers to their performance scores for comparative analysis.</value>
    public Dictionary<string, double> TemplatePerformance { get; set; } = [];

    /// <summary>
    /// Gets or sets the performance metrics breakdown by model used in the library.
    /// </summary>
    /// <value>A dictionary mapping model names to their detailed performance metrics within this library context.</value>
    public Dictionary<string, ModelPerformanceMetrics> ModelPerformance { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of performance insights generated from the analytics.
    /// </summary>
    /// <value>A list of performance insights providing actionable intelligence for optimization.</value>
    public List<PerformanceInsight> Insights { get; set; } = [];

    /// <summary>
    /// Gets or sets additional custom performance metrics specific to this library.
    /// </summary>
    /// <value>A dictionary containing extended metrics and library-specific performance indicators.</value>
    public Dictionary<string, object> CustomMetrics { get; set; } = [];
}
