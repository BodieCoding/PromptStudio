namespace PromptStudio.Core.DTOs.Library;

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
    /// Whether to import execution history
    /// </summary>
    public bool ImportExecutionHistory { get; set; } = false;

    /// <summary>
    /// Whether to validate before import
    /// </summary>
    public bool ValidateBeforeImport { get; set; } = true;

    /// <summary>
    /// Whether to create backup
    /// </summary>
    public bool CreateBackup { get; set; } = true;

    /// <summary>
    /// Optional custom metadata to include in the import
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }

    /// <summary>
    /// Custom options for specific import scenarios
    /// </summary>
    public Dictionary<string, object> CustomOptions { get; set; } = new();
}
