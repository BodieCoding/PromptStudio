using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive resource analytics result with utilization metrics and optimization insights
/// </summary>
public class ResourceAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Resource analytics summary
    /// </summary>
    public ResourceAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// Resource utilization analytics
    /// </summary>
    public ResourceUtilizationAnalytics Utilization { get; set; } = new();

    /// <summary>
    /// Resource performance analytics
    /// </summary>
    public ResourcePerformanceAnalytics Performance { get; set; } = new();

    /// <summary>
    /// Resource capacity analytics
    /// </summary>
    public ResourceCapacityAnalytics Capacity { get; set; } = new();

    /// <summary>
    /// Resource efficiency analytics
    /// </summary>
    public ResourceEfficiencyAnalytics Efficiency { get; set; } = new();

    /// <summary>
    /// Resource cost analytics
    /// </summary>
    public ResourceCostAnalytics Cost { get; set; } = new();

    /// <summary>
    /// Time-series data for resource metrics
    /// </summary>
    public List<ResourceAnalyticsTimePoint>? TimeSeries { get; set; }

    /// <summary>
    /// Resource optimization recommendations
    /// </summary>
    public List<ResourceOptimizationRecommendation> Recommendations { get; set; } = new();
}

/// <summary>
/// Resource analytics summary
/// </summary>
public class ResourceAnalyticsSummary
{
    /// <summary>
    /// Total number of resources
    /// </summary>
    public long TotalResources { get; set; }

    /// <summary>
    /// Active resources count
    /// </summary>
    public long ActiveResources { get; set; }

    /// <summary>
    /// Idle resources count
    /// </summary>
    public long IdleResources { get; set; }

    /// <summary>
    /// Overall resource utilization percentage
    /// </summary>
    public double OverallUtilization { get; set; }

    /// <summary>
    /// Resource efficiency score (0-100)
    /// </summary>
    public double ResourceEfficiencyScore { get; set; }

    /// <summary>
    /// Most utilized resource type
    /// </summary>
    public string? MostUtilizedResourceType { get; set; }

    /// <summary>
    /// Least utilized resource type
    /// </summary>
    public string? LeastUtilizedResourceType { get; set; }

    /// <summary>
    /// Total resource cost
    /// </summary>
    public decimal TotalResourceCost { get; set; }

    /// <summary>
    /// Resource optimization potential savings
    /// </summary>
    public decimal OptimizationSavings { get; set; }

    /// <summary>
    /// Key resource insights
    /// </summary>
    public List<string> KeyInsights { get; set; } = new();
}

/// <summary>
/// Resource utilization analytics
/// </summary>
public class ResourceUtilizationAnalytics
{
    /// <summary>
    /// CPU utilization analytics
    /// </summary>
    public CpuUtilizationAnalytics Cpu { get; set; } = new();

    /// <summary>
    /// Memory utilization analytics
    /// </summary>
    public MemoryUtilizationAnalytics Memory { get; set; } = new();

    /// <summary>
    /// Storage utilization analytics
    /// </summary>
    public StorageUtilizationAnalytics Storage { get; set; } = new();

    /// <summary>
    /// Network utilization analytics
    /// </summary>
    public NetworkUtilizationAnalytics Network { get; set; } = new();

    /// <summary>
    /// GPU utilization analytics (if applicable)
    /// </summary>
    public GpuUtilizationAnalytics? Gpu { get; set; }

    /// <summary>
    /// Utilization by resource type
    /// </summary>
    public Dictionary<string, ResourceTypeUtilization> ByResourceType { get; set; } = new();

    /// <summary>
    /// Utilization by provider
    /// </summary>
    public Dictionary<string, ProviderUtilization> ByProvider { get; set; } = new();

    /// <summary>
    /// Peak utilization periods
    /// </summary>
    public List<PeakUtilizationPeriod> PeakPeriods { get; set; } = new();
}

/// <summary>
/// Resource performance analytics
/// </summary>
public class ResourcePerformanceAnalytics
{
    /// <summary>
    /// Response time analytics
    /// </summary>
    public ResponseTimeAnalytics ResponseTime { get; set; } = new();

    /// <summary>
    /// Throughput analytics
    /// </summary>
    public ThroughputAnalytics Throughput { get; set; } = new();

    /// <summary>
    /// Error rate analytics
    /// </summary>
    public ErrorRateAnalytics ErrorRate { get; set; } = new();

    /// <summary>
    /// Availability analytics
    /// </summary>
    public AvailabilityAnalytics Availability { get; set; } = new();

    /// <summary>
    /// Performance by resource type
    /// </summary>
    public Dictionary<string, ResourceTypePerformance> ByResourceType { get; set; } = new();

    /// <summary>
    /// SLA compliance analytics
    /// </summary>
    public SlaComplianceAnalytics SlaCompliance { get; set; } = new();
}

/// <summary>
/// Resource capacity analytics
/// </summary>
public class ResourceCapacityAnalytics
{
    /// <summary>
    /// Current capacity utilization
    /// </summary>
    public double CurrentCapacityUtilization { get; set; }

    /// <summary>
    /// Projected capacity utilization
    /// </summary>
    public double ProjectedCapacityUtilization { get; set; }

    /// <summary>
    /// Capacity forecasting
    /// </summary>
    public CapacityForecastAnalysis Forecasting { get; set; } = new();

    /// <summary>
    /// Capacity constraints
    /// </summary>
    public List<CapacityConstraint> Constraints { get; set; } = new();

    /// <summary>
    /// Scaling recommendations
    /// </summary>
    public List<ScalingRecommendation> ScalingRecommendations { get; set; } = new();

    /// <summary>
    /// Capacity planning insights
    /// </summary>
    public CapacityPlanningInsights PlanningInsights { get; set; } = new();
}

/// <summary>
/// Resource efficiency analytics
/// </summary>
public class ResourceEfficiencyAnalytics
{
    /// <summary>
    /// Efficiency metrics by resource
    /// </summary>
    public Dictionary<string, ResourceEfficiencyMetrics> ByResource { get; set; } = new();

    /// <summary>
    /// Resource rightsizing opportunities
    /// </summary>
    public List<RightsizingOpportunity> RightsizingOpportunities { get; set; } = new();

    /// <summary>
    /// Idle resource identification
    /// </summary>
    public List<IdleResourceAlert> IdleResources { get; set; } = new();

    /// <summary>
    /// Over-provisioned resource alerts
    /// </summary>
    public List<OverProvisionedResourceAlert> OverProvisionedResources { get; set; } = new();

    /// <summary>
    /// Resource consolidation opportunities
    /// </summary>
    public List<ResourceConsolidationOpportunity> ConsolidationOpportunities { get; set; } = new();
}

/// <summary>
/// Resource cost analytics
/// </summary>
public class ResourceCostAnalytics
{
    /// <summary>
    /// Cost by resource type
    /// </summary>
    public Dictionary<string, ResourceTypeCost> CostByResourceType { get; set; } = new();

    /// <summary>
    /// Cost efficiency metrics
    /// </summary>
    public ResourceCostEfficiency CostEfficiency { get; set; } = new();

    /// <summary>
    /// Cost allocation by project
    /// </summary>
    public Dictionary<string, ProjectResourceCost> CostByProject { get; set; } = new();

    /// <summary>
    /// Cost optimization opportunities
    /// </summary>
    public List<ResourceCostOptimization> CostOptimizations { get; set; } = new();

    /// <summary>
    /// Reserved instance analytics
    /// </summary>
    public ReservedInstanceAnalytics ReservedInstances { get; set; } = new();
}

// Supporting classes for resource analytics data structures

public class CpuUtilizationAnalytics
{
    public double AverageUtilization { get; set; }
    public double PeakUtilization { get; set; }
    public double MinUtilization { get; set; }
    public Dictionary<string, double> UtilizationByResource { get; set; } = new();
    public List<CpuBottleneck> Bottlenecks { get; set; } = new();
}

public class MemoryUtilizationAnalytics
{
    public double AverageUtilization { get; set; }
    public double PeakUtilization { get; set; }
    public double MinUtilization { get; set; }
    public Dictionary<string, double> UtilizationByResource { get; set; } = new();
    public List<MemoryPressureAlert> PressureAlerts { get; set; } = new();
}

public class StorageUtilizationAnalytics
{
    public double AverageUtilization { get; set; }
    public double PeakUtilization { get; set; }
    public long TotalCapacityGB { get; set; }
    public long UsedCapacityGB { get; set; }
    public Dictionary<string, StorageTypeUtilization> ByStorageType { get; set; } = new();
    public List<StorageCapacityAlert> CapacityAlerts { get; set; } = new();
}

public class NetworkUtilizationAnalytics
{
    public double AverageBandwidthUtilization { get; set; }
    public double PeakBandwidthUtilization { get; set; }
    public long TotalDataTransferredGB { get; set; }
    public Dictionary<string, double> UtilizationByRegion { get; set; } = new();
    public List<NetworkBottleneck> Bottlenecks { get; set; } = new();
}

public class GpuUtilizationAnalytics
{
    public double AverageUtilization { get; set; }
    public double PeakUtilization { get; set; }
    public Dictionary<string, double> UtilizationByGpuType { get; set; } = new();
    public Dictionary<string, double> MemoryUtilizationByGpu { get; set; } = new();
    public List<GpuBottleneck> Bottlenecks { get; set; } = new();
}

public class ResourceTypeUtilization
{
    public string ResourceType { get; set; } = string.Empty;
    public double AverageUtilization { get; set; }
    public double PeakUtilization { get; set; }
    public long ResourceCount { get; set; }
    public double EfficiencyScore { get; set; }
}

public class ProviderUtilization
{
    public string Provider { get; set; } = string.Empty;
    public double AverageUtilization { get; set; }
    public long ResourceCount { get; set; }
    public decimal TotalCost { get; set; }
    public double CostPerUtilizationPoint { get; set; }
}

public class PeakUtilizationPeriod
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double PeakUtilization { get; set; }
    public string ResourceType { get; set; } = string.Empty;
    public string TriggerReason { get; set; } = string.Empty;
}

public class ResourceTypePerformance
{
    public string ResourceType { get; set; } = string.Empty;
    public double AverageResponseTime { get; set; }
    public double AverageThroughput { get; set; }
    public double ErrorRate { get; set; }
    public double Availability { get; set; }
    public double PerformanceScore { get; set; }
}

public class SlaComplianceAnalytics
{
    public double OverallSlaCompliance { get; set; }
    public Dictionary<string, double> ComplianceByService { get; set; } = new();
    public List<SlaViolation> Violations { get; set; } = new();
}

public class CapacityForecastAnalysis
{
    public List<CapacityForecast> Forecasts { get; set; } = new();
    public DateTime ProjectedCapacityExhaustion { get; set; }
    public double ForecastAccuracy { get; set; }
    public List<string> ForecastAssumptions { get; set; } = new();
}

public class CapacityConstraint
{
    public string ResourceType { get; set; } = string.Empty;
    public string ConstraintType { get; set; } = string.Empty;
    public double CurrentUtilization { get; set; }
    public double ConstraintThreshold { get; set; }
    public DateTime ProjectedConstraintDate { get; set; }
    public string Impact { get; set; } = string.Empty;
}

public class ScalingRecommendation
{
    public string ResourceType { get; set; } = string.Empty;
    public string ScalingAction { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public double ExpectedUtilizationChange { get; set; }
    public decimal CostImpact { get; set; }
    public string Justification { get; set; } = string.Empty;
}

public class CapacityPlanningInsights
{
    public List<string> KeyFindings { get; set; } = new();
    public List<string> Recommendations { get; set; } = new();
    public Dictionary<string, double> GrowthProjections { get; set; } = new();
}

public class ResourceEfficiencyMetrics
{
    public string ResourceId { get; set; } = string.Empty;
    public string ResourceName { get; set; } = string.Empty;
    public double EfficiencyScore { get; set; }
    public double UtilizationRate { get; set; }
    public decimal CostEfficiency { get; set; }
    public List<string> EfficiencyFactors { get; set; } = new();
}

public class IdleResourceAlert
{
    public string ResourceId { get; set; } = string.Empty;
    public string ResourceName { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
    public TimeSpan IdleDuration { get; set; }
    public decimal MonthlyCost { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class OverProvisionedResourceAlert
{
    public string ResourceId { get; set; } = string.Empty;
    public string ResourceName { get; set; } = string.Empty;
    public double CurrentUtilization { get; set; }
    public string RecommendedSize { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
}

public class ResourceConsolidationOpportunity
{
    public List<string> ResourceIds { get; set; } = new();
    public string ConsolidationType { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
    public double ExpectedUtilization { get; set; }
}

public class ResourceTypeCost
{
    public string ResourceType { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public decimal AverageCostPerResource { get; set; }
    public long ResourceCount { get; set; }
    public double CostPercentage { get; set; }
}

public class ResourceCostEfficiency
{
    public decimal CostPerUtilizationPoint { get; set; }
    public Dictionary<string, decimal> EfficiencyByResourceType { get; set; } = new();
    public List<CostInefficiency> Inefficiencies { get; set; } = new();
}

public class ProjectResourceCost
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public Dictionary<string, decimal> CostByResourceType { get; set; } = new();
    public double UtilizationRate { get; set; }
}

public class ResourceCostOptimization
{
    public string OptimizationType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
    public string ImplementationEffort { get; set; } = string.Empty;
    public List<string> AffectedResources { get; set; } = new();
}

public class ReservedInstanceAnalytics
{
    public decimal ReservedInstanceCoverage { get; set; }
    public decimal PotentialSavingsFromReservedInstances { get; set; }
    public List<ReservedInstanceRecommendation> Recommendations { get; set; } = new();
}

// Additional supporting classes
public class CpuBottleneck
{
    public string ResourceId { get; set; } = string.Empty;
    public double UtilizationThreshold { get; set; }
    public DateTime DetectedAt { get; set; }
    public TimeSpan Duration { get; set; }
}

public class MemoryPressureAlert
{
    public string ResourceId { get; set; } = string.Empty;
    public double MemoryPressure { get; set; }
    public DateTime AlertTime { get; set; }
    public string Severity { get; set; } = string.Empty;
}

public class StorageTypeUtilization
{
    public string StorageType { get; set; } = string.Empty;
    public long CapacityGB { get; set; }
    public long UsedGB { get; set; }
    public double UtilizationPercentage { get; set; }
}

public class StorageCapacityAlert
{
    public string StorageId { get; set; } = string.Empty;
    public double CurrentUtilization { get; set; }
    public DateTime ProjectedFull { get; set; }
    public string AlertLevel { get; set; } = string.Empty;
}

public class NetworkBottleneck
{
    public string NetworkSegment { get; set; } = string.Empty;
    public double BandwidthUtilization { get; set; }
    public DateTime DetectedAt { get; set; }
    public string ImpactDescription { get; set; } = string.Empty;
}

public class GpuBottleneck
{
    public string GpuId { get; set; } = string.Empty;
    public double GpuUtilization { get; set; }
    public double MemoryUtilization { get; set; }
    public string BottleneckType { get; set; } = string.Empty;
}

public class DowntimeIncident
{
    public string ResourceId { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public string Cause { get; set; } = string.Empty;
}

public class SlaViolation
{
    public string ServiceName { get; set; } = string.Empty;
    public string SlaMetric { get; set; } = string.Empty;
    public DateTime ViolationTime { get; set; }
    public string ViolationDescription { get; set; } = string.Empty;
}

public class CapacityForecast
{
    public DateTime ForecastDate { get; set; }
    public double ProjectedUtilization { get; set; }
    public string ResourceType { get; set; } = string.Empty;
    public double ConfidenceLevel { get; set; }
}

public class CostInefficiency
{
    public string ResourceId { get; set; } = string.Empty;
    public string InefficiencyType { get; set; } = string.Empty;
    public decimal WastedCost { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class ReservedInstanceRecommendation
{
    public string InstanceType { get; set; } = string.Empty;
    public int RecommendedQuantity { get; set; }
    public string Term { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
    public double UtilizationRequirement { get; set; }
}

public class ResourceAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public long ActiveResources { get; set; }
    public double AverageUtilization { get; set; }
    public double AverageCpuUtilization { get; set; }
    public double AverageMemoryUtilization { get; set; }
    public double AverageStorageUtilization { get; set; }
    public decimal TotalCost { get; set; }
    public double EfficiencyScore { get; set; }
}

public class ResourceOptimizationRecommendation
{
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> AffectedResources { get; set; } = new();
    public decimal PotentialSavings { get; set; }
    public double ExpectedEfficiencyGain { get; set; }
    public string ImplementationComplexity { get; set; } = string.Empty;
    public List<string>? ImplementationSteps { get; set; }
}
