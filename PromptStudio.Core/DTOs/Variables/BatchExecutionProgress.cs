namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Progress information for batch execution
/// </summary>
public class BatchExecutionProgress
{
    public int TotalItems { get; set; }
    public int CompletedItems { get; set; }
    public int SuccessfulItems { get; set; }
    public int FailedItems { get; set; }
    public double PercentComplete => TotalItems > 0 ? (double)CompletedItems / TotalItems * 100 : 0;
    public TimeSpan ElapsedTime { get; set; }
    public TimeSpan? EstimatedTimeRemaining { get; set; }
    public string? CurrentItem { get; set; }
}
