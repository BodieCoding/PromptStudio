namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents a concise performance summary for a library, providing key metrics for dashboard and overview displays.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Lightweight summary DTO for dashboard services, overview screens, and performance comparison displays.
/// Used by analytics services for efficient bulk data retrieval and summary reporting scenarios.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Condensed performance data optimized for list views and summary displays.
/// Contains essential metrics without detailed breakdowns for efficient mass retrieval and presentation.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Library dashboard and overview displays</item>
/// <item>Performance comparison and ranking lists</item>
/// <item>Summary reporting and executive dashboards</item>
/// <item>Quick performance assessment and filtering</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Optimized for bulk retrieval and list display scenarios. Minimal payload size enables efficient
/// collection operations. TokenUsage nesting provides cost visibility without compromising performance.</para>
/// </remarks>
public class LibraryPerformanceSummary
{
    /// <summary>
    /// Gets or sets the unique identifier of the library.
    /// </summary>
    /// <value>A valid GUID representing the library entity.</value>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the display name of the library.
    /// </summary>
    /// <value>A human-readable string identifying the library for display purposes.</value>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the count of templates contained within the library.
    /// </summary>
    /// <value>A non-negative integer representing the number of templates in the library.</value>
    public int TemplateCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions across all library templates.
    /// </summary>
    /// <value>A non-negative integer representing the cumulative execution count.</value>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the overall success rate for the library.
    /// </summary>
    /// <value>A decimal between 0.0 and 1.0 representing the aggregate success rate across all templates.</value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the aggregated token usage statistics for the library.
    /// </summary>
    /// <value>A TokenUsage object containing cumulative token consumption metrics, or null if tracking is disabled.</value>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Gets or sets the total monetary cost incurred by the library.
    /// </summary>
    /// <value>A decimal representing the cumulative cost across all executions, or null if cost tracking is disabled.</value>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Gets or sets the calculated performance score for the library.
    /// </summary>
    /// <value>A decimal score representing overall performance based on multiple factors including success rate, speed, and efficiency.</value>
    public double PerformanceScore { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the most recent execution in the library.
    /// </summary>
    /// <value>A DateTime representing the last activity, or null if no executions have occurred.</value>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the library was created.
    /// </summary>
    /// <value>A DateTime representing the library creation date.</value>
    public DateTime CreatedAt { get; set; }
}
