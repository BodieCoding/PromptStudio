using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Filter options for cost analytics queries
/// </summary>
public class CostAnalyticsFilterOptions
{
    /// <summary>
    /// Time range for the analytics query
    /// </summary>
    public DateTimeRange? TimeRange { get; set; }

    /// <summary>
    /// Filter by specific tenants
    /// </summary>
    public List<Guid>? TenantIds { get; set; }

    /// <summary>
    /// Filter by cost categories
    /// </summary>
    public List<string>? CostCategories { get; set; }

    /// <summary>
    /// Filter by LLM providers
    /// </summary>
    public List<string>? LlmProviders { get; set; }

    /// <summary>
    /// Filter by specific models
    /// </summary>
    public List<string>? ModelNames { get; set; }

    /// <summary>
    /// Filter by users
    /// </summary>
    public List<Guid>? UserIds { get; set; }

    /// <summary>
    /// Filter by projects
    /// </summary>
    public List<Guid>? ProjectIds { get; set; }

    /// <summary>
    /// Filter by cost centers or departments
    /// </summary>
    public List<string>? CostCenters { get; set; }

    /// <summary>
    /// Filter by tags
    /// </summary>
    public Dictionary<string, List<string>>? Tags { get; set; }

    /// <summary>
    /// Minimum cost threshold
    /// </summary>
    public decimal? MinCost { get; set; }

    /// <summary>
    /// Maximum cost threshold
    /// </summary>
    public decimal? MaxCost { get; set; }

    /// <summary>
    /// Include only billable costs
    /// </summary>
    public bool? BillableOnly { get; set; }

    /// <summary>
    /// Include cost breakdown details
    /// </summary>
    public bool IncludeBreakdown { get; set; } = true;

    /// <summary>
    /// Include forecasting data
    /// </summary>
    public bool IncludeForecasting { get; set; } = false;

    /// <summary>
    /// Include budget variance analysis
    /// </summary>
    public bool IncludeBudgetVariance { get; set; } = false;

    /// <summary>
    /// Include optimization suggestions
    /// </summary>
    public bool IncludeOptimizationSuggestions { get; set; } = false;

    /// <summary>
    /// Group by dimension
    /// </summary>
    public CostGroupingDimension? GroupBy { get; set; }

    /// <summary>
    /// Sort order for results
    /// </summary>
    public SortDirection SortOrder { get; set; } = SortDirection.Descending;

    /// <summary>
    /// Sort by field
    /// </summary>
    public CostSortField SortBy { get; set; } = CostSortField.TotalCost;

    /// <summary>
    /// Aggregation level
    /// </summary>
    public AnalyticsAggregationLevel AggregationLevel { get; set; } = AnalyticsAggregationLevel.Daily;

    /// <summary>
    /// Maximum number of results to return
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Offset for pagination
    /// </summary>
    public int Offset { get; set; } = 0;
}

/// <summary>
/// Cost grouping dimensions
/// </summary>
public enum CostGroupingDimension
{
    Tenant,
    User,
    Project,
    LlmProvider,
    Model,
    CostCenter,
    Department,
    CostCategory,
    Tag,
    Day,
    Week,
    Month,
    Quarter,
    Year
}

/// <summary>
/// Cost sorting fields
/// </summary>
public enum CostSortField
{
    TotalCost,
    TokenCost,
    ApiCallCost,
    StorageCost,
    ComputeCost,
    CostPerToken,
    CostPerRequest,
    Volume,
    Timestamp,
    ProviderName,
    ModelName,
    TenantName,
    UserName,
    ProjectName
}
