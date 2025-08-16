namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents comprehensive quality analysis for a library, including metrics, issues, and improvement recommendations.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary DTO for library quality assurance services, governance workflows, and compliance monitoring.
/// Used by quality management systems, automated review processes, and continuous improvement workflows.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Complete quality analysis package with scoring, issue identification, and actionable recommendations.
/// Designed for comprehensive quality reporting and automated quality management processes.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Library quality assessment and certification</item>
/// <item>Automated quality monitoring and alerting</item>
/// <item>Quality improvement planning and tracking</item>
/// <item>Compliance reporting and audit support</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Complex analysis object containing nested collections and detailed metrics.
/// Consider pagination for libraries with many issues/recommendations. Detailed analysis should be kept focused
/// to prevent excessive payload sizes in quality reporting scenarios.</para>
/// </remarks>
public class LibraryQualityAnalysis
{
    /// <summary>
    /// Gets or sets the unique identifier of the library being analyzed.
    /// </summary>
    /// <value>A valid GUID representing the library entity.</value>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the overall quality score for the library.
    /// </summary>
    /// <value>A decimal score (typically 0.0 to 1.0) representing the overall quality assessment.</value>
    public double OverallQualityScore { get; set; }

    /// <summary>
    /// Gets or sets the detailed quality metrics breakdown by category.
    /// </summary>
    /// <value>A dictionary mapping quality metric names to their scores, providing granular quality insights.</value>
    public Dictionary<string, double> QualityMetrics { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of quality issues identified in the library.
    /// </summary>
    /// <value>A list of quality issues requiring attention, ordered by severity and impact.</value>
    public List<QualityIssue> Issues { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of quality improvement recommendations.
    /// </summary>
    /// <value>A list of actionable recommendations for improving library quality, prioritized by impact.</value>
    public List<QualityRecommendation> Recommendations { get; set; } = [];

    /// <summary>
    /// Gets or sets additional detailed analysis data and supporting metrics.
    /// </summary>
    /// <value>A dictionary containing extended analysis results, statistical data, and quality trend information.</value>
    public Dictionary<string, object> DetailedAnalysis { get; set; } = [];
}
