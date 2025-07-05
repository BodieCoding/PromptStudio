using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the association and usage tracking between workflows and prompt templates, providing comprehensive traceability, performance analytics, and version management.
/// 
/// <para><strong>Business Context:</strong></para>
/// WorkflowTemplateUsage enables organizations to track template utilization across workflows,
/// monitor performance in different contexts, manage template versions, and maintain audit
/// trails for compliance and optimization. This supports template reuse strategies, performance
/// optimization, and impact analysis when templates are modified or deprecated, ensuring
/// enterprise-grade governance and operational excellence.
/// 
/// <para><strong>Technical Context:</strong></para>
/// WorkflowTemplateUsage serves as a linking entity with performance tracking capabilities,
/// providing version snapshots, context-specific configuration, and usage analytics within
/// the workflow execution framework. The entity maintains referential integrity while enabling
/// flexible template composition and comprehensive usage monitoring.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Complete template usage traceability for governance and compliance
/// - Performance analytics for template optimization and workflow tuning
/// - Version management with snapshot preservation for reproducibility
/// - Context-aware template configuration for flexible workflow design
/// - Usage analytics for template ROI and optimization opportunities
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Association Object Pattern: Rich relationship with behavior and data
/// - Snapshot Pattern: Version preservation for audit and reproducibility
/// - Performance Monitoring Pattern: Context-specific metrics collection
/// - Configuration Pattern: Flexible template adaptation per usage context
/// - Audit Trail Pattern: Complete usage lifecycle tracking
/// 
/// <para><strong>Template Lifecycle Management:</strong></para>
/// - Version tracking for template evolution and compatibility
/// - Snapshot preservation for deterministic execution and audit
/// - Active/inactive status for lifecycle and deprecation management
/// - Performance monitoring for optimization and quality assessment
/// - Configuration management for context-specific customization
/// 
/// <para><strong>Performance Analytics:</strong></para>
/// - Execution count tracking for usage pattern analysis
/// - Cost monitoring for resource optimization and budgeting
/// - Execution time analysis for performance tuning and bottleneck identification
/// - Quality scoring for template effectiveness measurement
/// - Context-specific metrics for comparative analysis
/// 
/// <para><strong>Integration Architecture:</strong></para>
/// - Workflow Engine: Template execution and performance monitoring
/// - Template Repository: Version management and snapshot creation
/// - Analytics Platform: Usage metrics and optimization insights
/// - Configuration Management: Context-specific template adaptation
/// - Audit System: Compliance tracking and change management
/// 
/// <para><strong>Usage Contexts:</strong></para>
/// - Primary: Main template for node execution
/// - Fallback: Alternative template for error handling or validation
/// - Validation: Template for output verification and quality control
/// - A/B Testing: Template variants for performance comparison
/// - Conditional: Template for specific execution conditions
/// </remarks>
/// <example>
/// <code>
/// // Track template usage in a customer service workflow
/// var templateUsage = new WorkflowTemplateUsage
/// {
///     FlowId = customerServiceFlow.Id,
///     TemplateId = responseGeneratorTemplate.Id,
///     NodeId = responseNode.Id,
///     TemplateVersion = "v2.1.0",
///     TemplateSnapshot = await templateRepository.GetSnapshotAsync(responseGeneratorTemplate.Id, "v2.1.0"),
///     NodeRole = "primary",
///     IsActive = true,
///     UsageConfiguration = JsonSerializer.Serialize(new
///     {
///         MaxTokens = 1500,
///         Temperature = 0.7,
///         CustomPromptPrefix = "As a customer service representative:",
///         ResponseFormat = "structured",
///         QualityThreshold = 0.85
///     })
/// };
/// 
/// // Track performance over time
/// await templateUsage.UpdatePerformanceMetrics(new
/// {
///     ExecutionCount = 1247,
///     AverageCost = 0.023m,
///     AverageExecutionTime = 2340, // milliseconds
///     QualityScore = 0.91m
/// });
/// 
/// // Handle template version upgrade
/// when (responseGeneratorTemplate.CurrentVersion != templateUsage.TemplateVersion)
/// {
///     // Create new usage record for new version
///     var upgradedUsage = templateUsage.Clone();
///     upgradedUsage.TemplateVersion = responseGeneratorTemplate.CurrentVersion;
///     upgradedUsage.TemplateSnapshot = await templateRepository.GetCurrentSnapshotAsync(responseGeneratorTemplate.Id);
///     upgradedUsage.ExecutionCount = 0; // Reset metrics for new version
///     
///     // Deactivate old usage
///     templateUsage.IsActive = false;
///     
///     await repository.AddAsync(upgradedUsage);
/// }
/// </code>
/// </example>
public class WorkflowTemplateUsage : AuditableEntity
{
    /// <summary>
    /// Reference to the workflow that utilizes the template for execution context and traceability.
    /// <value>Guid identifier linking this usage record to the parent workflow</value>
    /// </summary>
    /// <remarks>
    /// Establishes the workflow context for template usage analysis and enables
    /// workflow-specific performance tracking and optimization insights.
    /// </remarks>
    public Guid FlowId { get; set; }

    /// <summary>
    /// Navigation property to the workflow entity for comprehensive context analysis.
    /// <value>PromptFlow entity representing the workflow using the template</value>
    /// </summary>
    /// <remarks>
    /// Provides access to workflow metadata, execution history, and configuration
    /// for comprehensive template usage analysis and optimization recommendations.
    /// </remarks>
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Reference to the prompt template being utilized within the workflow context.
    /// <value>Guid identifier linking this usage record to the template being used</value>
    /// </summary>
    /// <remarks>
    /// Establishes template identity for usage tracking, performance analysis,
    /// and version management across multiple workflow contexts.
    /// </remarks>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Navigation property to the template entity for configuration and version access.
    /// <value>PromptTemplate entity representing the template being utilized</value>
    /// </summary>
    /// <remarks>
    /// Provides access to template content, configuration, and version history
    /// for comprehensive usage analysis and change impact assessment.
    /// </remarks>
    public virtual PromptTemplate Template { get; set; } = null!;
    
    /// <summary>
    /// Reference to the specific workflow node where the template is utilized.
    /// <value>Guid identifier linking this usage to the specific execution node</value>
    /// </summary>
    /// <remarks>
    /// Establishes node-specific context for template usage, enabling granular
    /// performance analysis and node-level optimization strategies.
    /// </remarks>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Navigation property to the workflow node for execution context and configuration.
    /// <value>FlowNode entity representing the specific node using the template</value>
    /// </summary>
    /// <remarks>
    /// Provides access to node configuration, execution parameters, and context
    /// for detailed template usage analysis and performance optimization.
    /// </remarks>
    public virtual FlowNode Node { get; set; } = null!;
    
    /// <summary>
    /// Version identifier of the template when it was associated with the workflow node.
    /// <value>String representing the template version for compatibility and audit tracking</value>
    /// </summary>
    /// <remarks>
    /// Critical for version compatibility, change impact analysis, and audit trails.
    /// Enables rollback scenarios and ensures reproducible workflow execution.
    /// </remarks>
    [Required]
    [StringLength(20)]
    public string TemplateVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Immutable snapshot of template content at the time of association for reproducibility.
    /// <value>String containing the complete template content and configuration at association time</value>
    /// </summary>
    /// <remarks>
    /// Ensures deterministic execution and enables audit trails even when
    /// the original template is modified or deleted. Critical for compliance scenarios.
    /// </remarks>
    public string? TemplateSnapshot { get; set; }
    
    /// <summary>
    /// Functional role of the template within the specific node execution context.
    /// <value>String identifying the template's purpose within the node (e.g., "primary", "fallback", "validation")</value>
    /// </summary>
    /// <remarks>
    /// Defines how the template is utilized within the node's execution logic,
    /// supporting complex template composition and conditional execution patterns.
    /// </remarks>
    [StringLength(50)]
    public string NodeRole { get; set; } = "primary";
    
    /// <summary>
    /// Flag indicating whether this template usage is currently active within the workflow.
    /// <value>Boolean controlling whether this template usage participates in workflow execution</value>
    /// </summary>
    /// <remarks>
    /// Enables template lifecycle management, A/B testing scenarios, and gradual
    /// template migration without disrupting workflow operation. Uses 'new' to override base IsActive.
    /// </remarks>
    public new bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Context-specific configuration parameters for template execution within this node.
    /// <value>JSON string containing node-specific template configuration and parameters</value>
    /// </summary>
    /// <remarks>
    /// Enables flexible template adaptation per usage context while maintaining
    /// template reusability and supporting specialized execution requirements.
    /// </remarks>
    public string? UsageConfiguration { get; set; }

    // Performance Metrics
    /// <summary>
    /// Total number of executions for this template usage within the workflow context.
    /// <value>Long integer representing cumulative execution count for usage analytics</value>
    /// </summary>
    /// <remarks>
    /// Provides usage volume metrics for template popularity analysis, capacity
    /// planning, and performance optimization prioritization decisions.
    /// </remarks>
    public long ExecutionCount { get; set; } = 0;

    /// <summary>
    /// Average financial cost per execution for this template usage in the workflow context.
    /// <value>Decimal representing mean execution cost for resource optimization and budgeting</value>
    /// </summary>
    /// <remarks>
    /// Enables cost analysis, budget planning, and template efficiency comparison
    /// across different usage contexts and workflow configurations.
    /// </remarks>
    public decimal AverageCost { get; set; } = 0;

    /// <summary>
    /// Average execution duration in milliseconds for performance monitoring and optimization.
    /// <value>Integer representing mean execution time for performance analysis and tuning</value>
    /// </summary>
    /// <remarks>
    /// Critical for performance optimization, SLA monitoring, and identifying
    /// performance bottlenecks in workflow execution paths.
    /// </remarks>
    public int AverageExecutionTime { get; set; } = 0;

    /// <summary>
    /// Average quality score for template outputs within this usage context.
    /// <value>Decimal representing mean quality assessment for effectiveness measurement</value>
    /// </summary>
    /// <remarks>
    /// Enables template effectiveness evaluation, quality trend analysis, and
    /// data-driven decisions for template optimization and replacement strategies.
    /// </remarks>
    public decimal? QualityScore { get; set; }
}
