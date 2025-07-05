namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Comprehensive result summary for batch template execution operations with detailed metrics and outcome analysis.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by batch execution services to return structured results from multi-variable set processing operations.
/// Enables systematic analysis of large-scale template testing, A/B testing workflows, and bulk data processing
/// with comprehensive success/failure analysis and performance metrics for optimization insights.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Aggregates individual variable set execution results with batch-level statistics, performance metrics,
/// and detailed outcome analysis. Supports both real-time monitoring during execution and post-execution
/// analysis for optimization, quality assurance, and operational reporting requirements.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Batch execution services populate this with aggregated results
/// - Analytics services consume metrics for performance trend analysis
/// - Testing services use this for A/B test statistical analysis
/// - Quality assurance services analyze failure patterns and success rates
/// - Reporting services format this data for executive dashboards
/// 
/// <para><strong>Performance Analysis:</strong></para>
/// Success rate calculation: SuccessfulExecutions / TotalVariableSets
/// Average execution time: TotalExecutionTime / TotalVariableSets
/// Error rate analysis available through individual Results collection
/// 
/// <para><strong>Memory Considerations:</strong></para>
/// Results collection can be large for extensive batch operations
/// Consider pagination or result streaming for very large batches
/// ExecutionMetrics provides summary data without full result details
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage
/// var batchResult = await batchExecutionService.ExecuteAsync(templateId, variableSets);
/// var successRate = (double)batchResult.SuccessfulExecutions / batchResult.TotalVariableSets;
/// if (successRate &lt; 0.95) {
///     await alertService.NotifyQualityIssueAsync(batchResult.ExecutionId, successRate);
/// }
/// </code>
/// </example>
public class BatchExecutionResult
{
    /// <summary>
    /// Unique identifier for this batch execution operation.
    /// Used for tracking, auditing, and correlating related execution activities
    /// across different services and operational workflows.
    /// </summary>
    public Guid ExecutionId { get; set; }
    
    /// <summary>
    /// Total number of variable sets processed in the batch operation.
    /// Provides baseline for calculating success rates, completion percentages,
    /// and overall batch operation scope for analytics and reporting.
    /// </summary>
    public int TotalVariableSets { get; set; }
    
    /// <summary>
    /// Number of variable sets that executed successfully without errors.
    /// Used for success rate calculation and quality metrics analysis.
    /// Service layers can use this for SLA compliance monitoring.
    /// </summary>
    public int SuccessfulExecutions { get; set; }
    
    /// <summary>
    /// Number of variable sets that failed during execution.
    /// Enables error rate analysis and quality issue identification.
    /// Should be analyzed with Results collection for failure pattern insights.
    /// </summary>
    public int FailedExecutions { get; set; }
    
    /// <summary>
    /// Total time consumed for the complete batch execution operation.
    /// Includes all variable set processing, overhead, and coordination time.
    /// Used for performance analysis and resource planning by service layers.
    /// </summary>
    public TimeSpan TotalExecutionTime { get; set; }
    
    /// <summary>
    /// Detailed execution results for each individual variable set in the batch.
    /// Provides granular outcome analysis, error details, and individual metrics.
    /// Service layers can analyze this for detailed failure investigation and optimization.
    /// </summary>
    public List<VariableSetExecutionResult> Results { get; set; } = new();
    
    /// <summary>
    /// Aggregated execution metrics and performance indicators for the batch operation.
    /// Common metrics: average_execution_time, token_usage_total, cost_total, error_categories.
    /// Service layers can extract specialized metrics for analytics and optimization insights.
    /// </summary>
    public Dictionary<string, object> ExecutionMetrics { get; set; } = new();
}
