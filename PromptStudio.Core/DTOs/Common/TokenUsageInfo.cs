namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Token usage information
/// </summary>
public class TokenUsageInfo
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
    public decimal? Cost { get; set; }
}
