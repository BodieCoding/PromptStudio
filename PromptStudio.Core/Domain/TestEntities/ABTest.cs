using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a comprehensive A/B testing experiment framework for systematic optimization of prompts, workflows, and AI system components.
/// 
/// <para><strong>Business Context:</strong></para>
/// ABTest enables organizations to make data-driven decisions about AI system optimization through
/// controlled experimentation and statistical analysis. By comparing different variations of prompts,
/// workflows, models, or configurations, teams can identify optimal solutions, improve performance,
/// and minimize risks associated with AI system changes in production environments.
/// 
/// <para><strong>Technical Context:</strong></para>
/// ABTest serves as the central orchestrator for controlled experiments within the LLMOps platform,
/// providing traffic allocation, statistical analysis, and result management capabilities with
/// enterprise-grade audit trails, multi-tenancy support, and comprehensive experiment lifecycle
/// management for scalable AI optimization workflows.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Data-driven optimization through controlled experimentation
/// - Risk mitigation via systematic testing before production deployment
/// - Statistical validation of AI system improvements and changes
/// - Performance benchmarking and comparative analysis capabilities
/// - Enterprise-grade experiment governance and compliance support
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Experiment Pattern: Controlled testing with statistical validation
/// - Observer Pattern: Real-time monitoring and metrics collection
/// - Strategy Pattern: Different testing approaches for various entity types
/// - State Machine: Experiment lifecycle management and status transitions
/// - Multi-tenancy: Organizational isolation and access control
/// 
/// <para><strong>Statistical Framework:</strong></para>
/// - Sample size calculation for statistical power and significance
/// - Confidence interval estimation for result reliability
/// - Traffic allocation algorithms for fair variant distribution
/// - A/B/n testing support for multiple variant comparisons
/// - Sequential testing for early stopping and result validation
/// 
/// <para><strong>Experiment Lifecycle:</strong></para>
/// 1. Design: Hypothesis formation and variant configuration
/// 2. Validation: Configuration review and approval processes
/// 3. Execution: Traffic routing and data collection
/// 4. Analysis: Statistical evaluation and result interpretation
/// 5. Decision: Winner selection and deployment recommendations
/// 6. Archive: Historical preservation and learning capture
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index on TenantId and Status for efficient experiment queries
/// - Optimize traffic allocation algorithms for minimal latency
/// - Consider caching active experiments for traffic routing
/// - Monitor experiment performance impact on system resources
/// - Archive completed experiments to manage database growth
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Traffic Router: Dynamic variant selection and load balancing
/// - Analytics Platform: Real-time metrics and statistical analysis
/// - Notification System: Experiment alerts and status updates
/// - Deployment Pipeline: Winner promotion and rollout automation
/// - Monitoring System: Experiment health and performance tracking
/// 
/// <para><strong>Compliance and Governance:</strong></para>
/// - Complete audit trail for regulatory compliance requirements
/// - Multi-tenant data isolation for security and privacy
/// - Experiment approval workflows for controlled testing
/// - Data retention policies for privacy and storage management
/// - Access control for experiment management and result viewing
/// </remarks>
/// <example>
/// <code>
/// // Create a new A/B test for prompt optimization
/// var abTest = new ABTest
/// {
///     TenantId = organizationId,
///     Name = "Customer Support Prompt Optimization",
///     Description = "Testing improved prompt templates for customer support automation",
///     Hypothesis = "New structured prompt will improve response relevance by 15%",
///     EntityType = TestEntityType.PromptTemplate,
///     TargetSampleSize = 1000,
///     ConfidenceLevel = 0.95m,
///     PrimaryMetric = "response_relevance_score",
///     SecondaryMetrics = JsonSerializer.Serialize(new[] 
///     { 
///         "response_time", "user_satisfaction", "resolution_rate" 
///     }),
///     TrafficAllocation = 50m, // 50% of traffic
///     TestOwner = "ai-team@company.com"
/// };
/// 
/// // Add variants for comparison
/// abTest.Variants.Add(new ABTestVariant
/// {
///     Name = "Control - Current Prompt",
///     IsControl = true,
///     TrafficWeight = 50m,
///     EntityId = currentPromptId
/// });
/// 
/// abTest.Variants.Add(new ABTestVariant
/// {
///     Name = "Variant A - Structured Prompt",
///     TrafficWeight = 50m,
///     EntityId = newPromptId
/// });
/// 
/// // Start the experiment
/// abTest.Status = ABTestStatus.Running;
/// abTest.StartDate = DateTime.UtcNow;
/// 
/// await repository.AddAsync(abTest);
/// </code>
/// </example>
public class ABTest : AuditableEntity
{
    /// <summary>
    /// Human-readable name for the experiment used in reporting and management interfaces.
    /// <value>String up to 100 characters identifying the experiment purpose and scope</value>
    /// </summary>
    /// <remarks>
    /// Should be descriptive and unique within the organization to facilitate
    /// experiment tracking, reporting, and team communication.
    /// </remarks>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Comprehensive description explaining the experiment's objectives, methodology, and expected outcomes.
    /// <value>String up to 500 characters detailing experiment context and goals</value>
    /// </summary>
    /// <remarks>
    /// Provides context for stakeholders, documents experiment rationale,
    /// and supports future analysis and learning from experiment results.
    /// </remarks>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Specific, testable hypothesis that the experiment is designed to validate or refute.
    /// <value>String up to 1000 characters stating the testable prediction</value>
    /// </summary>
    /// <remarks>
    /// Foundation for statistical analysis and success criteria definition.
    /// Should be specific, measurable, and aligned with business objectives.
    /// </remarks>
    [StringLength(1000)]
    public string? Hypothesis { get; set; }
    
    /// <summary>
    /// Current lifecycle status controlling experiment behavior and data collection.
    /// <value>ABTestStatus enum indicating experiment operational state</value>
    /// </summary>
    /// <remarks>
    /// Controls traffic routing, data collection, and available operations.
    /// Status transitions trigger automated workflow processes and notifications.
    /// </remarks>
    public ABTestStatus Status { get; set; } = ABTestStatus.Draft;
    
    /// <summary>
    /// Category of system component being tested to determine appropriate testing methodology.
    /// <value>TestEntityType enum specifying what type of component is under test</value>
    /// </summary>
    /// <remarks>
    /// Determines metrics collection strategy, analysis framework,
    /// and testing methodology applied to the experiment.
    /// </remarks>
    public TestEntityType EntityType { get; set; }
    
    /// <summary>
    /// Planned experiment start date for scheduling and timeline management.
    /// <value>DateTime in UTC when experiment data collection should begin</value>
    /// </summary>
    /// <remarks>
    /// Used for experiment scheduling, timeline tracking, and automated
    /// status transitions when integrated with scheduling systems.
    /// </remarks>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Planned experiment end date for duration control and automatic termination.
    /// <value>DateTime in UTC when experiment should conclude</value>
    /// </summary>
    /// <remarks>
    /// Prevents experiments from running indefinitely and triggers
    /// automated analysis when the target duration is reached.
    /// </remarks>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Required number of samples for statistical significance and reliable results.
    /// <value>Integer representing minimum execution count needed for valid analysis</value>
    /// </summary>
    /// <remarks>
    /// Based on statistical power analysis considering effect size,
    /// significance level, and desired confidence in results.
    /// </remarks>
    public int TargetSampleSize { get; set; } = 100;
    
    /// <summary>
    /// Actual number of samples collected so far for progress tracking.
    /// <value>Integer count of executions recorded across all variants</value>
    /// </summary>
    /// <remarks>
    /// Automatically updated as executions are recorded. Used for
    /// progress monitoring and completion criteria evaluation.
    /// </remarks>
    public int CurrentSampleSize { get; set; } = 0;
    
    /// <summary>
    /// Statistical confidence level required for result validation and decision making.
    /// <value>Decimal between 0 and 1 representing confidence threshold (e.g., 0.95 for 95%)</value>
    /// </summary>
    /// <remarks>
    /// Determines the reliability threshold for statistical analysis
    /// and influences sample size requirements and result interpretation.
    /// </remarks>
    public decimal ConfidenceLevel { get; set; } = 0.95m;
    
    /// <summary>
    /// P-value threshold for determining statistical significance of results.
    /// <value>Decimal between 0 and 1 representing significance threshold (e.g., 0.05 for 5%)</value>
    /// </summary>
    /// <remarks>
    /// Used in hypothesis testing to determine if observed differences
    /// between variants are statistically significant or due to chance.
    /// </remarks>
    public decimal SignificanceThreshold { get; set; } = 0.05m;
    
    /// <summary>
    /// Primary success metric used for variant comparison and winner determination.
    /// <value>String identifying the key performance indicator for this experiment</value>
    /// </summary>
    /// <remarks>
    /// Should align with business objectives and be consistently measurable
    /// across all variants. Examples: "conversion_rate", "response_quality", "execution_time".
    /// </remarks>
    [StringLength(100)]
    public string? PrimaryMetric { get; set; }
    
    /// <summary>
    /// Additional metrics tracked for comprehensive analysis and insights.
    /// <value>JSON array of metric names for supplementary analysis</value>
    /// </summary>
    /// <remarks>
    /// Provides deeper insights beyond the primary metric and supports
    /// multi-dimensional analysis of variant performance and trade-offs.
    /// </remarks>
    [StringLength(500)]
    public string? SecondaryMetrics { get; set; }
    
    /// <summary>
    /// Experiment-specific configuration parameters and settings.
    /// <value>JSON object containing test-specific configuration and parameters</value>
    /// </summary>
    /// <remarks>
    /// Flexible storage for experiment-specific settings, thresholds,
    /// feature flags, and other configuration data needed for execution.
    /// </remarks>
    public string? TestConfiguration { get; set; }
    
    /// <summary>
    /// Percentage of total traffic allocated to this experiment for controlled exposure.
    /// <value>Decimal between 0 and 100 representing traffic percentage</value>
    /// </summary>
    /// <remarks>
    /// Enables gradual rollout and risk mitigation by limiting experiment
    /// exposure while maintaining statistical validity and business continuity.
    /// </remarks>
    public decimal TrafficAllocation { get; set; } = 100;
    
    /// <summary>
    /// Individual or team responsible for experiment design, execution, and analysis.
    /// <value>String identifier (email or username) of the experiment owner</value>
    /// </summary>
    /// <remarks>
    /// Establishes accountability, enables communication about experiment
    /// status, and provides contact for questions or issues during execution.
    /// </remarks>
    [StringLength(100)]
    public string? TestOwner { get; set; }
    
    /// <summary>
    /// Categorization tags for experiment organization and discovery.
    /// <value>JSON array of tag strings for filtering and grouping experiments</value>
    /// </summary>
    /// <remarks>
    /// Supports experiment organization, filtering, and discovery within
    /// large-scale testing programs and cross-team collaboration.
    /// </remarks>
    [StringLength(500)]
    public string? Tags { get; set; }

    // Results and Analysis
    /// <summary>
    /// High-level summary of experiment results and key findings.
    /// <value>JSON object containing summarized results and insights</value>
    /// </summary>
    /// <remarks>
    /// Generated after experiment completion to provide quick access
    /// to key findings without requiring detailed statistical analysis.
    /// </remarks>
    public string? ResultsSummary { get; set; }
    
    /// <summary>
    /// Detailed statistical analysis results including significance tests and confidence intervals.
    /// <value>JSON object containing comprehensive statistical analysis data</value>
    /// </summary>
    /// <remarks>
    /// Complete statistical analysis including p-values, confidence intervals,
    /// effect sizes, and other statistical measures for thorough result evaluation.
    /// </remarks>
    public string? StatisticalAnalysis { get; set; }
    
    /// <summary>
    /// Reference to the variant declared as the winner based on statistical analysis.
    /// <value>Guid identifier of the winning variant, if determined</value>
    /// </summary>
    /// <remarks>
    /// Set when statistical analysis indicates a clear winner or when
    /// business decisions determine the preferred variant for deployment.
    /// </remarks>
    public Guid? WinnerVariantId { get; set; }

    /// <summary>
    /// Navigation property to the winning variant entity for direct access.
    /// <value>ABTestVariant entity representing the experiment winner</value>
    /// </summary>
    /// <remarks>
    /// Provides convenient access to winner details, configuration,
    /// and performance metrics for deployment and promotion workflows.
    /// </remarks>
    public virtual ABTestVariant? WinnerVariant { get; set; }
    
    /// <summary>
    /// Final conclusions, recommendations, and actionable insights from the experiment.
    /// <value>String up to 2000 characters documenting lessons learned and next steps</value>
    /// </summary>
    /// <remarks>
    /// Documents key learnings, recommendations for implementation,
    /// and insights for future experiments and optimization efforts.
    /// </remarks>
    [StringLength(2000)]
    public string? Conclusions { get; set; }

    // Navigation Properties
    /// <summary>
    /// Collection of all variants participating in this experiment.
    /// <value>ICollection of ABTestVariant entities representing different test conditions</value>
    /// </summary>
    /// <remarks>
    /// Contains all variants being compared in the experiment, including
    /// control and treatment variants with their respective configurations.
    /// </remarks>
    public virtual ICollection<ABTestVariant> Variants { get; set; } = [];

    /// <summary>
    /// Collection of all execution results recorded for this experiment.
    /// <value>ICollection of ABTestResult entities representing individual executions</value>
    /// </summary>
    /// <remarks>
    /// Complete record of all executions across all variants, providing
    /// the raw data foundation for statistical analysis and reporting.
    /// </remarks>
    public virtual ICollection<ABTestResult> Results { get; set; } = [];
}

