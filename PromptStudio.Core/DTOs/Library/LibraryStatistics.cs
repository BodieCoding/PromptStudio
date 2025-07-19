using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Comprehensive statistics for a prompt library
/// </summary>
public class LibraryStatistics
{
    /// <summary>
    /// Gets or sets the library ID these statistics are for
    /// </summary>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the library name
    /// </summary>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total number of templates in the library
    /// </summary>
    public int TemplateCount { get; set; }

    /// <summary>
    /// Gets or sets the number of active (non-deleted) templates
    /// </summary>
    public int ActiveTemplateCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions across all templates
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the total number of successful executions
    /// </summary>
    public long SuccessfulExecutions { get; set; }

    /// <summary>
    /// Gets or sets the total number of failed executions
    /// </summary>
    public long FailedExecutions { get; set; }

    /// <summary>
    /// Gets or sets the success rate (0.0 to 1.0)
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions : 0.0;

    /// <summary>
    /// Gets or sets the average execution time in milliseconds
    /// </summary>
    public double AverageExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the date range for these statistics
    /// </summary>
    public DateTimeRange StatisticsPeriod { get; set; } = new();

    /// <summary>
    /// Gets or sets the library creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date
    /// </summary>
    public DateTime LastUpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the number of unique users who have executed templates
    /// </summary>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Gets or sets the most popular template in the library
    /// </summary>
    public string? MostPopularTemplate { get; set; }

    /// <summary>
    /// Gets or sets the template with the highest success rate
    /// </summary>
    public string? HighestSuccessRateTemplate { get; set; }

    /// <summary>
    /// Gets or sets additional statistical metadata
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}
