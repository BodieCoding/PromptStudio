namespace PromptStudio.Core.Domain;

/// <summary>
/// Options for importing libraries into the prompt studio
/// This class allows customization of the import process, such as whether to overwrite existing templates,
/// </summary>
public class LibraryImportOptions
{
    /// <summary>
    /// Whether to overwrite existing templates with the same name
    /// </summary>
    public bool OverwriteExisting { get; set; } = false;

    /// <summary>
    /// Whether to import permissions from the source library
    /// </summary>
    public bool ImportPermissions { get; set; } = true;

    /// <summary>
    /// Optional custom metadata to include in the import
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }
}
