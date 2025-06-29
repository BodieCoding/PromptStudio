/// <summary>
/// Performance metrics for a template
/// </summary>
public class TemplatePerformanceMetrics
{
    public Guid TemplateId { get; set; }
    public double SuccessRate { get; set; }
    public double AverageLatency { get; set; }
    public double P95Latency { get; set; }
    public double P99Latency { get; set; }
    public long ErrorCount { get; set; }
    public Dictionary<string, long> ErrorsByType { get; set; } = new();
    public double QualityScore { get; set; }
    public Dictionary<string, double> PerformanceTrends { get; set; } = new();
}
