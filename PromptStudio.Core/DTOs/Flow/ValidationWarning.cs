namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents a non-critical validation warning that suggests flow improvements or best practice recommendations.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Advisory feedback DTO used by validation services, code quality tools, and optimization recommendations.
/// Essential for providing guidance on flow improvements and best practices without blocking execution.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Structured warning information with node context and categorization for improvement suggestions and best practices.
/// Designed for integration with development tools, quality analyzers, and optimization recommendation systems.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Development IDE improvement suggestions and best practice hints</item>
/// <item>Code quality assessment and optimization recommendations</item>
/// <item>Performance improvement guidance and efficiency tips</item>
/// <item>Flow maintainability and design pattern suggestions</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight warning representation optimized for advisory feedback scenarios.
/// Warning messages should provide actionable improvement suggestions. Type categorization enables filtering by concern area.</para>
/// </remarks>
public class ValidationWarning
{
    /// <summary>
    /// Gets or sets the identifier of the node where the warning was identified.
    /// </summary>
    /// <value>A string identifying the specific node with the warning, or null if the warning is flow-level.</value>
    public string? NodeId { get; set; }

    /// <summary>
    /// Gets or sets the human-readable warning message describing the improvement opportunity.
    /// </summary>
    /// <value>A clear, helpful message explaining the warning and suggested improvements.</value>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the warning type for categorization and filtering.
    /// </summary>
    /// <value>A string categorizing the warning type (e.g., "performance", "best_practice", "optimization").</value>
    public string Type { get; set; } = string.Empty;
}
