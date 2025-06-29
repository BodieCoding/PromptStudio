namespace PromptStudio.Core.Domain;

/// <summary>
/// Workflow status for approval workflows
/// </summary>
public enum WorkflowStatus
{
    Draft = 0,
    UnderReview = 1,
    ChangesRequested = 2,
    Approved = 3,
    Published = 4,
    Deprecated = 5,
    Archived = 6,
    Suspended = 7,
    Rejected = 8,
    Error = 9
}
