namespace PromptStudio.Core.Domain;

/// <summary>
/// Audit status enumeration
/// </summary>
public enum AuditStatus
{
    Planned = 0,
    InProgress = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4
}
