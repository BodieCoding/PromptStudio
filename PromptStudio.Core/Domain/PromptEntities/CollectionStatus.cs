namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the operational lifecycle states of variable collections within template management workflows.
/// 
/// <para><strong>Business Context:</strong></para>
/// Collection status enables systematic management of variable datasets used for template testing,
/// validation, and production execution. Status control supports development workflows, quality assurance
/// processes, and production deployment strategies while maintaining data integrity and governance compliance.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Collection status integrates with template execution engines, testing frameworks, and deployment
/// pipelines to control variable dataset availability and usage. Each status has specific operational
/// capabilities that support development, testing, and production workflows.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Systematic variable dataset lifecycle management
/// - Quality assurance through controlled collection progression
/// - Production deployment safety with staged collection availability
/// - Data integrity preservation through status-based access control
/// </summary>
/// <remarks>
/// <para><strong>Status Progression:</strong></para>
/// Typical flow: Draft → Active (for production use) → Deprecated → Archived
/// Collections may cycle between Draft and Active during iterative development.
/// 
/// <para><strong>Operational Capabilities:</strong></para>
/// - Draft: Development and testing, unrestricted modification
/// - Active: Production execution, restricted modification for stability
/// - Deprecated: Legacy support with migration planning
/// - Archived: Historical preservation with compliance focus
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Template execution systems for dataset availability
/// - Testing frameworks for validation dataset selection
/// - Deployment pipelines for production dataset promotion
/// - Quality assurance processes for collection validation
/// </remarks>
/// <example>
/// A variable collection for customer data moves from Draft (development) to Active (production)
/// once validation and testing confirm data quality and compliance.
/// </example>
public enum CollectionStatus
{
    /// <summary>
    /// Production-ready status indicating the collection is validated and available for operational use.
    /// Collections have passed quality assurance processes and are approved for template execution
    /// in production environments with appropriate data governance controls.
    /// </summary>
    Active = 0,
    
    /// <summary>
    /// Development status for collections under construction, testing, or validation.
    /// Collections can be freely modified and are available for development and testing
    /// workflows but not approved for production execution.
    /// </summary>
    Draft = 1,
    
    /// <summary>
    /// End-of-life status preserving collections for historical reference and compliance.
    /// Collections are no longer operational but maintained for audit trails, compliance
    /// requirements, and historical analysis with restricted access controls.
    /// </summary>
    Archived = 2,
    
    /// <summary>
    /// Phased retirement status indicating collections are superseded but maintained for transition.
    /// Supports legacy template execution and migration planning while encouraging adoption
    /// of newer, improved collection alternatives.
    /// </summary>
    Deprecated = 3
}
