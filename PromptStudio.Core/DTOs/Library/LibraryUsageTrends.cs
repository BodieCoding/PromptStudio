using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Usage trends for a prompt library
/// </summary>
public class LibraryUsageTrends
{
    /// <summary>
    /// Gets or sets the library ID
    /// </summary>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the trend analysis period
    /// </summary>
    public DateTimeRange AnalysisPeriod { get; set; } = new();

    /// <summary>
    /// Gets or sets the granularity of the trend data (hour, day, week, month)
    /// </summary>
    public string Granularity { get; set; } = "day";

    /// <summary>
    /// Gets or sets execution count trends over time
    /// </summary>
    public List<TrendDataPoint> ExecutionTrends { get; set; } = new();

    /// <summary>
    /// Gets or sets user engagement trends over time
    /// </summary>
    public List<TrendDataPoint> UserEngagementTrends { get; set; } = new();

    /// <summary>
    /// Gets or sets success rate trends over time
    /// </summary>
    public List<TrendDataPoint> SuccessRateTrends { get; set; } = new();

    /// <summary>
    /// Gets or sets performance trends over time
    /// </summary>
    public List<TrendDataPoint> PerformanceTrends { get; set; } = new();

    /// <summary>
    /// Gets or sets template popularity trends
    /// </summary>
    public Dictionary<Guid, List<TrendDataPoint>> TemplatePopularityTrends { get; set; } = new();

    /// <summary>
    /// Gets or sets usage patterns by time of day
    /// </summary>
    public Dictionary<int, double> HourlyUsagePattern { get; set; } = new();

    /// <summary>
    /// Gets or sets usage patterns by day of week
    /// </summary>
    public Dictionary<DayOfWeek, double> DailyUsagePattern { get; set; } = new();

    /// <summary>
    /// Gets or sets seasonal trends (if applicable)
    /// </summary>
    public List<SeasonalTrend> SeasonalTrends { get; set; } = new();

    /// <summary>
    /// Gets or sets growth metrics
    /// </summary>
    public GrowthMetrics GrowthMetrics { get; set; } = new();

    /// <summary>
    /// Gets or sets trend forecasts
    /// </summary>
    public List<ForecastDataPoint> Forecasts { get; set; } = new();

    /// <summary>
    /// Gets or sets insights derived from trend analysis
    /// </summary>
    public List<string> TrendInsights { get; set; } = new();

    /// <summary>
    /// Gets or sets anomalies detected in the trends
    /// </summary>
    public List<TrendAnomaly> Anomalies { get; set; } = new();

    /// <summary>
    /// Gets or sets the trend analysis completion timestamp
    /// </summary>
    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Individual trend data point
/// </summary>
public class TrendDataPoint
{
    /// <summary>
    /// Gets or sets the timestamp for this data point
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the value at this point
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Gets or sets any additional metadata for this data point
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Seasonal trend information
/// </summary>
public class SeasonalTrend
{
    /// <summary>
    /// Gets or sets the season identifier
    /// </summary>
    public string Season { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the average value for this season
    /// </summary>
    public double AverageValue { get; set; }

    /// <summary>
    /// Gets or sets the growth rate for this season
    /// </summary>
    public double GrowthRate { get; set; }

    /// <summary>
    /// Gets or sets the season start date
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the season end date
    /// </summary>
    public DateTime EndDate { get; set; }
}

/// <summary>
/// Growth metrics for the library
/// </summary>
public class GrowthMetrics
{
    /// <summary>
    /// Gets or sets the overall growth rate (percentage)
    /// </summary>
    public double OverallGrowthRate { get; set; }

    /// <summary>
    /// Gets or sets the month-over-month growth rate
    /// </summary>
    public double MonthOverMonthGrowth { get; set; }

    /// <summary>
    /// Gets or sets the week-over-week growth rate
    /// </summary>
    public double WeekOverWeekGrowth { get; set; }

    /// <summary>
    /// Gets or sets the compound annual growth rate
    /// </summary>
    public double CompoundAnnualGrowthRate { get; set; }

    /// <summary>
    /// Gets or sets whether the growth trend is positive
    /// </summary>
    public bool IsGrowthPositive { get; set; }

    /// <summary>
    /// Gets or sets the velocity of growth (acceleration/deceleration)
    /// </summary>
    public double GrowthVelocity { get; set; }
}

/// <summary>
/// Forecast data point for predictive analysis
/// </summary>
public class ForecastDataPoint
{
    /// <summary>
    /// Gets or sets the forecasted timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the forecasted value
    /// </summary>
    public double ForecastedValue { get; set; }

    /// <summary>
    /// Gets or sets the confidence level of the forecast (0.0 to 1.0)
    /// </summary>
    public double Confidence { get; set; }

    /// <summary>
    /// Gets or sets the lower bound of the forecast range
    /// </summary>
    public double LowerBound { get; set; }

    /// <summary>
    /// Gets or sets the upper bound of the forecast range
    /// </summary>
    public double UpperBound { get; set; }
}

/// <summary>
/// Anomaly detected in trend data
/// </summary>
public class TrendAnomaly
{
    /// <summary>
    /// Gets or sets the timestamp when the anomaly occurred
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the type of anomaly
    /// </summary>
    public string AnomalyType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity of the anomaly (Low, Medium, High, Critical)
    /// </summary>
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the anomaly
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the actual value at the time of anomaly
    /// </summary>
    public double ActualValue { get; set; }

    /// <summary>
    /// Gets or sets the expected value at the time of anomaly
    /// </summary>
    public double ExpectedValue { get; set; }

    /// <summary>
    /// Gets or sets the deviation from expected (percentage)
    /// </summary>
    public double DeviationPercentage { get; set; }

    /// <summary>
    /// Gets or sets potential causes of the anomaly
    /// </summary>
    public List<string> PotentialCauses { get; set; } = new();

    /// <summary>
    /// Gets or sets recommended actions to address the anomaly
    /// </summary>
    public List<string> RecommendedActions { get; set; } = new();
}
