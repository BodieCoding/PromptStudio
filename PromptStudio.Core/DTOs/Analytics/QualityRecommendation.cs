namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Quality recommendation
/// </summary>
public class QualityRecommendation
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public double ExpectedImpact { get; set; }
    public List<string> ActionItems { get; set; } = new();
}
