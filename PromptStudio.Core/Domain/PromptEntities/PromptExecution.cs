using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a single execution instance of a prompt template with comprehensive LLMOps tracking.
/// 
/// <para><strong>Business Context:</strong></para>
/// PromptExecution serves as the fundamental audit and analytics unit in enterprise LLMOps,
/// capturing every interaction with language models for compliance, optimization, cost management,
/// and quality assurance. Each execution provides detailed tracking of request parameters,
/// responses, performance metrics, costs, and quality assessments essential for data-driven
/// prompt engineering and AI operations management.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity provides comprehensive execution tracking with variable resolution, provider
/// and model identification, performance metrics, error handling, and quality assessment.
/// It supports multi-tenant environments with audit trails, soft deletion, and optimistic
/// concurrency control for enterprise-scale AI operations with full traceability.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Complete audit trail for AI interactions and compliance requirements
/// - Detailed cost tracking and budget management for AI operations
/// - Performance monitoring and SLA compliance validation
/// - Quality assessment and continuous improvement analytics
/// - Variable tracking for systematic testing and optimization
/// - Provider and model comparison for strategic decision-making
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Audit Trail: Comprehensive tracking of all execution details and metadata
/// - Performance Monitoring: Response time, token usage, and cost tracking
/// - Quality Assessment: Automated scoring and continuous improvement feedback
/// - Multi-tenancy: Tenant isolation through AuditableEntity inheritance
/// - Event Sourcing: Complete execution history for analytics and compliance
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index ExecutedAt for time-series queries and reporting performance
/// - Consider partitioning by date or tenant for large-scale deployments
/// - Response text may require compression or external storage for large outputs
/// - Quality scores should be calculated asynchronously to avoid execution delays
/// - Variable values stored as JSON for flexible querying and analysis
/// 
/// <para><strong>Integration Points:</strong></para>
/// - AI Providers: Multi-provider support with standardized metrics collection
/// - Cost Management: Integration with billing and budget management systems
/// - Quality Systems: Automated quality assessment and scoring algorithms
/// - Analytics Platform: Time-series analysis and performance dashboards
/// - Compliance Systems: Audit trail generation and regulatory reporting
/// - Template Engine: Execution history for template optimization and A/B testing
/// </remarks>
/// <example>
/// <code>
/// // Creating a new prompt execution record
/// var execution = new PromptExecution
/// {
///     PromptTemplateId = templateId,
///     ResolvedPrompt = "Generate a customer response for billing inquiry...",
///     VariableValues = JsonSerializer.Serialize(new 
///     { 
///         customer_name = "John Doe", 
///         issue_type = "billing",
///         priority = "high" 
///     }),
///     AiProvider = "openai",
///     Model = "gpt-4",
///     Response = "Thank you for contacting us about your billing inquiry...",
///     ResponseTimeMs = 1250,
///     TokensUsed = 450,
///     Cost = 0.0125m,
///     Status = ExecutionStatus.Success,
///     QualityScore = 0.85m,
///     ExecutedBy = "user@company.com",
///     ExecutionContext = "web-ui",
///     TenantId = currentTenantId
/// };
/// 
/// // Recording execution metrics for analytics
/// await executionService.RecordAsync(execution);
/// 
/// // Analyzing execution performance
/// var metrics = await analyticsService.GetExecutionMetricsAsync(
///     templateId, DateTimeRange.LastDays(7));
/// </code>
/// </example>
public class PromptExecution : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the prompt template used for this execution.
    /// Links this execution to the specific template version and configuration used.
    /// </summary>
    /// <value>The GUID of the PromptTemplate that was executed.</value>
    /// <remarks>
    /// Template linkage is critical for performance analysis, A/B testing,
    /// and understanding the impact of template changes on execution outcomes.
    /// </remarks>
    public Guid PromptTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets the final prompt text after all variable substitutions have been applied.
    /// Represents the exact text that was sent to the language model for processing.
    /// </summary>
    /// <value>
    /// The complete, resolved prompt text with all variables replaced by actual values.
    /// Required field as it represents the core input to the LLM execution.
    /// </value>
    /// <remarks>
    /// Storing the resolved prompt enables exact reproduction of executions,
    /// debugging of variable substitution issues, and compliance auditing
    /// of actual content sent to external AI providers.
    /// </remarks>
    [Required]
    public string ResolvedPrompt { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the variable values used for template substitution in JSON format.
    /// Contains the key-value pairs that were used to resolve template variables.
    /// </summary>
    /// <value>
    /// A JSON string containing variable names and their corresponding values used in this execution.
    /// Null if the template contained no variables or if variable tracking is disabled.
    /// </value>
    /// <example>
    /// {"customer_name": "John Doe", "issue_type": "billing", "priority": "high", "language": "en"}
    /// </example>
    /// <remarks>
    /// Variable value tracking enables analysis of input patterns, performance correlation
    /// with specific inputs, and reproduction of execution scenarios for testing.
    /// </remarks>
    public string? VariableValues { get; set; }
    
    /// <summary>
    /// Gets or sets the AI provider used for this execution.
    /// Identifies the specific LLM service provider that processed the request.
    /// </summary>
    /// <value>
    /// The provider identifier (e.g., "openai", "anthropic", "azure-openai", "bedrock").
    /// Optional field with maximum length of 50 characters.
    /// </value>
    /// <example>
    /// Examples: "openai", "anthropic", "azure-openai", "bedrock", "huggingface", "cohere"
    /// </example>
    /// <remarks>
    /// Provider tracking enables cost analysis, performance comparison across providers,
    /// and strategic decisions about provider selection and optimization.
    /// </remarks>
    [StringLength(50)]
    public string? AiProvider { get; set; }
    
    /// <summary>
    /// Gets or sets the specific model used for this execution.
    /// Identifies the exact language model that generated the response.
    /// </summary>
    /// <value>
    /// The model identifier (e.g., "gpt-4", "gpt-3.5-turbo", "claude-3-opus").
    /// Optional field with maximum length of 50 characters.
    /// </value>
    /// <example>
    /// Examples: "gpt-4", "gpt-3.5-turbo", "claude-3-opus", "claude-3-sonnet", "llama-2-70b"
    /// </example>
    /// <remarks>
    /// Model tracking is essential for performance analysis, cost optimization,
    /// and understanding the capabilities and limitations of different models.
    /// </remarks>
    [StringLength(50)]
    public string? Model { get; set; }
    
    /// <summary>
    /// Gets or sets the response generated by the AI provider.
    /// Contains the complete output from the language model for this execution.
    /// </summary>
    /// <value>
    /// The full text response generated by the AI model. Can be null if execution failed
    /// or if response storage is disabled for privacy or storage optimization reasons.
    /// </value>
    /// <remarks>
    /// Response storage enables quality analysis, content auditing, and customer support.
    /// For sensitive applications, responses may be truncated, hashed, or omitted entirely
    /// while preserving metadata for analytics purposes.
    /// </remarks>
    public string? Response { get; set; }
    
    /// <summary>
    /// Gets or sets the time taken for the AI request in milliseconds.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables performance monitoring and SLA compliance tracking for AI interactions,
    /// supporting enterprise requirements for response time analysis, optimization
    /// initiatives, and service level agreement validation across different models and providers.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Measured from request initiation to response completion, including network latency.
    /// Used for performance analytics, alerting, and capacity planning decisions.
    /// </summary>
    /// <value>
    /// The response time in milliseconds, or null if timing information is not available.
    /// Includes all overhead from request preparation to response parsing.
    /// </value>
    /// <remarks>
    /// Critical for SLA monitoring and performance optimization.
    /// High response times may indicate network issues, model capacity constraints, or complex prompts.
    /// </remarks>
    /// <example>
    /// Typical ranges: 200-2000ms for simple prompts, 2000-10000ms for complex generations
    /// </example>
    public int? ResponseTimeMs { get; set; }
    
    /// <summary>
    /// Gets or sets the number of tokens used in the request processing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides precise usage tracking for cost calculation and capacity planning,
    /// enabling accurate billing, budget management, and usage optimization
    /// in enterprise LLMOps environments with token-based pricing models.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Includes both input and output tokens when available from the AI provider.
    /// Token counts vary by model and tokenization method used by each provider.
    /// </summary>
    /// <value>
    /// The total number of tokens consumed during request processing,
    /// or null if token information is not provided by the AI service.
    /// </value>
    /// <remarks>
    /// Essential for cost calculation and usage analytics.
    /// Different models have different token limits and pricing structures.
    /// </remarks>
    /// <example>
    /// Typical ranges: 50-500 tokens for simple requests, 1000-4000 tokens for complex prompts
    /// </example>
    public int? TokensUsed { get; set; }
    
    /// <summary>
    /// Gets or sets the monetary cost of the AI request.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables precise cost tracking and budget management for AI operations,
    /// supporting enterprise financial controls, cost optimization initiatives,
    /// and ROI analysis for prompt engineering investments and model selection decisions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Calculated based on provider pricing models, token usage, and request parameters.
    /// May include additional fees for premium features or specialized model access.
    /// </summary>
    /// <value>
    /// The cost in the configured currency (typically USD), or null if cost information is not available.
    /// Represents the actual amount charged by the AI provider for this specific request.
    /// </value>
    /// <remarks>
    /// Critical for financial reporting and cost optimization analysis.
    /// Costs vary significantly between providers, models, and request complexity.
    /// </remarks>
    /// <example>
    /// Typical ranges: $0.001-$0.01 for simple requests, $0.01-$0.10 for complex generations
    /// </example>
    public decimal? Cost { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when this execution was performed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides precise timing for audit trails, performance analysis, and compliance reporting,
    /// enabling temporal analysis of usage patterns, peak load identification,
    /// and correlation with business events for optimization and planning purposes.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// UTC timestamp set at execution initiation for consistent timezone handling.
    /// Used for time-series analysis, reporting, and audit trail construction.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> representing when the execution occurred in UTC.
    /// Defaults to the current UTC time when the execution record is created.
    /// </value>
    /// <remarks>
    /// Essential for audit trails and temporal analysis of usage patterns.
    /// All timestamps stored in UTC for consistent multi-timezone operations.
    /// </remarks>
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the execution status indicating success, failure, or other states.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables reliability monitoring and error tracking for AI operations,
    /// supporting enterprise quality assurance, SLA compliance, and operational
    /// excellence initiatives with comprehensive execution state management.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based status tracking with predefined states for operational clarity.
    /// Used for filtering, alerting, and success rate calculations in analytics.
    /// </summary>
    /// <value>
    /// An <see cref="ExecutionStatus"/> enum value indicating the execution outcome.
    /// Default is Success for completed executions.
    /// </value>
    /// <remarks>
    /// Critical for reliability monitoring and error analysis.
    /// Failed executions should include detailed error information for debugging.
    /// </remarks>
    public ExecutionStatus Status { get; set; } = ExecutionStatus.Success;
    
    /// <summary>
    /// Gets or sets the error message if execution failed.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides detailed error information for troubleshooting and quality improvement,
    /// supporting enterprise debugging workflows, root cause analysis,
    /// and continuous improvement of prompt reliability and system robustness.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Contains detailed error descriptions from AI providers or internal processing.
    /// Should include sufficient detail for debugging without exposing sensitive information.
    /// </summary>
    /// <value>
    /// A detailed error message describing what went wrong during execution,
    /// or null if the execution was successful.
    /// </value>
    /// <remarks>
    /// Essential for debugging and quality improvement initiatives.
    /// Should be sanitized to avoid exposing sensitive data in error messages.
    /// </remarks>
    /// <example>
    /// Examples: "Rate limit exceeded", "Invalid API key", "Model not available", "Timeout occurred"
    /// </example>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the quality score for this execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables automated quality assessment and continuous improvement tracking,
    /// supporting enterprise quality assurance programs, A/B testing validation,
    /// and data-driven optimization of prompt engineering efforts.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Normalized score between 0.0 and 1.0 calculated by quality assessment algorithms.
    /// May be derived from automated analysis, user feedback, or business metrics.
    /// </summary>
    /// <value>
    /// A decimal value between 0.0 (lowest quality) and 1.0 (highest quality),
    /// or null if quality assessment has not been performed.
    /// </value>
    /// <remarks>
    /// Used for quality trend analysis and automated optimization decisions.
    /// Quality scores enable systematic improvement of prompt performance over time.
    /// </remarks>
    /// <example>
    /// Quality ranges: 0.0-0.3 (poor), 0.3-0.7 (acceptable), 0.7-1.0 (excellent)
    /// </example>
    public decimal? QualityScore { get; set; }
    
    /// <summary>
    /// Gets or sets the identifier of the user or system that initiated this execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables accountability tracking and usage attribution for enterprise governance,
    /// supporting audit requirements, user behavior analysis, and resource allocation
    /// decisions based on actual usage patterns and user-specific performance metrics.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// User identifier from the organization's identity system or system component name.
    /// Used for access control verification and usage analytics by user or system.
    /// </summary>
    /// <value>
    /// A string identifying the executing user or system component.
    /// Can be null for anonymous or system-initiated executions. Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Critical for audit trails and user-specific analytics.
    /// Should follow organizational identity standards for consistency.
    /// </remarks>
    /// <example>
    /// Examples: "user@company.com", "api-client-123", "batch-processor", "system"
    /// </example>
    [StringLength(100)]
    public string? ExecutedBy { get; set; }
    
    /// <summary>
    /// Gets or sets the execution context identifying the source or environment of the request.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables context-aware analytics and optimization by distinguishing between different
    /// usage scenarios, supporting targeted improvements and context-specific performance
    /// monitoring across web interfaces, APIs, batch processing, and testing environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Categorizes executions by source system or operational context.
    /// Used for filtering analytics and applying context-specific optimization strategies.
    /// </summary>
    /// <value>
    /// A string identifying the execution context or source system.
    /// Can be null if context tracking is not enabled. Maximum length is 50 characters.
    /// </value>
    /// <remarks>
    /// Enables context-aware performance analysis and optimization.
    /// Different contexts may have different performance characteristics and requirements.
    /// </remarks>
    /// <example>
    /// Examples: "web-ui", "rest-api", "batch-job", "unit-test", "integration-test", "mobile-app"
    /// </example>
    [StringLength(50)]
    public string? ExecutionContext { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the variable collection used for this execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links executions to specific variable datasets for batch processing analysis,
    /// enabling systematic testing, performance comparison across variable sets,
    /// and data-driven optimization of variable collection effectiveness.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional foreign key reference to VariableCollection entity.
    /// Used for batch execution tracking and variable set performance analysis.
    /// </summary>
    /// <value>
    /// The GUID of the VariableCollection used for this execution,
    /// or null if executed with individual variables or no variable collection.
    /// </value>
    /// <remarks>
    /// Enables batch execution analysis and variable collection optimization.
    /// Particularly useful for A/B testing and systematic prompt evaluation.
    /// </remarks>
    public Guid? VariableCollectionId { get; set; }
    
    // Navigation properties
    /// <summary>
    /// Gets or sets the navigation property to the executed prompt template.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides direct access to template metadata and configuration for analysis
    /// without requiring separate database queries, enabling efficient reporting and analytics.
    /// </summary>
    /// <value>
    /// The <see cref="PromptTemplate"/> entity that was executed.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Enables efficient querying of template information during execution analysis.
    /// </remarks>
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the navigation property to the variable collection used in this execution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to variable collection metadata and values for comprehensive
    /// execution analysis and batch processing performance evaluation.
    /// </summary>
    /// <value>
    /// The <see cref="VariableCollection"/> entity used for this execution,
    /// or null if no variable collection was used.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework Core.
    /// Supports analysis of variable collection impact on execution performance.
    /// </remarks>
    public virtual VariableCollection? VariableCollection { get; set; }
}
