using PromptStudio.Core.DTOs.Models;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Interface for managing multiple model providers
/// </summary>
public interface IModelProviderManager
{
    /// <summary>
    /// Get all registered providers
    /// </summary>
    IEnumerable<IModelProvider> GetProviders();

    /// <summary>
    /// Get a specific provider by name
    /// </summary>
    IModelProvider? GetProvider(string providerName);

    /// <summary>
    /// Get all available models from all providers
    /// </summary>
    Task<IEnumerable<ModelInfo>> GetAllAvailableModelsAsync();

    /// <summary>
    /// Execute a prompt using the appropriate provider
    /// </summary>
    Task<ModelResponse> ExecutePromptAsync(ModelRequest request);
}
