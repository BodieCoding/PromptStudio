namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Filter options for lab analytics queries
/// </summary>
public class LabAnalyticsFilterOptions
{
    /// <summary>
    /// Filter by specific lab IDs
    /// </summary>
    public List<Guid>? LabIds { get; set; }

    /// <summary>
    /// Filter by lab owners
    /// </summary>
    public List<Guid>? OwnerIds { get; set; }

    /// <summary>
    /// Filter by experiment types
    /// </summary>
    public List<string>? ExperimentTypes { get; set; }

    /// <summary>
    /// Filter by experiment status
    /// </summary>
    public List<string>? ExperimentStatuses { get; set; }

    /// <summary>
    /// Filter by collaboration level
    /// </summary>
    public string? CollaborationLevel { get; set; }

    /// <summary>
    /// Filter by success rate range
    /// </summary>
    public DoubleRange? SuccessRateRange { get; set; }

    /// <summary>
    /// Filter by resource usage range
    /// </summary>
    public DoubleRange? ResourceUsageRange { get; set; }

    /// <summary>
    /// Include archived labs
    /// </summary>
    public bool IncludeArchived { get; set; } = false;

    /// <summary>
    /// Group results by specific dimensions
    /// </summary>
    public List<string>? GroupBy { get; set; }

    /// <summary>
    /// Include collaboration metrics
    /// </summary>
    public bool IncludeCollaborationMetrics { get; set; } = false;

    /// <summary>
    /// Include innovation metrics
    /// </summary>
    public bool IncludeInnovationMetrics { get; set; } = false;

    /// <summary>
    /// Custom filters as key-value pairs
    /// </summary>
    public Dictionary<string, object>? CustomFilters { get; set; }
}

/// <summary>
/// Represents a double range filter (redefining here to avoid dependency issues)
/// </summary>
public class DoubleRange
{
    public double? Min { get; set; }
    public double? Max { get; set; }
}
