namespace PromptStudio.Core.DTOs.Analytics;

public class BackupOptimization
{
    public string BackupType { get; set; } = string.Empty;
    public string CurrentStrategy { get; set; } = string.Empty;
    public string RecommendedStrategy { get; set; } = string.Empty;
    public decimal CostSavings { get; set; }
}
