using PromptStudio.Core.Interfaces;
using PromptStudio.Core.DTOs.Models;
using Microsoft.Extensions.Logging;

namespace PromptStudio.Core.Services;

/// <summary>
/// Manages multiple model providers and routes requests to the appropriate provider
/// </summary>
public class ModelProviderManager : IModelProviderManager
{
    private readonly ILogger<ModelProviderManager> _logger;
    private readonly IEnumerable<IModelProvider> _providers;

    public ModelProviderManager(ILogger<ModelProviderManager> logger, IEnumerable<IModelProvider> providers)
    {
        _logger = logger;
        _providers = providers;
    }

    public IEnumerable<IModelProvider> GetProviders()
    {
        return _providers;
    }

    public IModelProvider? GetProvider(string providerName)
    {
        return _providers.FirstOrDefault(p => p.ProviderName.Equals(providerName, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<ModelInfo>> GetAllAvailableModelsAsync()
    {
        var allModels = new List<ModelInfo>();

        foreach (var provider in _providers)
        {
            try
            {
                if (await provider.IsAvailableAsync())
                {
                    var models = await provider.GetAvailableModelsAsync();
                    allModels.AddRange(models);
                }
                else
                {
                    _logger.LogDebug("Provider {ProviderName} is not available", provider.ProviderName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting models from provider {ProviderName}", provider.ProviderName);
            }
        }

        return allModels;
    }

    public async Task<ModelResponse> ExecutePromptAsync(ModelRequest request)
    {
        try
        {
            // Determine which provider should handle this request
            var provider = DetermineProvider(request.ModelId);
            
            if (provider == null)
            {
                return new ModelResponse
                {
                    Success = false,
                    ErrorMessage = $"No provider found for model '{request.ModelId}'"
                };
            }

            // Check if the provider is available
            if (!await provider.IsAvailableAsync())
            {
                return new ModelResponse
                {
                    Success = false,
                    ErrorMessage = $"Provider '{provider.ProviderName}' is not currently available"
                };
            }

            // Execute the request
            _logger.LogInformation("Routing prompt to provider {ProviderName} for model {ModelId}", 
                provider.ProviderName, request.ModelId);
            
            return await provider.ExecutePromptAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing prompt for model {ModelId}", request.ModelId);
            
            return new ModelResponse
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    private IModelProvider? DetermineProvider(string modelId)
    {
        // Strategy 1: Check if model ID contains provider prefix
        if (modelId.StartsWith("mcp-", StringComparison.OrdinalIgnoreCase))
        {
            return GetProvider("mcp");
        }
        
        if (modelId.StartsWith("ollama-", StringComparison.OrdinalIgnoreCase))
        {
            return GetProvider("ollama");
        }

        // Strategy 2: Check common model names
        var commonCopilotModels = new[] { "gpt-4", "gpt-3.5-turbo", "gpt-4-turbo" };
        if (commonCopilotModels.Contains(modelId, StringComparer.OrdinalIgnoreCase))
        {
            return GetProvider("copilot");
        }

        // Strategy 3: Query each provider to see if they support the model
        foreach (var provider in _providers)
        {
            try
            {
                var models = provider.GetAvailableModelsAsync().Result;
                if (models.Any(m => m.Id.Equals(modelId, StringComparison.OrdinalIgnoreCase)))
                {
                    return provider;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error checking if provider {ProviderName} supports model {ModelId}", 
                    provider.ProviderName, modelId);
            }
        }

        // Strategy 4: Default to the first available provider
        return _providers.FirstOrDefault();
    }
}
