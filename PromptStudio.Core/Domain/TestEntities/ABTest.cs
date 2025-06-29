using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// A/B testing framework for prompt templates and workflows
/// Enables data-driven optimization and performance comparison
/// </summary>
public class ABTest : AuditableEntity
{
    /// <summary>
    /// Test name for identification and reporting
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed description of the test purpose and methodology
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Hypothesis being tested
    /// </summary>
    [StringLength(1000)]
    public string? Hypothesis { get; set; }
    
    /// <summary>
    /// Test status and lifecycle management
    /// </summary>
    public ABTestStatus Status { get; set; } = ABTestStatus.Draft;
    
    /// <summary>
    /// Type of entity being tested
    /// </summary>
    public TestEntityType EntityType { get; set; }
    
    /// <summary>
    /// Test start date
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Test end date
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Target sample size for statistical significance
    /// </summary>
    public int TargetSampleSize { get; set; } = 100;
    
    /// <summary>
    /// Current sample size achieved
    /// </summary>
    public int CurrentSampleSize { get; set; } = 0;
    
    /// <summary>
    /// Required confidence level (e.g., 0.95 for 95%)
    /// </summary>
    public decimal ConfidenceLevel { get; set; } = 0.95m;
    
    /// <summary>
    /// Statistical significance threshold
    /// </summary>
    public decimal SignificanceThreshold { get; set; } = 0.05m;
    
    /// <summary>
    /// Primary success metric being measured
    /// </summary>
    [StringLength(100)]
    public string? PrimaryMetric { get; set; }
    
    /// <summary>
    /// Secondary metrics being tracked (JSON array)
    /// </summary>
    [StringLength(500)]
    public string? SecondaryMetrics { get; set; }
    
    /// <summary>
    /// Test configuration and parameters (JSON)
    /// </summary>
    public string? TestConfiguration { get; set; }
    
    /// <summary>
    /// Traffic allocation percentage (0-100)
    /// </summary>
    public decimal TrafficAllocation { get; set; } = 100;
    
    /// <summary>
    /// Who created and owns this test
    /// </summary>
    [StringLength(100)]
    public string? TestOwner { get; set; }
    
    /// <summary>
    /// Tags for test categorization (JSON array)
    /// </summary>
    [StringLength(500)]
    public string? Tags { get; set; }
    
    // Results and Analysis
    /// <summary>
    /// Test results summary (JSON)
    /// </summary>
    public string? ResultsSummary { get; set; }
    
    /// <summary>
    /// Statistical analysis results (JSON)
    /// </summary>
    public string? StatisticalAnalysis { get; set; }
    
    /// <summary>
    /// Winner variant (if determined)
    /// </summary>
    public Guid? WinnerVariantId { get; set; }
    public virtual ABTestVariant? WinnerVariant { get; set; }
    
    /// <summary>
    /// Test conclusions and recommendations
    /// </summary>
    [StringLength(2000)]
    public string? Conclusions { get; set; }
    
    // Navigation Properties
    public virtual ICollection<ABTestVariant> Variants { get; set; } = new List<ABTestVariant>();
    public virtual ICollection<ABTestResult> Results { get; set; } = new List<ABTestResult>();
}

/// <summary>
/// Individual variant in an A/B test
/// </summary>
public class ABTestVariant : AuditableEntity
{
    /// <summary>
    /// Reference to the parent A/B test
    /// </summary>
    public Guid ABTestId { get; set; }
    public virtual ABTest ABTest { get; set; } = null!;
    
    /// <summary>
    /// Variant name (e.g., "Control", "Variant A", "New Prompt")
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Variant description
    /// </summary>
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Whether this is the control variant
    /// </summary>
    public bool IsControl { get; set; } = false;
    
    /// <summary>
    /// Traffic allocation percentage for this variant (0-100)
    /// </summary>
    public decimal TrafficWeight { get; set; } = 50;
    
    /// <summary>
    /// Entity being tested in this variant
    /// </summary>
    public Guid? EntityId { get; set; }
    
    /// <summary>
    /// Entity version (for templates/workflows)
    /// </summary>
    [StringLength(20)]
    public string? EntityVersion { get; set; }
    
    /// <summary>
    /// Variant-specific configuration (JSON)
    /// </summary>
    public string? Configuration { get; set; }
    
    /// <summary>
    /// Number of executions for this variant
    /// </summary>
    public long ExecutionCount { get; set; } = 0;
    
    /// <summary>
    /// Success count for primary metric
    /// </summary>
    public long SuccessCount { get; set; } = 0;
    
    /// <summary>
    /// Conversion rate for primary metric
    /// </summary>
    public decimal ConversionRate { get; set; } = 0;
    
    /// <summary>
    /// Average performance score
    /// </summary>
    public decimal? AverageScore { get; set; }
    
    /// <summary>
    /// Variant status
    /// </summary>
    public VariantStatus Status { get; set; } = VariantStatus.Active;
    
    // Navigation Properties
    public virtual ICollection<ABTestResult> Results { get; set; } = new List<ABTestResult>();
}

/// <summary>
/// Individual result/execution record for A/B tests
/// </summary>
public class ABTestResult : AuditableEntity
{
    /// <summary>
    /// Reference to the A/B test
    /// </summary>
    public Guid ABTestId { get; set; }
    public virtual ABTest ABTest { get; set; } = null!;
    
    /// <summary>
    /// Reference to the test variant
    /// </summary>
    public Guid VariantId { get; set; }
    public virtual ABTestVariant Variant { get; set; } = null!;
    
    /// <summary>
    /// Unique session/user identifier for this test execution
    /// </summary>
    [StringLength(100)]
    public string SessionId { get; set; } = string.Empty;
    
    /// <summary>
    /// User identifier (if available)
    /// </summary>
    [StringLength(100)]
    public string? UserId { get; set; }
    
    /// <summary>
    /// Execution timestamp
    /// </summary>
    public DateTime ExecutionTime { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Primary metric result
    /// </summary>
    public decimal? PrimaryMetricValue { get; set; }
    
    /// <summary>
    /// Secondary metrics results (JSON)
    /// </summary>
    public string? SecondaryMetricValues { get; set; }
    
    /// <summary>
    /// Whether this execution was successful for the primary metric
    /// </summary>
    public bool Success { get; set; } = false;
    
    /// <summary>
    /// Execution context and metadata (JSON)
    /// </summary>
    public string? ExecutionContext { get; set; }
    
    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Execution duration in milliseconds
    /// </summary>
    public int? DurationMs { get; set; }
    
    /// <summary>
    /// Cost of this execution
    /// </summary>
    public decimal? Cost { get; set; }
    
    /// <summary>
    /// Quality score for this execution
    /// </summary>
    public decimal? QualityScore { get; set; }
}

/// <summary>
/// A/B test status
/// </summary>
public enum ABTestStatus
{
    Draft = 0,
    Ready = 1,
    Running = 2,
    Paused = 3,
    Completed = 4,
    Cancelled = 5,
    Archived = 6
}

/// <summary>
/// Type of entity being tested
/// </summary>
public enum TestEntityType
{
    PromptTemplate = 0,
    Workflow = 1,
    VariableSet = 2,
    Model = 3,
    Configuration = 4
}

/// <summary>
/// A/B test variant status
/// </summary>
public enum VariantStatus
{
    Active = 0,
    Paused = 1,
    Stopped = 2,
    Winner = 3,
    Loser = 4
}
