namespace PromptStudio.Core.Domain;

/// <summary>
/// Token usage information
/// </summary>
public class TokenUsage
{
    /// <summary>
    /// Number of input tokens
    /// </summary>
    public int InputTokens { get; set; }

    /// <summary>
    /// Number of output tokens
    /// </summary>
    public int OutputTokens { get; set; }

    /// <summary>
    /// Total tokens used
    /// </summary>
    public int TotalTokens => InputTokens + OutputTokens;

    /// <summary>
    /// Cost per input token
    /// </summary>
    public decimal? InputTokenCost { get; set; }

    /// <summary>
    /// Cost per output token
    /// </summary>
    public decimal? OutputTokenCost { get; set; }

    /// <summary>
    /// Total cost for this token usage
    /// </summary>
    public decimal? TotalCost => (InputTokens * (InputTokenCost ?? 0)) + (OutputTokens * (OutputTokenCost ?? 0));
}
