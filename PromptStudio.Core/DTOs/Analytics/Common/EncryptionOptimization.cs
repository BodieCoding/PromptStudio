namespace PromptStudio.Core.DTOs.Analytics;

public class EncryptionOptimization
{
    public string EncryptionType { get; set; } = string.Empty;
    public string CurrentImplementation { get; set; } = string.Empty;
    public string RecommendedImplementation { get; set; } = string.Empty;
    public double PerformanceImpact { get; set; }
}
