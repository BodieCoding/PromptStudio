using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Node performance metrics
/// </summary>
public class NodePerformanceMetrics
{
    /// <summary>
    /// Node ID
    /// </summary>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Node name
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Node type
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// Average execution time for this node
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Number of times this node was executed
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Success rate for this node
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Token usage for this node
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Total cost for this node
    /// </summary>
    public decimal? TotalCost { get; set; }
}
