namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Enumeration of error severity levels.
/// </summary>
public enum ErrorSeverity
{
    /// <summary>
    /// Informational message, not an error.
    /// </summary>
    Info = 0,

    /// <summary>
    /// Warning message indicating potential issues.
    /// </summary>
    Warning = 1,

    /// <summary>
    /// Error that affects functionality but may be recoverable.
    /// </summary>
    Error = 2,

    /// <summary>
    /// Critical error that prevents continued execution.
    /// </summary>
    Critical = 3,

    /// <summary>
    /// Fatal error that requires immediate attention.
    /// </summary>
    Fatal = 4
}
