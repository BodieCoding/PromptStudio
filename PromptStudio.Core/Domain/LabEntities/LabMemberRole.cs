namespace PromptStudio.Core.Domain;

/// <summary>
/// Lab member roles
/// </summary>
public enum LabMemberRole
{
    /// <summary>
    /// Can view labs, libraries, and templates
    /// </summary>
    Viewer = 0,

    /// <summary>
    /// Can execute templates and workflows
    /// </summary>
    Executor = 1,

    /// <summary>
    /// Can create and edit templates and libraries
    /// </summary>
    Contributor = 2,

    /// <summary>
    /// Can manage libraries, templates, and workflows
    /// </summary>
    Manager = 3,

    /// <summary>
    /// Can manage the lab and its members
    /// </summary>
    Admin = 4,

    /// <summary>
    /// Full ownership of the lab
    /// </summary>
    Owner = 5
}
