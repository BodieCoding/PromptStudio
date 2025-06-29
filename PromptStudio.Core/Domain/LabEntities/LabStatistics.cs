namespace PromptStudio.Core.Domain;


/// <summary>
/// Lab statistics (Updated for Guid-based architecture)
/// </summary>
public class LabStatistics
{
    /// <summary>
    /// Lab ID
    /// </summary>
    public Guid LabId { get; set; }

    /// <summary>
    /// Lab name
    /// </summary>
    public string LabName { get; set; } = string.Empty;

    /// <summary>
    /// Total number of libraries in the lab
    /// </summary>
    public int TotalLibraries { get; set; }

    /// <summary>
    /// Number of active (non-deleted) libraries
    /// </summary>
    public int ActiveLibraries { get; set; }

    /// <summary>
    /// Total number of templates across all libraries
    /// </summary>
    public int TotalTemplates { get; set; }

    /// <summary>
    /// Number of active templates
    /// </summary>
    public int ActiveTemplates { get; set; }

    /// <summary>
    /// Total number of workflows in the lab
    /// </summary>
    public int TotalWorkflows { get; set; }

    /// <summary>
    /// Number of active workflows
    /// </summary>
    public int ActiveWorkflows { get; set; }

    /// <summary>
    /// Total number of executions across all templates
    /// </summary>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Number of successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Number of failed executions
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Success rate as a percentage
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Most recent execution date
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Most recent activity date (template update, execution, etc.)
    /// </summary>
    public DateTime? LastActivity { get; set; }

    /// <summary>
    /// Number of unique users who have accessed the lab
    /// </summary>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Number of lab members
    /// </summary>
    public int MemberCount { get; set; }

    /// <summary>
    /// Total token usage across all executions
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total cost across all executions
    /// </summary>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Most active library in the lab
    /// </summary>
    public LibraryPerformanceSummary? MostActiveLibrary { get; set; }

    /// <summary>
    /// Creation date of the lab
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Last update date of the lab
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
