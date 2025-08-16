namespace PromptStudio.Core.DTOs.Analytics;

public class SessionDayStats
{
    public long SessionCount { get; set; }
    public double AverageDurationMinutes { get; set; }
    public double AverageActionsPerSession { get; set; }
}
