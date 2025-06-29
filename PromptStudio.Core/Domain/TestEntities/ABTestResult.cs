using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents an individual execution result within an A/B testing experiment for comprehensive performance tracking and statistical analysis.
/// 
/// <para><strong>Business Context:</strong></para>
/// ABTestResult captures detailed execution data for each test iteration, enabling organizations
/// to build comprehensive datasets for statistical analysis, performance optimization, and 
/// data-driven decision making. Each result represents a single user interaction or system
/// execution that contributes to the overall experiment outcomes and insights.
/// 
/// <para><strong>Technical Context:</strong></para>
/// ABTestResult serves as the fundamental data unit for A/B testing analytics, providing
/// granular execution tracking with session correlation, performance metrics, and error
/// handling within enterprise-grade audit and multi-tenancy frameworks for scalable
/// experiment management and analysis.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Granular execution tracking for detailed performance analysis
/// - Session-based correlation for user journey understanding
/// - Multi-metric data collection for comprehensive experiment insights
/// - Error tracking and debugging support for experiment reliability
/// - Statistical foundation for data-driven optimization decisions
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Event Sourcing: Immutable execution records for audit trails
/// - Observer Pattern: Real-time metrics collection and aggregation
/// - Strategy Pattern: Different metrics collection for various entity types
/// - Data Analytics: Foundation for statistical analysis and reporting
/// - Multi-tenancy: Organizational data isolation and security
/// 
/// <para><strong>Data Collection Strategy:</strong></para>
/// - Primary metrics: Core success/failure indicators
/// - Secondary metrics: Supporting performance and quality measures
/// - Execution context: Environmental and configuration data
/// - Error handling: Comprehensive failure analysis and debugging
/// - Cost tracking: Resource utilization and optimization insights
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index on ABTestId and VariantId for efficient aggregation
/// - Index on ExecutionTime for time-based analysis
/// - Consider partitioning by date for large-scale experiments
/// - Optimize JSON storage for secondary metrics and context data
/// - Monitor storage growth for long-running experiments
/// 
/// <para><strong>Analytics Integration:</strong></para>
/// - Real-time aggregation for live experiment monitoring
/// - Batch processing for statistical significance testing
/// - Time-series analysis for trend identification
/// - Cohort analysis for user behavior understanding
/// - Cost analysis for resource optimization
/// 
/// <para><strong>Compliance and Audit:</strong></para>
/// - Complete execution audit trail for regulatory compliance
/// - Data retention policies for privacy and governance
/// - Anonymization support for sensitive user data
/// - Cross-tenant data isolation for security
/// - Immutable records for experiment integrity
/// </remarks>
/// <example>
/// <code>
/// // Record successful execution result
/// var result = new ABTestResult
/// {
///     ABTestId = experiment.Id,
///     VariantId = selectedVariant.Id,
///     SessionId = userSession.Id,
///     UserId = user?.Id,
///     ExecutionTime = DateTime.UtcNow,
///     PrimaryMetricValue = responseQuality,
///     Success = responseQuality >= qualityThreshold,
///     SecondaryMetricValues = JsonSerializer.Serialize(new
///     {
///         ResponseTime = stopwatch.ElapsedMilliseconds,
///         TokenCount = response.TokenUsage,
///         UserSatisfaction = feedbackScore
///     }),
///     DurationMs = (int)stopwatch.ElapsedMilliseconds,
///     Cost = CalculateExecutionCost(response),
///     QualityScore = EvaluateResponseQuality(response)
/// };
/// 
/// await repository.AddAsync(result);
/// 
/// // Aggregate results for real-time analysis
/// var conversionRate = await repository.GetConversionRateAsync(
///     variantId, 
///     DateTimeOffset.UtcNow.AddHours(-24)
/// );
/// </code>
/// </example>
public class ABTestResult : AuditableEntity
{
    /// <summary>
    /// Reference to the parent A/B testing experiment for result correlation and aggregation.
    /// <value>Guid identifier linking this result to its parent experiment</value>
    /// </summary>
    /// <remarks>
    /// Enables efficient result grouping and experiment-level analysis.
    /// Used for cross-variant comparisons and overall experiment success metrics.
    /// </remarks>
    public Guid ABTestId { get; set; }

    /// <summary>
    /// Navigation property to the parent A/B testing experiment.
    /// <value>ABTest entity containing experiment configuration and metadata</value>
    /// </summary>
    /// <remarks>
    /// Provides access to experiment context, configuration, and other variants
    /// for comprehensive analysis and result interpretation.
    /// </remarks>
    public virtual ABTest ABTest { get; set; } = null!;

    /// <summary>
    /// Reference to the specific variant that produced this execution result.
    /// <value>Guid identifier linking this result to the tested variant</value>
    /// </summary>
    /// <remarks>
    /// Critical for variant-specific performance analysis and comparison.
    /// Enables traffic allocation validation and variant effectiveness measurement.
    /// </remarks>
    public Guid VariantId { get; set; }

    /// <summary>
    /// Navigation property to the variant that generated this result.
    /// <value>ABTestVariant entity containing variant configuration and metadata</value>
    /// </summary>
    /// <remarks>
    /// Provides access to variant-specific configuration, traffic allocation,
    /// and other variant properties for contextual result analysis.
    /// </remarks>
    public virtual ABTestVariant Variant { get; set; } = null!;

    /// <summary>
    /// Unique session identifier for correlating multiple executions within a user session.
    /// <value>String identifier for session-based analysis and user journey tracking</value>
    /// </summary>
    /// <remarks>
    /// Enables session-based analytics, user journey mapping, and multi-step
    /// experiment analysis. Critical for understanding user interaction patterns.
    /// </remarks>
    [StringLength(100)]
    public string SessionId { get; set; } = string.Empty;

    /// <summary>
    /// Optional user identifier for user-specific analysis and personalization insights.
    /// <value>String identifier for user-based analytics and behavior tracking</value>
    /// </summary>
    /// <remarks>
    /// Supports user-level experiment analysis while respecting privacy requirements.
    /// May be anonymized or pseudonymized based on compliance needs.
    /// </remarks>
    [StringLength(100)]
    public string? UserId { get; set; }

    /// <summary>
    /// Precise timestamp of when this execution occurred for temporal analysis.
    /// <value>DateTime in UTC representing the exact execution time</value>
    /// </summary>
    /// <remarks>
    /// Essential for time-series analysis, experiment duration tracking,
    /// and temporal correlation with external events or system changes.
    /// </remarks>
    public DateTime ExecutionTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Quantitative result for the experiment's primary success metric.
    /// <value>Decimal value representing the primary metric measurement</value>
    /// </summary>
    /// <remarks>
    /// Core metric for statistical analysis and variant comparison.
    /// Definition and scale determined by experiment configuration and entity type.
    /// </remarks>
    public decimal? PrimaryMetricValue { get; set; }

    /// <summary>
    /// Additional metrics captured during execution for comprehensive performance analysis.
    /// <value>JSON string containing secondary metric names and values</value>
    /// </summary>
    /// <remarks>
    /// Flexible structure for capturing entity-specific metrics, quality indicators,
    /// performance measurements, and custom evaluation criteria.
    /// </remarks>
    public string? SecondaryMetricValues { get; set; }

    /// <summary>
    /// Boolean indicator of whether this execution met the primary success criteria.
    /// <value>True if execution succeeded according to primary metric threshold</value>
    /// </summary>
    /// <remarks>
    /// Simplified success indicator for conversion rate calculations
    /// and binary success/failure analysis across variants.
    /// </remarks>
    public bool Success { get; set; } = false;

    /// <summary>
    /// Contextual information about the execution environment and parameters.
    /// <value>JSON string containing execution context, parameters, and environmental data</value>
    /// </summary>
    /// <remarks>
    /// Captures execution conditions, input parameters, system state,
    /// and other contextual factors that may influence result interpretation.
    /// </remarks>
    public string? ExecutionContext { get; set; }

    /// <summary>
    /// Error message details when execution fails for debugging and analysis.
    /// <value>String containing error details and failure information</value>
    /// </summary>
    /// <remarks>
    /// Critical for experiment reliability analysis, error pattern identification,
    /// and variant stability assessment. Helps identify problematic configurations.
    /// </remarks>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Execution duration in milliseconds for performance analysis and optimization.
    /// <value>Integer representing execution time in milliseconds</value>
    /// </summary>
    /// <remarks>
    /// Key performance indicator for system efficiency, user experience,
    /// and resource utilization analysis across different variants.
    /// </remarks>
    public int? DurationMs { get; set; }

    /// <summary>
    /// Financial cost of this execution for resource optimization and budget analysis.
    /// <value>Decimal representing the monetary cost of this execution</value>
    /// </summary>
    /// <remarks>
    /// Enables cost-effectiveness analysis, budget tracking, and ROI calculation
    /// for different variants and experiment configurations.
    /// </remarks>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Qualitative assessment score for this execution's output quality.
    /// <value>Decimal score representing output quality on a defined scale</value>
    /// </summary>
    /// <remarks>
    /// Subjective or computed quality metric for output evaluation,
    /// user satisfaction tracking, and variant quality comparison.
    /// </remarks>
    public decimal? QualityScore { get; set; }
}
