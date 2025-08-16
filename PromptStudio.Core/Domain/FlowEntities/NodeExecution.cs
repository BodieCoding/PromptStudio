using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the execution record for individual nodes within workflow executions, providing comprehensive tracking and debugging capabilities.
/// 
/// <para><strong>Business Context:</strong></para>
/// This entity serves as the cornerstone for workflow observability and performance optimization in enterprise LLMOps environments.
/// It captures detailed execution metrics, timing data, resource consumption, and quality indicators for each node execution,
/// enabling sophisticated analytics, debugging, and optimization strategies for complex AI workflows.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity provides granular execution tracking with support for retry mechanisms, caching strategies, error handling,
/// and performance monitoring. It integrates with AI providers to track token usage and costs, while maintaining
/// comprehensive audit trails for compliance and debugging purposes.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Comprehensive workflow observability and debugging capabilities
/// - Performance optimization through detailed metrics and timing data
/// - Cost tracking and resource management for AI model usage
/// - Quality assurance through execution result scoring and validation
/// - Enterprise-grade audit trails for compliance and troubleshooting
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Observer Pattern: Captures execution events for monitoring and analytics
/// - State Machine: Tracks execution status transitions and retry logic
/// - Metrics Collection: Comprehensive performance and quality data gathering
/// - Multi-tenancy: Inherits tenant isolation from AuditableEntity
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Large datasets benefit from indexing on FlowExecutionId and ExecutionOrder
/// - JSON properties (InputData, OutputData) should be optimized for query patterns
/// - PerformanceMetrics and DebugInfo may require compression for large workflows
/// - Consider partitioning by execution date for high-volume environments
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Workflow Engine: Core execution tracking for flow processing
/// - Analytics Service: Performance metrics and quality analysis
/// - Billing System: Cost tracking and resource consumption reporting
/// - Monitoring: Real-time execution status and error tracking
/// - Debugging Tools: Detailed execution traces and diagnostic information
/// </remarks>
/// <example>
/// <code>
/// // Creating a node execution record
/// var nodeExecution = new NodeExecution
/// {
///     FlowExecutionId = flowExecutionId,
///     NodeId = nodeId,
///     NodeKey = "prompt_generation",
///     NodeType = FlowNodeType.PromptExecution,
///     ExecutionOrder = 1,
///     StartTime = DateTime.UtcNow,
///     Status = NodeExecutionStatus.Running,
///     TenantId = currentTenantId
/// };
/// 
/// // Completing execution with results
/// nodeExecution.EndTime = DateTime.UtcNow;
/// nodeExecution.Status = NodeExecutionStatus.Completed;
/// nodeExecution.OutputData = JsonSerializer.Serialize(results);
/// nodeExecution.DurationMs = (int)(nodeExecution.EndTime - nodeExecution.StartTime).Value.TotalMilliseconds;
/// nodeExecution.QualityScore = CalculateQualityScore(results);
/// 
/// // Tracking AI usage and costs
/// nodeExecution.AiProvider = "OpenAI";
/// nodeExecution.AiModel = "gpt-4";
/// nodeExecution.TokensUsed = response.Usage.TotalTokens;
/// nodeExecution.Cost = CalculateCost(nodeExecution.TokensUsed, nodeExecution.AiModel);
/// </code>
/// </example>
public class NodeExecution : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the parent workflow execution that contains this node execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links individual node executions to their parent workflow execution, enabling comprehensive
    /// workflow analysis, debugging, and performance optimization across all constituent nodes.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with FlowExecution entity. Essential for grouping node executions
    /// and understanding workflow-level performance characteristics and dependencies.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the unique identifier of the parent workflow execution.
    /// </value>
    /// <remarks>
    /// This property is required and establishes the hierarchical relationship between
    /// workflow executions and their constituent node executions.
    /// </remarks>
    public Guid FlowExecutionId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the parent workflow execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to workflow-level metadata, configuration, and context
    /// without requiring separate database queries.
    /// </summary>
    /// <value>
    /// A <see cref="FlowExecution"/> instance representing the parent workflow execution.
    /// </value>
    public virtual FlowExecution FlowExecution { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the unique identifier of the workflow node that was executed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links the execution record to the specific node definition, enabling analysis
    /// of node-specific performance patterns, success rates, and optimization opportunities.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with FlowNode entity. Critical for understanding
    /// which node configuration and logic produced specific execution results.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the unique identifier of the executed node.
    /// </value>
    public Guid NodeId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the executed workflow node.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to node configuration, type information, and metadata
    /// for execution analysis and debugging purposes.
    /// </summary>
    /// <value>
    /// A <see cref="FlowNode"/> instance representing the executed node.
    /// </value>
    public virtual FlowNode Node { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the human-readable key identifying the node within its workflow context.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides a stable, human-readable identifier for node executions that remains
    /// consistent across workflow versions and executions, enabling efficient debugging
    /// and performance analysis in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Duplicated from the FlowNode for performance optimization and to maintain
    /// execution history even if node definitions change or are deleted.
    /// </summary>
    /// <value>
    /// A string representing the node's key within the workflow (e.g., "start", "llm-classification").
    /// Maximum length is 50 characters. Cannot be null or empty.
    /// </value>
    /// <remarks>
    /// This value is copied from the FlowNode at execution time to ensure
    /// historical execution records remain valid even if node definitions change.
    /// </remarks>
    [Required]
    [StringLength(50)]
    public string NodeKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of node that was executed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables type-specific analysis and optimization of workflow performance,
    /// allowing enterprises to identify bottlenecks and optimization opportunities
    /// based on node functionality (e.g., LLM calls vs. data transformations).
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Duplicated from FlowNode for performance and historical consistency.
    /// Used for execution routing and result interpretation.
    /// </summary>
    /// <value>
    /// A <see cref="FlowNodeType"/> enum value indicating the executed node's type.
    /// </value>
    public FlowNodeType NodeType { get; set; }
    
    /// <summary>
    /// Gets or sets the execution sequence order within the parent workflow execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables reconstruction of execution flow and analysis of workflow timing patterns,
    /// critical for debugging complex parallel workflows and identifying performance bottlenecks.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Represents the chronological order of node execution starts within the workflow.
    /// May not be strictly sequential for parallel execution scenarios.
    /// </summary>
    /// <value>
    /// An integer representing the execution order, starting from 1.
    /// </value>
    /// <remarks>
    /// For parallel workflows, multiple nodes may have the same or similar execution orders.
    /// Use in combination with StartTime for precise execution sequence analysis.
    /// </remarks>
    public int ExecutionOrder { get; set; }
    
    /// <summary>
    /// Gets or sets the UTC timestamp when node execution began.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides precise timing data for performance analysis, SLA monitoring,
    /// and workflow optimization in enterprise environments with strict timing requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Recorded in UTC for consistency across different time zones and deployments.
    /// Used for calculating execution duration and analyzing workflow timing patterns.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> representing the execution start time in UTC.
    /// </value>
    /// <remarks>
    /// Always recorded in UTC to ensure consistent timing analysis across global deployments.
    /// Essential for accurate performance monitoring and SLA compliance tracking.
    /// </remarks>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the UTC timestamp when node execution completed or failed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables precise duration calculation and completion time analysis for performance
    /// optimization and capacity planning in enterprise workflow environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Null while execution is in progress. Set when execution reaches a terminal state
    /// (Completed, Failed, Cancelled, TimedOut, or Skipped).
    /// </summary>
    /// <value>
    /// A nullable <see cref="DateTime"/> representing the execution end time in UTC,
    /// or null if execution is still in progress.
    /// </value>
    /// <remarks>
    /// Used in combination with StartTime to calculate precise execution duration.
    /// Null values indicate ongoing or abandoned executions.
    /// </remarks>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the total execution duration in milliseconds.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides immediate access to execution timing data for performance monitoring,
    /// SLA compliance checking, and workflow optimization without requiring date calculations.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Calculated as (EndTime - StartTime).TotalMilliseconds when execution completes.
    /// Null for in-progress or failed-to-start executions.
    /// </summary>
    /// <value>
    /// A nullable integer representing the execution duration in milliseconds,
    /// or null if execution has not completed.
    /// </value>
    /// <remarks>
    /// Pre-calculated for performance optimization in reporting and analytics queries.
    /// Used for performance trending and capacity planning analysis.
    /// </remarks>
    public int? DurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the input data provided to this node in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables comprehensive debugging, audit trails, and data lineage tracking
    /// by preserving the exact input data used for each node execution.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Stored as JSON to accommodate varying input schemas across different node types.
    /// May contain variable values, previous node outputs, or configuration data.
    /// </summary>
    /// <value>
    /// A JSON string containing the input data provided to the node.
    /// Defaults to "{}" for nodes without input data.
    /// </value>
    /// <remarks>
    /// Critical for debugging and reproducing execution issues.
    /// May contain sensitive data - consider encryption for production environments.
    /// </remarks>
    public string InputData { get; set; } = "{}";
    
    /// <summary>
    /// Gets or sets the output data produced by this node in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Preserves execution results for debugging, audit purposes, and data lineage tracking,
    /// enabling comprehensive analysis of workflow data transformations and AI model outputs.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Stored as JSON to accommodate varying output schemas across different node types.
    /// Null for failed executions or nodes that don't produce output.
    /// </summary>
    /// <value>
    /// A nullable JSON string containing the output data produced by the node,
    /// or null if no output was generated or execution failed.
    /// </value>
    /// <remarks>
    /// Essential for debugging downstream node failures and analyzing data flow.
    /// Large outputs may require compression or external storage strategies.
    /// </remarks>
    public string? OutputData { get; set; }
    
    /// <summary>
    /// Gets or sets the current execution status of this node.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides real-time visibility into workflow execution progress and enables
    /// automated monitoring, alerting, and recovery mechanisms in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Status transitions follow a defined state machine pattern. Terminal states
    /// include Completed, Failed, Cancelled, TimedOut, and Skipped.
    /// </summary>
    /// <value>
    /// A <see cref="NodeExecutionStatus"/> enum value indicating the current execution state.
    /// Defaults to Pending.
    /// </value>
    /// <remarks>
    /// Status changes should be atomic and logged for audit purposes.
    /// Used by monitoring systems for real-time execution tracking.
    /// </remarks>
    public NodeExecutionStatus Status { get; set; } = NodeExecutionStatus.Pending;
    
    /// <summary>
    /// Gets or sets the error message if execution failed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides immediate diagnostic information for troubleshooting failed executions,
    /// enabling rapid problem resolution and reducing mean time to recovery (MTTR)
    /// in enterprise production environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Contains user-friendly error descriptions suitable for display in monitoring dashboards.
    /// Should not contain sensitive information or internal system details.
    /// </summary>
    /// <value>
    /// A string containing the error message, or null if execution was successful.
    /// </value>
    /// <remarks>
    /// Used for high-level error reporting and user notifications.
    /// Detailed technical information should be stored in ErrorStackTrace.
    /// </remarks>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the detailed stack trace for debugging execution failures.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides detailed technical information for developers and system administrators
    /// to diagnose and resolve complex execution failures in enterprise workflow environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Contains full exception stack trace including call hierarchy and technical details.
    /// May contain sensitive system information - access should be restricted appropriately.
    /// </summary>
    /// <value>
    /// A string containing the full exception stack trace, or null if execution was successful.
    /// </value>
    /// <remarks>
    /// Contains detailed technical information for debugging purposes.
    /// Access should be restricted to authorized technical personnel.
    /// </remarks>
    public string? ErrorStackTrace { get; set; }
    
    /// <summary>
    /// Gets or sets the number of retry attempts made for this node execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks retry behavior for reliability analysis and helps identify nodes
    /// that frequently require retries, indicating potential reliability issues
    /// or suboptimal configuration in enterprise environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Incremented each time the node execution is retried after a failure.
    /// Used in conjunction with node-level MaxRetries configuration.
    /// </summary>
    /// <value>
    /// An integer representing the number of retry attempts. Defaults to 0.
    /// </value>
    /// <remarks>
    /// Used for reliability analysis and identifying problematic nodes.
    /// High retry counts may indicate configuration or environmental issues.
    /// </remarks>
    public int RetryCount { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the monetary cost incurred by this node execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables precise cost tracking and optimization for AI model usage,
    /// supporting enterprise cost management, budgeting, and ROI analysis
    /// for LLMOps deployments with usage-based pricing models.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Calculated based on token usage, model pricing, and execution duration.
    /// Expressed in the system's base currency for consistency.
    /// </summary>
    /// <value>
    /// A nullable decimal representing the execution cost in the base currency,
    /// or null if cost calculation is not applicable for this node type.
    /// </value>
    /// <remarks>
    /// Critical for enterprise cost management and optimization.
    /// Used for billing calculations and budget tracking.
    /// </remarks>
    public decimal? Cost { get; set; }
    
    /// <summary>
    /// Gets or sets the number of tokens consumed by AI model calls during execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides granular usage tracking for AI model consumption, enabling
    /// precise cost calculation, usage optimization, and capacity planning
    /// in enterprise LLMOps environments with token-based billing.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Includes both input and output tokens for complete usage tracking.
    /// Null for nodes that don't use AI models (e.g., data transformation nodes).
    /// </summary>
    /// <value>
    /// A nullable integer representing the total token usage,
    /// or null if the node doesn't use AI models.
    /// </value>
    /// <remarks>
    /// Essential for cost calculation and usage optimization.
    /// Used for capacity planning and performance analysis.
    /// </remarks>
    public int? TokensUsed { get; set; }
    
    /// <summary>
    /// Gets or sets the AI provider used for this node execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables provider-specific performance analysis and cost optimization,
    /// supporting multi-provider strategies and vendor management in enterprise
    /// environments with diverse AI service requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Common values include "OpenAI", "Anthropic", "Azure", "AWS", "Google".
    /// Used for provider-specific billing and performance analytics.
    /// </summary>
    /// <value>
    /// A string identifying the AI provider, or null if no AI provider was used.
    /// Maximum length is 50 characters.
    /// </value>
    /// <remarks>
    /// Used for provider-specific analysis and cost optimization.
    /// Enables multi-provider performance comparison and vendor management.
    /// </remarks>
    [StringLength(50)]
    public string? AiProvider { get; set; }
    
    /// <summary>
    /// Gets or sets the specific AI model used during node execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables model-specific performance analysis and optimization, supporting
    /// enterprises in selecting optimal models for different use cases and
    /// balancing cost, performance, and quality requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Examples include "gpt-4", "gpt-3.5-turbo", "claude-2", "palm-2".
    /// Used for model-specific billing calculations and performance metrics.
    /// </summary>
    /// <value>
    /// A string identifying the AI model, or null if no AI model was used.
    /// Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Critical for model-specific performance analysis and cost optimization.
    /// Enables comparison of different models for similar use cases.
    /// </remarks>
    [StringLength(100)]
    public string? AiModel { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the prompt template used during execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Establishes critical traceability between workflow executions and prompt templates,
    /// enabling template performance analysis, A/B testing, and optimization strategies
    /// in enterprise prompt management environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with PromptTemplate entity. Essential for
    /// linking execution results to specific prompt configurations.
    /// </summary>
    /// <value>
    /// A nullable <see cref="Guid"/> representing the prompt template identifier,
    /// or null if the node doesn't use a prompt template.
    /// </value>
    /// <remarks>
    /// Critical for template performance analysis and optimization.
    /// Enables A/B testing and template effectiveness measurement.
    /// </remarks>
    public Guid? PromptTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the associated prompt template.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to template content and metadata for execution analysis
    /// and debugging without requiring separate database queries.
    /// </summary>
    /// <value>
    /// A <see cref="PromptTemplate"/> instance, or null if no template was used.
    /// </value>
    public virtual PromptTemplate? PromptTemplate { get; set; }
    
    /// <summary>
    /// Gets or sets the version of the prompt template when it was used for execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Captures template version for audit trails and rollback capabilities,
    /// enabling enterprises to track template evolution impact on execution
    /// performance and quality metrics over time.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Snapshot of template version at execution time. Critical for understanding
    /// which template version produced specific results.
    /// </summary>
    /// <value>
    /// A string representing the template version (e.g., "1.0", "2.3-beta"),
    /// or null if versioning is not tracked or no template was used.
    /// Maximum length is 20 characters.
    /// </value>
    /// <remarks>
    /// Essential for template version impact analysis and rollback scenarios.
    /// Enables correlation between template changes and execution quality.
    /// </remarks>
    [StringLength(20)]
    public string? TemplateVersion { get; set; }
    
    /// <summary>
    /// Gets or sets the quality score for this node execution result.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides quantitative quality assessment for execution outputs, enabling
    /// automated quality monitoring, SLA compliance tracking, and optimization
    /// strategies in enterprise LLMOps environments requiring quality assurance.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Normalized score between 0.0 (lowest quality) and 1.0 (highest quality).
    /// Calculated using quality assessment algorithms or human feedback.
    /// </summary>
    /// <value>
    /// A nullable decimal representing the quality score (0.0 to 1.0),
    /// or null if quality assessment is not available or applicable.
    /// </value>
    /// <remarks>
    /// Used for quality trending analysis and automated quality gates.
    /// May be calculated post-execution through quality assessment pipelines.
    /// </remarks>
    public decimal? QualityScore { get; set; }
    
    /// <summary>
    /// Gets or sets the confidence level of the execution result.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Indicates the system's confidence in the execution result quality,
    /// enabling automated decision-making, quality filtering, and human review
    /// triggers in enterprise workflows requiring reliability assurance.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Normalized confidence level between 0.0 (no confidence) and 1.0 (maximum confidence).
    /// May be provided by AI models or calculated from execution characteristics.
    /// </summary>
    /// <value>
    /// A nullable decimal representing the confidence level (0.0 to 1.0),
    /// or null if confidence assessment is not available.
    /// </value>
    /// <remarks>
    /// Used for automated quality gates and human review triggers.
    /// Low confidence scores may indicate need for manual review.
    /// </remarks>
    public decimal? ConfidenceLevel { get; set; }
    
    /// <summary>
    /// Gets or sets performance metrics collected during execution in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Captures detailed performance characteristics for analysis and optimization,
    /// enabling enterprises to identify performance bottlenecks, resource consumption
    /// patterns, and optimization opportunities across their workflow portfolio.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON object containing metrics like memory usage, CPU time, API response times,
    /// cache hit rates, and other performance indicators specific to the node type.
    /// </summary>
    /// <value>
    /// A nullable JSON string containing performance metrics,
    /// or null if performance tracking is not enabled.
    /// </value>
    /// <example>
    /// {"memoryUsageMB": 45.2, "cpuTimeMs": 234, "apiResponseTimeMs": 1250, "cacheHitRate": 0.85}
    /// </example>
    /// <remarks>
    /// Used for performance analysis and capacity planning.
    /// Structure varies by node type and enabled performance tracking features.
    /// </remarks>
    public string? PerformanceMetrics { get; set; }
    
    /// <summary>
    /// Gets or sets debug information collected during execution in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides detailed execution context and diagnostic information for
    /// troubleshooting complex workflow issues and understanding execution
    /// behavior in enterprise debugging and support scenarios.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON object containing debug-specific information like variable values,
    /// intermediate results, decision points, and execution path details.
    /// </summary>
    /// <value>
    /// A nullable JSON string containing debug information,
    /// or null if debug tracking is not enabled.
    /// </value>
    /// <example>
    /// {"variables": {"user_id": "123"}, "decisions": [{"condition": "x > 5", "result": true}]}
    /// </example>
    /// <remarks>
    /// May contain sensitive information - access should be restricted appropriately.
    /// Used primarily for debugging and development purposes.
    /// </remarks>
    public string? DebugInfo { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether this execution result was served from cache.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks cache effectiveness and performance optimization opportunities,
    /// enabling enterprises to measure cache hit rates and optimize caching
    /// strategies for improved performance and cost reduction.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// True when execution result was retrieved from cache rather than computed.
    /// Used for cache performance analysis and optimization.
    /// </summary>
    /// <value>
    /// <c>true</c> if the result was served from cache; otherwise, <c>false</c>.
    /// Default is <c>false</c>.
    /// </value>
    /// <remarks>
    /// Used for cache performance analysis and cost optimization.
    /// Cache hits typically have lower cost and faster execution times.
    /// </remarks>
    public bool CacheHit { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the cache key used for this execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables cache analysis and debugging by providing visibility into
    /// cache key strategies and cache effectiveness patterns in enterprise
    /// environments with sophisticated caching requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// String representation of the cache key used for storing or retrieving
    /// the execution result. Null if caching is not used for this execution.
    /// </summary>
    /// <value>
    /// A string representing the cache key, or null if caching was not used.
    /// Maximum length is 256 characters.
    /// </value>
    /// <remarks>
    /// Used for cache debugging and analysis of cache key distribution.
    /// Helps identify cache key patterns and potential improvements.
    /// </remarks>
    [StringLength(256)]
    public string? CacheKey { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the edge traversed to reach this node.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks the execution path through the workflow, enabling path analysis,
    /// performance optimization, and understanding of conditional execution
    /// patterns in complex enterprise workflow environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with FlowEdge entity. Represents the specific
    /// edge that was traversed to reach this node during execution.
    /// </summary>
    /// <value>
    /// A nullable <see cref="Guid"/> representing the incoming edge identifier,
    /// or null if this is the starting node of the workflow.
    /// </value>
    /// <remarks>
    /// Used for execution path analysis and workflow optimization.
    /// Null for workflow entry points that don't have incoming edges.
    /// </remarks>
    public Guid? IncomingEdgeId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the edge traversed to reach this node.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to edge configuration and metadata for execution
    /// path analysis without requiring separate database queries.
    /// </summary>
    /// <value>
    /// A <see cref="FlowEdge"/> instance representing the incoming edge,
    /// or null if this is the starting node.
    /// </value>
    public virtual FlowEdge? IncomingEdge { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of edge traversals originating from this node execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks all outgoing execution paths from this node, enabling comprehensive
    /// workflow analysis, parallel execution tracking, and conditional logic
    /// analysis in enterprise workflow environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Collection of EdgeTraversal entities representing each outgoing path
    /// taken during execution. Multiple traversals indicate parallel or conditional paths.
    /// </summary>
    /// <value>
    /// A collection of <see cref="EdgeTraversal"/> entities representing outgoing paths.
    /// </value>
    /// <remarks>
    /// Used for complete execution path reconstruction and analysis.
    /// Empty collection indicates workflow termination or execution failure.
    /// </remarks>
    public virtual ICollection<EdgeTraversal> OutgoingTraversals { get; set; } = [];
}
