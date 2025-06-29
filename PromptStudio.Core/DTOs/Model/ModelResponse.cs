namespace PromptStudio.Core.DTOs.Model;

/// <summary>
/// Response from model execution
/// </summary>
public class ModelResponse
{
    public bool Success { get; set; }
    public string? Content { get; set; }
    public string? ErrorMessage { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();
    public long ExecutionTimeMs { get; set; }
    public int TokensUsed { get; set; }
}
