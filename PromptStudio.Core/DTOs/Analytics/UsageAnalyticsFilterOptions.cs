namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Filter options for usage analytics queries
/// </summary>
public class UsageAnalyticsFilterOptions
{
    /// <summary>
    /// Filter by specific user IDs
    /// </summary>
    public List<Guid>? UserIds { get; set; }

    /// <summary>
    /// Filter by user roles
    /// </summary>
    public List<string>? UserRoles { get; set; }

    /// <summary>
    /// Filter by departments
    /// </summary>
    public List<string>? Departments { get; set; }

    /// <summary>
    /// Filter by specific features or modules
    /// </summary>
    public List<string>? Features { get; set; }

    /// <summary>
    /// Filter by activity types
    /// </summary>
    public List<string>? ActivityTypes { get; set; }

    /// <summary>
    /// Filter by libraries
    /// </summary>
    public List<Guid>? LibraryIds { get; set; }

    /// <summary>
    /// Filter by templates
    /// </summary>
    public List<Guid>? TemplateIds { get; set; }

    /// <summary>
    /// Minimum session duration in minutes
    /// </summary>
    public int? MinSessionDurationMinutes { get; set; }

    /// <summary>
    /// Maximum session duration in minutes
    /// </summary>
    public int? MaxSessionDurationMinutes { get; set; }

    /// <summary>
    /// Filter by new vs returning users
    /// </summary>
    public string? UserType { get; set; } // "new", "returning", "all"

    /// <summary>
    /// Filter by device types
    /// </summary>
    public List<string>? DeviceTypes { get; set; }

    /// <summary>
    /// Filter by geographic regions
    /// </summary>
    public List<string>? Regions { get; set; }

    /// <summary>
    /// Group results by specific dimensions
    /// </summary>
    public List<string>? GroupBy { get; set; }

    /// <summary>
    /// Include cohort analysis
    /// </summary>
    public bool IncludeCohortAnalysis { get; set; } = false;

    /// <summary>
    /// Include funnel analysis
    /// </summary>
    public bool IncludeFunnelAnalysis { get; set; } = false;

    /// <summary>
    /// Include user journey mapping
    /// </summary>
    public bool IncludeUserJourney { get; set; } = false;

    /// <summary>
    /// Custom filters as key-value pairs
    /// </summary>
    public Dictionary<string, object>? CustomFilters { get; set; }
}
