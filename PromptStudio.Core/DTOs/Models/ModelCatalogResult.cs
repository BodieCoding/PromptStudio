using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Result of model catalog discovery operation.
/// </summary>
public class ModelCatalogResult : OperationResult
{
    /// <summary>
    /// Gets or sets the discovered models.
    /// </summary>
    public List<AIModelInfo>? Models { get; set; }

    /// <summary>
    /// Gets or sets the total number of models available.
    /// </summary>
    public int TotalModels { get; set; }

    /// <summary>
    /// Gets or sets provider-specific discovery results.
    /// </summary>
    public Dictionary<Guid, ProviderDiscoveryResult>? ProviderResults { get; set; }

    /// <summary>
    /// Gets or sets the discovery timestamp.
    /// </summary>
    public DateTime DiscoveredAt { get; set; }
}

/// <summary>
/// Discovery result for a specific provider.
/// </summary>
public class ProviderDiscoveryResult
{
    /// <summary>
    /// Gets or sets the provider ID.
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Gets or sets whether discovery was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the number of models discovered.
    /// </summary>
    public int ModelsDiscovered { get; set; }

    /// <summary>
    /// Gets or sets any error message.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the discovery duration.
    /// </summary>
    public TimeSpan Duration { get; set; }
}

/// <summary>
/// Comprehensive information about an AI model.
/// </summary>
public class AIModelInfo
{
    /// <summary>
    /// Gets or sets the model identifier.
    /// </summary>
    public string ModelId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the model name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the provider ID that hosts this model.
    /// </summary>
    public Guid ProviderId { get; set; }

    /// <summary>
    /// Gets or sets the model description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the model capabilities.
    /// </summary>
    public List<string>? Capabilities { get; set; }

    /// <summary>
    /// Gets or sets the model type (e.g., "text", "image", "code").
    /// </summary>
    public string? ModelType { get; set; }

    /// <summary>
    /// Gets or sets the maximum token limit.
    /// </summary>
    public int? MaxTokens { get; set; }

    /// <summary>
    /// Gets or sets whether the model is currently available.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Gets or sets performance metrics.
    /// </summary>
    public ModelPerformanceMetrics? PerformanceMetrics { get; set; }

    /// <summary>
    /// Gets or sets cost information.
    /// </summary>
    public ModelCostInfo? CostInfo { get; set; }

    /// <summary>
    /// Gets or sets model-specific configuration.
    /// </summary>
    public Dictionary<string, object>? Configuration { get; set; }
}

/// <summary>
/// Performance metrics for a model.
/// </summary>
public class ModelPerformanceMetrics
{
    /// <summary>
    /// Gets or sets the average response time.
    /// </summary>
    public double AverageResponseTimeMs { get; set; }

    /// <summary>
    /// Gets or sets the success rate percentage.
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the throughput in requests per minute.
    /// </summary>
    public double ThroughputRpm { get; set; }

    /// <summary>
    /// Gets or sets quality metrics.
    /// </summary>
    public Dictionary<string, double>? QualityMetrics { get; set; }
}

/// <summary>
/// Cost information for a model.
/// </summary>
public class ModelCostInfo
{
    /// <summary>
    /// Gets or sets the cost per input token.
    /// </summary>
    public decimal? InputTokenCost { get; set; }

    /// <summary>
    /// Gets or sets the cost per output token.
    /// </summary>
    public decimal? OutputTokenCost { get; set; }

    /// <summary>
    /// Gets or sets the currency used for pricing.
    /// </summary>
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Gets or sets additional cost factors.
    /// </summary>
    public Dictionary<string, decimal>? AdditionalCosts { get; set; }
}
