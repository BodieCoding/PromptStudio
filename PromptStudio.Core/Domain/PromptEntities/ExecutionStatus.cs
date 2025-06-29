namespace PromptStudio.Core.Domain;

/// <summary>
/// Execution status enumeration
/// </summary>
public enum ExecutionStatus
{
    Success = 0,
    Failed = 1,
    Timeout = 2,
    Cancelled = 3,
    PartialSuccess = 4
}
