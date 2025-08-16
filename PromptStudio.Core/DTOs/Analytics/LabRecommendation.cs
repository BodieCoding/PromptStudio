namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Lab recommendation
/// </summary>
public class LabRecommendation
{
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ExpectedBenefit { get; set; }
}
