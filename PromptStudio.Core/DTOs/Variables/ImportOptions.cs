namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Configuration options for variable data import operations controlling metadata, validation, and safety behaviors.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by variable import services to configure import behavior, data validation, metadata assignment, and
/// safety mechanisms for variable collection creation workflows. Enables comprehensive control over import
/// operations with data quality assurance and operational safety features for production environments.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains collection metadata settings, data validation configurations, safety options, and format-specific
/// parsing parameters. Supports both simple data imports and complex enterprise workflows with comprehensive
/// configuration flexibility for diverse organizational requirements and data governance policies.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>Variable Import Service - Primary configuration for data import orchestration and behavior control</item>
///   <item>Collection Management Service - Metadata assignment and organizational structure creation</item>
///   <item>Data Validation Service - Quality assurance and validation rule enforcement</item>
///   <item>Backup Service - Safety mechanism configuration and data protection workflows</item>
///   <item>Tag Management Service - Categorization and discovery metadata assignment</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Collection metadata enables proper organization and discovery of imported data</item>
///   <item>Validation options ensure data quality and consistency across import operations</item>
///   <item>Safety features provide data protection and recovery capabilities</item>
///   <item>Format options enable flexible data source integration and processing</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Production imports with validation, backup, and comprehensive metadata</item>
///   <item>Development imports with relaxed validation for experimental workflows</item>
///   <item>Bulk data migration with specialized CSV options and safety features</item>
///   <item>Automated imports with predefined metadata and validation standards</item>
///   <item>User-driven imports with customizable options and interactive configuration</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Data validation adds processing time but ensures quality and consistency</item>
///   <item>Backup creation increases operation time but provides critical safety</item>
///   <item>Complex CSV options may impact parsing performance for large datasets</item>
///   <item>Metadata processing is typically lightweight with minimal performance impact</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for production variable import
/// var options = new ImportOptions {
///     CollectionName = "ProductionVariables",
///     Description = "Q4 campaign variable set",
///     Tags = "campaign,q4,production",
///     ValidateData = true,
///     CreateBackup = true,
///     CsvOptions = new CsvParsingOptions { HasHeaders = true, MaxRows = 50000 }
/// };
/// var result = await importService.ImportVariablesAsync(data, options);
/// </code>
/// </example>
public class ImportOptions
{
    /// <summary>
    /// Name for the variable collection to be created from the imported data.
    /// Provides organizational context and enables collection identification and management.
    /// Should follow naming conventions and be unique within the target scope.
    /// Critical for collection organization, discovery, and reference workflows.
    /// </summary>
    public string CollectionName { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional descriptive text providing context and purpose information for the imported variable collection.
    /// Helps users understand the collection's intended use, data source, and relevance.
    /// Useful for team collaboration, documentation, and collection management workflows.
    /// Supports rich descriptions for enhanced user experience and data governance.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Optional comma-separated tags for categorization, discovery, and organization of the variable collection.
    /// Enables flexible taxonomy and search capabilities for large-scale variable management.
    /// Supports filtering, grouping, and automated organization workflows.
    /// Common tags include environment, purpose, team, or data source identifiers.
    /// </summary>
    public string? Tags { get; set; }
    
    /// <summary>
    /// Optional CSV-specific parsing configuration for fine-tuned data import behavior.
    /// Provides detailed control over CSV format interpretation and data processing.
    /// Null value uses default CSV parsing settings for standard format files.
    /// Critical for handling diverse CSV dialects and specialized data formats.
    /// </summary>
    public CsvParsingOptions? CsvOptions { get; set; }
    
    /// <summary>
    /// Determines whether imported data undergoes validation before collection creation.
    /// When true, enforces data quality standards and prevents invalid data import.
    /// When false, allows faster imports but may introduce data quality issues.
    /// Recommended true for production environments and critical data workflows.
    /// </summary>
    public bool ValidateData { get; set; } = true;
    
    /// <summary>
    /// Controls whether a backup is created before performing the import operation.
    /// When true, enables recovery and rollback capabilities for import operations.
    /// When false, improves performance but eliminates recovery options for failed imports.
    /// Strongly recommended true for production environments and valuable data.
    /// </summary>
    public bool CreateBackup { get; set; } = true;
}
