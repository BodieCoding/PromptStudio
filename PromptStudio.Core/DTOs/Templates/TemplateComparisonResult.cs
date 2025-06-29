namespace PromptStudio.Core.DTOs.Templates;

/// <summary>
/// Result of comparing two template versions
/// </summary>
public class TemplateComparisonResult
{
    public string Version1 { get; set; } = string.Empty;
    public string Version2 { get; set; } = string.Empty;
    public List<string> ContentDifferences { get; set; } = new();
    public List<string> VariableChanges { get; set; } = new();
    public Dictionary<string, object> MetricChanges { get; set; } = new();
}
