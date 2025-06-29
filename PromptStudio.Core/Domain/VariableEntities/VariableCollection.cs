using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Collection of variable sets for batch testing and execution
/// Enhanced with enterprise features and Guid-based identification
/// </summary>
public class VariableCollection : AuditableEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    // Foreign key - updated to Guid
    public Guid PromptTemplateId { get; set; }
    public PromptTemplate PromptTemplate { get; set; } = null!;
    
    /// <summary>
    /// JSON array of variable sets, each containing key-value pairs for variables
    /// Example: [{"name": "John", "role": "Developer"}, {"name": "Jane", "role": "Designer"}]
    /// </summary>
    public string VariableSets { get; set; } = "[]";
    
    /// <summary>
    /// Total number of variable sets in this collection
    /// </summary>
    public int VariableSetCount { get; set; } = 0;
    
    /// <summary>
    /// Source of this collection (manual, csv_import, api, generated)
    /// </summary>
    [StringLength(50)]
    public string Source { get; set; } = "manual";
    
    /// <summary>
    /// Original CSV data if imported from CSV
    /// </summary>
    public string? OriginalCsvData { get; set; }
    
    /// <summary>
    /// Tags for categorization and search (JSON array)
    /// </summary>
    [StringLength(500)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Collection status for lifecycle management
    /// </summary>
    public CollectionStatus Status { get; set; } = CollectionStatus.Active;
    
    /// <summary>
    /// Whether this collection is archived
    /// </summary>
    public bool IsArchived { get; set; } = false;
    
    /// <summary>
    /// Last time this collection was used for execution
    /// </summary>
    public DateTime? LastUsedAt { get; set; }
    
    /// <summary>
    /// Number of times this collection has been used
    /// </summary>
    public long UsageCount { get; set; } = 0;
    
    // Navigation properties
    public virtual ICollection<PromptExecution> Executions { get; set; } = new List<PromptExecution>();
}
