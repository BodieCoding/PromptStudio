namespace PromptStudio.Core.DTOs.Analytics;

public class FunnelStep
{
    public int StepOrder { get; set; }
    public string StepName { get; set; } = string.Empty;
    public long Users { get; set; }
    public double ConversionRate { get; set; }
    public double DropOffRate { get; set; }
}
