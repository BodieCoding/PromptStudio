namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents resource requirements for workflow execution.
/// Defines the computational and infrastructure resources needed for optimal workflow performance.
/// </summary>
/// <remarks>
/// <para><strong>Resource Planning:</strong></para>
/// <para>Specifies minimum, recommended, and maximum resource allocations for workflow execution.
/// Used for capacity planning, cost estimation, and performance optimization in production environments.</para>
/// </remarks>
public class ResourceRequirements
{
    /// <summary>
    /// Gets or sets the minimum CPU cores required for execution.
    /// </summary>
    /// <value>Minimum number of CPU cores needed for basic workflow operation.</value>
    public int MinCpuCores { get; set; } = 1;

    /// <summary>
    /// Gets or sets the recommended CPU cores for optimal performance.
    /// </summary>
    /// <value>Recommended number of CPU cores for efficient execution.</value>
    public int RecommendedCpuCores { get; set; } = 2;

    /// <summary>
    /// Gets or sets the maximum CPU cores that can be utilized.
    /// </summary>
    /// <value>Maximum number of CPU cores the workflow can effectively use.</value>
    public int? MaxCpuCores { get; set; }

    /// <summary>
    /// Gets or sets the minimum memory required in megabytes.
    /// </summary>
    /// <value>Minimum RAM allocation in MB for workflow execution.</value>
    public long MinMemoryMB { get; set; } = 512;

    /// <summary>
    /// Gets or sets the recommended memory in megabytes.
    /// </summary>
    /// <value>Recommended RAM allocation in MB for optimal performance.</value>
    public long RecommendedMemoryMB { get; set; } = 1024;

    /// <summary>
    /// Gets or sets the maximum memory that may be used in megabytes.
    /// </summary>
    /// <value>Maximum RAM allocation in MB the workflow may consume.</value>
    public long? MaxMemoryMB { get; set; }

    /// <summary>
    /// Gets or sets the estimated storage space required in megabytes.
    /// </summary>
    /// <value>Estimated disk space needed for temporary files and output.</value>
    public long EstimatedStorageMB { get; set; }

    /// <summary>
    /// Gets or sets the network bandwidth requirements in Mbps.
    /// </summary>
    /// <value>Required network bandwidth for external API calls and data transfer.</value>
    public double? NetworkBandwidthMbps { get; set; }

    /// <summary>
    /// Gets or sets whether GPU acceleration is required.
    /// </summary>
    /// <value>True if the workflow requires GPU processing capabilities.</value>
    public bool RequiresGpu { get; set; }

    /// <summary>
    /// Gets or sets the estimated execution time.
    /// </summary>
    /// <value>Expected duration for workflow completion under normal conditions.</value>
    public TimeSpan? EstimatedExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets additional resource specifications.
    /// </summary>
    /// <value>Custom resource requirements specific to the workflow.</value>
    public Dictionary<string, object>? AdditionalRequirements { get; set; }
}

/// <summary>
/// Represents external service dependencies for workflow execution.
/// </summary>
public class ExternalServiceDependency
{
    /// <summary>
    /// Gets or sets the service name or identifier.
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the service endpoint or URL.
    /// </summary>
    public string? ServiceEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the required service version.
    /// </summary>
    public string? RequiredVersion { get; set; }

    /// <summary>
    /// Gets or sets whether this dependency is critical for execution.
    /// </summary>
    public bool IsCritical { get; set; } = true;

    /// <summary>
    /// Gets or sets the authentication requirements.
    /// </summary>
    public Dictionary<string, string>? AuthenticationRequirements { get; set; }

    /// <summary>
    /// Gets or sets the service health check configuration.
    /// </summary>
    public Dictionary<string, object>? HealthCheckConfig { get; set; }
}

/// <summary>
/// Represents data source dependencies for workflow execution.
/// </summary>
public class DataSourceDependency
{
    /// <summary>
    /// Gets or sets the data source name or identifier.
    /// </summary>
    public string DataSourceName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data source type (database, file, API, etc.).
    /// </summary>
    public string DataSourceType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the connection information.
    /// </summary>
    public Dictionary<string, string>? ConnectionInfo { get; set; }

    /// <summary>
    /// Gets or sets the required permissions or access level.
    /// </summary>
    public List<string>? RequiredPermissions { get; set; }

    /// <summary>
    /// Gets or sets whether this data source is critical for execution.
    /// </summary>
    public bool IsCritical { get; set; } = true;
}

/// <summary>
/// Represents library dependencies for workflow execution.
/// </summary>
public class LibraryDependency
{
    /// <summary>
    /// Gets or sets the library name.
    /// </summary>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the required library version.
    /// </summary>
    public string RequiredVersion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the library source or repository.
    /// </summary>
    public string? LibrarySource { get; set; }

    /// <summary>
    /// Gets or sets whether this library is critical for execution.
    /// </summary>
    public bool IsCritical { get; set; } = true;

    /// <summary>
    /// Gets or sets additional configuration for the library.
    /// </summary>
    public Dictionary<string, object>? LibraryConfig { get; set; }
}
