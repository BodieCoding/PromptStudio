namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Represents the result of a batch execution operation containing multiple individual execution results.
/// Provides comprehensive batch operation metadata and individual result tracking for bulk prompt processing.
/// </summary>
/// <remarks>
/// <para><strong>Batch Processing Pattern:</strong></para>
/// <para>Encapsulates the outcomes of executing multiple prompts or prompt templates in a single batch operation.
/// Supports both parallel and sequential execution scenarios with detailed per-item tracking and overall
/// batch operation statistics for monitoring and reporting purposes.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>CSV-based variable batch execution across multiple prompt templates</description></item>
/// <item><description>Bulk prompt processing with variable substitution from data sources</description></item>
/// <item><description>Multi-template execution workflows with dependency tracking</description></item>
/// <item><description>Performance testing and benchmarking of prompt execution operations</description></item>
/// </list>
/// 
/// <para><strong>Performance Monitoring:</strong></para>
/// <para>Provides detailed timing and throughput metrics for batch operations, enabling performance
/// optimization and resource planning for large-scale prompt processing scenarios.</para>
/// </remarks>
public class BatchExecutionResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the entire batch operation completed successfully.
    /// True only if all individual executions completed without errors.
    /// </summary>
    /// <value>
    /// True if all individual executions succeeded; false if any execution failed or the batch operation encountered errors.
    /// </value>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets the collection of individual execution results within this batch.
    /// Contains the detailed results for each item processed in the batch operation.
    /// </summary>
    /// <value>
    /// A list of IndividualExecutionResult instances representing each execution in the batch.
    /// </value>
    public List<IndividualExecutionResult> Results { get; set; } = new();

    /// <summary>
    /// Gets or sets the unique identifier for this batch execution operation.
    /// Enables tracking and correlation of batch results across system components.
    /// </summary>
    /// <value>
    /// A unique identifier for this batch execution, or null if not assigned.
    /// </value>
    public Guid? BatchId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the batch execution operation started.
    /// Provides timing information for performance monitoring and auditing.
    /// </summary>
    /// <value>
    /// The UTC timestamp when batch execution began, or null if not tracked.
    /// </value>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the batch execution operation completed.
    /// Combined with StartTime, enables duration calculation for performance analysis.
    /// </summary>
    /// <value>
    /// The UTC timestamp when batch execution finished, or null if not tracked or still running.
    /// </value>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets the duration of the entire batch execution operation.
    /// Calculated property providing the time span between batch start and end times.
    /// </summary>
    /// <value>
    /// The batch execution duration, or null if timing information is incomplete.
    /// </value>
    public TimeSpan? TotalDuration => StartTime.HasValue && EndTime.HasValue 
        ? EndTime.Value - StartTime.Value 
        : null;

    /// <summary>
    /// Gets or sets the collection of error messages encountered during batch execution.
    /// Includes batch-level errors that aren't specific to individual executions.
    /// </summary>
    /// <value>
    /// A list of batch-level error messages. Individual execution errors are tracked in the Results collection.
    /// </value>
    public List<string> BatchErrors { get; set; } = new();

    /// <summary>
    /// Gets or sets additional metadata associated with the batch execution.
    /// Provides extensible storage for batch-specific information and configuration.
    /// </summary>
    /// <value>
    /// A dictionary containing batch metadata such as processing options, source information, or performance metrics.
    /// </value>
    public Dictionary<string, object> Metadata { get; set; } = new();

    /// <summary>
    /// Gets the total number of individual executions in this batch.
    /// Provides count information for progress tracking and reporting.
    /// </summary>
    /// <value>
    /// The total count of individual executions in the Results collection.
    /// </value>
    public int TotalCount => Results.Count;

    /// <summary>
    /// Gets the number of individual executions that completed successfully.
    /// Provides success count for batch operation reporting and monitoring.
    /// </summary>
    /// <value>
    /// The count of individual executions with IsSuccess = true.
    /// </value>
    public int SuccessCount => Results.Count(r => r.IsSuccess);

    /// <summary>
    /// Gets the number of individual executions that failed.
    /// Provides failure count for batch operation reporting and error analysis.
    /// </summary>
    /// <value>
    /// The count of individual executions with IsSuccess = false.
    /// </value>
    public int FailureCount => Results.Count(r => !r.IsSuccess);

    /// <summary>
    /// Gets the success rate as a percentage of total executions.
    /// Provides batch operation quality metrics for monitoring and reporting.
    /// </summary>
    /// <value>
    /// The percentage of successful executions (0-100), or 0 if no executions were performed.
    /// </value>
    public double SuccessRate => TotalCount > 0 ? (double)SuccessCount / TotalCount * 100 : 0;

    /// <summary>
    /// Gets the average execution duration across all individual executions.
    /// Provides performance metrics for batch operation analysis.
    /// </summary>
    /// <value>
    /// The average execution duration, or null if no executions have timing information.
    /// </value>
    public TimeSpan? AverageExecutionDuration
    {
        get
        {
            var durationsWithTiming = Results.Where(r => r.Duration.HasValue).Select(r => r.Duration!.Value).ToList();
            return durationsWithTiming.Any() 
                ? TimeSpan.FromTicks((long)durationsWithTiming.Average(d => d.Ticks))
                : null;
        }
    }

    /// <summary>
    /// Gets the throughput rate as executions per second.
    /// Provides performance metrics for batch operation efficiency analysis.
    /// </summary>
    /// <value>
    /// The number of executions per second, or null if timing information is unavailable.
    /// </value>
    public double? ThroughputPerSecond => TotalDuration?.TotalSeconds > 0 
        ? TotalCount / TotalDuration.Value.TotalSeconds 
        : null;

    /// <summary>
    /// Gets a value indicating whether the batch encountered any batch-level errors.
    /// Convenience property for quick error checking at the batch level.
    /// </summary>
    /// <value>
    /// True if the BatchErrors collection contains one or more error messages; false otherwise.
    /// </value>
    public bool HasBatchErrors => BatchErrors.Any();

    /// <summary>
    /// Gets a value indicating whether any individual executions in the batch failed.
    /// Convenience property for checking if any individual operations encountered errors.
    /// </summary>
    /// <value>
    /// True if any individual execution has IsSuccess = false; false if all executions succeeded.
    /// </value>
    public bool HasIndividualFailures => Results.Any(r => !r.IsSuccess);

    /// <summary>
    /// Gets a collection of all error messages from both batch-level and individual execution errors.
    /// Provides a consolidated view of all errors encountered during the batch operation.
    /// </summary>
    /// <value>
    /// A flattened collection of all error messages from the batch and individual executions.
    /// </value>
    public IEnumerable<string> AllErrors
    {
        get
        {
            var allErrors = new List<string>(BatchErrors);
            allErrors.AddRange(Results.SelectMany(r => r.Errors));
            return allErrors;
        }
    }

    /// <summary>
    /// Initializes a new instance of the BatchExecutionResult class with default values.
    /// Creates a batch result instance ready for population by batch execution services.
    /// </summary>
    public BatchExecutionResult()
    {
        BatchId = Guid.NewGuid();
    }

    /// <summary>
    /// Initializes a new instance of the BatchExecutionResult class with a specific batch identifier.
    /// Creates a batch result with the specified identifier for tracking purposes.
    /// </summary>
    /// <param name="batchId">The unique identifier for this batch execution</param>
    public BatchExecutionResult(Guid batchId)
    {
        BatchId = batchId;
    }

    /// <summary>
    /// Adds an individual execution result to the batch.
    /// Provides fluent API for accumulating individual results during batch processing.
    /// </summary>
    /// <param name="result">The individual execution result to add</param>
    /// <returns>This BatchExecutionResult instance for method chaining</returns>
    public BatchExecutionResult AddResult(IndividualExecutionResult result)
    {
        Results.Add(result);
        UpdateBatchSuccess();
        return this;
    }

    /// <summary>
    /// Adds multiple individual execution results to the batch.
    /// Provides fluent API for accumulating multiple results during batch processing.
    /// </summary>
    /// <param name="results">The collection of individual execution results to add</param>
    /// <returns>This BatchExecutionResult instance for method chaining</returns>
    public BatchExecutionResult AddResults(IEnumerable<IndividualExecutionResult> results)
    {
        Results.AddRange(results);
        UpdateBatchSuccess();
        return this;
    }

    /// <summary>
    /// Adds a batch-level error message to the execution result.
    /// Batch-level errors indicate problems with the batch operation itself, not individual executions.
    /// </summary>
    /// <param name="error">The batch-level error message to add</param>
    /// <returns>This BatchExecutionResult instance for method chaining</returns>
    public BatchExecutionResult AddBatchError(string error)
    {
        BatchErrors.Add(error);
        IsSuccess = false;
        return this;
    }

    /// <summary>
    /// Adds metadata to the batch execution result.
    /// Provides fluent API for accumulating batch-level metadata.
    /// </summary>
    /// <param name="key">The metadata key</param>
    /// <param name="value">The metadata value</param>
    /// <returns>This BatchExecutionResult instance for method chaining</returns>
    public BatchExecutionResult AddMetadata(string key, object value)
    {
        Metadata[key] = value;
        return this;
    }

    /// <summary>
    /// Marks the batch execution as started with the current timestamp.
    /// Begins timing tracking for batch performance monitoring.
    /// </summary>
    /// <returns>This BatchExecutionResult instance for method chaining</returns>
    public BatchExecutionResult MarkStarted()
    {
        StartTime = DateTime.UtcNow;
        return this;
    }

    /// <summary>
    /// Marks the batch execution as completed with the current timestamp.
    /// Completes timing tracking and updates final batch success status.
    /// </summary>
    /// <returns>This BatchExecutionResult instance for method chaining</returns>
    public BatchExecutionResult MarkCompleted()
    {
        EndTime = DateTime.UtcNow;
        UpdateBatchSuccess();
        return this;
    }

    /// <summary>
    /// Updates the batch success status based on current results and errors.
    /// Internal method to maintain consistency between individual results and batch status.
    /// </summary>
    private void UpdateBatchSuccess()
    {
        IsSuccess = !HasBatchErrors && !HasIndividualFailures;
    }

    /// <summary>
    /// Creates a summary report of the batch execution results.
    /// Provides formatted text summary suitable for logging and reporting.
    /// </summary>
    /// <returns>A formatted string containing batch execution statistics and timing information</returns>
    public string CreateSummaryReport()
    {
        var duration = TotalDuration?.TotalSeconds.ToString("F2") ?? "Unknown";
        var throughput = ThroughputPerSecond?.ToString("F2") ?? "Unknown";
        var avgDuration = AverageExecutionDuration?.TotalMilliseconds.ToString("F2") ?? "Unknown";
        
        return $"Batch Execution Summary:\n" +
               $"  Batch ID: {BatchId}\n" +
               $"  Total Executions: {TotalCount}\n" +
               $"  Successful: {SuccessCount} ({SuccessRate:F1}%)\n" +
               $"  Failed: {FailureCount}\n" +
               $"  Duration: {duration} seconds\n" +
               $"  Throughput: {throughput} executions/second\n" +
               $"  Average Execution Time: {avgDuration} ms\n" +
               $"  Batch Errors: {BatchErrors.Count}\n" +
               $"  Overall Status: {(IsSuccess ? "Success" : "Failed")}";
    }

    /// <summary>
    /// Returns a string representation of the batch execution result for debugging and logging.
    /// Provides essential batch information in a readable format.
    /// </summary>
    /// <returns>String representation including batch statistics and timing information</returns>
    public override string ToString()
    {
        var duration = TotalDuration?.TotalSeconds.ToString("F2") ?? "Unknown";
        var status = IsSuccess ? "Success" : "Failed";
        
        return $"BatchExecutionResult: {status} - {SuccessCount}/{TotalCount} succeeded in {duration}s";
    }
}
