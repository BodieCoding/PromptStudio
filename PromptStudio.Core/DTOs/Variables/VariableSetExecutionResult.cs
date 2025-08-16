namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Individual execution result for a single variable set within batch processing operations with comprehensive outcome details.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by batch execution services to track individual variable set processing results within larger batch operations.
/// Enables granular success/failure analysis, performance monitoring, and detailed debugging capabilities for
/// complex batch processing workflows and systematic template testing scenarios.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains variable set identification, execution outcome details, performance metrics, and error information
/// for individual variable set processing. Supports both real-time execution monitoring and post-execution
/// analysis for optimization, troubleshooting, and quality assurance workflows.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>Batch Execution Service - Individual variable set result tracking and aggregation</item>
///   <item>Error Analysis Service - Granular failure analysis and troubleshooting support</item>
///   <item>Performance Service - Individual execution performance monitoring and optimization</item>
///   <item>Quality Service - Variable set success rate analysis and quality assessment</item>
///   <item>Debug Service - Detailed execution tracing and problem diagnosis</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Index-based identification enables precise result correlation and ordering</item>
///   <item>Variable dictionary provides complete input context for result interpretation</item>
///   <item>Execution outcome supports both success analysis and failure diagnosis</item>
///   <item>Performance metrics enable optimization and bottleneck identification</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Batch operation result aggregation and statistical analysis</item>
///   <item>Individual execution debugging and error analysis workflows</item>
///   <item>Performance monitoring and optimization insight generation</item>
///   <item>Quality assurance validation and success rate tracking</item>
///   <item>A/B testing result analysis and comparison workflows</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Variable dictionary size impacts memory usage for large variable sets</item>
///   <item>Resolved prompt content may be substantial for complex templates</item>
///   <item>Metadata collection should be bounded for memory efficiency</item>
///   <item>Consider result streaming for very large batch operations</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for batch result analysis
/// var results = await batchService.GetExecutionResultsAsync(batchId);
/// var failedResults = results.Where(r =&gt; !r.Success).ToList();
/// foreach (var failure in failedResults) {
///     await errorService.AnalyzeFailureAsync(failure.Index, failure.Variables, failure.Error);
/// }
/// </code>
/// </example>
public class VariableSetExecutionResult
{
    /// <summary>
    /// Zero-based index position of this variable set within the batch execution sequence.
    /// Provides ordering context and enables precise result correlation with input data.
    /// Critical for maintaining execution order and enabling detailed batch analysis workflows.
    /// </summary>
    public int Index { get; set; }
    
    /// <summary>
    /// Dictionary containing the complete variable name-value pairs used for this execution.
    /// Provides full input context for result interpretation and debugging workflows.
    /// Essential for correlating execution outcomes with specific input configurations.
    /// Used by error analysis services for troubleshooting and optimization insights.
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = [];
    
    /// <summary>
    /// Complete resolved prompt content after variable substitution and processing.
    /// Contains the final prompt text that was sent for execution with all variables resolved.
    /// Critical for debugging variable substitution issues and prompt validation workflows.
    /// May be substantial in size for complex templates with extensive variable content.
    /// </summary>
    public string? ResolvedPrompt { get; set; }
    
    /// <summary>
    /// Indicates whether this variable set execution completed successfully without errors.
    /// True for successful executions that produced valid results and completed normally.
    /// False for executions that encountered errors, validation failures, or processing issues.
    /// Service layers should check this before processing execution results or outcomes.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Detailed error message for failed executions providing diagnostic information and troubleshooting context.
    /// Contains actionable error details for debugging, optimization, and error resolution workflows.
    /// Null or empty for successful executions, populated with diagnostic details for failures.
    /// Used by error analysis services for systematic failure pattern identification and resolution.
    /// </summary>
    public string? Error { get; set; }
    
    /// <summary>
    /// Total execution time for this variable set including processing, execution, and result handling.
    /// Provides performance metrics for optimization analysis and bottleneck identification.
    /// Used for performance monitoring, SLA tracking, and execution efficiency optimization.
    /// Critical for identifying slow variable sets and performance optimization opportunities.
    /// </summary>
    public TimeSpan ExecutionTime { get; set; }
    
    /// <summary>
    /// Additional execution metadata and context information for specialized processing scenarios.
    /// Contains provider-specific data, execution context, and custom processing information.
    /// Supports extensible execution tracking and custom analytics requirements.
    /// Used by specialized services for advanced execution analysis and custom workflow support.
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = [];
}
