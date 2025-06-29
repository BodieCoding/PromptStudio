namespace PromptStudio.Core.Domain;

/// <summary>
/// Predefined workflow category types for system-defined categories and seeding.
/// 
/// <para><strong>Business Context:</strong></para>
/// WorkflowCategoryType provides standardized category types for workflow organization,
/// ensuring consistency across deployments while supporting common enterprise
/// workflow patterns and business process categories.
/// 
/// <para><strong>Technical Context:</strong></para>
/// This enum is used primarily for seeding system-defined WorkflowCategory entities
/// and providing consistent category types across different tenant environments.
/// It supports migration from the legacy enum-based system to the flexible entity-based approach.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Standardized workflow category types across enterprise deployments
/// - Consistent seeding and migration support
/// - Common workflow patterns and business process alignment
/// - Legacy system compatibility during migration
/// </summary>
/// <remarks>
/// <para><strong>Usage Patterns:</strong></para>
/// - Used for seeding system-defined WorkflowCategory entities
/// - Migration support from legacy enum-based categorization
/// - Administrative interfaces for category type selection
/// - Reporting and analytics category grouping
/// 
/// <para><strong>Migration Strategy:</strong></para>
/// These values correspond to the original WorkflowCategory enum values,
/// enabling smooth migration to the flexible entity-based category system
/// while preserving existing categorization schemes and user familiarity.
/// </remarks>
/// <example>
/// <code>
/// // Used for seeding system categories
/// var systemCategories = new[]
/// {
///     new WorkflowCategory { Name = "Customer Service", CategoryType = CategoryType.Functional, IsSystemDefined = true },
///     new WorkflowCategory { Name = "Data Processing", CategoryType = CategoryType.Technical, IsSystemDefined = true },
///     new WorkflowCategory { Name = "Content Generation", CategoryType = CategoryType.Functional, IsSystemDefined = true }
/// };
/// </code>
/// </example>
public enum WorkflowCategoryType
{
    /// <summary>
    /// General-purpose workflows not fitting specific categories
    /// </summary>
    General = 0,
    
    /// <summary>
    /// Data processing, transformation, and analysis workflows
    /// </summary>
    DataProcessing = 1,
    
    /// <summary>
    /// Content creation, generation, and publishing workflows
    /// </summary>
    ContentGeneration = 2,
    
    /// <summary>
    /// Data analysis, reporting, and insight generation workflows
    /// </summary>
    Analysis = 3,
    
    /// <summary>
    /// Customer service, support, and interaction workflows
    /// </summary>
    CustomerService = 4,
    
    /// <summary>
    /// Marketing, campaigns, and promotional workflows
    /// </summary>
    Marketing = 5,
    
    /// <summary>
    /// Software development, coding, and technical workflows
    /// </summary>
    Development = 6,
    
    /// <summary>
    /// Quality assurance, testing, and validation workflows
    /// </summary>
    QualityAssurance = 7,
    
    /// <summary>
    /// Research, investigation, and discovery workflows
    /// </summary>
    Research = 8,
    
    /// <summary>
    /// Operational processes and business workflows
    /// </summary>
    Operations = 9,
    
    /// <summary>
    /// Training, education, and knowledge transfer workflows
    /// </summary>
    Training = 10,
    
    /// <summary>
    /// System integration and data synchronization workflows
    /// </summary>
    Integration = 11,
    
    /// <summary>
    /// A/B testing and experimentation workflows
    /// </summary>
    Experimentation = 12,
    
    /// <summary>
    /// Compliance, regulatory, and governance workflows
    /// </summary>
    Compliance = 13,
    
    /// <summary>
    /// Security, access control, and protection workflows
    /// </summary>
    Security = 14,
    
    /// <summary>
    /// Financial processes and accounting workflows
    /// </summary>
    Finance = 15,
    
    /// <summary>
    /// Human resources and personnel management workflows
    /// </summary>
    HumanResources = 16,
    
    /// <summary>
    /// Reporting, dashboard, and business intelligence workflows
    /// </summary>
    Reporting = 17,
    
    /// <summary>
    /// Automation, orchestration, and process workflows
    /// </summary>
    Automation = 18,
    
    /// <summary>
    /// Testing, validation, and verification workflows
    /// </summary>
    Testing = 19,
    
    /// <summary>
    /// Monitoring, alerting, and observability workflows
    /// </summary>
    Monitoring = 20
}
