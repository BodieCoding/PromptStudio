using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents an execution of a prompt template with specific variable values
/// Enhanced with enterprise features, analytics, and performance tracking
/// </summary>
public class PromptExecution : AuditableEntity
{
    // Foreign key - updated to Guid
    public Guid PromptTemplateId { get; set; }
    
    /// <summary>
    /// The final prompt text after variable substitution
    /// </summary>
    [Required]
    public string ResolvedPrompt { get; set; } = string.Empty;
    
    /// <summary>
    /// JSON string containing the variable values used in this execution
    /// </summary>
    public string? VariableValues { get; set; }
    
    /// <summary>
    /// The AI provider used (OpenAI, Claude, etc.)
    /// </summary>
    [StringLength(50)]
    public string? AiProvider { get; set; }
    
    /// <summary>
    /// The model used (gpt-4, claude-3, etc.)
    /// </summary>
    [StringLength(50)]
    public string? Model { get; set; }
    
    /// <summary>
    /// The response from the AI provider
    /// </summary>
    public string? Response { get; set; }
    
    /// <summary>
    /// Time taken for the AI request in milliseconds
    /// </summary>
    public int? ResponseTimeMs { get; set; }
    
    /// <summary>
    /// Tokens used in the request (if available)
    /// </summary>
    public int? TokensUsed { get; set; }
    
    /// <summary>
    /// Cost of the request (if available)
    /// </summary>
    public decimal? Cost { get; set; }
    
    /// <summary>
    /// When this execution was performed
    /// </summary>
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Execution status
    /// </summary>
    public ExecutionStatus Status { get; set; } = ExecutionStatus.Success;
    
    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Quality score for this execution (0.0 - 1.0)
    /// </summary>
    public decimal? QualityScore { get; set; }
    
    /// <summary>
    /// User or system that initiated this execution
    /// </summary>
    [StringLength(100)]
    public string? ExecutedBy { get; set; }
    
    /// <summary>
    /// Execution context (web, api, batch, test, etc.)
    /// </summary>
    [StringLength(50)]
    public string? ExecutionContext { get; set; }
    
    /// <summary>
    /// Variable collection used for this execution (if applicable)
    /// </summary>
    public Guid? VariableCollectionId { get; set; }
    
    // Navigation properties
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
    public virtual VariableCollection? VariableCollection { get; set; }
}
