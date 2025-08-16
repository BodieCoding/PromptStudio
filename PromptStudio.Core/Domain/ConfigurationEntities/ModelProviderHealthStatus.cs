namespace PromptStudio.Core.Domain;

/// <summary>
/// Enumeration of model provider health status values.
/// Tracks the operational status of model provider configurations.
/// </summary>
public enum ModelProviderHealthStatus
{
    /// <summary>
    /// Health status is unknown or not yet checked.
    /// </summary>
    Unknown,

    /// <summary>
    /// Provider is healthy and operational.
    /// </summary>
    Healthy,

    /// <summary>
    /// Provider is experiencing performance issues but still functional.
    /// </summary>
    Degraded,

    /// <summary>
    /// Provider is unhealthy or not responding.
    /// </summary>
    Unhealthy,

    /// <summary>
    /// Provider is temporarily disabled for maintenance.
    /// </summary>
    Maintenance
}
