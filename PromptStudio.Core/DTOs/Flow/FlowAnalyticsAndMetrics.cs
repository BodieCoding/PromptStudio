using System.ComponentModel.DataAnnotations;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Aggregated metrics for flows
/// </summary>
public class FlowMetrics
{
    public Guid FlowId { get; set; }
    public long TotalExecutions { get; set; }
    public decimal AverageCost { get; set; }
    public double AverageExecutionTime { get; set; }
    public decimal? SuccessRate { get; set; }
    public decimal? QualityScore { get; set; }
    public DateTime LastExecutedAt { get; set; }
    public DateTime CalculatedAt { get; set; }
}

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
    
    public Dictionary<string, long> ErrorTypes { get; set; } = new();
    public Dictionary<string, long> NodeExecutionCounts { get; set; } = new();
    
    public decimal? QualityScore { get; set; }
    public int? UserRatingAverage { get; set; }
    
    public List<DailyMetrics> DailyBreakdown { get; set; } = new();
}

/// <summary>
/// Daily execution metrics
/// </summary>
public class DailyMetrics
{
    public DateTime Date { get; set; }
    public long Executions { get; set; }
    public decimal Cost { get; set; }
    public double AverageTime { get; set; }
    public decimal SuccessRate { get; set; }
}

/// <summary>
/// Flow validation session for caching validation results
/// </summary>
public class FlowValidationSession : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    [Required]
    [StringLength(20)]
    public string FlowVersion { get; set; } = string.Empty;
    
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Full validation result as JSON
    /// </summary>
    public string ValidationData { get; set; } = "{}";
    
    public DateTime ValidatedAt { get; set; }
    
    [StringLength(100)]
    public string? ValidatedBy { get; set; }
    
    public TimeSpan ValidationDuration { get; set; }
    
    /// <summary>
    /// Hash of flow structure for cache invalidation
    /// </summary>
    [StringLength(64)]
    public string FlowHash { get; set; } = string.Empty;
}
