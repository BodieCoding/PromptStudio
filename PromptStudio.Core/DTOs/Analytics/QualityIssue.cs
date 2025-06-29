namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Quality issue
/// </summary>
public class QualityIssue
{
    public string Type { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public Dictionary<string, object> Details { get; set; } = new();
}
