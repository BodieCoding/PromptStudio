namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents resource utilization metrics during flow execution.
/// Tracks system resources consumed by workflow operations for monitoring and optimization.
/// </summary>
/// <remarks>
/// <para><strong>Resource Monitoring:</strong></para>
/// <para>Provides comprehensive tracking of system resources including CPU, memory,
/// network, and API usage during flow execution. Essential for performance optimization,
/// cost management, and capacity planning in production environments.</para>
/// </remarks>
public class ResourceUtilization
{
    /// <summary>
    /// Gets or sets the unique identifier for this resource utilization record.
    /// Enables correlation across monitoring and analysis systems.
    /// </summary>
    /// <value>
    /// A unique identifier for this utilization measurement.
    /// </value>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the timestamp when these metrics were recorded.
    /// Enables temporal analysis of resource usage patterns.
    /// </summary>
    /// <value>
    /// The UTC timestamp when this measurement was taken.
    /// </value>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the duration over which these metrics were collected.
    /// Indicates the measurement window for these resource values.
    /// </summary>
    /// <value>
    /// The time span over which these metrics were measured.
    /// </value>
    public TimeSpan MeasurementDuration { get; set; }

    /// <summary>
    /// Gets or sets the CPU usage percentage.
    /// Tracks processor utilization during execution.
    /// </summary>
    /// <value>
    /// CPU usage as a percentage (0-100), if available.
    /// </value>
    public double? CpuUsagePercent { get; set; }

    /// <summary>
    /// Gets or sets the memory usage in bytes.
    /// Tracks RAM consumption during execution.
    /// </summary>
    /// <value>
    /// Memory usage in bytes, if available.
    /// </value>
    public long? MemoryUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the peak memory usage in bytes.
    /// Tracks the maximum memory consumption during the measurement period.
    /// </summary>
    /// <value>
    /// Peak memory usage in bytes, if available.
    /// </value>
    public long? PeakMemoryUsageBytes { get; set; }

    /// <summary>
    /// Gets or sets the number of garbage collections that occurred.
    /// Tracks memory management overhead during execution.
    /// </summary>
    /// <value>
    /// The count of garbage collection events, if available.
    /// </value>
    public int? GarbageCollections { get; set; }

    /// <summary>
    /// Gets or sets the network bytes sent.
    /// Tracks outbound network traffic during execution.
    /// </summary>
    /// <value>
    /// The number of bytes sent over the network, if available.
    /// </value>
    public long? NetworkBytesSent { get; set; }

    /// <summary>
    /// Gets or sets the network bytes received.
    /// Tracks inbound network traffic during execution.
    /// </summary>
    /// <value>
    /// The number of bytes received from the network, if available.
    /// </value>
    public long? NetworkBytesReceived { get; set; }

    /// <summary>
    /// Gets or sets the number of API calls made.
    /// Tracks external service invocations during execution.
    /// </summary>
    /// <value>
    /// The count of API calls made during the measurement period.
    /// </value>
    public int ApiCalls { get; set; }

    /// <summary>
    /// Gets or sets the total tokens consumed in API calls.
    /// Tracks language model token usage for cost management.
    /// </summary>
    /// <value>
    /// The total number of tokens consumed, if available.
    /// </value>
    public long? TotalTokensConsumed { get; set; }

    /// <summary>
    /// Gets or sets the input tokens consumed in API calls.
    /// Tracks prompt token usage for detailed cost analysis.
    /// </summary>
    /// <value>
    /// The number of input tokens consumed, if available.
    /// </value>
    public long? InputTokensConsumed { get; set; }

    /// <summary>
    /// Gets or sets the output tokens generated in API calls.
    /// Tracks completion token usage for detailed cost analysis.
    /// </summary>
    /// <value>
    /// The number of output tokens generated, if available.
    /// </value>
    public long? OutputTokensConsumed { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost of API usage.
    /// Tracks financial cost of external service consumption.
    /// </summary>
    /// <value>
    /// The estimated cost in the default currency, if available.
    /// </value>
    public decimal? EstimatedCost { get; set; }

    /// <summary>
    /// Gets or sets the currency for cost calculations.
    /// Specifies the currency unit for cost estimates.
    /// </summary>
    /// <value>
    /// The currency code (e.g., "USD", "EUR") for cost values.
    /// </value>
    public string? Currency { get; set; }

    /// <summary>
    /// Gets or sets the disk I/O read bytes.
    /// Tracks storage read operations during execution.
    /// </summary>
    /// <value>
    /// The number of bytes read from disk, if available.
    /// </value>
    public long? DiskReadBytes { get; set; }

    /// <summary>
    /// Gets or sets the disk I/O write bytes.
    /// Tracks storage write operations during execution.
    /// </summary>
    /// <value>
    /// The number of bytes written to disk, if available.
    /// </value>
    public long? DiskWriteBytes { get; set; }

    /// <summary>
    /// Gets or sets the number of database queries executed.
    /// Tracks database access patterns during execution.
    /// </summary>
    /// <value>
    /// The count of database queries executed.
    /// </value>
    public int DatabaseQueries { get; set; }

    /// <summary>
    /// Gets or sets the total database query execution time.
    /// Tracks time spent in database operations.
    /// </summary>
    /// <value>
    /// The cumulative time spent executing database queries.
    /// </value>
    public TimeSpan DatabaseQueryTime { get; set; }

    /// <summary>
    /// Gets or sets the number of cache hits.
    /// Tracks cache effectiveness during execution.
    /// </summary>
    /// <value>
    /// The count of successful cache lookups.
    /// </value>
    public int CacheHits { get; set; }

    /// <summary>
    /// Gets or sets the number of cache misses.
    /// Tracks cache effectiveness during execution.
    /// </summary>
    /// <value>
    /// The count of failed cache lookups.
    /// </value>
    public int CacheMisses { get; set; }

    /// <summary>
    /// Gets or sets additional resource metrics.
    /// Provides extensible storage for custom resource measurements.
    /// </summary>
    /// <value>
    /// A dictionary containing additional resource metrics and measurements.
    /// </value>
    public Dictionary<string, object> AdditionalMetrics { get; set; } = new();

    /// <summary>
    /// Gets or sets the context in which these metrics were collected.
    /// Provides information about the measurement environment and scope.
    /// </summary>
    /// <value>
    /// A dictionary containing context information for the measurements.
    /// </value>
    public Dictionary<string, object> Context { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the ResourceUtilization class with default values.
    /// </summary>
    public ResourceUtilization()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ResourceUtilization class with a measurement duration.
    /// </summary>
    /// <param name="measurementDuration">The duration over which metrics were collected</param>
    public ResourceUtilization(TimeSpan measurementDuration)
    {
        MeasurementDuration = measurementDuration;
    }

    /// <summary>
    /// Calculates the cache hit ratio as a percentage.
    /// </summary>
    /// <returns>Cache hit ratio as a percentage (0-100), or null if no cache operations occurred</returns>
    public double? CalculateCacheHitRatio()
    {
        var totalCacheOperations = CacheHits + CacheMisses;
        return totalCacheOperations > 0 ? (double)CacheHits / totalCacheOperations * 100 : null;
    }

    /// <summary>
    /// Calculates the API calls per second rate.
    /// </summary>
    /// <returns>API calls per second, or null if measurement duration is zero</returns>
    public double? CalculateApiCallsPerSecond()
    {
        return MeasurementDuration.TotalSeconds > 0 ? ApiCalls / MeasurementDuration.TotalSeconds : null;
    }

    /// <summary>
    /// Calculates the total network throughput in bytes per second.
    /// </summary>
    /// <returns>Network throughput in bytes per second, or null if no network data or duration</returns>
    public double? CalculateNetworkThroughput()
    {
        if (MeasurementDuration.TotalSeconds <= 0)
            return null;

        var totalBytes = (NetworkBytesSent ?? 0) + (NetworkBytesReceived ?? 0);
        return totalBytes > 0 ? totalBytes / MeasurementDuration.TotalSeconds : null;
    }

    /// <summary>
    /// Calculates the average tokens per API call.
    /// </summary>
    /// <returns>Average tokens per API call, or null if no API calls or token data</returns>
    public double? CalculateAverageTokensPerCall()
    {
        return ApiCalls > 0 && TotalTokensConsumed.HasValue ? 
            (double)TotalTokensConsumed.Value / ApiCalls : null;
    }

    /// <summary>
    /// Gets the memory usage in megabytes for easier reading.
    /// </summary>
    /// <returns>Memory usage in MB, or null if not available</returns>
    public double? GetMemoryUsageMB()
    {
        return MemoryUsageBytes.HasValue ? MemoryUsageBytes.Value / (1024.0 * 1024.0) : null;
    }

    /// <summary>
    /// Gets the peak memory usage in megabytes for easier reading.
    /// </summary>
    /// <returns>Peak memory usage in MB, or null if not available</returns>
    public double? GetPeakMemoryUsageMB()
    {
        return PeakMemoryUsageBytes.HasValue ? PeakMemoryUsageBytes.Value / (1024.0 * 1024.0) : null;
    }

    /// <summary>
    /// Adds a custom metric to the additional metrics collection.
    /// </summary>
    /// <param name="name">The metric name</param>
    /// <param name="value">The metric value</param>
    /// <returns>This resource utilization instance for method chaining</returns>
    public ResourceUtilization AddMetric(string name, object value)
    {
        AdditionalMetrics[name] = value;
        return this;
    }

    /// <summary>
    /// Adds context information to this resource utilization record.
    /// </summary>
    /// <param name="key">The context key</param>
    /// <param name="value">The context value</param>
    /// <returns>This resource utilization instance for method chaining</returns>
    public ResourceUtilization AddContext(string key, object value)
    {
        Context[key] = value;
        return this;
    }

    /// <summary>
    /// Returns a string representation of the resource utilization metrics.
    /// </summary>
    /// <returns>String representation including key resource metrics</returns>
    public override string ToString()
    {
        var components = new List<string>();

        if (CpuUsagePercent.HasValue)
            components.Add($"CPU: {CpuUsagePercent.Value:F1}%");

        if (MemoryUsageBytes.HasValue)
            components.Add($"Memory: {GetMemoryUsageMB():F1}MB");

        if (ApiCalls > 0)
            components.Add($"API Calls: {ApiCalls}");

        if (TotalTokensConsumed.HasValue)
            components.Add($"Tokens: {TotalTokensConsumed.Value:N0}");

        if (EstimatedCost.HasValue)
            components.Add($"Cost: {EstimatedCost.Value:C} {Currency}");

        return $"ResourceUtilization: {string.Join(", ", components)}";
    }
}
