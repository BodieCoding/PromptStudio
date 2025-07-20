namespace PromptStudio.Core.DTOs.Analytics;

// Performance Predictions
public class ResponseTimePrediction
{
    public double PredictedAverageMs { get; set; }
    public double PredictedP95Ms { get; set; }
    public double PredictedP99Ms { get; set; }
    public DateTime PredictionDate { get; set; }
    public double ConfidenceInterval { get; set; }
}

public class ThroughputPrediction
{
    public double PredictedRPS { get; set; }
    public double PredictedPeakRPS { get; set; }
    public DateTime PredictionDate { get; set; }
    public double ConfidenceInterval { get; set; }
}

public class ErrorRatePrediction
{
    public double PredictedErrorRate { get; set; }
    public Dictionary<string, double> ErrorRateByType { get; set; } = new();
    public DateTime PredictionDate { get; set; }
    public double ConfidenceInterval { get; set; }
}

public class ReliabilityPrediction
{
    public double PredictedUptime { get; set; }
    public double PredictedMTTR { get; set; }
    public double PredictedMTBF { get; set; }
    public DateTime PredictionDate { get; set; }
}

// Bottleneck and SLA Predictions
public class PredictedBottleneck
{
    public string BottleneckLocation { get; set; } = string.Empty;
    public string BottleneckType { get; set; } = string.Empty;
    public DateTime PredictedOccurrence { get; set; }
    public double Severity { get; set; }
    public string ImpactDescription { get; set; } = string.Empty;
}

public class SlaViolationPrediction
{
    public string SlaMetric { get; set; } = string.Empty;
    public double PredictedViolationRate { get; set; }
    public DateTime PredictedViolationDate { get; set; }
    public string ImpactAssessment { get; set; } = string.Empty;
}

// Capacity Predictions
public class ResourceCapacityPrediction
{
    public string ResourceType { get; set; } = string.Empty;
    public double CurrentCapacity { get; set; }
    public double PredictedRequiredCapacity { get; set; }
    public DateTime CapacityExhaustionDate { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class StorageCapacityPrediction
{
    public string StorageType { get; set; } = string.Empty;
    public long CurrentSizeBytes { get; set; }
    public long PredictedSizeBytes { get; set; }
    public DateTime CapacityExhaustionDate { get; set; }
    public double GrowthRate { get; set; }
}

public class NetworkCapacityPrediction
{
    public string NetworkSegment { get; set; } = string.Empty;
    public double CurrentBandwidthUtilization { get; set; }
    public double PredictedBandwidthUtilization { get; set; }
    public DateTime CongestionDate { get; set; }
}

public class PredictedScalingRequirement
{
    public string ResourceType { get; set; } = string.Empty;
    public int CurrentInstances { get; set; }
    public int PredictedRequiredInstances { get; set; }
    public DateTime ScalingDate { get; set; }
    public string ScalingTrigger { get; set; } = string.Empty;
}

public class CapacityExhaustionPrediction
{
    public string ResourceType { get; set; } = string.Empty;
    public DateTime ExhaustionDate { get; set; }
    public double ConfidenceLevel { get; set; }
    public string ImpactSeverity { get; set; } = string.Empty;
    public List<string> RecommendedActions { get; set; } = new();
}

// Cost Predictions
public class CostPrediction
{
    public decimal PredictedAmount { get; set; }
    public DateTime PredictionDate { get; set; }
    public double ConfidenceInterval { get; set; }
    public string CostCategory { get; set; } = string.Empty;
    public List<string> CostDrivers { get; set; } = new();
}

public class CostOptimizationPrediction
{
    public decimal PotentialSavings { get; set; }
    public DateTime OptimizationDate { get; set; }
    public string OptimizationStrategy { get; set; } = string.Empty;
    public double SuccessProbability { get; set; }
}

public class BudgetVariancePrediction
{
    public decimal PredictedVariance { get; set; }
    public DateTime VarianceDate { get; set; }
    public string VarianceCategory { get; set; } = string.Empty;
    public double VarianceProbability { get; set; }
}

public class CostSpikePrediction
{
    public decimal PredictedSpikeAmount { get; set; }
    public DateTime SpikeDate { get; set; }
    public string SpikeTrigger { get; set; } = string.Empty;
    public double SpikeProbability { get; set; }
    public List<string> MitigationStrategies { get; set; } = new();
}

// Usage and Trend Predictions
public class UsageTrend
{
    public DateTime TrendDate { get; set; }
    public double UsageValue { get; set; }
    public string TrendDirection { get; set; } = string.Empty;
    public double TrendStrength { get; set; }
}

public class PerformanceTrend
{
    public DateTime TrendDate { get; set; }
    public double PerformanceScore { get; set; }
    public string TrendDirection { get; set; } = string.Empty;
    public Dictionary<string, double> ComponentPerformance { get; set; } = new();
}

public class CostTrend
{
    public DateTime TrendDate { get; set; }
    public decimal CostValue { get; set; }
    public string TrendDirection { get; set; } = string.Empty;
    public double TrendAcceleration { get; set; }
}

public class TechnologyAdoptionTrend
{
    public string TechnologyName { get; set; } = string.Empty;
    public double AdoptionRate { get; set; }
    public DateTime TrendDate { get; set; }
    public string AdoptionStage { get; set; } = string.Empty;
}

public class MarketTrendImpact
{
    public string TrendName { get; set; } = string.Empty;
    public string ImpactArea { get; set; } = string.Empty;
    public double ImpactMagnitude { get; set; }
    public DateTime ExpectedImpactDate { get; set; }
    public string ImpactDescription { get; set; } = string.Empty;
}

// Risk Predictions
public class SystemFailureRisk
{
    public string SystemComponent { get; set; } = string.Empty;
    public double FailureProbability { get; set; }
    public DateTime PredictedFailureDate { get; set; }
    public string FailureType { get; set; } = string.Empty;
    public string ImpactSeverity { get; set; } = string.Empty;
}

public class SecurityRiskPrediction
{
    public string ThreatType { get; set; } = string.Empty;
    public double RiskProbability { get; set; }
    public string VulnerableComponent { get; set; } = string.Empty;
    public string RiskSeverity { get; set; } = string.Empty;
    public List<string> MitigationStrategies { get; set; } = new();
}

public class PerformanceDegradationRisk
{
    public string Component { get; set; } = string.Empty;
    public double DegradationProbability { get; set; }
    public DateTime PredictedOnsetDate { get; set; }
    public double ExpectedImpact { get; set; }
    public string DegradationCause { get; set; } = string.Empty;
}

public class ComplianceRiskPrediction
{
    public string ComplianceFramework { get; set; } = string.Empty;
    public double ViolationProbability { get; set; }
    public string RiskArea { get; set; } = string.Empty;
    public string PotentialViolation { get; set; } = string.Empty;
    public List<string> PreventiveActions { get; set; } = new();
}

public class BusinessContinuityRisk
{
    public string RiskType { get; set; } = string.Empty;
    public double DisruptionProbability { get; set; }
    public string BusinessFunction { get; set; } = string.Empty;
    public decimal EstimatedLoss { get; set; }
    public string RecoveryStrategy { get; set; } = string.Empty;
}

public class OverallRiskAssessment
{
    public double TotalRiskScore { get; set; }
    public string RiskLevel { get; set; } = string.Empty;
    public List<string> HighestRisks { get; set; } = new();
    public List<string> ImmediateActions { get; set; } = new();
    public DateTime AssessmentDate { get; set; }
}
