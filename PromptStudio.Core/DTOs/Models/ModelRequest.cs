namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Request to execute a prompt
/// </summary>
public class ModelRequest
{
    public string ModelId { get; set; } = string.Empty;
    public string Prompt { get; set; } = string.Empty;
    public string? SystemMessage { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
    public Dictionary<string, object> Variables { get; set; } = new();
}
