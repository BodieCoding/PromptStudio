namespace PromptStudio.Core.DTOs.Analytics;

public class StorageOptimization
{
    public string StorageType { get; set; } = string.Empty;
    public double CurrentUtilization { get; set; }
    public double OptimalUtilization { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}
