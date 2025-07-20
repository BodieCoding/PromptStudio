namespace PromptStudio.Core.DTOs.Analytics;

// Resource and Infrastructure Optimization Classes
public class ResourceUtilizationOptimization
{
    public string ResourceId { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
    public double CurrentUtilization { get; set; }
    public double OptimalUtilization { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
}

public class CachingOptimization
{
    public string CacheType { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public double CurrentHitRate { get; set; }
    public double ProjectedHitRate { get; set; }
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal ExpectedSavings { get; set; }
}

public class DatabaseOptimization
{
    public string DatabaseType { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public string RecommendedIndexes { get; set; } = string.Empty;
    public string QueryOptimizations { get; set; } = string.Empty;
    public double ExpectedPerformanceGain { get; set; }
}

public class ApiOptimization
{
    public string ApiEndpoint { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public double CurrentResponseTime { get; set; }
    public double ProjectedResponseTime { get; set; }
    public string RecommendedChanges { get; set; } = string.Empty;
}

public class ModelInferenceOptimization
{
    public string ModelId { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty;
    public double CurrentLatency { get; set; }
    public double ProjectedLatency { get; set; }
    public decimal CostImpact { get; set; }
    public string RecommendedConfiguration { get; set; } = string.Empty;
}

// Cost Optimization Classes
public class InfrastructureCostOptimization
{
    public string InfrastructureComponent { get; set; } = string.Empty;
    public decimal CurrentCost { get; set; }
    public decimal OptimizedCost { get; set; }
    public decimal PotentialSavings { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class LlmProviderCostOptimization
{
    public string ProviderId { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
    public decimal CurrentCostPerToken { get; set; }
    public decimal OptimizedCostPerToken { get; set; }
    public string AlternativeProvider { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
}

public class StorageCostOptimization
{
    public string StorageType { get; set; } = string.Empty;
    public long CurrentSize { get; set; }
    public long OptimizedSize { get; set; }
    public decimal CurrentCost { get; set; }
    public decimal OptimizedCost { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class ComputeCostOptimization
{
    public string ComputeResource { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal CurrentCost { get; set; }
    public decimal OptimizedCost { get; set; }
}

public class NetworkCostOptimization
{
    public string NetworkComponent { get; set; } = string.Empty;
    public decimal DataTransferCost { get; set; }
    public decimal OptimizedDataTransferCost { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class LicenseCostOptimization
{
    public string SoftwareLicense { get; set; } = string.Empty;
    public int CurrentLicenseCount { get; set; }
    public int OptimalLicenseCount { get; set; }
    public decimal CurrentCost { get; set; }
    public decimal OptimizedCost { get; set; }
}

// Resource Rightsizing
public class ResourceRightsizingRecommendation
{
    public string ResourceId { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
    public string CurrentSize { get; set; } = string.Empty;
    public string RecommendedSize { get; set; } = string.Empty;
    public decimal CostImpact { get; set; }
    public string Justification { get; set; } = string.Empty;
}

// Performance Optimization Classes
public class CpuOptimization
{
    public double CurrentUtilization { get; set; }
    public double OptimalUtilization { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
    public double ExpectedImprovement { get; set; }
}

public class MemoryOptimization
{
    public double CurrentUtilization { get; set; }
    public double OptimalUtilization { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
    public double ExpectedImprovement { get; set; }
}

public class StorageOptimization
{
    public string StorageType { get; set; } = string.Empty;
    public double CurrentUtilization { get; set; }
    public double OptimalUtilization { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class NetworkOptimization
{
    public string NetworkComponent { get; set; } = string.Empty;
    public double CurrentBandwidth { get; set; }
    public double OptimalBandwidth { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class AutoScalingOptimization
{
    public string ResourceType { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal ExpectedSavings { get; set; }
}

public class LoadBalancingOptimization
{
    public string LoadBalancerType { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public double ExpectedPerformanceGain { get; set; }
}

// Process and Workflow Optimization Classes
public class ProcessAutomationRecommendation
{
    public string ProcessName { get; set; } = string.Empty;
    public string CurrentState { get; set; } = string.Empty;
    public string RecommendedAutomation { get; set; } = string.Empty;
    public double TimesSavings { get; set; }
    public decimal CostSavings { get; set; }
}

public class ParallelProcessingRecommendation
{
    public string ProcessName { get; set; } = string.Empty;
    public string CurrentImplementation { get; set; } = string.Empty;
    public string RecommendedApproach { get; set; } = string.Empty;
    public double ExpectedSpeedup { get; set; }
}

public class WorkflowSimplificationRecommendation
{
    public string WorkflowName { get; set; } = string.Empty;
    public int CurrentSteps { get; set; }
    public int RecommendedSteps { get; set; }
    public string SimplificationActions { get; set; } = string.Empty;
    public double EfficiencyGain { get; set; }
}

public class BottleneckEliminationRecommendation
{
    public string BottleneckLocation { get; set; } = string.Empty;
    public string BottleneckType { get; set; } = string.Empty;
    public string RecommendedSolution { get; set; } = string.Empty;
    public double ExpectedImprovement { get; set; }
}

public class IntegrationOptimization
{
    public string IntegrationType { get; set; } = string.Empty;
    public string CurrentImplementation { get; set; } = string.Empty;
    public string RecommendedApproach { get; set; } = string.Empty;
    public double ExpectedPerformanceGain { get; set; }
}

// Security Optimization Classes
public class AccessControlOptimization
{
    public string AccessMethod { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public string SecurityImpact { get; set; } = string.Empty;
}

public class EncryptionOptimization
{
    public string EncryptionType { get; set; } = string.Empty;
    public string CurrentImplementation { get; set; } = string.Empty;
    public string RecommendedImplementation { get; set; } = string.Empty;
    public double PerformanceImpact { get; set; }
}

public class ComplianceOptimization
{
    public string ComplianceFramework { get; set; } = string.Empty;
    public string CurrentCompliance { get; set; } = string.Empty;
    public string RecommendedActions { get; set; } = string.Empty;
    public string ExpectedOutcome { get; set; } = string.Empty;
}

public class VulnerabilityManagementOptimization
{
    public string VulnerabilityType { get; set; } = string.Empty;
    public string CurrentMitigations { get; set; } = string.Empty;
    public string RecommendedMitigations { get; set; } = string.Empty;
    public string RiskReduction { get; set; } = string.Empty;
}

public class SecurityMonitoringOptimization
{
    public string MonitoringType { get; set; } = string.Empty;
    public string CurrentCoverage { get; set; } = string.Empty;
    public string RecommendedCoverage { get; set; } = string.Empty;
    public string ThreatDetectionImprovement { get; set; } = string.Empty;
}

// Architecture Optimization Classes
public class MicroservicesOptimization
{
    public string ServiceName { get; set; } = string.Empty;
    public string CurrentArchitecture { get; set; } = string.Empty;
    public string RecommendedArchitecture { get; set; } = string.Empty;
    public double ScalabilityImprovement { get; set; }
}

public class ServiceMeshOptimization
{
    public string MeshComponent { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public double PerformanceGain { get; set; }
}

public class ApiDesignOptimization
{
    public string ApiName { get; set; } = string.Empty;
    public string CurrentDesign { get; set; } = string.Empty;
    public string RecommendedDesign { get; set; } = string.Empty;
    public double EfficiencyGain { get; set; }
}

public class DataArchitectureOptimization
{
    public string DataComponent { get; set; } = string.Empty;
    public string CurrentArchitecture { get; set; } = string.Empty;
    public string RecommendedArchitecture { get; set; } = string.Empty;
    public double PerformanceGain { get; set; }
}

public class ScalabilityArchitectureOptimization
{
    public string ArchitectureComponent { get; set; } = string.Empty;
    public string CurrentDesign { get; set; } = string.Empty;
    public string RecommendedDesign { get; set; } = string.Empty;
    public double ScalabilityFactor { get; set; }
}

// Operations Optimization Classes
public class MonitoringOptimization
{
    public string MonitoringTool { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public string VisibilityImprovement { get; set; } = string.Empty;
}

public class DeploymentOptimization
{
    public string DeploymentProcess { get; set; } = string.Empty;
    public string CurrentApproach { get; set; } = string.Empty;
    public string RecommendedApproach { get; set; } = string.Empty;
    public double DeploymentTimeReduction { get; set; }
}

public class BackupOptimization
{
    public string BackupType { get; set; } = string.Empty;
    public string CurrentStrategy { get; set; } = string.Empty;
    public string RecommendedStrategy { get; set; } = string.Empty;
    public decimal CostSavings { get; set; }
}

public class LogManagementOptimization
{
    public string LoggingSystem { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal StorageSavings { get; set; }
}

public class ConfigurationManagementOptimization
{
    public string ConfigurationArea { get; set; } = string.Empty;
    public string CurrentApproach { get; set; } = string.Empty;
    public string RecommendedApproach { get; set; } = string.Empty;
    public double EfficiencyGain { get; set; }
}
