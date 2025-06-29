namespace PromptStudio.Core.Domain;

/// <summary>
/// Template approval workflow status
/// </summary>
public enum TemplateStatus
{
    Draft = 0,
    UnderReview = 1,
    ChangesRequested = 2,
    Approved = 3,
    Published = 4,
    Deprecated = 5,
    Archived = 6,
    Rejected = 7
}
