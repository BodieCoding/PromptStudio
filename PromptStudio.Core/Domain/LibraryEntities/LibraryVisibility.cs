namespace PromptStudio.Core.Domain;

/// <summary>
/// Library visibility levels for access control
/// </summary>
public enum LibraryVisibility
{
    /// <summary>
    /// Only owner and explicitly granted users can access
    /// </summary>
    Private = 0,
    
    /// <summary>
    /// All organization members can view
    /// </summary>
    Internal = 1,
    
    /// <summary>
    /// Organization members can view and contribute
    /// </summary>
    Collaborative = 2,
    
    /// <summary>
    /// Public read access (for open-source organizations)
    /// </summary>
    Public = 3
}