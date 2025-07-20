using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Filter options for resource analytics queries
/// </summary>
public class ResourceAnalyticsFilterOptions
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
    /// Filter by resource types
    /// </summary>
    public List<string>? ResourceTypes { get; set; }

    /// <summary>
    /// Filter by resource categories
    /// </summary>
    public List<string>? ResourceCategories { get; set; }

    /// <summary>
    /// Filter by resource providers
    /// </summary>
    public List<string>? ResourceProviders { get; set; }

    /// <summary>
    /// Filter by resource regions
    /// </summary>
    public List<string>? ResourceRegions { get; set; }

    /// <summary>
    /// Filter by projects
    /// </summary>
    public List<Guid>? ProjectIds { get; set; }

    /// <summary>
    /// Filter by environments
    /// </summary>
    public List<string>? Environments { get; set; }

    /// <summary>
    /// Filter by resource status
    /// </summary>
    public List<string>? ResourceStatuses { get; set; }

    /// <summary>
    /// Filter by tags
    /// </summary>
    public Dictionary<string, List<string>>? Tags { get; set; }

    /// <summary>
    /// Minimum utilization threshold (0-100)
    /// </summary>
    public double? MinUtilization { get; set; }

    /// <summary>
    /// Maximum utilization threshold (0-100)
    /// </summary>
    public double? MaxUtilization { get; set; }

    /// <summary>
    /// Include only underutilized resources
    /// </summary>
    public bool? UnderutilizedOnly { get; set; }

    /// <summary>
    /// Include only overutilized resources
    /// </summary>
    public bool? OverutilizedOnly { get; set; }

    /// <summary>
    /// Include capacity planning data
    /// </summary>
    public bool IncludeCapacityPlanning { get; set; } = false;

    /// <summary>
    /// Include performance metrics
    /// </summary>
    public bool IncludePerformanceMetrics { get; set; } = true;

    /// <summary>
    /// Include cost allocation data
    /// </summary>
    public bool IncludeCostAllocation { get; set; } = false;

    /// <summary>
    /// Include optimization recommendations
    /// </summary>
    public bool IncludeOptimizationRecommendations { get; set; } = false;

    /// <summary>
    /// Group by dimension
    /// </summary>
    public ResourceGroupingDimension? GroupBy { get; set; }

    /// <summary>
    /// Sort order for results
    /// </summary>
    public SortDirection SortOrder { get; set; } = SortDirection.Descending;

    /// <summary>
    /// Sort by field
    /// </summary>
    public ResourceSortField SortBy { get; set; } = ResourceSortField.Utilization;

    /// <summary>
    /// Aggregation level
    /// </summary>
    public AnalyticsAggregationLevel AggregationLevel { get; set; } = AnalyticsAggregationLevel.Hourly;

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
/// Resource grouping dimensions
/// </summary>
public enum ResourceGroupingDimension
{
    Tenant,
    Project,
    ResourceType,
    ResourceCategory,
    Provider,
    Region,
    Environment,
    CostCenter,
    Department,
    Tag,
    Hour,
    Day,
    Week,
    Month,
    Quarter,
    Year
}

/// <summary>
/// Resource sorting fields
/// </summary>
public enum ResourceSortField
{
    Utilization,
    CpuUtilization,
    MemoryUtilization,
    StorageUtilization,
    NetworkUtilization,
    Cost,
    Performance,
    Efficiency,
    ResourceName,
    ResourceType,
    Provider,
    Region,
    CreatedDate,
    LastUsed,
    Timestamp
}
