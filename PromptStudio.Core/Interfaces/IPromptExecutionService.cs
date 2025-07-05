using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Enterprise-grade service interface for managing prompt executions with Guid-based architecture
/// Supports multi-tenancy, audit trails, soft deletes, and advanced analytics
/// </summary>
public interface IPromptExecutionService
{
    #region Execution Operations

    /// <summary>
    /// Execute a prompt template with provided variables
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variables">Variables as JSON string</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="executedBy">User executing the prompt</param>
    /// <param name="aiProvider">AI provider name</param>
    /// <param name="model">Model name</param>
    /// <param name="executionContext">Additional execution context</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result with resolved prompt</returns>
    Task<EnhancedExecutionResult> ExecutePromptTemplateAsync(
        Guid templateId, 
        string variables, 
        Guid tenantId, 
        Guid executedBy,
        string? aiProvider = null, 
        string? model = null,
        Dictionary<string, object>? executionContext = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute a prompt template with provided variable dictionary
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="executedBy">User executing the prompt</param>
    /// <param name="aiProvider">AI provider name</param>
    /// <param name="model">Model name</param>
    /// <param name="executionContext">Additional execution context</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result with resolved prompt</returns>
    Task<EnhancedExecutionResult> ExecutePromptTemplateAsync(
        Guid templateId, 
        Dictionary<string, string> variableValues, 
        Guid tenantId, 
        Guid executedBy,
        string? aiProvider = null, 
        string? model = null,
        Dictionary<string, object>? executionContext = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute batch processing with a variable collection
    /// </summary>
    /// <param name="collectionId">Variable collection ID</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="executedBy">User executing the batch</param>
    /// <param name="aiProvider">AI provider name</param>
    /// <param name="model">Model name</param>
    /// <param name="options">Batch execution options</param>
    /// <param name="progress">Progress reporter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Batch execution result</returns>
    Task<EnhancedBatchExecutionResult> ExecuteBatchAsync(
        Guid collectionId, 
        Guid promptId, 
        Guid tenantId, 
        Guid executedBy,
        string? aiProvider = null,
        string? model = null,
        BatchExecutionOptions? options = null,
        IProgress<BatchExecutionProgress>? progress = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Batch executes a prompt template against multiple variable sets with advanced options
    /// </summary>
    /// <param name="templateId">The prompt template ID to execute</param>
    /// <param name="variableSets">List of variable sets to use</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="executedBy">User executing the batch</param>
    /// <param name="aiProvider">AI provider name</param>
    /// <param name="model">Model name</param>
    /// <param name="options">Execution options</param>
    /// <param name="progress">Progress reporter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of execution results</returns>
    Task<List<EnhancedIndividualExecutionResult>> BatchExecuteAsync(
        Guid templateId, 
        List<Dictionary<string, string>> variableSets, 
        Guid tenantId, 
        Guid executedBy,
        string? aiProvider = null, 
        string? model = null,
        BatchExecutionOptions? options = null,
        IProgress<BatchExecutionProgress>? progress = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Execution History & Retrieval

    /// <summary>
    /// Get execution history for prompts with tenant isolation
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <param name="includeDeleted">Whether to include soft-deleted items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of prompt executions</returns>
    Task<List<PromptExecution>> GetExecutionHistoryAsync(
        Guid tenantId,
        Guid? promptId = null, 
        Guid? userId = null,
        int limit = 50,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get execution history with advanced filtering and pagination
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="filter">Advanced filter options</param>
    /// <param name="skip">Number of items to skip</param>
    /// <param name="take">Number of items to take</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of prompt executions</returns>
    Task<EnhancedPagedResult<PromptExecution>> GetExecutionHistoryAdvancedAsync(
        Guid tenantId,
        ExecutionHistoryFilter? filter = null,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get execution by ID with tenant validation
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="includeDeleted">Whether to include soft-deleted items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution details, or null if not found</returns>
    Task<PromptExecution?> GetExecutionByIdAsync(Guid executionId, Guid tenantId, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get total count of executions with filtering
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="filter">Optional filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Total count of executions</returns>
    Task<long> GetTotalExecutionsCountAsync(Guid tenantId, ExecutionCountFilter? filter = null, CancellationToken cancellationToken = default);

    #endregion

    #region Execution Analytics & Insights

    /// <summary>
    /// Get comprehensive execution statistics for a prompt template
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="timeRange">Time range for statistics</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Enhanced execution statistics</returns>
    Task<EnhancedExecutionStatistics> GetExecutionStatisticsAsync(Guid promptId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get execution statistics for a prompt library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="timeRange">Time range for statistics</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Library execution statistics</returns>
    Task<EnhancedLibraryExecutionStatistics> GetLibraryExecutionStatisticsAsync(Guid libraryId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get most frequently executed templates with advanced metrics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="limit">Maximum number of templates to return</param>
    /// <param name="timeRange">Time range for analysis</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of templates with execution metrics</returns>
    Task<List<EnhancedTemplateExecutionSummary>> GetMostExecutedTemplatesAsync(
        Guid tenantId,
        Guid? libraryId = null, 
        int limit = 10, 
        TimeSpan? timeRange = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get performance trends and analytics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="templateId">Optional template ID</param>
    /// <param name="timeRange">Time range for analysis</param>
    /// <param name="granularity">Time granularity (hour, day, week, month)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Performance trend data</returns>
    Task<ExecutionTrendAnalysis> GetExecutionTrendsAsync(
        Guid tenantId,
        Guid? templateId = null,
        TimeSpan? timeRange = null,
        string granularity = "day",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get AI model performance comparison
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="templateId">Optional template ID</param>
    /// <param name="timeRange">Time range for analysis</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Model performance comparison</returns>
    Task<ModelPerformanceComparison> GetModelPerformanceAsync(
        Guid tenantId,
        Guid? templateId = null,
        TimeSpan? timeRange = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Execution Management

    /// <summary>
    /// Save a list of prompt executions to the database with audit trail
    /// </summary>
    /// <param name="executions">List of prompt executions</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of saved prompt executions</returns>
    Task<List<PromptExecution>> SavePromptExecutionsAsync(List<PromptExecution> executions, Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft delete execution by ID
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="deletedBy">User deleting the execution</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, false otherwise</returns>
    Task<bool> SoftDeleteExecutionAsync(Guid executionId, Guid tenantId, Guid deletedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete executions older than specified date with tenant isolation
    /// </summary>
    /// <param name="olderThan">Date threshold</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <param name="hardDelete">Whether to perform hard delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of executions deleted</returns>
    Task<long> DeleteOldExecutionsAsync(
        DateTime olderThan, 
        Guid tenantId,
        Guid? promptId = null,
        bool hardDelete = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Restore soft-deleted execution
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="restoredBy">User restoring the execution</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if restored successfully</returns>
    Task<bool> RestoreExecutionAsync(Guid executionId, Guid tenantId, Guid restoredBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Bulk update execution metadata
    /// </summary>
    /// <param name="updates">List of execution updates</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="updatedBy">User performing updates</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of executions updated</returns>
    Task<int> BulkUpdateExecutionsAsync(List<ExecutionMetadataUpdate> updates, Guid tenantId, Guid updatedBy, CancellationToken cancellationToken = default);

    #endregion

    #region Export & Reporting

    /// <summary>
    /// Export execution data for analysis
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="filter">Export filter</param>
    /// <param name="format">Export format (csv, json, excel)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Exported data</returns>
    Task<byte[]> ExportExecutionDataAsync(
        Guid tenantId,
        ExecutionExportFilter filter,
        string format = "csv",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate execution report
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="reportType">Type of report to generate</param>
    /// <param name="parameters">Report parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Generated report</returns>
    Task<ExecutionReport> GenerateExecutionReportAsync(
        Guid tenantId,
        string reportType,
        Dictionary<string, object>? parameters = null,
        CancellationToken cancellationToken = default);

    #endregion
}

#region Enhanced Result Classes

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
#endregion
