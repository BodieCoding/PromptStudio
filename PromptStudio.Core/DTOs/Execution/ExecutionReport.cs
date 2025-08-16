using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Generated execution report with analytics and insights
/// </summary>
public class ExecutionReport
{
    /// <summary>
    /// Report ID
    /// </summary>
    public Guid ReportId { get; set; }

    /// <summary>
    /// Report type identifier
    /// </summary>
    public string ReportType { get; set; } = string.Empty;

    /// <summary>
    /// Report title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Report description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// When the report was generated
    /// </summary>
    public DateTime GeneratedAt { get; set; }

    /// <summary>
    /// User who generated the report
    /// </summary>
    public Guid GeneratedBy { get; set; }

    /// <summary>
    /// Time range covered by the report
    /// </summary>
    public DateTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Executive summary of key findings
    /// </summary>
    public ExecutionReportSummary Summary { get; set; } = new();

    /// <summary>
    /// Detailed analytics sections
    /// </summary>
    public List<ExecutionReportSection> Sections { get; set; } = [];

    /// <summary>
    /// Key performance indicators
    /// </summary>
    public List<ExecutionReportKPI> KeyPerformanceIndicators { get; set; } = [];

    /// <summary>
    /// Charts and visualizations data
    /// </summary>
    public List<ExecutionReportChart>? Charts { get; set; }

    /// <summary>
    /// Recommendations based on the analysis
    /// </summary>
    public List<string> Recommendations { get; set; } = [];

    /// <summary>
    /// Report parameters used for generation
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = [];

    /// <summary>
    /// Report metadata
    /// </summary>
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// Report format (HTML, PDF, JSON, etc.)
    /// </summary>
    public string Format { get; set; } = "JSON";

    /// <summary>
    /// Report data export URL or path
    /// </summary>
    public string? DataExportUrl { get; set; }
}

/// <summary>
/// Executive summary of the execution report
/// </summary>
public class ExecutionReportSummary
{
    /// <summary>
    /// Total number of executions analyzed
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Overall success rate
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Average response time
    /// </summary>
    public double AverageResponseTimeMs { get; set; }

    /// <summary>
    /// Total cost for the period
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Average quality score
    /// </summary>
    public double? AverageQualityScore { get; set; }

    /// <summary>
    /// Key highlights and insights
    /// </summary>
    public List<string> KeyHighlights { get; set; } = [];

    /// <summary>
    /// Notable trends or patterns
    /// </summary>
    public List<string> NotableTrends { get; set; } = [];

    /// <summary>
    /// Areas of concern
    /// </summary>
    public List<string> Concerns { get; set; } = [];
}

/// <summary>
/// A section within the execution report
/// </summary>
public class ExecutionReportSection
{
    /// <summary>
    /// Section identifier
    /// </summary>
    public string SectionId { get; set; } = string.Empty;

    /// <summary>
    /// Section title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Section description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Section order/priority
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Section content/data
    /// </summary>
    public Dictionary<string, object> Content { get; set; } = [];

    /// <summary>
    /// Subsections
    /// </summary>
    public List<ExecutionReportSection>? Subsections { get; set; }
}

/// <summary>
/// Key Performance Indicator in the report
/// </summary>
public class ExecutionReportKPI
{
    /// <summary>
    /// KPI identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// KPI name/title
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Current value
    /// </summary>
    public object Value { get; set; } = new();

    /// <summary>
    /// Previous value for comparison
    /// </summary>
    public object? PreviousValue { get; set; }

    /// <summary>
    /// Target or goal value
    /// </summary>
    public object? Target { get; set; }

    /// <summary>
    /// Unit of measurement
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// Trend direction (up, down, stable)
    /// </summary>
    public string? Trend { get; set; }

    /// <summary>
    /// Percentage change from previous period
    /// </summary>
    public double? PercentageChange { get; set; }

    /// <summary>
    /// Status (good, warning, critical)
    /// </summary>
    public string Status { get; set; } = "good";

    /// <summary>
    /// Description or explanation
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Chart or visualization data in the report
/// </summary>
public class ExecutionReportChart
{
    /// <summary>
    /// Chart identifier
    /// </summary>
    public string ChartId { get; set; } = string.Empty;

    /// <summary>
    /// Chart title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Chart type (line, bar, pie, etc.)
    /// </summary>
    public string ChartType { get; set; } = string.Empty;

    /// <summary>
    /// Chart data
    /// </summary>
    public Dictionary<string, object> Data { get; set; } = [];

    /// <summary>
    /// Chart configuration options
    /// </summary>
    public Dictionary<string, object>? Options { get; set; }

    /// <summary>
    /// Chart description or explanation
    /// </summary>
    public string? Description { get; set; }
}
