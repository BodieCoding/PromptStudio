namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Request model for creating variable collections with comprehensive validation and configuration options.
/// </summary>
/// <remarks>
/// <para><strong>Usage:</strong></para>
/// Used to encapsulate all parameters required for variable collection creation, avoiding parameter list code smells
/// and providing a clean, extensible interface for collection creation operations.
/// </remarks>
public class CreateVariableCollectionRequest
{
    /// <summary>
    /// Gets or sets the name of the variable collection.
    /// </summary>
    /// <value>A descriptive name for the collection that must be unique within the template scope.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the template identifier that this collection is associated with.
    /// </summary>
    /// <value>The unique identifier of the prompt template.</value>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the CSV data content for the collection.
    /// </summary>
    /// <value>CSV formatted data containing variable values for the collection.</value>
    public string CsvData { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an optional description for the collection.
    /// </summary>
    /// <value>A detailed description explaining the purpose and contents of the collection.</value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets optional metadata for the collection.
    /// </summary>
    /// <value>Additional metadata for collection categorization and management.</value>
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Gets or sets validation options for the collection creation.
    /// </summary>
    /// <value>Configuration options for data validation during collection creation.</value>
    public ValidationOptions? ValidationOptions { get; set; }
}

/// <summary>
/// Validation options for collection operations.
/// </summary>
public class ValidationOptions
{
    /// <summary>
    /// Gets or sets whether to perform strict validation.
    /// </summary>
    public bool StrictValidation { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to skip empty rows during validation.
    /// </summary>
    public bool SkipEmptyRows { get; set; } = true;

    /// <summary>
    /// Gets or sets the maximum number of validation errors to report.
    /// </summary>
    public int MaxErrors { get; set; } = 100;
}
