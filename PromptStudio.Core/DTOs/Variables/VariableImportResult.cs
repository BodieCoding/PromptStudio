using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Variable import result
/// </summary>
public class VariableImportResult
{
    public VariableCollection? Collection { get; set; }
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public int ImportedRows { get; set; }
    public int SkippedRows { get; set; }
    public Dictionary<string, object> ImportStatistics { get; set; } = new();
}
