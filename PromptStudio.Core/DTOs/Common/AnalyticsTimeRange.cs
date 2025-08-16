namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Analytics time range with predefined periods and custom range support
/// </summary>
public class AnalyticsTimeRange
{
    /// <summary>
    /// Start of the time range
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// End of the time range
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Predefined time period (if applicable)
    /// </summary>
    public string? PresetPeriod { get; set; }

    /// <summary>
    /// Time zone for the range
    /// </summary>
    public string TimeZone { get; set; } = "UTC";

    /// <summary>
    /// Aggregation level for the data
    /// </summary>
    public AnalyticsAggregationLevel AggregationLevel { get; set; } = AnalyticsAggregationLevel.Hourly;

    /// <summary>
    /// Duration of the time range
    /// </summary>
    public TimeSpan Duration => EndTime - StartTime;

    /// <summary>
    /// Whether this is a real-time range (updated continuously)
    /// </summary>
    public bool IsRealTime { get; set; } = false;

    /// <summary>
    /// Custom range label
    /// </summary>
    public string? Label { get; set; }

    public AnalyticsTimeRange()
    {
        // Default to last 24 hours
        EndTime = DateTime.UtcNow;
        StartTime = EndTime.AddDays(-1);
    }

    public AnalyticsTimeRange(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    /// <summary>
    /// Create a predefined time range
    /// </summary>
    /// <param name="preset">Preset period (e.g., "last-24h", "last-7d", "last-30d")</param>
    /// <returns>Analytics time range</returns>
    public static AnalyticsTimeRange CreatePreset(string preset)
    {
        var now = DateTime.UtcNow;
        return preset.ToLower() switch
        {
            "last-1h" => new AnalyticsTimeRange(now.AddHours(-1), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Minute },
            "last-6h" => new AnalyticsTimeRange(now.AddHours(-6), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.FiveMinutes },
            "last-24h" => new AnalyticsTimeRange(now.AddDays(-1), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Hourly },
            "last-7d" => new AnalyticsTimeRange(now.AddDays(-7), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Daily },
            "last-30d" => new AnalyticsTimeRange(now.AddDays(-30), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Daily },
            "last-90d" => new AnalyticsTimeRange(now.AddDays(-90), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Weekly },
            "last-1y" => new AnalyticsTimeRange(now.AddYears(-1), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Monthly },
            "today" => new AnalyticsTimeRange(now.Date, now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Hourly },
            "yesterday" => new AnalyticsTimeRange(now.Date.AddDays(-1), now.Date) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Hourly },
            "this-week" => new AnalyticsTimeRange(now.Date.AddDays(-(int)now.DayOfWeek), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Daily },
            "this-month" => new AnalyticsTimeRange(new DateTime(now.Year, now.Month, 1), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Daily },
            "this-quarter" => new AnalyticsTimeRange(GetQuarterStart(now), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Weekly },
            "this-year" => new AnalyticsTimeRange(new DateTime(now.Year, 1, 1), now) { PresetPeriod = preset, AggregationLevel = AnalyticsAggregationLevel.Monthly },
            _ => new AnalyticsTimeRange(now.AddDays(-1), now) { PresetPeriod = "last-24h", AggregationLevel = AnalyticsAggregationLevel.Hourly }
        };
    }

    private static DateTime GetQuarterStart(DateTime date)
    {
        int quarterNumber = (date.Month - 1) / 3 + 1;
        int quarterStartMonth = (quarterNumber - 1) * 3 + 1;
        return new DateTime(date.Year, quarterStartMonth, 1);
    }
}
