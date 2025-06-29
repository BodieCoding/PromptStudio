namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the type classification for library categories, enabling administrative organization and filtering.
/// 
/// <para><strong>Business Context:</strong></para>
/// LibraryCategoryType provides meta-categorization for organizing categories themselves,
/// supporting different management policies and user interface organization
/// based on the purpose and scope of each category type.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Used for category filtering, administrative workflows, and UI organization.
/// Enables different treatment of system vs. user categories and functional vs. organizational groupings.
/// </summary>
/// <remarks>
/// <para><strong>Usage Guidelines:</strong></para>
/// - Functional: Categories based on business functions or use cases
/// - Departmental: Categories aligned with organizational departments
/// - ProjectBased: Categories for specific projects or initiatives  
/// - IndustrySpecific: Categories for industry-specific use cases
/// - OrganizationSpecific: Custom categories unique to the organization
/// - General: Catch-all for uncategorized or general-purpose categories
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Stored as string in database for maintainability and readability
/// - Used for category filtering and administrative grouping
/// - Should be indexed for efficient category management queries
/// </remarks>
/// <example>
/// <code>
/// // Functional category for business processes
/// var supportCategory = new LibraryCategory
/// {
///     Name = "Customer Support",
///     LibraryCategoryType = LibraryCategoryType.Functional,
///     IsSystemDefined = true
/// };
/// 
/// // Departmental category for organizational alignment
/// var marketingCategory = new LibraryCategory  
/// {
///     Name = "Marketing Team",
///     LibraryCategoryType = LibraryCategoryType.Departmental,
///     IsSystemDefined = false
/// };
/// 
/// // Organization-specific custom category
/// var customCategory = new LibraryCategory
/// {
///     Name = "ACME Corp Procedures", 
///     LibraryCategoryType = LibraryCategoryType.OrganizationSpecific,
///     IsSystemDefined = false
/// };
/// </code>
/// </example>
public enum LibraryCategoryType
{
    /// <summary>
    /// General or uncategorized category type.
    /// Used as default for categories that don't fit other specific types.
    /// </summary>
    General = 0,
    
    /// <summary>
    /// Categories organized by business function or use case.
    /// Examples: Customer Support, Marketing, Sales, Development, Quality Assurance
    /// </summary>
    Functional = 1,
    
    /// <summary>
    /// Categories aligned with organizational departments.
    /// Examples: Engineering Department, Marketing Department, Legal Department
    /// </summary>
    Departmental = 2,
    
    /// <summary>
    /// Categories for specific projects or initiatives.
    /// Examples: Project Alpha, Q4 Campaign, Digital Transformation Initiative
    /// </summary>
    ProjectBased = 3,
    
    /// <summary>
    /// Categories specific to particular industries.
    /// Examples: Healthcare, Financial Services, Retail, Manufacturing
    /// </summary>
    IndustrySpecific = 4,
    
    /// <summary>
    /// Custom categories unique to the specific organization.
    /// Examples: Company-specific processes, proprietary methodologies, internal standards
    /// </summary>
    OrganizationSpecific = 5,
    
    /// <summary>
    /// Categories for compliance and regulatory requirements.
    /// Examples: GDPR Compliance, SOX Requirements, Industry Regulations
    /// </summary>
    Compliance = 6,
    
    /// <summary>
    /// Categories for technical integrations and system connections.
    /// Examples: API Integration, Data Pipeline, System Migration
    /// </summary>
    Integration = 7
}
