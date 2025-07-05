namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Configuration options for library import operations in the Prompt Studio ecosystem.
/// Defines comprehensive parameters to control the behavior and scope of library imports,
/// supporting various import scenarios from basic template transfers to complex migrations
/// with permission preservation and validation requirements.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <list type="bullet">
///   <item>Library Management Service - Import configuration and validation</item>
///   <item>Template Service - Template overwrite and conflict resolution</item>
///   <item>Permission Service - Permission inheritance and mapping</item>
///   <item>Backup Service - Automated backup creation and restoration</item>
///   <item>Validation Service - Pre-import validation and compliance checks</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>JSON serializable for API transport and configuration storage</item>
///   <item>Supports nested dictionary structures for custom metadata and options</item>
///   <item>Boolean flags enable/disable specific import behaviors</item>
///   <item>Compatible with batch import operations and automation workflows</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Library migration between environments (dev/staging/prod)</item>
///   <item>Template synchronization across teams and organizations</item>
///   <item>Backup restoration with selective feature import</item>
///   <item>Integration with CI/CD pipelines for automated deployments</item>
///   <item>Cross-tenant library sharing with permission mapping</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Validation adds processing overhead - disable for trusted sources</item>
///   <item>Backup creation impacts import duration - configure based on risk tolerance</item>
///   <item>Custom metadata increases payload size - optimize for network efficiency</item>
///   <item>Permission import requires additional authorization checks</item>
/// </list>
/// </remarks>
public class LibraryImportOptions
{
    /// <summary>
    /// Determines whether existing templates with matching names should be overwritten during import.
    /// When true, conflicting templates are replaced with imported versions and original data is lost.
    /// When false, conflicting templates are skipped and import continues with remaining items.
    /// Critical for maintaining data integrity during library synchronization operations.
    /// </summary>
    public bool OverwriteExisting { get; set; } = false;

    /// <summary>
    /// Controls whether permission settings are imported along with library content.
    /// When true, preserves original access control and user assignments from source library.
    /// When false, imported content inherits default permissions from target environment.
    /// Essential for maintaining security boundaries during cross-environment migrations.
    /// </summary>
    public bool ImportPermissions { get; set; } = true;

    /// <summary>
    /// Specifies whether execution history and analytics data should be included in the import.
    /// When true, preserves usage patterns, performance metrics, and historical execution data.
    /// When false, imported content starts with clean execution history in target environment.
    /// Important for compliance requirements and performance analysis continuity.
    /// </summary>
    public bool ImportExecutionHistory { get; set; } = false;

    /// <summary>
    /// Enables pre-import validation to verify content integrity and compatibility.
    /// When true, performs schema validation, dependency checks, and compatibility analysis.
    /// When false, skips validation for faster import but may result in runtime errors.
    /// Recommended for production imports to prevent system instability.
    /// </summary>
    public bool ValidateBeforeImport { get; set; } = true;

    /// <summary>
    /// Triggers automatic backup creation before performing the import operation.
    /// When true, creates point-in-time backup for rollback and recovery purposes.
    /// When false, skips backup creation for faster import but limits recovery options.
    /// Critical for production environments and irreversible import operations.
    /// </summary>
    public bool CreateBackup { get; set; } = true;

    /// <summary>
    /// Additional metadata to associate with the imported library content.
    /// Supports custom tagging, categorization, and environment-specific annotations.
    /// Commonly used for import tracking, source attribution, and operational metadata.
    /// Key-value pairs are preserved and accessible through library management APIs.
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }

    /// <summary>
    /// Extended configuration options for specialized import scenarios and integrations.
    /// Provides extensibility for custom import behaviors and third-party integrations.
    /// Values are passed to import service extensions and custom processing handlers.
    /// Enables advanced import workflows without modifying core import logic.
    /// </summary>
    public Dictionary<string, object> CustomOptions { get; set; } = new();
}
