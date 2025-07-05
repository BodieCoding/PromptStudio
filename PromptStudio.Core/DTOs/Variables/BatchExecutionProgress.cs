namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Real-time progress tracking data transfer object for batch execution operations with comprehensive metrics and timing information.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by batch execution services to provide real-time progress updates, performance monitoring, and completion
/// estimation for long-running template processing operations. Enables responsive user interfaces, progress
/// dashboards, and operational monitoring for large-scale batch processing workflows and system optimization.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains execution progress metrics, success/failure statistics, timing information, and completion estimates.
/// Supports both real-time progress monitoring and post-execution analysis with comprehensive operational
/// visibility for performance optimization and user experience enhancement.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>Batch Execution Service - Real-time progress tracking and status reporting</item>
///   <item>Progress Monitoring Service - User interface updates and dashboard data provision</item>
///   <item>Performance Service - Execution performance analysis and optimization insights</item>
///   <item>Notification Service - Progress-based alerts and completion notifications</item>
///   <item>Analytics Service - Batch execution performance tracking and trend analysis</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Progress metrics enable real-time completion percentage calculation</item>
///   <item>Success/failure statistics provide quality monitoring during execution</item>
///   <item>Timing information supports performance analysis and completion estimation</item>
///   <item>Current item tracking enables detailed progress visualization</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Real-time progress bars and user interface updates for long operations</item>
///   <item>Performance monitoring dashboards and operational visibility systems</item>
///   <item>Progress-based notification systems and completion alerts</item>
///   <item>Performance optimization analysis based on execution timing patterns</item>
///   <item>Quality monitoring with real-time success/failure rate tracking</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Progress updates should be throttled to avoid excessive overhead</item>
///   <item>Time estimation calculations may be computationally intensive</item>
///   <item>Current item tracking adds minimal overhead but improves user experience</item>
///   <item>Statistics should be updated efficiently to maintain batch performance</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for progress monitoring
/// var progress = await batchService.GetExecutionProgressAsync(batchId);
/// if (progress.PercentComplete &gt;= 50 && !notificationSent) {
///     await notificationService.SendProgressUpdateAsync(progress);
///     notificationSent = true;
/// }
/// var successRate = (double)progress.SuccessfulItems / Math.Max(1, progress.CompletedItems);
/// </code>
/// </example>
public class BatchExecutionProgress
{
    /// <summary>
    /// Total number of items scheduled for processing in the batch execution operation.
    /// Provides the complete scope for progress calculation and completion estimation.
    /// Used as the denominator for percentage calculations and progress bar visualization.
    /// Critical for understanding the full scale and scope of the batch operation.
    /// </summary>
    public int TotalItems { get; set; }
    
    /// <summary>
    /// Number of items that have finished processing regardless of success or failure outcome.
    /// Includes both successful and failed processing attempts for comprehensive progress tracking.
    /// Used for completion percentage calculation and progress visualization in user interfaces.
    /// Represents the actual work completed rather than just successful outcomes.
    /// </summary>
    public int CompletedItems { get; set; }
    
    /// <summary>
    /// Number of items that completed processing successfully without errors or failures.
    /// Subset of CompletedItems representing quality outcomes and operational success.
    /// Used for success rate calculation and quality monitoring during execution.
    /// Critical for understanding batch operation effectiveness and data quality.
    /// </summary>
    public int SuccessfulItems { get; set; }
    
    /// <summary>
    /// Number of items that failed processing due to errors, validation issues, or system problems.
    /// Subset of CompletedItems representing processing failures requiring attention.
    /// Used for failure rate monitoring and error pattern analysis during execution.
    /// Important for identifying systematic issues and quality concerns in real-time.
    /// </summary>
    public int FailedItems { get; set; }
    
    /// <summary>
    /// Calculated completion percentage based on completed items relative to total items.
    /// Computed as (CompletedItems / TotalItems) × 100 for progress visualization.
    /// Returns 0 when no items are scheduled to prevent division by zero errors.
    /// Primary metric for progress bars, dashboards, and completion status displays.
    /// </summary>
    public double PercentComplete => TotalItems > 0 ? (double)CompletedItems / TotalItems * 100 : 0;
    
    /// <summary>
    /// Total time elapsed since the batch execution operation began.
    /// Provides operational timing context and performance analysis data.
    /// Used for performance monitoring, SLA tracking, and completion time estimation.
    /// Critical for understanding execution performance and system efficiency.
    /// </summary>
    public TimeSpan ElapsedTime { get; set; }
    
    /// <summary>
    /// Estimated time remaining until batch execution completion based on current performance patterns.
    /// Calculated using current execution rate and remaining items to process.
    /// Null when estimation is not available or reliable due to insufficient data.
    /// Useful for user experience and operational planning but should be treated as approximate.
    /// </summary>
    public TimeSpan? EstimatedTimeRemaining { get; set; }
    
    /// <summary>
    /// Description or identifier of the item currently being processed for detailed progress visibility.
    /// Provides granular insight into execution progress and current operation context.
    /// Useful for debugging, monitoring, and user interface enhancement with detailed progress information.
    /// May be null or empty when no specific item tracking is available or required.
    /// </summary>
    public string? CurrentItem { get; set; }
}
