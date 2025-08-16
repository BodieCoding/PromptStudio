namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// User segmentation analytics
/// </summary>
public class UserSegmentationAnalytics
{
    /// <summary>
    /// User segments by activity level
    /// </summary>
    public Dictionary<string, UserSegment> SegmentsByActivity { get; set; } = [];

    /// <summary>
    /// User segments by role
    /// </summary>
    public Dictionary<string, UserSegment> SegmentsByRole { get; set; } = [];

    /// <summary>
    /// User segments by department
    /// </summary>
    public Dictionary<string, UserSegment> SegmentsByDepartment { get; set; } = [];

    /// <summary>
    /// Power users identification
    /// </summary>
    public List<PowerUser> PowerUsers { get; set; } = [];
}
