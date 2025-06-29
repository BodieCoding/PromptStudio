using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a collection of variable sets designed for batch testing, execution, and data-driven prompt operations.
/// 
/// <para><strong>Business Context:</strong></para>
/// This entity serves as the foundation for enterprise-scale prompt testing and batch processing workflows.
/// It enables organizations to manage large datasets for A/B testing, quality assurance, performance benchmarking,
/// and automated prompt validation across diverse scenarios and use cases in production LLMOps environments.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity provides structured storage for variable datasets with support for multiple data sources,
/// lifecycle management, usage tracking, and performance optimization. It integrates with prompt templates
/// to enable systematic testing and validation of prompt variations across different input scenarios.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Systematic prompt testing and validation across multiple scenarios
/// - Enterprise-scale batch processing and data-driven prompt operations
/// - Comprehensive usage tracking and performance analytics
/// - Multi-source data integration with CSV import and API support
/// - Lifecycle management for test data organization and maintenance
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Data Container Pattern: Structured storage for variable datasets
/// - Batch Processing: Enables systematic execution across multiple data points
/// - Source Tracking: Maintains data lineage and import history
/// - Multi-tenancy: Inherits tenant isolation from AuditableEntity
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - VariableSets JSON field may require indexing for large collections
/// - Consider pagination for UI display of large variable collections
/// - Usage tracking enables performance optimization and resource planning
/// - Archive functionality helps manage storage costs for inactive collections
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Prompt Templates: Core data source for template variable validation
/// - Execution Engine: Batch processing input for systematic execution
/// - Testing Framework: Data foundation for A/B testing and quality assurance
/// - Import Services: CSV and API integration for external data sources
/// - Analytics: Usage patterns and performance tracking
/// </remarks>
/// <example>
/// <code>
/// // Creating a variable collection from CSV data
/// var collection = new VariableCollection
/// {
///     Name = "Customer Support Scenarios",
///     Description = "Test cases for customer support chatbot responses",
///     PromptTemplateId = templateId,
///     Source = "csv_import",
///     VariableSets = JsonSerializer.Serialize(new[]
///     {
///         new { customer_type = "premium", issue_severity = "high", response_tone = "urgent" },
///         new { customer_type = "standard", issue_severity = "medium", response_tone = "helpful" }
///     }),
///     VariableSetCount = 2,
///     Tags = JsonSerializer.Serialize(new[] { "testing", "customer-support", "production" }),
///     TenantId = currentTenantId
/// };
/// 
/// // Using collection for batch execution
/// var results = await executionService.ExecuteBatchAsync(collection.Id, templateId);
/// 
/// // Tracking usage
/// collection.UsageCount++;
/// collection.LastUsedAt = DateTime.UtcNow;
/// await collectionService.UpdateAsync(collection);
/// </code>
/// </example>
public class VariableCollection : AuditableEntity
{
    /// <summary>
    /// Gets or sets the human-readable name of the variable collection.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides clear identification and organization of variable collections for
    /// enterprise teams managing multiple test datasets and execution scenarios
    /// across different projects and use cases.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Used for display in user interfaces, reporting, and collection management.
    /// Should be descriptive and unique within the organization's naming conventions.
    /// </summary>
    /// <value>
    /// A descriptive name for the collection. Cannot be null or empty.
    /// Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Should follow organizational naming conventions for consistency.
    /// Used in user interfaces and reporting for collection identification.
    /// </remarks>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional description explaining the purpose and content of the variable collection.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides context and documentation for the collection's intended use,
    /// helping team members understand the dataset's purpose and appropriate
    /// usage scenarios in enterprise collaborative environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional field for additional context and documentation. Used for
    /// collection browsing and selection in user interfaces.
    /// </summary>
    /// <value>
    /// A descriptive explanation of the collection's purpose and content.
    /// Optional field with maximum length of 500 characters.
    /// </value>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the prompt template associated with this variable collection.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links the variable collection to its target prompt template, ensuring
    /// variable compatibility and enabling template-specific testing and
    /// validation workflows in enterprise prompt management systems.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with PromptTemplate entity. Variables in the
    /// collection should match the template's variable definitions.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the associated prompt template's unique identifier.
    /// </value>
    /// <remarks>
    /// Variables in the collection must be compatible with the template's variable schema.
    /// Used for validation and template-specific processing.
    /// </remarks>
    public Guid PromptTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the associated prompt template.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to template metadata and variable definitions for
    /// validation and compatibility checking without separate database queries.
    /// </summary>
    /// <value>
    /// A <see cref="PromptTemplate"/> instance representing the associated template.
    /// </value>
    public PromptTemplate PromptTemplate { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the variable datasets stored as a JSON array of objects.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Contains the actual test data used for systematic prompt execution and testing,
    /// enabling comprehensive validation across multiple scenarios and use cases
    /// in enterprise quality assurance and performance testing workflows.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON array where each element is an object containing key-value pairs
    /// matching the template's variable definitions. Validated against template schema.
    /// </summary>
    /// <value>
    /// A JSON string representing an array of variable objects.
    /// Default is "[]" (empty array).
    /// </value>
    /// <example>
    /// [{"name": "John", "role": "Developer", "experience": "5 years"}, 
    ///  {"name": "Jane", "role": "Designer", "experience": "3 years"}]
    /// </example>
    /// <remarks>
    /// Structure must be compatible with the associated template's variable definitions.
    /// Large datasets may require pagination for UI display and processing.
    /// </remarks>
    public string VariableSets { get; set; } = "[]";
    
    /// <summary>
    /// Gets or sets the total number of variable sets contained in this collection.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides immediate visibility into collection size for resource planning,
    /// performance estimation, and batch processing optimization in enterprise
    /// environments with large-scale testing requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Maintained automatically when variable sets are added or removed.
    /// Used for progress tracking and resource allocation during batch processing.
    /// </summary>
    /// <value>
    /// An integer representing the number of variable sets. Default is 0.
    /// </value>
    /// <remarks>
    /// Should be kept in sync with the actual count in VariableSets array.
    /// Used for performance estimation and resource planning.
    /// </remarks>
    public int VariableSetCount { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the source of this variable collection for data lineage tracking.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks data provenance for audit purposes, quality assurance, and
    /// troubleshooting, enabling enterprises to understand data origins and
    /// maintain data governance standards across their testing workflows.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Common values include "manual", "csv_import", "api", "generated".
    /// Used for data lineage reporting and source-specific processing logic.
    /// </summary>
    /// <value>
    /// A string indicating the data source. Default is "manual".
    /// Maximum length is 50 characters.
    /// </value>
    /// <example>
    /// Examples: "manual", "csv_import", "api_integration", "generated_synthetic", "database_export"
    /// </example>
    /// <remarks>
    /// Critical for data governance and audit trail requirements.
    /// Used to apply source-specific validation and processing rules.
    /// </remarks>
    [StringLength(50)]
    public string Source { get; set; } = "manual";
    
    /// <summary>
    /// Gets or sets the original CSV data if the collection was imported from a CSV file.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Preserves the original import data for audit purposes, re-import scenarios,
    /// and troubleshooting import issues in enterprise environments with
    /// strict data lineage and audit requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Contains the raw CSV content as imported. Used for re-processing,
    /// validation, and audit trail purposes.
    /// </summary>
    /// <value>
    /// A string containing the original CSV data, or null if not imported from CSV.
    /// </value>
    /// <remarks>
    /// May contain large amounts of data - consider compression for storage optimization.
    /// Essential for audit trails and re-import capabilities.
    /// </remarks>
    public string? OriginalCsvData { get; set; }
    
    /// <summary>
    /// Gets or sets the tags for categorization and search functionality.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables efficient organization and discovery of variable collections
    /// in enterprise environments with large numbers of test datasets across
    /// multiple projects, teams, and use cases.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Stored as JSON array of strings. Used for filtering, search,
    /// and collection organization in user interfaces.
    /// </summary>
    /// <value>
    /// A JSON array of tag strings, or null if no tags are assigned.
    /// Maximum length is 500 characters for the entire JSON structure.
    /// </value>
    /// <example>
    /// ["testing", "customer-support", "production", "high-priority"]
    /// </example>
    /// <remarks>
    /// Used for collection discovery and organization in user interfaces.
    /// Should follow organizational tagging conventions for consistency.
    /// </remarks>
    [StringLength(500)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Gets or sets the current lifecycle status of the variable collection.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables lifecycle management and governance of test data collections,
    /// ensuring appropriate usage controls and preventing accidental use of
    /// deprecated or draft datasets in production testing environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Controls collection availability and usage permissions. Used for
    /// workflow integration and access control decisions.
    /// </summary>
    /// <value>
    /// A <see cref="CollectionStatus"/> enum value indicating the current status.
    /// Default is Active.
    /// </value>
    /// <remarks>
    /// Used for access control and workflow integration decisions.
    /// Inactive collections should be excluded from execution workflows.
    /// </remarks>
    public CollectionStatus Status { get; set; } = CollectionStatus.Active;
    
    /// <summary>
    /// Gets or sets a value indicating whether this collection has been archived.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides soft deletion capability for historical preservation while
    /// removing collections from active use, supporting enterprise data
    /// retention policies and storage optimization strategies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Archived collections are excluded from normal operations but preserved
    /// for historical reference and audit purposes.
    /// </summary>
    /// <value>
    /// <c>true</c> if the collection is archived; otherwise, <c>false</c>.
    /// Default is <c>false</c>.
    /// </value>
    /// <remarks>
    /// Archived collections should be excluded from active workflows and UI listings.
    /// Supports data retention policies and storage optimization.
    /// </remarks>
    public bool IsArchived { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the timestamp of the most recent usage of this collection.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables usage pattern analysis and identification of unused collections
    /// for archival or cleanup, supporting enterprise data management and
    /// storage optimization initiatives.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Updated each time the collection is used for execution or testing.
    /// Used for usage analytics and cleanup decision making.
    /// </summary>
    /// <value>
    /// A nullable <see cref="DateTime"/> representing the last usage time in UTC,
    /// or null if the collection has never been used.
    /// </value>
    /// <remarks>
    /// Used for collection lifecycle management and cleanup decisions.
    /// Updated automatically during execution workflows.
    /// </remarks>
    public DateTime? LastUsedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the total number of times this collection has been used for execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides usage analytics for understanding collection popularity and
    /// value, enabling data-driven decisions about collection maintenance,
    /// optimization, and resource allocation in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Incremented each time the collection is used for batch execution
    /// or testing workflows. Used for analytics and reporting.
    /// </summary>
    /// <value>
    /// A long integer representing the total usage count. Default is 0.
    /// </value>
    /// <remarks>
    /// Used for popularity analysis and resource allocation decisions.
    /// High usage counts indicate valuable collections that warrant optimization.
    /// </remarks>
    public long UsageCount { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the collection of prompt executions that used this variable collection.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides complete execution history and enables analysis of collection
    /// performance across different execution scenarios and time periods
    /// for comprehensive quality and performance assessment.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Navigation property linking to all executions that used this collection.
    /// Used for execution history analysis and performance tracking.
    /// </summary>
    /// <value>
    /// A collection of <see cref="PromptExecution"/> entities representing execution history.
    /// </value>
    /// <remarks>
    /// Used for execution history analysis and performance tracking.
    /// Large collections may require pagination for performance optimization.
    /// </remarks>
    public virtual ICollection<PromptExecution> Executions { get; set; } = new List<PromptExecution>();
}
