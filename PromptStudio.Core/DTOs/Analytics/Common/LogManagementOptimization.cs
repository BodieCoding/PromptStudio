namespace PromptStudio.Core.DTOs.Analytics;

public class LogManagementOptimization
{
    public string LoggingSystem { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal StorageSavings { get; set; }
}
