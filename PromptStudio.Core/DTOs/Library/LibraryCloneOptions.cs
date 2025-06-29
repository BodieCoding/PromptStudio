namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Options for cloning a prompt library
/// This class allows customization of the cloning process, such as whether to clone permissions and templates,
/// </summary>
public class LibraryCloneOptions
{
    /// <summary>
    /// Whether to clone permissions from the source library
    /// </summary>
    public bool ClonePermissions { get; set; } = false;

    /// <summary>
    /// Whether to clone templates from the source library
    /// </summary>
    public bool CloneTemplates { get; set; } = true;

    /// <summary>
    /// Whether to clone execution history
    /// </summary>
    public bool CloneExecutionHistory { get; set; } = false;

    /// <summary>
    /// Whether to clone analytics
    /// </summary>
    public bool CloneAnalytics { get; set; } = false;

    /// <summary>
    /// Whether to reset usage statistics
    /// </summary>
    public bool ResetUsageStatistics { get; set; } = true;

    /// <summary>
    /// New description for the cloned library
    /// </summary>
    public string? NewDescription { get; set; }

    /// <summary>
    /// Template name mappings
    /// </summary>
    public Dictionary<string, string>? TemplateNameMappings { get; set; }

    /// <summary>
    /// Optional custom metadata to include in the cloned library
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }

    /// <summary>
    /// Custom options for specific cloning scenarios
    /// </summary>
    public Dictionary<string, object> CustomOptions { get; set; } = new();
}
