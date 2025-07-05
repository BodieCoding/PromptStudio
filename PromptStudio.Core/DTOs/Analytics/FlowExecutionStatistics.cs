namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents comprehensive execution statistics for a workflow/flow, providing operational insights and performance metrics.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Core analytics DTO for flow execution monitoring, operational dashboards, and performance optimization.
/// Used by monitoring services, alerting systems, and capacity planning tools in LLMOps environments.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Aggregated statistical data with computed properties for real-time monitoring.
/// Includes both raw metrics and calculated performance indicators for comprehensive flow analysis.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Real-time operational monitoring and alerting</item>
/// <item>Flow performance optimization and bottleneck identification</item>
/// <item>Cost analysis and resource utilization tracking</item>
/// <item>User adoption and engagement analytics</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Optimized for frequent dashboard updates with computed properties for efficiency.
/// Nullable fields allow for gradual data population and handle missing metrics gracefully.
/// TokenUsage nesting provides detailed cost analysis without payload inflation.</para>
/// </remarks>
public class FlowExecutionStatistics
{
    /// <summary>
    /// Gets or sets the unique identifier of the flow being analyzed.
    /// </summary>
    /// <value>A valid GUID representing the flow entity.</value>
    public Guid FlowId { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions recorded for this flow.
    /// </summary>
    /// <value>A non-negative integer representing the cumulative execution count.</value>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of successful executions without errors.
    /// </summary>
    /// <value>A non-negative integer that should not exceed TotalExecutions.</value>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of failed executions due to errors or timeouts.
    /// </summary>
    /// <value>A non-negative integer representing failed execution attempts.</value>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Gets the calculated success rate as a percentage (0-100).
    /// </summary>
    /// <value>A computed percentage where 100 represents perfect success rate, or 0 if no executions recorded.</value>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Gets or sets the average execution duration across all completed executions.
    /// </summary>
    /// <value>A TimeSpan representing the mean execution time, or null if duration data is unavailable.</value>
    public TimeSpan? AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the most recent execution.
    /// </summary>
    /// <value>A DateTime representing the last execution time, or null if no executions recorded.</value>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the first execution in the analysis period.
    /// </summary>
    /// <value>A DateTime representing the earliest execution time, or null if no executions recorded.</value>
    public DateTime? FirstExecution { get; set; }

    /// <summary>
    /// Gets or sets the count of unique users who have executed this flow.
    /// </summary>
    /// <value>A non-negative integer representing distinct user count based on user identification.</value>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Gets or sets the aggregated token usage statistics across all executions.
    /// </summary>
    /// <value>A TokenUsage object containing detailed token consumption metrics, or null if token tracking is disabled.</value>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Gets or sets the total monetary cost incurred across all executions.
    /// </summary>
    /// <value>A decimal representing the cumulative cost in the configured currency, or null if cost tracking is disabled.</value>
    public decimal? TotalCost { get; set; }
}