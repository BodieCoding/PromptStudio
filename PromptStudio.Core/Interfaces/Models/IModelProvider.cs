using PromptStudio.Core.Domain;

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
/// Information about an available model
/// </summary>
public class ModelInfo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public Dictionary<string, object> Capabilities { get; set; } = new();
}

/// <summary>
/// Request to execute a prompt
/// </summary>
public class ModelRequest
{
    public string ModelId { get; set; } = string.Empty;
    public string Prompt { get; set; } = string.Empty;
    public string? SystemMessage { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
    public Dictionary<string, object> Variables { get; set; } = new();
}

/// <summary>
/// Response from model execution
/// </summary>
public class ModelResponse
{
    public bool Success { get; set; }
    public string? Content { get; set; }
    public string? ErrorMessage { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();
    public long ExecutionTimeMs { get; set; }
    public int TokensUsed { get; set; }
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
