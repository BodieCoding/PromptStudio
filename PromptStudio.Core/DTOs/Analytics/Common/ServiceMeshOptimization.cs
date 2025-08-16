namespace PromptStudio.Core.DTOs.Analytics;

public class ServiceMeshOptimization
{
    public string MeshComponent { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public double PerformanceGain { get; set; }
}
