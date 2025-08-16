using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents an individual variant within an A/B testing experiment for prompt optimization and performance comparison.
/// 
/// <para><strong>Business Context:</strong></para>
/// ABTestVariant enables controlled experimentation by defining specific configurations,
/// parameter variations, or alternative implementations to test against baseline performance.
/// Organizations can systematically compare different prompt approaches, model configurations,
/// or workflow variations to identify optimal solutions through data-driven analysis.
/// 
/// <para><strong>Technical Context:</strong></para>
/// ABTestVariant serves as a controlled experiment condition within the A/B testing framework,
/// providing traffic allocation, performance tracking, and statistical analysis capabilities
/// with enterprise audit trails and multi-tenant isolation for comprehensive experiment management.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Controlled experiment configuration and management
/// - Performance tracking and statistical analysis
/// - Traffic allocation and load balancing capabilities
/// - Data-driven optimization and decision making
/// - Enterprise-grade experiment governance and audit trails
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Experiment Pattern: Controlled variations for systematic testing
/// - Observer Pattern: Performance metrics collection and analysis
/// - Strategy Pattern: Different configurations and implementations
/// - Audit Trail: Complete experiment tracking and governance
/// - Multi-tenancy: Organizational isolation for experiment security
/// 
/// <para><strong>Statistical Considerations:</strong></para>
/// - Traffic allocation controls exposure and sample distribution
/// - Performance metrics enable statistical significance testing
/// - Control variant provides baseline for comparison analysis
/// - Success metrics support hypothesis validation and decision making
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Index on ABTestId for efficient variant retrieval
/// - Track execution counts for performance monitoring
/// - Monitor conversion rates for optimization insights
/// - Consider caching active variants for traffic routing
/// 
/// <para><strong>Integration Points:</strong></para>
/// - A/B Testing Framework: Experiment design and execution
/// - Traffic Router: Variant selection and load distribution
/// - Analytics Platform: Performance metrics and statistical analysis
/// - Monitoring System: Experiment health and performance tracking
/// </remarks>
/// <example>
/// <code>
/// // Creating control and test variants for prompt optimization
/// var controlVariant = new ABTestVariant
/// {
///     ABTestId = abTest.Id,
///     Name = "Control - Original Prompt",
///     Description = "Current production prompt template as baseline",
///     IsControl = true,
///     TrafficWeight = 50.0m,
///     EntityId = originalTemplate.Id,
///     EntityVersion = "1.0.0",
///     Configuration = JsonSerializer.Serialize(new { temperature = 0.7, max_tokens = 100 }),
///     Status = VariantStatus.Active
/// };
/// 
/// var testVariant = new ABTestVariant
/// {
///     ABTestId = abTest.Id,
///     Name = "Test - Enhanced Prompt",
///     Description = "Optimized prompt with improved context and examples",
///     IsControl = false,
///     TrafficWeight = 50.0m,
///     EntityId = enhancedTemplate.Id,
///     EntityVersion = "2.0.0",
///     Configuration = JsonSerializer.Serialize(new { temperature = 0.8, max_tokens = 150 }),
///     Status = VariantStatus.Active
/// };
/// 
/// await variantService.CreateAsync(controlVariant);
/// await variantService.CreateAsync(testVariant);
/// </code>
/// </example>
public class ABTestVariant : AuditableEntity
{
    /// <summary>
    /// Gets or sets the identifier of the parent A/B test this variant belongs to.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Establishes the experiment context for the variant, enabling proper
    /// test organization, result analysis, and experiment management within
    /// the broader A/B testing framework and governance structure.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key reference to the parent ABTest entity.
    /// Required for all variant relationships and experiment tracking.
    /// </summary>
    /// <value>
    /// A valid Guid that references an existing ABTest.
    /// Cannot be empty or default Guid value.
    /// </value>
    /// <remarks>
    /// Used in experiment queries and performance analysis.
    /// Essential for maintaining variant-test relationships and data integrity.
    /// </remarks>
    public Guid ABTestId { get; set; }

    /// <summary>
    /// Gets or sets the parent A/B test navigation property.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to the experiment configuration, hypothesis,
    /// and overall test context for variant analysis and management.
    /// </summary>
    /// <value>
    /// The ABTest entity this variant belongs to.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    public virtual ABTest ABTest { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the descriptive name of this experiment variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides clear identification for variant tracking, reporting,
    /// and team communication about different experimental conditions
    /// being tested in optimization experiments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Required field with maximum length for UI and reporting compatibility.
    /// Used in experiment dashboards, analytics, and administrative interfaces.
    /// </summary>
    /// <value>
    /// A descriptive name for the variant experiment condition.
    /// Cannot be null or empty. Maximum length is 100 characters.
    /// </value>
    /// <remarks>
    /// Should clearly indicate the variant's purpose or key differences.
    /// Used extensively in analytics reports and experiment tracking.
    /// </remarks>
    /// <example>
    /// Examples: "Control - Original", "Test - High Temperature", "Variant A - Enhanced Context"
    /// </example>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a detailed description of the variant's configuration and purpose.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Documents the variant's specific changes, configuration, and expected
    /// improvements, supporting experiment understanding and result interpretation
    /// for data-driven optimization decisions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional field with generous length for comprehensive documentation.
    /// Used in experiment reports and team collaboration.
    /// </summary>
    /// <value>
    /// A detailed description of the variant's purpose and configuration.
    /// Can be null. Maximum length is 500 characters.
    /// </value>
    /// <remarks>
    /// Should explain what makes this variant different from others.
    /// Important for experiment result interpretation and learning.
    /// </remarks>
    /// <example>
    /// "Enhanced prompt template with improved context examples and increased temperature for more creative responses"
    /// </example>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets whether this variant serves as the control baseline for comparison.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Identifies the baseline configuration for statistical comparison,
    /// ensuring proper experimental design with clear control and treatment
    /// groups for valid hypothesis testing and result interpretation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Boolean flag indicating control variant status.
    /// Used in statistical analysis and experiment result calculations.
    /// </summary>
    /// <value>
    /// True if this variant serves as the experimental control, false otherwise.
    /// Default is false for newly created variants.
    /// </value>
    /// <remarks>
    /// Only one variant per test should typically be marked as control.
    /// Critical for proper statistical analysis and result interpretation.
    /// </remarks>
    /// <example>
    /// controlVariant.IsControl = true; // Baseline configuration
    /// testVariant.IsControl = false;   // Experimental variation
    /// </example>
    public bool IsControl { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the traffic allocation percentage for this variant in the experiment.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls experiment exposure and risk management, enabling gradual rollouts,
    /// statistical power optimization, and business impact control through
    /// precise traffic distribution across experimental conditions.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Decimal percentage value for traffic routing and load balancing.
    /// Used by experiment frameworks and traffic splitting algorithms.
    /// </summary>
    /// <value>
    /// Traffic weight as decimal percentage for this variant.
    /// Default is 50 for equal distribution in two-variant tests.
    /// </value>
    /// <remarks>
    /// Traffic weights across all variants should sum to 100% for proper distribution.
    /// Used by load balancers and experiment routing systems.
    /// </remarks>
    /// <example>
    /// controlVariant.TrafficWeight = 70.0m; // 70% to control
    /// testVariant.TrafficWeight = 30.0m;    // 30% to test
    /// </example>
    public decimal TrafficWeight { get; set; } = 50;
    
    /// <summary>
    /// Gets or sets the identifier of the entity being tested in this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links the variant to the specific prompt template, workflow, or configuration
    /// being tested, enabling proper experiment execution and result attribution
    /// for targeted optimization and performance analysis.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional reference to the entity under test (template, workflow, etc.).
    /// Used for experiment execution and configuration management.
    /// </summary>
    /// <value>
    /// A valid Guid referencing the entity being tested, or null if not applicable.
    /// </value>
    /// <remarks>
    /// Entity type is determined by the parent ABTest's EntityType property.
    /// Critical for experiment execution and result tracking.
    /// </remarks>
    /// <example>
    /// variant.EntityId = promptTemplate.Id; // Testing specific template
    /// </example>
    public Guid? EntityId { get; set; }
    
    /// <summary>
    /// Gets or sets the version of the entity being tested for change tracking.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks the specific version of templates or workflows being tested,
    /// ensuring experiment reproducibility and enabling version-specific
    /// performance analysis and optimization insights.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional version string for entity version tracking.
    /// Used for experiment reproducibility and change impact analysis.
    /// </summary>
    /// <value>
    /// Version string of the entity being tested (e.g., "1.2.0").
    /// Can be null if version tracking is not applicable. Maximum length is 20 characters.
    /// </value>
    /// <remarks>
    /// Should follow semantic versioning conventions when applicable.
    /// Important for experiment reproducibility and change tracking.
    /// </remarks>
    /// <example>
    /// variant.EntityVersion = "2.1.0"; // Testing version 2.1.0 of template
    /// </example>
    [StringLength(20)]
    public string? EntityVersion { get; set; }
    
    /// <summary>
    /// Gets or sets the variant-specific configuration parameters as JSON.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Stores the complete variant configuration for experiment execution,
    /// enabling parameter testing, configuration optimization, and detailed
    /// experiment reproduction across different environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON-formatted string containing variant-specific parameters and settings.
    /// Used by experiment execution engines for variant behavior implementation.
    /// </summary>
    /// <value>
    /// A JSON string containing variant configuration parameters.
    /// Can be null if no specific configuration is needed.
    /// </value>
    /// <remarks>
    /// Should contain all parameters needed to reproduce the variant.
    /// Used by runtime systems for variant-specific behavior implementation.
    /// </remarks>
    /// <example>
    /// {"temperature": 0.8, "max_tokens": 150, "model": "gpt-4", "system_prompt": "enhanced"}
    /// </example>
    public string? Configuration { get; set; }
    
    /// <summary>
    /// Gets or sets the total number of executions performed for this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks experiment sample size for statistical significance analysis,
    /// performance monitoring, and experiment completion assessment,
    /// supporting data-driven decision making and statistical validation.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Performance counter updated by experiment execution systems.
    /// Used for statistical analysis and experiment progress tracking.
    /// </summary>
    /// <value>
    /// Total count of executions for this variant since experiment start.
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
    /// Gets or sets the number of successful outcomes for the primary success metric.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Tracks positive outcomes for conversion rate calculation and success
    /// measurement, enabling statistical comparison and optimization decisions
    /// based on primary business objectives and success criteria.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Performance counter for primary metric success tracking.
    /// Used for conversion rate calculation and statistical analysis.
    /// </summary>
    /// <value>
    /// Count of successful executions based on primary success metric.
    /// Default is 0 for newly created variants.
    /// </value>
    /// <remarks>
    /// Success criteria should be consistently defined across all variants.
    /// Used for primary metric analysis and optimization decisions.
    /// </remarks>
    /// <example>
    /// if (result.Success) { variant.SuccessCount++; }
    /// </example>
    public long SuccessCount { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the conversion rate for the primary success metric.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides the key performance indicator for variant effectiveness,
    /// enabling direct comparison between experimental conditions and
    /// data-driven optimization decisions based on business impact.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Calculated as SuccessCount / ExecutionCount when ExecutionCount > 0.
    /// Updated by analytics systems and experiment monitoring processes.
    /// </summary>
    /// <value>
    /// Conversion rate as decimal between 0 and 1 (0% to 100%).
    /// Default is 0 for newly created variants.
    /// </value>
    /// <remarks>
    /// Should be calculated consistently across all variants for valid comparison.
    /// Primary metric for statistical significance testing and optimization.
    /// </remarks>
    /// <example>
    /// variant.ConversionRate = (decimal)variant.SuccessCount / variant.ExecutionCount;
    /// </example>
    public decimal ConversionRate { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the average performance score across all executions for this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides comprehensive performance assessment beyond binary success metrics,
    /// enabling nuanced comparison and optimization decisions based on
    /// quality, effectiveness, and overall performance characteristics.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Optional aggregated score from execution quality assessments.
    /// Calculated from individual execution scores and quality metrics.
    /// </summary>
    /// <value>
    /// Average performance score, typically normalized to 0-1 or 0-100 scale.
    /// Null if performance scoring has not been implemented or calculated.
    /// </value>
    /// <remarks>
    /// Scoring methodology should be consistent across all variants.
    /// Supplements conversion rate with qualitative performance assessment.
    /// </remarks>
    /// <example>
    /// if (variant.AverageScore > 0.8m) { /* High-performing variant */ }
    /// </example>
    public decimal? AverageScore { get; set; }
    
    /// <summary>
    /// Gets or sets the current operational status of this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls variant participation in experiments and enables lifecycle
    /// management, supporting operational control, result interpretation,
    /// and experiment governance through status-based workflows.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Enum-based status for variant lifecycle and operational control.
    /// Used by experiment engines for traffic routing and execution decisions.
    /// </summary>
    /// <value>
    /// A <see cref="VariantStatus"/> enum value indicating current operational state.
    /// Default is Active for newly created variants.
    /// </value>
    /// <remarks>
    /// Status changes should be logged for experiment audit trails.
    /// Used for controlling variant participation and result interpretation.
    /// </remarks>
    /// <example>
    /// variant.Status = VariantStatus.Winner; // Mark as winning variant
    /// </example>
    public VariantStatus Status { get; set; } = VariantStatus.Active;
    
    /// <summary>
    /// Gets or sets the collection of individual execution results for this variant.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides detailed execution history for performance analysis,
    /// debugging, statistical validation, and comprehensive experiment
    /// insights and result interpretation.
    /// </summary>
    /// <value>
    /// Collection of ABTestResult entities for this variant.
    /// Loaded through Entity Framework navigation when needed.
    /// </value>
    /// <remarks>
    /// Navigation property for Entity Framework relationships.
    /// Use Include() in queries when detailed result analysis is needed.
    /// </remarks>
    public virtual ICollection<ABTestResult> Results { get; set; } = [];
}
