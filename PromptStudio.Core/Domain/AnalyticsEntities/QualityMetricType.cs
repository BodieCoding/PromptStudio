namespace PromptStudio.Core.Domain;

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
