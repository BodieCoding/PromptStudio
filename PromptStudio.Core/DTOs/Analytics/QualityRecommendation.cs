namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents a quality improvement recommendation generated from analytics, providing actionable guidance for optimization.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Used by quality optimization services, template improvement workflows, and governance systems.
/// Essential for AI-driven quality enhancement and automated improvement suggestion systems in LLMOps environments.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Structured recommendation data with priority classification and impact assessment.
/// Designed for efficient processing by automated improvement systems and clear presentation in optimization workflows.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Automated quality improvement suggestions</item>
/// <item>Template optimization workflow guidance</item>
/// <item>Prioritized improvement planning and roadmaps</item>
/// <item>Quality enhancement tracking and measurement</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight recommendation DTO optimized for suggestion engines and batch processing.
/// Action items list should be bounded to maintain response performance and user comprehension.</para>
/// </remarks>
public class QualityRecommendation
{
    /// <summary>
    /// Gets or sets the type/category of the quality recommendation.
    /// </summary>
    /// <value>A string identifying the recommendation category (e.g., "performance", "security", "usability").</value>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the human-readable title of the recommendation.
    /// </summary>
    /// <value>A concise title describing the recommendation for display purposes.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description of the quality recommendation.
    /// </summary>
    /// <value>A comprehensive explanation of the recommendation and its benefits.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority level of the recommendation.
    /// </summary>
    /// <value>A string indicating priority (e.g., "low", "medium", "high", "critical").</value>
    public string Priority { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expected impact score of implementing this recommendation.
    /// </summary>
    /// <value>A decimal representing the anticipated improvement impact, typically normalized to a 0.0-1.0 scale.</value>
    public double ExpectedImpact { get; set; }

    /// <summary>
    /// Gets or sets the list of specific action items to implement the recommendation.
    /// </summary>
    /// <value>A list of concrete steps that can be taken to implement the quality improvement.</value>
    public List<string> ActionItems { get; set; } = [];
}
