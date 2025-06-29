namespace PromptStudio.Core.Domain;

/// <summary>
/// Result of importing libraries into the system
/// This class encapsulates the outcome of an import operation, including success status,
/// </summary>
public class LibraryImportResult
{
    /// <summary>
    /// Whether the import was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// List of imported libraries
    /// </summary>
    public List<PromptLibrary> ImportedLibraries { get; set; } = new();

    /// <summary>
    /// List of errors encountered during import
    /// </summary>
    public List<string> Errors { get; set; } = new();
}
