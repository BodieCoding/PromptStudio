namespace PromptStudio.Core.DTOs.Analytics;

// Architecture Optimization Classes
public class MicroservicesOptimization
{
    public string ServiceName { get; set; } = string.Empty;
    public string CurrentArchitecture { get; set; } = string.Empty;
    public string RecommendedArchitecture { get; set; } = string.Empty;
    public double ScalabilityImprovement { get; set; }
}
