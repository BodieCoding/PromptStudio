using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive workflow analysis result DTO with detailed structural and performance insights.
/// Provides enterprise-grade workflow analysis capabilities with comprehensive metrics,
/// optimization recommendations, and performance insights for workflow improvement and management.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary analysis result DTO for IWorkflowOrchestrationService workflow analysis operations,
/// providing comprehensive workflow insights with structural analysis, performance metrics,
/// and optimization recommendations for enterprise workflow improvement and management.</para>
/// 
/// <para><strong>Analysis Scope:</strong></para>
/// <para>Multi-dimensional workflow analysis including structural integrity, performance characteristics,
/// resource utilization, execution patterns, and optimization opportunities. Designed for enterprise
/// workflow optimization and continuous improvement initiatives.</para>
/// 
/// <para><strong>Insight Categories:</strong></para>
/// <list type="bullet">
/// <item>Structural analysis and complexity metrics</item>
/// <item>Performance bottleneck identification</item>
/// <item>Resource utilization optimization opportunities</item>
/// <item>Execution pattern analysis and recommendations</item>
/// </list>
/// </remarks>
public class WorkflowAnalysisResult
{
    /// <summary>
    /// Gets or sets the analyzed workflow identifier.
    /// </summary>
    /// <value>The unique identifier of the workflow that was analyzed.</value>
    public int WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the workflow name for identification and reporting.
    /// </summary>
    /// <value>The name of the analyzed workflow for context and reporting.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the analysis timestamp for tracking and versioning.
    /// </summary>
    /// <value>The date and time when the workflow analysis was performed.</value>
    public DateTime AnalysisTimestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the analysis version for tracking and comparison.
    /// </summary>
    /// <value>The version of the workflow that was analyzed for historical comparison.</value>
    public long AnalyzedVersion { get; set; }

    /// <summary>
    /// Gets or sets the overall analysis score for the workflow quality.
    /// </summary>
    /// <value>A numeric score representing the overall quality and efficiency of the workflow.</value>
    public double OverallScore { get; set; }

    /// <summary>
    /// Gets or sets the structural analysis results for workflow architecture evaluation.
    /// </summary>
    /// <value>Comprehensive structural analysis including complexity metrics and architectural insights.</value>
    public StructuralAnalysisResult? StructuralAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the performance analysis results for execution efficiency evaluation.
    /// </summary>
    /// <value>Comprehensive performance analysis including bottlenecks and optimization opportunities.</value>
    public PerformanceAnalysisResult? PerformanceAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the resource utilization analysis for efficiency optimization.
    /// </summary>
    /// <value>Analysis of resource usage patterns and optimization opportunities.</value>
    public ResourceUtilizationAnalysis? ResourceAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the execution pattern analysis for workflow behavior insights.
    /// </summary>
    /// <value>Analysis of execution patterns, success rates, and behavioral characteristics.</value>
    public ExecutionPatternAnalysis? ExecutionPatterns { get; set; }

    /// <summary>
    /// Gets or sets the optimization recommendations for workflow improvement.
    /// </summary>
    /// <value>Collection of actionable recommendations for workflow optimization and improvement.</value>
    public List<OptimizationRecommendation>? OptimizationRecommendations { get; set; }

    /// <summary>
    /// Gets or sets the risk assessment results for workflow reliability evaluation.
    /// </summary>
    /// <value>Assessment of potential risks and reliability concerns for the workflow.</value>
    public RiskAssessmentResult? RiskAssessment { get; set; }

    /// <summary>
    /// Gets or sets the compliance analysis results for regulatory adherence evaluation.
    /// </summary>
    /// <value>Analysis of workflow compliance with organizational and regulatory standards.</value>
    public ComplianceAnalysisResult? ComplianceAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the cost analysis results for financial impact evaluation.
    /// </summary>
    /// <value>Analysis of workflow execution costs and financial optimization opportunities.</value>
    public CostAnalysisResult? CostAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the security analysis results for workflow security evaluation.
    /// </summary>
    /// <value>Analysis of security implications and potential vulnerabilities in the workflow.</value>
    public SecurityAnalysisResult? SecurityAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the scalability analysis results for workflow growth evaluation.
    /// </summary>
    /// <value>Analysis of workflow scalability characteristics and limitations.</value>
    public ScalabilityAnalysisResult? ScalabilityAnalysis { get; set; }

    /// <summary>
    /// Gets or sets additional analysis metadata for extensibility and context.
    /// </summary>
    /// <value>Dictionary of additional analysis properties for workflow-specific insights.</value>
    public Dictionary<string, object>? AdditionalMetadata { get; set; }
}

/// <summary>
/// Represents structural analysis results for workflow architecture evaluation.
/// </summary>
public class StructuralAnalysisResult
{
    /// <summary>
    /// Gets or sets the complexity score for the workflow structure.
    /// </summary>
    /// <value>Numeric score representing the structural complexity of the workflow.</value>
    public double ComplexityScore { get; set; }

    /// <summary>
    /// Gets or sets the total number of nodes in the workflow.
    /// </summary>
    /// <value>Count of all nodes present in the workflow structure.</value>
    public int NodeCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of edges in the workflow.
    /// </summary>
    /// <value>Count of all edges present in the workflow structure.</value>
    public int EdgeCount { get; set; }

    /// <summary>
    /// Gets or sets the number of execution paths in the workflow.
    /// </summary>
    /// <value>Count of distinct execution paths through the workflow.</value>
    public int ExecutionPathCount { get; set; }

    /// <summary>
    /// Gets or sets the maximum depth of the workflow structure.
    /// </summary>
    /// <value>Maximum depth from start to end nodes in the workflow.</value>
    public int MaxDepth { get; set; }

    /// <summary>
    /// Gets or sets the branching factor analysis for the workflow.
    /// </summary>
    /// <value>Analysis of branching patterns and parallel execution opportunities.</value>
    public BranchingAnalysis? BranchingAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the cyclic dependency analysis results.
    /// </summary>
    /// <value>Analysis of potential cycles and dependency issues in the workflow.</value>
    public CyclicAnalysis? CyclicAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the node type distribution analysis.
    /// </summary>
    /// <value>Distribution analysis of different node types in the workflow.</value>
    public NodeTypeDistribution? NodeTypeDistribution { get; set; }
}

/// <summary>
/// Represents performance analysis results for workflow execution efficiency.
/// </summary>
public class PerformanceAnalysisResult
{
    /// <summary>
    /// Gets or sets the performance score for the workflow execution.
    /// </summary>
    /// <value>Numeric score representing the overall performance efficiency of the workflow.</value>
    public double PerformanceScore { get; set; }

    /// <summary>
    /// Gets or sets identified performance bottlenecks in the workflow.
    /// </summary>
    /// <value>Collection of performance bottlenecks that may impact workflow execution.</value>
    public List<PerformanceBottleneck>? Bottlenecks { get; set; }

    /// <summary>
    /// Gets or sets the estimated execution time for the workflow.
    /// </summary>
    /// <value>Predicted execution time based on analysis of workflow structure and historical data.</value>
    public TimeSpan EstimatedExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the parallelization opportunities in the workflow.
    /// </summary>
    /// <value>Analysis of opportunities for parallel execution and performance improvement.</value>
    public ParallelizationAnalysis? ParallelizationOpportunities { get; set; }

    /// <summary>
    /// Gets or sets the execution efficiency metrics for the workflow.
    /// </summary>
    /// <value>Metrics related to execution efficiency and performance characteristics.</value>
    public ExecutionEfficiencyMetrics? EfficiencyMetrics { get; set; }
}

/// <summary>
/// Represents resource utilization analysis for workflow efficiency optimization.
/// </summary>
public class ResourceUtilizationAnalysis
{
    /// <summary>
    /// Gets or sets the resource efficiency score for the workflow.
    /// </summary>
    /// <value>Numeric score representing the efficiency of resource utilization.</value>
    public double ResourceEfficiencyScore { get; set; }

    /// <summary>
    /// Gets or sets the CPU utilization analysis results.
    /// </summary>
    /// <value>Analysis of CPU usage patterns and optimization opportunities.</value>
    public ResourceUtilizationMetrics? CpuUtilization { get; set; }

    /// <summary>
    /// Gets or sets the memory utilization analysis results.
    /// </summary>
    /// <value>Analysis of memory usage patterns and optimization opportunities.</value>
    public ResourceUtilizationMetrics? MemoryUtilization { get; set; }

    /// <summary>
    /// Gets or sets the network utilization analysis results.
    /// </summary>
    /// <value>Analysis of network usage patterns and optimization opportunities.</value>
    public ResourceUtilizationMetrics? NetworkUtilization { get; set; }

    /// <summary>
    /// Gets or sets the storage utilization analysis results.
    /// </summary>
    /// <value>Analysis of storage usage patterns and optimization opportunities.</value>
    public ResourceUtilizationMetrics? StorageUtilization { get; set; }
}

/// <summary>
/// Represents execution pattern analysis for workflow behavior insights.
/// </summary>
public class ExecutionPatternAnalysis
{
    /// <summary>
    /// Gets or sets the execution consistency score for the workflow.
    /// </summary>
    /// <value>Numeric score representing the consistency of workflow executions.</value>
    public double ExecutionConsistencyScore { get; set; }

    /// <summary>
    /// Gets or sets the success rate analysis for workflow executions.
    /// </summary>
    /// <value>Analysis of execution success patterns and failure characteristics.</value>
    public SuccessRateAnalysis? SuccessRateAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the execution timing patterns for the workflow.
    /// </summary>
    /// <value>Analysis of execution timing patterns and scheduling characteristics.</value>
    public ExecutionTimingPatterns? TimingPatterns { get; set; }

    /// <summary>
    /// Gets or sets the failure pattern analysis for reliability insights.
    /// </summary>
    /// <value>Analysis of failure patterns and reliability characteristics.</value>
    public FailurePatternAnalysis? FailurePatterns { get; set; }
}

/// <summary>
/// Represents an optimization recommendation for workflow improvement.
/// </summary>
public class OptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation identifier for tracking and reference.
    /// </summary>
    /// <value>Unique identifier for the optimization recommendation.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation title for identification.
    /// </summary>
    /// <value>Brief title describing the optimization recommendation.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation description with detailed explanation.
    /// </summary>
    /// <value>Detailed description of the recommended optimization and its benefits.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority level for the recommendation.
    /// </summary>
    /// <value>Priority level indicating the importance of implementing this recommendation.</value>
    public RecommendationPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the category of the optimization recommendation.
    /// </summary>
    /// <value>Category classification for the type of optimization recommended.</value>
    public OptimizationCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the estimated impact of implementing the recommendation.
    /// </summary>
    /// <value>Quantitative and qualitative impact assessment for the recommendation.</value>
    public OptimizationImpact? EstimatedImpact { get; set; }

    /// <summary>
    /// Gets or sets the implementation complexity for the recommendation.
    /// </summary>
    /// <value>Assessment of the complexity and effort required to implement the recommendation.</value>
    public ImplementationComplexity ImplementationComplexity { get; set; }

    /// <summary>
    /// Gets or sets the implementation steps for the recommendation.
    /// </summary>
    /// <value>Detailed steps for implementing the optimization recommendation.</value>
    public List<string>? ImplementationSteps { get; set; }
}

/// <summary>
/// Enumeration of optimization categories for recommendation classification.
/// </summary>
public enum OptimizationCategory
{
    /// <summary>Performance optimization</summary>
    Performance = 0,
    /// <summary>Resource utilization optimization</summary>
    ResourceUtilization = 1,
    /// <summary>Cost optimization</summary>
    Cost = 2,
    /// <summary>Reliability optimization</summary>
    Reliability = 3,
    /// <summary>Security optimization</summary>
    Security = 4,
    /// <summary>Maintainability optimization</summary>
    Maintainability = 5,
    /// <summary>Scalability optimization</summary>
    Scalability = 6
}

/// <summary>
/// Enumeration of implementation complexity levels for planning and resource allocation.
/// </summary>
public enum ImplementationComplexity
{
    /// <summary>Low complexity implementation</summary>
    Low = 0,
    /// <summary>Medium complexity implementation</summary>
    Medium = 1,
    /// <summary>High complexity implementation</summary>
    High = 2,
    /// <summary>Very high complexity implementation</summary>
    VeryHigh = 3
}

// Minimal supporting classes to complete the analysis result
/// <summary>Simple risk assessment result for workflow analysis</summary>
public class RiskAssessmentResult
{
    public double RiskScore { get; set; }
    public List<string>? IdentifiedRisks { get; set; }
}

/// <summary>Simple compliance analysis result</summary>
public class ComplianceAnalysisResult
{
    public double ComplianceScore { get; set; }
    public List<string>? ComplianceIssues { get; set; }
}

/// <summary>Simple cost analysis result</summary>
public class CostAnalysisResult
{
    public decimal EstimatedCost { get; set; }
    public List<string>? CostOptimizations { get; set; }
}

/// <summary>Simple security analysis result</summary>
public class SecurityAnalysisResult
{
    public double SecurityScore { get; set; }
    public List<string>? SecurityConcerns { get; set; }
}

/// <summary>Simple scalability analysis result</summary>
public class ScalabilityAnalysisResult
{
    public double ScalabilityScore { get; set; }
    public List<string>? ScalabilityLimitations { get; set; }
}

/// <summary>Simple branching analysis</summary>
public class BranchingAnalysis
{
    public int BranchingFactor { get; set; }
    public int ParallelPaths { get; set; }
}

/// <summary>Simple cyclic analysis</summary>
public class CyclicAnalysis
{
    public bool HasCycles { get; set; }
    public List<string>? CyclicPaths { get; set; }
}

/// <summary>Simple node type distribution</summary>
public class NodeTypeDistribution
{
    public Dictionary<string, int>? NodeTypeCounts { get; set; }
}

/// <summary>Simple performance bottleneck</summary>
public class PerformanceBottleneck
{
    public string NodeId { get; set; } = string.Empty;
    public string BottleneckType { get; set; } = string.Empty;
    public double ImpactScore { get; set; }
}

/// <summary>Simple parallelization analysis</summary>
public class ParallelizationAnalysis
{
    public int ParallelizableNodes { get; set; }
    public double PotentialSpeedup { get; set; }
}

/// <summary>Simple execution efficiency metrics</summary>
public class ExecutionEfficiencyMetrics
{
    public double EfficiencyScore { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
}

/// <summary>Simple resource utilization metrics</summary>
public class ResourceUtilizationMetrics
{
    public double AverageUtilization { get; set; }
    public double PeakUtilization { get; set; }
}

/// <summary>Simple success rate analysis</summary>
public class SuccessRateAnalysis
{
    public double OverallSuccessRate { get; set; }
    public Dictionary<string, double>? NodeSuccessRates { get; set; }
}

/// <summary>Simple execution timing patterns</summary>
public class ExecutionTimingPatterns
{
    public TimeSpan AverageExecutionTime { get; set; }
    public List<string>? TimingInsights { get; set; }
}

/// <summary>Simple failure pattern analysis</summary>
public class FailurePatternAnalysis
{
    public double FailureRate { get; set; }
    public List<string>? CommonFailures { get; set; }
}

/// <summary>Simple optimization impact assessment</summary>
public class OptimizationImpact
{
    public double PerformanceImprovement { get; set; }
    public decimal CostReduction { get; set; }
    public string ImpactDescription { get; set; } = string.Empty;
}
