namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents structural optimization recommendations for workflow improvement.
/// </summary>
public class StructuralOptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the recommendation type.
    /// </summary>
    public string RecommendationType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommendation description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expected impact level.
    /// </summary>
    public OptimizationImpact Impact { get; set; }

    /// <summary>
    /// Gets or sets the implementation effort required.
    /// </summary>
    public ImplementationEffort Effort { get; set; }

    /// <summary>
    /// Gets or sets additional recommendation details.
    /// </summary>
    public Dictionary<string, object>? Details { get; set; }
}

/// <summary>
/// Represents reliability optimization recommendations for workflow stability.
/// </summary>
public class ReliabilityOptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the reliability issue identified.
    /// </summary>
    public string Issue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommended solution.
    /// </summary>
    public string Solution { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the risk level if not addressed.
    /// </summary>
    public RiskLevel RiskLevel { get; set; }

    /// <summary>
    /// Gets or sets the expected reliability improvement.
    /// </summary>
    public double ExpectedImprovement { get; set; }
}

/// <summary>
/// Represents cost optimization recommendations for workflow efficiency.
/// </summary>
public class CostOptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the cost optimization area.
    /// </summary>
    public string OptimizationArea { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current cost estimate.
    /// </summary>
    public decimal CurrentCost { get; set; }

    /// <summary>
    /// Gets or sets the optimized cost estimate.
    /// </summary>
    public decimal OptimizedCost { get; set; }

    /// <summary>
    /// Gets or sets the potential savings.
    /// </summary>
    public decimal PotentialSavings { get; set; }

    /// <summary>
    /// Gets or sets the optimization strategy.
    /// </summary>
    public string Strategy { get; set; } = string.Empty;
}

/// <summary>
/// Represents scalability optimization recommendations for workflow growth.
/// </summary>
public class ScalabilityOptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the scalability bottleneck identified.
    /// </summary>
    public string Bottleneck { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommended scaling approach.
    /// </summary>
    public string ScalingApproach { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum recommended scale.
    /// </summary>
    public int MaxRecommendedScale { get; set; }

    /// <summary>
    /// Gets or sets scaling considerations.
    /// </summary>
    public List<string>? ScalingConsiderations { get; set; }
}

/// <summary>
/// Represents maintainability optimization recommendations for workflow upkeep.
/// </summary>
public class MaintainabilityOptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the maintainability issue.
    /// </summary>
    public string Issue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recommended improvement.
    /// </summary>
    public string Improvement { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maintenance complexity reduction.
    /// </summary>
    public ComplexityReduction ComplexityReduction { get; set; }

    /// <summary>
    /// Gets or sets the long-term benefits.
    /// </summary>
    public List<string>? LongTermBenefits { get; set; }
}

/// <summary>
/// Represents security optimization recommendations for workflow protection.
/// </summary>
public class SecurityOptimizationRecommendation
{
    /// <summary>
    /// Gets or sets the security vulnerability identified.
    /// </summary>
    public string Vulnerability { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level.
    /// </summary>
    public SecuritySeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the recommended mitigation.
    /// </summary>
    public string Mitigation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets compliance considerations.
    /// </summary>
    public List<string>? ComplianceConsiderations { get; set; }
}

/// <summary>
/// Supporting enums for optimization recommendations.
/// </summary>
public enum OptimizationImpact
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}

public enum ImplementationEffort
{
    Minimal = 0,
    Low = 1,
    Medium = 2,
    High = 3,
    Extensive = 4
}

public enum RiskLevel
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}

public enum ComplexityReduction
{
    None = 0,
    Minimal = 1,
    Moderate = 2,
    Significant = 3,
    Major = 4
}

public enum SecuritySeverity
{
    Info = 0,
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

/// <summary>
/// Represents optimization ROI analysis.
/// </summary>
public class OptimizationRoiAnalysis
{
    /// <summary>
    /// Gets or sets the investment required.
    /// </summary>
    public decimal InvestmentRequired { get; set; }

    /// <summary>
    /// Gets or sets the expected annual savings.
    /// </summary>
    public decimal ExpectedAnnualSavings { get; set; }

    /// <summary>
    /// Gets or sets the payback period in months.
    /// </summary>
    public int PaybackPeriodMonths { get; set; }

    /// <summary>
    /// Gets or sets the ROI percentage.
    /// </summary>
    public double RoiPercentage { get; set; }
}

/// <summary>
/// Represents optimization risk assessment.
/// </summary>
public class OptimizationRiskAssessment
{
    /// <summary>
    /// Gets or sets the overall risk level.
    /// </summary>
    public RiskLevel OverallRisk { get; set; }

    /// <summary>
    /// Gets or sets identified risks.
    /// </summary>
    public List<OptimizationRisk>? IdentifiedRisks { get; set; }

    /// <summary>
    /// Gets or sets mitigation strategies.
    /// </summary>
    public List<string>? MitigationStrategies { get; set; }
}

/// <summary>
/// Represents an individual optimization risk.
/// </summary>
public class OptimizationRisk
{
    /// <summary>
    /// Gets or sets the risk description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the risk severity.
    /// </summary>
    public RiskLevel Severity { get; set; }

    /// <summary>
    /// Gets or sets the probability of occurrence.
    /// </summary>
    public double Probability { get; set; }

    /// <summary>
    /// Gets or sets the potential impact.
    /// </summary>
    public string Impact { get; set; } = string.Empty;
}

/// <summary>
/// Represents optimization implementation roadmap.
/// </summary>
public class OptimizationImplementationRoadmap
{
    /// <summary>
    /// Gets or sets the implementation phases.
    /// </summary>
    public List<ImplementationPhase>? Phases { get; set; }

    /// <summary>
    /// Gets or sets the total timeline.
    /// </summary>
    public TimeSpan TotalTimeline { get; set; }

    /// <summary>
    /// Gets or sets key milestones.
    /// </summary>
    public List<string>? KeyMilestones { get; set; }
}

/// <summary>
/// Represents an implementation phase.
/// </summary>
public class ImplementationPhase
{
    /// <summary>
    /// Gets or sets the phase name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phase description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated duration.
    /// </summary>
    public TimeSpan EstimatedDuration { get; set; }

    /// <summary>
    /// Gets or sets the deliverables.
    /// </summary>
    public List<string>? Deliverables { get; set; }
}

/// <summary>
/// Represents optimization success metrics.
/// </summary>
public class OptimizationSuccessMetrics
{
    /// <summary>
    /// Gets or sets the key performance indicators.
    /// </summary>
    public Dictionary<string, double>? KeyPerformanceIndicators { get; set; }

    /// <summary>
    /// Gets or sets the success criteria.
    /// </summary>
    public List<string>? SuccessCriteria { get; set; }

    /// <summary>
    /// Gets or sets the measurement timeline.
    /// </summary>
    public TimeSpan MeasurementTimeline { get; set; }
}

/// <summary>
/// Represents resource utilization baseline.
/// </summary>
public class ResourceUtilizationBaseline
{
    /// <summary>
    /// Gets or sets the baseline CPU usage.
    /// </summary>
    public double BaselineCpuUsage { get; set; }

    /// <summary>
    /// Gets or sets the baseline memory usage.
    /// </summary>
    public long BaselineMemoryUsage { get; set; }

    /// <summary>
    /// Gets or sets the baseline network usage.
    /// </summary>
    public long BaselineNetworkUsage { get; set; }

    /// <summary>
    /// Gets or sets the baseline execution time.
    /// </summary>
    public TimeSpan BaselineExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the baseline cost.
    /// </summary>
    public decimal BaselineCost { get; set; }
}

/// <summary>
/// Represents resource utilization projection after optimization.
/// </summary>
public class ResourceUtilizationProjection
{
    /// <summary>
    /// Gets or sets the projected CPU usage.
    /// </summary>
    public double ProjectedCpuUsage { get; set; }

    /// <summary>
    /// Gets or sets the projected memory usage.
    /// </summary>
    public long ProjectedMemoryUsage { get; set; }

    /// <summary>
    /// Gets or sets the projected network usage.
    /// </summary>
    public long ProjectedNetworkUsage { get; set; }

    /// <summary>
    /// Gets or sets the projected execution time.
    /// </summary>
    public TimeSpan ProjectedExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the projected cost.
    /// </summary>
    public decimal ProjectedCost { get; set; }

    /// <summary>
    /// Gets or sets the confidence level of projections.
    /// </summary>
    public double ConfidenceLevel { get; set; }
}
