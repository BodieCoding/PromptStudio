using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Quality analysis result for a prompt library
/// </summary>
public class LibraryQualityAnalysis
{
    /// <summary>
    /// Gets or sets the library ID that was analyzed
    /// </summary>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the library name
    /// </summary>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the overall quality score (0.0 to 1.0)
    /// </summary>
    public double OverallQualityScore { get; set; }

    /// <summary>
    /// Gets or sets the quality grade (A, B, C, D, F)
    /// </summary>
    public string QualityGrade { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets individual quality metrics
    /// </summary>
    public QualityMetrics Metrics { get; set; } = new();

    /// <summary>
    /// Gets or sets quality issues found
    /// </summary>
    public List<QualityIssue> Issues { get; set; } = new();

    /// <summary>
    /// Gets or sets optimization recommendations
    /// </summary>
    public List<QualityRecommendation> Recommendations { get; set; } = new();

    /// <summary>
    /// Gets or sets template-specific quality analysis
    /// </summary>
    public Dictionary<Guid, TemplateQualityAnalysis> TemplateAnalysis { get; set; } = new();

    /// <summary>
    /// Gets or sets historical quality trends
    /// </summary>
    public List<QualityTrendPoint> QualityTrends { get; set; } = new();

    /// <summary>
    /// Gets or sets benchmarks and comparisons
    /// </summary>
    public QualityBenchmarks Benchmarks { get; set; } = new();

    /// <summary>
    /// Gets or sets the analysis completion timestamp
    /// </summary>
    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the time period analyzed
    /// </summary>
    public DateTimeRange AnalysisPeriod { get; set; } = new();

    /// <summary>
    /// Gets or sets additional analysis metadata
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Quality metrics breakdown
/// </summary>
public class QualityMetrics
{
    /// <summary>
    /// Gets or sets template completeness score (0.0 to 1.0)
    /// </summary>
    public double CompletenessScore { get; set; }

    /// <summary>
    /// Gets or sets template consistency score (0.0 to 1.0)
    /// </summary>
    public double ConsistencyScore { get; set; }

    /// <summary>
    /// Gets or sets template reliability score (0.0 to 1.0)
    /// </summary>
    public double ReliabilityScore { get; set; }

    /// <summary>
    /// Gets or sets template performance score (0.0 to 1.0)
    /// </summary>
    public double PerformanceScore { get; set; }

    /// <summary>
    /// Gets or sets template maintainability score (0.0 to 1.0)
    /// </summary>
    public double MaintainabilityScore { get; set; }

    /// <summary>
    /// Gets or sets template documentation score (0.0 to 1.0)
    /// </summary>
    public double DocumentationScore { get; set; }

    /// <summary>
    /// Gets or sets template usage efficiency score (0.0 to 1.0)
    /// </summary>
    public double EfficiencyScore { get; set; }

    /// <summary>
    /// Gets or sets template error handling score (0.0 to 1.0)
    /// </summary>
    public double ErrorHandlingScore { get; set; }
}

/// <summary>
/// Individual quality issue
/// </summary>
public class QualityIssue
{
    /// <summary>
    /// Gets or sets the issue ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the issue type
    /// </summary>
    public string IssueType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issue severity (Critical, High, Medium, Low)
    /// </summary>
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issue title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the issue description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the affected template ID (if applicable)
    /// </summary>
    public Guid? AffectedTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the affected template name
    /// </summary>
    public string? AffectedTemplateName { get; set; }

    /// <summary>
    /// Gets or sets the impact assessment
    /// </summary>
    public string Impact { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets suggested resolution steps
    /// </summary>
    public List<string> ResolutionSteps { get; set; } = new();

    /// <summary>
    /// Gets or sets the estimated effort to fix
    /// </summary>
    public string EstimatedEffort { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority for fixing this issue
    /// </summary>
    public int Priority { get; set; } = 0;
}

/// <summary>
/// Quality improvement recommendation
/// </summary>
public class QualityRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation ID
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the recommendation category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expected impact (0.0 to 1.0)
    /// </summary>
    public double ExpectedImpact { get; set; }

    /// <summary>
    /// Gets or sets the implementation difficulty (Easy, Medium, Hard)
    /// </summary>
    public string ImplementationDifficulty { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommended actions
    /// </summary>
    public List<string> RecommendedActions { get; set; } = new();

    /// <summary>
    /// Gets or sets the success criteria for this recommendation
    /// </summary>
    public List<string> SuccessCriteria { get; set; } = new();

    /// <summary>
    /// Gets or sets the priority level (1-10)
    /// </summary>
    public int Priority { get; set; } = 5;
}

/// <summary>
/// Template-specific quality analysis
/// </summary>
public class TemplateQualityAnalysis
{
    /// <summary>
    /// Gets or sets the template ID
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name
    /// </summary>
    public string TemplateName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the template quality score (0.0 to 1.0)
    /// </summary>
    public double QualityScore { get; set; }

    /// <summary>
    /// Gets or sets template-specific issues
    /// </summary>
    public List<QualityIssue> Issues { get; set; } = new();

    /// <summary>
    /// Gets or sets template-specific recommendations
    /// </summary>
    public List<QualityRecommendation> Recommendations { get; set; } = new();

    /// <summary>
    /// Gets or sets template complexity metrics
    /// </summary>
    public TemplateComplexityMetrics ComplexityMetrics { get; set; } = new();
}

/// <summary>
/// Template complexity metrics
/// </summary>
public class TemplateComplexityMetrics
{
    /// <summary>
    /// Gets or sets the number of variables in the template
    /// </summary>
    public int VariableCount { get; set; }

    /// <summary>
    /// Gets or sets the template length in characters
    /// </summary>
    public int TemplateLength { get; set; }

    /// <summary>
    /// Gets or sets the complexity score based on structure
    /// </summary>
    public double StructuralComplexity { get; set; }

    /// <summary>
    /// Gets or sets the cyclomatic complexity (if applicable)
    /// </summary>
    public double CyclomaticComplexity { get; set; }

    /// <summary>
    /// Gets or sets the nesting depth of conditional logic
    /// </summary>
    public int NestingDepth { get; set; }
}

/// <summary>
/// Quality trend data point
/// </summary>
public class QualityTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the quality score at this point
    /// </summary>
    public double QualityScore { get; set; }

    /// <summary>
    /// Gets or sets the number of templates analyzed
    /// </summary>
    public int TemplateCount { get; set; }

    /// <summary>
    /// Gets or sets the number of issues found
    /// </summary>
    public int IssueCount { get; set; }
}

/// <summary>
/// Quality benchmarks and comparisons
/// </summary>
public class QualityBenchmarks
{
    /// <summary>
    /// Gets or sets the industry average quality score
    /// </summary>
    public double IndustryAverage { get; set; }

    /// <summary>
    /// Gets or sets the organization average quality score
    /// </summary>
    public double OrganizationAverage { get; set; }

    /// <summary>
    /// Gets or sets the percentile rank of this library
    /// </summary>
    public double PercentileRank { get; set; }

    /// <summary>
    /// Gets or sets comparison with similar libraries
    /// </summary>
    public List<LibraryComparison> SimilarLibraries { get; set; } = new();
}

/// <summary>
/// Comparison with similar libraries
/// </summary>
public class LibraryComparison
{
    /// <summary>
    /// Gets or sets the comparison library name
    /// </summary>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the comparison library quality score
    /// </summary>
    public double QualityScore { get; set; }

    /// <summary>
    /// Gets or sets the similarity score to current library
    /// </summary>
    public double SimilarityScore { get; set; }

    /// <summary>
    /// Gets or sets key differences
    /// </summary>
    public List<string> KeyDifferences { get; set; } = new();
}
