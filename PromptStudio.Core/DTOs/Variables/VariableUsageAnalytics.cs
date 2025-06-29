namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Variable usage analytics
/// </summary>
public class VariableUsageAnalytics
{
    public Guid TemplateId { get; set; }
    public Dictionary<string, long> VariableUsageCount { get; set; } = new();
    public Dictionary<string, List<string>> MostCommonValues { get; set; } = new();
    public Dictionary<string, double> VariableSuccessRates { get; set; } = new();
    public Dictionary<string, TimeSpan> VariablePerformance { get; set; } = new();
    public List<string> UnusedVariables { get; set; } = new();
    public List<string> HighPerformingVariables { get; set; } = new();
}
