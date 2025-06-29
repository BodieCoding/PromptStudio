namespace PromptStudio.Core.Domain;

/// <summary>
/// Options for cloning a prompt library
/// This class allows customization of the cloning process, such as whether to clone permissions and templates,
/// </summary>
public class LibraryCloneOptions
{
    /// <summary>
    /// Whether to clone permissions from the source library
    /// </summary>
    public bool ClonePermissions { get; set; } = true;

    /// <summary>
    /// Whether to clone templates from the source library
    /// </summary>
    public bool CloneTemplates { get; set; } = true;

    /// <summary>
    /// Optional custom metadata to include in the cloned library
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }
}
