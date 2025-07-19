using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Time range specification for analytics queries
/// </summary>
public class AnalyticsTimeRange
{
    /// <summary>
    /// Start date/time for the analytics period
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// End date/time for the analytics period
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Time zone for the analytics data
    /// </summary>
    public string? TimeZone { get; set; }

    /// <summary>
    /// Granularity for time-series data (hour, day, week, month)
    /// </summary>
    public string Granularity { get; set; } = "day";

    /// <summary>
    /// Whether to include partial periods at the edges
    /// </summary>
    public bool IncludePartialPeriods { get; set; } = false;

    /// <summary>
    /// Predefined time range type (today, this_week, last_30_days, etc.)
    /// </summary>
    public string? PredefinedRange { get; set; }

    /// <summary>
    /// Convert to DateTimeRange
    /// </summary>
    public DateTimeRange ToDateTimeRange()
    {
        return new DateTimeRange
        {
            StartDate = StartDate,
            EndDate = EndDate
        };
    }

    /// <summary>
    /// Create analytics time range from predefined range
    /// </summary>
    public static AnalyticsTimeRange FromPredefined(string range, string? timeZone = null)
    {
        var now = DateTime.UtcNow;
        var result = new AnalyticsTimeRange { TimeZone = timeZone, PredefinedRange = range };

        switch (range.ToLower())
        {
            case "today":
                result.StartDate = now.Date;
                result.EndDate = now.Date.AddDays(1).AddTicks(-1);
                result.Granularity = "hour";
                break;
            case "yesterday":
                result.StartDate = now.Date.AddDays(-1);
                result.EndDate = now.Date.AddTicks(-1);
                result.Granularity = "hour";
                break;
            case "this_week":
                var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek);
                result.StartDate = startOfWeek;
                result.EndDate = startOfWeek.AddDays(7).AddTicks(-1);
                result.Granularity = "day";
                break;
            case "last_week":
                var lastWeekStart = now.Date.AddDays(-(int)now.DayOfWeek - 7);
                result.StartDate = lastWeekStart;
                result.EndDate = lastWeekStart.AddDays(7).AddTicks(-1);
                result.Granularity = "day";
                break;
            case "this_month":
                result.StartDate = new DateTime(now.Year, now.Month, 1);
                result.EndDate = result.StartDate.AddMonths(1).AddTicks(-1);
                result.Granularity = "day";
                break;
            case "last_month":
                var lastMonthStart = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                result.StartDate = lastMonthStart;
                result.EndDate = lastMonthStart.AddMonths(1).AddTicks(-1);
                result.Granularity = "day";
                break;
            case "last_7_days":
                result.StartDate = now.Date.AddDays(-7);
                result.EndDate = now;
                result.Granularity = "day";
                break;
            case "last_30_days":
                result.StartDate = now.Date.AddDays(-30);
                result.EndDate = now;
                result.Granularity = "day";
                break;
            case "last_90_days":
                result.StartDate = now.Date.AddDays(-90);
                result.EndDate = now;
                result.Granularity = "week";
                break;
            default:
                throw new ArgumentException($"Unknown predefined range: {range}");
        }

        return result;
    }
}
