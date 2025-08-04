namespace PromptStudio.Core.DTOs.Statistics;

/// <summary>
/// Placeholder DTO for model performance comparison
/// </summary>
public class ModelPerformance
{
    public string? ModelProvider { get; set; }
    public string? ModelName { get; set; }
    public int TotalExecutions { get; set; }
    public int SuccessfulExecutions { get; set; }
    public int FailedExecutions { get; set; }
    public double AverageExecutionTime { get; set; }
    public int TotalTokensUsed { get; set; }
    public double AverageTokensPerExecution { get; set; }
}
