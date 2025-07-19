namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of collection optimization analysis and recommendations
/// </summary>
public class CollectionOptimizationResult
{
    /// <summary>
    /// Gets or sets the collection ID that was analyzed
    /// </summary>
    public Guid CollectionId { get; set; }

    /// <summary>
    /// Gets or sets the collection name
    /// </summary>
    public string CollectionName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the overall optimization score (0.0 to 1.0)
    /// Higher scores indicate better optimization
    /// </summary>
    public double OptimizationScore { get; set; }

    /// <summary>
    /// Gets or sets the performance improvement potential (0.0 to 1.0)
    /// </summary>
    public double ImprovementPotential { get; set; }

    /// <summary>
    /// Gets or sets specific optimization recommendations
    /// </summary>
    public List<OptimizationRecommendation> Recommendations { get; set; } = new();

    /// <summary>
    /// Gets or sets identified performance bottlenecks
    /// </summary>
    public List<PerformanceBottleneck> Bottlenecks { get; set; } = new();

    /// <summary>
    /// Gets or sets variable usage efficiency analysis
    /// </summary>
    public Dictionary<string, VariableEfficiency> VariableEfficiency { get; set; } = new();

    /// <summary>
    /// Gets or sets suggested variable set optimizations
    /// </summary>
    public List<VariableSetOptimization> SuggestedOptimizations { get; set; } = new();

    /// <summary>
    /// Gets or sets quality metrics for the collection
    /// </summary>
    public CollectionQualityMetrics QualityMetrics { get; set; } = new();

    /// <summary>
    /// Gets or sets the analysis timestamp
    /// </summary>
    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets additional optimization metadata
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Individual optimization recommendation
/// </summary>
public class OptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the recommendation title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category (Performance, Quality, Efficiency, etc.)
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority level (High, Medium, Low)
    /// </summary>
    public string Priority { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated impact (0.0 to 1.0)
    /// </summary>
    public double EstimatedImpact { get; set; }

    /// <summary>
    /// Gets or sets the implementation effort (0.0 to 1.0)
    /// </summary>
    public double ImplementationEffort { get; set; }

    /// <summary>
    /// Gets or sets suggested actions to implement the recommendation
    /// </summary>
    public List<string> SuggestedActions { get; set; } = new();
}

/// <summary>
/// Performance bottleneck identification
/// </summary>
public class PerformanceBottleneck
{
    /// <summary>
    /// Gets or sets the bottleneck type
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the bottleneck description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level (Critical, High, Medium, Low)
    /// </summary>
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the affected components
    /// </summary>
    public List<string> AffectedComponents { get; set; } = new();

    /// <summary>
    /// Gets or sets suggested resolutions
    /// </summary>
    public List<string> SuggestedResolutions { get; set; } = new();
}

/// <summary>
/// Variable efficiency analysis
/// </summary>
public class VariableEfficiency
{
    /// <summary>
    /// Gets or sets the variable name
    /// </summary>
    public string VariableName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the usage frequency
    /// </summary>
    public int UsageFrequency { get; set; }

    /// <summary>
    /// Gets or sets the efficiency score (0.0 to 1.0)
    /// </summary>
    public double EfficiencyScore { get; set; }

    /// <summary>
    /// Gets or sets the value diversity ratio
    /// </summary>
    public double ValueDiversity { get; set; }

    /// <summary>
    /// Gets or sets suggested improvements
    /// </summary>
    public List<string> SuggestedImprovements { get; set; } = new();
}

/// <summary>
/// Variable set optimization suggestions
/// </summary>
public class VariableSetOptimization
{
    /// <summary>
    /// Gets or sets the current variable set
    /// </summary>
    public Dictionary<string, string> CurrentSet { get; set; } = new();

    /// <summary>
    /// Gets or sets the optimized variable set
    /// </summary>
    public Dictionary<string, string> OptimizedSet { get; set; } = new();

    /// <summary>
    /// Gets or sets the expected performance improvement
    /// </summary>
    public double ExpectedImprovement { get; set; }

    /// <summary>
    /// Gets or sets the optimization rationale
    /// </summary>
    public string Rationale { get; set; } = string.Empty;
}

/// <summary>
/// Collection quality metrics
/// </summary>
public class CollectionQualityMetrics
{
    /// <summary>
    /// Gets or sets the data completeness score (0.0 to 1.0)
    /// </summary>
    public double DataCompleteness { get; set; }

    /// <summary>
    /// Gets or sets the data consistency score (0.0 to 1.0)
    /// </summary>
    public double DataConsistency { get; set; }

    /// <summary>
    /// Gets or sets the variable coverage score (0.0 to 1.0)
    /// </summary>
    public double VariableCoverage { get; set; }

    /// <summary>
    /// Gets or sets the execution reliability score (0.0 to 1.0)
    /// </summary>
    public double ExecutionReliability { get; set; }

    /// <summary>
    /// Gets or sets the overall quality score (0.0 to 1.0)
    /// </summary>
    public double OverallQuality { get; set; }
}
