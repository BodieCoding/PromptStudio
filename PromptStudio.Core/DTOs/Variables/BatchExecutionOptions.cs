namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Configuration options for batch template execution operations controlling performance, reliability, and operational behavior.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by batch execution services to configure execution parameters, performance characteristics, and operational
/// behavior for large-scale template processing workflows. Enables fine-tuned control over concurrency, error handling,
/// result management, and execution metadata for optimal performance and reliability in diverse operational scenarios.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains performance tuning parameters, error handling configurations, result management options, and execution
/// metadata settings. Supports both high-throughput batch processing and careful controlled execution scenarios
/// with comprehensive configuration flexibility for diverse operational requirements.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>Batch Execution Service - Primary configuration for execution orchestration and performance tuning</item>
///   <item>Performance Service - Concurrency and timeout management for optimal resource utilization</item>
///   <item>Error Handling Service - Failure behavior configuration and resilience management</item>
///   <item>Storage Service - Result persistence and intermediate data management configuration</item>
///   <item>Monitoring Service - Execution tracking and operational metadata collection</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Concurrency settings enable performance optimization and resource management</item>
///   <item>Timeout configurations provide execution control and resource protection</item>
///   <item>Error handling options support resilient execution and partial success scenarios</item>
///   <item>Storage options enable result management and intermediate data persistence</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>High-throughput batch processing with optimized concurrency settings</item>
///   <item>Controlled execution scenarios with conservative concurrency and timeouts</item>
///   <item>Resilient processing workflows with continue-on-error configurations</item>
///   <item>Debug-friendly executions with intermediate result saving enabled</item>
///   <item>Custom processing scenarios with specialized execution metadata</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Higher concurrency improves throughput but increases resource consumption</item>
///   <item>Shorter timeouts reduce resource usage but may cause false failures</item>
///   <item>Intermediate result saving impacts storage requirements and performance</item>
///   <item>Execution metadata collection should be balanced against performance needs</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for high-throughput batch processing
/// var options = new BatchExecutionOptions {
///     MaxConcurrency = 20,
///     Timeout = TimeSpan.FromMinutes(10),
///     ContinueOnError = true,
///     SaveIntermediateResults = false
/// };
/// var result = await batchService.ExecuteAsync(templateId, variableSets, options);
/// </code>
/// </example>
public class BatchExecutionOptions
{
    /// <summary>
    /// Maximum number of concurrent executions allowed during batch processing.
    /// Controls resource utilization and system load during large-scale operations.
    /// Higher values improve throughput but increase memory and processing resource consumption.
    /// Recommended range: 5-50 depending on system capacity and template complexity.
    /// </summary>
    public int MaxConcurrency { get; set; } = 10;
    
    /// <summary>
    /// Maximum time allowed for individual variable set execution before timeout.
    /// Prevents runaway executions and ensures predictable resource utilization.
    /// Should be set based on expected template complexity and processing requirements.
    /// Shorter timeouts reduce resource usage but may cause legitimate executions to fail.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);
    
    /// <summary>
    /// Determines whether batch execution continues when individual variable sets fail.
    /// When true, individual failures don't stop the entire batch operation.
    /// When false, first failure terminates the entire batch for fail-fast behavior.
    /// Recommended true for exploratory analysis, false for critical production workflows.
    /// </summary>
    public bool ContinueOnError { get; set; } = true;
    
    /// <summary>
    /// Controls whether intermediate execution results are persisted during batch processing.
    /// When true, enables result recovery and partial result analysis for long-running batches.
    /// When false, improves performance by avoiding intermediate storage overhead.
    /// Recommended true for large batches or unreliable execution environments.
    /// </summary>
    public bool SaveIntermediateResults { get; set; } = true;
    
    /// <summary>
    /// Additional execution metadata and custom configuration parameters for specialized processing scenarios.
    /// Supports custom execution behaviors, provider-specific configurations, and workflow customizations.
    /// Used by service extensions and custom processing handlers for advanced execution control.
    /// Enables flexible batch processing without modifying core execution logic.
    /// </summary>
    public Dictionary<string, object> ExecutionMetadata { get; set; } = new();
}
