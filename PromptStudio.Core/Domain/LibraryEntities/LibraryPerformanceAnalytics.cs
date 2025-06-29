namespace PromptStudio.Core.Domain;


/// <summary>
/// Library performance analytics
/// </summary>
public class LibraryPerformanceAnalytics
{
    public Guid LibraryId { get; set; }
    public double OverallSuccessRate { get; set; }
    public TimeSpan AverageResponseTime { get; set; }
    public long TotalExecutions { get; set; }
    public Dictionary<string, double> TemplatePerformance { get; set; } = new();
    public Dictionary<string, ModelPerformanceMetrics> ModelPerformance { get; set; } = new();
    public List<PerformanceInsight> Insights { get; set; } = new();
    public Dictionary<string, object> CustomMetrics { get; set; } = new();
}
