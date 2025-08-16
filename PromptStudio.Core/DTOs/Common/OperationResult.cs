namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Generic operation result with success status and comprehensive error reporting.
/// </summary>
/// <remarks>
/// <para><strong>Usage:</strong></para>
/// Standardized result type for operations that need to return success/failure status
/// with detailed error information and optional result data.
/// </remarks>
public class OperationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    /// <value><c>true</c> if the operation completed successfully; otherwise, <c>false</c>.</value>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets error messages if the operation failed.
    /// </summary>
    /// <value>Collection of error messages describing what went wrong.</value>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// Gets or sets warning messages for the operation.
    /// </summary>
    /// <value>Collection of warning messages for non-critical issues.</value>
    public List<string> Warnings { get; set; } = [];

    /// <summary>
    /// Gets or sets additional metadata about the operation.
    /// </summary>
    /// <value>Dictionary containing operation metadata and context information.</value>
    public Dictionary<string, object> Metadata { get; set; } = [];
}

/// <summary>
/// Generic operation result with typed result data.
/// </summary>
/// <typeparam name="T">The type of the result data.</typeparam>
public class OperationResult<T> : OperationResult
{
    /// <summary>
    /// Gets or sets the result data from the operation.
    /// </summary>
    /// <value>The operation result data, or default value if operation failed.</value>
    public T? Data { get; set; }
}
