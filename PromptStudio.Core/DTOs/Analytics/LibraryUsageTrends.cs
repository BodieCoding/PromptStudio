namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Represents usage trend analysis for a library, including historical data, growth metrics, and predictive insights.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary DTO for analytics services providing library trend analysis, usage forecasting, and strategic insights.
/// Used by recommendation engines to identify trending libraries and by governance services for capacity planning.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Complex aggregated data structure containing time-series trend points, computed growth rates, and AI-generated insights.
/// Designed for efficient serialization of large trend datasets with minimal data loss.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Trend visualization in analytics dashboards</item>
/// <item>Usage forecasting and capacity planning</item>
/// <item>Library recommendation based on trend analysis</item>
/// <item>Strategic decision support for library governance</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Contains nested collections and complex objects. Consider pagination for large trend datasets.
/// Dictionary serialization is optimized for JSON but may require custom handling in binary formats.
/// Insights collection should be limited to prevent payload bloat.</para>
/// </remarks>
public class LibraryUsageTrends
{
    /// <summary>
    /// Gets or sets the unique identifier of the library being analyzed.
    /// </summary>
    /// <value>A valid GUID representing the library entity.</value>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the time-series trend data points indexed by date.
    /// </summary>
    /// <value>A dictionary mapping dates to trend data points, representing usage patterns over time.</value>
    public Dictionary<DateTime, UsageTrendPoint> TrendData { get; set; } = new();

    /// <summary>
    /// Gets or sets the calculated growth rate as a decimal percentage.
    /// </summary>
    /// <value>A decimal representing the growth rate, where positive values indicate growth and negative values indicate decline.</value>
    public double GrowthRate { get; set; }

    /// <summary>
    /// Gets or sets the collection of AI-generated insights about the usage trends.
    /// </summary>
    /// <value>A list of trend insights providing analytical observations and recommendations.</value>
    public List<TrendInsight> Insights { get; set; } = new();

    /// <summary>
    /// Gets or sets the forecasting data and predictions for future usage.
    /// </summary>
    /// <value>A dictionary containing various forecast models and their predictions, keyed by forecast type.</value>
    public Dictionary<string, object> Forecasts { get; set; } = new();
}
