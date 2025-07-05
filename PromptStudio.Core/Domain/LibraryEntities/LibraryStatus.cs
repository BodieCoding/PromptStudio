namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the operational lifecycle states of prompt libraries within enterprise governance frameworks.
/// 
/// <para><strong>Business Context:</strong></para>
/// Library status controls operational availability, content modification capabilities, and lifecycle management
/// for collections of AI templates and workflows. Status-based governance ensures appropriate access controls,
/// compliance with organizational policies, and systematic progression through library development and retirement phases.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Status integrates with access control systems, content management workflows, and administrative interfaces
/// to enforce policy-based library management. Each status has specific operational capabilities and access
/// permissions that support enterprise governance requirements and collaborative development processes.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Systematic library lifecycle management and governance
/// - Policy-based access control and content protection
/// - Collaborative development workflow support
/// - Enterprise compliance and audit trail maintenance
/// </summary>
/// <remarks>
/// <para><strong>Status Progression:</strong></para>
/// Typical flow: Active → ReadOnly (for stability) → Archived (for retirement)
/// Alternative: Active → UnderReview (for compliance) → Active/Deprecated
/// 
/// <para><strong>Operational Implications:</strong></para>
/// - Active: Full read/write operations, active development and deployment
/// - ReadOnly: Consumption allowed, modifications restricted for stability
/// - Archived: Historical preservation, limited access for compliance
/// - UnderReview: Compliance evaluation, restricted access during assessment
/// - Deprecated: Phased retirement with migration planning
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Content management systems for modification controls
/// - Access control systems for permission enforcement
/// - Workflow engines for lifecycle automation
/// - Compliance systems for audit and governance
/// </remarks>
/// <example>
/// A production library moves to ReadOnly during critical deployments,
/// then returns to Active once stability is confirmed.
/// </example>
public enum LibraryStatus
{
    /// <summary>
    /// Operational status with full read/write capabilities for active development and deployment.
    /// Library content can be modified, new templates added, and all operational features are available
    /// for collaborative development and production use.
    /// </summary>
    Active = 0,
    
    /// <summary>
    /// Restricted status allowing content consumption but preventing modifications.
    /// Provides stability during critical operations, compliance reviews, or production deployments
    /// while maintaining access to existing templates and workflows.
    /// </summary>
    ReadOnly = 1,
    
    /// <summary>
    /// End-of-life status preserving library content for historical reference and compliance.
    /// Library is no longer operational but maintained for audit trails, historical analysis,
    /// and regulatory compliance requirements with restricted access controls.
    /// </summary>
    Archived = 2,
    
    /// <summary>
    /// Temporary status during compliance evaluation, security review, or quality assessment.
    /// Library access is restricted to authorized reviewers while evaluation processes
    /// determine continued operational status or required modifications.
    /// </summary>
    UnderReview = 3,
    
    /// <summary>
    /// Phased retirement status indicating the library is superseded but maintained for transition.
    /// Supports migration planning and backward compatibility while encouraging adoption
    /// of newer, improved library alternatives.
    /// </summary>
    Deprecated = 4
}

