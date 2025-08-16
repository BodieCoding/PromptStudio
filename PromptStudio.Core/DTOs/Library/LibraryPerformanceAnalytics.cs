using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Performance analytics for a prompt library
/// </summary>
public class LibraryPerformanceAnalytics
{
    /// <summary>
    /// Gets or sets the library ID
    /// </summary>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the analysis period
    /// </summary>
    public DateTimeRange AnalysisPeriod { get; set; } = new();

    /// <summary>
    /// Gets or sets the average response time in milliseconds
    /// </summary>
    public double AverageResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the median response time in milliseconds
    /// </summary>
    public double MedianResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the 95th percentile response time in milliseconds
    /// </summary>
    public double P95ResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the fastest response time in milliseconds
    /// </summary>
    public double FastestResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the slowest response time in milliseconds
    /// </summary>
    public double SlowestResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the throughput (executions per hour)
    /// </summary>
    public double Throughput { get; set; }

    /// <summary>
    /// Gets or sets the error rate (0.0 to 1.0)
    /// </summary>
    public double ErrorRate { get; set; }

    /// <summary>
    /// Gets or sets the availability percentage (0.0 to 100.0)
    /// </summary>
    public double Availability { get; set; }

    /// <summary>
    /// Gets or sets performance by template
    /// </summary>
    public Dictionary<Guid, TemplatePerformance> TemplatePerformance { get; set; } = [];

    /// <summary>
    /// Gets or sets performance trends over time
    /// </summary>
    public List<PerformanceDataPoint> PerformanceTrend { get; set; } = [];

    /// <summary>
    /// Gets or sets resource utilization metrics
    /// </summary>
    public ResourceUtilization ResourceUtilization { get; set; } = new();

    /// <summary>
    /// Gets or sets performance insights and recommendations
    /// </summary>
    public List<string> Insights { get; set; } = [];

    /// <summary>
    /// Gets or sets bottlenecks identified in the analysis
    /// </summary>
    public List<string> Bottlenecks { get; set; } = [];

    /// <summary>
    /// Gets or sets the analysis completion timestamp
    /// </summary>
    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Performance metrics for an individual template
/// </summary>
public class TemplatePerformance
{
    /// <summary>
    /// Gets or sets the template ID
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name
    /// </summary>
    public string TemplateName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the average execution time
    /// </summary>
    public double AverageExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the success rate
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the total executions
    /// </summary>
    public int TotalExecutions { get; set; }
}

/// <summary>
/// Performance data point for trending
/// </summary>
public class PerformanceDataPoint
{
    /// <summary>
    /// Gets or sets the timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the response time in milliseconds
    /// </summary>
    public double ResponseTime { get; set; }

    /// <summary>
    /// Gets or sets the number of executions at this point
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the error rate at this point
    /// </summary>
    public double ErrorRate { get; set; }
}

/// <summary>
/// Resource utilization metrics
/// </summary>
public class ResourceUtilization
{
    /// <summary>
    /// Gets or sets CPU utilization percentage
    /// </summary>
    public double CpuUtilization { get; set; }

    /// <summary>
    /// Gets or sets memory utilization percentage
    /// </summary>
    public double MemoryUtilization { get; set; }

    /// <summary>
    /// Gets or sets network I/O metrics
    /// </summary>
    public double NetworkIO { get; set; }

    /// <summary>
    /// Gets or sets storage I/O metrics
    /// </summary>
    public double StorageIO { get; set; }
}
