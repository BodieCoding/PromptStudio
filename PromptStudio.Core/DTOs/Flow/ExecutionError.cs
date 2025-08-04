namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents an error that occurred during flow execution.
/// Provides detailed error information for debugging and monitoring purposes.
/// </summary>
/// <remarks>
/// <para><strong>Error Tracking:</strong></para>
/// <para>Comprehensive error representation that captures all relevant context for
/// debugging flow execution issues, enabling effective troubleshooting and
/// monitoring of flow reliability in production environments.</para>
/// </remarks>
public class ExecutionError
{
    /// <summary>
    /// Gets or sets the unique identifier for this error instance.
    /// Enables correlation across logs and monitoring systems.
    /// </summary>
    /// <value>
    /// A unique identifier for this error occurrence.
    /// </value>
    public Guid ErrorId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the error code or identifier.
    /// Provides categorization for error handling and monitoring.
    /// </summary>
    /// <value>
    /// A string code identifying the type of error (e.g., "TIMEOUT", "VALIDATION_FAILED").
    /// </value>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the human-readable error message.
    /// Provides clear description of what went wrong.
    /// </summary>
    /// <value>
    /// A descriptive error message for users and developers.
    /// </value>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets detailed error information including stack traces.
    /// Contains technical details for debugging purposes.
    /// </summary>
    /// <value>
    /// Detailed error information including stack traces and technical context.
    /// </value>
    public string? Details { get; set; }

    /// <summary>
    /// Gets or sets the severity level of the error.
    /// Indicates the impact and urgency of the error.
    /// </summary>
    /// <value>
    /// The error severity level.
    /// </value>
    public ErrorSeverity Severity { get; set; } = ErrorSeverity.Error;

    /// <summary>
    /// Gets or sets the identifier of the node where the error occurred.
    /// Provides context for error location within the flow.
    /// </summary>
    /// <value>
    /// The unique identifier of the flow node that generated the error.
    /// </value>
    public Guid? NodeId { get; set; }

    /// <summary>
    /// Gets or sets the name of the node where the error occurred.
    /// Provides human-readable context for error location.
    /// </summary>
    /// <value>
    /// The name of the flow node that generated the error.
    /// </value>
    public string? NodeName { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the error occurred.
    /// Enables temporal analysis of error patterns.
    /// </summary>
    /// <value>
    /// The UTC timestamp when this error was generated.
    /// </value>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the source component or system that generated the error.
    /// Provides context for error origin and ownership.
    /// </summary>
    /// <value>
    /// The name or identifier of the component that generated the error.
    /// </value>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets additional metadata about the error.
    /// Provides extensible storage for error-specific information.
    /// </summary>
    /// <value>
    /// A dictionary containing additional error context and metadata.
    /// </value>
    public Dictionary<string, object> Metadata { get; set; } = new();

    /// <summary>
    /// Gets or sets the inner error that caused this error.
    /// Enables error chaining and root cause analysis.
    /// </summary>
    /// <value>
    /// The underlying error that caused this error, if any.
    /// </value>
    public ExecutionError? InnerError { get; set; }

    /// <summary>
    /// Gets or sets whether this error is recoverable.
    /// Indicates if the operation can be retried or if manual intervention is required.
    /// </summary>
    /// <value>
    /// True if the error condition might be temporary and retryable; false otherwise.
    /// </value>
    public bool IsRecoverable { get; set; } = false;

    /// <summary>
    /// Initializes a new instance of the ExecutionError class with default values.
    /// </summary>
    public ExecutionError()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ExecutionError class with a message.
    /// </summary>
    /// <param name="message">The error message</param>
    public ExecutionError(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Initializes a new instance of the ExecutionError class with a message and details.
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="details">Additional error details</param>
    public ExecutionError(string message, string details)
    {
        Message = message;
        Details = details;
    }

    /// <summary>
    /// Creates an ExecutionError from a .NET Exception.
    /// Extracts relevant information from the exception for flow error tracking.
    /// </summary>
    /// <param name="exception">The exception to convert</param>
    /// <param name="nodeId">Optional node identifier where the error occurred</param>
    /// <param name="nodeName">Optional node name where the error occurred</param>
    /// <returns>A new ExecutionError instance</returns>
    public static ExecutionError FromException(Exception exception, Guid? nodeId = null, string? nodeName = null)
    {
        return new ExecutionError
        {
            Message = exception.Message,
            Details = exception.ToString(),
            NodeId = nodeId,
            NodeName = nodeName,
            Source = exception.Source,
            InnerError = exception.InnerException != null ? FromException(exception.InnerException) : null
        };
    }

    /// <summary>
    /// Returns a string representation of the execution error.
    /// </summary>
    /// <returns>String representation including error message and context</returns>
    public override string ToString()
    {
        var nodeContext = !string.IsNullOrEmpty(NodeName) ? $" in {NodeName}" : "";
        var codeContext = !string.IsNullOrEmpty(ErrorCode) ? $" [{ErrorCode}]" : "";
        return $"ExecutionError{codeContext}: {Message}{nodeContext}";
    }
}
