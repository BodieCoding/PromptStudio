using PromptStudio.Core.Interfaces.Data;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a quality metric measurement for various entities in the PromptStudio system.
/// Used to track and evaluate the quality, performance, and effectiveness of prompts, workflows,
/// and other system components over time.
/// 
/// <para><strong>Domain Context:</strong></para>
/// <para>Quality metrics provide quantitative measurements that enable data-driven decision making
/// about prompt effectiveness, workflow optimization, and system performance improvements.
/// These metrics are used for analytics, reporting, and continuous improvement processes.</para>
/// 
/// <para><strong>Measurement Categories:</strong></para>
/// <para>Supports various metric types including accuracy, performance, user satisfaction,
/// cost-effectiveness, and business impact measurements. Metrics can be aggregated and
/// analyzed across different time periods and entity groupings.</para>
/// </summary>
/// <remarks>
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Track prompt template effectiveness and accuracy over time</description></item>
/// <item><description>Measure workflow execution performance and success rates</description></item>
/// <item><description>Monitor system-wide quality trends and improvements</description></item>
/// <item><description>Generate quality reports and analytics dashboards</description></item>
/// <item><description>Support A/B testing and comparative quality analysis</description></item>
/// </list>
/// 
/// <para><strong>Metric Types:</strong></para>
/// <list type="bullet">
/// <item><description>Accuracy: Correctness and precision of outputs</description></item>
/// <item><description>Performance: Speed, efficiency, and resource utilization</description></item>
/// <item><description>Satisfaction: User feedback and rating scores</description></item>
/// <item><description>Cost: Economic efficiency and resource optimization</description></item>
/// <item><description>Engagement: Usage patterns and adoption metrics</description></item>
/// </list>
/// </remarks>
public class QualityMetric : AuditableEntity
{
    /// <summary>
    /// Gets or sets the type of entity this metric measures.
    /// Specifies the domain entity category (e.g., "PromptTemplate", "Workflow", "Execution").
    /// </summary>
    /// <value>A string representing the entity type being measured.</value>
    /// <example>
    /// <code>
    /// EntityType = "PromptTemplate";  // Measuring a prompt template
    /// EntityType = "WorkflowExecution";  // Measuring a workflow execution
    /// EntityType = "UserInteraction";  // Measuring user interaction quality
    /// </code>
    /// </example>
    public required string EntityType { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the specific entity being measured.
    /// References the primary key of the entity specified in EntityType.
    /// </summary>
    /// <value>The GUID identifier of the measured entity.</value>
    /// <remarks>
    /// This creates a polymorphic relationship allowing quality metrics to be
    /// associated with any type of entity in the system while maintaining referential integrity.
    /// </remarks>
    public required Guid EntityId { get; set; }

    /// <summary>
    /// Gets or sets the category or type of quality metric being measured.
    /// Categorizes the metric for filtering, grouping, and analysis purposes.
    /// </summary>
    /// <value>The quality metric type from the QualityMetricType enumeration.</value>
    /// <example>
    /// <code>
    /// MetricType = QualityMetricType.Accuracy;      // Accuracy measurement
    /// MetricType = QualityMetricType.Performance;   // Performance measurement  
    /// MetricType = QualityMetricType.Satisfaction;  // User satisfaction
    /// MetricType = QualityMetricType.CostEfficiency; // Cost effectiveness
    /// </code>
    /// </example>
    public required QualityMetricType MetricType { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name for this specific metric.
    /// Provides a descriptive label for the metric used in reports and dashboards.
    /// </summary>
    /// <value>A string containing the metric name (max 100 characters).</value>
    /// <example>
    /// <code>
    /// MetricName = "Response Accuracy";
    /// MetricName = "Execution Speed";
    /// MetricName = "User Rating";
    /// MetricName = "Cost Per Token";
    /// </code>
    /// </example>
    public required string MetricName { get; set; }

    /// <summary>
    /// Gets or sets the numerical value of the quality metric.
    /// The primary measurement value with precision based on metric type.
    /// </summary>
    /// <value>A decimal value representing the measured quality metric.</value>
    /// <remarks>
    /// <para><strong>Value Ranges by Metric Type:</strong></para>
    /// <list type="bullet">
    /// <item><description>Accuracy: 0.0 to 1.0 (percentage as decimal)</description></item>
    /// <item><description>Performance: Positive values (time in ms, throughput, etc.)</description></item>
    /// <item><description>Satisfaction: 1.0 to 5.0 (rating scale)</description></item>
    /// <item><description>Cost: Positive decimal values (currency amounts)</description></item>
    /// </list>
    /// </remarks>
    public required decimal Score { get; set; }

    /// <summary>
    /// Gets or sets the unit of measurement for the metric score.
    /// Provides context for interpreting the numerical score value.
    /// </summary>
    /// <value>A string describing the measurement unit (max 20 characters).</value>
    /// <example>
    /// <code>
    /// Unit = "%";          // Percentage
    /// Unit = "ms";         // Milliseconds
    /// Unit = "stars";      // Star rating
    /// Unit = "USD";        // Currency
    /// Unit = "tokens/sec"; // Rate measurement
    /// </code>
    /// </example>
    public string? Unit { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this metric was measured or recorded.
    /// Enables time-series analysis and trend tracking for quality metrics.
    /// </summary>
    /// <value>A DateTimeOffset representing when the measurement was taken.</value>
    /// <remarks>
    /// This timestamp may differ from CreatedAt if the metric represents
    /// historical data being imported or batch-processed measurements.
    /// </remarks>
    public required DateTimeOffset MeasuredAt { get; set; }

    /// <summary>
    /// Gets or sets the time period or duration this metric represents.
    /// Specifies the timespan over which the measurement was aggregated.
    /// </summary>
    /// <value>A TimeSpan representing the measurement period, or null for point-in-time metrics.</value>
    /// <example>
    /// <code>
    /// MeasurementPeriod = TimeSpan.FromHours(24);  // Daily aggregation
    /// MeasurementPeriod = TimeSpan.FromMinutes(5); // 5-minute window
    /// MeasurementPeriod = null;                    // Instantaneous measurement
    /// </code>
    /// </example>
    public TimeSpan? MeasurementPeriod { get; set; }

    /// <summary>
    /// Gets or sets the confidence level or reliability score for this metric.
    /// Indicates the statistical confidence or reliability of the measurement.
    /// </summary>
    /// <value>A decimal from 0.0 to 1.0 representing confidence level, or null if not applicable.</value>
    /// <example>
    /// <code>
    /// Confidence = 0.95m;  // 95% confidence level
    /// Confidence = 0.80m;  // 80% confidence level
    /// Confidence = null;   // Confidence not measured/applicable
    /// </code>
    /// </example>
    public decimal? Confidence { get; set; }

    /// <summary>
    /// Gets or sets additional context or metadata about the metric measurement.
    /// Stores structured data providing additional measurement context and parameters.
    /// </summary>
    /// <value>A JSON string containing metric-specific context data, or null if not needed.</value>
    /// <example>
    /// <code>
    /// Context = "{\"sample_size\": 1000, \"method\": \"automated\", \"version\": \"2.1\"}";
    /// Context = "{\"user_cohort\": \"premium\", \"region\": \"US-East\"}";
    /// </code>
    /// </example>
    public string? Context { get; set; }

    /// <summary>
    /// Gets or sets the source system or method that generated this metric.
    /// Identifies how and where the measurement was obtained for traceability.
    /// </summary>
    /// <value>A string identifying the measurement source (max 50 characters).</value>
    /// <example>
    /// <code>
    /// Source = "AutomatedTesting";
    /// Source = "UserFeedback";  
    /// Source = "SystemMonitoring";
    /// Source = "ManualReview";
    /// </code>
    /// </example>
    public string? Source { get; set; }
}

/// <summary>
/// Enumeration of quality metric types supported in the PromptStudio system.
/// Categorizes different types of measurements for filtering and analysis.
/// </summary>
public enum QualityMetricType
{
    /// <summary>
    /// Accuracy and correctness measurements.
    /// Measures how precise or correct the outputs are.
    /// </summary>
    Accuracy,

    /// <summary>
    /// Performance and efficiency measurements.
    /// Measures speed, throughput, and resource utilization.
    /// </summary>
    Performance,

    /// <summary>
    /// User satisfaction and feedback measurements.
    /// Measures user ratings, feedback scores, and satisfaction levels.
    /// </summary>
    Satisfaction,

    /// <summary>
    /// Cost efficiency and economic measurements.
    /// Measures cost per operation, ROI, and economic efficiency.
    /// </summary>
    CostEfficiency,

    /// <summary>
    /// Engagement and usage measurements.
    /// Measures adoption, usage frequency, and engagement levels.
    /// </summary>
    Engagement,

    /// <summary>
    /// Reliability and stability measurements.
    /// Measures uptime, error rates, and system reliability.
    /// </summary>
    Reliability,

    /// <summary>
    /// Business impact and value measurements.
    /// Measures business outcomes and value delivered.
    /// </summary>
    BusinessImpact,

    /// <summary>
    /// Custom or specialized measurements.
    /// For organization-specific or unique metric types.
    /// </summary>
    Custom
}
