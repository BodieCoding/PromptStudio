using System.ComponentModel.DataAnnotations;
using System.Security;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a comprehensive workflow definition for orchestrating prompt-based AI operations in enterprise environments.
/// 
/// <para><strong>Business Context:</strong></para>
/// PromptFlows enable sophisticated AI workflow orchestration by combining multiple prompt templates,
/// conditional logic, data transformations, and integration points into cohesive business processes.
/// Organizations can build complex AI-driven workflows that automate document processing, customer service,
/// content generation, and other knowledge work while maintaining full governance and audit capabilities.
/// 
/// <para><strong>Technical Context:</strong></para>
/// PromptFlow implements a node-and-edge graph structure for workflow definition with enterprise features
/// including version management, template traceability, performance analytics, and AI-assisted development.
/// Each flow supports complex orchestration patterns with branching, loops, parallel execution,
/// and integration with external systems through a comprehensive execution engine.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Complex AI workflow orchestration with visual design capabilities
/// - Enterprise-grade version management and change tracking
/// - Performance analytics and optimization insights
/// - Template lineage and impact analysis
/// - AI-assisted workflow development and optimization
/// - Multi-tenant security and governance support
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Workflow Pattern: Node-and-edge graph for process orchestration
/// - Version Management: Semantic versioning with change tracking
/// - Template Traceability: Links between flows and template dependencies
/// - Audit Trail: Comprehensive change and execution tracking
/// - Multi-tenancy: Organizational isolation and security boundaries
/// 
/// <para><strong>Architectural Considerations:</strong></para>
/// - JSON storage for flexible node and edge configurations
/// - Template dependency tracking for impact analysis
/// - Performance metrics for optimization and monitoring
/// - AI assistance integration for development productivity
/// - Execution engine compatibility for runtime orchestration
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index on WorkflowLibraryId for library-based queries
/// - Consider caching active flows for execution performance
/// - JSON field optimization for large workflow definitions
/// - Template dependency indexing for lineage queries
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Workflow Execution Engine: Runtime orchestration and execution
/// - Template Management: Dependency tracking and impact analysis
/// - Version Control: Change management and rollback capabilities
/// - Analytics Platform: Performance monitoring and optimization
/// - AI Assistant: Development support and workflow optimization
/// </remarks>
/// <example>
/// <code>
/// // Creating a comprehensive customer service workflow
/// var flow = new PromptFlow
/// {
///     Name = "Intelligent Customer Support Workflow",
///     Description = "Multi-step customer service automation with sentiment analysis, response generation, and escalation logic",
///     Version = "1.2.0",
///     WorkflowLibraryId = customerServiceLibrary.Id,
///     NodesJson = JsonSerializer.Serialize(new[] {
///         new { id = "sentiment", type = "sentiment-analysis", template_id = sentimentTemplate.Id },
///         new { id = "response", type = "response-generation", template_id = responseTemplate.Id },
///         new { id = "escalation", type = "conditional", rules = escalationRules }
///     }),
///     EdgesJson = JsonSerializer.Serialize(new[] {
///         new { from = "sentiment", to = "response", condition = "positive" },
///         new { from = "sentiment", to = "escalation", condition = "negative" }
///     }),
///     Status = WorkflowStatus.Active,
///     OrganizationId = currentTenantId
/// };
/// 
/// await flowService.CreateAsync(flow);
/// </code>
/// </example>
public class PromptFlow : AuditableEntity
{
    /// <summary>
    /// Gets or sets the human-readable name of the workflow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides clear identification for workflow management, team collaboration,
    /// and administrative interfaces, enabling easy recognition and organization
    /// of complex workflow ecosystems in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Required field with maximum length for UI and database compatibility.
    /// Used extensively in workflow selection, reporting, and management interfaces.
    /// </summary>
    /// <value>
    /// A descriptive name that should reflect the workflow's purpose and scope.
    /// Cannot be null or empty. Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Should be descriptive and follow organizational naming conventions.
    /// Used in UI components, reports, and workflow selection interfaces.
    /// </remarks>
    /// <example>
    /// Examples: "Customer Service Automation", "Document Classification Pipeline", "Content Generation Workflow"
    /// </example>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional detailed description of the workflow's purpose and functionality.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Documents workflow objectives, use cases, and expected outcomes for team
    /// understanding, maintenance, and knowledge transfer, supporting effective
    /// workflow management and collaboration.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional field with generous length for comprehensive documentation.
    /// Used in workflow documentation, discovery, and team onboarding.
    /// </summary>
    /// <value>
    /// A detailed description of workflow purpose, functionality, and use cases.
    /// Can be null. Maximum length is 500 characters.
    /// </value>
    /// <remarks>
    /// Should explain what the workflow does and when to use it.
    /// Important for workflow discovery and team knowledge sharing.
    /// </remarks>
    /// <example>
    /// "Automates customer service responses by analyzing sentiment, generating appropriate responses, and escalating complex issues to human agents"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the semantic version identifier for workflow version management.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables comprehensive version management, change tracking, and rollback
    /// capabilities for workflow evolution, supporting governance requirements
    /// and change management in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Required semantic version string following SemVer conventions (e.g., "1.2.0").
    /// Used for version comparison, dependency management, and rollback operations.
    /// </summary>
    /// <value>
    /// A semantic version string following SemVer format (major.minor.patch).
    /// Cannot be null or empty. Maximum length is 20 characters.
    /// </value>
    /// <remarks>
    /// Should follow semantic versioning conventions for compatibility tracking.
    /// Critical for version management and dependency resolution.
    /// </remarks>
    /// <example>
    /// Examples: "1.0.0" (initial), "1.1.0" (new features), "2.0.0" (breaking changes)
    /// </example>
    [Required]
    [StringLength(20)]
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// Gets or sets the identifier of the workflow library containing this flow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Establishes organizational structure and access control boundaries
    /// for workflow management, enabling proper governance, permissions,
    /// and organizational hierarchy within enterprise workflow ecosystems.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key reference to WorkflowLibrary for organizational structure.
    /// Required for all workflows to establish proper containment and access control.
    /// </summary>
    /// <value>
    /// A valid Guid that references an existing WorkflowLibrary.
    /// Cannot be empty or default Guid value.
    /// </value>
    /// <remarks>
    /// Used for access control, organizational queries, and workflow categorization.
    /// Essential for multi-tenant security and workflow organization.
    /// </remarks>
    public Guid WorkflowLibraryId { get; set; }

    /// <summary>
    /// Gets or sets the workflow library navigation property.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to organizational context and library-level settings
    /// for workflow execution and management operations.
    /// </summary>
    /// <value>
    /// The WorkflowLibrary entity this flow belongs to.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual WorkflowLibrary WorkflowLibrary { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the current status of the workflow for approval workflows and lifecycle management.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables workflow lifecycle management, approval processes, and governance
    /// controls, supporting enterprise change management and quality assurance
    /// requirements for workflow deployment and operation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based status tracking for workflow lifecycle states.
    /// Used for access control, approval workflows, and operational management.
    /// </summary>
    /// <value>
    /// A <see cref="WorkflowStatus"/> enum value indicating current lifecycle state.
    /// Default is Draft for newly created workflows.
    /// </value>
    /// <remarks>
    /// Status transitions should follow defined approval and governance processes.
    /// Used for controlling workflow availability and execution permissions.
    /// </remarks>
    /// <example>
    /// Draft → PendingApproval → Approved → Active → Archived
    /// </example>
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Draft;

    /// <summary>
    /// Gets or sets whether this workflow is currently active and available for execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides operational control over workflow availability and execution,
    /// enabling maintenance windows, gradual rollouts, and emergency deactivation
    /// while preserving workflow configuration and history.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag controlling workflow execution availability.
    /// Overrides inherited IsActive from AuditableEntity for workflow-specific semantics.
    /// </summary>
    /// <value>
    /// True if the workflow should be available for execution, false otherwise.
    /// Default is true for newly created workflows.
    /// </value>
    /// <remarks>
    /// Distinct from soft deletion - allows temporary deactivation without data loss.
    /// Used by execution engines for workflow availability checks.
    /// </remarks>
    /// <example>
    /// workflow.IsActive = false; // Temporarily disable problematic workflow
    /// </example>
    public new bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Gets or sets whether this workflow requires approval before becoming active.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Implements governance and compliance requirements for workflow deployment,
    /// ensuring appropriate review and approval processes for business-critical
    /// or sensitive workflow operations.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag controlling approval workflow requirements.
    /// Used in workflow deployment and status transition logic.
    /// </summary>
    /// <value>
    /// True if the workflow must be approved before activation, false otherwise.
    /// Default is false for newly created workflows.
    /// </value>
    /// <remarks>
    /// Approval requirements typically based on workflow complexity or business impact.
    /// Integrates with organizational approval and governance systems.
    /// </remarks>
    /// <example>
    /// if (workflow.RequiresApproval && workflow.Status == WorkflowStatus.Draft) { /* Start approval process */ }
    /// </example>
    public bool RequiresApproval { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the identifier of the base workflow for inheritance and forking scenarios.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables workflow inheritance, forking, and template relationships,
    /// supporting workflow reuse, standardization, and organizational
    /// knowledge sharing across teams and projects.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional foreign key reference to another PromptFlow entity.
    /// Used for tracking workflow relationships and inheritance chains.
    /// </summary>
    /// <value>
    /// A valid Guid referencing a base PromptFlow, or null if not inherited.
    /// </value>
    /// <remarks>
    /// Enables template workflows and organizational workflow standardization.
    /// Supports workflow evolution tracking and relationship analysis.
    /// </remarks>
    /// <example>
    /// var customizedFlow = baseFlow.Fork(); // customizedFlow.BaseFlowId = baseFlow.Id
    /// </example>
    public Guid? BaseFlowId { get; set; }

    /// <summary>
    /// Gets or sets the base workflow navigation property for inheritance relationships.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to the source workflow configuration for inheritance,
    /// comparison, and relationship analysis in workflow management.
    /// </summary>
    /// <value>
    /// The PromptFlow entity this workflow is based on, or null if not inherited.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual PromptFlow? BaseFlow { get; set; }
    
    /// <summary>
    /// Gets or sets the workflow category identifier for organization and discovery.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables workflow categorization, discovery, and organizational structure,
    /// supporting workflow management, search, and team specialization
    /// in large-scale enterprise workflow ecosystems through flexible categorization.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key reference to WorkflowCategory entity for flexible categorization.
    /// Used in search, filtering, and organizational reporting with full category management.
    /// </summary>
    /// <value>
    /// A valid Guid that references an existing WorkflowCategory.
    /// Cannot be empty or default Guid value.
    /// </value>
    /// <remarks>
    /// Categories support hierarchical organization and both system and user-defined types.
    /// Used extensively in workflow discovery and management interfaces.
    /// </remarks>
    /// <example>
    /// workflow.WorkflowCategoryId = customerServiceCategory.Id;
    /// </example>
    public Guid WorkflowCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the workflow category navigation property for organization and discovery.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to category information including hierarchy, descriptions,
    /// and visual properties for enhanced workflow organization and user experience.
    /// </summary>
    /// <value>
    /// The WorkflowCategory entity this workflow belongs to.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework relationships.
    /// Enables rich category information access without additional queries.
    /// </remarks>
    public virtual WorkflowCategory WorkflowCategory { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets tags for flexible categorization and enhanced search capabilities.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables flexible workflow tagging, enhanced search, and custom
    /// categorization schemes beyond formal categories, supporting
    /// team-specific organization and discovery patterns.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON array format for storing multiple tag values.
    /// Used in search indexing and filtering operations.
    /// </summary>
    /// <value>
    /// A JSON-formatted string containing an array of tag values.
    /// Can be null. Maximum length is 1000 characters.
    /// </value>
    /// <remarks>
    /// Should contain relevant keywords for workflow discovery and organization.
    /// JSON format enables flexible tag management and search optimization.
    /// </remarks>
    /// <example>
    /// ["customer-service", "sentiment-analysis", "auto-escalation", "multilingual"]
    /// </example>
    [StringLength(1000)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Gets or sets the expected execution time in milliseconds for resource planning.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables resource planning, capacity management, and performance
    /// expectations setting for workflow operations and infrastructure
    /// sizing in enterprise deployment scenarios.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional integer value representing expected execution duration.
    /// Used for capacity planning and performance monitoring baselines.
    /// </summary>
    /// <value>
    /// Expected execution time in milliseconds, or null if not estimated.
    /// </value>
    /// <remarks>
    /// Should be updated based on actual performance data and optimization.
    /// Used for resource allocation and performance expectation management.
    /// </remarks>
    /// <example>
    /// workflow.ExpectedExecutionTime = 5000; // 5 seconds expected
    /// </example>
    public int? ExpectedExecutionTime { get; set; }
    
    /// <summary>
    /// Gets or sets the expected cost per execution for budgeting and resource planning.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables cost forecasting, budget planning, and financial optimization
    /// for workflow operations, supporting cost-conscious decision making
    /// and resource allocation in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional decimal value representing expected execution cost.
    /// Used for budget planning and cost optimization analysis.
    /// </summary>
    /// <value>
    /// Expected cost per execution in the system's configured currency, or null if not estimated.
    /// </value>
    /// <remarks>
    /// Should include all associated costs (LLM API, processing, storage).
    /// Used for cost optimization and budget planning analytics.
    /// </remarks>
    /// <example>
    /// workflow.ExpectedCost = 0.15m; // $0.15 per execution expected
    /// </example>
    public decimal? ExpectedCost { get; set; }
    
    /// <summary>
    /// Gets or sets the total number of executions performed for this workflow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks workflow usage and popularity for optimization prioritization,
    /// resource allocation, and performance analysis, supporting data-driven
    /// workflow management and improvement decisions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Performance counter updated via background processes.
    /// Used for usage analytics and performance trend analysis.
    /// </summary>
    /// <value>
    /// Total count of workflow executions since creation.
    /// Default is 0 for newly created workflows.
    /// </value>
    /// <remarks>
    /// Updated by execution tracking systems and analytics processes.
    /// Critical for understanding workflow adoption and performance trends.
    /// </remarks>
    public long ExecutionCount { get; set; } = 0;

    /// <summary>
    /// Gets or sets the average cost per execution based on actual usage data.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides actual cost insights for budget optimization, cost analysis,
    /// and financial planning based on real workflow execution data
    /// rather than estimates.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Performance metric updated via background analytics processes.
    /// Calculated from actual execution cost data and usage patterns.
    /// </summary>
    /// <value>
    /// Average cost per execution in the system's configured currency.
    /// Default is 0 for newly created workflows.
    /// </value>
    /// <remarks>
    /// Updated by cost tracking and analytics systems.
    /// Used for accurate cost forecasting and optimization.
    /// </remarks>
    public decimal AverageCost { get; set; } = 0;

    /// <summary>
    /// Gets or sets the average execution time in milliseconds based on actual performance data.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides performance insights for optimization, capacity planning,
    /// and SLA management based on real workflow execution performance
    /// rather than estimates.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Performance metric updated via background analytics processes.
    /// Calculated from actual execution timing data and performance monitoring.
    /// </summary>
    /// <value>
    /// Average execution time in milliseconds based on actual performance.
    /// Default is 0 for newly created workflows.
    /// </value>
    /// <remarks>
    /// Updated by performance monitoring and analytics systems.
    /// Critical for performance optimization and capacity planning.
    /// </remarks>
    public int AverageExecutionTime { get; set; } = 0;

    /// <summary>
    /// Gets or sets the overall quality score for outputs from this workflow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables quality-based workflow comparison and optimization decisions,
    /// supporting output quality improvement and workflow effectiveness
    /// measurement for continuous improvement.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional decimal score typically normalized to 0-1 or 0-100 scale.
    /// Calculated through quality evaluation frameworks and user feedback.
    /// </summary>
    /// <value>
    /// Quality score for workflow outputs, typically 0-1 or 0-100 scale.
    /// Null if quality assessment has not been performed.
    /// </value>
    /// <remarks>
    /// Quality scoring methodology should be consistent across workflows.
    /// May incorporate human evaluation, automated metrics, or user feedback.
    /// </remarks>
    public decimal? QualityScore { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the most recent workflow execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks workflow usage recency for maintenance planning, optimization
    /// prioritization, and activity monitoring in workflow lifecycle management.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// UTC timestamp updated by execution tracking systems.
    /// Used for activity analysis and workflow lifecycle decisions.
    /// </summary>
    /// <value>
    /// UTC timestamp of the most recent execution, or null if never executed.
    /// </value>
    /// <remarks>
    /// Updated by workflow execution systems and activity tracking.
    /// Important for understanding workflow usage patterns and activity.
    /// </remarks>
    public DateTime? LastExecutedAt { get; set; }
    
    /// <summary>
    /// Gets or sets whether this workflow was generated using AI assistance.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks AI-assisted development for quality analysis, improvement
    /// feedback, and understanding of AI tool effectiveness in workflow
    /// development processes and productivity enhancement.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag indicating AI involvement in workflow creation.
    /// Used for development analytics and AI tool improvement.
    /// </summary>
    /// <value>
    /// True if workflow was created with AI assistance, false otherwise.
    /// Default is false for manually created workflows.
    /// </value>
    /// <remarks>
    /// Helps distinguish AI-generated from manually created workflows.
    /// Used for analyzing AI tool effectiveness and development patterns.
    /// </remarks>
    public bool IsAiGenerated { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the original prompt used to generate this workflow (if AI-generated).
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Preserves the original intent and requirements for AI-generated workflows,
    /// supporting regeneration, refinement, and understanding of workflow
    /// purpose and design decisions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional text field storing the original AI generation prompt.
    /// Used for workflow regeneration and design intent preservation.
    /// </summary>
    /// <value>
    /// The original prompt text used for AI generation, or null if not AI-generated.
    /// </value>
    /// <remarks>
    /// Critical for understanding AI-generated workflow intent and requirements.
    /// Enables workflow regeneration and iterative improvement.
    /// </remarks>
    /// <example>
    /// "Create a customer service workflow that analyzes sentiment and routes complaints appropriately"
    /// </example>
    public string? GenerationPrompt { get; set; }
    
    /// <summary>
    /// Gets or sets the AI confidence score for generated workflows.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Indicates AI system confidence in generated workflow quality,
    /// supporting quality assessment, review prioritization, and
    /// AI tool improvement for workflow generation capabilities.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional decimal value between 0.0 and 1.0 representing AI confidence.
    /// Generated by AI systems during workflow creation process.
    /// </summary>
    /// <value>
    /// AI confidence score between 0.0 and 1.0, or null if not AI-generated.
    /// </value>
    /// <remarks>
    /// Lower confidence scores may indicate need for additional review.
    /// Used for prioritizing human review and quality assurance efforts.
    /// </remarks>
    /// <example>
    /// if (workflow.AiConfidenceScore < 0.7m) { /* Requires additional review */ }
    /// </example>
    public decimal? AiConfidenceScore { get; set; }
    
    /// <summary>
    /// Gets or sets the complexity score for resource allocation and optimization.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables complexity-based resource allocation, performance optimization
    /// prioritization, and capacity planning for workflow execution
    /// infrastructure and development effort estimation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Integer scale typically 1-10 representing workflow complexity.
    /// Used for resource allocation and performance optimization planning.
    /// </summary>
    /// <value>
    /// Complexity score typically on a 1-10 scale.
    /// Default is 1 for newly created workflows.
    /// </value>
    /// <remarks>
    /// Should consider factors like node count, branching complexity, and integration points.
    /// Used for capacity planning and development effort estimation.
    /// </remarks>
    /// <example>
    /// Simple linear flow = 1-3, Complex branching = 4-7, Enterprise integration = 8-10
    /// </example>
    public int ComplexityScore { get; set; } = 1;

    /// <summary>
    /// Gets or sets whether this workflow is currently being used in A/B testing experiments.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks experiment participation for proper workflow management,
    /// preventing conflicting changes during active experiments and
    /// ensuring experiment integrity and statistical validity.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag indicating active experiment participation.
    /// Used for workflow change control and experiment management.
    /// </summary>
    /// <value>
    /// True if the workflow is currently in active A/B testing, false otherwise.
    /// Default is false for newly created workflows.
    /// </value>
    /// <remarks>
    /// Should prevent certain workflow modifications during active experiments.
    /// Used for experiment integrity and change control processes.
    /// </remarks>
    /// <example>
    /// if (workflow.IsInExperiment) { /* Prevent breaking changes */ }
    /// </example>
    public bool IsInExperiment { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the React Flow compatible JSON data for visual builder compatibility.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables seamless integration with visual workflow builders and frontend
    /// applications, supporting user-friendly workflow design and management
    /// interfaces with complete flow structure representation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON string containing React Flow compatible workflow definition.
    /// Used by frontend visual builders and workflow design interfaces.
    /// </summary>
    /// <value>
    /// React Flow compatible JSON containing complete workflow structure.
    /// Default is empty JSON object "{}" for newly created workflows.
    /// </value>
    /// <remarks>
    /// Should be synchronized with relational data for consistency.
    /// Critical for visual workflow builder functionality and user experience.
    /// </remarks>
    /// <example>
    /// {"nodes": [...], "edges": [...], "viewport": {...}}
    /// </example>
    public string FlowData { get; set; } = "{}";
    
    /// <summary>
    /// Gets or sets the storage mode for this workflow's data representation.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls data storage strategy for optimal performance, query capabilities,
    /// and frontend compatibility, enabling flexible workflow management
    /// approaches based on usage patterns and requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum value controlling storage strategy (JSON, Relational, or Hybrid).
    /// Used by data access layers for optimal query and storage decisions.
    /// </summary>
    /// <value>
    /// A <see cref="FlowStorageMode"/> enum value indicating storage strategy.
    /// Default is Hybrid for newly created workflows.
    /// </value>
    /// <remarks>
    /// Hybrid mode provides both JSON and relational storage for optimal flexibility.
    /// Storage mode selection impacts query performance and frontend compatibility.
    /// </remarks>
    /// <example>
    /// JSON = frontend-optimized, Relational = query-optimized, Hybrid = both
    /// </example>
    public FlowStorageMode StorageMode { get; set; } = FlowStorageMode.Hybrid;
    
    /// <summary>
    /// Gets or sets the last synchronization timestamp between JSON and relational data.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Ensures data consistency between different storage representations,
    /// supporting reliable workflow management and preventing data
    /// inconsistencies in hybrid storage scenarios.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// UTC timestamp indicating last successful synchronization operation.
    /// Used by synchronization processes for consistency management.
    /// </summary>
    /// <value>
    /// UTC timestamp of last synchronization, or null if never synchronized.
    /// </value>
    /// <remarks>
    /// Critical for maintaining data consistency in hybrid storage mode.
    /// Used by background synchronization and data integrity processes.
    /// </remarks>
    public DateTime? LastSyncAt { get; set; }
    
    /// <summary>
    /// Gets or sets the hash of the flow structure for change detection and integrity verification.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables rapid change detection, data integrity verification,
    /// and synchronization optimization for workflow management
    /// and version control operations.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// SHA-256 or similar hash of the workflow structure for change detection.
    /// Used for optimization and integrity verification in data operations.
    /// </summary>
    /// <value>
    /// Hash string representing workflow structure state.
    /// Maximum length is 64 characters. Can be null for newly created workflows.
    /// </value>
    /// <remarks>
    /// Should be updated whenever workflow structure changes.
    /// Used for efficient change detection and synchronization optimization.
    /// </remarks>
    [StringLength(64)]
    public string? FlowHash { get; set; }
    
    // Navigation properties
    
    /// <summary>
    /// Gets or sets the collection of nodes that make up this workflow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to individual workflow components for detailed
    /// analysis, execution, and management operations.
    /// </summary>
    /// <value>
    /// Collection of FlowNode entities belonging to this workflow.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual ICollection<FlowNode> Nodes { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of edges that define workflow connections and flow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to workflow connectivity and flow logic for
    /// execution planning and workflow analysis operations.
    /// </summary>
    /// <value>
    /// Collection of FlowEdge entities defining workflow connections.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual ICollection<FlowEdge> Edges { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of executions performed with this workflow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides detailed execution history for performance analysis,
    /// debugging, and workflow optimization insights.
    /// </summary>
    /// <value>
    /// Collection of FlowExecution entities for this workflow.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual ICollection<FlowExecution> Executions { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of template usage relationships for this workflow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides template dependency tracking and impact analysis
    /// for workflow management and change impact assessment.
    /// </summary>
    /// <value>
    /// Collection of WorkflowTemplateUsage entities tracking template dependencies.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual ICollection<WorkflowTemplateUsage> TemplateUsages { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of variants created for A/B testing and optimization.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to experimental variants for performance
    /// comparison and optimization analysis.
    /// </summary>
    /// <value>
    /// Collection of FlowVariant entities for this workflow.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual ICollection<FlowVariant> Variants { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of AI-generated suggestions for workflow improvement.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides AI-powered optimization suggestions for workflow
    /// enhancement and performance improvement.
    /// </summary>
    /// <value>
    /// Collection of WorkflowSuggestion entities for this workflow.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual ICollection<WorkflowSuggestion> Suggestions { get; set; } = [];
}
