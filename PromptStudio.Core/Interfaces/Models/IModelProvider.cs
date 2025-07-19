using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Models;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Interface for AI model providers (Copilot, Ollama, MCP, etc.)
/// </summary>
public interface IModelProvider
{
    /// <summary>
    /// Provider name (e.g., "copilot", "ollama", "mcp")
    /// </summary>
    string ProviderName { get; }

    /// <summary>
    /// List available models for this provider
    /// </summary>
    Task<IEnumerable<ModelInfo>> GetAvailableModelsAsync();

    /// <summary>
    /// Execute a prompt with the specified model
    /// </summary>
    Task<ModelResponse> ExecutePromptAsync(ModelRequest request);

    /// <summary>
    /// Check if the provider is currently available
    /// </summary>
    Task<bool> IsAvailableAsync();
}

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
