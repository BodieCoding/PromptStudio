using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Execution;
using PromptStudio.Core.DTOs.Statistics;

namespace PromptStudio.Core.Interfaces.Execution;

/// <summary>
/// Service interface for managing prompt executions (MVP focused)
/// Provides core execution operations without enterprise complexity
/// </summary>
public interface IPromptExecutionService
{
    #region Template Execution

    /// <summary>
    /// Execute a prompt template with provided content string
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="templateContent">Template content to execute</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="userId">User executing the prompt</param>
    /// <param name="modelProvider">AI provider name</param>
    /// <param name="modelName">Model name</param>
    /// <param name="variables">Variables for template substitution</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution record</returns>
    Task<PromptExecution> ExecutePromptTemplateAsync(
        Guid templateId,
        string templateContent,
        Guid libraryId,
        Guid userId,
        string? modelProvider = null,
        string? modelName = null,
        Dictionary<string, object>? variables = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute a prompt template with variable substitutions
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variableValues">Dictionary of variable values</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="userId">User executing the prompt</param>
    /// <param name="modelProvider">AI provider name</param>
    /// <param name="modelName">Model name</param>
    /// <param name="executionContext">Additional execution context</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution record</returns>
    Task<PromptExecution> ExecutePromptTemplateAsync(
        Guid templateId,
        Dictionary<string, string> variableValues,
        Guid libraryId,
        Guid userId,
        string? modelProvider = null,
        string? modelName = null,
        Dictionary<string, object>? executionContext = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Batch Execution

    /// <summary>
    /// Execute a batch of prompt executions from a variable collection
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variableCollectionId">Variable collection ID</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="userId">User executing the batch</param>
    /// <param name="modelProvider">AI provider name</param>
    /// <param name="modelName">Model name</param>
    /// <param name="options">Batch execution options</param>
    /// <param name="progress">Progress reporter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Batch execution result</returns>
    Task<BatchExecutionResult> ExecuteBatchAsync(
        Guid templateId,
        Guid variableCollectionId,
        Guid libraryId,
        Guid userId,
        string? modelProvider = null,
        string? modelName = null,
        BatchExecutionOptions? options = null,
        IProgress<BatchExecutionProgress>? progress = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Execute a batch of prompt templates with multiple variable sets
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variableSets">List of variable sets</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="userId">User executing the batch</param>
    /// <param name="modelProvider">AI provider name</param>
    /// <param name="modelName">Model name</param>
    /// <param name="options">Batch execution options</param>
    /// <param name="progress">Progress reporter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Batch execution result</returns>
    Task<BatchExecutionResult> BatchExecuteAsync(
        Guid templateId,
        List<Dictionary<string, string>> variableSets,
        Guid libraryId,
        Guid userId,
        string? modelProvider = null,
        string? modelName = null,
        BatchExecutionOptions? options = null,
        IProgress<BatchExecutionProgress>? progress = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Execution History and Retrieval

    /// <summary>
    /// Get execution history for a template with filtering
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <param name="includeDetails">Whether to include detailed information</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of executions</returns>
    Task<PagedResult<PromptExecution>> GetExecutionHistoryAsync(
        Guid templateId,
        Guid? libraryId = null,
        Guid? userId = null,
        int limit = 100,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get advanced execution history with detailed filtering
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="filter">Execution history filter</param>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of executions</returns>
    Task<PagedResult<PromptExecution>> GetExecutionHistoryAdvancedAsync(
        Guid templateId,
        ExecutionHistoryFilter? filter = null,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a specific execution by ID
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="includeTemplate">Whether to include template information</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution details, or null if not found</returns>
    Task<PromptExecution?> GetExecutionByIdAsync(
        Guid executionId,
        Guid userId,
        bool includeTemplate = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get total execution count with filtering
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="filter">Optional execution count filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Total count of executions</returns>
    Task<int> GetTotalExecutionsCountAsync(
        Guid templateId,
        ExecutionCountFilter? filter = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Statistics and Analytics

    /// <summary>
    /// Get execution statistics for a template
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="timeWindow">Optional time window for statistics</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution statistics</returns>
    Task<ExecutionStatistics> GetExecutionStatisticsAsync(
        Guid templateId,
        Guid libraryId,
        TimeSpan? timeWindow = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get execution statistics for all templates in a library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="timeWindow">Optional time window for statistics</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Library execution statistics</returns>
    Task<LibraryExecutionStatistics> GetLibraryExecutionStatisticsAsync(
        Guid libraryId,
        Guid userId,
        TimeSpan? timeWindow = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get most executed templates
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="limit">Maximum number of templates to return</param>
    /// <param name="timeWindow">Optional time window for analysis</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of template execution rankings</returns>
    Task<List<TemplateExecutionRank>> GetMostExecutedTemplatesAsync(
        Guid libraryId,
        Guid? userId = null,
        int limit = 10,
        TimeSpan? timeWindow = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get execution trends over time
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="templateId">Optional template ID</param>
    /// <param name="timeWindow">Optional time window for analysis</param>
    /// <param name="granularity">Time granularity (hourly, daily, weekly, monthly)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of execution trends</returns>
    Task<List<ExecutionTrend>> GetExecutionTrendsAsync(
        Guid libraryId,
        Guid? templateId = null,
        TimeSpan? timeWindow = null,
        string granularity = "daily",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get model performance analytics
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="templateId">Optional template ID</param>
    /// <param name="timeWindow">Optional time window for analysis</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of model performance metrics</returns>
    Task<List<ModelPerformance>> GetModelPerformanceAsync(
        Guid libraryId,
        Guid? templateId = null,
        TimeSpan? timeWindow = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Data Management

    /// <summary>
    /// Save a single execution record to the database
    /// </summary>
    /// <param name="execution">Execution to save</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Saved execution record</returns>
    Task<PromptExecution> SaveExecutionAsync(
        PromptExecution execution,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete execution by ID (soft delete)
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <param name="userId">User deleting the execution</param>
    /// <param name="libraryId">Library ID for validation</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> DeleteExecutionAsync(
        Guid executionId,
        Guid userId,
        Guid libraryId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Restore a soft-deleted execution
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <param name="userId">User restoring the execution</param>
    /// <param name="libraryId">Library ID for validation</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if restored successfully</returns>
    Task<bool> RestoreExecutionAsync(
        Guid executionId,
        Guid userId,
        Guid libraryId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Bulk update execution metadata
    /// </summary>
    /// <param name="updates">List of execution updates</param>
    /// <param name="userId">User performing updates</param>
    /// <param name="libraryId">Library ID for validation</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of executions updated</returns>
    Task<int> BulkUpdateExecutionsAsync(
        List<ExecutionMetadataUpdate> updates,
        Guid userId,
        Guid libraryId,
        CancellationToken cancellationToken = default);

    #endregion

    #region Export and Reporting

    /// <summary>
    /// Export execution data for analysis
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="filter">Export filter</param>
    /// <param name="format">Export format (csv, json, excel)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Exported data as byte array</returns>
    Task<byte[]> ExportExecutionDataAsync(
        Guid libraryId,
        ExecutionExportFilter filter,
        string format = "csv",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate execution report
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="reportType">Type of report to generate</param>
    /// <param name="timeWindow">Time window for the report</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Generated report</returns>
    Task<ExecutionReport> GenerateExecutionReportAsync(
        Guid libraryId,
        string reportType,
        TimeSpan? timeWindow = null,
        CancellationToken cancellationToken = default);

    #endregion
}
