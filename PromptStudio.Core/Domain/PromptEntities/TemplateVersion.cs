using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a version snapshot of a prompt template, enabling comprehensive version control and collaborative development.
/// 
/// <para><strong>Business Context:</strong></para>
/// This entity supports enterprise-grade version management for prompt templates, enabling teams to track changes,
/// maintain release history, implement approval workflows, and perform rollbacks in production environments.
/// It facilitates collaborative development, change management, and quality assurance processes essential
/// for enterprise LLMOps deployments with multiple stakeholders and strict governance requirements.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity provides comprehensive version tracking with semantic versioning, content snapshots, metadata preservation,
/// and performance metrics. It supports branching and merging workflows, approval processes, and integrity verification
/// through content hashing and size tracking for enterprise-scale template management.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Complete version history and change tracking for prompt templates
/// - Enterprise approval workflows and governance capabilities
/// - Performance comparison across different template versions
/// - Rollback and branch management for safe template evolution
/// - Comprehensive audit trails for compliance and debugging
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Memento Pattern: Captures and preserves template state at specific points in time
/// - Version Control: Semantic versioning with branching and approval workflows
/// - Snapshot Pattern: Complete state preservation including content and metadata
/// - Multi-tenancy: Inherits tenant isolation from AuditableEntity
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Content snapshots may require compression for large templates
/// - Consider partitioning by template or date for optimal query performance
/// - Content hash enables efficient duplicate detection and integrity verification
/// - Performance metrics should be aggregated periodically for accuracy
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Version Control System: Template change management and approval workflows
/// - Template Engine: Version-specific template rendering and execution
/// - Performance Analytics: Version comparison and optimization insights
/// - Deployment Pipeline: Release management and rollback capabilities
/// - Collaboration Tools: Change tracking and team coordination
/// </remarks>
/// <example>
/// <code>
/// // Creating a new template version
/// var version = new TemplateVersion
/// {
///     PromptTemplateId = templateId,
///     Version = "2.1.0",
///     VersionName = "Enhanced Customer Support",
///     Content = updatedContent,
///     MetadataSnapshot = JsonSerializer.Serialize(templateMetadata),
///     VariablesSnapshot = JsonSerializer.Serialize(variables),
///     ChangeNotes = "Added sentiment analysis and improved response quality",
///     ChangeType = VersionChangeType.Minor,
///     ApprovalStatus = VersionApprovalStatus.PendingReview,
///     ContentHash = ComputeHash(updatedContent),
///     ContentSize = Encoding.UTF8.GetByteCount(updatedContent),
///     TenantId = currentTenantId
/// };
/// 
/// // Approving a version
/// version.ApprovalStatus = VersionApprovalStatus.Approved;
/// version.ApprovedBy = currentUser.Id;
/// version.ApprovedAt = DateTime.UtcNow;
/// version.IsStable = true;
/// 
/// // Making it the current version
/// await versionService.SetAsCurrentAsync(version.Id);
/// </code>
/// </example>
public class TemplateVersion : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the prompt template that owns this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links the version to its parent template, enabling version history tracking
    /// and template evolution analysis in enterprise prompt management systems
    /// with comprehensive change control requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with PromptTemplate entity. Required for all version records.
    /// Used for version collection queries and template-specific operations.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the unique identifier of the owning prompt template.
    /// </value>
    public Guid PromptTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the owning prompt template.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to template metadata and current state for version
    /// comparison and management operations without separate database queries.
    /// </summary>
    /// <value>
    /// A <see cref="PromptTemplate"/> instance representing the owning template.
    /// </value>
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the semantic version identifier for this template version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides standardized version identification following semantic versioning principles,
    /// enabling clear communication about changes and compatibility in enterprise
    /// environments with multiple stakeholders and deployment stages.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Should follow semantic versioning format (MAJOR.MINOR.PATCH).
    /// Used for version comparison, dependency resolution, and deployment decisions.
    /// </summary>
    /// <value>
    /// A string representing the semantic version (e.g., "2.1.0", "1.5.3-beta").
    /// Cannot be null or empty. Maximum length is 20 characters.
    /// </value>
    /// <remarks>
    /// Should follow semantic versioning conventions for consistency.
    /// Used for version comparison and compatibility assessment.
    /// </remarks>
    [Required]
    [StringLength(20)]
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional human-readable name for this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides meaningful identification for versions beyond numeric identifiers,
    /// improving communication and recognition in enterprise environments where
    /// versions may have specific business significance or feature names.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Used for display purposes in user interfaces and reports.
    /// Should be descriptive and reflect the version's key characteristics or changes.
    /// </summary>
    /// <value>
    /// A descriptive name for the version, or null if no specific name is assigned.
    /// Maximum length is 100 characters.
    /// </value>
    /// <example>
    /// Examples: "Enhanced Customer Support", "Multi-language Support", "Performance Optimization"
    /// </example>
    [StringLength(100)]
    public string? VersionName { get; set; }
    
    /// <summary>
    /// Gets or sets the complete template content snapshot at this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Preserves the exact template content for historical reference, rollback capabilities,
    /// and change analysis, enabling enterprises to maintain complete audit trails
    /// and support regulatory compliance requirements for AI model governance.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Contains the complete template content including prompt text and variable placeholders.
    /// Used for version comparison, rollback operations, and historical analysis.
    /// </summary>
    /// <value>
    /// A string containing the complete template content at this version.
    /// Cannot be null or empty.
    /// </value>
    /// <remarks>
    /// Preserves exact content for rollback and comparison purposes.
    /// Large content may require compression for storage optimization.
    /// </remarks>
    [Required]
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a snapshot of the template metadata at this version in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Captures complete template configuration and settings for historical reference
    /// and change analysis, enabling comprehensive impact assessment and rollback
    /// capabilities in enterprise template management environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON serialization of template metadata including categories, tags, settings,
    /// and configuration options at the time of version creation.
    /// </summary>
    /// <value>
    /// A JSON string containing the template metadata snapshot,
    /// or null if metadata tracking is not enabled.
    /// </value>
    /// <remarks>
    /// Enables complete template state restoration and change impact analysis.
    /// Used for configuration rollback and historical analysis.
    /// </remarks>
    public string? MetadataSnapshot { get; set; }
    
    /// <summary>
    /// Gets or sets a snapshot of the variable definitions at this version in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Preserves variable schema and configuration for compatibility assessment
    /// and rollback scenarios, ensuring enterprises can maintain data integrity
    /// and interface compatibility across template versions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON serialization of all variable definitions including types, validation rules,
    /// and default values at the time of version creation.
    /// </summary>
    /// <value>
    /// A JSON string containing the variable definitions snapshot,
    /// or null if variable tracking is not enabled.
    /// </value>
    /// <remarks>
    /// Critical for maintaining compatibility when rolling back versions.
    /// Used for variable schema evolution analysis and impact assessment.
    /// </remarks>
    public string? VariablesSnapshot { get; set; }
    
    /// <summary>
    /// Gets or sets the change notes describing modifications in this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Documents the rationale and details of changes for team communication,
    /// audit purposes, and change impact assessment in enterprise environments
    /// requiring comprehensive change documentation and approval workflows.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Freeform text describing changes, improvements, bug fixes, or new features.
    /// Used for change communication and audit trail documentation.
    /// </summary>
    /// <value>
    /// A description of changes made in this version, or null if no notes are provided.
    /// Maximum length is 1000 characters.
    /// </value>
    /// <example>
    /// "Added sentiment analysis capability and improved response quality for negative feedback scenarios. Fixed variable validation for edge cases."
    /// </example>
    [StringLength(1000)]
    public string? ChangeNotes { get; set; }
    
    /// <summary>
    /// Gets or sets the type of change represented by this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Categorizes the change impact for risk assessment, deployment planning,
    /// and approval workflow routing in enterprise environments with
    /// different approval requirements for different change types.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Follows semantic versioning principles for change classification.
    /// Used for deployment pipeline routing and approval workflow decisions.
    /// </summary>
    /// <value>
    /// A <see cref="VersionChangeType"/> enum value indicating the change type.
    /// Default is Minor.
    /// </value>
    /// <remarks>
    /// Used for determining appropriate approval workflows and deployment strategies.
    /// Should align with semantic versioning change impact classification.
    /// </remarks>
    public VersionChangeType ChangeType { get; set; } = VersionChangeType.Minor;
    
    /// <summary>
    /// Gets or sets a value indicating whether this version is marked as stable and production-ready.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Distinguishes between experimental and production-ready versions,
    /// supporting enterprise deployment strategies with clear stability
    /// indicators and risk management for production AI deployments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Stable versions are suitable for production deployment and should have
    /// passed all quality gates and approval processes.
    /// </summary>
    /// <value>
    /// <c>true</c> if the version is stable and production-ready; otherwise, <c>false</c>.
    /// Default is <c>false</c>.
    /// </value>
    /// <remarks>
    /// Used for deployment filtering and production readiness assessment.
    /// Should only be set after appropriate testing and approval processes.
    /// </remarks>
    public bool IsStable { get; set; } = false;
    
    /// <summary>
    /// Gets or sets a value indicating whether this is the currently active version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Identifies the version currently used for template execution,
    /// enabling clear version management and controlled deployment
    /// in enterprise environments with multiple concurrent versions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Only one version per template should be marked as current.
    /// Used for template resolution and execution routing.
    /// </summary>
    /// <value>
    /// <c>true</c> if this is the current active version; otherwise, <c>false</c>.
    /// Default is <c>false</c>.
    /// </value>
    /// <remarks>
    /// Enforced as a business rule - only one version per template can be current.
    /// Used for determining which version to use for template execution.
    /// </remarks>
    public bool IsCurrent { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the unique identifier of the parent version for branching and merging scenarios.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables sophisticated branching and merging workflows for collaborative development,
    /// supporting enterprise development practices with parallel feature development
    /// and controlled integration processes.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// References another TemplateVersion that serves as the base for this version.
    /// Used for tracking version genealogy and merge conflict resolution.
    /// </summary>
    /// <value>
    /// A nullable <see cref="Guid"/> representing the parent version identifier,
    /// or null for initial versions or main branch versions.
    /// </value>
    /// <remarks>
    /// Used for branch tracking and merge conflict resolution.
    /// Enables sophisticated version control workflows similar to Git branching.
    /// </remarks>
    public Guid? ParentVersionId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the parent version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to parent version data for comparison and merge operations
    /// without requiring separate database queries.
    /// </summary>
    /// <value>
    /// A <see cref="TemplateVersion"/> instance representing the parent version,
    /// or null if no parent exists.
    /// </value>
    public virtual TemplateVersion? ParentVersion { get; set; }
    
    /// <summary>
    /// Gets or sets the size of this version's content in bytes.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables storage management and performance optimization by tracking
    /// content size growth over time, supporting enterprise resource planning
    /// and storage optimization strategies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Calculated as the byte length of the content string.
    /// Used for storage analytics and performance optimization decisions.
    /// </summary>
    /// <value>
    /// A long integer representing the content size in bytes. Default is 0.
    /// </value>
    /// <remarks>
    /// Used for storage management and performance optimization.
    /// Large content sizes may indicate need for compression or external storage.
    /// </remarks>
    public long ContentSize { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the SHA-256 hash of the content for integrity verification and duplicate detection.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Ensures content integrity and enables efficient duplicate detection,
    /// supporting enterprise data governance and storage optimization
    /// requirements with large-scale template management.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// SHA-256 hash of the version content. Used for integrity verification
    /// and identifying identical content across different versions.
    /// </summary>
    /// <value>
    /// A 64-character hexadecimal string representing the SHA-256 hash,
    /// or null if hash calculation is not performed.
    /// </value>
    /// <remarks>
    /// Used for content integrity verification and duplicate detection.
    /// Enables efficient storage optimization through deduplication.
    /// </remarks>
    [StringLength(64)]
    public string? ContentHash { get; set; }
    
    /// <summary>
    /// Gets or sets the approval status of this version for enterprise governance workflows.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Implements enterprise approval workflows and governance controls,
    /// ensuring only approved versions are deployed to production environments
    /// and maintaining audit trails for regulatory compliance.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Controls version availability for deployment and execution.
    /// Used for approval workflow routing and access control decisions.
    /// </summary>
    /// <value>
    /// A <see cref="VersionApprovalStatus"/> enum value indicating the approval state.
    /// Default is Draft.
    /// </value>
    /// <remarks>
    /// Used for approval workflow management and deployment control.
    /// Should be updated through appropriate approval processes.
    /// </remarks>
    public VersionApprovalStatus ApprovalStatus { get; set; } = VersionApprovalStatus.Draft;
    
    /// <summary>
    /// Gets or sets the identifier of the user who approved this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Maintains accountability and audit trails for approval decisions,
    /// supporting enterprise governance requirements and regulatory
    /// compliance in controlled AI deployment environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// User identifier from the organization's identity system.
    /// Set when the version approval status is changed to approved.
    /// </summary>
    /// <value>
    /// A string identifying the approving user, or null if not yet approved.
    /// Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Used for audit trails and accountability tracking.
    /// Should be set automatically when approval status changes to approved.
    /// </remarks>
    [StringLength(100)]
    public string? ApprovedBy { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when this version was approved.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Records the exact time of approval for audit trails and compliance
    /// reporting, supporting enterprise governance and regulatory requirements
    /// for AI model change management and deployment tracking.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// UTC timestamp set when approval status changes to approved.
    /// Used for audit reporting and compliance documentation.
    /// </summary>
    /// <value>
    /// A nullable <see cref="DateTime"/> representing the approval time in UTC,
    /// or null if the version has not been approved.
    /// </value>
    /// <remarks>
    /// Used for audit trails and compliance reporting.
    /// Should be set automatically during the approval process.
    /// </remarks>
    public DateTime? ApprovedAt { get; set; }
    
    /// <summary>
    /// Gets or sets the average cost per execution for this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables cost comparison across versions for optimization decisions,
    /// supporting enterprise cost management and ROI analysis for
    /// template improvements and AI model selection strategies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Calculated average based on actual execution costs.
    /// Updated periodically as more executions are completed.
    /// </summary>
    /// <value>
    /// A nullable decimal representing the average cost per execution,
    /// or null if no executions have been recorded.
    /// </value>
    /// <remarks>
    /// Used for version performance comparison and cost optimization.
    /// Should be updated regularly based on execution metrics.
    /// </remarks>
    public decimal? AverageCost { get; set; }
    
    /// <summary>
    /// Gets or sets the average response time for this version in milliseconds.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables performance comparison across versions for optimization decisions,
    /// supporting enterprise SLA management and user experience optimization
    /// through data-driven version selection and improvement strategies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Calculated average response time in milliseconds.
    /// Updated based on actual execution performance metrics.
    /// </summary>
    /// <value>
    /// A nullable integer representing the average response time in milliseconds,
    /// or null if no executions have been recorded.
    /// </value>
    /// <remarks>
    /// Used for performance comparison and optimization decisions.
    /// Critical for SLA compliance and user experience management.
    /// </remarks>
    public int? AverageResponseTime { get; set; }
    
    /// <summary>
    /// Gets or sets the average quality score for this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables quality comparison across versions for improvement tracking,
    /// supporting enterprise quality assurance and continuous improvement
    /// programs with data-driven quality optimization strategies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Normalized quality score between 0.0 and 1.0.
    /// Calculated based on quality assessment algorithms and user feedback.
    /// </summary>
    /// <value>
    /// A nullable decimal representing the average quality score (0.0 to 1.0),
    /// or null if no quality assessments have been recorded.
    /// </value>
    /// <remarks>
    /// Used for quality tracking and version comparison.
    /// Should be updated based on quality assessment results and user feedback.
    /// </remarks>
    public decimal? QualityScore { get; set; }
    
    /// <summary>
    /// Gets or sets the total number of executions for this version.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks version usage for adoption analysis and confidence building,
    /// supporting enterprise decision-making about version stability
    /// and readiness for broader deployment based on usage patterns.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Incremented each time this version is executed.
    /// Used for calculating statistical significance of performance metrics.
    /// </summary>
    /// <value>
    /// A long integer representing the total execution count. Default is 0.
    /// </value>
    /// <remarks>
    /// Used for adoption tracking and statistical confidence assessment.
    /// Higher execution counts provide more reliable performance metrics.
    /// </remarks>
    public long ExecutionCount { get; set; } = 0;
}