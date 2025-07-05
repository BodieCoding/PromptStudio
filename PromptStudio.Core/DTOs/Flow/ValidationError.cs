namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents a critical validation error that prevents flow execution, providing detailed error context and categorization.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Error reporting DTO used by validation services, development tools, and quality assurance systems.
/// Essential for providing actionable feedback to developers and automated validation pipelines.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Structured error information with node context and categorization for efficient error handling and reporting.
/// Designed for integration with development tools, error tracking systems, and automated quality gates.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Development IDE error reporting and highlighting</item>
/// <item>Automated validation in CI/CD pipelines</item>
/// <item>Error categorization and resolution guidance</item>
/// <item>Quality metrics and flow health monitoring</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight error representation optimized for collection processing and real-time feedback.
/// Error messages should be concise yet informative. Type categorization enables efficient error filtering and prioritization.</para>
/// </remarks>
public class ValidationError
{
    /// <summary>
    /// Gets or sets the identifier of the node where the error occurred.
    /// </summary>
    /// <value>A string identifying the specific node with the error, or null if the error is flow-level.</value>
    public string? NodeId { get; set; }

    /// <summary>
    /// Gets or sets the human-readable error message describing the validation issue.
    /// </summary>
    /// <value>A clear, actionable message explaining the error and potential resolution steps.</value>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the error type for categorization and filtering.
    /// </summary>
    /// <value>A string categorizing the error type (e.g., "connection", "data", "logic", "syntax").</value>
    public string Type { get; set; } = string.Empty;
}
