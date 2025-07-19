namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Extended interface for enhanced model provider management with cost estimation and validation
/// Extends the existing IModelProviderManager from IModelProvider.cs
/// </summary>
public interface IEnhancedModelProviderManager : IModelProviderManager
{
    /// <summary>
    /// Get available models from all providers
    /// </summary>
    Task<IEnumerable<EnhancedModelInfo>> GetAvailableModelsAsync();
    
    /// <summary>
    /// Validate model availability and configuration
    /// </summary>
    Task<bool> ValidateModelAsync(string modelId);
    
    /// <summary>
    /// Get estimated cost for a request
    /// </summary>
    Task<decimal> EstimateCostAsync(ModelRequest request);
}

/// <summary>
/// Enhanced model information with cost and capability details
/// </summary>
public class EnhancedModelInfo : ModelInfo
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
    public List<string> Features { get; set; } = new();
}
