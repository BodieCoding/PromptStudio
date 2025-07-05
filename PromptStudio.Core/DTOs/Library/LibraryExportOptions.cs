namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Represents configuration options for library export operations, providing fine-grained control over export content and behavior.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Configuration DTO used by library export services to customize export behavior and content inclusion.
/// Enables flexible export scenarios from lightweight content-only exports to comprehensive exports with full metadata and permissions.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Export configuration parameters with optional metadata support for customizable export operations.
/// Designed for reusable export configurations and integration with various export formats and destinations.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Library backup and archive operations</item>
/// <item>Content migration between environments</item>
/// <item>Selective data sharing and distribution</item>
/// <item>Integration with external systems and tools</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight configuration object with minimal overhead. Template inclusion significantly affects export size and processing time.
/// Custom metadata should be kept minimal to maintain efficient export operations and reasonable payload sizes.</para>
/// </remarks>
public class LibraryExportOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to include permission data in the export.
    /// </summary>
    /// <value>True to include user permissions and access controls; otherwise, false for content-only export.</value>
    public bool IncludePermissions { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to include template definitions in the export.
    /// </summary>
    /// <value>True to include all template content and configurations; otherwise, false for metadata-only export.</value>
    public bool IncludeTemplates { get; set; } = true;

    /// <summary>
    /// Gets or sets optional custom metadata to include in the export package.
    /// </summary>
    /// <value>A dictionary containing additional metadata for export customization, or null if not required.</value>
    public Dictionary<string, object>? CustomMetadata { get; set; }
}
