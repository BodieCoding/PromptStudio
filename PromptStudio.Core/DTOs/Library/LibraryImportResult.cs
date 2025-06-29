using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Library;

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
    /// The imported library (if successful)
    /// </summary>
    public PromptLibrary? Library { get; set; }

    /// <summary>
    /// List of imported libraries
    /// </summary>
    public List<PromptLibrary> ImportedLibraries { get; set; } = new();

    /// <summary>
    /// List of errors encountered during import
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// List of warnings encountered during import
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Number of templates imported
    /// </summary>
    public int ImportedTemplates { get; set; }

    /// <summary>
    /// Number of templates skipped
    /// </summary>
    public int SkippedTemplates { get; set; }

    /// <summary>
    /// Import statistics
    /// </summary>
    public Dictionary<string, object> ImportStatistics { get; set; } = new();
}
