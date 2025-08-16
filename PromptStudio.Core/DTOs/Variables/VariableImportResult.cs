using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Comprehensive result data transfer object for variable collection import operations with detailed outcome analysis and statistics.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by variable import services to return structured results from data import operations including created collections,
/// error diagnostics, import statistics, and quality metrics. Enables systematic import validation, success analysis,
/// and error handling for variable data integration workflows in template management systems.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains imported collection references, success indicators, error and warning diagnostics, and comprehensive
/// import statistics. Supports both successful imports with detailed metrics and failed imports with actionable
/// error information for troubleshooting and import optimization workflows.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>Variable Import Service - Primary result container for import operations and success validation</item>
///   <item>Collection Management Service - New collection integration and lifecycle management</item>
///   <item>Error Handling Service - Import error analysis and user feedback generation</item>
///   <item>Analytics Service - Import performance tracking and success rate monitoring</item>
///   <item>Audit Service - Import activity logging and compliance documentation</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Collection reference provides immediate access to imported data for further processing</item>
///   <item>Success indicator enables clear go/no-go decision making in workflows</item>
///   <item>Error and warning collections support comprehensive quality assessment</item>
///   <item>Statistical data enables import performance analysis and optimization</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Variable data import workflows with success validation and error handling</item>
///   <item>Import quality assessment and improvement based on statistics and diagnostics</item>
///   <item>Collection lifecycle management and post-import processing workflows</item>
///   <item>Error-driven import process improvement and user guidance</item>
///   <item>Import audit trails and compliance documentation for governance</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Collection object may contain substantial data for large imports</item>
///   <item>Error collection should be bounded to prevent memory issues</item>
///   <item>Statistics calculation adds minimal overhead but provides valuable insights</item>
///   <item>Success validation may involve additional processing for quality assurance</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for variable import with validation
/// var result = await importService.ImportAsync(data, options);
/// if (!result.Success) {
///     await errorService.LogImportFailureAsync(result.Errors);
///     return BadRequest("Import failed");
/// }
/// var successRate = (double)result.ImportedRows / (result.ImportedRows + result.SkippedRows);
/// await collectionService.ActivateCollectionAsync(result.Collection.Id);
/// </code>
/// </example>
public class VariableImportResult
{
    /// <summary>
    /// Successfully created variable collection containing the imported data and metadata.
    /// Provides immediate access to the imported collection for further processing and integration.
    /// Null when import failed or no collection was created due to validation errors.
    /// Contains complete domain object ready for use in template execution workflows.
    /// </summary>
    public VariableCollection? Collection { get; set; }
    
    /// <summary>
    /// Indicates whether the variable import operation completed successfully without critical errors.
    /// True when collection was created and data was imported according to specified quality standards.
    /// False when critical errors prevented successful import completion or collection creation.
    /// Service layers should check this before processing the imported collection.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Collection of critical error messages that prevented successful import completion.
    /// Contains actionable error information for troubleshooting data format issues and import failures.
    /// Includes validation errors, format problems, and system-level issues.
    /// Service layers should log these errors and provide appropriate user feedback for resolution.
    /// </summary>
    public List<string> Errors { get; set; } = [];
    
    /// <summary>
    /// Collection of warning messages for non-critical issues encountered during import operations.
    /// Contains information about data quality concerns, skipped records, and optimization opportunities.
    /// Does not prevent successful import but may indicate potential processing issues or improvements.
    /// Useful for import quality assessment and user guidance for optimal data preparation.
    /// </summary>
    public List<string> Warnings { get; set; } = [];
    
    /// <summary>
    /// Number of data rows successfully imported and included in the variable collection.
    /// Represents the usable data output from the import operation.
    /// Used for success rate calculation and import completeness assessment.
    /// Critical metric for understanding the effective scope of the import operation.
    /// </summary>
    public int ImportedRows { get; set; }
    
    /// <summary>
    /// Number of data rows that were skipped during import due to validation failures or format issues.
    /// Indicates data quality problems or format inconsistencies in the source data.
    /// Used with ImportedRows for comprehensive import analysis and quality assessment.
    /// High skip rates may indicate need for data preparation or format improvements.
    /// </summary>
    public int SkippedRows { get; set; }
    
    /// <summary>
    /// Additional import statistics and performance metrics for operation analysis and optimization.
    /// Contains operation-specific metrics like processing time, memory usage, and data characteristics.
    /// Supports custom statistics collection for specialized import scenarios and performance monitoring.
    /// Used by analytics services for import performance analysis and system optimization insights.
    /// </summary>
    public Dictionary<string, object> ImportStatistics { get; set; } = [];
}
