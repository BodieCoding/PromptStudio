namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Lab resource utilization analytics
/// </summary>
public class LabResourceAnalytics
{
    /// <summary>
    /// Compute resource utilization percentage
    /// </summary>
    public double ComputeUtilization { get; set; }

    /// <summary>
    /// Storage utilization percentage
    /// </summary>
    public double StorageUtilization { get; set; }

    /// <summary>
    /// Cost breakdown by resource type
    /// </summary>
    public Dictionary<string, decimal> CostBreakdown { get; set; } = [];
}
