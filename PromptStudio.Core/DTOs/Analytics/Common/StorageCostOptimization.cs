namespace PromptStudio.Core.DTOs.Analytics;

public class StorageCostOptimization
{
    public string StorageType { get; set; } = string.Empty;
    public long CurrentSize { get; set; }
    public long OptimizedSize { get; set; }
    public decimal CurrentCost { get; set; }
    public decimal OptimizedCost { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}
