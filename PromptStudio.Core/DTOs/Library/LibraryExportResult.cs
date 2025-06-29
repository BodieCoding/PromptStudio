namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Result of exporting a prompt library to JSON
/// This class encapsulates the outcome of an export operation, including success status,
/// JSON content of the exported library, and an optional message.
/// it is used to provide feedback on the export process, indicating whether the export was successful
/// </summary>
public class LibraryExportResult
{
    /// <summary>
    /// Indicates if the export was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// JSON content of the exported library
    /// </summary>
    public string? JsonContent { get; set; }

    /// <summary>
    /// Optional message with details about the export operation
    /// </summary>
    public string? Message { get; set; }
}
