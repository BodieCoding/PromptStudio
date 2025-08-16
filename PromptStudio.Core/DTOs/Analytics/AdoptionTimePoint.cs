namespace PromptStudio.Core.DTOs.Analytics;

public class AdoptionTimePoint
{
    public DateTime Date { get; set; }
    public long AdoptionCount { get; set; }
    public double CumulativeAdoptionRate { get; set; }
}
