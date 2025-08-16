namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents a performance insight generated from analytics, providing actionable intelligence about system performance.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Used by analytics engines to deliver performance insights, monitoring services for alerting, and optimization services
/// for automated recommendations. Essential for AI-driven performance optimization in LLMOps environments.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Structured insight data with confidence scoring and contextual recommendations.
/// Designed for efficient processing by automated systems and clear presentation to human operators.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Automated performance monitoring and alerting</item>
/// <item>Dashboard insights and recommendation display</item>
/// <item>Performance optimization workflow triggers</item>
/// <item>Historical performance analysis and reporting</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight DTO designed for real-time insight delivery. Data dictionary is flexible but should be kept minimal
/// to prevent serialization overhead. Recommendations list should be bounded to maintain response performance.</para>
/// </remarks>
public class PerformanceInsight
{
    /// <summary>
    /// Gets or sets the type/category of the performance insight.
    /// </summary>
    /// <value>A string identifying the insight category (e.g., "latency", "throughput", "cost").</value>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the human-readable title of the insight.
    /// </summary>
    /// <value>A concise title describing the insight for display purposes.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description of the performance insight.
    /// </summary>
    /// <value>A comprehensive explanation of the insight and its implications.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level of the insight.
    /// </summary>
    /// <value>A string indicating severity (e.g., "low", "medium", "high", "critical").</value>
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the confidence score for this insight.
    /// </summary>
    /// <value>A decimal between 0.0 and 1.0 indicating the confidence level of the insight accuracy.</value>
    public double Confidence { get; set; }

    /// <summary>
    /// Gets or sets additional contextual data supporting the insight.
    /// </summary>
    /// <value>A dictionary containing supporting metrics, thresholds, and contextual information.</value>
    public Dictionary<string, object> Data { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of actionable recommendations based on this insight.
    /// </summary>
    /// <value>A list of specific actions that can be taken to address the insight.</value>
    public List<string> Recommendations { get; set; } = [];
}
