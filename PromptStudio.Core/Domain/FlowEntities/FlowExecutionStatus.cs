namespace PromptStudio.Core.Domain;

/// <summary>
/// Flow execution status
/// </summary>
public enum FlowExecutionStatus
{
    Pending = 0,
    Running = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4,
    TimedOut = 5,
    PartiallyCompleted = 6
}
