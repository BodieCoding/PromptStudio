namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents a quality issue identified during template analysis, providing detailed information for remediation.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Used by quality assurance services, template validation systems, and governance workflows.
/// Essential for automated quality monitoring and compliance reporting in template management systems.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Structured quality issue data with severity classification and location context.
/// Designed for efficient processing by quality management systems and clear presentation in review workflows.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Automated quality validation and reporting</item>
/// <item>Template review and approval workflows</item>
/// <item>Compliance monitoring and audit trails</item>
/// <item>Quality trend analysis and improvement tracking</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight issue representation optimized for batch processing and reporting.
/// Details dictionary should be kept minimal to maintain efficient serialization in quality report scenarios.</para>
/// </remarks>
public class QualityIssue
{
    /// <summary>
    /// Gets or sets the type/category of the quality issue.
    /// </summary>
    /// <value>A string identifying the issue category (e.g., "syntax", "performance", "security").</value>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level of the quality issue.
    /// </summary>
    /// <value>A string indicating severity (e.g., "low", "medium", "high", "critical").</value>
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the human-readable description of the quality issue.
    /// </summary>
    /// <value>A detailed explanation of the issue and its potential impact.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the location where the quality issue was detected.
    /// </summary>
    /// <value>A string identifying the specific location within the template or system where the issue exists.</value>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional contextual details about the quality issue.
    /// </summary>
    /// <value>A dictionary containing supporting information, metrics, and context for the quality issue.</value>
    public Dictionary<string, object> Details { get; set; } = [];
}
