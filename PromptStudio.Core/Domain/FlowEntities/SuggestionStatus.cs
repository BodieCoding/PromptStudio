namespace PromptStudio.Core.Domain;

/// <summary>
/// Status of AI suggestions
/// </summary>
public enum SuggestionStatus
{
    Pending = 0,
    Accepted = 1,
    Rejected = 2,
    Implemented = 3,
    Deferred = 4,
    UnderReview = 5
}
