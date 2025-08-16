namespace PromptStudio.Core.DTOs.Analytics;

// Supporting classes for the analytics data structures

public class TopUserActivity
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public long ActivityCount { get; set; }
    public TimeSpan TotalActiveTime { get; set; }
}
