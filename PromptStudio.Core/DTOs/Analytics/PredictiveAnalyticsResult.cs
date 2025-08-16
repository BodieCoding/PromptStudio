using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive predictive analytics result with AI-driven insights and forecasts
/// </summary>
public class PredictiveAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Predictive analytics summary
    /// </summary>
    public PredictiveAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// Usage forecasting analytics
    /// </summary>
    public UsageForecastingAnalytics UsageForecasting { get; set; } = new();

    /// <summary>
    /// Performance prediction analytics
    /// </summary>
    public PerformancePredictionAnalytics PerformancePrediction { get; set; } = new();

    /// <summary>
    /// Capacity prediction analytics
    /// </summary>
    public CapacityPredictionAnalytics CapacityPrediction { get; set; } = new();

    /// <summary>
    /// Cost forecasting analytics
    /// </summary>
    public CostForecastingPredictiveAnalytics CostForecasting { get; set; } = new();

    /// <summary>
    /// Anomaly detection analytics
    /// </summary>
    public AnomalyDetectionAnalytics AnomalyDetection { get; set; } = new();

    /// <summary>
    /// Trend analysis and predictions
    /// </summary>
    public TrendPredictionAnalytics TrendPrediction { get; set; } = new();

    /// <summary>
    /// Risk assessment predictions
    /// </summary>
    public RiskPredictionAnalytics RiskPrediction { get; set; } = new();

    /// <summary>
    /// Model confidence metrics
    /// </summary>
    public ModelConfidenceMetrics ModelConfidence { get; set; } = new();

    /// <summary>
    /// Time-series prediction data
    /// </summary>
    public List<PredictiveAnalyticsTimePoint>? TimeSeries { get; set; }

    /// <summary>
    /// Predictive insights and recommendations
    /// </summary>
    public List<PredictiveInsight> Insights { get; set; } = [];
}

/// <summary>
/// Predictive analytics summary
/// </summary>
public class PredictiveAnalyticsSummary
{
    /// <summary>
    /// Overall prediction confidence score (0-100)
    /// </summary>
    public double OverallConfidence { get; set; }

    /// <summary>
    /// Primary prediction types included
    /// </summary>
    public List<PredictionType> PredictionTypes { get; set; } = [];

    /// <summary>
    /// Time horizon for predictions
    /// </summary>
    public PredictionTimeHorizon TimeHorizon { get; set; }

    /// <summary>
    /// Number of models used for predictions
    /// </summary>
    public int ModelCount { get; set; }

    /// <summary>
    /// Key predicted metrics
    /// </summary>
    public Dictionary<string, double> KeyPredictions { get; set; } = [];

    /// <summary>
    /// Prediction accuracy from historical validation
    /// </summary>
    public double HistoricalAccuracy { get; set; }

    /// <summary>
    /// Most significant predicted changes
    /// </summary>
    public List<string> SignificantChanges { get; set; } = [];

    /// <summary>
    /// Prediction risks and uncertainties
    /// </summary>
    public List<string> PredictionRisks { get; set; } = [];
}

/// <summary>
/// Usage forecasting analytics
/// </summary>
public class UsageForecastingAnalytics
{
    /// <summary>
    /// Token usage predictions
    /// </summary>
    public TokenUsagePrediction TokenUsage { get; set; } = new();

    /// <summary>
    /// API call predictions
    /// </summary>
    public ApiCallPrediction ApiCalls { get; set; } = new();

    /// <summary>
    /// User activity predictions
    /// </summary>
    public UserActivityPrediction UserActivity { get; set; } = new();

    /// <summary>
    /// Resource demand predictions
    /// </summary>
    public ResourceDemandPrediction ResourceDemand { get; set; } = new();

    /// <summary>
    /// Seasonal usage patterns
    /// </summary>
    public List<SeasonalUsagePattern> SeasonalPatterns { get; set; } = [];

    /// <summary>
    /// Growth trend predictions
    /// </summary>
    public GrowthTrendPrediction GrowthTrends { get; set; } = new();
}

/// <summary>
/// Performance prediction analytics
/// </summary>
public class PerformancePredictionAnalytics
{
    /// <summary>
    /// Response time predictions
    /// </summary>
    public ResponseTimePrediction ResponseTime { get; set; } = new();

    /// <summary>
    /// Throughput predictions
    /// </summary>
    public ThroughputPrediction Throughput { get; set; } = new();

    /// <summary>
    /// Error rate predictions
    /// </summary>
    public ErrorRatePrediction ErrorRate { get; set; } = new();

    /// <summary>
    /// System reliability predictions
    /// </summary>
    public ReliabilityPrediction Reliability { get; set; } = new();

    /// <summary>
    /// Performance bottleneck predictions
    /// </summary>
    public List<PredictedBottleneck> PredictedBottlenecks { get; set; } = [];

    /// <summary>
    /// SLA violation predictions
    /// </summary>
    public SlaViolationPrediction SlaViolations { get; set; } = new();
}

/// <summary>
/// Capacity prediction analytics
/// </summary>
public class CapacityPredictionAnalytics
{
    /// <summary>
    /// Resource capacity predictions
    /// </summary>
    public ResourceCapacityPrediction ResourceCapacity { get; set; } = new();

    /// <summary>
    /// Storage capacity predictions
    /// </summary>
    public StorageCapacityPrediction StorageCapacity { get; set; } = new();

    /// <summary>
    /// Network capacity predictions
    /// </summary>
    public NetworkCapacityPrediction NetworkCapacity { get; set; } = new();

    /// <summary>
    /// Scaling requirement predictions
    /// </summary>
    public List<PredictedScalingRequirement> ScalingRequirements { get; set; } = [];

    /// <summary>
    /// Capacity exhaustion predictions
    /// </summary>
    public List<CapacityExhaustionPrediction> CapacityExhaustions { get; set; } = [];
}

/// <summary>
/// Cost forecasting predictive analytics
/// </summary>
public class CostForecastingPredictiveAnalytics
{
    /// <summary>
    /// Total cost predictions
    /// </summary>
    public CostPrediction TotalCost { get; set; } = new();

    /// <summary>
    /// Cost by provider predictions
    /// </summary>
    public Dictionary<string, CostPrediction> CostByProvider { get; set; } = [];

    /// <summary>
    /// Cost optimization predictions
    /// </summary>
    public CostOptimizationPrediction Optimization { get; set; } = new();

    /// <summary>
    /// Budget variance predictions
    /// </summary>
    public BudgetVariancePrediction BudgetVariance { get; set; } = new();

    /// <summary>
    /// Cost spike predictions
    /// </summary>
    public List<CostSpikePrediction> CostSpikes { get; set; } = [];
}

/// <summary>
/// Anomaly detection analytics
/// </summary>
public class AnomalyDetectionAnalytics
{
    /// <summary>
    /// Detected anomalies
    /// </summary>
    public List<DetectedAnomaly> DetectedAnomalies { get; set; } = [];

    /// <summary>
    /// Predicted future anomalies
    /// </summary>
    public List<PredictedAnomaly> PredictedAnomalies { get; set; } = [];

    /// <summary>
    /// Anomaly patterns
    /// </summary>
    public List<AnomalyPattern> AnomalyPatterns { get; set; } = [];

    /// <summary>
    /// Anomaly detection model performance
    /// </summary>
    public AnomalyDetectionModelMetrics ModelMetrics { get; set; } = new();
}

/// <summary>
/// Trend prediction analytics
/// </summary>
public class TrendPredictionAnalytics
{
    /// <summary>
    /// Usage trends
    /// </summary>
    public List<UsageTrend> UsageTrends { get; set; } = [];

    /// <summary>
    /// Performance trends
    /// </summary>
    public List<PerformanceTrend> PerformanceTrends { get; set; } = [];

    /// <summary>
    /// Cost trends
    /// </summary>
    public List<CostTrend> CostTrends { get; set; } = [];

    /// <summary>
    /// Technology adoption trends
    /// </summary>
    public List<TechnologyAdoptionTrend> TechnologyTrends { get; set; } = [];

    /// <summary>
    /// Market trend impacts
    /// </summary>
    public List<MarketTrendImpact> MarketTrends { get; set; } = [];
}

/// <summary>
/// Risk prediction analytics
/// </summary>
public class RiskPredictionAnalytics
{
    /// <summary>
    /// System failure risk predictions
    /// </summary>
    public List<SystemFailureRisk> SystemFailureRisks { get; set; } = [];

    /// <summary>
    /// Security risk predictions
    /// </summary>
    public List<SecurityRiskPrediction> SecurityRisks { get; set; } = [];

    /// <summary>
    /// Performance degradation risks
    /// </summary>
    public List<PerformanceDegradationRisk> PerformanceRisks { get; set; } = [];

    /// <summary>
    /// Compliance risk predictions
    /// </summary>
    public List<ComplianceRiskPrediction> ComplianceRisks { get; set; } = [];

    /// <summary>
    /// Business continuity risks
    /// </summary>
    public List<BusinessContinuityRisk> BusinessContinuityRisks { get; set; } = [];

    /// <summary>
    /// Overall risk assessment
    /// </summary>
    public OverallRiskAssessment OverallRisk { get; set; } = new();
}

/// <summary>
/// Model confidence metrics
/// </summary>
public class ModelConfidenceMetrics
{
    /// <summary>
    /// Model accuracy scores
    /// </summary>
    public Dictionary<string, double> ModelAccuracy { get; set; } = [];

    /// <summary>
    /// Prediction confidence intervals
    /// </summary>
    public Dictionary<string, ConfidenceInterval> ConfidenceIntervals { get; set; } = [];

    /// <summary>
    /// Model validation results
    /// </summary>
    public ModelValidationResults ValidationResults { get; set; } = new();

    /// <summary>
    /// Feature importance scores
    /// </summary>
    public Dictionary<string, double> FeatureImportance { get; set; } = [];

    /// <summary>
    /// Model performance metrics
    /// </summary>
    public ModelPerformanceMetrics Performance { get; set; } = new();
}

// Supporting enums and classes

/// <summary>
/// Types of predictions available
/// </summary>
public enum PredictionType
{
    UsageForecasting,
    PerformancePrediction,
    CapacityPlanning,
    CostForecasting,
    AnomalyDetection,
    TrendAnalysis,
    RiskAssessment,
    DemandForecasting,
    ResourceOptimization,
    UserBehavior,
    SeasonalPatterns,
    MarketTrends
}

/// <summary>
/// Time horizons for predictions
/// </summary>
public enum PredictionTimeHorizon
{
    ShortTerm,    // 1-7 days
    MediumTerm,   // 1-4 weeks
    LongTerm,     // 1-12 months
    Extended      // 1+ years
}

/// <summary>
/// Predictive analytics options
/// </summary>
public class PredictiveAnalyticsOptions
{
    /// <summary>
    /// Types of predictions to include
    /// </summary>
    public List<PredictionType>? PredictionTypes { get; set; }

    /// <summary>
    /// Time horizon for predictions
    /// </summary>
    public PredictionTimeHorizon TimeHorizon { get; set; } = PredictionTimeHorizon.MediumTerm;

    /// <summary>
    /// Confidence level required (0-100)
    /// </summary>
    public double MinConfidenceLevel { get; set; } = 70.0;

    /// <summary>
    /// Include uncertainty estimates
    /// </summary>
    public bool IncludeUncertaintyEstimates { get; set; } = true;

    /// <summary>
    /// Include scenario analysis
    /// </summary>
    public bool IncludeScenarioAnalysis { get; set; } = false;

    /// <summary>
    /// Include sensitivity analysis
    /// </summary>
    public bool IncludeSensitivityAnalysis { get; set; } = false;

    /// <summary>
    /// Maximum number of predictions per type
    /// </summary>
    public int? MaxPredictionsPerType { get; set; }

    /// <summary>
    /// Custom prediction parameters
    /// </summary>
    public Dictionary<string, object>? CustomParameters { get; set; }
}

// Additional supporting classes would continue here...
// (The file is getting quite large, so I'll include key classes and can add more if needed)

public class TokenUsagePrediction
{
    public long PredictedTokens { get; set; }
    public double ConfidenceLevel { get; set; }
    public Dictionary<string, long> TokensByModel { get; set; } = [];
    public List<TokenUsageForecastPoint> ForecastPoints { get; set; } = [];
}

public class ApiCallPrediction
{
    public long PredictedApiCalls { get; set; }
    public double ConfidenceLevel { get; set; }
    public Dictionary<string, long> CallsByEndpoint { get; set; } = [];
    public List<ApiCallForecastPoint> ForecastPoints { get; set; } = [];
}

public class UserActivityPrediction
{
    public int PredictedActiveUsers { get; set; }
    public double UserGrowthRate { get; set; }
    public Dictionary<string, int> UsersByActivity { get; set; } = [];
    public List<UserActivityForecastPoint> ForecastPoints { get; set; } = [];
}

public class ResourceDemandPrediction
{
    public Dictionary<string, double> DemandByResourceType { get; set; } = [];
    public List<ResourceDemandForecastPoint> ForecastPoints { get; set; } = [];
}

public class SeasonalUsagePattern
{
    public string PatternName { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public double AverageIncrease { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class GrowthTrendPrediction
{
    public double MonthlyGrowthRate { get; set; }
    public double YearOverYearGrowth { get; set; }
    public string GrowthPattern { get; set; } = string.Empty;
    public List<string> GrowthDrivers { get; set; } = [];
}

public class PredictiveAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public Dictionary<string, double> PredictedMetrics { get; set; } = [];
    public Dictionary<string, double> ConfidenceLevels { get; set; } = [];
    public Dictionary<string, ConfidenceInterval> ConfidenceIntervals { get; set; } = [];
}

public class PredictiveInsight
{
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Confidence { get; set; }
    public DateTime PredictedDate { get; set; }
    public double ImpactScore { get; set; }
    public List<string>? RecommendedActions { get; set; }
    public Dictionary<string, object>? SupportingData { get; set; }
}

public class ConfidenceInterval
{
    public double LowerBound { get; set; }
    public double UpperBound { get; set; }
    public double ConfidenceLevel { get; set; }
}

public class ModelValidationResults
{
    public double CrossValidationScore { get; set; }
    public double MeanAbsoluteError { get; set; }
    public double RootMeanSquareError { get; set; }
    public double R2Score { get; set; }
    public List<string> ValidationNotes { get; set; } = [];
}

public class DetectedAnomaly
{
    public DateTime DetectionTime { get; set; }
    public string AnomalyType { get; set; } = string.Empty;
    public double Severity { get; set; }
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, double> AffectedMetrics { get; set; } = [];
}

public class PredictedAnomaly
{
    public DateTime PredictedTime { get; set; }
    public string AnomalyType { get; set; } = string.Empty;
    public double Probability { get; set; }
    public double PredictedSeverity { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class AnomalyPattern
{
    public string PatternType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Frequency { get; set; }
    public List<string> TriggerConditions { get; set; } = [];
}

public class AnomalyDetectionModelMetrics
{
    public double TruePositiveRate { get; set; }
    public double FalsePositiveRate { get; set; }
    public double Precision { get; set; }
    public double Recall { get; set; }
    public double F1Score { get; set; }
}

// Forecast point classes for time series data
public class TokenUsageForecastPoint
{
    public DateTime Timestamp { get; set; }
    public long PredictedTokens { get; set; }
    public ConfidenceInterval ConfidenceInterval { get; set; } = new();
}

public class ApiCallForecastPoint
{
    public DateTime Timestamp { get; set; }
    public long PredictedCalls { get; set; }
    public ConfidenceInterval ConfidenceInterval { get; set; } = new();
}

public class UserActivityForecastPoint
{
    public DateTime Timestamp { get; set; }
    public int PredictedActiveUsers { get; set; }
    public ConfidenceInterval ConfidenceInterval { get; set; } = new();
}

public class ResourceDemandForecastPoint
{
    public DateTime Timestamp { get; set; }
    public Dictionary<string, double> PredictedDemand { get; set; } = [];
    public Dictionary<string, ConfidenceInterval> ConfidenceIntervals { get; set; } = [];
}

// Additional prediction classes would be defined here as needed...
