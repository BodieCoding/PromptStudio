using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Comprehensive workflow optimization result DTO with detailed improvement recommendations and impact analysis.
/// Provides enterprise-grade workflow optimization capabilities with comprehensive performance improvements,
/// resource optimization, cost reduction strategies, and actionable recommendations for workflow enhancement.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary optimization result DTO for IWorkflowOrchestrationService optimization operations,
/// providing comprehensive workflow improvement recommendations with detailed impact analysis,
/// implementation guidance, and performance improvement projections for enterprise workflow optimization.</para>
/// 
/// <para><strong>Optimization Scope:</strong></para>
/// <para>Multi-dimensional workflow optimization including performance enhancement, resource utilization
/// improvement, cost optimization, reliability enhancement, and scalability improvements. Designed for
/// enterprise workflow optimization with comprehensive improvement strategies and impact assessments.</para>
/// 
/// <para><strong>Recommendation Categories:</strong></para>
/// <list type="bullet">
/// <item>Performance optimization with execution speed improvements</item>
/// <item>Resource utilization optimization for cost efficiency</item>
/// <item>Structural optimization for maintainability and scalability</item>
/// <item>Reliability enhancement for error reduction and stability</item>
/// </list>
/// </remarks>
public class WorkflowOptimizationResult
{
    /// <summary>
    /// Gets or sets the workflow identifier that was optimized.
    /// </summary>
    /// <value>The unique identifier of the workflow that underwent optimization analysis.</value>
    public int WorkflowId { get; set; }

    /// <summary>
    /// Gets or sets the workflow name for identification and reporting.
    /// </summary>
    /// <value>The name of the workflow that was analyzed for optimization.</value>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optimization analysis timestamp.
    /// </summary>
    /// <value>The date and time when the workflow optimization analysis was performed.</value>
    public DateTime OptimizationAnalysisTimestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the workflow version that was analyzed.
    /// </summary>
    /// <value>The version number of the workflow that was analyzed for optimization.</value>
    public long AnalyzedWorkflowVersion { get; set; }

    /// <summary>
    /// Gets or sets the overall optimization potential score.
    /// </summary>
    /// <value>Numeric score representing the overall potential for workflow optimization (0.0 to 100.0).</value>
    public double OverallOptimizationPotential { get; set; }

    /// <summary>
    /// Gets or sets the current workflow performance baseline metrics.
    /// </summary>
    /// <value>Current performance metrics serving as baseline for optimization improvements.</value>
    public WorkflowPerformanceBaseline? CurrentPerformanceBaseline { get; set; }

    /// <summary>
    /// Gets or sets the projected performance after optimization implementation.
    /// </summary>
    /// <value>Projected performance metrics after implementing optimization recommendations.</value>
    public WorkflowPerformanceProjection? ProjectedPerformanceAfterOptimization { get; set; }

    /// <summary>
    /// Gets or sets the performance optimization recommendations.
    /// </summary>
    /// <value>Collection of recommendations for improving workflow execution performance.</value>
    public List<PerformanceOptimizationRecommendation>? PerformanceOptimizations { get; set; }

    /// <summary>
    /// Gets or sets the resource optimization recommendations.
    /// </summary>
    /// <value>Collection of recommendations for optimizing resource utilization and costs.</value>
    public List<ResourceOptimizationRecommendation>? ResourceOptimizations { get; set; }

    /// <summary>
    /// Gets or sets the structural optimization recommendations.
    /// </summary>
    /// <value>Collection of recommendations for improving workflow structure and architecture.</value>
    public List<StructuralOptimizationRecommendation>? StructuralOptimizations { get; set; }

    /// <summary>
    /// Gets or sets the reliability optimization recommendations.
    /// </summary>
    /// <value>Collection of recommendations for improving workflow reliability and error handling.</value>
    public List<ReliabilityOptimizationRecommendation>? ReliabilityOptimizations { get; set; }

    /// <summary>
    /// Gets or sets the cost optimization recommendations.
    /// </summary>
    /// <value>Collection of recommendations for reducing workflow execution costs.</value>
    public List<CostOptimizationRecommendation>? CostOptimizations { get; set; }

    /// <summary>
    /// Gets or sets the scalability optimization recommendations.
    /// </summary>
    /// <value>Collection of recommendations for improving workflow scalability and capacity.</value>
    public List<ScalabilityOptimizationRecommendation>? ScalabilityOptimizations { get; set; }

    /// <summary>
    /// Gets or sets the maintainability optimization recommendations.
    /// </summary>
    /// <value>Collection of recommendations for improving workflow maintainability and management.</value>
    public List<MaintainabilityOptimizationRecommendation>? MaintainabilityOptimizations { get; set; }

    /// <summary>
    /// Gets or sets the security optimization recommendations.
    /// </summary>
    /// <value>Collection of recommendations for improving workflow security and compliance.</value>
    public List<SecurityOptimizationRecommendation>? SecurityOptimizations { get; set; }

    /// <summary>
    /// Gets or sets the implementation priority matrix for optimization recommendations.
    /// </summary>
    /// <value>Matrix providing implementation priority guidance for optimization recommendations.</value>
    public OptimizationImplementationMatrix? ImplementationMatrix { get; set; }

    /// <summary>
    /// Gets or sets the estimated return on investment for optimization efforts.
    /// </summary>
    /// <value>Financial analysis of the return on investment for implementing optimizations.</value>
    public OptimizationRoiAnalysis? RoiAnalysis { get; set; }

    /// <summary>
    /// Gets or sets the risk assessment for optimization implementations.
    /// </summary>
    /// <value>Risk analysis for potential issues and mitigation strategies for optimizations.</value>
    public OptimizationRiskAssessment? RiskAssessment { get; set; }

    /// <summary>
    /// Gets or sets the implementation roadmap for optimization execution.
    /// </summary>
    /// <value>Structured roadmap for implementing optimization recommendations over time.</value>
    public OptimizationImplementationRoadmap? ImplementationRoadmap { get; set; }

    /// <summary>
    /// Gets or sets the success metrics for measuring optimization effectiveness.
    /// </summary>
    /// <value>Metrics and KPIs for measuring the success of optimization implementations.</value>
    public OptimizationSuccessMetrics? SuccessMetrics { get; set; }

    /// <summary>
    /// Gets or sets the optimization confidence score based on analysis quality.
    /// </summary>
    /// <value>Confidence percentage in the optimization recommendations based on data quality.</value>
    public double OptimizationConfidenceScore { get; set; }

    /// <summary>
    /// Gets or sets custom optimization properties for workflow-specific recommendations.
    /// </summary>
    /// <value>Dictionary of custom optimization properties for workflow-specific optimization guidance.</value>
    public Dictionary<string, object>? CustomOptimizationProperties { get; set; }
}

/// <summary>
/// Represents current workflow performance baseline metrics for optimization comparison.
/// </summary>
public class WorkflowPerformanceBaseline
{
    /// <summary>
    /// Gets or sets the current average execution duration.
    /// </summary>
    /// <value>Current average time taken for workflow execution.</value>
    public TimeSpan CurrentAverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the current success rate percentage.
    /// </summary>
    /// <value>Current percentage of successful workflow executions.</value>
    public double CurrentSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the current resource utilization metrics.
    /// </summary>
    /// <value>Current resource consumption patterns and utilization metrics.</value>
    public ResourceUtilizationBaseline? CurrentResourceUtilization { get; set; }

    /// <summary>
    /// Gets or sets the current cost per execution.
    /// </summary>
    /// <value>Current average cost for each workflow execution.</value>
    public decimal CurrentCostPerExecution { get; set; }

    /// <summary>
    /// Gets or sets the current error rate percentage.
    /// </summary>
    /// <value>Current percentage of workflow executions that encounter errors.</value>
    public double CurrentErrorRate { get; set; }

    /// <summary>
    /// Gets or sets the current throughput rate.
    /// </summary>
    /// <value>Current number of workflow executions completed per unit time.</value>
    public double CurrentThroughput { get; set; }

    /// <summary>
    /// Gets or sets the current quality score.
    /// </summary>
    /// <value>Current overall quality score for workflow execution outcomes.</value>
    public double CurrentQualityScore { get; set; }

    /// <summary>
    /// Gets or sets the current user satisfaction score.
    /// </summary>
    /// <value>Current user satisfaction rating with workflow performance.</value>
    public double CurrentUserSatisfactionScore { get; set; }
}

/// <summary>
/// Represents projected workflow performance after optimization implementation.
/// </summary>
public class WorkflowPerformanceProjection
{
    /// <summary>
    /// Gets or sets the projected average execution duration after optimization.
    /// </summary>
    /// <value>Projected average time for workflow execution after optimization.</value>
    public TimeSpan ProjectedAverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the projected success rate percentage after optimization.
    /// </summary>
    /// <value>Projected percentage of successful workflow executions after optimization.</value>
    public double ProjectedSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the projected resource utilization after optimization.
    /// </summary>
    /// <value>Projected resource consumption patterns after optimization implementation.</value>
    public ResourceUtilizationProjection? ProjectedResourceUtilization { get; set; }

    /// <summary>
    /// Gets or sets the projected cost per execution after optimization.
    /// </summary>
    /// <value>Projected average cost for each workflow execution after optimization.</value>
    public decimal ProjectedCostPerExecution { get; set; }

    /// <summary>
    /// Gets or sets the projected error rate percentage after optimization.
    /// </summary>
    /// <value>Projected percentage of workflow executions that encounter errors after optimization.</value>
    public double ProjectedErrorRate { get; set; }

    /// <summary>
    /// Gets or sets the projected throughput rate after optimization.
    /// </summary>
    /// <value>Projected number of workflow executions completed per unit time after optimization.</value>
    public double ProjectedThroughput { get; set; }

    /// <summary>
    /// Gets or sets the projected quality score after optimization.
    /// </summary>
    /// <value>Projected overall quality score for workflow execution outcomes after optimization.</value>
    public double ProjectedQualityScore { get; set; }

    /// <summary>
    /// Gets or sets the projected user satisfaction score after optimization.
    /// </summary>
    /// <value>Projected user satisfaction rating with workflow performance after optimization.</value>
    public double ProjectedUserSatisfactionScore { get; set; }

    /// <summary>
    /// Gets or sets the confidence level in the projections.
    /// </summary>
    /// <value>Confidence percentage in the accuracy of performance projections.</value>
    public double ProjectionConfidenceLevel { get; set; }
}

/// <summary>
/// Represents a performance optimization recommendation with detailed implementation guidance.
/// </summary>
public class PerformanceOptimizationRecommendation : OptimizationRecommendationBase
{
    /// <summary>
    /// Gets or sets the performance improvement category.
    /// </summary>
    /// <value>Category of performance improvement targeted by this recommendation.</value>
    public PerformanceImprovementCategory ImprovementCategory { get; set; }

    /// <summary>
    /// Gets or sets the estimated execution time reduction.
    /// </summary>
    /// <value>Estimated reduction in workflow execution time from this optimization.</value>
    public TimeSpan? EstimatedExecutionTimeReduction { get; set; }

    /// <summary>
    /// Gets or sets the estimated throughput improvement percentage.
    /// </summary>
    /// <value>Estimated percentage improvement in workflow execution throughput.</value>
    public double? EstimatedThroughputImprovement { get; set; }

    /// <summary>
    /// Gets or sets the specific nodes or components affected by this optimization.
    /// </summary>
    /// <value>Collection of workflow nodes or components that would be modified by this optimization.</value>
    public List<string>? AffectedComponents { get; set; }

    /// <summary>
    /// Gets or sets the performance bottlenecks addressed by this recommendation.
    /// </summary>
    /// <value>Collection of performance bottlenecks that this optimization addresses.</value>
    public List<PerformanceBottleneck>? AddressedBottlenecks { get; set; }
}

/// <summary>
/// Represents a resource optimization recommendation for efficiency improvement.
/// </summary>
public class ResourceOptimizationRecommendation : OptimizationRecommendationBase
{
    /// <summary>
    /// Gets or sets the resource optimization category.
    /// </summary>
    /// <value>Category of resource optimization targeted by this recommendation.</value>
    public ResourceOptimizationCategory OptimizationCategory { get; set; }

    /// <summary>
    /// Gets or sets the estimated CPU usage reduction percentage.
    /// </summary>
    /// <value>Estimated percentage reduction in CPU utilization from this optimization.</value>
    public double? EstimatedCpuReduction { get; set; }

    /// <summary>
    /// Gets or sets the estimated memory usage reduction in bytes.
    /// </summary>
    /// <value>Estimated reduction in memory consumption from this optimization.</value>
    public long? EstimatedMemoryReduction { get; set; }

    /// <summary>
    /// Gets or sets the estimated network usage reduction in bytes.
    /// </summary>
    /// <value>Estimated reduction in network bandwidth consumption from this optimization.</value>
    public long? EstimatedNetworkReduction { get; set; }

    /// <summary>
    /// Gets or sets the estimated storage usage reduction in bytes.
    /// </summary>
    /// <value>Estimated reduction in storage space consumption from this optimization.</value>
    public long? EstimatedStorageReduction { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost savings from resource reduction.
    /// </summary>
    /// <value>Estimated financial savings from reduced resource consumption.</value>
    public decimal? EstimatedCostSavings { get; set; }
}

/// <summary>
/// Base class for optimization recommendations with common properties.
/// </summary>
public abstract class OptimizationRecommendationBase
{
    /// <summary>
    /// Gets or sets the recommendation identifier.
    /// </summary>
    /// <value>Unique identifier for the optimization recommendation.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation title.
    /// </summary>
    /// <value>Brief title describing the optimization recommendation.</value>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation description.
    /// </summary>
    /// <value>Detailed description of the optimization recommendation.</value>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation priority level.
    /// </summary>
    /// <value>Priority level for implementing this optimization recommendation.</value>
    public RecommendationPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the implementation complexity level.
    /// </summary>
    /// <value>Complexity level for implementing this optimization recommendation.</value>
    public ImplementationComplexity ImplementationComplexity { get; set; }

    /// <summary>
    /// Gets or sets the estimated implementation time.
    /// </summary>
    /// <value>Estimated time required to implement this optimization recommendation.</value>
    public TimeSpan? EstimatedImplementationTime { get; set; }

    /// <summary>
    /// Gets or sets the confidence level in this recommendation.
    /// </summary>
    /// <value>Confidence percentage in the effectiveness of this optimization recommendation.</value>
    public double ConfidenceLevel { get; set; }

    /// <summary>
    /// Gets or sets the implementation steps for this recommendation.
    /// </summary>
    /// <value>Detailed steps for implementing this optimization recommendation.</value>
    public List<string>? ImplementationSteps { get; set; }

    /// <summary>
    /// Gets or sets the prerequisites for implementing this recommendation.
    /// </summary>
    /// <value>Prerequisites that must be met before implementing this optimization.</value>
    public List<string>? Prerequisites { get; set; }

    /// <summary>
    /// Gets or sets the potential risks of implementing this recommendation.
    /// </summary>
    /// <value>Collection of potential risks and their mitigation strategies.</value>
    public List<OptimizationRisk>? PotentialRisks { get; set; }

    /// <summary>
    /// Gets or sets the success criteria for measuring recommendation effectiveness.
    /// </summary>
    /// <value>Criteria for determining whether the optimization implementation was successful.</value>
    public List<string>? SuccessCriteria { get; set; }
}

/// <summary>
/// Represents an optimization implementation matrix for priority guidance.
/// </summary>
public class OptimizationImplementationMatrix
{
    /// <summary>
    /// Gets or sets the high-priority, low-complexity optimizations (quick wins).
    /// </summary>
    /// <value>Collection of optimization recommendations that are high priority and low complexity.</value>
    public List<string>? QuickWins { get; set; }

    /// <summary>
    /// Gets or sets the high-priority, high-complexity optimizations (major projects).
    /// </summary>
    /// <value>Collection of optimization recommendations that are high priority but high complexity.</value>
    public List<string>? MajorProjects { get; set; }

    /// <summary>
    /// Gets or sets the low-priority, low-complexity optimizations (fill-in tasks).
    /// </summary>
    /// <value>Collection of optimization recommendations that are low priority and low complexity.</value>
    public List<string>? FillInTasks { get; set; }

    /// <summary>
    /// Gets or sets the low-priority, high-complexity optimizations (questionable value).
    /// </summary>
    /// <value>Collection of optimization recommendations that are low priority and high complexity.</value>
    public List<string>? QuestionableValue { get; set; }

    /// <summary>
    /// Gets or sets the recommended implementation sequence.
    /// </summary>
    /// <value>Ordered sequence of optimization recommendations for implementation.</value>
    public List<string>? RecommendedImplementationSequence { get; set; }
}

/// <summary>
/// Enumeration of performance improvement categories for optimization classification.
/// </summary>
public enum PerformanceImprovementCategory
{
    /// <summary>Execution speed optimization</summary>
    ExecutionSpeed = 0,
    /// <summary>Throughput optimization</summary>
    Throughput = 1,
    /// <summary>Latency reduction</summary>
    LatencyReduction = 2,
    /// <summary>Parallelization improvement</summary>
    Parallelization = 3,
    /// <summary>Caching optimization</summary>
    Caching = 4,
    /// <summary>Algorithm optimization</summary>
    AlgorithmOptimization = 5,
    /// <summary>Data flow optimization</summary>
    DataFlowOptimization = 6,
    /// <summary>Resource pooling</summary>
    ResourcePooling = 7
}

/// <summary>
/// Enumeration of resource optimization categories for resource efficiency.
/// </summary>
public enum ResourceOptimizationCategory
{
    /// <summary>CPU utilization optimization</summary>
    CpuUtilization = 0,
    /// <summary>Memory usage optimization</summary>
    MemoryUsage = 1,
    /// <summary>Network bandwidth optimization</summary>
    NetworkBandwidth = 2,
    /// <summary>Storage utilization optimization</summary>
    StorageUtilization = 3,
    /// <summary>Resource pooling optimization</summary>
    ResourcePooling = 4,
    /// <summary>Resource scheduling optimization</summary>
    ResourceScheduling = 5,
    /// <summary>Resource allocation optimization</summary>
    ResourceAllocation = 6,
    /// <summary>Resource cleanup optimization</summary>
    ResourceCleanup = 7
}
