using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Trend analysis data for execution performance over time
/// </summary>
public class ExecutionTrendAnalysis
{
    /// <summary>
    /// Time range for the analysis
    /// </summary>
    public DateTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Granularity of the trend data (hour, day, week, month)
    /// </summary>
    public string Granularity { get; set; } = "day";

    /// <summary>
    /// Data points over time
    /// </summary>
    public List<ExecutionTrendPoint> TrendPoints { get; set; } = new();

    /// <summary>
    /// Overall trend direction (increasing, decreasing, stable)
    /// </summary>
    public string TrendDirection { get; set; } = "stable";

    /// <summary>
    /// Percentage change from start to end of period
    /// </summary>
    public double? PercentageChange { get; set; }

    /// <summary>
    /// Average executions per time period
    /// </summary>
    public double AverageExecutionsPerPeriod { get; set; }

    /// <summary>
    /// Peak executions in a single time period
    /// </summary>
    public long PeakExecutions { get; set; }

    /// <summary>
    /// Date/time of peak executions
    /// </summary>
    public DateTime? PeakDateTime { get; set; }

    /// <summary>
    /// Total executions in the analysis period
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Average response time trend
    /// </summary>
    public List<ResponseTimeTrendPoint>? ResponseTimeTrend { get; set; }

    /// <summary>
    /// Quality score trend
    /// </summary>
    public List<QualityTrendPoint>? QualityTrend { get; set; }

    /// <summary>
    /// Cost trend analysis
    /// </summary>
    public List<CostTrendPoint>? CostTrend { get; set; }

    /// <summary>
    /// Token usage trend
    /// </summary>
    public List<TokenUsageTrendPoint>? TokenUsageTrend { get; set; }
}

/// <summary>
/// A single point in the execution trend
/// </summary>
public class ExecutionTrendPoint
{
    /// <summary>
    /// Timestamp for this data point
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Number of executions at this point
    /// </summary>
    public long ExecutionCount { get; set; }

    /// <summary>
    /// Success rate (0-1)
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Average response time in milliseconds
    /// </summary>
    public double AverageResponseTimeMs { get; set; }
}

/// <summary>
/// Response time trend point
/// </summary>
public class ResponseTimeTrendPoint
{
    public DateTime Timestamp { get; set; }
    public double AverageResponseTimeMs { get; set; }
    public double MedianResponseTimeMs { get; set; }
    public double P95ResponseTimeMs { get; set; }
}



/// <summary>
/// Token usage trend point
/// </summary>
public class TokenUsageTrendPoint
{
    public DateTime Timestamp { get; set; }
    public long TotalTokens { get; set; }
    public long AverageTokensPerExecution { get; set; }
    public long InputTokens { get; set; }
    public long OutputTokens { get; set; }
}
