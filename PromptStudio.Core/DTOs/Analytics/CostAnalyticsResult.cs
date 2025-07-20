using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive cost analytics result with detailed financial metrics and optimization insights
/// </summary>
public class CostAnalyticsResult
{
    /// <summary>
    /// Time range for this analytics result
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Cost analytics summary
    /// </summary>
    public CostAnalyticsSummary Summary { get; set; } = new();

    /// <summary>
    /// Cost breakdown by different dimensions
    /// </summary>
    public CostBreakdownAnalytics Breakdown { get; set; } = new();

    /// <summary>
    /// Usage-based cost analytics
    /// </summary>
    public UsageCostAnalytics Usage { get; set; } = new();

    /// <summary>
    /// Budget tracking and variance analytics
    /// </summary>
    public BudgetAnalytics Budget { get; set; } = new();

    /// <summary>
    /// Cost forecasting and trends
    /// </summary>
    public CostForecastingAnalytics Forecasting { get; set; } = new();

    /// <summary>
    /// Cost optimization analytics
    /// </summary>
    public CostOptimizationAnalytics Optimization { get; set; } = new();

    /// <summary>
    /// Time-series data for cost metrics
    /// </summary>
    public List<CostAnalyticsTimePoint>? TimeSeries { get; set; }

    /// <summary>
    /// Cost optimization recommendations
    /// </summary>
    public List<CostOptimizationRecommendation> Recommendations { get; set; } = new();
}

/// <summary>
/// Cost analytics summary
/// </summary>
public class CostAnalyticsSummary
{
    /// <summary>
    /// Total cost for the period
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Previous period cost for comparison
    /// </summary>
    public decimal? PreviousPeriodCost { get; set; }

    /// <summary>
    /// Period-over-period cost change
    /// </summary>
    public decimal? CostChange { get; set; }

    /// <summary>
    /// Percentage change from previous period
    /// </summary>
    public double? PercentChange { get; set; }

    /// <summary>
    /// Average daily cost
    /// </summary>
    public decimal AverageDailyCost { get; set; }

    /// <summary>
    /// Peak cost day
    /// </summary>
    public DateTime? PeakCostDate { get; set; }

    /// <summary>
    /// Peak cost amount
    /// </summary>
    public decimal? PeakCostAmount { get; set; }

    /// <summary>
    /// Most expensive provider
    /// </summary>
    public string? MostExpensiveProvider { get; set; }

    /// <summary>
    /// Most expensive model
    /// </summary>
    public string? MostExpensiveModel { get; set; }

    /// <summary>
    /// Cost efficiency score (0-100)
    /// </summary>
    public double CostEfficiencyScore { get; set; }

    /// <summary>
    /// Key cost insights
    /// </summary>
    public List<string> KeyInsights { get; set; } = new();
}

/// <summary>
/// Cost breakdown analytics
/// </summary>
public class CostBreakdownAnalytics
{
    /// <summary>
    /// Cost breakdown by provider
    /// </summary>
    public Dictionary<string, ProviderCostBreakdown> ByProvider { get; set; } = new();

    /// <summary>
    /// Cost breakdown by model
    /// </summary>
    public Dictionary<string, ModelCostBreakdown> ByModel { get; set; } = new();

    /// <summary>
    /// Cost breakdown by tenant
    /// </summary>
    public Dictionary<string, TenantCostBreakdown> ByTenant { get; set; } = new();

    /// <summary>
    /// Cost breakdown by cost category
    /// </summary>
    public Dictionary<string, CategoryCostBreakdown> ByCategory { get; set; } = new();

    /// <summary>
    /// Cost breakdown by user
    /// </summary>
    public Dictionary<string, UserCostBreakdown> ByUser { get; set; } = new();

    /// <summary>
    /// Cost breakdown by project
    /// </summary>
    public Dictionary<string, ProjectCostBreakdown> ByProject { get; set; } = new();

    /// <summary>
    /// Cost breakdown by cost center
    /// </summary>
    public Dictionary<string, CostCenterBreakdown> ByCostCenter { get; set; } = new();
}

/// <summary>
/// Usage-based cost analytics
/// </summary>
public class UsageCostAnalytics
{
    /// <summary>
    /// Cost per token by model
    /// </summary>
    public Dictionary<string, decimal> CostPerTokenByModel { get; set; } = new();

    /// <summary>
    /// Cost per request by model
    /// </summary>
    public Dictionary<string, decimal> CostPerRequestByModel { get; set; } = new();

    /// <summary>
    /// Token usage and costs
    /// </summary>
    public TokenUsageCostAnalysis TokenUsage { get; set; } = new();

    /// <summary>
    /// API call usage and costs
    /// </summary>
    public ApiUsageCostAnalysis ApiUsage { get; set; } = new();

    /// <summary>
    /// Storage costs
    /// </summary>
    public StorageCostAnalysis Storage { get; set; } = new();

    /// <summary>
    /// Compute costs
    /// </summary>
    public ComputeCostAnalysis Compute { get; set; } = new();
}

/// <summary>
/// Budget tracking and variance analytics
/// </summary>
public class BudgetAnalytics
{
    /// <summary>
    /// Total budget for the period
    /// </summary>
    public decimal? TotalBudget { get; set; }

    /// <summary>
    /// Remaining budget
    /// </summary>
    public decimal? RemainingBudget { get; set; }

    /// <summary>
    /// Budget utilization percentage
    /// </summary>
    public double? BudgetUtilization { get; set; }

    /// <summary>
    /// Budget variance (actual vs budget)
    /// </summary>
    public decimal? BudgetVariance { get; set; }

    /// <summary>
    /// Budget variance percentage
    /// </summary>
    public double? BudgetVariancePercentage { get; set; }

    /// <summary>
    /// Projected end-of-period spend
    /// </summary>
    public decimal? ProjectedSpend { get; set; }

    /// <summary>
    /// Budget burn rate (daily average)
    /// </summary>
    public decimal? BurnRate { get; set; }

    /// <summary>
    /// Days until budget exhausted
    /// </summary>
    public int? DaysUntilBudgetExhausted { get; set; }

    /// <summary>
    /// Budget alerts
    /// </summary>
    public List<BudgetAlert> Alerts { get; set; } = new();
}

/// <summary>
/// Cost forecasting and trends
/// </summary>
public class CostForecastingAnalytics
{
    /// <summary>
    /// Forecasted costs for upcoming periods
    /// </summary>
    public List<CostForecast> Forecasts { get; set; } = new();

    /// <summary>
    /// Cost trend analysis
    /// </summary>
    public CostTrendAnalysis Trends { get; set; } = new();

    /// <summary>
    /// Seasonal cost patterns
    /// </summary>
    public List<SeasonalCostPattern> SeasonalPatterns { get; set; } = new();

    /// <summary>
    /// Forecast confidence level
    /// </summary>
    public double ForecastConfidence { get; set; }

    /// <summary>
    /// Factors influencing forecast
    /// </summary>
    public List<string> ForecastFactors { get; set; } = new();
}

/// <summary>
/// Cost optimization analytics
/// </summary>
public class CostOptimizationAnalytics
{
    /// <summary>
    /// Total potential savings identified
    /// </summary>
    public decimal PotentialSavings { get; set; }

    /// <summary>
    /// Optimization opportunities by category
    /// </summary>
    public Dictionary<string, List<CostOptimizationOpportunity>> OpportunitiesByCategory { get; set; } = new();

    /// <summary>
    /// Resource rightsizing opportunities
    /// </summary>
    public List<RightsizingOpportunity> RightsizingOpportunities { get; set; } = new();

    /// <summary>
    /// Provider switching opportunities
    /// </summary>
    public List<ProviderSwitchingOpportunity> ProviderSwitchingOpportunities { get; set; } = new();

    /// <summary>
    /// Volume discount opportunities
    /// </summary>
    public List<VolumeDiscountOpportunity> VolumeDiscountOpportunities { get; set; } = new();

    /// <summary>
    /// Unused resource identification
    /// </summary>
    public List<UnusedResourceAlert> UnusedResources { get; set; } = new();
}

// Supporting classes for cost analytics data structures

public class ProviderCostBreakdown
{
    public string ProviderName { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public double Percentage { get; set; }
    public long TotalRequests { get; set; }
    public decimal AverageCostPerRequest { get; set; }
    public Dictionary<string, decimal> CostByModel { get; set; } = new();
}

public class ModelCostBreakdown
{
    public string ModelName { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public double Percentage { get; set; }
    public long TotalTokens { get; set; }
    public long TotalRequests { get; set; }
    public decimal CostPerToken { get; set; }
    public decimal CostPerRequest { get; set; }
}

public class TenantCostBreakdown
{
    public Guid TenantId { get; set; }
    public string TenantName { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public double Percentage { get; set; }
    public int UserCount { get; set; }
    public decimal CostPerUser { get; set; }
    public Dictionary<string, decimal> CostByCategory { get; set; } = new();
}

public class CategoryCostBreakdown
{
    public string Category { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public double Percentage { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class UserCostBreakdown
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public double Percentage { get; set; }
    public long TotalRequests { get; set; }
    public decimal CostPerRequest { get; set; }
    public Dictionary<string, decimal> CostByProvider { get; set; } = new();
}

public class ProjectCostBreakdown
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public double Percentage { get; set; }
    public decimal? Budget { get; set; }
    public double? BudgetUtilization { get; set; }
    public Dictionary<string, decimal> CostByCategory { get; set; } = new();
}

public class CostCenterBreakdown
{
    public string CostCenter { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public double Percentage { get; set; }
    public decimal? Budget { get; set; }
    public double? BudgetVariance { get; set; }
}

public class TokenUsageCostAnalysis
{
    public long TotalInputTokens { get; set; }
    public long TotalOutputTokens { get; set; }
    public long TotalTokens { get; set; }
    public decimal InputTokensCost { get; set; }
    public decimal OutputTokensCost { get; set; }
    public decimal TotalTokensCost { get; set; }
    public decimal AverageInputTokenCost { get; set; }
    public decimal AverageOutputTokenCost { get; set; }
}

public class ApiUsageCostAnalysis
{
    public long TotalApiCalls { get; set; }
    public decimal TotalApiCost { get; set; }
    public decimal AverageCostPerCall { get; set; }
    public Dictionary<string, ApiCallCostBreakdown> CostByEndpoint { get; set; } = new();
}

public class StorageCostAnalysis
{
    public decimal TotalStorageCost { get; set; }
    public long TotalStorageGB { get; set; }
    public decimal CostPerGB { get; set; }
    public Dictionary<string, decimal> CostByStorageType { get; set; } = new();
}

public class ComputeCostAnalysis
{
    public decimal TotalComputeCost { get; set; }
    public double TotalComputeHours { get; set; }
    public decimal CostPerHour { get; set; }
    public Dictionary<string, decimal> CostByInstanceType { get; set; } = new();
}

public class BudgetAlert
{
    public string AlertType { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime AlertDate { get; set; }
    public decimal? ThresholdValue { get; set; }
    public decimal? CurrentValue { get; set; }
}

public class CostForecast
{
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public decimal ForecastedCost { get; set; }
    public decimal LowerBound { get; set; }
    public decimal UpperBound { get; set; }
    public double ConfidenceLevel { get; set; }
    public string ForecastMethod { get; set; } = string.Empty;
}

public class CostTrendAnalysis
{
    public string TrendDirection { get; set; } = string.Empty;
    public double TrendStrength { get; set; }
    public decimal MonthlyGrowthRate { get; set; }
    public List<string> TrendDrivers { get; set; } = new();
    public bool IsSeasonalTrend { get; set; }
}

public class SeasonalCostPattern
{
    public string PatternType { get; set; } = string.Empty;
    public string Season { get; set; } = string.Empty;
    public double PatternStrength { get; set; }
    public decimal AverageIncrease { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class CostOptimizationOpportunity
{
    public string OpportunityType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
    public double ImpactScore { get; set; }
    public string Priority { get; set; } = "medium";
    public string ImplementationEffort { get; set; } = string.Empty;
    public List<string> RequiredActions { get; set; } = new();
}

public class RightsizingOpportunity
{
    public string ResourceType { get; set; } = string.Empty;
    public string CurrentConfiguration { get; set; } = string.Empty;
    public string RecommendedConfiguration { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
    public double UtilizationRate { get; set; }
    public string RightsizingReason { get; set; } = string.Empty;
}

public class ProviderSwitchingOpportunity
{
    public string CurrentProvider { get; set; } = string.Empty;
    public string RecommendedProvider { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public decimal CurrentCost { get; set; }
    public decimal RecommendedCost { get; set; }
    public decimal PotentialSavings { get; set; }
    public double QualityImpact { get; set; }
    public List<string> Considerations { get; set; } = new();
}

public class VolumeDiscountOpportunity
{
    public string Provider { get; set; } = string.Empty;
    public decimal CurrentVolume { get; set; }
    public decimal DiscountThreshold { get; set; }
    public decimal DiscountRate { get; set; }
    public decimal PotentialSavings { get; set; }
    public DateTime NextEvaluationDate { get; set; }
}

public class UnusedResourceAlert
{
    public string ResourceType { get; set; } = string.Empty;
    public string ResourceId { get; set; } = string.Empty;
    public string ResourceName { get; set; } = string.Empty;
    public decimal MonthlyCost { get; set; }
    public DateTime LastUsed { get; set; }
    public int DaysUnused { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
}

public class ApiCallCostBreakdown
{
    public string Endpoint { get; set; } = string.Empty;
    public long CallCount { get; set; }
    public decimal TotalCost { get; set; }
    public decimal AverageCostPerCall { get; set; }
}

public class CostAnalyticsTimePoint
{
    public DateTime Timestamp { get; set; }
    public decimal TotalCost { get; set; }
    public decimal TokenCost { get; set; }
    public decimal ApiCallCost { get; set; }
    public decimal StorageCost { get; set; }
    public decimal ComputeCost { get; set; }
    public long TotalRequests { get; set; }
    public long TotalTokens { get; set; }
}

public class CostOptimizationRecommendation
{
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = "medium";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PotentialSavings { get; set; }
    public double ImpactScore { get; set; }
    public string ImplementationComplexity { get; set; } = string.Empty;
    public List<string>? ImplementationSteps { get; set; }
    public DateTime? RecommendedImplementationDate { get; set; }
}
