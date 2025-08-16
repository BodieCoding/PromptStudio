namespace PromptStudio.Core.DTOs.Analytics;

public class FunnelDropOff
{
    public string FromStep { get; set; } = string.Empty;
    public string ToStep { get; set; } = string.Empty;
    public double DropOffRate { get; set; }
    public List<string> PossibleReasons { get; set; } = [];
}
