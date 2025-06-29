namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of executing a single variable set
/// </summary>
public class VariableSetExecutionResult
{
    public int Index { get; set; }
    public Dictionary<string, string> Variables { get; set; } = new();
    public string? ResolvedPrompt { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();
}
