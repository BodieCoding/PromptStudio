using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a complete execution instance of a workflow with comprehensive traceability, analytics, and enterprise features.
/// 
/// <para><strong>Business Context:</strong></para>
/// This entity serves as the primary execution record for workflow runs in enterprise LLMOps environments,
/// capturing complete execution context, performance metrics, cost tracking, quality assessment, and user feedback.
/// It enables comprehensive workflow analytics, debugging, cost optimization, and quality assurance across
/// complex AI workflow deployments in production environments.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity provides enterprise-grade execution tracking with support for A/B testing, environment-specific
/// execution, version control, comprehensive error handling, and detailed performance monitoring. It integrates
/// with experimentation frameworks, cost management systems, and quality assurance pipelines.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Complete workflow execution observability and traceability
/// - Enterprise cost tracking and resource optimization capabilities
/// - Quality assurance and performance monitoring for production workflows
/// - A/B testing and experimentation framework integration
/// - Comprehensive audit trails for compliance and debugging
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Aggregate Root: Contains all related execution data and child entities
/// - Event Sourcing: Captures complete execution history and state changes
/// - Observer Pattern: Enables monitoring and analytics through execution events
/// - Multi-tenancy: Inherits tenant isolation from AuditableEntity
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Large JSON fields may require compression for high-volume workflows
/// - Consider partitioning by execution date for optimal query performance
/// - Metrics and node executions should be loaded lazily for performance
/// - Archive old executions based on retention policies to manage storage
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Workflow Engine: Core execution tracking and state management
/// - Analytics Platform: Performance metrics and business intelligence
/// - Cost Management: Resource consumption and billing integration
/// - Quality Assurance: Quality scoring and validation frameworks
/// - Experimentation: A/B testing and variant analysis systems
/// </remarks>
/// <example>
/// <code>
/// // Creating a new workflow execution
/// var execution = new FlowExecution
/// {
///     FlowId = workflowId,
///     FlowVersion = "2.1.0",
///     InputVariables = JsonSerializer.Serialize(inputData),
///     ExecutedBy = currentUser.Id,
///     Environment = "production",
///     Status = FlowExecutionStatus.Running,
///     TenantId = currentTenantId
/// };
/// 
/// // Completing execution with results
/// execution.Status = FlowExecutionStatus.Completed;
/// execution.OutputResult = JsonSerializer.Serialize(results);
/// execution.TotalExecutionTime = (int)stopwatch.ElapsedMilliseconds;
/// execution.TotalCost = CalculateTotalCost(execution.NodeExecutions);
/// execution.QualityScore = await qualityService.CalculateScoreAsync(execution);
/// 
/// // Adding user feedback
/// execution.UserRating = 4;
/// execution.UserFeedback = "Good results, but could be faster";
/// execution.FeedbackAt = DateTime.UtcNow;
/// 
/// await executionService.CompleteAsync(execution);
/// </code>
/// </example>
public class FlowExecution : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the workflow that was executed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links the execution to its workflow definition, enabling workflow-specific
    /// analytics, performance tracking, and optimization strategies in enterprise
    /// environments with multiple workflow types and versions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with PromptFlow entity. Essential for
    /// understanding which workflow configuration produced specific results.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the unique identifier of the executed workflow.
    /// </value>
    public Guid FlowId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the executed workflow.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to workflow definition and metadata for execution
    /// analysis and debugging without requiring separate database queries.
    /// </summary>
    /// <value>
    /// A <see cref="PromptFlow"/> instance representing the executed workflow.
    /// </value>
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the version of the workflow that was executed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Captures workflow version for audit trails and rollback capabilities,
    /// enabling enterprises to track workflow evolution impact on execution
    /// performance and quality metrics over time in production environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Snapshot of workflow version at execution time. Critical for understanding
    /// which workflow version produced specific results and for change impact analysis.
    /// </summary>
    /// <value>
    /// A string representing the workflow version (e.g., "2.1.0", "1.5-beta").
    /// Cannot be null or empty. Maximum length is 20 characters.
    /// </value>
    /// <remarks>
    /// Essential for workflow version impact analysis and rollback scenarios.
    /// Enables correlation between workflow changes and execution quality.
    /// </remarks>
    [Required]
    [StringLength(20)]
    public string FlowVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the input variables provided for this execution in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Preserves the exact input data used for execution, enabling comprehensive
    /// debugging, audit trails, data lineage tracking, and reproducibility
    /// in enterprise environments with strict compliance requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON object containing all input variables and their values.
    /// Essential for execution reproduction and input-output correlation analysis.
    /// </summary>
    /// <value>
    /// A JSON string containing the input variables and their values.
    /// Defaults to "{}" for executions without input variables.
    /// </value>
    /// <remarks>
    /// Critical for debugging and reproducing execution issues.
    /// May contain sensitive data - consider encryption for production environments.
    /// </remarks>
    public string InputVariables { get; set; } = "{}";
    
    /// <summary>
    /// Gets or sets the final output result from the workflow execution in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Captures the complete workflow output for analysis, quality assessment,
    /// and business value measurement, enabling comprehensive evaluation of
    /// workflow effectiveness and ROI in enterprise AI deployments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON object containing the final workflow results. Used for quality
    /// scoring, business metrics calculation, and downstream system integration.
    /// </summary>
    /// <value>
    /// A JSON string containing the final workflow output results.
    /// Defaults to "{}" for failed executions or workflows without output.
    /// </value>
    /// <remarks>
    /// Used for quality assessment and business value measurement.
    /// Large outputs may require compression or external storage strategies.
    /// </remarks>
    public string OutputResult { get; set; } = "{}";
    
    /// <summary>
    /// Gets or sets the execution context and metadata in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Preserves execution environment details, configuration settings, and
    /// contextual information for comprehensive debugging, compliance reporting,
    /// and execution environment analysis in enterprise deployments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON object containing execution environment details, configuration
    /// overrides, feature flags, and other contextual execution information.
    /// </summary>
    /// <value>
    /// A JSON string containing execution context and metadata.
    /// Defaults to "{}" for minimal context information.
    /// </value>
    /// <example>
    /// {"environment": "production", "region": "us-west-2", "features": ["caching", "analytics"]}
    /// </example>
    /// <remarks>
    /// Critical for understanding execution environment and configuration impact.
    /// Used for environment-specific analysis and debugging.
    /// </remarks>
    public string ExecutionContext { get; set; } = "{}";
    
    /// <summary>
    /// Gets or sets the identifier of the user or system that initiated this execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables user-specific analytics, access auditing, and personalized
    /// performance tracking, supporting enterprise user experience optimization
    /// and security compliance requirements in multi-user environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// User ID, service account, or system identifier that triggered the execution.
    /// Used for access control validation and user-specific analytics.
    /// </summary>
    /// <value>
    /// A string identifying the execution initiator, or null for system-initiated executions.
    /// Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Used for user-specific analytics and security auditing.
    /// Should match the organization's identity management standards.
    /// </remarks>
    [StringLength(100)]
    public string? ExecutedBy { get; set; }
    
    /// <summary>
    /// Gets or sets the execution environment for deployment stage tracking.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables environment-specific analytics and quality tracking, supporting
    /// enterprise deployment pipelines with multiple stages and environment-specific
    /// performance optimization and quality assurance strategies.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Common values include "development", "staging", "production".
    /// Used for environment-specific metrics and deployment analytics.
    /// </summary>
    /// <value>
    /// A string identifying the execution environment. Default is "production".
    /// Maximum length is 50 characters.
    /// </value>
    /// <remarks>
    /// Used for environment-specific performance analysis and quality tracking.
    /// Should align with organizational deployment pipeline stages.
    /// </remarks>
    [StringLength(50)]
    public string Environment { get; set; } = "production";
    
    // Performance Metrics
    /// <summary>
    /// Total execution time in milliseconds
    /// </summary>
    public int TotalExecutionTime { get; set; }
    
    /// <summary>
    /// Total cost for this execution
    /// </summary>
    public decimal TotalCost { get; set; }
    
    /// <summary>
    /// Total tokens consumed across all nodes
    /// </summary>
    public long TotalTokens { get; set; }
      // Quality and Status
    /// <summary>
    /// Overall execution status
    /// </summary>
    public FlowExecutionStatus Status { get; set; }
    
    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Overall quality score for this execution (0.0 - 1.0)
    /// </summary>
    public decimal? QualityScore { get; set; }
    
    // Experimentation and A/B Testing
    /// <summary>
    /// Experiment ID if this execution was part of an A/B test
    /// </summary>
    public Guid? ExperimentId { get; set; }
    
    /// <summary>
    /// Variant ID if this execution used a specific workflow variant
    /// </summary>
    public Guid? VariantId { get; set; }
    public virtual FlowVariant? Variant { get; set; }
    
    // User Feedback
    /// <summary>
    /// User rating for this execution (1-5 stars)
    /// </summary>
    public int? UserRating { get; set; }
    
    /// <summary>
    /// User feedback comments
    /// </summary>
    public string? UserFeedback { get; set; }
    
    /// <summary>
    /// When user provided feedback
    /// </summary>
    public DateTime? FeedbackAt { get; set; }

    // Navigation properties
    public virtual ICollection<NodeExecution> NodeExecutions { get; set; } = new List<NodeExecution>();
    public virtual ICollection<ExecutionMetric> Metrics { get; set; } = new List<ExecutionMetric>();
}