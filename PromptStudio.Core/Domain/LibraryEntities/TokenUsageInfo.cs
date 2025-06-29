namespace PromptStudio.Core.Domain;

public class TokenUsageInfo
{
    public long TotalTokens { get; set; }
    public long TotalInputTokens { get; set; }
    public long TotalOutputTokens { get; set; }
    public Dictionary<string, long> TokensByModel { get; set; } = new();
    public Dictionary<string, long> TokensByCategory { get; set; } = new();
}
