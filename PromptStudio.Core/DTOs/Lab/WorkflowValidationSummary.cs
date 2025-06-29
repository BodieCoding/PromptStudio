namespace PromptStudio.Core.DTOs.Lab;

/// <summary>
/// Workflow validation summary for lab validation
/// </summary>
public class WorkflowValidationSummary
{
    /// <summary>
    /// Workflow ID
    /// </summary>
    public Guid WorkflowId { get; set; }

    /// <summary>
    /// Workflow name
    /// </summary>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Whether the workflow is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Workflow-specific errors
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Workflow-specific warnings
    /// </summary>
    public List<string> Warnings { get; set; } = new();
}
