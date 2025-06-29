namespace PromptStudio.Core.DTOs.Lab;

/// <summary>
/// Library validation summary for lab validation
/// </summary>
public class LibraryValidationSummary
{
    /// <summary>
    /// Library ID
    /// </summary>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Library name
    /// </summary>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Whether the library is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Library-specific errors
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Library-specific warnings
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Number of invalid templates in the library
    /// </summary>
    public int InvalidTemplateCount { get; set; }
}
