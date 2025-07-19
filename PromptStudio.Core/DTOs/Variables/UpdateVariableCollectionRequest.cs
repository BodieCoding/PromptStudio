namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Request model for updating variable collections with change tracking and validation options.
/// </summary>
/// <remarks>
/// <para><strong>Usage:</strong></para>
/// Used to encapsulate all parameters required for variable collection updates, providing a clean interface
/// for collection modification operations with proper change tracking and validation support.
/// </remarks>
public class UpdateVariableCollectionRequest
{
    /// <summary>
    /// Gets or sets the updated name for the collection.
    /// </summary>
    /// <value>New name for the collection, or null to keep existing name.</value>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the updated description for the collection.
    /// </summary>
    /// <value>New description for the collection, or null to keep existing description.</value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets updated CSV data for the collection.
    /// </summary>
    /// <value>Updated CSV formatted data, or null to keep existing data.</value>
    public string? CsvData { get; set; }

    /// <summary>
    /// Gets or sets updated metadata for the collection.
    /// </summary>
    /// <value>Updated metadata dictionary, or null to keep existing metadata.</value>
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Gets or sets validation options for the update operation.
    /// </summary>
    /// <value>Configuration options for data validation during collection update.</value>
    public ValidationOptions? ValidationOptions { get; set; }

    /// <summary>
    /// Gets or sets whether to preserve existing execution history.
    /// </summary>
    /// <value>True to preserve execution history, false to allow breaking changes.</value>
    public bool PreserveExecutionHistory { get; set; } = true;
}
