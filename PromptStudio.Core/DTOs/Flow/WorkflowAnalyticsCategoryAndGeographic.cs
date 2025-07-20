using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents category-based analytics results for organizational insights.
/// </summary>
public class CategoryAnalyticsResult
{
    /// <summary>
    /// Gets or sets analytics breakdown by workflow category.
    /// </summary>
    /// <value>Dictionary of category names and their analytics data.</value>
    public Dictionary<string, CategoryAnalyticsData>? CategoryBreakdown { get; set; }

    /// <summary>
    /// Gets or sets the most active categories by execution volume.
    /// </summary>
    /// <value>Collection of categories ranked by execution activity.</value>
    public List<CategoryActivityEntry>? MostActiveCategories { get; set; }

    /// <summary>
    /// Gets or sets the best performing categories by success rate.
    /// </summary>
    /// <value>Collection of categories with the highest success rates.</value>
    public List<CategoryPerformanceEntry>? BestPerformingCategories { get; set; }

    /// <summary>
    /// Gets or sets categories with growth potential.
    /// </summary>
    /// <value>Collection of categories showing growth opportunities.</value>
    public List<CategoryGrowthEntry>? GrowthPotentialCategories { get; set; }

    /// <summary>
    /// Gets or sets category adoption trends over time.
    /// </summary>
    /// <value>Trend data showing how category usage has changed over time.</value>
    public List<CategoryAdoptionTrendPoint>? AdoptionTrends { get; set; }
}

/// <summary>
/// Represents analytics data for a specific workflow category.
/// </summary>
public class CategoryAnalyticsData
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    /// <value>Name of the workflow category.</value>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total number of workflows in this category.
    /// </summary>
    /// <value>Count of workflows classified under this category.</value>
    public int WorkflowCount { get; set; }

    /// <summary>
    /// Gets or sets the total execution count for this category.
    /// </summary>
    /// <value>Total number of executions across all workflows in this category.</value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the average success rate for this category.
    /// </summary>
    /// <value>Average success rate percentage (0-100) for workflows in this category.</value>
    public double AverageSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the category performance score.
    /// </summary>
    /// <value>Calculated performance score (0-100) for this category.</value>
    public double PerformanceScore { get; set; }

    /// <summary>
    /// Gets or sets the unique user count for this category.
    /// </summary>
    /// <value>Number of unique users who have executed workflows in this category.</value>
    public int UniqueUserCount { get; set; }
}

/// <summary>
/// Represents a category activity entry for activity analysis.
/// </summary>
public class CategoryActivityEntry
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    /// <value>Name of the workflow category.</value>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the activity score for this category.
    /// </summary>
    /// <value>Calculated activity score (0-100) based on execution volume and frequency.</value>
    public double ActivityScore { get; set; }

    /// <summary>
    /// Gets or sets the execution frequency for this category.
    /// </summary>
    /// <value>Average number of executions per time unit for this category.</value>
    public double ExecutionFrequency { get; set; }

    /// <summary>
    /// Gets or sets the growth rate for this category.
    /// </summary>
    /// <value>Percentage growth rate in activity for this category.</value>
    public double GrowthRate { get; set; }
}

/// <summary>
/// Represents a category performance entry for performance analysis.
/// </summary>
public class CategoryPerformanceEntry
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    /// <value>Name of the workflow category.</value>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the performance score for this category.
    /// </summary>
    /// <value>Calculated performance score (0-100) for this category.</value>
    public double PerformanceScore { get; set; }

    /// <summary>
    /// Gets or sets the reliability score for this category.
    /// </summary>
    /// <value>Reliability score (0-100) based on error rates and consistency.</value>
    public double ReliabilityScore { get; set; }

    /// <summary>
    /// Gets or sets the efficiency score for this category.
    /// </summary>
    /// <value>Efficiency score (0-100) based on execution times and resource usage.</value>
    public double EfficiencyScore { get; set; }
}

/// <summary>
/// Represents a category growth entry for growth analysis.
/// </summary>
public class CategoryGrowthEntry
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    /// <value>Name of the workflow category with growth potential.</value>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the growth potential score.
    /// </summary>
    /// <value>Calculated growth potential score (0-100) for this category.</value>
    public double GrowthPotentialScore { get; set; }

    /// <summary>
    /// Gets or sets the current adoption rate.
    /// </summary>
    /// <value>Current adoption rate percentage for this category.</value>
    public double CurrentAdoptionRate { get; set; }

    /// <summary>
    /// Gets or sets the projected adoption rate.
    /// </summary>
    /// <value>Projected future adoption rate for this category.</value>
    public double ProjectedAdoptionRate { get; set; }

    /// <summary>
    /// Gets or sets growth recommendations for this category.
    /// </summary>
    /// <value>List of recommendations to enhance growth in this category.</value>
    public List<string>? GrowthRecommendations { get; set; }
}

/// <summary>
/// Represents a category adoption trend point for temporal adoption analysis.
/// </summary>
public class CategoryAdoptionTrendPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this adoption measurement.
    /// </summary>
    /// <value>Date and time for this adoption data point.</value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    /// <value>Name of the category for this trend point.</value>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the adoption rate at this point.
    /// </summary>
    /// <value>Adoption rate percentage (0-100) at this time point.</value>
    public double AdoptionRate { get; set; }

    /// <summary>
    /// Gets or sets the user count at this point.
    /// </summary>
    /// <value>Number of users using this category at this time point.</value>
    public int UserCount { get; set; }

    /// <summary>
    /// Gets or sets the execution count at this point.
    /// </summary>
    /// <value>Number of executions for this category at this time point.</value>
    public long ExecutionCount { get; set; }
}

/// <summary>
/// Represents geographic analytics results for location-based insights.
/// </summary>
public class GeographicAnalyticsResult
{
    /// <summary>
    /// Gets or sets analytics breakdown by geographic region.
    /// </summary>
    /// <value>Dictionary of region names and their analytics data.</value>
    public Dictionary<string, GeographicAnalyticsData>? RegionBreakdown { get; set; }

    /// <summary>
    /// Gets or sets the most active regions by execution volume.
    /// </summary>
    /// <value>Collection of regions ranked by workflow execution activity.</value>
    public List<RegionActivityEntry>? MostActiveRegions { get; set; }

    /// <summary>
    /// Gets or sets performance comparison across regions.
    /// </summary>
    /// <value>Comparison of performance metrics across different geographic regions.</value>
    public List<RegionPerformanceComparison>? RegionPerformanceComparisons { get; set; }

    /// <summary>
    /// Gets or sets geographic growth opportunities.
    /// </summary>
    /// <value>Regions with potential for growth and expansion.</value>
    public List<GeographicGrowthOpportunity>? GrowthOpportunities { get; set; }

    /// <summary>
    /// Gets or sets time zone impact analysis.
    /// </summary>
    /// <value>Analysis of how time zones affect workflow execution patterns.</value>
    public TimeZoneImpactAnalysis? TimeZoneImpact { get; set; }
}

/// <summary>
/// Represents analytics data for a specific geographic region.
/// </summary>
public class GeographicAnalyticsData
{
    /// <summary>
    /// Gets or sets the region name.
    /// </summary>
    /// <value>Name of the geographic region.</value>
    public string RegionName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the country code for this region.
    /// </summary>
    /// <value>ISO country code for the region.</value>
    public string CountryCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total execution count for this region.
    /// </summary>
    /// <value>Total number of workflow executions from this region.</value>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the unique user count for this region.
    /// </summary>
    /// <value>Number of unique users from this region.</value>
    public int UniqueUserCount { get; set; }

    /// <summary>
    /// Gets or sets the average performance for this region.
    /// </summary>
    /// <value>Average performance metrics for workflows executed from this region.</value>
    public double AveragePerformance { get; set; }

    /// <summary>
    /// Gets or sets the peak usage times for this region.
    /// </summary>
    /// <value>Time periods when usage is highest in this region.</value>
    public List<TimeSpan>? PeakUsageTimes { get; set; }
}

/// <summary>
/// Represents a region activity entry for activity analysis.
/// </summary>
public class RegionActivityEntry
{
    /// <summary>
    /// Gets or sets the region name.
    /// </summary>
    /// <value>Name of the geographic region.</value>
    public string RegionName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the activity score for this region.
    /// </summary>
    /// <value>Calculated activity score (0-100) for this region.</value>
    public double ActivityScore { get; set; }

    /// <summary>
    /// Gets or sets the execution density for this region.
    /// </summary>
    /// <value>Execution density (executions per user) for this region.</value>
    public double ExecutionDensity { get; set; }

    /// <summary>
    /// Gets or sets the growth trend for this region.
    /// </summary>
    /// <value>Growth trend indicator for this region.</value>
    public string GrowthTrend { get; set; } = string.Empty;
}

/// <summary>
/// Represents performance comparison between geographic regions.
/// </summary>
public class RegionPerformanceComparison
{
    /// <summary>
    /// Gets or sets the comparison identifier.
    /// </summary>
    /// <value>Unique identifier for this performance comparison.</value>
    public string ComparisonId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the regions being compared.
    /// </summary>
    /// <value>List of region names included in this comparison.</value>
    public List<string>? ComparedRegions { get; set; }

    /// <summary>
    /// Gets or sets performance metrics by region.
    /// </summary>
    /// <value>Dictionary of region names and their performance scores.</value>
    public Dictionary<string, double>? PerformanceByRegion { get; set; }

    /// <summary>
    /// Gets or sets the best performing region.
    /// </summary>
    /// <value>Name of the region with the highest performance score.</value>
    public string BestPerformingRegion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets insights from the regional comparison.
    /// </summary>
    /// <value>Key insights derived from the regional performance comparison.</value>
    public List<string>? ComparisonInsights { get; set; }
}

/// <summary>
/// Represents a geographic growth opportunity for expansion planning.
/// </summary>
public class GeographicGrowthOpportunity
{
    /// <summary>
    /// Gets or sets the opportunity identifier.
    /// </summary>
    /// <value>Unique identifier for this geographic growth opportunity.</value>
    public string OpportunityId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the target region name.
    /// </summary>
    /// <value>Name of the region with growth opportunity.</value>
    public string TargetRegion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the growth potential score.
    /// </summary>
    /// <value>Calculated growth potential score (0-100) for this region.</value>
    public double GrowthPotentialScore { get; set; }

    /// <summary>
    /// Gets or sets the estimated market size.
    /// </summary>
    /// <value>Estimated market size or user potential in this region.</value>
    public int EstimatedMarketSize { get; set; }

    /// <summary>
    /// Gets or sets expansion recommendations.
    /// </summary>
    /// <value>Specific recommendations for expanding into this region.</value>
    public List<string>? ExpansionRecommendations { get; set; }
}

/// <summary>
/// Represents time zone impact analysis for geographic insights.
/// </summary>
public class TimeZoneImpactAnalysis
{
    /// <summary>
    /// Gets or sets usage patterns by time zone.
    /// </summary>
    /// <value>Dictionary of time zones and their usage pattern data.</value>
    public Dictionary<string, TimeZoneUsagePattern>? UsagePatternsByTimeZone { get; set; }

    /// <summary>
    /// Gets or sets peak global usage times.
    /// </summary>
    /// <value>Time periods when global usage reaches its peak.</value>
    public List<TimeSpan>? PeakGlobalUsageTimes { get; set; }

    /// <summary>
    /// Gets or sets time zone coordination insights.
    /// </summary>
    /// <value>Insights about coordinating across different time zones.</value>
    public List<string>? CoordinationInsights { get; set; }

    /// <summary>
    /// Gets or sets recommendations for time zone optimization.
    /// </summary>
    /// <value>Recommendations for optimizing operations across time zones.</value>
    public List<string>? OptimizationRecommendations { get; set; }
}

/// <summary>
/// Represents usage patterns for a specific time zone.
/// </summary>
public class TimeZoneUsagePattern
{
    /// <summary>
    /// Gets or sets the time zone identifier.
    /// </summary>
    /// <value>Standard time zone identifier.</value>
    public string TimeZoneId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the peak usage hour in local time.
    /// </summary>
    /// <value>Hour of the day (0-23) when usage is highest in this time zone.</value>
    public int PeakUsageHour { get; set; }

    /// <summary>
    /// Gets or sets the off-peak usage hours.
    /// </summary>
    /// <value>Hours of the day when usage is lowest in this time zone.</value>
    public List<int>? OffPeakUsageHours { get; set; }

    /// <summary>
    /// Gets or sets the business hours usage percentage.
    /// </summary>
    /// <value>Percentage of usage that occurs during typical business hours.</value>
    public double BusinessHoursUsagePercentage { get; set; }

    /// <summary>
    /// Gets or sets weekend usage patterns.
    /// </summary>
    /// <value>Description of how usage patterns differ on weekends.</value>
    public string WeekendUsagePattern { get; set; } = string.Empty;
}
