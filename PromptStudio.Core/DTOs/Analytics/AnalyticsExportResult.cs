using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Analytics export result containing exported data and metadata
/// </summary>
public class AnalyticsExportResult
{
    /// <summary>
    /// Unique identifier for the export
    /// </summary>
    public Guid ExportId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Export status
    /// </summary>
    public AnalyticsExportStatus Status { get; set; }

    /// <summary>
    /// Export format used
    /// </summary>
    public AnalyticsExportFormat Format { get; set; }

    /// <summary>
    /// Export creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Export completion timestamp
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// File path or download URL for the exported data
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// Download URL (if applicable)
    /// </summary>
    public string? DownloadUrl { get; set; }

    /// <summary>
    /// File size in bytes
    /// </summary>
    public long? FileSizeBytes { get; set; }

    /// <summary>
    /// Export metadata
    /// </summary>
    public AnalyticsExportMetadata Metadata { get; set; } = new();

    /// <summary>
    /// Export summary statistics
    /// </summary>
    public AnalyticsExportSummary Summary { get; set; } = new();

    /// <summary>
    /// Error message (if status is failed)
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Export expiration timestamp
    /// </summary>
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Security and access information
    /// </summary>
    public AnalyticsExportSecurity Security { get; set; } = new();
}

/// <summary>
/// Analytics export request for initiating data exports
/// </summary>
public class AnalyticsExportRequest
{
    /// <summary>
    /// Export format
    /// </summary>
    public AnalyticsExportFormat Format { get; set; } = AnalyticsExportFormat.Csv;

    /// <summary>
    /// Analytics data type to export
    /// </summary>
    public AnalyticsDataType DataType { get; set; }

    /// <summary>
    /// Time range for the export
    /// </summary>
    public DateTimeRange? TimeRange { get; set; }

    /// <summary>
    /// Tenant IDs to include in export
    /// </summary>
    public List<Guid>? TenantIds { get; set; }

    /// <summary>
    /// Specific metrics to export
    /// </summary>
    public List<string>? Metrics { get; set; }

    /// <summary>
    /// Aggregation level for the exported data
    /// </summary>
    public AnalyticsAggregationLevel? AggregationLevel { get; set; }

    /// <summary>
    /// Filtering options
    /// </summary>
    public Dictionary<string, object>? Filters { get; set; }

    /// <summary>
    /// Sorting options
    /// </summary>
    public List<AnalyticsExportSortOption>? SortOptions { get; set; }

    /// <summary>
    /// Maximum number of records to export
    /// </summary>
    public int? MaxRecords { get; set; }

    /// <summary>
    /// Include headers in export
    /// </summary>
    public bool IncludeHeaders { get; set; } = true;

    /// <summary>
    /// Include metadata in export
    /// </summary>
    public bool IncludeMetadata { get; set; } = false;

    /// <summary>
    /// Compress the export file
    /// </summary>
    public bool CompressFile { get; set; } = false;

    /// <summary>
    /// Export password protection (optional)
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Email notification settings
    /// </summary>
    public AnalyticsExportNotificationSettings? NotificationSettings { get; set; }

    /// <summary>
    /// Export expiration duration (hours)
    /// </summary>
    public int ExpirationHours { get; set; } = 24;

    /// <summary>
    /// Custom export parameters
    /// </summary>
    public Dictionary<string, object>? CustomParameters { get; set; }

    /// <summary>
    /// Export name/description
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Export description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Tags for the export
    /// </summary>
    public List<string>? Tags { get; set; }
}

/// <summary>
/// Analytics report result containing generated report and metadata
/// </summary>
public class AnalyticsReportResult
{
    /// <summary>
    /// Unique identifier for the report
    /// </summary>
    public Guid ReportId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Report name
    /// </summary>
    public string ReportName { get; set; } = string.Empty;

    /// <summary>
    /// Report type
    /// </summary>
    public AnalyticsReportType ReportType { get; set; }

    /// <summary>
    /// Report status
    /// </summary>
    public AnalyticsReportStatus Status { get; set; }

    /// <summary>
    /// Report generation timestamp
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Report completion timestamp
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Time range covered by the report
    /// </summary>
    public AnalyticsTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Report sections
    /// </summary>
    public List<AnalyticsReportSection> Sections { get; set; } = [];

    /// <summary>
    /// Report summary
    /// </summary>
    public AnalyticsReportSummary Summary { get; set; } = new();

    /// <summary>
    /// Report file information
    /// </summary>
    public AnalyticsReportFile? ReportFile { get; set; }

    /// <summary>
    /// Report metadata
    /// </summary>
    public AnalyticsReportMetadata Metadata { get; set; } = new();

    /// <summary>
    /// Error message (if status is failed)
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Report insights and key findings
    /// </summary>
    public List<AnalyticsReportInsight> Insights { get; set; } = [];

    /// <summary>
    /// Report recommendations
    /// </summary>
    public List<AnalyticsReportRecommendation> Recommendations { get; set; } = [];
}

/// <summary>
/// Analytics report request for generating reports
/// </summary>
public class AnalyticsReportRequest
{
    /// <summary>
    /// Report type to generate
    /// </summary>
    public AnalyticsReportType ReportType { get; set; }

    /// <summary>
    /// Report name
    /// </summary>
    public string ReportName { get; set; } = string.Empty;

    /// <summary>
    /// Time range for the report
    /// </summary>
    public DateTimeRange? TimeRange { get; set; }

    /// <summary>
    /// Tenant IDs to include in report
    /// </summary>
    public List<Guid>? TenantIds { get; set; }

    /// <summary>
    /// Report template to use
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Report sections to include
    /// </summary>
    public List<AnalyticsReportSectionType>? Sections { get; set; }

    /// <summary>
    /// Report format options
    /// </summary>
    public AnalyticsReportFormatOptions FormatOptions { get; set; } = new();

    /// <summary>
    /// Include detailed charts and visualizations
    /// </summary>
    public bool IncludeVisualizations { get; set; } = true;

    /// <summary>
    /// Include raw data appendix
    /// </summary>
    public bool IncludeRawData { get; set; } = false;

    /// <summary>
    /// Include executive summary
    /// </summary>
    public bool IncludeExecutiveSummary { get; set; } = true;

    /// <summary>
    /// Include recommendations
    /// </summary>
    public bool IncludeRecommendations { get; set; } = true;

    /// <summary>
    /// Filtering options for report data
    /// </summary>
    public Dictionary<string, object>? Filters { get; set; }

    /// <summary>
    /// Custom report parameters
    /// </summary>
    public Dictionary<string, object>? CustomParameters { get; set; }

    /// <summary>
    /// Report distribution settings
    /// </summary>
    public AnalyticsReportDistributionSettings? DistributionSettings { get; set; }

    /// <summary>
    /// Report scheduling settings (for recurring reports)
    /// </summary>
    public AnalyticsReportScheduleSettings? ScheduleSettings { get; set; }

    /// <summary>
    /// Report description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Report tags
    /// </summary>
    public List<string>? Tags { get; set; }
}

// Enumerations

/// <summary>
/// Analytics export status
/// </summary>
public enum AnalyticsExportStatus
{
    Pending,
    InProgress,
    Completed,
    Failed,
    Expired,
    Cancelled
}

/// <summary>
/// Analytics export format
/// </summary>
public enum AnalyticsExportFormat
{
    Csv,
    Excel,
    Json,
    Pdf,
    Parquet,
    Xml
}

/// <summary>
/// Analytics data type for exports
/// </summary>
public enum AnalyticsDataType
{
    Usage,
    Performance,
    Cost,
    Lab,
    Template,
    Workflow,
    Resource,
    Predictive,
    All
}

/// <summary>
/// Analytics report type
/// </summary>
public enum AnalyticsReportType
{
    Executive,
    Operational,
    Financial,
    Performance,
    Usage,
    Compliance,
    Custom,
    Scheduled,
    AdHoc
}

/// <summary>
/// Analytics report status
/// </summary>
public enum AnalyticsReportStatus
{
    Queued,
    Generating,
    Completed,
    Failed,
    Scheduled
}

/// <summary>
/// Analytics report section type
/// </summary>
public enum AnalyticsReportSectionType
{
    ExecutiveSummary,
    UsageAnalysis,
    PerformanceAnalysis,
    CostAnalysis,
    ResourceAnalysis,
    TrendAnalysis,
    Recommendations,
    RawData,
    Charts,
    Appendix
}

// Supporting classes

/// <summary>
/// Analytics export metadata
/// </summary>
public class AnalyticsExportMetadata
{
    /// <summary>
    /// Data source information
    /// </summary>
    public string DataSource { get; set; } = string.Empty;

    /// <summary>
    /// Export generation method
    /// </summary>
    public string GenerationMethod { get; set; } = string.Empty;

    /// <summary>
    /// Schema version used
    /// </summary>
    public string SchemaVersion { get; set; } = string.Empty;

    /// <summary>
    /// Export parameters used
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = [];

    /// <summary>
    /// Data quality metrics
    /// </summary>
    public Dictionary<string, double> QualityMetrics { get; set; } = [];

    /// <summary>
    /// Export notes
    /// </summary>
    public List<string> Notes { get; set; } = [];
}

/// <summary>
/// Analytics export summary
/// </summary>
public class AnalyticsExportSummary
{
    /// <summary>
    /// Total number of records exported
    /// </summary>
    public long TotalRecords { get; set; }

    /// <summary>
    /// Number of columns exported
    /// </summary>
    public int ColumnCount { get; set; }

    /// <summary>
    /// Time range of exported data
    /// </summary>
    public DateTimeRange? DataTimeRange { get; set; }

    /// <summary>
    /// Export duration (milliseconds)
    /// </summary>
    public long ExportDurationMs { get; set; }

    /// <summary>
    /// Data completeness percentage
    /// </summary>
    public double DataCompleteness { get; set; }

    /// <summary>
    /// Key statistics from exported data
    /// </summary>
    public Dictionary<string, double> KeyStatistics { get; set; } = [];
}

/// <summary>
/// Analytics export security settings
/// </summary>
public class AnalyticsExportSecurity
{
    /// <summary>
    /// Whether the export is password protected
    /// </summary>
    public bool IsPasswordProtected { get; set; }

    /// <summary>
    /// Whether the export is encrypted
    /// </summary>
    public bool IsEncrypted { get; set; }

    /// <summary>
    /// Access permissions
    /// </summary>
    public List<string> AccessPermissions { get; set; } = [];

    /// <summary>
    /// Download restrictions
    /// </summary>
    public AnalyticsExportDownloadRestrictions? DownloadRestrictions { get; set; }

    /// <summary>
    /// Security audit trail
    /// </summary>
    public List<AnalyticsExportAuditEntry> AuditTrail { get; set; } = [];
}

/// <summary>
/// Analytics export sort option
/// </summary>
public class AnalyticsExportSortOption
{
    /// <summary>
    /// Field to sort by
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Sort direction
    /// </summary>
    public SortDirection Direction { get; set; } = SortDirection.Ascending;

    /// <summary>
    /// Sort priority (for multiple sort fields)
    /// </summary>
    public int Priority { get; set; } = 1;
}

/// <summary>
/// Analytics export notification settings
/// </summary>
public class AnalyticsExportNotificationSettings
{
    /// <summary>
    /// Email addresses to notify
    /// </summary>
    public List<string> EmailAddresses { get; set; } = [];

    /// <summary>
    /// Notify on completion
    /// </summary>
    public bool NotifyOnCompletion { get; set; } = true;

    /// <summary>
    /// Notify on failure
    /// </summary>
    public bool NotifyOnFailure { get; set; } = true;

    /// <summary>
    /// Include download link in notification
    /// </summary>
    public bool IncludeDownloadLink { get; set; } = true;

    /// <summary>
    /// Custom notification message
    /// </summary>
    public string? CustomMessage { get; set; }
}

/// <summary>
/// Analytics report section
/// </summary>
public class AnalyticsReportSection
{
    /// <summary>
    /// Section identifier
    /// </summary>
    public string SectionId { get; set; } = string.Empty;

    /// <summary>
    /// Section type
    /// </summary>
    public AnalyticsReportSectionType Type { get; set; }

    /// <summary>
    /// Section title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Section content
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Section data (charts, tables, etc.)
    /// </summary>
    public Dictionary<string, object>? Data { get; set; }

    /// <summary>
    /// Section order
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Section visibility
    /// </summary>
    public bool IsVisible { get; set; } = true;
}

/// <summary>
/// Analytics report summary
/// </summary>
public class AnalyticsReportSummary
{
    /// <summary>
    /// Key performance indicators
    /// </summary>
    public Dictionary<string, double> KeyPerformanceIndicators { get; set; } = [];

    /// <summary>
    /// Summary insights
    /// </summary>
    public List<string> SummaryInsights { get; set; } = [];

    /// <summary>
    /// Critical findings
    /// </summary>
    public List<string> CriticalFindings { get; set; } = [];

    /// <summary>
    /// Trend highlights
    /// </summary>
    public List<string> TrendHighlights { get; set; } = [];

    /// <summary>
    /// Performance scores
    /// </summary>
    public Dictionary<string, double> PerformanceScores { get; set; } = [];
}

/// <summary>
/// Analytics report file information
/// </summary>
public class AnalyticsReportFile
{
    /// <summary>
    /// File name
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// File format
    /// </summary>
    public AnalyticsExportFormat Format { get; set; }

    /// <summary>
    /// File path
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// Download URL
    /// </summary>
    public string? DownloadUrl { get; set; }

    /// <summary>
    /// File size in bytes
    /// </summary>
    public long FileSizeBytes { get; set; }

    /// <summary>
    /// File expiration timestamp
    /// </summary>
    public DateTime? ExpiresAt { get; set; }
}

/// <summary>
/// Analytics report metadata
/// </summary>
public class AnalyticsReportMetadata
{
    /// <summary>
    /// Report generation parameters
    /// </summary>
    public Dictionary<string, object> GenerationParameters { get; set; } = [];

    /// <summary>
    /// Data sources used
    /// </summary>
    public List<string> DataSources { get; set; } = [];

    /// <summary>
    /// Report template information
    /// </summary>
    public AnalyticsReportTemplate? Template { get; set; }

    /// <summary>
    /// Report version
    /// </summary>
    public string Version { get; set; } = "1.0";

    /// <summary>
    /// Report generation duration (milliseconds)
    /// </summary>
    public long GenerationDurationMs { get; set; }

    /// <summary>
    /// Report quality metrics
    /// </summary>
    public Dictionary<string, double> QualityMetrics { get; set; } = [];
}

/// <summary>
/// Analytics report insight
/// </summary>
public class AnalyticsReportInsight
{
    /// <summary>
    /// Insight type
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Insight priority
    /// </summary>
    public string Priority { get; set; } = "medium";

    /// <summary>
    /// Insight title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Insight description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Impact assessment
    /// </summary>
    public string Impact { get; set; } = string.Empty;

    /// <summary>
    /// Supporting data
    /// </summary>
    public Dictionary<string, object>? SupportingData { get; set; }

    /// <summary>
    /// Confidence level (0-100)
    /// </summary>
    public double ConfidenceLevel { get; set; }
}

/// <summary>
/// Analytics report recommendation
/// </summary>
public class AnalyticsReportRecommendation
{
    /// <summary>
    /// Recommendation type
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Recommendation priority
    /// </summary>
    public string Priority { get; set; } = "medium";

    /// <summary>
    /// Recommendation title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Recommendation description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Implementation steps
    /// </summary>
    public List<string> ImplementationSteps { get; set; } = [];

    /// <summary>
    /// Expected impact
    /// </summary>
    public string ExpectedImpact { get; set; } = string.Empty;

    /// <summary>
    /// Implementation effort estimate
    /// </summary>
    public string ImplementationEffort { get; set; } = string.Empty;
}

/// <summary>
/// Analytics report format options
/// </summary>
public class AnalyticsReportFormatOptions
{
    /// <summary>
    /// Report format
    /// </summary>
    public AnalyticsExportFormat Format { get; set; } = AnalyticsExportFormat.Pdf;

    /// <summary>
    /// Page orientation (for PDF reports)
    /// </summary>
    public string PageOrientation { get; set; } = "portrait";

    /// <summary>
    /// Include table of contents
    /// </summary>
    public bool IncludeTableOfContents { get; set; } = true;

    /// <summary>
    /// Include page numbers
    /// </summary>
    public bool IncludePageNumbers { get; set; } = true;

    /// <summary>
    /// Color scheme
    /// </summary>
    public string ColorScheme { get; set; } = "default";

    /// <summary>
    /// Chart style
    /// </summary>
    public string ChartStyle { get; set; } = "modern";

    /// <summary>
    /// Font size
    /// </summary>
    public int FontSize { get; set; } = 12;

    /// <summary>
    /// Custom CSS (for HTML/PDF reports)
    /// </summary>
    public string? CustomCss { get; set; }
}

/// <summary>
/// Analytics report distribution settings
/// </summary>
public class AnalyticsReportDistributionSettings
{
    /// <summary>
    /// Email distribution list
    /// </summary>
    public List<string> EmailAddresses { get; set; } = [];

    /// <summary>
    /// Distribute via file share
    /// </summary>
    public bool DistributeViaFileShare { get; set; } = false;

    /// <summary>
    /// File share path
    /// </summary>
    public string? FileSharePath { get; set; }

    /// <summary>
    /// Include download link
    /// </summary>
    public bool IncludeDownloadLink { get; set; } = true;

    /// <summary>
    /// Distribution message
    /// </summary>
    public string? DistributionMessage { get; set; }
}

/// <summary>
/// Analytics report schedule settings
/// </summary>
public class AnalyticsReportScheduleSettings
{
    /// <summary>
    /// Schedule type
    /// </summary>
    public string ScheduleType { get; set; } = string.Empty;

    /// <summary>
    /// Recurrence pattern
    /// </summary>
    public string RecurrencePattern { get; set; } = string.Empty;

    /// <summary>
    /// Next run time
    /// </summary>
    public DateTime? NextRunTime { get; set; }

    /// <summary>
    /// Schedule enabled
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Schedule parameters
    /// </summary>
    public Dictionary<string, object>? ScheduleParameters { get; set; }
}

/// <summary>
/// Analytics report template
/// </summary>
public class AnalyticsReportTemplate
{
    /// <summary>
    /// Template ID
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Template name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Template version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Template description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Analytics export download restrictions
/// </summary>
public class AnalyticsExportDownloadRestrictions
{
    /// <summary>
    /// Maximum number of downloads allowed
    /// </summary>
    public int? MaxDownloads { get; set; }

    /// <summary>
    /// IP address restrictions
    /// </summary>
    public List<string>? AllowedIpAddresses { get; set; }

    /// <summary>
    /// User restrictions
    /// </summary>
    public List<Guid>? AllowedUserIds { get; set; }

    /// <summary>
    /// Time-based access restrictions
    /// </summary>
    public DateTimeRange? AccessTimeWindow { get; set; }
}

/// <summary>
/// Analytics export audit entry
/// </summary>
public class AnalyticsExportAuditEntry
{
    /// <summary>
    /// Audit timestamp
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Action performed
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// User who performed the action
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// User IP address
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// Additional audit details
    /// </summary>
    public Dictionary<string, object>? Details { get; set; }
}
