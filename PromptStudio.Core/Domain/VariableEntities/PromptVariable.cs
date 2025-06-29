using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Variables that can be substituted in prompt templates (like {{variable_name}})
/// Enhanced with enterprise features and Guid-based identification
/// </summary>
public class PromptVariable : AuditableEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? Description { get; set; }
    
    public string? DefaultValue { get; set; }
    
    [Required]
    public VariableType Type { get; set; } = VariableType.Text;
    
    // Foreign key - updated to Guid
    public Guid PromptTemplateId { get; set; }
    
    /// <summary>
    /// Whether this variable is required (must have a value)
    /// </summary>
    public bool IsRequired { get; set; } = true;
    
    /// <summary>
    /// Variable validation rules (JSON format)
    /// Examples: min/max length, regex patterns, allowed values
    /// </summary>
    [StringLength(1000)]
    public string? ValidationRules { get; set; }
    
    /// <summary>
    /// Display order within the template
    /// </summary>
    public int SortOrder { get; set; } = 0;
    
    /// <summary>
    /// Help text for users filling out this variable
    /// </summary>
    [StringLength(500)]
    public string? HelpText { get; set; }
    
    /// <summary>
    /// Example values for this variable (JSON array)
    /// </summary>
    [StringLength(1000)]
    public string? ExampleValues { get; set; }
    
    // Navigation properties
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
}
