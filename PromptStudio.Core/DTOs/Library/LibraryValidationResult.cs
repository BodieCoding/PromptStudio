namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Library validation result
/// </summary>
public class LibraryValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public double QualityScore { get; set; }
    public Dictionary<string, object> ValidationDetails { get; set; } = new();
}
