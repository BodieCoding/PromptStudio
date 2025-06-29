namespace PromptStudio.Core.Domain;

/// <summary>
/// Quality metrics
/// </summary>
public class QualityMetrics
{
    public double AverageScore { get; set; }
    public double MedianScore { get; set; }
    public double StandardDeviation { get; set; }
    public double MinScore { get; set; }
    public double MaxScore { get; set; }
    public Dictionary<string, int> ScoreDistribution { get; set; } = new();
}
/// <summary>
/// Enhanced result of a single prompt execution
/// </summary>
public class EnhancedExecutionResult
{
    /// <summary>
    /// Execution ID
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Name of the executed prompt template
    /// </summary>
    public string PromptName { get; set; } = string.Empty;

    /// <summary>
    /// Resolved prompt content with variables substituted
    /// </summary>
    public string ResolvedPrompt { get; set; } = string.Empty;

    /// <summary>
    /// Variables used in the execution
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = new();

    /// <summary>
    /// Whether the execution was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Error code for categorization
    /// </summary>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Execution timestamp
    /// </summary>
    public DateTime ExecutedAt { get; set; }

    /// <summary>
    /// Execution duration
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// AI provider used
    /// </summary>
    public string? AiProvider { get; set; }

    /// <summary>
    /// Model used
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Token usage information
    /// </summary>
    public TokenUsageInfo? TokenUsage { get; set; }

    /// <summary>
    /// Quality score if available
    /// </summary>
    public double? QualityScore { get; set; }

    /// <summary>
    /// Additional execution metadata
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Enhanced result of an individual execution within a batch
/// </summary>
public class EnhancedIndividualExecutionResult
{
    /// <summary>
    /// Index in the batch
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Variables used for this execution
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = new();

    /// <summary>
    /// Resolved prompt content
    /// </summary>
    public string ResolvedPrompt { get; set; } = string.Empty;

    /// <summary>
    /// Whether the execution was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Error code for categorization
    /// </summary>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Execution ID if saved to database
    /// </summary>
    public Guid? ExecutionId { get; set; }

    /// <summary>
    /// Execution duration
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Token usage for this execution
    /// </summary>
    public TokenUsageInfo? TokenUsage { get; set; }

    /// <summary>
    /// Quality score if available
    /// </summary>
    public double? QualityScore { get; set; }

    /// <summary>
    /// Retry count if applicable
    /// </summary>
    public int RetryCount { get; set; }
}

/// <summary>
/// Enhanced result of a batch execution operation
/// </summary>
public class EnhancedBatchExecutionResult
{
    /// <summary>
    /// Batch execution ID
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// Name of the variable collection used
    /// </summary>
    public string CollectionName { get; set; } = string.Empty;

    /// <summary>
    /// Name of the prompt template executed
    /// </summary>
    public string PromptName { get; set; } = string.Empty;

    /// <summary>
    /// Total number of variable sets processed
    /// </summary>
    public int TotalSets { get; set; }

    /// <summary>
    /// Number of successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Number of failed executions
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Number of skipped executions
    /// </summary>
    public int SkippedExecutions { get; set; }

    /// <summary>
    /// Individual execution results
    /// </summary>
    public List<EnhancedIndividualExecutionResult> Results { get; set; } = new();

    /// <summary>
    /// Overall success rate
    /// </summary>
    public double SuccessRate => TotalSets > 0 ? (double)SuccessfulExecutions / TotalSets : 0;

    /// <summary>
    /// Total execution time
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// <summary>
    /// Average execution time per item
    /// </summary>
    public TimeSpan AverageDuration { get; set; }

    /// <summary>
    /// Total token usage across all executions
    /// </summary>
    public TokenUsageInfo? TotalTokenUsage { get; set; }

    /// <summary>
    /// Quality metrics for the batch
    /// </summary>
    public BatchQualityMetrics? QualityMetrics { get; set; }

    /// <summary>
    /// Performance insights
    /// </summary>
    public Dictionary<string, object> PerformanceMetrics { get; set; } = new();
}

#endregion

#region Supporting Classes

/// <summary>
/// Token usage information
/// </summary>
public class TokenUsageInfo
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
    public decimal? Cost { get; set; }
}

/// <summary>
/// Batch quality metrics
/// </summary>
public class BatchQualityMetrics
{
    public double AverageQualityScore { get; set; }
    public double QualityStandardDeviation { get; set; }
    public double MinQualityScore { get; set; }
    public double MaxQualityScore { get; set; }
    public Dictionary<string, int> QualityDistribution { get; set; } = new();
}

/// <summary>
/// Filter for execution history
/// </summary>
public class ExecutionHistoryFilter
{
    public Guid? PromptId { get; set; }
    public Guid? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? Success { get; set; }
    public string? AiProvider { get; set; }
    public string? Model { get; set; }
    public List<string>? ErrorCodes { get; set; }
    public double? MinQualityScore { get; set; }
    public double? MaxQualityScore { get; set; }
    public string? SearchTerm { get; set; }
}

/// <summary>
/// Enhanced paged result
/// </summary>
public class EnhancedPagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Skip { get; set; }
    public int Take { get; set; }
    public long TotalCount { get; set; }
    public bool HasMore => Skip + Take < TotalCount;
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Filter for execution count queries
/// </summary>
public class ExecutionCountFilter
{
    public Guid? PromptId { get; set; }
    public Guid? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? Success { get; set; }
    public string? AiProvider { get; set; }
    public string? Model { get; set; }
}

/// <summary>
/// Enhanced execution statistics with additional metrics
/// </summary>
public class EnhancedExecutionStatistics
{
    public long TotalExecutions { get; set; }
    public long SuccessfulExecutions { get; set; }
    public long FailedExecutions { get; set; }
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;
    public DateTime? LastExecution { get; set; }
    public DateTime? FirstExecution { get; set; }
    public double AverageExecutionsPerDay { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
    public TimeSpan MedianExecutionTime { get; set; }
    public TimeSpan P95ExecutionTime { get; set; }
    public TokenUsageInfo? TotalTokenUsage { get; set; }
    public Dictionary<string, long> VariableUsageCount { get; set; } = new();
    public Dictionary<string, long> ErrorsByType { get; set; } = new();
    public Dictionary<string, double> ModelPerformance { get; set; } = new();
    public QualityMetrics? QualityMetrics { get; set; }
}

/// <summary>
/// Enhanced library execution statistics
/// </summary>
public class EnhancedLibraryExecutionStatistics
{
    public long TotalExecutions { get; set; }
    public int ExecutedTemplatesCount { get; set; }
    public int TotalTemplatesCount { get; set; }
    public double AverageExecutionsPerTemplate { get; set; }
    public DateTime? LastExecution { get; set; }
    public List<EnhancedTemplateExecutionSummary> TemplateStatistics { get; set; } = new();
    public Dictionary<string, long> ExecutionsByModel { get; set; } = new();
    public Dictionary<string, double> SuccessRatesByModel { get; set; } = new();
    public TokenUsageInfo? TotalTokenUsage { get; set; }
}

/// <summary>
/// Enhanced template execution summary
/// </summary>
public class EnhancedTemplateExecutionSummary
{
    public Guid TemplateId { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public long ExecutionCount { get; set; }
    public DateTime? LastExecution { get; set; }
    public double SuccessRate { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
    public TokenUsageInfo? TokenUsage { get; set; }
    public double? QualityScore { get; set; }
    public Dictionary<string, object> PerformanceMetrics { get; set; } = new();
}

/// <summary>
/// Execution trend analysis
/// </summary>
public class ExecutionTrendAnalysis
{
    public Dictionary<DateTime, ExecutionTrendPoint> TrendData { get; set; } = new();
    public double GrowthRate { get; set; }
    public List<TrendInsight> Insights { get; set; } = new();
    public Dictionary<string, object> Forecasts { get; set; } = new();
}

/// <summary>
/// Point in execution trend
/// </summary>
public class ExecutionTrendPoint
{
    public DateTime Date { get; set; }
    public long ExecutionCount { get; set; }
    public double SuccessRate { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
    public long TokenUsage { get; set; }
    public double QualityScore { get; set; }
}

/// <summary>
/// Trend insight
/// </summary>
public class TrendInsight
{
    public string Type { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public double Confidence { get; set; }
    public Dictionary<string, object> Data { get; set; } = new();
}

/// <summary>
/// Model performance comparison
/// </summary>
public class ModelPerformanceComparison
{
    public Dictionary<string, ModelPerformanceMetrics> ModelMetrics { get; set; } = new();
    public string BestPerformingModel { get; set; } = string.Empty;
    public List<ModelRecommendation> Recommendations { get; set; } = new();
}

/// <summary>
/// Performance metrics for a specific model
/// </summary>
public class ModelPerformanceMetrics
{
    public string ModelName { get; set; } = string.Empty;
    public long ExecutionCount { get; set; }
    public double SuccessRate { get; set; }
    public TimeSpan AverageLatency { get; set; }
    public TokenUsageInfo? TokenUsage { get; set; }
    public double QualityScore { get; set; }
    public decimal? AverageCost { get; set; }
}

/// <summary>
/// Model recommendation
/// </summary>
public class ModelRecommendation
{
    public string ModelName { get; set; } = string.Empty;
    public string RecommendationType { get; set; } = string.Empty;
    public string Reasoning { get; set; } = string.Empty;
    public double Confidence { get; set; }
    public Dictionary<string, object> Metrics { get; set; } = new();
}

/// <summary>
/// Execution metadata update
/// </summary>
public class ExecutionMetadataUpdate
{
    public Guid ExecutionId { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
    public string? Notes { get; set; }
    public List<string>? Tags { get; set; }
}

/// <summary>
/// Filter for execution export
/// </summary>
public class ExecutionExportFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<Guid>? PromptIds { get; set; }
    public List<Guid>? UserIds { get; set; }
    public bool? Success { get; set; }
    public List<string>? AiProviders { get; set; }
    public List<string>? Models { get; set; }
    public bool IncludeMetadata { get; set; } = true;
    public bool IncludeTokenUsage { get; set; } = true;
    public List<string>? Fields { get; set; }
}

/// <summary>
/// Execution report
/// </summary>
public class ExecutionReport
{
    public string ReportId { get; set; } = string.Empty;
    public string ReportType { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; }
    public byte[] Content { get; set; } = Array.Empty<byte>();
    public string ContentType { get; set; } = string.Empty;
    public Dictionary<string, object> Metadata { get; set; } = new();
}