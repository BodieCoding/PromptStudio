namespace PromptStudio.Core.Domain;

/// <summary>
/// Library lifecycle status
/// </summary>
public enum LibraryStatus
{
    Active = 0,
    ReadOnly = 1,
    Archived = 2,
    UnderReview = 3,
    Deprecated = 4
}

