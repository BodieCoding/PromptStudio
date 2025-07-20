using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents a workflow performance entry for analytics insights.
/// </summary>
public class WorkflowPerformanceEntry
{
    /// <summary>
    /// Gets or sets the workflow identifier.
    /// </summary>
    /// <value>Unique identifier for the workflow in the performance analysis.</value>
    public string WorkflowId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow name for identification.
    /// </summary>
    /// <value>Human-readable name of the workflow.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the success rate percentage for this workflow.
    /// </summary>
    /// <value>Success rate percentage (0-100) for the workflow.</value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution duration for this workflow.
    /// </summary>
    /// <value>Average time taken to execute this workflow.</value>
    public TimeSpan AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions for this workflow.
    /// </summary>
    /// <value>Total count of executions for this workflow in the analysis period.</value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the performance score for this workflow.
    /// </summary>
    /// <value>Calculated performance score based on multiple metrics (0-100).</value>
    public double PerformanceScore { get; set; }
}

/// <summary>
/// Represents a resource utilization trend point for temporal analysis.
/// </summary>
public class ResourceUtilizationTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this trend point.
    /// </summary>
    /// <value>Date and time for this resource utilization measurement.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the CPU utilization percentage at this point.
    /// </summary>
    /// <value>CPU utilization percentage (0-100) at this time point.</value>
    public double CpuUtilization { get; set; }

    /// <summary>
    /// Gets or sets the memory utilization in bytes at this point.
    /// </summary>
    /// <value>Memory consumption in bytes at this time point.</value>
    public long MemoryUtilization { get; set; }

    /// <summary>
    /// Gets or sets the network utilization at this point.
    /// </summary>
    /// <value>Network bandwidth utilization in bytes at this time point.</value>
    public long NetworkUtilization { get; set; }

    /// <summary>
    /// Gets or sets the storage utilization at this point.
    /// </summary>
    /// <value>Storage space utilization in bytes at this time point.</value>
    public long StorageUtilization { get; set; }
}

/// <summary>
/// Represents a resource-intensive workflow for optimization analysis.
/// </summary>
public class ResourceIntensiveWorkflow
{
    /// <summary>
    /// Gets or sets the workflow identifier.
    /// </summary>
    /// <value>Unique identifier for the resource-intensive workflow.</value>
    public string WorkflowId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the workflow name.
    /// </summary>
    /// <value>Human-readable name of the workflow.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the resource intensity score.
    /// </summary>
    /// <value>Calculated score representing the resource intensity (0-100).</value>
    public double ResourceIntensityScore { get; set; }

    /// <summary>
    /// Gets or sets the average CPU usage for this workflow.
    /// </summary>
    /// <value>Average CPU utilization percentage for this workflow.</value>
    public double AverageCpuUsage { get; set; }

    /// <summary>
    /// Gets or sets the average memory usage for this workflow.
    /// </summary>
    /// <value>Average memory consumption in bytes for this workflow.</value>
    public long AverageMemoryUsage { get; set; }

    /// <summary>
    /// Gets or sets the total resource cost for this workflow.
    /// </summary>
    /// <value>Total resource cost associated with this workflow.</value>
    public decimal TotalResourceCost { get; set; }
}

/// <summary>
/// Represents a resource optimization opportunity identified through analytics.
/// </summary>
public class ResourceOptimizationOpportunity
{
    /// <summary>
    /// Gets or sets the opportunity identifier.
    /// </summary>
    /// <value>Unique identifier for the optimization opportunity.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the opportunity title.
    /// </summary>
    /// <value>Brief title describing the optimization opportunity.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the opportunity description.
    /// </summary>
    /// <value>Detailed description of the optimization opportunity.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the potential resource savings.
    /// </summary>
    /// <value>Estimated resource savings from implementing this optimization.</value>
    public Dictionary<string, double>? PotentialSavings { get; set; }

    /// <summary>
    /// Gets or sets the implementation complexity level.
    /// </summary>
    /// <value>Complexity level for implementing this optimization (Low, Medium, High).</value>
    public string ComplexityLevel { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated ROI for this optimization.
    /// </summary>
    /// <value>Return on investment percentage expected from this optimization.</value>
    public double? EstimatedRoi { get; set; }
}

/// <summary>
/// Represents an execution volume trend point for workflow analytics.
/// </summary>
public class ExecutionVolumeTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this trend point.
    /// </summary>
    /// <value>Date and time for this execution volume measurement.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the execution count at this point.
    /// </summary>
    /// <value>Number of workflow executions at this time point.</value>
    public long ExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the unique workflow count at this point.
    /// </summary>
    /// <value>Number of unique workflows executed at this time point.</value>
    public int UniqueWorkflowCount { get; set; }

    /// <summary>
    /// Gets or sets the active user count at this point.
    /// </summary>
    /// <value>Number of active users at this time point.</value>
    public int ActiveUserCount { get; set; }
}

/// <summary>
/// Represents a performance trend point for temporal performance analysis.
/// </summary>
public class PerformanceTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this trend point.
    /// </summary>
    /// <value>Date and time for this performance measurement.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the success rate at this point.
    /// </summary>
    /// <value>Success rate percentage (0-100) at this time point.</value>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution duration at this point.
    /// </summary>
    /// <value>Average execution duration at this time point.</value>
    public TimeSpan AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the throughput at this point.
    /// </summary>
    /// <value>Execution throughput (executions per unit time) at this time point.</value>
    public double Throughput { get; set; }

    /// <summary>
    /// Gets or sets the error rate at this point.
    /// </summary>
    /// <value>Error rate percentage (0-100) at this time point.</value>
    public double ErrorRate { get; set; }
}

/// <summary>
/// Represents a cost trend point for financial analysis.
/// </summary>
public class CostTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this trend point.
    /// </summary>
    /// <value>Date and time for this cost measurement.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the total cost at this point.
    /// </summary>
    /// <value>Total cost incurred at this time point.</value>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Gets or sets the cost per execution at this point.
    /// </summary>
    /// <value>Average cost per workflow execution at this time point.</value>
    public decimal CostPerExecution { get; set; }

    /// <summary>
    /// Gets or sets the resource cost at this point.
    /// </summary>
    /// <value>Resource-related costs at this time point.</value>
    public decimal ResourceCost { get; set; }

    /// <summary>
    /// Gets or sets the operational cost at this point.
    /// </summary>
    /// <value>Operational costs at this time point.</value>
    public decimal OperationalCost { get; set; }
}

/// <summary>
/// Represents a user activity trend point for behavioral analysis.
/// </summary>
public class UserActivityTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this trend point.
    /// </summary>
    /// <value>Date and time for this user activity measurement.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the active user count at this point.
    /// </summary>
    /// <value>Number of active users at this time point.</value>
    public int ActiveUserCount { get; set; }

    /// <summary>
    /// Gets or sets the new user count at this point.
    /// </summary>
    /// <value>Number of new users at this time point.</value>
    public int NewUserCount { get; set; }

    /// <summary>
    /// Gets or sets the user engagement score at this point.
    /// </summary>
    /// <value>Calculated user engagement score (0-100) at this time point.</value>
    public double UserEngagementScore { get; set; }

    /// <summary>
    /// Gets or sets the average sessions per user at this point.
    /// </summary>
    /// <value>Average number of sessions per user at this time point.</value>
    public double AverageSessionsPerUser { get; set; }
}

/// <summary>
/// Represents an error rate trend point for reliability analysis.
/// </summary>
public class ErrorRateTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this trend point.
    /// </summary>
    /// <value>Date and time for this error rate measurement.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the overall error rate at this point.
    /// </summary>
    /// <value>Overall error rate percentage (0-100) at this time point.</value>
    public double ErrorRate { get; set; }

    /// <summary>
    /// Gets or sets the critical error count at this point.
    /// </summary>
    /// <value>Number of critical errors at this time point.</value>
    public int CriticalErrorCount { get; set; }

    /// <summary>
    /// Gets or sets the warning count at this point.
    /// </summary>
    /// <value>Number of warnings at this time point.</value>
    public int WarningCount { get; set; }

    /// <summary>
    /// Gets or sets the timeout error count at this point.
    /// </summary>
    /// <value>Number of timeout errors at this time point.</value>
    public int TimeoutErrorCount { get; set; }
}
