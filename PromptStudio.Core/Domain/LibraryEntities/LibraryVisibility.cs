namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines visibility and access control levels for prompt libraries in multi-tenant enterprise environments.
/// 
/// <para><strong>Business Context:</strong></para>
/// Library visibility controls determine content sharing policies, collaboration scope, and intellectual property
/// protection within and across organizational boundaries. Visibility levels enable strategic knowledge sharing
/// while maintaining appropriate security boundaries and competitive advantage protection.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Visibility integrates with identity management systems, authorization frameworks, and content discovery
/// mechanisms to enforce access policies. Each level has specific authentication requirements, search visibility,
/// and collaboration capabilities that align with enterprise security and governance policies.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Strategic knowledge sharing and collaboration control
/// - Intellectual property protection and competitive advantage
/// - Multi-tenant security with appropriate boundary enforcement
/// - Flexible collaboration models for different organizational needs
/// </summary>
/// <remarks>
/// <para><strong>Access Hierarchy:</strong></para>
/// Private (most restrictive) → Internal → Collaborative → Public (most open)
/// Each level inherits access from more restrictive levels while adding additional capabilities.
/// 
/// <para><strong>Security Considerations:</strong></para>
/// - Private: Explicit user grant required, audit trail for access
/// - Internal: Organization membership verification, role-based access
/// - Collaborative: Contribution controls, change approval workflows
/// - Public: Content sanitization, compliance with open-source policies
/// 
/// <para><strong>Collaboration Patterns:</strong></para>
/// Visibility level determines available collaboration features, content sharing mechanisms,
/// and workflow integration capabilities for cross-team and cross-organizational scenarios.
/// </remarks>
/// <example>
/// A competitive analysis library might be Private, while best practices
/// for common tasks could be Internal or Collaborative for knowledge sharing.
/// </example>
public enum LibraryVisibility
{
    /// <summary>
    /// Restricted access requiring explicit user grants from library owners.
    /// Content is hidden from discovery mechanisms and requires specific invitation
    /// or permission assignment for access, ensuring maximum confidentiality.
    /// </summary>
    Private = 0,
    
    /// <summary>
    /// Organization-wide visibility for all authenticated organization members.
    /// Content is discoverable within the organization and accessible to members
    /// based on their organizational roles and security clearance levels.
    /// </summary>
    Internal = 1,
    
    /// <summary>
    /// Enhanced internal visibility with contribution capabilities for organization members.
    /// Supports collaborative development workflows where members can contribute content,
    /// suggest improvements, and participate in library development processes.
    /// </summary>
    Collaborative = 2,
    
    /// <summary>
    /// Open visibility with read access for external users in open-source organizations.
    /// Content is publicly discoverable and accessible while maintaining appropriate
    /// controls for contribution and modification by external parties.
    /// </summary>
    Public = 3
}