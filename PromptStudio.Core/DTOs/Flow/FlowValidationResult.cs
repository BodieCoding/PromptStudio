namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents the comprehensive validation result of a flow, including errors, warnings, and overall validity status.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary validation result DTO used by flow validation services, design-time validation, and flow deployment pipelines.
/// Essential for ensuring flow quality before execution and providing developer feedback in flow editors.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Validation result aggregation with categorized errors and warnings for comprehensive flow quality assessment.
/// Designed for efficient validation reporting and integration with development tools and CI/CD pipelines.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Flow design-time validation and real-time feedback</item>
/// <item>Pre-deployment quality gates and automated testing</item>
/// <item>Development tooling integration and error reporting</item>
/// <item>Flow quality assessment and best practice enforcement</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight validation result optimized for real-time feedback scenarios.
/// Error and warning collections should be bounded to prevent excessive feedback that overwhelms developers.
/// Consider summarizing or prioritizing validation issues for complex flows.</para>
/// </remarks>
public class FlowValidationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the flow passes all validation checks.
    /// </summary>
    /// <value>True if the flow is valid and ready for execution; otherwise, false.</value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the collection of validation errors that prevent flow execution.
    /// </summary>
    /// <value>A list of critical errors that must be resolved before the flow can be executed.</value>
    public List<ValidationError> Errors { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of validation warnings that suggest improvements.
    /// </summary>
    /// <value>A list of non-critical issues that should be addressed for optimal flow performance and maintainability.</value>
    public List<ValidationWarning> Warnings { get; set; } = new();
}
