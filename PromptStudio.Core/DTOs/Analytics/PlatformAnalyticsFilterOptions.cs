namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Filter options for platform-wide analytics queries
/// </summary>
public class PlatformAnalyticsFilterOptions
{
    /// <summary>
    /// Filter by specific user IDs
    /// </summary>
    public List<Guid>? UserIds { get; set; }

    /// <summary>
    /// Filter by specific libraries
    /// </summary>
    public List<Guid>? LibraryIds { get; set; }

    /// <summary>
    /// Filter by specific templates
    /// </summary>
    public List<Guid>? TemplateIds { get; set; }

    /// <summary>
    /// Filter by user roles
    /// </summary>
    public List<string>? UserRoles { get; set; }

    /// <summary>
    /// Filter by department or organizational unit
    /// </summary>
    public List<string>? Departments { get; set; }

    /// <summary>
    /// Filter by AI providers
    /// </summary>
    public List<string>? AiProviders { get; set; }

    /// <summary>
    /// Filter by models
    /// </summary>
    public List<string>? Models { get; set; }

    /// <summary>
    /// Include specific analytics domains
    /// </summary>
    public List<string>? IncludeDomains { get; set; }

    /// <summary>
    /// Exclude specific analytics domains
    /// </summary>
    public List<string>? ExcludeDomains { get; set; }

    /// <summary>
    /// Minimum activity threshold
    /// </summary>
    public int? MinActivityThreshold { get; set; }

    /// <summary>
    /// Include archived/deleted items
    /// </summary>
    public bool IncludeArchived { get; set; } = false;

    /// <summary>
    /// Group results by specific dimensions
    /// </summary>
    public List<string>? GroupBy { get; set; }

    /// <summary>
    /// Include predictive analytics
    /// </summary>
    public bool IncludePredictive { get; set; } = false;

    /// <summary>
    /// Custom filters as key-value pairs
    /// </summary>
    public Dictionary<string, object>? CustomFilters { get; set; }
}
