namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of CSV parsing operation
/// </summary>
public class CsvParsingResult
{
    public List<Dictionary<string, string>> VariableSets { get; set; } = new();
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public Dictionary<string, Type> InferredTypes { get; set; } = new();
    public int TotalRows { get; set; }
    public int ValidRows { get; set; }
    public Dictionary<string, object> Statistics { get; set; } = new();
}
