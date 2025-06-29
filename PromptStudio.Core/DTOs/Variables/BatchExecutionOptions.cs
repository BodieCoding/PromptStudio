namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Options for batch execution
/// </summary>
public class BatchExecutionOptions
{
    public int MaxConcurrency { get; set; } = 10;
    public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);
    public bool ContinueOnError { get; set; } = true;
    public bool SaveIntermediateResults { get; set; } = true;
    public Dictionary<string, object> ExecutionMetadata { get; set; } = new();
}
