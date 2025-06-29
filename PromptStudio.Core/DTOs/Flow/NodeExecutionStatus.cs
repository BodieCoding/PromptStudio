namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Node execution status
/// </summary>
public enum NodeExecutionStatus
{
    Pending = 0,
    Running = 1,
    Success = 2,
    Failed = 3,
    Cancelled = 4,
    Timeout = 5,
    Skipped = 6,
    Retrying = 7
}
