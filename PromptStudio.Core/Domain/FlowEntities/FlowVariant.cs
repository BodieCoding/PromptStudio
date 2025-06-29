using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a variant of a PromptFlow for A/B testing, optimization experiments, and version comparison.
/// 
/// <para><strong>Business Context:</strong></para>
/// FlowVariants enable data-driven optimization of prompt workflows through controlled experimentation
/// and performance comparison. Organizations can test different workflow configurations, prompt templates,
/// node arrangements, and parameters to identify optimal configurations while maintaining
/// proper experiment design and statistical significance.
/// 
/// <para><strong>Technical Context:</strong></para>
/// FlowVariant extends the base PromptFlow with experimental configuration and tracking capabilities,
/// supporting A/B testing frameworks, traffic splitting, and performance analytics through
/// enterprise audit trails and multi-tenant isolation.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Data-driven workflow optimization through controlled experiments
/// - Performance comparison across different flow configurations
/// - Scientific approach to prompt engineering improvements
/// - Statistical significance tracking and results analysis
/// - Enterprise-grade experiment management and governance
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Variant Pattern: Controlled modifications of base workflows for testing
/// - Audit Trail: Complete tracking of experiment changes and configurations
/// - Multi-tenancy: Organizational isolation for experiment security
/// - Soft Delete: Experiment history preservation for analysis
/// 
/// <para><strong>Experiment Design:</strong></para>
/// - Traffic allocation controls exposure percentages
/// - Change tracking documents specific modifications
/// - Performance metrics enable statistical comparison
/// - Rollback capabilities ensure experiment safety
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index on BaseFlowId for variant enumeration
/// - JSON fields for flexible experiment configuration
/// - Consider caching active variants for traffic routing
/// - Monitor variant performance impact on base flow
/// 
/// <para><strong>Integration Points:</strong></para>
/// - A/B Testing Framework: Experiment design and execution
/// - Analytics Platform: Performance metrics and statistical analysis
/// - Traffic Router: Variant selection and load balancing
/// - Monitoring System: Experiment health and performance tracking
/// </remarks>
/// <example>
/// <code>
/// // Creating a flow variant for optimization testing
/// var variant = new FlowVariant
/// {
///     BaseFlowId = originalFlow.Id,
///     VariantName = "Optimized Customer Service v2",
///     Description = "Enhanced flow with improved sentiment analysis and faster response generation",
///     Changes = JsonSerializer.Serialize(new {
///         modified_nodes = new[] { "sentiment-node", "response-generator" },
///         updated_prompts = new[] { "greeting-template", "escalation-template" },
///         parameter_changes = new { temperature = 0.7, max_tokens = 150 }
///     }),
///     TrafficAllocation = 25.0, // 25% of traffic
///     IsActive = true,
///     OrganizationId = currentTenantId
/// };
/// 
/// await variantService.CreateAsync(variant);
/// </code>
/// </example>
public class FlowVariant : AuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier of the base PromptFlow this variant is derived from.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Establishes the relationship between the variant and its source workflow,
    /// enabling proper experiment design, change tracking, and performance comparison
    /// against the baseline configuration.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key reference to the source PromptFlow entity.
    /// Required for all variant relationships and experiment tracking.
    /// </summary>
    /// <value>
    /// A valid Guid that references an existing PromptFlow.
    /// Cannot be empty or default Guid value.
    /// </value>
    /// <remarks>
    /// Used in experiment queries and performance comparison analytics.
    /// Essential for understanding experiment relationships and inheritance.
    /// </remarks>
    public Guid BaseFlowId { get; set; }

    /// <summary>
    /// Gets or sets the base PromptFlow navigation property.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to the source workflow configuration and properties
    /// for comparison, inheritance, and experiment analysis.
    /// </summary>
    /// <value>
    /// The PromptFlow entity this variant is based on.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual PromptFlow BaseFlow { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the descriptive name of this flow variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides clear identification for experiment tracking, reporting,
    /// and team communication about different workflow configurations
    /// being tested in optimization experiments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Required field with maximum length for UI and reporting compatibility.
    /// Used in experiment dashboards, analytics, and administrative interfaces.
    /// </summary>
    /// <value>
    /// A descriptive name for the variant experiment.
    /// Cannot be null or empty. Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Should clearly indicate the experiment hypothesis or optimization goal.
    /// Used extensively in analytics reports and experiment tracking.
    /// </remarks>
    /// <example>
    /// Examples: "High Temperature Config", "Streamlined Response Flow", "Enhanced Sentiment Analysis"
    /// </example>
    [Required]
    [StringLength(100)]
    public string VariantName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a detailed description of the variant's purpose and expected improvements.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Documents the experiment hypothesis, expected outcomes, and specific
    /// improvements being tested, supporting scientific experiment design
    /// and result interpretation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional field with generous length for comprehensive documentation.
    /// Used in experiment reports and team collaboration.
    /// </summary>
    /// <value>
    /// A detailed description of the variant's purpose and hypothesis.
    /// Can be empty. Maximum length is 500 characters.
    /// </value>
    /// <remarks>
    /// Should explain what changes were made and why.
    /// Critical for experiment result interpretation and learning.
    /// </remarks>
    /// <example>
    /// "Tests improved response quality by increasing temperature to 0.8 and adding sentiment analysis pre-processing step"
    /// </example>
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a JSON description of the specific changes made in this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides detailed tracking of experiment modifications for reproducibility,
    /// change analysis, and scientific validation of experiment results,
    /// supporting data-driven optimization decisions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON-formatted string containing structured change documentation.
    /// Used for change analysis, experiment reproducibility, and automated processing.
    /// </summary>
    /// <value>
    /// A JSON string documenting specific changes made to the base flow.
    /// Default is empty JSON object "{}".
    /// </value>
    /// <remarks>
    /// Should include modified nodes, parameter changes, template updates, etc.
    /// Critical for understanding experiment scope and impact analysis.
    /// </remarks>
    /// <example>
    /// {"modified_nodes": ["response-gen"], "parameters": {"temperature": 0.8}, "new_templates": ["enhanced-greeting"]}
    /// </example>
    public string Changes { get; set; } = "{}";
    
    /// <summary>
    /// Gets or sets the complete variant configuration and modifications as JSON.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Stores the full variant configuration for experiment execution,
    /// enabling complete workflow reconstruction and automated experiment
    /// deployment across different environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON-formatted string containing complete variant configuration.
    /// Used by workflow execution engine for variant-specific behavior.
    /// </summary>
    /// <value>
    /// A JSON string containing the complete variant configuration.
    /// Default is empty JSON object "{}".
    /// </value>
    /// <remarks>
    /// Contains all configuration needed to execute the variant.
    /// Used by runtime systems for variant behavior implementation.
    /// </remarks>
    /// <example>
    /// {"nodes": {...}, "edges": [...], "parameters": {...}, "templates": {...}}
    /// </example>
    public string VariantData { get; set; } = "{}";

    /// <summary>
    /// Gets or sets whether this variant is currently active in A/B testing experiments.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls experiment participation and traffic allocation, enabling
    /// dynamic experiment management, gradual rollouts, and emergency
    /// deactivation for problematic variants.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag controlling variant participation in traffic routing.
    /// Used by experiment engines for variant selection and load balancing.
    /// </summary>
    /// <value>
    /// True if the variant should receive experiment traffic, false otherwise.
    /// Default is true for newly created variants.
    /// </value>
    /// <remarks>
    /// Can be toggled for experiment control without deleting variant data.
    /// Essential for experiment safety and rollback capabilities.
    /// </remarks>
    /// <example>
    /// variant.VariantIsActive = false; // Disable problematic variant
    /// </example>
    public bool VariantIsActive { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the percentage of traffic allocated to this variant for A/B testing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls experiment exposure and risk management, enabling gradual rollouts,
    /// statistical power optimization, and business impact control through
    /// precise traffic allocation across variant populations.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Decimal percentage value between 0 and 100 for traffic routing.
    /// Used by traffic splitting algorithms and experiment frameworks.
    /// </summary>
    /// <value>
    /// Percentage value between 0.0 and 100.0 representing traffic allocation.
    /// Default is 0.0 for newly created variants.
    /// </value>
    /// <remarks>
    /// Total allocation across all variants should typically sum to ≤100%.
    /// Used by load balancers and experiment routing systems.
    /// </remarks>
    /// <example>
    /// variant.TrafficPercentage = 25.0; // Allocate 25% of traffic
    /// </example>
    public double TrafficPercentage { get; set; } = 0.0;
    
    /// <summary>
    /// Gets or sets the priority level for variant selection in traffic routing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables sophisticated experiment management with priority-based
    /// variant selection, canary deployments, and controlled rollout
    /// strategies for risk management and performance optimization.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Integer priority value for variant ordering and selection logic.
    /// Higher values typically indicate higher priority in routing decisions.
    /// </summary>
    /// <value>
    /// Integer priority value for variant selection ordering.
    /// Default is 0 for newly created variants.
    /// </value>
    /// <remarks>
    /// Used in combination with traffic percentage for sophisticated routing.
    /// Enables priority-based fallback and canary deployment patterns.
    /// </remarks>
    /// <example>
    /// variant.Priority = 10; // High priority for canary deployment
    /// </example>
    public int Priority { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the total number of executions for this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks experiment sample size for statistical significance analysis,
    /// performance monitoring, and experiment completion assessment,
    /// supporting data-driven decision making and experiment validation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Incrementing counter updated on each variant execution.
    /// Used for statistical analysis and performance metrics calculation.
    /// </summary>
    /// <value>
    /// Non-negative integer count of variant executions.
    /// Default is 0 for newly created variants.
    /// </value>
    /// <remarks>
    /// Critical for statistical significance calculations and sample size planning.
    /// Should be incremented atomically to ensure accuracy.
    /// </remarks>
    /// <example>
    /// if (variant.ExecutionCount >= minSampleSize) { /* Analyze results */ }
    /// </example>
    public long ExecutionCount { get; set; } = 0;

    /// <summary>
    /// Gets or sets the average cost per execution for this variant in the configured currency.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables cost-benefit analysis, budget optimization, and ROI calculation
    /// for different variant configurations, supporting financial decision
    /// making in LLM optimization and resource allocation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Decimal value representing average execution cost across all runs.
    /// Updated through cost tracking and execution monitoring systems.
    /// </summary>
    /// <value>
    /// Average cost per execution in the system's configured currency.
    /// Default is 0 for newly created variants.
    /// </value>
    /// <remarks>
    /// Should include all associated costs (LLM API, processing, storage).
    /// Used for cost optimization and budget planning analytics.
    /// </remarks>
    /// <example>
    /// // Higher quality variant may have higher costs
    /// if (variant.AverageCost > budgetThreshold) { /* Consider optimization */ }
    /// </example>
    public decimal AverageCost { get; set; } = 0;

    /// <summary>
    /// Gets or sets the average execution time in milliseconds for this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks performance characteristics for user experience optimization,
    /// SLA compliance monitoring, and performance-based variant selection,
    /// supporting quality of service and responsiveness goals.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Integer milliseconds representing average end-to-end execution time.
    /// Updated through execution monitoring and performance tracking systems.
    /// </summary>
    /// <value>
    /// Average execution time in milliseconds across all variant runs.
    /// Default is 0 for newly created variants.
    /// </value>
    /// <remarks>
    /// Should measure complete workflow execution including all processing steps.
    /// Critical for performance SLA monitoring and user experience optimization.
    /// </remarks>
    /// <example>
    /// if (variant.AverageExecutionTime > maxAcceptableLatency) { /* Alert or optimize */ }
    /// </example>
    public int AverageExecutionTime { get; set; } = 0;

    /// <summary>
    /// Gets or sets the overall quality score for outputs from this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enables quality-based variant comparison and optimization decisions,
    /// supporting output quality improvement and customer satisfaction
    /// goals through quantitative quality assessment.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional decimal score typically normalized to 0-1 or 0-100 scale.
    /// Calculated through quality evaluation frameworks and user feedback.
    /// </summary>
    /// <value>
    /// Quality score for variant outputs, typically 0-1 or 0-100 scale.
    /// Null if quality assessment has not been performed.
    /// </value>
    /// <remarks>
    /// Quality scoring methodology should be consistent across variants.
    /// May incorporate human evaluation, automated metrics, or user feedback.
    /// </remarks>
    /// <example>
    /// if (variant.QualityScore > 0.85m) { /* High quality variant */ }
    /// </example>
    public decimal? QualityScore { get; set; }

    /// <summary>
    /// Gets or sets the conversion rate for business objectives achieved by this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Measures business impact and effectiveness of different variant configurations,
    /// enabling ROI calculation and business-driven optimization decisions
    /// based on actual outcome achievement and goal completion.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional decimal rate typically expressed as percentage (0-1 scale).
    /// Calculated based on defined business objectives and success criteria.
    /// </summary>
    /// <value>
    /// Conversion rate as decimal percentage (0-1 scale) for business objectives.
    /// Null if conversion tracking has not been implemented or measured.
    /// </value>
    /// <remarks>
    /// Business objectives should be clearly defined and consistently measured.
    /// Critical for understanding real-world impact of variant optimizations.
    /// </remarks>
    /// <example>
    /// if (variant.ConversionRate > baselineConversionRate * 1.1m) { /* Significant improvement */ }
    /// </example>
    public decimal? ConversionRate { get; set; }

    /// <summary>
    /// Gets or sets the user satisfaction score for this variant based on feedback and ratings.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Captures user experience and satisfaction metrics for variant comparison,
    /// enabling user-centric optimization and experience improvement through
    /// direct feedback integration and satisfaction tracking.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional decimal score representing aggregated user satisfaction.
    /// Typically collected through ratings, surveys, or feedback mechanisms.
    /// </summary>
    /// <value>
    /// User satisfaction score, typically normalized to 0-1 or 1-5 scale.
    /// Null if user satisfaction has not been measured or collected.
    /// </value>
    /// <remarks>
    /// Should be based on consistent feedback collection methodology.
    /// Important for balancing technical performance with user experience.
    /// </remarks>
    /// <example>
    /// if (variant.UserSatisfactionScore >= 4.0m) { /* High user satisfaction */ }
    /// </example>
    public decimal? UserSatisfactionScore { get; set; }
    
    /// <summary>
    /// Gets or sets whether the variant's performance results are statistically significant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Indicates reliability and validity of experiment results for decision making,
    /// preventing premature conclusions and ensuring scientific rigor in
    /// optimization decisions and variant selection processes.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag set by statistical analysis algorithms.
    /// Based on sample size, effect size, and statistical testing results.
    /// </summary>
    /// <value>
    /// True if results meet statistical significance criteria, false otherwise.
    /// Default is false until sufficient data and analysis are available.
    /// </value>
    /// <remarks>
    /// Should only be set to true after proper statistical analysis.
    /// Critical for ensuring reliable experiment conclusions and decisions.
    /// </remarks>
    /// <example>
    /// if (variant.IsStatisticallySignificant && variant.QualityScore > baseline) { /* Valid improvement */ }
    /// </example>
    public bool IsStatisticallySignificant { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the confidence level for statistical significance testing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Quantifies certainty in experiment results and effect measurements,
    /// enabling risk-adjusted decision making and appropriate confidence
    /// assessment for variant optimization and deployment decisions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional decimal value between 0 and 1 representing confidence level.
    /// Typically 0.95 (95%) or 0.99 (99%) for business applications.
    /// </summary>
    /// <value>
    /// Confidence level as decimal between 0 and 1 (e.g., 0.95 for 95%).
    /// Null if statistical analysis has not been performed.
    /// </value>
    /// <remarks>
    /// Should align with organizational standards for statistical rigor.
    /// Higher confidence levels require larger sample sizes for significance.
    /// </remarks>
    /// <example>
    /// if (variant.ConfidenceLevel >= 0.95m) { /* High confidence in results */ }
    /// </example>
    public decimal? ConfidenceLevel { get; set; }
    
    /// <summary>
    /// Gets or sets the p-value from statistical hypothesis testing for this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides statistical evidence for variant performance differences,
    /// enabling scientific decision making and risk assessment in
    /// optimization choices and experiment result interpretation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional decimal p-value from statistical testing (typically 0-1).
    /// Lower values indicate stronger evidence against null hypothesis.
    /// </summary>
    /// <value>
    /// P-value from statistical testing, typically between 0 and 1.
    /// Null if statistical testing has not been performed.
    /// </value>
    /// <remarks>
    /// Lower p-values (e.g., &lt;0.05) typically indicate statistical significance.
    /// Should be interpreted in context of effect size and business impact.
    /// </remarks>
    /// <example>
    /// if (variant.PValue &lt; 0.05m) { /* Statistically significant difference */ }
    /// </example>
    public decimal? PValue { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of flow executions performed with this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides detailed execution history for analysis, debugging,
    /// and performance investigation, supporting comprehensive experiment
    /// tracking and result validation.
    /// </summary>
    /// <value>
    /// Collection of FlowExecution entities for this variant.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework relationships.
    /// Use Include() in queries when execution details are needed for analysis.
    /// </remarks>
    public virtual ICollection<FlowExecution> Executions { get; set; } = new List<FlowExecution>();
}
