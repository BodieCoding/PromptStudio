namespace PromptStudio.Core.DTOs.Lab;

/// <summary>
/// Represents a workflow validation summary within a lab environment, providing focused validation results and quality metrics.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Workflow-focused validation summary DTO used by lab validation services and workflow management systems.
/// Provides targeted workflow validation feedback within broader lab validation workflows and quality assurance processes.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Workflow-specific validation data with issue categorization and quality assessment.
/// Designed for integration with workflow management systems and lab-wide validation reporting.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Workflow-specific validation feedback within lab contexts</item>
/// <item>Flow configuration assessment and error localization</item>
/// <item>Workflow compliance checking and governance reporting</item>
/// <item>Quality metrics aggregation for workflow management</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight workflow summary optimized for inclusion in lab validation collections.
/// Error and warning lists should be bounded for effective presentation and user comprehension.
/// Validation results should be cached to avoid repeated expensive workflow analysis.</para>
/// </remarks>
public class WorkflowValidationSummary
{
    /// <summary>
    /// Gets or sets the unique identifier of the validated workflow.
    /// </summary>
    /// <value>A GUID that uniquely identifies the workflow within the lab environment.</value>
    public Guid WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the validated workflow.
    /// </summary>
    /// <value>A string providing a descriptive name for the workflow for display and identification purposes.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the workflow passes all validation checks.
    /// </summary>
    /// <value>True if the workflow configuration and all its components are valid; otherwise, false.</value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the collection of workflow-specific validation errors.
    /// </summary>
    /// <value>A list of critical errors specific to this workflow that must be resolved.</value>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of workflow-specific validation warnings.
    /// </summary>
    /// <value>A list of non-critical issues specific to this workflow suggesting improvements.</value>
    public List<string> Warnings { get; set; } = [];
}
