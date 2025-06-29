using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the content storage entity for prompt templates, optimized for performance and scalability.
/// 
/// <para><strong>Business Context:</strong></para>
/// This entity implements a content separation pattern to optimize database performance by storing large prompt content
/// separately from frequently accessed metadata. This architecture enables efficient querying of template metadata
/// without loading large content payloads, supporting enterprise-scale prompt libraries with thousands of templates.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity supports content compression for large prompts, integrity verification through hashing,
/// and multi-language content management. It provides enterprise features including content encoding specification,
/// MIME type handling, and size tracking for resource management and billing purposes.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Performance optimization through content separation
/// - Scalable storage for large prompt libraries
/// - Content integrity and deduplication capabilities
/// - Multi-language and encoding support for global deployments
/// - Resource tracking for enterprise billing and monitoring
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Content Separation Pattern: Separates content from metadata for performance
/// - Compression Strategy: Automatic compression for large content
/// - Integrity Verification: SHA-256 hashing for content validation
/// - Multi-tenancy: Inherits tenant isolation from AuditableEntity
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Content is lazily loaded when accessing PromptTemplate.Content navigation
/// - Compression threshold typically set at 1KB to balance CPU vs storage
/// - Content hash enables efficient deduplication across tenants
/// - Size tracking enables resource quotas and billing metrics
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Template Management: Core content storage for PromptTemplate entities
/// - Version Control: Content changes tracked through audit trail
/// - Export/Import: Supports content serialization with metadata
/// - API Services: Enables streaming for large content downloads
/// </remarks>
/// <example>
/// <code>
/// // Creating a new prompt content with compression
/// var content = new PromptContent
/// {
///     PromptTemplateId = templateId,
///     Content = "Your large prompt content here...",
///     ContentType = "text/plain",
///     Language = "en-US",
///     TenantId = currentTenantId
/// };
/// 
/// // Content will be automatically compressed if size exceeds threshold
/// await contentService.CreateAsync(content);
/// 
/// // Retrieving content with integrity verification
/// var retrievedContent = await contentService.GetByTemplateIdAsync(templateId);
/// if (retrievedContent.ContentHash != ComputeHash(retrievedContent.Content))
/// {
///     throw new InvalidOperationException("Content integrity check failed");
/// }
/// </code>
/// </example>
public class PromptContent : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the prompt template that owns this content.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Establishes the relationship between content and its template metadata, enabling content separation
    /// for performance optimization while maintaining referential integrity.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with PromptTemplate entity. Required for all content records.
    /// Supports cascade operations for template deletion and content cleanup.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the unique identifier of the owning prompt template.
    /// </value>
    /// <remarks>
    /// This property is required and must reference an existing PromptTemplate.
    /// Content cannot exist without a parent template due to business logic constraints.
    /// </remarks>
    public Guid PromptTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the parent prompt template.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to template metadata without requiring separate queries,
    /// enabling efficient data access patterns for content operations.
    /// </summary>
    /// <value>
    /// A <see cref="PromptTemplate"/> instance representing the parent template.
    /// </value>
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the actual prompt content with variable placeholders and instructions.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Contains the core prompt text that will be processed by AI models, including variable
    /// placeholders for dynamic content injection and system instructions for model behavior.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Supports variable placeholders in {{variable_name}} format. Content is validated
    /// for proper syntax and variable references during save operations.
    /// </summary>
    /// <value>
    /// A string containing the prompt content with variable placeholders.
    /// Cannot be null or empty for active templates.
    /// </value>
    /// <remarks>
    /// Content is automatically compressed if size exceeds the configured threshold.
    /// Variable placeholders are validated against the template's variable definitions.
    /// </remarks>
    [Required]
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the compressed version of the content for large prompts.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables efficient storage of large prompt content by reducing storage costs
    /// and improving transfer performance for enterprise-scale deployments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Uses GZIP compression algorithm. Automatically populated when content
    /// exceeds the compression threshold (typically 1KB).
    /// </summary>
    /// <value>
    /// A byte array containing the GZIP-compressed content, or null if content is not compressed.
    /// </value>
    /// <remarks>
    /// When this property has a value, the Content property may be empty to save storage.
    /// Decompression is handled automatically by the content service layer.
    /// </remarks>
    public byte[]? CompressedContent { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the content is stored in compressed format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Indicates the storage format for efficient content retrieval and processing,
    /// enabling the system to handle both compressed and uncompressed content seamlessly.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// When true, content should be read from CompressedContent property.
    /// When false, content is available directly in the Content property.
    /// </summary>
    /// <value>
    /// <c>true</c> if content is compressed; otherwise, <c>false</c>. Default is <c>false</c>.
    /// </value>
    public bool IsCompressed { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the content size in bytes for resource management and billing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables resource quota enforcement, billing calculations, and storage optimization
    /// decisions in enterprise environments with usage-based pricing models.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Represents the uncompressed content size for consistent measurement.
    /// Updated automatically when content is modified.
    /// </summary>
    /// <value>
    /// The size of the uncompressed content in bytes. Default is 0.
    /// </value>
    /// <remarks>
    /// Used for storage quota enforcement and billing calculations.
    /// Size is measured against the uncompressed content for consistency.
    /// </remarks>
    public long ContentSize { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the MIME type of the content for proper handling and processing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables correct content interpretation and processing by different system components,
    /// supporting various content formats including plain text, JSON, and structured data.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Common values include "text/plain", "application/json", "text/markdown".
    /// Used by API responses and content processing pipelines.
    /// </summary>
    /// <value>
    /// A string representing the MIME type. Default is "text/plain".
    /// Maximum length is 100 characters.
    /// </value>
    [StringLength(100)]
    public string ContentType { get; set; } = "text/plain";
    
    /// <summary>
    /// Gets or sets the character encoding of the content for proper text processing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Ensures correct character handling for international content and prevents
    /// encoding-related corruption in multi-language enterprise deployments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Common values include "UTF-8", "ASCII", "UTF-16". Used by content
    /// processing services to ensure proper character interpretation.
    /// </summary>
    /// <value>
    /// A string representing the character encoding. Default is "UTF-8".
    /// Maximum length is 20 characters.
    /// </value>
    [StringLength(20)]
    public string Encoding { get; set; } = "UTF-8";
    
    /// <summary>
    /// Gets or sets the SHA-256 hash of the content for integrity verification and deduplication.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides content integrity verification and enables deduplication strategies
    /// to optimize storage costs in large-scale deployments with similar content.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// SHA-256 hash of the uncompressed content. Generated automatically when
    /// content is created or modified. Used for integrity checks and deduplication.
    /// </summary>
    /// <value>
    /// A 64-character hexadecimal string representing the SHA-256 hash,
    /// or null if hash has not been calculated.
    /// </value>
    /// <remarks>
    /// Hash is calculated against the uncompressed content for consistency.
    /// Used for detecting content changes and preventing data corruption.
    /// </remarks>
    [StringLength(64)]
    public string? ContentHash { get; set; }
    
    /// <summary>
    /// Gets or sets the language code of the prompt content for localization and processing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables language-specific content management and processing, supporting
    /// international deployments with localized prompt libraries and AI model selection.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Uses ISO 639-1 language codes (e.g., "en", "es", "fr") or extended
    /// codes for regional variants (e.g., "en-US", "en-GB").
    /// </summary>
    /// <value>
    /// A string representing the ISO language code. Default is "en".
    /// Maximum length is 10 characters to support extended language codes.
    /// </value>
    /// <remarks>
    /// Used for language-specific prompt processing and AI model selection.
    /// Supports both basic (en) and extended (en-US) language codes.
    /// </remarks>
    [StringLength(10)]
    public string Language { get; set; } = "en";
}
