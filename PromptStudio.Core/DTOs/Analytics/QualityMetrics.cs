namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive quality assessment metrics for template and execution performance evaluation.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by quality assurance services, analytics platforms, and optimization systems to evaluate
/// and track template performance quality. Enables systematic quality monitoring, comparative analysis,
/// and data-driven optimization decisions across different templates, libraries, and organizational units.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Provides statistical quality assessment including central tendency measures, variability indicators,
/// and distribution analysis. Supports both real-time quality monitoring and historical trend analysis
/// for comprehensive quality management and continuous improvement processes.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Quality assurance services use this for systematic template evaluation
/// - Analytics services aggregate metrics for quality trend analysis and reporting
/// - Optimization services identify improvement opportunities based on quality patterns
/// - Monitoring services track quality SLAs and trigger alerts for quality degradation
/// - Reporting services generate quality summaries and comparative analysis reports
/// 
/// <para><strong>Statistical Interpretation:</strong></para>
/// AverageScore and MedianScore provide central tendency assessment
/// StandardDeviation indicates quality consistency and predictability
/// ScoreDistribution enables detailed quality pattern analysis and outlier identification
/// 
/// <para><strong>Quality Thresholds:</strong></para>
/// Service layers can establish quality gates based on these metrics
/// Comparative analysis enables benchmarking and best practice identification
/// Trend analysis supports proactive quality management and improvement planning
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for quality assessment
/// var metrics = await qualityService.CalculateMetricsAsync(templateId, timeRange);
/// if (metrics.AverageScore &lt; 0.8 || metrics.StandardDeviation &gt; 0.2) {
///     await alertService.NotifyQualityIssueAsync(templateId, metrics);
/// }
/// var consistencyScore = 1.0 - metrics.StandardDeviation; // Higher = more consistent
/// </code>
/// </example>
public class QualityMetrics
{
    /// <summary>
    /// Calculated average quality score across all evaluated executions.
    /// Provides central tendency assessment for overall quality performance.
    /// Service layers use this as primary quality indicator for template assessment.
    /// </summary>
    public double AverageScore { get; set; }

    /// <summary>
    /// Calculated median quality score providing robust central tendency measure.
    /// Less sensitive to outliers than average, useful for skewed quality distributions.
    /// Quality assurance services use this for balanced quality assessment.
    /// </summary>
    public double MedianScore { get; set; }

    /// <summary>
    /// Standard deviation of quality scores indicating consistency and predictability.
    /// Lower values indicate more consistent quality performance.
    /// Service layers use this to assess quality reliability and optimization needs.
    /// </summary>
    public double StandardDeviation { get; set; }

    /// <summary>
    /// Minimum quality score observed indicating worst-case performance.
    /// Used for quality floor assessment and failure pattern analysis.
    /// Monitoring services can trigger alerts when minimum scores are unacceptable.
    /// </summary>
    public double MinScore { get; set; }

    /// <summary>
    /// Maximum quality score observed indicating best-case performance potential.
    /// Used for quality ceiling assessment and optimization target setting.
    /// Service layers can analyze optimal conditions for quality improvement strategies.
    /// </summary>
    public double MaxScore { get; set; }

    /// <summary>
    /// Distribution of quality scores across defined score ranges for pattern analysis.
    /// Key: score range identifier, Value: count of executions in that range.
    /// Enables detailed quality distribution analysis and outlier identification.
    /// </summary>
    public Dictionary<string, int> ScoreDistribution { get; set; } = [];
}
