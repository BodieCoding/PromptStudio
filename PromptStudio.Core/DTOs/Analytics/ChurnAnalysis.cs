namespace PromptStudio.Core.DTOs.Analytics;

public class ChurnAnalysis
{
    public double ChurnRate { get; set; }
    public List<string> ChurnReasons { get; set; } = [];
    public Dictionary<string, double> ChurnBySegment { get; set; } = [];
}
