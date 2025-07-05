namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the approval workflow states for template and variable version management in enterprise environments.
/// 
/// <para><strong>Business Context:</strong></para>
/// Version approval status provides governance controls for AI template evolution, ensuring quality assurance,
/// risk management, and compliance requirements are met before deploying changes to production environments.
/// The approval workflow supports collaborative development while maintaining operational stability and audit compliance.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Approval status integrates with workflow management systems, notification services, and deployment pipelines
/// to automate governance processes. Each status has specific access permissions, visibility rules, and transition
/// requirements that enforce organizational policies for template change management.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Enterprise governance for AI template evolution
/// - Quality assurance through structured review processes
/// - Risk mitigation with controlled deployment workflows
/// - Compliance support with comprehensive audit trails
/// </summary>
/// <remarks>
/// <para><strong>Approval Workflow:</strong></para>
/// Typical progression: Draft → SubmittedForReview → UnderReview → Approved → Deployed
/// Alternative paths include ChangesRequested (back to Draft), Rejected, or Deprecated
/// 
/// <para><strong>Access Controls:</strong></para>
/// - Draft: Author access only for development and refinement
/// - Review stages: Restricted to designated reviewers and stakeholders
/// - Approved: Ready for deployment with appropriate authorization
/// - Deployed: Production status with full operational availability
/// - Deprecated: Maintained for compatibility but discouraged for new use
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Workflow systems for automated status transitions
/// - Notification services for stakeholder communication
/// - Deployment pipelines for automated promotion
/// - Audit systems for compliance and change tracking
/// </remarks>
/// <example>
/// A template version moves: Draft → SubmittedForReview → UnderReview → 
/// Approved → Deployed, with potential returns to Draft if ChangesRequested.
/// </example>
public enum VersionApprovalStatus
{
    /// <summary>
    /// Initial development status where versions are being created and refined.
    /// Accessible only to authors for development, testing, and preparation
    /// before formal submission to the approval workflow.
    /// </summary>
    Draft = 0,
    
    /// <summary>
    /// Status indicating the version has been submitted for formal review.
    /// Version is locked from further editing and queued for evaluation
    /// by designated reviewers and approval stakeholders.
    /// </summary>
    SubmittedForReview = 1,
    
    /// <summary>
    /// Active review status where designated reviewers are evaluating the version.
    /// Review process includes quality assessment, compliance validation,
    /// and business alignment evaluation before approval decisions.
    /// </summary>
    UnderReview = 2,
    
    /// <summary>
    /// Status indicating reviewers have identified issues requiring resolution.
    /// Version returns to draft-like state with specific feedback that must
    /// be addressed before resubmission to the approval workflow.
    /// </summary>
    ChangesRequested = 3,
    
    /// <summary>
    /// Status indicating successful completion of the approval process.
    /// Version has been validated for quality, compliance, and business alignment,
    /// and is authorized for deployment to production environments.
    /// </summary>
    Approved = 4,
    
    /// <summary>
    /// Status indicating the version was rejected during the review process.
    /// Version cannot proceed to deployment and requires significant rework
    /// or alternative approaches to meet organizational approval criteria.
    /// </summary>
    Rejected = 5,
    
    /// <summary>
    /// Production status indicating the version is actively deployed and operational.
    /// Version is available for use in production workflows with full
    /// operational support and monitoring capabilities.
    /// </summary>
    Deployed = 6,
    
    /// <summary>
    /// End-of-life status indicating the version is superseded but maintained for compatibility.
    /// Deprecated versions remain operational for existing workflows while
    /// encouraging migration to newer, improved versions.
    /// </summary>
    Deprecated = 7
}
