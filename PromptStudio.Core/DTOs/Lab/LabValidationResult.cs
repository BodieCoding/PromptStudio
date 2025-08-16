namespace PromptStudio.Core.DTOs.Lab;

/// <summary>
/// Represents comprehensive validation results for a lab environment, including all libraries, workflows, and aggregated quality assessments.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary validation result DTO for lab management services, quality assurance systems, and deployment pipelines.
/// Used by lab governance services for environment validation, pre-deployment quality gates, and comprehensive compliance checking.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Complete lab validation aggregation with nested library and workflow validation summaries.
/// Designed for comprehensive quality assessment and hierarchical validation reporting across entire lab environments.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Lab environment quality gates and deployment validation</item>
/// <item>Comprehensive compliance checking and governance reporting</item>
/// <item>Multi-component validation with detailed breakdown</item>
/// <item>Quality assurance dashboards and management interfaces</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Contains nested validation collections that may grow large for complex lab environments.
/// Consider pagination or summarization for labs with many libraries and workflows. Validation results should be cached
/// to avoid repeated expensive validation operations.</para>
/// </remarks>
public class LabValidationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the entire lab environment passes all validation checks.
    /// </summary>
    /// <value>True if all libraries, workflows, and lab-level validations are successful; otherwise, false.</value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the collection of critical validation errors that prevent lab deployment.
    /// </summary>
    /// <value>A list of lab-level errors that must be resolved before the lab can be considered valid.</value>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of validation warnings suggesting improvements.
    /// </summary>
    /// <value>A list of non-critical issues that should be addressed for optimal lab performance and maintainability.</value>
    public List<string> Warnings { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of optimization recommendations for the lab.
    /// </summary>
    /// <value>A list of suggested improvements for enhanced performance, efficiency, and best practices compliance.</value>
    public List<string> Recommendations { get; set; } = [];

    /// <summary>
    /// Gets or sets the validation results for all libraries within the lab.
    /// </summary>
    /// <value>A collection of library-specific validation summaries providing detailed component-level feedback.</value>
    public List<LibraryValidationSummary> LibraryValidations { get; set; } = [];

    /// <summary>
    /// Gets or sets the validation results for all workflows within the lab.
    /// </summary>
    /// <value>A collection of workflow-specific validation summaries providing detailed component-level feedback.</value>
    public List<WorkflowValidationSummary> WorkflowValidations { get; set; } = [];
}
