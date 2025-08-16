namespace PromptStudio.Core.DTOs.Analytics;

public class UserPath
{
    public List<string> Path { get; set; } = [];
    public long Usage { get; set; }
    public double SuccessRate { get; set; }
    public double AverageCompletionTimeMinutes { get; set; }
}
