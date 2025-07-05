namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the lifecycle states of prompt templates within the enterprise approval and governance workflow.
/// 
/// <para><strong>Business Context:</strong></para>
/// Template status provides governance and quality control for prompt assets used in production systems.
/// The approval workflow ensures that only validated, reviewed, and approved prompts are available
/// for business-critical operations, supporting compliance requirements and maintaining quality standards
/// across the organization's AI-powered processes.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Status controls template visibility in UI components, availability for execution engines,
/// and integration with approval workflow systems. Each status has specific access permissions
/// and operational capabilities that enforce governance policies throughout the template lifecycle.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Enterprise-grade governance for AI prompt assets
/// - Quality assurance through structured review processes
/// - Compliance support with audit trails and approvals
/// - Risk mitigation through controlled template deployment
/// </summary>
/// <remarks>
/// <para><strong>Approval Workflow:</strong></para>
/// Typical flow: Draft → UnderReview → Approved → Published
/// Alternative paths include: ChangesRequested (back to Draft), Rejected, or Deprecated
/// 
/// <para><strong>Governance Controls:</strong></para>
/// - Only Published templates available for production execution
/// - Draft and UnderReview templates accessible only to authors and reviewers
/// - Deprecated templates remain accessible for existing workflows but not new ones
/// - Archived templates preserved for compliance but not operational use
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Approval workflow systems for status transitions
/// - Execution engines for template availability filtering
/// - UI components for role-based template visibility
/// - Audit systems for compliance tracking
/// </remarks>
/// <example>
/// A customer service template moves through: Draft (created) → UnderReview (submitted) → 
/// Approved (reviewed) → Published (deployed) → Deprecated (superseded by v2).
/// </example>
public enum TemplateStatus
{
    /// <summary>
    /// Initial status for templates under development, not ready for review.
    /// Templates in draft status are only visible to authors and can be freely edited
    /// without formal approval processes or version control requirements.
    /// </summary>
    Draft = 0,
    
    /// <summary>
    /// Status indicating the template is being evaluated by designated reviewers.
    /// Templates under review are locked from editing and await approval decisions
    /// based on quality, compliance, and business alignment criteria.
    /// </summary>
    UnderReview = 1,
    
    /// <summary>
    /// Status indicating reviewers have requested modifications before approval.
    /// Templates return to draft-like editing capability with specific feedback
    /// and requirements that must be addressed before resubmission for review.
    /// </summary>
    ChangesRequested = 2,
    
    /// <summary>
    /// Status indicating successful completion of the review and approval process.
    /// Approved templates are validated for quality and compliance but not yet
    /// deployed for production use, awaiting final publication authorization.
    /// </summary>
    Approved = 3,
    
    /// <summary>
    /// Active status indicating the template is deployed and available for production use.
    /// Published templates are accessible to authorized users and systems for
    /// operational workflows and business process execution.
    /// </summary>
    Published = 4,
    
    /// <summary>
    /// Status indicating the template is superseded but remains available for existing workflows.
    /// Deprecated templates maintain operational capability for backward compatibility
    /// while encouraging migration to newer, improved template versions.
    /// </summary>
    Deprecated = 5,
    
    /// <summary>
    /// Final status for templates removed from operational use but preserved for compliance.
    /// Archived templates are not available for new workflows but maintain data integrity
    /// for audit trails, historical analysis, and regulatory compliance requirements.
    /// </summary>
    Archived = 6,
    
    /// <summary>
    /// Status indicating the template was evaluated and rejected during the review process.
    /// Rejected templates cannot proceed to publication and require significant
    /// rework or alternative approaches to meet approval criteria.
    /// </summary>
    Rejected = 7
}
