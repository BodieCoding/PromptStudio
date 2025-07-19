using PromptStudio.Core.DTOs.Models;
using PromptStudio.Core.Interfaces.Models;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Extended interface for advanced model provider management with cost estimation and validation
/// Extends the existing IModelProviderManager from IModelProvider.cs
/// </summary>
public interface IAdvancedModelProviderManager : IModelProviderManager
{
    /// <summary>
    /// Get available models from all providers
    /// </summary>
    Task<IEnumerable<DetailedModelInfo>> GetAvailableModelsAsync();
    
    /// <summary>
    /// Validate model availability and configuration
    /// </summary>
    Task<bool> ValidateModelAsync(string modelId);
    
    /// <summary>
    /// Get estimated cost for a request
    /// </summary>
    Task<decimal> EstimateCostAsync(ModelRequest request);
}

