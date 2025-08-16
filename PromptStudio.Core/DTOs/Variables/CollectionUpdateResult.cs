using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of variable collection update operations with change tracking and validation details.
/// </summary>
/// <remarks>
/// <para><strong>Usage:</strong></para>
/// Provides comprehensive information about collection update operations including success status,
/// validation results, and change tracking information.
/// </remarks>
public class CollectionUpdateResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the update was successful.
    /// </summary>
    /// <value><c>true</c> if the update completed successfully; otherwise, <c>false</c>.</value>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets the updated collection entity.
    /// </summary>
    /// <value>The updated variable collection, or null if update failed.</value>
    public VariableCollection? UpdatedCollection { get; set; }

    /// <summary>
    /// Gets or sets validation errors that occurred during the update.
    /// </summary>
    /// <value>Collection of validation error messages.</value>
    public List<string> ValidationErrors { get; set; } = [];

    /// <summary>
    /// Gets or sets warning messages from the update operation.
    /// </summary>
    /// <value>Collection of warning messages for non-critical issues.</value>
    public List<string> Warnings { get; set; } = [];

    /// <summary>
    /// Gets or sets details about the changes made to the collection.
    /// </summary>
    /// <value>Dictionary containing information about what was changed.</value>
    public Dictionary<string, object> ChangeDetails { get; set; } = [];

    /// <summary>
    /// Gets or sets statistics about the update operation.
    /// </summary>
    /// <value>Dictionary containing update operation statistics.</value>
    public Dictionary<string, object> UpdateStatistics { get; set; } = [];
}
