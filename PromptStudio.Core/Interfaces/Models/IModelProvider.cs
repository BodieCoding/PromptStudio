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
