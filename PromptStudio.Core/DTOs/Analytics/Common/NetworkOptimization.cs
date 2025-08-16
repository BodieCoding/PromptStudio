namespace PromptStudio.Core.DTOs.Analytics;

public class NetworkOptimization
{
    public string NetworkComponent { get; set; } = string.Empty;
    public double CurrentBandwidth { get; set; }
    public double OptimalBandwidth { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}
