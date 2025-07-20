using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Filter options for provider queries.
/// </summary>
public class ProviderFilterOptions
{
    /// <summary>
    /// Gets or sets the provider types to filter by.
    /// </summary>
    public List<string>? ProviderTypes { get; set; }

    /// <summary>
    /// Gets or sets whether to include only enabled providers.
    /// </summary>
    public bool? EnabledOnly { get; set; }

    /// <summary>
    /// Gets or sets the health status filter.
    /// </summary>
    public List<string>? HealthStatuses { get; set; }

    /// <summary>
    /// Gets or sets capabilities that providers must have.
    /// </summary>
    public List<string>? RequiredCapabilities { get; set; }

    /// <summary>
    /// Gets or sets pagination settings.
    /// </summary>
    public PageRequest? Pagination { get; set; }
}

/// <summary>
/// Filter options for model discovery.
/// </summary>
public class ModelDiscoveryFilterOptions
{
    /// <summary>
    /// Gets or sets the provider IDs to search within.
    /// </summary>
    public List<Guid>? ProviderIds { get; set; }

    /// <summary>
    /// Gets or sets model capabilities to filter by.
    /// </summary>
    public List<string>? RequiredCapabilities { get; set; }

    /// <summary>
    /// Gets or sets the model types to include.
    /// </summary>
    public List<string>? ModelTypes { get; set; }

    /// <summary>
    /// Gets or sets whether to include only available models.
    /// </summary>
    public bool? AvailableOnly { get; set; } = true;

    /// <summary>
    /// Gets or sets pagination settings.
    /// </summary>
    public PageRequest? Pagination { get; set; }
}

/// <summary>
/// Request to update a provider configuration.
/// </summary>
public class UpdateProviderRequest
{
    /// <summary>
    /// Gets or sets the updated provider name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the updated description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets whether the provider is enabled.
    /// </summary>
    public bool? IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets the updated connection settings.
    /// </summary>
    public Dictionary<string, object>? ConnectionSettings { get; set; }

    /// <summary>
    /// Gets or sets the updated security settings.
    /// </summary>
    public Dictionary<string, object>? SecuritySettings { get; set; }

    /// <summary>
    /// Gets or sets the updated priority.
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// Gets or sets custom properties to update.
    /// </summary>
    public Dictionary<string, object>? CustomProperties { get; set; }
}

/// <summary>
/// Result of provider update operation.
/// </summary>
public class ProviderUpdateResult : OperationResult
{
    /// <summary>
    /// Gets or sets the updated provider information.
    /// </summary>
    public AIProviderInfo? UpdatedProvider { get; set; }

    /// <summary>
    /// Gets or sets changes that were applied.
    /// </summary>
    public Dictionary<string, object>? AppliedChanges { get; set; }

    /// <summary>
    /// Gets or sets validation results for the update.
    /// </summary>
    public Dictionary<string, bool>? ValidationResults { get; set; }
}
