namespace PromptStudio.Core.DTOs.Analytics;

public class ApiOptimization
{
    public string ApiEndpoint { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public double CurrentResponseTime { get; set; }
    public double ProjectedResponseTime { get; set; }
    public string RecommendedChanges { get; set; } = string.Empty;
}
