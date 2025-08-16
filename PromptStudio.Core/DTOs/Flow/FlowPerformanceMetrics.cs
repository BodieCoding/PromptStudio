using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Flow performance metrics
/// </summary>
public class FlowPerformanceMetrics
{
    /// <summary>
    /// Flow ID
    /// </summary>
    public Guid FlowId { get; set; }

    /// <summary>
    /// Average execution time
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Median execution time
    /// </summary>
    public TimeSpan MedianExecutionTime { get; set; }

    /// <summary>
    /// 95th percentile execution time
    /// </summary>
    public TimeSpan P95ExecutionTime { get; set; }

    /// <summary>
    /// 99th percentile execution time
    /// </summary>
    public TimeSpan P99ExecutionTime { get; set; }

    /// <summary>
    /// Fastest execution time
    /// </summary>
    public TimeSpan FastestExecutionTime { get; set; }

    /// <summary>
    /// Slowest execution time
    /// </summary>
    public TimeSpan SlowestExecutionTime { get; set; }

    /// <summary>
    /// Throughput (executions per hour)
    /// </summary>
    public double Throughput { get; set; }

    /// <summary>
    /// Bottleneck nodes (nodes that take the longest time)
    /// </summary>
    public List<NodePerformanceMetrics> BottleneckNodes { get; set; } = [];
}
