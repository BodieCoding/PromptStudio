namespace PromptStudio.Core.DTOs.Templates;

/// <summary>
/// Result of template analysis with complexity metrics and optimization suggestions
/// </summary>
public class TemplateAnalysisResult
{
    public double ComplexityScore { get; set; }
    public int VariableCount { get; set; }
    public int TokenCount { get; set; }
    public List<string> OptimizationSuggestions { get; set; } = new();
    public List<string> QualityIssues { get; set; } = new();
    public Dictionary<string, object> Metrics { get; set; } = new();
}
