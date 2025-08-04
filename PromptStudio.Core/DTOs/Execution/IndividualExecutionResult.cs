namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Represents the result of an individual execution within a batch operation.
/// Extends ExecutionResult with batch-specific context and ordering information.
/// </summary>
/// <remarks>
/// <para><strong>Batch Context:</strong></para>
/// <para>Provides additional context for individual executions that are part of a larger batch operation.
/// Maintains the relationship between individual results and their position within the batch,
/// enabling detailed tracking and debugging of batch processing operations.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>Individual results within CSV-based batch processing</description></item>
/// <item><description>Per-item tracking in bulk prompt template execution</description></item>
/// <item><description>Error correlation and debugging in batch operations</description></item>
/// <item><description>Progress reporting and partial result retrieval</description></item>
/// </list>
/// 
/// <para><strong>Inheritance Pattern:</strong></para>
/// <para>Inherits all functionality from ExecutionResult while adding batch-specific metadata.
/// This design enables individual results to be used independently or as part of batch collections
/// without losing execution context or functionality.</para>
/// </remarks>
public class IndividualExecutionResult : ExecutionResult
{
    /// <summary>
    /// Gets or sets the zero-based index of this execution within the batch operation.
    /// Provides ordering context for batch result correlation and debugging.
    /// </summary>
    /// <value>
    /// The position of this execution within the batch, starting from 0.
    /// </value>
    public int BatchIndex { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the batch operation containing this execution.
    /// Links individual results back to their parent batch for tracking and correlation.
    /// </summary>
    /// <value>
    /// The unique identifier of the parent batch operation, or null if not part of a batch.
    /// </value>
    public Guid? BatchId { get; set; }

    /// <summary>
    /// Gets or sets an identifier for the source row or item that generated this execution.
    /// Provides traceability from execution results back to source data for debugging.
    /// </summary>
    /// <value>
    /// An identifier for the source data item (e.g., CSV row number, database record ID).
    /// </value>
    public string? SourceIdentifier { get; set; }

    /// <summary>
    /// Gets or sets the name of the prompt template executed for this individual result.
    /// Provides template context when multiple templates are processed in a single batch.
    /// </summary>
    /// <value>
    /// The name of the executed prompt template, or null if not applicable.
    /// </value>
    public string? PromptTemplateName { get; set; }

    /// <summary>
    /// Gets or sets additional context information specific to this individual execution.
    /// Provides batch-specific metadata that supplements the base execution metadata.
    /// </summary>
    /// <value>
    /// A dictionary containing individual execution context such as source row data, variable context, or processing options.
    /// </value>
    public Dictionary<string, object> BatchContext { get; set; } = new();

    /// <summary>
    /// Gets or sets the order in which this execution was processed relative to other executions in the batch.
    /// May differ from BatchIndex when parallel processing or custom ordering is used.
    /// </summary>
    /// <value>
    /// The processing order number, or null if processing order is not tracked.
    /// </value>
    public int? ProcessingOrder { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this execution was retried during batch processing.
    /// Provides insight into batch processing resilience and error recovery.
    /// </summary>
    /// <value>
    /// True if this execution was retried after initial failure; false otherwise.
    /// </value>
    public bool WasRetried { get; set; }

    /// <summary>
    /// Gets or sets the number of retry attempts made for this execution.
    /// Provides detailed retry information for batch processing analysis.
    /// </summary>
    /// <value>
    /// The number of retry attempts, or 0 if no retries were performed.
    /// </value>
    public int RetryCount { get; set; }

    /// <summary>
    /// Gets a value indicating whether this execution has source identification information.
    /// Convenience property for checking source traceability availability.
    /// </summary>
    /// <value>
    /// True if SourceIdentifier contains non-null, non-empty content; false otherwise.
    /// </value>
    public bool HasSourceIdentifier => !string.IsNullOrEmpty(SourceIdentifier);

    /// <summary>
    /// Gets a value indicating whether this execution has batch context information.
    /// Convenience property for checking batch-specific metadata availability.
    /// </summary>
    /// <value>
    /// True if BatchContext contains one or more entries; false otherwise.
    /// </value>
    public bool HasBatchContext => BatchContext.Any();

    /// <summary>
    /// Initializes a new instance of the IndividualExecutionResult class with default values.
    /// Creates an individual result instance ready for population by batch execution services.
    /// </summary>
    public IndividualExecutionResult()
    {
    }

    /// <summary>
    /// Initializes a new instance of the IndividualExecutionResult class with batch context.
    /// Creates an individual result with specified batch position and identifier.
    /// </summary>
    /// <param name="batchIndex">The zero-based index of this execution within the batch</param>
    /// <param name="batchId">The unique identifier of the parent batch operation</param>
    public IndividualExecutionResult(int batchIndex, Guid batchId)
    {
        BatchIndex = batchIndex;
        BatchId = batchId;
    }

    /// <summary>
    /// Initializes a new instance of the IndividualExecutionResult class with full batch context.
    /// Creates an individual result with specified batch information and source identification.
    /// </summary>
    /// <param name="batchIndex">The zero-based index of this execution within the batch</param>
    /// <param name="batchId">The unique identifier of the parent batch operation</param>
    /// <param name="sourceIdentifier">An identifier for the source data that generated this execution</param>
    public IndividualExecutionResult(int batchIndex, Guid batchId, string sourceIdentifier)
    {
        BatchIndex = batchIndex;
        BatchId = batchId;
        SourceIdentifier = sourceIdentifier;
    }

    /// <summary>
    /// Creates a successful IndividualExecutionResult with batch context and output content.
    /// Factory method for creating successful individual execution results within batches.
    /// </summary>
    /// <param name="output">The output content generated by the execution</param>
    /// <param name="batchIndex">The zero-based index of this execution within the batch</param>
    /// <param name="batchId">The unique identifier of the parent batch operation</param>
    /// <param name="sourceIdentifier">Optional identifier for the source data</param>
    /// <returns>A new IndividualExecutionResult instance marked as successful</returns>
    public static IndividualExecutionResult BatchSuccess(string output, int batchIndex, Guid batchId, string? sourceIdentifier = null)
    {
        return new IndividualExecutionResult(batchIndex, batchId, sourceIdentifier ?? string.Empty)
        {
            IsSuccess = true,
            Output = output,
            ExecutionId = Guid.NewGuid()
        };
    }

    /// <summary>
    /// Creates a failed IndividualExecutionResult with batch context and error message.
    /// Factory method for creating failed individual execution results within batches.
    /// </summary>
    /// <param name="error">The error message describing the failure</param>
    /// <param name="batchIndex">The zero-based index of this execution within the batch</param>
    /// <param name="batchId">The unique identifier of the parent batch operation</param>
    /// <param name="sourceIdentifier">Optional identifier for the source data</param>
    /// <returns>A new IndividualExecutionResult instance marked as failed</returns>
    public static IndividualExecutionResult BatchFailure(string error, int batchIndex, Guid batchId, string? sourceIdentifier = null)
    {
        return new IndividualExecutionResult(batchIndex, batchId, sourceIdentifier ?? string.Empty)
        {
            IsSuccess = false,
            ExecutionId = Guid.NewGuid(),
            Errors = new List<string> { error }
        };
    }

    /// <summary>
    /// Adds batch-specific context information to the execution result.
    /// Provides fluent API for accumulating batch context metadata.
    /// </summary>
    /// <param name="key">The context key</param>
    /// <param name="value">The context value</param>
    /// <returns>This IndividualExecutionResult instance for method chaining</returns>
    public IndividualExecutionResult AddBatchContext(string key, object value)
    {
        BatchContext[key] = value;
        return this;
    }

    /// <summary>
    /// Marks this execution as having been retried during batch processing.
    /// Updates retry tracking information for batch processing analysis.
    /// </summary>
    /// <param name="retryCount">The number of retry attempts made</param>
    /// <returns>This IndividualExecutionResult instance for method chaining</returns>
    public IndividualExecutionResult MarkRetried(int retryCount = 1)
    {
        WasRetried = true;
        RetryCount = retryCount;
        return this;
    }

    /// <summary>
    /// Sets the processing order for this execution within the batch.
    /// Provides ordering information for batch processing analysis and debugging.
    /// </summary>
    /// <param name="processingOrder">The order in which this execution was processed</param>
    /// <returns>This IndividualExecutionResult instance for method chaining</returns>
    public IndividualExecutionResult SetProcessingOrder(int processingOrder)
    {
        ProcessingOrder = processingOrder;
        return this;
    }

    /// <summary>
    /// Gets a formatted identifier that combines batch and source information.
    /// Provides a human-readable identifier for logging and debugging purposes.
    /// </summary>
    /// <value>
    /// A formatted string combining batch index and source identifier information.
    /// </value>
    public string FormattedIdentifier
    {
        get
        {
            if (HasSourceIdentifier)
                return $"Batch[{BatchIndex}]:{SourceIdentifier}";
            return $"Batch[{BatchIndex}]";
        }
    }

    /// <summary>
    /// Creates a summary report of the individual execution result within its batch context.
    /// Provides formatted text summary suitable for logging and batch analysis.
    /// </summary>
    /// <returns>A formatted string containing individual execution details and batch context</returns>
    public string CreateBatchSummaryReport()
    {
        var baseReport = ToString();
        var batchInfo = $"Batch Context:\n" +
                       $"  Batch ID: {BatchId}\n" +
                       $"  Batch Index: {BatchIndex}\n" +
                       $"  Source: {SourceIdentifier ?? "Not specified"}\n" +
                       $"  Template: {PromptTemplateName ?? "Not specified"}\n" +
                       $"  Processing Order: {ProcessingOrder?.ToString() ?? "Not tracked"}\n" +
                       $"  Retried: {(WasRetried ? $"Yes ({RetryCount} times)" : "No")}\n" +
                       $"  Context Items: {BatchContext.Count}";
        
        return $"{baseReport}\n{batchInfo}";
    }

    /// <summary>
    /// Returns a string representation of the individual execution result with batch context.
    /// Provides essential execution and batch information in a readable format.
    /// </summary>
    /// <returns>String representation including batch index, success status, and timing information</returns>
    public override string ToString()
    {
        var baseInfo = base.ToString();
        var batchInfo = $" [{FormattedIdentifier}]";
        var retryInfo = WasRetried ? $" (Retried {RetryCount}x)" : "";
        
        return $"{baseInfo}{batchInfo}{retryInfo}";
    }
}
