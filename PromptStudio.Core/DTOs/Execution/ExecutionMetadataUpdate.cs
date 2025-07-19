namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Request to update execution metadata
/// </summary>
public class ExecutionMetadataUpdate
{
    /// <summary>
    /// Execution ID to update
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// New tags to apply (null means no change, empty list means clear all tags)
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// New quality score (null means no change)
    /// </summary>
    public double? QualityScore { get; set; }

    /// <summary>
    /// New quality score explanation
    /// </summary>
    public string? QualityScoreExplanation { get; set; }

    /// <summary>
    /// Additional metadata to merge (null means no change, empty dict means clear all)
    /// </summary>
    public Dictionary<string, object>? AdditionalMetadata { get; set; }

    /// <summary>
    /// New notes or annotations
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Whether this execution should be marked as a favorite/bookmark
    /// </summary>
    public bool? IsFavorite { get; set; }

    /// <summary>
    /// New category or classification
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Priority level update
    /// </summary>
    public ExecutionPriority? Priority { get; set; }

    /// <summary>
    /// Custom status update
    /// </summary>
    public string? CustomStatus { get; set; }

    /// <summary>
    /// Version of the execution record being updated (for optimistic concurrency)
    /// </summary>
    public byte[]? RowVersion { get; set; }
}
