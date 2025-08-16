namespace PromptStudio.Core.DTOs.Analytics;

public class PowerUser
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public long ActivityScore { get; set; }
    public List<string> TopFeatures { get; set; } = [];
}
