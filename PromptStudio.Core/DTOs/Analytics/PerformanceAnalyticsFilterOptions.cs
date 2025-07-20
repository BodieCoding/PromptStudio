using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Execution;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Filter options for performance analytics queries
/// </summary>
public class PerformanceAnalyticsFilterOptions
{
    /// <summary>
    /// Filter by specific services or components
    /// </summary>
    public List<string>? Services { get; set; }

    /// <summary>
    /// Filter by specific endpoints or operations
    /// </summary>
    public List<string>? Endpoints { get; set; }

    /// <summary>
    /// Filter by response time range (milliseconds)
    /// </summary>
    public IntRange? ResponseTimeRange { get; set; }

    /// <summary>
    /// Filter by throughput range (requests per second)
    /// </summary>
    public DoubleRange? ThroughputRange { get; set; }

    /// <summary>
    /// Filter by error rate range (percentage)
    /// </summary>
    public DoubleRange? ErrorRateRange { get; set; }

    /// <summary>
    /// Filter by resource utilization range (percentage)
    /// </summary>
    public DoubleRange? ResourceUtilizationRange { get; set; }

    /// <summary>
    /// Include specific performance metrics
    /// </summary>
    public List<string>? IncludeMetrics { get; set; }

    /// <summary>
    /// Exclude specific performance metrics
    /// </summary>
    public List<string>? ExcludeMetrics { get; set; }

    /// <summary>
    /// Group results by specific dimensions
    /// </summary>
    public List<string>? GroupBy { get; set; }

    /// <summary>
    /// Include performance anomalies
    /// </summary>
    public bool IncludeAnomalies { get; set; } = false;

    /// <summary>
    /// Include benchmark comparisons
    /// </summary>
    public bool IncludeBenchmarks { get; set; } = false;

    /// <summary>
    /// Custom filters as key-value pairs
    /// </summary>
    public Dictionary<string, object>? CustomFilters { get; set; }
}


