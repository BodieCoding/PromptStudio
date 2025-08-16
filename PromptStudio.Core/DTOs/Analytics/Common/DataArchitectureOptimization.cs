namespace PromptStudio.Core.DTOs.Analytics;

public class DataArchitectureOptimization
{
    public string DataComponent { get; set; } = string.Empty;
    public string CurrentArchitecture { get; set; } = string.Empty;
    public string RecommendedArchitecture { get; set; } = string.Empty;
    public double PerformanceGain { get; set; }
}
