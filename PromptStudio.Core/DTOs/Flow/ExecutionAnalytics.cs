namespace PromptStudio.Core.DTOs.Flow;


/// <summary>
/// Execution analytics data
/// </summary>
public class ExecutionAnalytics
{
    public Guid FlowId { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    
    public long TotalExecutions { get; set; }
    public long SuccessfulExecutions { get; set; }
    public long FailedExecutions { get; set; }
    
    public decimal TotalCost { get; set; }
    public decimal AverageCost { get; set; }
    public long TotalTokens { get; set; }
    
    public double AverageExecutionTime { get; set; }
    public double MedianExecutionTime { get; set; }
    public double P95ExecutionTime { get; set; }
    
    public Dictionary<string, long> ErrorTypes { get; set; } = [];
    public Dictionary<string, long> NodeExecutionCounts { get; set; } = [];
    
    public decimal? QualityScore { get; set; }
    public int? UserRatingAverage { get; set; }
    
    public List<DailyMetrics> DailyBreakdown { get; set; } = [];
}
