namespace PromptStudio.Core.DTOs.Analytics;

public class ScalabilityArchitectureOptimization
{
    public string ArchitectureComponent { get; set; } = string.Empty;
    public string CurrentDesign { get; set; } = string.Empty;
    public string RecommendedDesign { get; set; } = string.Empty;
    public double ScalabilityFactor { get; set; }
}
