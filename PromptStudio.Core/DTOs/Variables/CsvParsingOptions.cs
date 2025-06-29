namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// CSV parsing options
/// </summary>
public class CsvParsingOptions
{
    public char Delimiter { get; set; } = ',';
    public bool HasHeaders { get; set; } = true;
    public bool TrimWhitespace { get; set; } = true;
    public bool SkipEmptyRows { get; set; } = true;
    public int MaxRows { get; set; } = 10000;
    public Dictionary<string, Type> ColumnTypes { get; set; } = new();
}
