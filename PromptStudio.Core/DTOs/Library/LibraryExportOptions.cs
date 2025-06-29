namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Options for exporting a prompt library to JSON
/// This class allows customization of the export process, such as whether to include permissions and templates,
/// </summary>
public class LibraryExportOptions
{
    /// <summary>
    /// Whether to include permissions in the export
    /// </summary>
    public bool IncludePermissions { get; set; } = true;

    /// <summary>
    /// Whether to include templates in the export
    /// </summary>
    public bool IncludeTemplates { get; set; } = true;

    /// <summary>
    /// Optional custom metadata to include in the export
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }
}
