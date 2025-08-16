namespace PromptStudio.Core.DTOs.Analytics;

public class JourneyTouchpoint
{
    public string TouchpointName { get; set; } = string.Empty;
    public long Interactions { get; set; }
    public double SatisfactionScore { get; set; }
    public TimeSpan AverageTimeSpent { get; set; }
}
