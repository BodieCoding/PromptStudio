using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Request for importing a variable collection from external sources
/// </summary>
public class ImportCollectionRequest
{
    /// <summary>
    /// Gets or sets the collection name
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection description
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the import data (CSV content, JSON, etc.)
    /// </summary>
    [Required]
    public string ImportData { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the import format (CSV, JSON, XML, etc.)
    /// </summary>
    [Required]
    public string ImportFormat { get; set; } = "CSV";

    /// <summary>
    /// Gets or sets the prompt template ID this collection is associated with
    /// </summary>
    [Required]
    public Guid PromptTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the lab ID for multi-tenancy
    /// </summary>
    [Required]
    public Guid LabId { get; set; }

    /// <summary>
    /// Gets or sets import options for parsing and validation
    /// </summary>
    public ImportOptions Options { get; set; } = new();

    /// <summary>
    /// Gets or sets whether to overwrite existing collections with the same name
    /// </summary>
    public bool OverwriteExisting { get; set; } = false;

    /// <summary>
    /// Gets or sets tags to apply to the imported collection
    /// </summary>
    public List<string> Tags { get; set; } = new();
}
