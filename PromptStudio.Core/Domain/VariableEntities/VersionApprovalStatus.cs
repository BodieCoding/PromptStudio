namespace PromptStudio.Core.Domain;

/// <summary>
/// Version approval workflow status
/// </summary>
public enum VersionApprovalStatus
{
    Draft = 0,
    SubmittedForReview = 1,
    UnderReview = 2,
    ChangesRequested = 3,
    Approved = 4,
    Rejected = 5,
    Deployed = 6,
    Deprecated = 7
}
