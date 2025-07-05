namespace PromptStudio.Core.Domain;

/// <summary>
/// Lab visibility and access control
/// Enhanced for enterprise scenarios
/// </summary>
public enum LabVisibility
{
    Private = 0,        // Only owner can access
    Internal = 1,       // Organization members can access
    Public = 2,         // Public read access
    TeamShared = 3      // Specific team access (future use)
}
