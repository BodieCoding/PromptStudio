namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Progress information for long-running import operations
/// </summary>
public class ImportProgress
{
    /// <summary>
    /// Gets or sets the current import stage
    /// </summary>
    public string Stage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the overall progress percentage (0-100)
    /// </summary>
    public int ProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets the number of records processed
    /// </summary>
    public int ProcessedRecords { get; set; }

    /// <summary>
    /// Gets or sets the total number of records to process
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Gets or sets the current operation description
    /// </summary>
    public string CurrentOperation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets any errors encountered during import
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Gets or sets any warnings generated during import
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Gets or sets whether the import is complete
    /// </summary>
    public bool IsComplete { get; set; }

    /// <summary>
    /// Gets or sets whether the import was successful
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets the import start time
    /// </summary>
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the estimated completion time
    /// </summary>
    public DateTime? EstimatedCompletionAt { get; set; }

    /// <summary>
    /// Gets or sets additional metadata about the import process
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}
