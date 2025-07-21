using System.ComponentModel.DataAnnotations;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a workflow definition that orchestrates multiple prompts and actions
/// </summary>
public class Workflow : AuditableEntity
{
    /// <summary>
    /// Unique identifier for the workflow
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Workflow name
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Workflow description
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Workflow definition in JSON format
    /// </summary>
    [Required]
    public string Definition { get; set; } = string.Empty;

    /// <summary>
    /// Workflow version
    /// </summary>
    public string Version { get; set; } = "1.0.0";

    /// <summary>
    /// Workflow status
    /// </summary>
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Draft;

    /// <summary>
    /// Whether the workflow is published
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Tags for categorization
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Workflow metadata
    /// </summary>
    public string? Metadata { get; set; }
}

/// <summary>
/// Workflow execution status
/// </summary>
public enum WorkflowStatus
{
    Draft = 0,
    Active = 1,
    Suspended = 2,
    Archived = 3
}

/// <summary>
/// Represents a workflow execution instance
/// </summary>
public class WorkflowExecution : AuditableEntity
{
    /// <summary>
    /// Unique identifier for the execution
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Workflow ID being executed
    /// </summary>
    public Guid WorkflowId { get; set; }

    /// <summary>
    /// Execution status
    /// </summary>
    public FlowExecutionStatus Status { get; set; } = FlowExecutionStatus.Pending;

    /// <summary>
    /// Execution start time
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Execution completion time
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Input data for the execution
    /// </summary>
    public string? InputData { get; set; }

    /// <summary>
    /// Output data from the execution
    /// </summary>
    public string? OutputData { get; set; }

    /// <summary>
    /// Error information if execution failed
    /// </summary>
    public string? ErrorInfo { get; set; }

    /// <summary>
    /// Execution metadata
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// Navigation property to workflow
    /// </summary>
    public virtual Workflow? Workflow { get; set; }
}

/// <summary>
/// Flow execution status enumeration
/// </summary>
public enum FlowExecutionStatus
{
    Pending = 0,
    Running = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4,
    Paused = 5
}
