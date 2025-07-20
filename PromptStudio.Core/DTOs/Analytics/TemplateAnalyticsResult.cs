using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive template analytics result with usage patterns and optimization insights
/// </summary>
public class TemplateAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Template analytics summary
    /// </summary>
    public TemplateAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// Template usage analytics
    /// </summary>
    public TemplateUsageAnalytics Usage { get; set; } = new();

    /// <summary>
    /// Template performance analytics
    /// </summary>
    public TemplatePerformanceAnalytics Performance { get; set; } = new();

    /// <summary>
    /// Template quality analytics
    /// </summary>
    public TemplateQualityAnalytics Quality { get; set; } = new();

    /// <summary>
    /// Variable effectiveness analytics
    /// </summary>
    public VariableEffectivenessAnalytics VariableEffectiveness { get; set; } = new();

    /// <summary>
    /// Template optimization analytics
    /// </summary>
    public TemplateOptimizationAnalytics Optimization { get; set; } = new();

    /// <summary>
    /// Time-series data for template metrics
    /// </summary>
    public List<TemplateAnalyticsTimePoint>? TimeSeries { get; set; }

    /// <summary>
    /// Template optimization recommendations
    /// </summary>
    public List<TemplateRecommendation> Recommendations { get; set; } = new();
}

/// <summary>
/// Template analytics summary
/// </summary>
public class TemplateAnalyticsSummary
{
    /// <summary>
    /// Total number of templates
    /// </summary>
    public long TotalTemplates { get; set; }

    /// <summary>
    /// Total template executions
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Overall success rate across all templates
    /// </summary>
    public double OverallSuccessRate { get; set; }

    /// <summary>
    /// Average quality score
    /// </summary>
    public double AverageQualityScore { get; set; }

    /// <summary>
    /// Most popular template
    /// </summary>
    public string? MostPopularTemplate { get; set; }

    /// <summary>
    /// Best performing template
    /// </summary>
    public string? BestPerformingTemplate { get; set; }

    /// <summary>
    /// Key insights
    /// </summary>
    public List<string> KeyInsights { get; set; } = new();
}

/// <summary>
/// Template performance analytics
/// </summary>
public class TemplatePerformanceAnalytics
{
    /// <summary>
    /// Average response time by template
    /// </summary>
    public Dictionary<string, double> ResponseTimeByTemplate { get; set; } = new();

    /// <summary>
    /// Success rate by template
    /// </summary>
    public Dictionary<string, double> SuccessRateByTemplate { get; set; } = new();

    /// <summary>
    /// Cost efficiency by template
    /// </summary>
    public Dictionary<string, decimal> CostEfficiencyByTemplate { get; set; } = new();

    /// <summary>
    /// Performance benchmarks
    /// </summary>
    public Dictionary<string, PerformanceBenchmark> Benchmarks { get; set; } = new();

    /// <summary>
    /// Performance improvement opportunities
    /// </summary>
    public List<PerformanceImprovement> ImprovementOpportunities { get; set; } = new();
}

/// <summary>
/// Template quality analytics
/// </summary>
public class TemplateQualityAnalytics
{
    /// <summary>
    /// Quality scores by template
    /// </summary>
    public Dictionary<string, double> QualityScoreByTemplate { get; set; } = new();

    /// <summary>
    /// Quality distribution
    /// </summary>
    public Dictionary<string, long> QualityDistribution { get; set; } = new();

    /// <summary>
    /// Quality trends over time
    /// </summary>
    public List<QualityTrendPoint>? QualityTrends { get; set; }

    /// <summary>
    /// Quality improvement recommendations
    /// </summary>
    public List<QualityImprovement> QualityImprovements { get; set; } = new();
}

/// <summary>
/// Variable effectiveness analytics
/// </summary>
public class VariableEffectivenessAnalytics
{
    /// <summary>
    /// Variable usage frequency
    /// </summary>
    public Dictionary<string, long> VariableUsageFrequency { get; set; } = new();

    /// <summary>
    /// Variable effectiveness scores
    /// </summary>
    public Dictionary<string, double> VariableEffectivenessScores { get; set; } = new();

    /// <summary>
    /// Most effective variables
    /// </summary>
    public List<VariableEffectiveness> MostEffectiveVariables { get; set; } = new();

    /// <summary>
    /// Underperforming variables
    /// </summary>
    public List<VariableEffectiveness> UnderperformingVariables { get; set; } = new();

    /// <summary>
    /// Variable optimization suggestions
    /// </summary>
    public List<VariableOptimizationSuggestion> OptimizationSuggestions { get; set; } = new();
}

/// <summary>
/// Template optimization analytics
/// </summary>
public class TemplateOptimizationAnalytics
{
    /// <summary>
    /// Optimization opportunities by template
    /// </summary>
    public Dictionary<string, List<OptimizationOpportunity>> OpportunitiesByTemplate { get; set; } = new();

    /// <summary>
    /// Automated optimization suggestions
    /// </summary>
    public List<AutomatedOptimizationSuggestion> AutomatedSuggestions { get; set; } = new();

    /// <summary>
    /// A/B testing recommendations
    /// </summary>
    public List<ABTestRecommendation> ABTestRecommendations { get; set; } = new();

    /// <summary>
    /// Template consolidation opportunities
    /// </summary>
    public List<TemplateConsolidationOpportunity> ConsolidationOpportunities { get; set; } = new();
}

// Supporting classes for the analytics data structures

public class TemplateUsageTrendPoint
{
    public DateTime Timestamp { get; set; }
    public long UsageCount { get; set; }
    public double AverageSuccessRate { get; set; }
    public long UniqueUsers { get; set; }
}

public class TemplateUsageMetrics
{
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public long UsageCount { get; set; }
    public double SuccessRate { get; set; }
    public long UniqueUsers { get; set; }
    public DateTime LastUsed { get; set; }
}

public class PerformanceBenchmark
{
    public double CurrentPerformance { get; set; }
    public double BenchmarkValue { get; set; }
    public string ComparisonResult { get; set; } = string.Empty;
    public double ImprovementPotential { get; set; }
}

public class PerformanceImprovement
{
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public string ImprovementType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double ExpectedImpact { get; set; }
    public string Priority { get; set; } = "medium";
}

public class QualityImprovement
{
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public string IssueType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SuggestedFix { get; set; } = string.Empty;
    public double ImpactScore { get; set; }
}

public class VariableEffectiveness
{
    public string VariableName { get; set; } = string.Empty;
    public double EffectivenessScore { get; set; }
    public long UsageCount { get; set; }
    public double AverageImpact { get; set; }
    public List<string> UsageContexts { get; set; } = new();
}

public class VariableOptimizationSuggestion
{
    public string VariableName { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public string Suggestion { get; set; } = string.Empty;
    public double ExpectedImpact { get; set; }
    public string Priority { get; set; } = "medium";
}

public class OptimizationOpportunity
{
    public string OpportunityType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double ImpactScore { get; set; }
    public string ImplementationComplexity { get; set; } = string.Empty;
    public List<string> RequiredActions { get; set; } = new();
}

public class AutomatedOptimizationSuggestion
{
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public string SuggestionType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double ConfidenceScore { get; set; }
    public bool CanAutoImplement { get; set; }
}

public class ABTestRecommendation
{
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public string TestType { get; set; } = string.Empty;
    public string TestDescription { get; set; } = string.Empty;
    public string ExpectedOutcome { get; set; } = string.Empty;
    public double PotentialImpact { get; set; }
}

public class TemplateConsolidationOpportunity
{
    public List<Guid> TemplateIds { get; set; } = new();
    public List<string> TemplateNames { get; set; } = new();
    public string ConsolidationReason { get; set; } = string.Empty;
    public double SimilarityScore { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class TemplateAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public long TotalTemplates { get; set; }
    public long TotalExecutions { get; set; }
    public double AverageSuccessRate { get; set; }
    public double AverageQualityScore { get; set; }
    public double AverageResponseTime { get; set; }
}

public class TemplateRecommendation
{
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? TemplateId { get; set; }
    public string? TemplateName { get; set; }
    public double ExpectedImpact { get; set; }
    public List<string>? ImplementationSteps { get; set; }
}
