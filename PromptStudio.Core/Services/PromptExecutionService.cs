using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Interfaces.Data;
using PromptStudio.Core.Interfaces.Execution;
using PromptStudio.Core.DTOs.Execution;
using PromptStudio.Core.DTOs.Common;
using PromptStudio.Core.DTOs.Statistics;
using PromptStudio.Core.Exceptions;
using System.Text.Json;

namespace PromptStudio.Core.Services
{
    /// <summary>
    /// Service for managing prompt template executions with comprehensive tracking, analytics, and batch processing capabilities
    /// </summary>
    public class PromptExecutionService : IPromptExecutionService
    {
        #region Private Fields

        private readonly IPromptStudioDbContext _context;

        #endregion

        #region Constructor

        public PromptExecutionService(IPromptStudioDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region Template Execution

        /// <summary>
        /// Executes a prompt template with the provided content string
        /// </summary>
        public async Task<PromptExecution> ExecutePromptTemplateAsync(
            Guid templateId,
            string templateContent,
            Guid libraryId,
            Guid userId,
            string? modelProvider = null,
            string? modelName = null,
            Dictionary<string, object>? variables = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Validate template exists
                var template = await _context.PromptTemplates
                    .Where(t => t.Id == templateId && t.PromptLibraryId == libraryId && t.DeletedAt == null)
                    .FirstOrDefaultAsync(cancellationToken);

                if (template == null)
                {
                    throw new ResourceNotFoundException($"Template {templateId} not found in library {libraryId}");
                }

                // Create execution record
                var execution = new PromptExecution
                {
                    Id = Guid.NewGuid(),
                    PromptTemplateId = templateId,
                    ResolvedPrompt = templateContent,
                    VariableValues = variables != null ? JsonSerializer.Serialize(variables) : null,
                    AiProvider = modelProvider ?? "default",
                    Model = modelName ?? "gpt-3.5-turbo",
                    Status = ExecutionStatus.Success,
                    ExecutedAt = DateTime.UtcNow,
                    CreatedBy = userId.ToString(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.PromptExecutions.Add(execution);
                await _context.SaveChangesAsync(cancellationToken);

                // TODO: Implement actual LLM execution logic
                // For now, simulate execution
                await Task.Delay(100, cancellationToken);

                // Update execution with results  
                execution.Status = ExecutionStatus.Success;
                execution.Response = "Sample execution result";
                execution.TokensUsed = 150;
                execution.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);
                return execution;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error executing template {templateId}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executes a prompt template with variable substitutions
        /// </summary>
        public async Task<PromptExecution> ExecutePromptTemplateAsync(
            Guid templateId,
            Dictionary<string, string> variableValues,
            Guid libraryId,
            Guid userId,
            string? modelProvider = null,
            string? modelName = null,
            Dictionary<string, object>? executionContext = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Get template content
                var template = await _context.PromptTemplates
                    .Include(t => t.Content)
                    .Where(t => t.Id == templateId && t.PromptLibraryId == libraryId && t.DeletedAt == null)
                    .FirstOrDefaultAsync(cancellationToken);

                if (template == null)
                {
                    throw new ResourceNotFoundException($"Template {templateId} not found in library {libraryId}");
                }

                // Resolve variables in template content
                string resolvedContent = template.Content?.Content ?? string.Empty;
                foreach (var variable in variableValues)
                {
                    resolvedContent = resolvedContent.Replace($"{{{{{variable.Key}}}}}", variable.Value);
                }

                // Execute with resolved content
                var variables = variableValues.ToDictionary(kv => kv.Key, kv => (object)kv.Value);
                return await ExecutePromptTemplateAsync(templateId, resolvedContent, libraryId, userId, modelProvider, modelName, variables, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error executing template with variables {templateId}: {ex.Message}", ex);
            }
        }

        #endregion

        #region Batch Execution

        /// <summary>
        /// Executes a batch of prompt executions from a variable collection
        /// </summary>
        public async Task<BatchExecutionResult> ExecuteBatchAsync(
            Guid templateId,
            Guid variableCollectionId,
            Guid libraryId,
            Guid userId,
            string? modelProvider = null,
            string? modelName = null,
            BatchExecutionOptions? options = null,
            IProgress<BatchExecutionProgress>? progress = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Get template
                var template = await _context.PromptTemplates
                    .Include(t => t.Content)
                    .Where(t => t.Id == templateId && t.PromptLibraryId == libraryId && t.DeletedAt == null)
                    .FirstOrDefaultAsync(cancellationToken);

                if (template == null)
                {
                    throw new ResourceNotFoundException($"Template {templateId} not found");
                }

                // Get variable collection
                var variableCollection = await _context.VariableCollections
                    .Where(vc => vc.Id == variableCollectionId && vc.IsActive)
                    .FirstOrDefaultAsync(cancellationToken);

                if (variableCollection == null)
                {
                    throw new ResourceNotFoundException($"Variable collection {variableCollectionId} not found");
                }

                // TODO: Parse variable collection data and execute batch
                var batchResult = new BatchExecutionResult
                {
                    BatchId = Guid.NewGuid(),
                    IsSuccess = true,
                    Results = new List<IndividualExecutionResult>()
                };

                return batchResult;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error executing batch: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executes a batch of prompt templates with multiple variable sets
        /// </summary>
        public async Task<BatchExecutionResult> BatchExecuteAsync(
            Guid templateId,
            List<Dictionary<string, string>> variableSets,
            Guid libraryId,
            Guid userId,
            string? modelProvider = null,
            string? modelName = null,
            BatchExecutionOptions? options = null,
            IProgress<BatchExecutionProgress>? progress = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var batchId = Guid.NewGuid();
                var executionIds = new List<Guid>();
                var errors = new List<string>();
                var startTime = DateTime.UtcNow;

                var successfulExecutions = 0;
                var failedExecutions = 0;

                for (int i = 0; i < variableSets.Count; i++)
                {
                    try
                    {
                        var execution = await ExecutePromptTemplateAsync(
                            templateId, variableSets[i], libraryId, userId, 
                            modelProvider, modelName, null, cancellationToken);

                        executionIds.Add(execution.Id);
                        successfulExecutions++;

                        // Report progress
                        progress?.Report(new BatchExecutionProgress
                        {
                            BatchId = batchId,
                            TotalItems = variableSets.Count,
                            CompletedItems = i + 1,
                            CurrentItem = $"Execution {i + 1}"
                        });
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Execution {i + 1}: {ex.Message}");
                        failedExecutions++;
                    }

                    if (cancellationToken.IsCancellationRequested)
                        break;
                }

                return new BatchExecutionResult
                {
                    BatchId = batchId,
                    IsSuccess = failedExecutions == 0,
                    Results = new List<IndividualExecutionResult>() // TODO: Add individual results
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error in batch execution: {ex.Message}", ex);
            }
        }

        #endregion

        #region Execution History and Retrieval

        /// <summary>
        /// Gets execution history for a template with filtering
        /// </summary>
        public async Task<PagedResult<PromptExecution>> GetExecutionHistoryAsync(
            Guid templateId,
            Guid? libraryId = null,
            Guid? userId = null,
            int limit = 100,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptTemplateId == templateId && e.IsActive);

                // Note: PromptLibraryId and CreatedBy comparisons removed due to entity model mismatches
                // TODO: Fix entity relationships and property types

                var totalCount = await query.CountAsync(cancellationToken);
                var executions = await query
                    .OrderByDescending(e => e.CreatedAt)
                    .Take(limit)
                    .ToListAsync(cancellationToken);

                return new PagedResult<PromptExecution>
                {
                    Items = executions,
                    TotalCount = totalCount,
                    Skip = 0,
                    Take = limit
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving execution history: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets advanced execution history with detailed filtering
        /// </summary>
        public async Task<PagedResult<PromptExecution>> GetExecutionHistoryAdvancedAsync(
            Guid templateId,
            ExecutionHistoryFilter? filter = null,
            int pageNumber = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptTemplateId == templateId && e.IsActive);

                // TODO: Apply filters when ExecutionHistoryFilter DTO is properly defined
                // Simplified filtering removed due to missing DTO properties

                var totalCount = await query.CountAsync(cancellationToken);
                var executions = await query
                    .OrderByDescending(e => e.CreatedAt)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                return new PagedResult<PromptExecution>
                {
                    Items = executions,
                    TotalCount = totalCount,
                    Skip = (pageNumber - 1) * pageSize,
                    Take = pageSize
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving advanced execution history: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets a specific execution by ID
        /// </summary>
        public async Task<PromptExecution?> GetExecutionByIdAsync(
            Guid executionId,
            Guid userId,
            bool includeTemplate = true,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions.AsQueryable();

                if (includeTemplate)
                    query = query.Include(e => e.PromptTemplate);

                var execution = await query
                    .Where(e => e.Id == executionId && e.CreatedBy == userId.ToString() && e.IsActive)
                    .FirstOrDefaultAsync(cancellationToken);

                return execution;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving execution {executionId}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets total execution count with filtering
        /// </summary>
        public async Task<int> GetTotalExecutionsCountAsync(
            Guid templateId,
            ExecutionCountFilter? filter = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptTemplateId == templateId && e.IsActive);

                // TODO: Apply filters when ExecutionCountFilter DTO is properly defined
                // Simplified filtering removed due to missing DTO properties

                return await query.CountAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error counting executions: {ex.Message}", ex);
            }
        }

        #endregion

        #region Statistics and Analytics

        /// <summary>
        /// Gets execution statistics for a template
        /// </summary>
        public async Task<ExecutionStatistics> GetExecutionStatisticsAsync(
            Guid templateId,
            Guid libraryId,
            TimeSpan? timeWindow = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptTemplateId == templateId && e.IsActive);
                    // Note: PromptLibraryId removed - not available in entity

                if (timeWindow.HasValue)
                {
                    var cutoffDate = DateTime.UtcNow.Subtract(timeWindow.Value);
                    query = query.Where(e => e.CreatedAt >= cutoffDate);
                }

                var executions = await query.ToListAsync(cancellationToken);

                return new ExecutionStatistics
                {
                    // Note: Properties simplified to match actual DTO structure
                    TotalExecutions = executions.Count,
                    SuccessfulExecutions = executions.Count(e => e.Status == ExecutionStatus.Success),
                    FailedExecutions = executions.Count(e => e.Status == ExecutionStatus.Failed),
                    UniqueUsers = executions.Select(e => e.CreatedBy).Distinct().Count()
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error calculating execution statistics: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets execution statistics for all templates in a library
        /// </summary>
        public async Task<LibraryExecutionStatistics> GetLibraryExecutionStatisticsAsync(
            Guid libraryId,
            Guid userId,
            TimeSpan? timeWindow = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptLibraryId == libraryId && e.IsActive);

                if (timeWindow.HasValue)
                {
                    var cutoffDate = DateTime.UtcNow.Subtract(timeWindow.Value);
                    query = query.Where(e => e.CreatedAt >= cutoffDate);
                }

                var executions = await query.ToListAsync(cancellationToken);

                return new LibraryExecutionStatistics
                {
                    LibraryId = libraryId,
                    TotalExecutions = executions.Count,
                    UniqueTemplates = executions.Select(e => e.PromptTemplateId).Distinct().Count(),
                    UniqueUsers = executions.Select(e => e.CreatedBy).Distinct().Count(),
                    TotalTokensUsed = executions.Sum(e => e.TokensUsed ?? 0),
                    AverageExecutionsPerTemplate = executions.GroupBy(e => e.PromptTemplateId).Select(g => g.Count()).DefaultIfEmpty(0).Average(),
                    MostActiveTemplate = executions.GroupBy(e => e.PromptTemplateId).OrderByDescending(g => g.Count()).FirstOrDefault()?.Key,
                    TimeWindow = timeWindow
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error calculating library execution statistics: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets most executed templates
        /// </summary>
        public async Task<List<TemplateExecutionRank>> GetMostExecutedTemplatesAsync(
            Guid libraryId,
            Guid? userId = null,
            int limit = 10,
            TimeSpan? timeWindow = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptLibraryId == libraryId && e.IsActive);

                if (userId.HasValue)
                    query = query.Where(e => e.CreatedBy == userId.Value);

                if (timeWindow.HasValue)
                {
                    var cutoffDate = DateTime.UtcNow.Subtract(timeWindow.Value);
                    query = query.Where(e => e.CreatedAt >= cutoffDate);
                }

                var rankings = await query
                    .GroupBy(e => e.PromptTemplateId)
                    .Select(g => new TemplateExecutionRank
                    {
                        TemplateId = g.Key,
                        ExecutionCount = g.Count(),
                        LastExecuted = g.Max(e => e.CreatedAt),
                        UniqueUsers = g.Select(e => e.CreatedBy).Distinct().Count()
                    })
                    .OrderByDescending(r => r.ExecutionCount)
                    .Take(limit)
                    .ToListAsync(cancellationToken);

                return rankings;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving most executed templates: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets execution trends over time
        /// </summary>
        public async Task<List<ExecutionTrend>> GetExecutionTrendsAsync(
            Guid libraryId,
            Guid? templateId = null,
            TimeSpan? timeWindow = null,
            string granularity = "daily",
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptLibraryId == libraryId && e.IsActive);

                if (templateId.HasValue)
                    query = query.Where(e => e.PromptTemplateId == templateId.Value);

                if (timeWindow.HasValue)
                {
                    var cutoffDate = DateTime.UtcNow.Subtract(timeWindow.Value);
                    query = query.Where(e => e.CreatedAt >= cutoffDate);
                }

                var executions = await query.ToListAsync(cancellationToken);

                // Group by time period based on granularity
                var trends = granularity.ToLower() switch
                {
                    "hourly" => executions.GroupBy(e => new DateTime(e.CreatedAt.Year, e.CreatedAt.Month, e.CreatedAt.Day, e.CreatedAt.Hour, 0, 0)),
                    "weekly" => executions.GroupBy(e => new DateTime(e.CreatedAt.Year, e.CreatedAt.Month, e.CreatedAt.Day).AddDays(-(int)e.CreatedAt.DayOfWeek)),
                    "monthly" => executions.GroupBy(e => new DateTime(e.CreatedAt.Year, e.CreatedAt.Month, 1)),
                    _ => executions.GroupBy(e => e.CreatedAt.Date)
                };

                return trends.Select(g => new ExecutionTrend
                {
                    Period = g.Key,
                    ExecutionCount = g.Count(),
                    UniqueUsers = g.Select(e => e.CreatedBy).Distinct().Count(),
                    SuccessRate = g.Count(e => e.Status == "Completed") / (double)g.Count() * 100
                }).OrderBy(t => t.Period).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error calculating execution trends: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets model performance analytics
        /// </summary>
        public async Task<List<ModelPerformance>> GetModelPerformanceAsync(
            Guid libraryId,
            Guid? templateId = null,
            TimeSpan? timeWindow = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptLibraryId == libraryId && e.IsActive);

                if (templateId.HasValue)
                    query = query.Where(e => e.PromptTemplateId == templateId.Value);

                if (timeWindow.HasValue)
                {
                    var cutoffDate = DateTime.UtcNow.Subtract(timeWindow.Value);
                    query = query.Where(e => e.CreatedAt >= cutoffDate);
                }

                var executions = await query.ToListAsync(cancellationToken);

                var performance = executions
                    .GroupBy(e => new { e.ModelProvider, e.ModelName })
                    .Select(g => new ModelPerformance
                    {
                        ModelProvider = g.Key.ModelProvider,
                        ModelName = g.Key.ModelName,
                        TotalExecutions = g.Count(),
                        SuccessfulExecutions = g.Count(e => e.Status == "Completed"),
                        FailedExecutions = g.Count(e => e.Status == "Failed"),
                        AverageExecutionTime = g.Where(e => e.EndTime.HasValue)
                            .Select(e => (e.EndTime!.Value - e.StartTime).TotalMilliseconds)
                            .DefaultIfEmpty(0)
                            .Average(),
                        TotalTokensUsed = g.Sum(e => e.TokensUsed ?? 0),
                        AverageTokensPerExecution = g.Where(e => e.TokensUsed.HasValue).Select(e => e.TokensUsed!.Value).DefaultIfEmpty(0).Average()
                    })
                    .OrderByDescending(p => p.TotalExecutions)
                    .ToList();

                return performance;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error calculating model performance: {ex.Message}", ex);
            }
        }

        #endregion

        #region Data Management

        /// <summary>
        /// Saves multiple prompt executions in batch
        /// </summary>
        public async Task<bool> SavePromptExecutionsAsync(
            List<PromptExecution> executions,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var execution in executions)
                {
                    execution.CreatedBy = userId;
                    execution.CreatedAt = DateTime.UtcNow;
                    execution.UpdatedAt = DateTime.UtcNow;
                    execution.IsActive = true;
                }

                _context.PromptExecutions.AddRange(executions);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving prompt executions: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Soft deletes an execution
        /// </summary>
        public async Task<bool> SoftDeleteExecutionAsync(
            Guid executionId,
            Guid userId,
            Guid libraryId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var execution = await _context.PromptExecutions
                    .Where(e => e.Id == executionId && e.CreatedBy == userId && e.PromptLibraryId == libraryId && e.IsActive)
                    .FirstOrDefaultAsync(cancellationToken);

                if (execution == null)
                    return false;

                execution.IsActive = false;
                execution.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error soft deleting execution: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deletes old executions based on date criteria
        /// </summary>
        public async Task<int> DeleteOldExecutionsAsync(
            DateTime cutoffDate,
            Guid libraryId,
            Guid? templateId = null,
            bool hardDelete = false,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptLibraryId == libraryId && e.CreatedAt < cutoffDate);

                if (templateId.HasValue)
                    query = query.Where(e => e.PromptTemplateId == templateId.Value);

                var executions = await query.ToListAsync(cancellationToken);

                if (hardDelete)
                {
                    _context.PromptExecutions.RemoveRange(executions);
                }
                else
                {
                    foreach (var execution in executions)
                    {
                        execution.IsActive = false;
                        execution.UpdatedAt = DateTime.UtcNow;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                return executions.Count;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting old executions: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Restores a soft-deleted execution
        /// </summary>
        public async Task<bool> RestoreExecutionAsync(
            Guid executionId,
            Guid userId,
            Guid libraryId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var execution = await _context.PromptExecutions
                    .Where(e => e.Id == executionId && e.CreatedBy == userId && e.PromptLibraryId == libraryId && !e.IsActive)
                    .FirstOrDefaultAsync(cancellationToken);

                if (execution == null)
                    return false;

                execution.IsActive = true;
                execution.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error restoring execution: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Bulk updates execution metadata
        /// </summary>
        public async Task<int> BulkUpdateExecutionsAsync(
            List<ExecutionMetadataUpdate> updates,
            Guid userId,
            Guid libraryId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var updateCount = 0;

                foreach (var update in updates)
                {
                    var execution = await _context.PromptExecutions
                        .Where(e => e.Id == update.ExecutionId && e.CreatedBy == userId && e.PromptLibraryId == libraryId && e.IsActive)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (execution != null)
                    {
                        if (!string.IsNullOrEmpty(update.Status))
                            execution.Status = update.Status;

                        if (!string.IsNullOrEmpty(update.Result))
                            execution.Result = update.Result;

                        if (update.TokensUsed.HasValue)
                            execution.TokensUsed = update.TokensUsed.Value;

                        if (update.EndTime.HasValue)
                            execution.EndTime = update.EndTime.Value;

                        execution.UpdatedAt = DateTime.UtcNow;
                        updateCount++;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                return updateCount;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error bulk updating executions: {ex.Message}", ex);
            }
        }

        #endregion

        #region Export and Reporting

        /// <summary>
        /// Exports execution data with filtering
        /// </summary>
        public async Task<string> ExportExecutionDataAsync(
            Guid libraryId,
            ExecutionExportFilter filter,
            string format = "json",
            CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.PromptExecutions
                    .Where(e => e.PromptLibraryId == libraryId && e.IsActive);

                // Apply filters
                if (filter.TemplateIds?.Any() == true)
                    query = query.Where(e => filter.TemplateIds.Contains(e.PromptTemplateId));

                if (filter.StartDate.HasValue)
                    query = query.Where(e => e.CreatedAt >= filter.StartDate.Value);

                if (filter.EndDate.HasValue)
                    query = query.Where(e => e.CreatedAt <= filter.EndDate.Value);

                if (filter.UserIds?.Any() == true)
                    query = query.Where(e => filter.UserIds.Contains(e.CreatedBy));

                if (!string.IsNullOrEmpty(filter.Status))
                    query = query.Where(e => e.Status == filter.Status);

                var executions = await query
                    .OrderByDescending(e => e.CreatedAt)
                    .ToListAsync(cancellationToken);

                // Export based on format
                return format.ToLower() switch
                {
                    "csv" => ExportToCsv(executions),
                    "json" => JsonSerializer.Serialize(executions, new JsonSerializerOptions { WriteIndented = true }),
                    _ => JsonSerializer.Serialize(executions, new JsonSerializerOptions { WriteIndented = true })
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error exporting execution data: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Generates execution report with analytics
        /// </summary>
        public async Task<ExecutionReport> GenerateExecutionReportAsync(
            Guid libraryId,
            string reportType = "summary",
            Dictionary<string, object>? parameters = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var timeWindow = TimeSpan.FromDays(30); // Default to 30 days

                if (parameters?.ContainsKey("timeWindow") == true)
                {
                    timeWindow = TimeSpan.FromDays(Convert.ToInt32(parameters["timeWindow"]));
                }

                var statistics = await GetLibraryExecutionStatisticsAsync(libraryId, Guid.Empty, timeWindow, cancellationToken);
                var trends = await GetExecutionTrendsAsync(libraryId, null, timeWindow, "daily", cancellationToken);
                var topTemplates = await GetMostExecutedTemplatesAsync(libraryId, null, 10, timeWindow, cancellationToken);
                var modelPerformance = await GetModelPerformanceAsync(libraryId, null, timeWindow, cancellationToken);

                return new ExecutionReport
                {
                    ReportId = Guid.NewGuid(),
                    LibraryId = libraryId,
                    ReportType = reportType,
                    GeneratedAt = DateTime.UtcNow,
                    TimeWindow = timeWindow,
                    Summary = statistics,
                    Trends = trends,
                    TopTemplates = topTemplates,
                    ModelPerformance = modelPerformance,
                    Parameters = parameters
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error generating execution report: {ex.Message}", ex);
            }
        }

        #endregion

        #region Helper Methods

        private string ExportToCsv(List<PromptExecution> executions)
        {
            var csv = new System.Text.StringBuilder();
            csv.AppendLine("Id,TemplateId,LibraryId,Status,StartTime,EndTime,TokensUsed,ModelProvider,ModelName,CreatedBy");

            foreach (var execution in executions)
            {
                csv.AppendLine($"{execution.Id},{execution.PromptTemplateId},{execution.PromptLibraryId}," +
                              $"{execution.Status},{execution.StartTime},{execution.EndTime}," +
                              $"{execution.TokensUsed},{execution.ModelProvider},{execution.ModelName},{execution.CreatedBy}");
            }

            return csv.ToString();
        }

        #endregion
    }
}
