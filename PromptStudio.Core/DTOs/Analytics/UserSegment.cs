namespace PromptStudio.Core.DTOs.Analytics;

public class UserSegment
{
    public string SegmentName { get; set; } = string.Empty;
    public long UserCount { get; set; }
    public double PercentageOfTotal { get; set; }
    public Dictionary<string, object> Characteristics { get; set; } = [];
}
