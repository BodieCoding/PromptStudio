using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Separates prompt content from metadata for performance optimization
/// Large content is stored separately to improve query performance for metadata operations
/// </summary>
public class PromptContent : AuditableEntity
{
    /// <summary>
    /// Reference to the prompt template this content belongs to
    /// </summary>
    public Guid PromptTemplateId { get; set; }
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
    
    /// <summary>
    /// The actual prompt content with variable placeholders
    /// </summary>
    [Required]
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Compressed version of content for very large prompts
    /// </summary>
    public byte[]? CompressedContent { get; set; }
    
    /// <summary>
    /// Whether the content is stored in compressed format
    /// </summary>
    public bool IsCompressed { get; set; } = false;
    
    /// <summary>
    /// Content size in bytes for resource management
    /// </summary>
    public long ContentSize { get; set; } = 0;
    
    /// <summary>
    /// MIME type of the content (text/plain, application/json, etc.)
    /// </summary>
    [StringLength(100)]
    public string ContentType { get; set; } = "text/plain";
    
    /// <summary>
    /// Content encoding (UTF-8, ASCII, etc.)
    /// </summary>
    [StringLength(20)]
    public string Encoding { get; set; } = "UTF-8";
    
    /// <summary>
    /// Content hash for integrity checking and deduplication
    /// </summary>
    [StringLength(64)]
    public string? ContentHash { get; set; }
    
    /// <summary>
    /// Language of the prompt content for localization
    /// </summary>
    [StringLength(10)]
    public string Language { get; set; } = "en";
}
