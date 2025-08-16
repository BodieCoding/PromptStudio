namespace PromptStudio.Core.DTOs.Analytics;

// Resource Rightsizing
public class ResourceRightsizingRecommendation
{
    public string ResourceId { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
    public string CurrentSize { get; set; } = string.Empty;
    public string RecommendedSize { get; set; } = string.Empty;
    public decimal CostImpact { get; set; }
    public string Justification { get; set; } = string.Empty;
}
