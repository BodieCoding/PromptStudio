using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Execution history for a variable collection
/// </summary>
public class CollectionExecutionHistory
{
    /// <summary>
    /// Gets or sets the collection ID
    /// </summary>
    public Guid CollectionId { get; set; }

    /// <summary>
    /// Gets or sets the collection name
    /// </summary>
    public string CollectionName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the prompt template ID
    /// </summary>
    public Guid PromptTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the prompt template name
    /// </summary>
    public string PromptTemplateName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of execution records
    /// </summary>
    public List<ExecutionRecord> Executions { get; set; } = [];

    /// <summary>
    /// Gets or sets the total execution count
    /// </summary>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the successful execution count
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Gets or sets the failed execution count
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Gets or sets the average execution duration in milliseconds
    /// </summary>
    public double AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the date range of executions
    /// </summary>
    public DateTimeRange ExecutionDateRange { get; set; } = new();

    /// <summary>
    /// Gets or sets performance statistics
    /// </summary>
    public Dictionary<string, object> Statistics { get; set; } = [];
}

/// <summary>
/// Individual execution record
/// </summary>
public class ExecutionRecord
{
    /// <summary>
    /// Gets or sets the execution ID
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Gets or sets the executed prompt
    /// </summary>
    public string ExecutedPrompt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the variable values used
    /// </summary>
    public Dictionary<string, string> VariableValues { get; set; } = [];

    /// <summary>
    /// Gets or sets the execution result
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// Gets or sets the execution status
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the execution duration in milliseconds
    /// </summary>
    public int DurationMs { get; set; }

    /// <summary>
    /// Gets or sets the execution timestamp
    /// </summary>
    public DateTime ExecutedAt { get; set; }

    /// <summary>
    /// Gets or sets any error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets execution metadata
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = [];
}
