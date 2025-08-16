using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive workflow analytics result with execution metrics and optimization insights
/// </summary>
public class WorkflowAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Workflow analytics summary
    /// </summary>
    public WorkflowAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// Workflow execution analytics
    /// </summary>
    public WorkflowExecutionAnalytics Execution { get; set; } = new();

    /// <summary>
    /// Workflow efficiency analytics
    /// </summary>
    public WorkflowEfficiencyAnalytics Efficiency { get; set; } = new();

    /// <summary>
    /// Bottleneck analysis
    /// </summary>
    public WorkflowBottleneckAnalysis Bottlenecks { get; set; } = new();

    /// <summary>
    /// Workflow optimization analytics
    /// </summary>
    public WorkflowOptimizationAnalytics Optimization { get; set; } = new();

    /// <summary>
    /// Time-series data for workflow metrics
    /// </summary>
    public List<WorkflowAnalyticsTimePoint>? TimeSeries { get; set; }

    /// <summary>
    /// Workflow optimization recommendations
    /// </summary>
    public List<WorkflowRecommendation> Recommendations { get; set; } = [];
}

/// <summary>
/// Workflow analytics summary
/// </summary>
public class WorkflowAnalyticsSummary
{
    /// <summary>
    /// Total number of workflows
    /// </summary>
    public long TotalWorkflows { get; set; }

    /// <summary>
    /// Total workflow executions
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Overall success rate across all workflows
    /// </summary>
    public double OverallSuccessRate { get; set; }

    /// <summary>
    /// Average execution duration in minutes
    /// </summary>
    public double AverageExecutionDurationMinutes { get; set; }

    /// <summary>
    /// Most efficient workflow
    /// </summary>
    public string? MostEfficientWorkflow { get; set; }

    /// <summary>
    /// Most complex workflow
    /// </summary>
    public string? MostComplexWorkflow { get; set; }

    /// <summary>
    /// Key performance insights
    /// </summary>
    public List<string> KeyInsights { get; set; } = [];
}

/// <summary>
/// Workflow execution analytics
/// </summary>
public class WorkflowExecutionAnalytics
{
    /// <summary>
    /// Execution success rate by workflow
    /// </summary>
    public Dictionary<string, double> SuccessRateByWorkflow { get; set; } = [];

    /// <summary>
    /// Average execution time by workflow
    /// </summary>
    public Dictionary<string, double> ExecutionTimeByWorkflow { get; set; } = [];

    /// <summary>
    /// Execution frequency by workflow
    /// </summary>
    public Dictionary<string, long> ExecutionFrequency { get; set; } = [];

    /// <summary>
    /// Failed executions analysis
    /// </summary>
    public FailedExecutionAnalysis FailedExecutions { get; set; } = new();

    /// <summary>
    /// Peak execution times
    /// </summary>
    public List<PeakExecutionTime> PeakExecutionTimes { get; set; } = [];
}

/// <summary>
/// Workflow efficiency analytics
/// </summary>
public class WorkflowEfficiencyAnalytics
{
    /// <summary>
    /// Efficiency score by workflow (0-100)
    /// </summary>
    public Dictionary<string, double> EfficiencyScoreByWorkflow { get; set; } = [];

    /// <summary>
    /// Resource utilization efficiency
    /// </summary>
    public Dictionary<string, double> ResourceUtilizationEfficiency { get; set; } = [];

    /// <summary>
    /// Time-to-completion optimization opportunities
    /// </summary>
    public List<TimeOptimizationOpportunity> TimeOptimizations { get; set; } = [];

    /// <summary>
    /// Parallel processing opportunities
    /// </summary>
    public List<ParallelProcessingOpportunity> ParallelProcessingOpportunities { get; set; } = [];

    /// <summary>
    /// Automation opportunities
    /// </summary>
    public List<AutomationOpportunity> AutomationOpportunities { get; set; } = [];
}

/// <summary>
/// Workflow bottleneck analysis
/// </summary>
public class WorkflowBottleneckAnalysis
{
    /// <summary>
    /// Identified bottlenecks by workflow
    /// </summary>
    public Dictionary<string, List<WorkflowBottleneck>> BottlenecksByWorkflow { get; set; } = [];

    /// <summary>
    /// Most common bottleneck types
    /// </summary>
    public Dictionary<string, long> CommonBottleneckTypes { get; set; } = [];

    /// <summary>
    /// Bottleneck impact analysis
    /// </summary>
    public List<BottleneckImpactAnalysis> BottleneckImpacts { get; set; } = [];

    /// <summary>
    /// Bottleneck resolution recommendations
    /// </summary>
    public List<BottleneckResolutionRecommendation> ResolutionRecommendations { get; set; } = [];
}

/// <summary>
/// Workflow optimization analytics
/// </summary>
public class WorkflowOptimizationAnalytics
{
    /// <summary>
    /// Optimization opportunities by workflow
    /// </summary>
    public Dictionary<string, List<WorkflowOptimizationOpportunity>> OpportunitiesByWorkflow { get; set; } = [];

    /// <summary>
    /// Design pattern recommendations
    /// </summary>
    public List<DesignPatternRecommendation> DesignPatterns { get; set; } = [];

    /// <summary>
    /// Performance tuning suggestions
    /// </summary>
    public List<PerformanceTuningSuggestion> PerformanceTuning { get; set; } = [];

    /// <summary>
    /// Scalability analysis
    /// </summary>
    public ScalabilityAnalysis Scalability { get; set; } = new();
}

// Supporting classes for the analytics data structures

public class FailedExecutionAnalysis
{
    /// <summary>
    /// Total failed executions
    /// </summary>
    public long TotalFailedExecutions { get; set; }

    /// <summary>
    /// Failure reasons breakdown
    /// </summary>
    public Dictionary<string, long> FailureReasons { get; set; } = [];

    /// <summary>
    /// Most problematic workflows
    /// </summary>
    public List<ProblematicWorkflow> ProblematicWorkflows { get; set; } = [];

    /// <summary>
    /// Failure patterns over time
    /// </summary>
    public List<FailurePatternPoint>? FailurePatterns { get; set; }
}

public class PeakExecutionTime
{
    public string WorkflowId { get; set; } = string.Empty;
    public string WorkflowName { get; set; } = string.Empty;
    public DateTime PeakTime { get; set; }
    public long ExecutionCount { get; set; }
    public string PeakReason { get; set; } = string.Empty;
}

public class TimeOptimizationOpportunity
{
    public Guid WorkflowId { get; set; }
    public string WorkflowName { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public double CurrentDurationMinutes { get; set; }
    public double OptimizedDurationMinutes { get; set; }
    public double TimeSavings { get; set; }
    public string Implementation { get; set; } = string.Empty;
}

public class ParallelProcessingOpportunity
{
    public Guid WorkflowId { get; set; }
    public string WorkflowName { get; set; } = string.Empty;
    public List<string> ParallelizableSteps { get; set; } = [];
    public double ExpectedSpeedup { get; set; }
    public string Implementation { get; set; } = string.Empty;
}

public class AutomationOpportunity
{
    public Guid WorkflowId { get; set; }
    public string WorkflowName { get; set; } = string.Empty;
    public List<string> AutomatableSteps { get; set; } = [];
    public double TimeReduction { get; set; }
    public string AutomationComplexity { get; set; } = string.Empty;
    public double ROI { get; set; }
}

public class WorkflowBottleneck
{
    public string BottleneckType { get; set; } = string.Empty;
    public string StepName { get; set; } = string.Empty;
    public double AverageDelay { get; set; }
    public double ImpactScore { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> SuggestedSolutions { get; set; } = [];
}

public class BottleneckImpactAnalysis
{
    public string BottleneckType { get; set; } = string.Empty;
    public double AverageImpactMinutes { get; set; }
    public long AffectedExecutions { get; set; }
    public decimal CostImpact { get; set; }
    public string BusinessImpact { get; set; } = string.Empty;
}

public class BottleneckResolutionRecommendation
{
    public string BottleneckType { get; set; } = string.Empty;
    public string RecommendationType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double ExpectedImprovement { get; set; }
    public string ImplementationComplexity { get; set; } = string.Empty;
    public List<string> ImplementationSteps { get; set; } = [];
}

public class WorkflowOptimizationOpportunity
{
    public string OpportunityType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double ImpactScore { get; set; }
    public string Priority { get; set; } = "medium";
    public List<string> RequiredActions { get; set; } = [];
}

public class DesignPatternRecommendation
{
    public string PatternName { get; set; } = string.Empty;
    public string PatternType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Guid> ApplicableWorkflows { get; set; } = [];
    public double ExpectedBenefit { get; set; }
    public string UseCaseDescription { get; set; } = string.Empty;
}

public class PerformanceTuningSuggestion
{
    public Guid WorkflowId { get; set; }
    public string WorkflowName { get; set; } = string.Empty;
    public string TuningType { get; set; } = string.Empty;
    public string Suggestion { get; set; } = string.Empty;
    public double ExpectedImpact { get; set; }
    public string DifficultyLevel { get; set; } = string.Empty;
}

public class ScalabilityAnalysis
{
    /// <summary>
    /// Current scalability rating (0-100)
    /// </summary>
    public double ScalabilityRating { get; set; }

    /// <summary>
    /// Scalability limitations
    /// </summary>
    public List<ScalabilityLimitation> Limitations { get; set; } = [];

    /// <summary>
    /// Scalability improvement recommendations
    /// </summary>
    public List<ScalabilityImprovement> Improvements { get; set; } = [];
}

public class ProblematicWorkflow
{
    public Guid WorkflowId { get; set; }
    public string WorkflowName { get; set; } = string.Empty;
    public long FailureCount { get; set; }
    public double FailureRate { get; set; }
    public List<string> CommonFailureReasons { get; set; } = [];
}

public class FailurePatternPoint
{
    public DateTime Timestamp { get; set; }
    public long FailureCount { get; set; }
    public string MostCommonReason { get; set; } = string.Empty;
    public double FailureRate { get; set; }
}

public class ScalabilityLimitation
{
    public string LimitationType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double ImpactScore { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class ScalabilityImprovement
{
    public string ImprovementType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double ExpectedScalabilityGain { get; set; }
    public string ImplementationEffort { get; set; } = string.Empty;
}

public class WorkflowAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public long TotalWorkflows { get; set; }
    public long TotalExecutions { get; set; }
    public double AverageSuccessRate { get; set; }
    public double AverageExecutionDuration { get; set; }
    public double AverageEfficiencyScore { get; set; }
}

public class WorkflowRecommendation
{
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? WorkflowId { get; set; }
    public string? WorkflowName { get; set; }
    public double ExpectedImpact { get; set; }
    public List<string>? ImplementationSteps { get; set; }
}
