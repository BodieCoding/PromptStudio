using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive performance analytics result with system metrics and optimization insights
/// </summary>
public class PerformanceAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Overall performance summary
    /// </summary>
    public PerformanceAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// System response time analytics
    /// </summary>
    public ResponseTimeAnalytics ResponseTime { get; set; } = new();

    /// <summary>
    /// System throughput analytics
    /// </summary>
    public ThroughputAnalytics Throughput { get; set; } = new();

    /// <summary>
    /// Error rate analytics
    /// </summary>
    public ErrorRateAnalytics ErrorRate { get; set; } = new();

    /// <summary>
    /// Resource utilization analytics
    /// </summary>
    public ResourceUtilizationMetrics ResourceUtilization { get; set; } = new();

    /// <summary>
    /// System availability analytics
    /// </summary>
    public AvailabilityAnalytics Availability { get; set; } = new();

    /// <summary>
    /// Performance bottleneck analysis
    /// </summary>
    public List<PerformanceBottleneck> Bottlenecks { get; set; } = [];

    /// <summary>
    /// Performance trends over time
    /// </summary>
    public List<PerformanceTrendPoint>? TimeSeries { get; set; }

    /// <summary>
    /// Performance benchmarks and comparisons
    /// </summary>
    public PerformanceBenchmarks? Benchmarks { get; set; }

    /// <summary>
    /// Performance optimization recommendations
    /// </summary>
    public List<PerformanceRecommendation> Recommendations { get; set; } = [];

    /// <summary>
    /// Performance alerts and anomalies
    /// </summary>
    public List<PerformanceAlert> Alerts { get; set; } = [];
}

/// <summary>
/// Performance analytics summary
/// </summary>
public class PerformanceAnalyticsSummary
{
    /// <summary>
    /// Overall performance score (0-100)
    /// </summary>
    public double PerformanceScore { get; set; }

    /// <summary>
    /// Average response time across all operations
    /// </summary>
    public double AverageResponseTimeMs { get; set; }

    /// <summary>
    /// 95th percentile response time
    /// </summary>
    public double P95ResponseTimeMs { get; set; }

    /// <summary>
    /// Total throughput (requests per second)
    /// </summary>
    public double TotalThroughputRps { get; set; }

    /// <summary>
    /// Overall error rate percentage
    /// </summary>
    public double ErrorRatePercentage { get; set; }

    /// <summary>
    /// System uptime percentage
    /// </summary>
    public double UptimePercentage { get; set; }

    /// <summary>
    /// Key performance indicators
    /// </summary>
    public List<string> KeyFindings { get; set; } = [];

    /// <summary>
    /// Performance improvement opportunities
    /// </summary>
    public List<string> ImprovementOpportunities { get; set; } = [];
}

/// <summary>
/// Response time analytics
/// </summary>
public class ResponseTimeAnalytics
{
    /// <summary>
    /// Average response time in milliseconds
    /// </summary>
    public double Average { get; set; }

    /// <summary>
    /// Median response time in milliseconds
    /// </summary>
    public double Median { get; set; }

    /// <summary>
    /// 95th percentile response time
    /// </summary>
    public double P95 { get; set; }

    /// <summary>
    /// 99th percentile response time
    /// </summary>
    public double P99 { get; set; }

    /// <summary>
    /// Minimum response time
    /// </summary>
    public double Min { get; set; }

    /// <summary>
    /// Maximum response time
    /// </summary>
    public double Max { get; set; }

    /// <summary>
    /// Response time distribution by ranges
    /// </summary>
    public Dictionary<string, long> Distribution { get; set; } = [];

    /// <summary>
    /// Response time by endpoint or operation
    /// </summary>
    public Dictionary<string, double> ByEndpoint { get; set; } = [];
}

/// <summary>
/// Throughput analytics
/// </summary>
public class ThroughputAnalytics
{
    /// <summary>
    /// Average requests per second
    /// </summary>
    public double AverageRps { get; set; }

    /// <summary>
    /// Peak requests per second
    /// </summary>
    public double PeakRps { get; set; }

    /// <summary>
    /// Peak timestamp
    /// </summary>
    public DateTime? PeakTimestamp { get; set; }

    /// <summary>
    /// Throughput by time period
    /// </summary>
    public Dictionary<string, double> ByTimePeriod { get; set; } = [];

    /// <summary>
    /// Throughput by endpoint or operation
    /// </summary>
    public Dictionary<string, double> ByEndpoint { get; set; } = [];

    /// <summary>
    /// Throughput growth rate
    /// </summary>
    public double? GrowthRate { get; set; }
}

/// <summary>
/// Error rate analytics
/// </summary>
public class ErrorRateAnalytics
{
    /// <summary>
    /// Overall error rate percentage
    /// </summary>
    public double OverallErrorRate { get; set; }

    /// <summary>
    /// Error rate by error type
    /// </summary>
    public Dictionary<string, double> ByErrorType { get; set; } = [];

    /// <summary>
    /// Error rate by endpoint or operation
    /// </summary>
    public Dictionary<string, double> ByEndpoint { get; set; } = [];

    /// <summary>
    /// Most common errors
    /// </summary>
    public List<ErrorFrequency> MostCommonErrors { get; set; } = [];

    /// <summary>
    /// Error rate trends
    /// </summary>
    public List<ErrorRateTrendPoint>? Trends { get; set; }
}

/// <summary>
/// Resource utilization metrics
/// </summary>
public class ResourceUtilizationMetrics
{
    /// <summary>
    /// CPU utilization percentage
    /// </summary>
    public double CpuUtilization { get; set; }

    /// <summary>
    /// Memory utilization percentage
    /// </summary>
    public double MemoryUtilization { get; set; }

    /// <summary>
    /// Disk utilization percentage
    /// </summary>
    public double DiskUtilization { get; set; }

    /// <summary>
    /// Network utilization percentage
    /// </summary>
    public double NetworkUtilization { get; set; }

    /// <summary>
    /// Resource utilization by component
    /// </summary>
    public Dictionary<string, ResourceComponentMetrics> ByComponent { get; set; } = [];

    /// <summary>
    /// Resource efficiency score (0-100)
    /// </summary>
    public double EfficiencyScore { get; set; }
}

/// <summary>
/// Availability analytics
/// </summary>
public class AvailabilityAnalytics
{
    /// <summary>
    /// Overall uptime percentage
    /// </summary>
    public double UptimePercentage { get; set; }

    /// <summary>
    /// Total downtime in minutes
    /// </summary>
    public double TotalDowntimeMinutes { get; set; }

    /// <summary>
    /// Number of outages
    /// </summary>
    public int OutageCount { get; set; }

    /// <summary>
    /// Average outage duration in minutes
    /// </summary>
    public double AverageOutageDurationMinutes { get; set; }

    /// <summary>
    /// Availability by service or component
    /// </summary>
    public Dictionary<string, double> ByComponent { get; set; } = [];

    /// <summary>
    /// Planned vs unplanned downtime
    /// </summary>
    public DowntimeBreakdown DowntimeBreakdown { get; set; } = new();
}

/// <summary>
/// Performance bottleneck information
/// </summary>
public class PerformanceBottleneck
{
    /// <summary>
    /// Bottleneck component or service
    /// </summary>
    public string Component { get; set; } = string.Empty;

    /// <summary>
    /// Bottleneck type (CPU, Memory, Network, etc.)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Severity level (low, medium, high, critical)
    /// </summary>
    public string Severity { get; set; } = "medium";

    /// <summary>
    /// Impact description
    /// </summary>
    public string Impact { get; set; } = string.Empty;

    /// <summary>
    /// Recommended actions
    /// </summary>
    public List<string> RecommendedActions { get; set; } = [];

    /// <summary>
    /// Performance impact score (0-100)
    /// </summary>
    public double ImpactScore { get; set; }
}

/// <summary>
/// Performance trend point
/// </summary>
public class PerformanceTrendPoint
{
    public DateTime Timestamp { get; set; }
    public double AverageResponseTimeMs { get; set; }
    public double ThroughputRps { get; set; }
    public double ErrorRate { get; set; }
    public double CpuUtilization { get; set; }
    public double MemoryUtilization { get; set; }
}

/// <summary>
/// Performance benchmarks
/// </summary>
public class PerformanceBenchmarks
{
    /// <summary>
    /// Industry benchmark comparison
    /// </summary>
    public Dictionary<string, BenchmarkComparison> IndustryBenchmarks { get; set; } = [];

    /// <summary>
    /// Historical performance comparison
    /// </summary>
    public Dictionary<string, BenchmarkComparison> HistoricalComparison { get; set; } = [];

    /// <summary>
    /// SLA compliance metrics
    /// </summary>
    public Dictionary<string, SlaComplianceMetrics> SlaCompliance { get; set; } = [];
}

/// <summary>
/// Performance recommendation
/// </summary>
public class PerformanceRecommendation
{
    /// <summary>
    /// Recommendation type
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Priority level (low, medium, high, critical)
    /// </summary>
    public string Priority { get; set; } = "medium";

    /// <summary>
    /// Recommendation title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Expected performance improvement
    /// </summary>
    public string? ExpectedImprovement { get; set; }

    /// <summary>
    /// Implementation effort estimate
    /// </summary>
    public string? ImplementationEffort { get; set; }

    /// <summary>
    /// Implementation steps
    /// </summary>
    public List<string>? ImplementationSteps { get; set; }
}

/// <summary>
/// Performance alert
/// </summary>
public class PerformanceAlert
{
    /// <summary>
    /// Alert severity (info, warning, critical)
    /// </summary>
    public string Severity { get; set; } = "info";

    /// <summary>
    /// Alert title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Alert message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Alert timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Affected component
    /// </summary>
    public string Component { get; set; } = string.Empty;

    /// <summary>
    /// Alert category
    /// </summary>
    public string Category { get; set; } = string.Empty;
}

// Supporting classes

public class ErrorFrequency
{
    public string ErrorType { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public long Count { get; set; }
    public double Percentage { get; set; }
}

public class ErrorRateTrendPoint
{
    public DateTime Timestamp { get; set; }
    public double ErrorRate { get; set; }
    public long ErrorCount { get; set; }
    public long TotalRequests { get; set; }
}

public class ResourceComponentMetrics
{
    public double CpuUtilization { get; set; }
    public double MemoryUtilization { get; set; }
    public double DiskUtilization { get; set; }
    public double NetworkUtilization { get; set; }
}

public class DowntimeBreakdown
{
    public double PlannedDowntimeMinutes { get; set; }
    public double UnplannedDowntimeMinutes { get; set; }
    public double PlannedDowntimePercentage { get; set; }
    public double UnplannedDowntimePercentage { get; set; }
}

public class BenchmarkComparison
{
    public double CurrentValue { get; set; }
    public double BenchmarkValue { get; set; }
    public double Variance { get; set; }
    public string PerformanceRating { get; set; } = string.Empty;
}

public class SlaComplianceMetrics
{
    public double SlaTarget { get; set; }
    public double ActualValue { get; set; }
    public double CompliancePercentage { get; set; }
    public bool IsCompliant { get; set; }
}
