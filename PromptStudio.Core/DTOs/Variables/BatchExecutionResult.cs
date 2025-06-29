namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of batch execution
/// </summary>
public class BatchExecutionResult
{
    public Guid ExecutionId { get; set; }
    public int TotalVariableSets { get; set; }
    public int SuccessfulExecutions { get; set; }
    public int FailedExecutions { get; set; }
    public TimeSpan TotalExecutionTime { get; set; }
    public List<VariableSetExecutionResult> Results { get; set; } = new();
    public Dictionary<string, object> ExecutionMetrics { get; set; } = new();
}
