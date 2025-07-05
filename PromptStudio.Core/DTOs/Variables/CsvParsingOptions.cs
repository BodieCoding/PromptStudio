namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Configuration options for CSV data parsing operations controlling format interpretation, data processing, and performance characteristics.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by CSV parsing services to configure data import behavior, format handling, and processing optimization
/// for variable data ingestion workflows. Enables flexible CSV format support, data validation, and performance
/// tuning for diverse data sources and import scenarios in template variable management systems.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains format specification parameters, data processing options, performance limits, and type interpretation
/// settings. Supports both simple CSV imports and complex data transformation scenarios with comprehensive
/// configuration flexibility for diverse data source integration requirements.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>CSV Import Service - Primary configuration for data parsing and format interpretation</item>
///   <item>Data Validation Service - Format compliance checking and data quality assessment</item>
///   <item>Type Conversion Service - Column type mapping and data transformation operations</item>
///   <item>Performance Service - Memory management and processing optimization configuration</item>
///   <item>Error Handling Service - Data validation and parsing error management</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Format options enable support for diverse CSV dialect variations</item>
///   <item>Processing flags provide data cleaning and validation capabilities</item>
///   <item>Performance limits prevent resource exhaustion during large data imports</item>
///   <item>Type mappings support proper data interpretation and validation</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Standard CSV imports with header detection and whitespace cleaning</item>
///   <item>Custom delimiter formats for specialized data sources and legacy systems</item>
///   <item>Large dataset imports with row limits and performance optimization</item>
///   <item>Typed data imports with column type specification and validation</item>
///   <item>Data quality workflows with empty row filtering and format validation</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Row limits prevent memory exhaustion during large file processing</item>
///   <item>Trimming operations add processing overhead but improve data quality</item>
///   <item>Type conversion validation impacts parsing performance significantly</item>
///   <item>Header detection requires additional file scanning for large datasets</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for robust CSV parsing
/// var options = new CsvParsingOptions {
///     Delimiter = ';',
///     HasHeaders = true,
///     TrimWhitespace = true,
///     MaxRows = 50000,
///     ColumnTypes = new Dictionary&lt;string, Type&gt; { {"price", typeof(decimal)}, {"date", typeof(DateTime)} }
/// };
/// var result = await csvService.ParseAsync(csvContent, options);
/// </code>
/// </example>
public class CsvParsingOptions
{
    /// <summary>
    /// Character used to separate fields within CSV records.
    /// Default comma (,) supports standard CSV format, but can be customized for alternative delimiters.
    /// Common alternatives include semicolon (;), tab (\t), and pipe (|) for specialized data formats.
    /// Critical for proper field separation and data interpretation across diverse CSV dialects.
    /// </summary>
    public char Delimiter { get; set; } = ',';
    
    /// <summary>
    /// Indicates whether the first row contains column headers rather than data.
    /// When true, first row is used for column naming and skipped during data processing.
    /// When false, all rows are treated as data and columns receive default numeric names.
    /// Essential for proper column identification and data mapping in variable import workflows.
    /// </summary>
    public bool HasHeaders { get; set; } = true;
    
    /// <summary>
    /// Determines whether leading and trailing whitespace is removed from field values.
    /// When true, improves data quality by cleaning common formatting inconsistencies.
    /// When false, preserves exact field content including whitespace for specialized scenarios.
    /// Recommended true for most imports to ensure clean variable data and reduce processing errors.
    /// </summary>
    public bool TrimWhitespace { get; set; } = true;
    
    /// <summary>
    /// Controls whether completely empty rows are excluded from processing and import.
    /// When true, rows with no data content are filtered out to improve data quality.
    /// When false, empty rows are preserved which may cause variable processing issues.
    /// Recommended true for most scenarios to prevent null variable sets and processing errors.
    /// </summary>
    public bool SkipEmptyRows { get; set; } = true;
    
    /// <summary>
    /// Maximum number of data rows to process during CSV import operations.
    /// Prevents memory exhaustion and processing timeouts during large file imports.
    /// Should be set based on system capacity and expected data volumes.
    /// Recommended values: 10,000-100,000 depending on system resources and data complexity.
    /// </summary>
    public int MaxRows { get; set; } = 10000;
    
    /// <summary>
    /// Dictionary mapping column names to their expected data types for validation and conversion.
    /// Enables proper data type interpretation and validation during import processing.
    /// Keys represent column names (from headers or default names), values specify target types.
    /// Used for data validation, type conversion, and error detection during variable import workflows.
    /// </summary>
    public Dictionary<string, Type> ColumnTypes { get; set; } = new();
}
