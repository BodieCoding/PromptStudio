namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Import options
/// </summary>
public class ImportOptions
{
    public string CollectionName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public CsvParsingOptions? CsvOptions { get; set; }
    public bool ValidateData { get; set; } = true;
    public bool CreateBackup { get; set; } = true;
}
