namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Comprehensive result data transfer object for CSV parsing operations with detailed outcome analysis and data quality metrics.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by CSV parsing services to return structured results from data import operations including parsed variable sets,
/// error diagnostics, data quality analysis, and parsing statistics. Enables systematic data validation, quality
/// assessment, and error handling for variable data ingestion workflows in template management systems.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains parsed variable data collections, error and warning diagnostics, type inference results, and comprehensive
/// parsing statistics. Supports both successful data imports with quality metrics and failed imports with detailed
/// error analysis for troubleshooting and data quality improvement workflows.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>CSV Import Service - Primary result container for parsing operations and data validation</item>
///   <item>Data Quality Service - Error analysis and data quality assessment workflows</item>
///   <item>Variable Management Service - Parsed data integration and variable set creation</item>
///   <item>Error Handling Service - Parsing error analysis and user feedback generation</item>
///   <item>Analytics Service - Import statistics tracking and data quality monitoring</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Variable sets collection provides immediate access to parsed data for processing</item>
///   <item>Error and warning collections enable comprehensive import quality assessment</item>
///   <item>Type inference results support data validation and processing optimization</item>
///   <item>Statistics enable import performance monitoring and data quality analysis</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Variable data import workflows with quality validation and error handling</item>
///   <item>Data quality assessment and improvement based on parsing diagnostics</item>
///   <item>Automated type inference for improved data processing and validation</item>
///   <item>Import statistics analysis for process optimization and monitoring</item>
///   <item>Error-driven data source improvement and format standardization</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Variable sets collection size impacts memory usage for large imports</item>
///   <item>Error collection should be bounded to prevent memory exhaustion</item>
///   <item>Type inference operations may be computationally intensive for large datasets</item>
///   <item>Statistics calculation adds processing overhead but provides valuable insights</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for CSV import with quality validation
/// var result = await csvService.ParseAsync(csvContent, options);
/// if (result.Errors.Any()) {
///     await errorService.LogImportErrorsAsync(result.Errors);
///     return BadRequest("CSV parsing failed");
/// }
/// var successRate = (double)result.ValidRows / result.TotalRows;
/// await variableService.ImportVariableSetsAsync(result.VariableSets);
/// </code>
/// </example>
public class CsvParsingResult
{
    /// <summary>
    /// Collection of successfully parsed variable sets representing individual data rows as name-value dictionaries.
    /// Each dictionary contains column names as keys and corresponding cell values as strings.
    /// Ready for immediate use in template execution and variable management workflows.
    /// Empty collection indicates parsing failure or no valid data rows in source.
    /// </summary>
    public List<Dictionary<string, string>> VariableSets { get; set; } = [];
    
    /// <summary>
    /// Collection of critical error messages encountered during CSV parsing operations.
    /// Contains actionable error information for troubleshooting data format issues and parsing failures.
    /// Includes format errors, type validation failures, and structural problems with source data.
    /// Service layers should handle these errors before proceeding with data processing.
    /// </summary>
    public List<string> Errors { get; set; } = [];
    
    /// <summary>
    /// Collection of warning messages for non-critical issues encountered during parsing operations.
    /// Contains information about data quality concerns, format inconsistencies, and optimization opportunities.
    /// Does not prevent successful parsing but may indicate potential data processing issues.
    /// Useful for data quality improvement and user guidance for optimal data formats.
    /// </summary>
    public List<string> Warnings { get; set; } = [];
    
    /// <summary>
    /// Dictionary mapping column names to their automatically inferred data types based on content analysis.
    /// Provides type recommendations for data validation and processing optimization.
    /// Keys represent column names, values specify the most appropriate .NET types for the data.
    /// Used for enhanced data validation and type-safe variable processing workflows.
    /// </summary>
    public Dictionary<string, Type> InferredTypes { get; set; } = [];
    
    /// <summary>
    /// Total number of data rows processed during the parsing operation including both valid and invalid rows.
    /// Provides comprehensive scope understanding and enables success rate calculation.
    /// Used for import completeness assessment and data quality analysis.
    /// Critical for understanding the full scope of the import operation.
    /// </summary>
    public int TotalRows { get; set; }
    
    /// <summary>
    /// Number of rows that passed validation and were successfully converted to variable sets.
    /// Indicates the count of usable data rows for template execution and processing.
    /// Used with TotalRows for success rate calculation and quality assessment.
    /// Represents the actual usable output from the parsing operation.
    /// </summary>
    public int ValidRows { get; set; }
    
    /// <summary>
    /// Additional parsing statistics and performance metrics for import analysis and optimization.
    /// Contains operation-specific metrics like processing time, memory usage, and data characteristics.
    /// Supports custom statistics collection for specialized parsing scenarios and performance monitoring.
    /// Used by analytics services for import performance analysis and optimization insights.
    /// </summary>
    public Dictionary<string, object> Statistics { get; set; } = [];
}
