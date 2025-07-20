using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Configuration settings for registering an AI model provider.
/// </summary>
public class AIProviderConfiguration
{
    /// <summary>
    /// Gets or sets the provider type (e.g., "OpenAI", "Azure", "Anthropic").
    /// </summary>
    public string ProviderType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the friendly name for the provider.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the provider description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the connection settings for the provider.
    /// </summary>
    public Dictionary<string, object>? ConnectionSettings { get; set; }

    /// <summary>
    /// Gets or sets the security settings for the provider.
    /// </summary>
    public Dictionary<string, object>? SecuritySettings { get; set; }

    /// <summary>
    /// Gets or sets whether the provider is enabled by default.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the priority order for provider selection.
    /// </summary>
    public int Priority { get; set; } = 100;

    /// <summary>
    /// Gets or sets provider-specific configuration properties.
    /// </summary>
    public Dictionary<string, object>? CustomProperties { get; set; }
}

/// <summary>
/// Result of provider registration operation.
/// </summary>
public class ProviderRegistrationResult : OperationResult
{
    /// <summary>
    /// Gets or sets the registered provider identifier.
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Gets or sets the provider configuration details.
    /// </summary>
    public AIProviderConfiguration? ProviderConfig { get; set; }

    /// <summary>
    /// Gets or sets the discovered capabilities.
    /// </summary>
    public List<string>? DiscoveredCapabilities { get; set; }

    /// <summary>
    /// Gets or sets the validation results.
    /// </summary>
    public Dictionary<string, bool>? ValidationResults { get; set; }
}

/// <summary>
/// Comprehensive information about an AI provider.
/// </summary>
public class AIProviderInfo
{
    /// <summary>
    /// Gets or sets the provider identifier.
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Gets or sets the provider name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the provider type.
    /// </summary>
    public string ProviderType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the provider description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets whether the provider is currently enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets the provider health status.
    /// </summary>
    public string HealthStatus { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the available models.
    /// </summary>
    public List<string>? AvailableModels { get; set; }

    /// <summary>
    /// Gets or sets the provider capabilities.
    /// </summary>
    public List<string>? Capabilities { get; set; }

    /// <summary>
    /// Gets or sets performance metrics (if requested).
    /// </summary>
    public Dictionary<string, object>? PerformanceMetrics { get; set; }

    /// <summary>
    /// Gets or sets the provider configuration timestamp.
    /// </summary>
    public DateTime ConfiguredAt { get; set; }

    /// <summary>
    /// Gets or sets the last health check timestamp.
    /// </summary>
    public DateTime? LastHealthCheckAt { get; set; }
}
