using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Comprehensive result data transfer object encapsulating the complete outcome of library import operations.
/// Provides detailed success/failure information, imported content references, error diagnostics,
/// and statistical metrics for service layer processing and client feedback.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <list type="bullet">
///   <item>Library Management Service - Import outcome processing and state management</item>
///   <item>Analytics Service - Import statistics collection and performance tracking</item>
///   <item>Notification Service - Success/failure alerts and error reporting</item>
///   <item>Audit Service - Import activity logging and compliance tracking</item>
///   <item>Error Handling Service - Failure analysis and troubleshooting support</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>JSON serializable for API responses and message queue processing</item>
///   <item>Contains references to imported domain entities for immediate use</item>
///   <item>Structured error and warning collections for systematic error handling</item>
///   <item>Statistical data enables performance monitoring and capacity planning</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Batch import operations with partial success handling</item>
///   <item>Migration status reporting and progress tracking</item>
///   <item>Error analysis and retry logic implementation</item>
///   <item>Import audit trails and compliance documentation</item>
///   <item>Performance optimization based on import statistics</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>ImportedLibraries collection size impacts memory usage</item>
///   <item>Error/Warning collections should be bounded for large imports</item>
///   <item>Statistics dictionary enables custom metric collection</item>
///   <item>Consider pagination for very large import results</item>
/// </list>
/// </remarks>
public class LibraryImportResult
{
    /// <summary>
    /// Indicates the overall success status of the library import operation.
    /// True when all import tasks completed successfully without critical errors.
    /// False when critical errors prevented successful import completion.
    /// Service layers should check this before processing imported content.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Primary imported library entity for single library import operations.
    /// Populated only when importing a single library and Success is true.
    /// Null for batch imports or failed operations - use ImportedLibraries collection instead.
    /// Provides immediate access to imported library for downstream processing.
    /// </summary>
    public PromptLibrary? Library { get; set; }

    /// <summary>
    /// Collection of all successfully imported library entities from the operation.
    /// Contains complete domain objects ready for immediate use by service layers.
    /// Empty collection indicates no libraries were successfully imported.
    /// Used for batch import operations and multi-library processing scenarios.
    /// </summary>
    public List<PromptLibrary> ImportedLibraries { get; set; } = [];

    /// <summary>
    /// Collection of critical error messages that prevented successful import completion.
    /// Contains actionable error information for troubleshooting and user feedback.
    /// Includes validation failures, permission errors, and system-level issues.
    /// Service layers should log these errors and provide appropriate user notifications.
    /// </summary>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// Collection of warning messages for non-critical issues encountered during import.
    /// Contains information about skipped items, partial failures, or compatibility issues.
    /// Does not prevent successful import but may indicate data loss or limitations.
    /// Useful for audit trails and informing users about potential import impacts.
    /// </summary>
    public List<string> Warnings { get; set; } = [];

    /// <summary>
    /// Total count of individual templates successfully imported across all libraries.
    /// Provides granular success metrics for import performance analysis.
    /// Used for progress reporting, billing calculations, and capacity planning.
    /// Does not include skipped or failed template imports.
    /// </summary>
    public int ImportedTemplates { get; set; }

    /// <summary>
    /// Total count of templates that were skipped during the import process.
    /// Includes templates skipped due to conflicts, validation failures, or user preferences.
    /// Combined with ImportedTemplates provides complete import coverage analysis.
    /// Important for understanding import completeness and potential data gaps.
    /// </summary>
    public int SkippedTemplates { get; set; }

    /// <summary>
    /// Extensible collection of detailed import statistics and performance metrics.
    /// Contains operation-specific metrics like processing time, memory usage, and throughput.
    /// Supports custom statistics collection for specialized import scenarios.
    /// Used by analytics services for performance monitoring and optimization insights.
    /// </summary>
    public Dictionary<string, object> ImportStatistics { get; set; } = [];
}
