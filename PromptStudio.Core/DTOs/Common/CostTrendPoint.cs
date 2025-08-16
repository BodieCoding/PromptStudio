namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Cost trend data point for time-series analysis
/// </summary>
public class CostTrendPoint
{
    /// <summary>
    /// Timestamp for this cost data point
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Total cost for this time period
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Average cost per execution for this time period
    /// </summary>
    public decimal AverageCostPerExecution { get; set; }

    /// <summary>
    /// Number of executions in this time period
    /// </summary>
    public long ExecutionCount { get; set; }

    /// <summary>
    /// Cost variance from previous period
    /// </summary>
    public decimal? CostVariance { get; set; }

    /// <summary>
    /// Percentage change from previous period
    /// </summary>
    public double? PercentageChange { get; set; }
}
