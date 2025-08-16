namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Detailed model information with cost and capability details
/// </summary>
public class DetailedModelInfo : ModelInfo
{
    /// <summary>
    /// Cost per input token
    /// </summary>
    public decimal InputTokenCost { get; set; }
    
    /// <summary>
    /// Cost per output token
    /// </summary>
    public decimal OutputTokenCost { get; set; }
    
    /// <summary>
    /// Maximum context length in tokens
    /// </summary>
    public int MaxTokens { get; set; }
    
    /// <summary>
    /// Supported features (function_calling, vision, etc.)
    /// </summary>
    public List<string> Features { get; set; } = [];
}
