using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive optimization recommendations result with AI-driven insights and actionable suggestions
/// </summary>
public class OptimizationRecommendationsResult
{
    /// <summary>
    /// Time range for this optimization analysis
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Optimization recommendations summary
    /// </summary>
    public OptimizationRecommendationsSummary Summary { get; set; } = new();

    /// <summary>
    /// Performance optimization recommendations
    /// </summary>
    public PerformanceOptimizationRecommendations Performance { get; set; } = new();

    /// <summary>
    /// Cost optimization recommendations
    /// </summary>
    public CostOptimizationRecommendations Cost { get; set; } = new();

    /// <summary>
    /// Resource optimization recommendations
    /// </summary>
    public ResourceOptimizationRecommendations Resource { get; set; } = new();

    /// <summary>
    /// Workflow optimization recommendations
    /// </summary>
    public WorkflowOptimizationRecommendations Workflow { get; set; } = new();

    /// <summary>
    /// Security optimization recommendations
    /// </summary>
    public SecurityOptimizationRecommendations Security { get; set; } = new();

    /// <summary>
    /// Architecture optimization recommendations
    /// </summary>
    public ArchitectureOptimizationRecommendations Architecture { get; set; } = new();

    /// <summary>
    /// Operational optimization recommendations
    /// </summary>
    public OperationalOptimizationRecommendations Operational { get; set; } = new();

    /// <summary>
    /// Prioritized list of all recommendations
    /// </summary>
    public List<OptimizationRecommendation> PrioritizedRecommendations { get; set; } = new();

    /// <summary>
    /// Implementation roadmap
    /// </summary>
    public OptimizationImplementationRoadmap ImplementationRoadmap { get; set; } = new();
}

/// <summary>
/// Optimization recommendations summary
/// </summary>
public class OptimizationRecommendationsSummary
{
    /// <summary>
    /// Total number of recommendations
    /// </summary>
    public int TotalRecommendations { get; set; }

    /// <summary>
    /// High priority recommendations count
    /// </summary>
    public int HighPriorityRecommendations { get; set; }

    /// <summary>
    /// Medium priority recommendations count
    /// </summary>
    public int MediumPriorityRecommendations { get; set; }

    /// <summary>
    /// Low priority recommendations count
    /// </summary>
    public int LowPriorityRecommendations { get; set; }

    /// <summary>
    /// Total potential cost savings
    /// </summary>
    public decimal TotalPotentialSavings { get; set; }

    /// <summary>
    /// Potential performance improvement percentage
    /// </summary>
    public double PotentialPerformanceImprovement { get; set; }

    /// <summary>
    /// Estimated implementation effort (hours)
    /// </summary>
    public double EstimatedImplementationEffort { get; set; }

    /// <summary>
    /// ROI estimation
    /// </summary>
    public double EstimatedRoi { get; set; }

    /// <summary>
    /// Top optimization categories
    /// </summary>
    public List<string> TopOptimizationCategories { get; set; } = new();

    /// <summary>
    /// Quick wins (low effort, high impact)
    /// </summary>
    public List<OptimizationRecommendation> QuickWins { get; set; } = new();

    /// <summary>
    /// Key optimization insights
    /// </summary>
    public List<string> KeyInsights { get; set; } = new();
}

/// <summary>
/// Performance optimization recommendations
/// </summary>
public class PerformanceOptimizationRecommendations
{
    /// <summary>
    /// Response time optimization recommendations
    /// </summary>
    public List<ResponseTimeOptimization> ResponseTimeOptimizations { get; set; } = new();

    /// <summary>
    /// Throughput optimization recommendations
    /// </summary>
    public List<ThroughputOptimization> ThroughputOptimizations { get; set; } = new();

    /// <summary>
    /// Resource utilization optimization recommendations
    /// </summary>
    public List<ResourceUtilizationOptimization> ResourceUtilizationOptimizations { get; set; } = new();

    /// <summary>
    /// Caching optimization recommendations
    /// </summary>
    public List<CachingOptimization> CachingOptimizations { get; set; } = new();

    /// <summary>
    /// Database optimization recommendations
    /// </summary>
    public List<DatabaseOptimization> DatabaseOptimizations { get; set; } = new();

    /// <summary>
    /// API optimization recommendations
    /// </summary>
    public List<ApiOptimization> ApiOptimizations { get; set; } = new();

    /// <summary>
    /// Model inference optimization recommendations
    /// </summary>
    public List<ModelInferenceOptimization> ModelInferenceOptimizations { get; set; } = new();
}

/// <summary>
/// Cost optimization recommendations
/// </summary>
public class CostOptimizationRecommendations
{
    /// <summary>
    /// Infrastructure cost optimizations
    /// </summary>
    public List<InfrastructureCostOptimization> InfrastructureOptimizations { get; set; } = new();

    /// <summary>
    /// LLM provider cost optimizations
    /// </summary>
    public List<LlmProviderCostOptimization> LlmProviderOptimizations { get; set; } = new();

    /// <summary>
    /// Storage cost optimizations
    /// </summary>
    public List<StorageCostOptimization> StorageOptimizations { get; set; } = new();

    /// <summary>
    /// Compute cost optimizations
    /// </summary>
    public List<ComputeCostOptimization> ComputeOptimizations { get; set; } = new();

    /// <summary>
    /// Network cost optimizations
    /// </summary>
    public List<NetworkCostOptimization> NetworkOptimizations { get; set; } = new();

    /// <summary>
    /// License cost optimizations
    /// </summary>
    public List<LicenseCostOptimization> LicenseOptimizations { get; set; } = new();

    /// <summary>
    /// Resource rightsizing recommendations
    /// </summary>
    public List<ResourceRightsizingRecommendation> RightsizingRecommendations { get; set; } = new();
}

/// <summary>
/// Resource optimization recommendations
/// </summary>
public class ResourceOptimizationRecommendations
{
    /// <summary>
    /// CPU optimization recommendations
    /// </summary>
    public List<CpuOptimization> CpuOptimizations { get; set; } = new();

    /// <summary>
    /// Memory optimization recommendations
    /// </summary>
    public List<MemoryOptimization> MemoryOptimizations { get; set; } = new();

    /// <summary>
    /// Storage optimization recommendations
    /// </summary>
    public List<StorageOptimization> StorageOptimizations { get; set; } = new();

    /// <summary>
    /// Network optimization recommendations
    /// </summary>
    public List<NetworkOptimization> NetworkOptimizations { get; set; } = new();

    /// <summary>
    /// Auto-scaling optimization recommendations
    /// </summary>
    public List<AutoScalingOptimization> AutoScalingOptimizations { get; set; } = new();

    /// <summary>
    /// Load balancing optimization recommendations
    /// </summary>
    public List<LoadBalancingOptimization> LoadBalancingOptimizations { get; set; } = new();
}

/// <summary>
/// Workflow optimization recommendations
/// </summary>
public class WorkflowOptimizationRecommendations
{
    /// <summary>
    /// Process automation recommendations
    /// </summary>
    public List<ProcessAutomationRecommendation> ProcessAutomations { get; set; } = new();

    /// <summary>
    /// Parallel processing recommendations
    /// </summary>
    public List<ParallelProcessingRecommendation> ParallelProcessingRecommendations { get; set; } = new();

    /// <summary>
    /// Workflow simplification recommendations
    /// </summary>
    public List<WorkflowSimplificationRecommendation> WorkflowSimplifications { get; set; } = new();

    /// <summary>
    /// Bottleneck elimination recommendations
    /// </summary>
    public List<BottleneckEliminationRecommendation> BottleneckEliminations { get; set; } = new();

    /// <summary>
    /// Integration optimization recommendations
    /// </summary>
    public List<IntegrationOptimization> IntegrationOptimizations { get; set; } = new();
}

/// <summary>
/// Security optimization recommendations
/// </summary>
public class SecurityOptimizationRecommendations
{
    /// <summary>
    /// Access control optimizations
    /// </summary>
    public List<AccessControlOptimization> AccessControlOptimizations { get; set; } = new();

    /// <summary>
    /// Encryption optimizations
    /// </summary>
    public List<EncryptionOptimization> EncryptionOptimizations { get; set; } = new();

    /// <summary>
    /// Audit and compliance optimizations
    /// </summary>
    public List<ComplianceOptimization> ComplianceOptimizations { get; set; } = new();

    /// <summary>
    /// Vulnerability management optimizations
    /// </summary>
    public List<VulnerabilityManagementOptimization> VulnerabilityOptimizations { get; set; } = new();

    /// <summary>
    /// Security monitoring optimizations
    /// </summary>
    public List<SecurityMonitoringOptimization> SecurityMonitoringOptimizations { get; set; } = new();
}

/// <summary>
/// Architecture optimization recommendations
/// </summary>
public class ArchitectureOptimizationRecommendations
{
    /// <summary>
    /// Microservices architecture optimizations
    /// </summary>
    public List<MicroservicesOptimization> MicroservicesOptimizations { get; set; } = new();

    /// <summary>
    /// Service mesh optimizations
    /// </summary>
    public List<ServiceMeshOptimization> ServiceMeshOptimizations { get; set; } = new();

    /// <summary>
    /// API design optimizations
    /// </summary>
    public List<ApiDesignOptimization> ApiDesignOptimizations { get; set; } = new();

    /// <summary>
    /// Data architecture optimizations
    /// </summary>
    public List<DataArchitectureOptimization> DataArchitectureOptimizations { get; set; } = new();

    /// <summary>
    /// Scalability architecture optimizations
    /// </summary>
    public List<ScalabilityArchitectureOptimization> ScalabilityOptimizations { get; set; } = new();
}

/// <summary>
/// Operational optimization recommendations
/// </summary>
public class OperationalOptimizationRecommendations
{
    /// <summary>
    /// Monitoring and alerting optimizations
    /// </summary>
    public List<MonitoringOptimization> MonitoringOptimizations { get; set; } = new();

    /// <summary>
    /// Deployment pipeline optimizations
    /// </summary>
    public List<DeploymentOptimization> DeploymentOptimizations { get; set; } = new();

    /// <summary>
    /// Backup and disaster recovery optimizations
    /// </summary>
    public List<BackupOptimization> BackupOptimizations { get; set; } = new();

    /// <summary>
    /// Log management optimizations
    /// </summary>
    public List<LogManagementOptimization> LogManagementOptimizations { get; set; } = new();

    /// <summary>
    /// Configuration management optimizations
    /// </summary>
    public List<ConfigurationManagementOptimization> ConfigurationOptimizations { get; set; } = new();
}

/// <summary>
/// Optimization scope enumeration
/// </summary>
public enum OptimizationScope
{
    System,
    Application,
    Service,
    Component,
    Resource,
    Process,
    Workflow,
    Configuration,
    Architecture,
    Infrastructure,
    Security,
    Performance,
    Cost,
    Operational
}

/// <summary>
/// Optimization analysis options
/// </summary>
public class OptimizationAnalysisOptions
{
    /// <summary>
    /// Optimization scopes to analyze
    /// </summary>
    public List<OptimizationScope>? OptimizationScopes { get; set; }

    /// <summary>
    /// Minimum potential savings to include ($)
    /// </summary>
    public decimal? MinPotentialSavings { get; set; }

    /// <summary>
    /// Minimum potential performance improvement (%)
    /// </summary>
    public double? MinPerformanceImprovement { get; set; }

    /// <summary>
    /// Maximum implementation effort (hours)
    /// </summary>
    public double? MaxImplementationEffort { get; set; }

    /// <summary>
    /// Minimum ROI threshold
    /// </summary>
    public double? MinRoiThreshold { get; set; }

    /// <summary>
    /// Include quick wins only
    /// </summary>
    public bool QuickWinsOnly { get; set; } = false;

    /// <summary>
    /// Priority levels to include
    /// </summary>
    public List<string>? PriorityLevels { get; set; }

    /// <summary>
    /// Include implementation roadmap
    /// </summary>
    public bool IncludeImplementationRoadmap { get; set; } = true;

    /// <summary>
    /// Include detailed analysis
    /// </summary>
    public bool IncludeDetailedAnalysis { get; set; } = false;

    /// <summary>
    /// Custom analysis parameters
    /// </summary>
    public Dictionary<string, object>? CustomParameters { get; set; }
}

/// <summary>
/// Base optimization recommendation
/// </summary>
public class OptimizationRecommendation
{
    /// <summary>
    /// Unique identifier for the recommendation
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Recommendation category
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Optimization scope
    /// </summary>
    public OptimizationScope Scope { get; set; }

    /// <summary>
    /// Priority level (high, medium, low)
    /// </summary>
    public string Priority { get; set; } = "medium";

    /// <summary>
    /// Recommendation title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Current state description
    /// </summary>
    public string CurrentState { get; set; } = string.Empty;

    /// <summary>
    /// Recommended state description
    /// </summary>
    public string RecommendedState { get; set; } = string.Empty;

    /// <summary>
    /// Potential cost savings
    /// </summary>
    public decimal PotentialSavings { get; set; }

    /// <summary>
    /// Potential performance improvement (%)
    /// </summary>
    public double PotentialPerformanceImprovement { get; set; }

    /// <summary>
    /// Implementation effort estimate (hours)
    /// </summary>
    public double ImplementationEffort { get; set; }

    /// <summary>
    /// Implementation complexity (low, medium, high)
    /// </summary>
    public string ImplementationComplexity { get; set; } = "medium";

    /// <summary>
    /// Expected ROI
    /// </summary>
    public double ExpectedRoi { get; set; }

    /// <summary>
    /// Implementation steps
    /// </summary>
    public List<string> ImplementationSteps { get; set; } = new();

    /// <summary>
    /// Prerequisites for implementation
    /// </summary>
    public List<string> Prerequisites { get; set; } = new();

    /// <summary>
    /// Potential risks
    /// </summary>
    public List<string> Risks { get; set; } = new();

    /// <summary>
    /// Success metrics
    /// </summary>
    public List<string> SuccessMetrics { get; set; } = new();

    /// <summary>
    /// Recommended timeline for implementation
    /// </summary>
    public string RecommendedTimeline { get; set; } = string.Empty;

    /// <summary>
    /// Impact score (0-100)
    /// </summary>
    public double ImpactScore { get; set; }

    /// <summary>
    /// Confidence level in the recommendation (0-100)
    /// </summary>
    public double ConfidenceLevel { get; set; }

    /// <summary>
    /// Related recommendations
    /// </summary>
    public List<Guid> RelatedRecommendations { get; set; } = new();

    /// <summary>
    /// Supporting data and metrics
    /// </summary>
    public Dictionary<string, object>? SupportingData { get; set; }

    /// <summary>
    /// Tags for categorization
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Last updated timestamp
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Implementation roadmap for optimization recommendations
/// </summary>
public class OptimizationImplementationRoadmap
{
    /// <summary>
    /// Roadmap phases
    /// </summary>
    public List<ImplementationPhase> Phases { get; set; } = new();

    /// <summary>
    /// Total estimated duration (days)
    /// </summary>
    public int TotalEstimatedDurationDays { get; set; }

    /// <summary>
    /// Total estimated cost
    /// </summary>
    public decimal TotalEstimatedCost { get; set; }

    /// <summary>
    /// Total expected savings
    /// </summary>
    public decimal TotalExpectedSavings { get; set; }

    /// <summary>
    /// Overall ROI
    /// </summary>
    public double OverallRoi { get; set; }

    /// <summary>
    /// Resource requirements
    /// </summary>
    public Dictionary<string, int> ResourceRequirements { get; set; } = new();

    /// <summary>
    /// Critical path recommendations
    /// </summary>
    public List<Guid> CriticalPathRecommendations { get; set; } = new();

    /// <summary>
    /// Dependencies between recommendations
    /// </summary>
    public List<RecommendationDependency> Dependencies { get; set; } = new();
}

// Supporting classes for specific optimization types

public class ResponseTimeOptimization : OptimizationRecommendation
{
    public double CurrentResponseTimeMs { get; set; }
    public double TargetResponseTimeMs { get; set; }
    public double ExpectedImprovementMs { get; set; }
    public List<string> OptimizationTechniques { get; set; } = new();
}

public class ThroughputOptimization : OptimizationRecommendation
{
    public double CurrentThroughputRPS { get; set; }
    public double TargetThroughputRPS { get; set; }
    public double ExpectedImprovementRPS { get; set; }
    public List<string> ScalingStrategies { get; set; } = new();
}

public class ImplementationPhase
{
    public int PhaseNumber { get; set; }
    public string PhaseName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Guid> RecommendationIds { get; set; } = new();
    public int EstimatedDurationDays { get; set; }
    public decimal EstimatedCost { get; set; }
    public decimal ExpectedSavings { get; set; }
    public List<string> Deliverables { get; set; } = new();
    public List<string> Milestones { get; set; } = new();
}

public class RecommendationDependency
{
    public Guid DependentRecommendationId { get; set; }
    public Guid PrerequisiteRecommendationId { get; set; }
    public string DependencyType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

// Additional specific optimization classes would be defined here
// (Similar pattern to ResponseTimeOptimization and ThroughputOptimization)
// Including: ResourceUtilizationOptimization, CachingOptimization, DatabaseOptimization, etc.
