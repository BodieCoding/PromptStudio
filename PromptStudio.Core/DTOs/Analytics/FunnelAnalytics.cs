namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Funnel analytics
/// </summary>
public class FunnelAnalytics
{
    /// <summary>
    /// Funnel steps
    /// </summary>
    public List<FunnelStep> Steps { get; set; } = [];

    /// <summary>
    /// Overall conversion rate
    /// </summary>
    public double OverallConversionRate { get; set; }

    /// <summary>
    /// Drop-off points
    /// </summary>
    public List<FunnelDropOff> DropOffs { get; set; } = [];
}
