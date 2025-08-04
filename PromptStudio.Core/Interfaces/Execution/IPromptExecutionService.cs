using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Execution;

namespace PromptStudio.Core.Interfaces.Execution;

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
    Task<PromptExecutionResult> ExecutePromptTemplateAsync(
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
    Task<PromptExecutionResult> ExecutePromptTemplateAsync(
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
    Task<PromptBatchExecutionResult> ExecuteBatchAsync(
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
    Task<List<PromptExecutionBatchItem>> BatchExecuteAsync(
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
    Task<PagedResult<PromptExecution>> GetExecutionHistoryAdvancedAsync(
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
    /// <returns>Prompt execution statistics</returns>
    Task<PromptExecutionStatistics> GetExecutionStatisticsAsync(Guid promptId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get execution statistics for a prompt library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="timeRange">Time range for statistics</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Library execution statistics</returns>
    Task<PromptStudio.Core.DTOs.Statistics.LibraryExecutionStatistics> GetLibraryExecutionStatisticsAsync(Guid libraryId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get most frequently executed templates with advanced metrics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="limit">Maximum number of templates to return</param>
    /// <param name="timeRange">Time range for analysis</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of templates with execution metrics</returns>
    Task<List<PromptTemplateExecutionSummary>> GetMostExecutedTemplatesAsync(
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
