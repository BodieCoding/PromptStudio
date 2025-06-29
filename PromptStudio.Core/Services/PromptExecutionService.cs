using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace PromptStudio.Core.Services;

/// <summary>
/// Service for managing prompt executions
/// </summary>
public class PromptExecutionService : IPromptExecutionService
{
    #region Private Fields

    private readonly IPromptStudioDbContext _context;
    private readonly IPromptTemplateService _promptTemplateService;
    private readonly IVariableService _variableService;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the PromptExecutionService
    /// </summary>
    /// <param name="context">Database context for data access</param>
    /// <param name="promptTemplateService">Prompt template service</param>
    /// <param name="variableService">Variable service</param>
    public PromptExecutionService(
        IPromptStudioDbContext context, 
        IPromptTemplateService promptTemplateService,
        IVariableService variableService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _promptTemplateService = promptTemplateService ?? throw new ArgumentNullException(nameof(promptTemplateService));
        _variableService = variableService ?? throw new ArgumentNullException(nameof(variableService));
    }

    #endregion

    #region Execution Operations

    /// <summary>
    /// Execute a prompt template with provided variables
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variables">Variables as JSON string</param>
    /// <param name="aiProvider">AI provider name</param>
    /// <param name="model">Model name</param>
    /// <returns>Execution result with resolved prompt</returns>
    public async Task<ExecutionResult> ExecutePromptTemplateAsync(int templateId, string variables, string? aiProvider = null, string? model = null)
    {
        try
        {
            var variableValues = JsonSerializer.Deserialize<Dictionary<string, string>>(variables) 
                ?? new Dictionary<string, string>();

            return await ExecutePromptTemplateAsync(templateId, variableValues, aiProvider, model);
        }
        catch (JsonException ex)
        {
            return new ExecutionResult
            {
                Success = false,
                Error = $"Invalid JSON format for variables: {ex.Message}",
                ExecutedAt = DateTime.UtcNow,
                AiProvider = aiProvider,
                Model = model
            };
        }
    }

    /// <summary>
    /// Execute a prompt template with provided variable dictionary
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <param name="aiProvider">AI provider name</param>
    /// <param name="model">Model name</param>
    /// <returns>Execution result with resolved prompt</returns>
    public async Task<ExecutionResult> ExecutePromptTemplateAsync(int templateId, Dictionary<string, string> variableValues, string? aiProvider = null, string? model = null)
    {
        try
        {
            var template = await _promptTemplateService.GetPromptTemplateByIdAsync(templateId);
            if (template == null)
            {
                return new ExecutionResult
                {
                    Success = false,
                    Error = $"Prompt template with ID {templateId} not found",
                    ExecutedAt = DateTime.UtcNow,
                    Variables = variableValues,
                    AiProvider = aiProvider,
                    Model = model
                };
            }

            if (!_promptTemplateService.ValidateVariables(template, variableValues))
            {
                return new ExecutionResult
                {
                    Success = false,
                    Error = "Missing required variables",
                    ExecutedAt = DateTime.UtcNow,
                    PromptName = template.Name,
                    Variables = variableValues,
                    AiProvider = aiProvider,
                    Model = model
                };
            }

            var resolvedPrompt = _promptTemplateService.ResolvePrompt(template, variableValues);

            // Save execution to database
            var execution = new PromptExecution
            {
                PromptTemplateId = templateId,
                ResolvedPrompt = resolvedPrompt,
                VariableValues = JsonSerializer.Serialize(variableValues),
                ExecutedAt = DateTime.UtcNow,
                AiProvider = aiProvider ?? "MCP",
                Model = model ?? "N/A"
            };

            _context.PromptExecutions.Add(execution);
            await _context.SaveChangesAsync();

            return new ExecutionResult
            {
                ExecutionId = execution.Id,
                PromptName = template.Name,
                ResolvedPrompt = resolvedPrompt,
                Variables = variableValues,
                Success = true,
                ExecutedAt = execution.ExecutedAt,
                AiProvider = execution.AiProvider,
                Model = execution.Model
            };
        }
        catch (Exception ex)
        {
            return new ExecutionResult
            {
                Success = false,
                Error = ex.Message,
                ExecutedAt = DateTime.UtcNow,
                Variables = variableValues,
                AiProvider = aiProvider,
                Model = model
            };
        }
    }

    /// <summary>
    /// Execute batch processing with a variable collection
    /// </summary>
    /// <param name="collectionId">Variable collection ID</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>Batch execution result</returns>
    public async Task<BatchExecutionResult> ExecuteBatchAsync(int collectionId, int promptId)
    {
        var collection = await _context.VariableCollections
            .FirstOrDefaultAsync(vc => vc.Id == collectionId);

        if (collection == null)
        {
            throw new ArgumentException($"Variable collection with ID {collectionId} not found", nameof(collectionId));
        }

        var template = await _promptTemplateService.GetPromptTemplateByIdAsync(promptId);
        if (template == null)
        {
            throw new ArgumentException($"Prompt template with ID {promptId} not found", nameof(promptId));
        }

        // Parse variable sets from JSON
        var variableSets = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(collection.VariableSets)
            ?? new List<Dictionary<string, string>>();

        // Execute batch
        var results = await BatchExecuteAsync(promptId, variableSets);

        return new BatchExecutionResult
        {
            CollectionName = collection.Name,
            PromptName = template.Name,
            TotalSets = variableSets.Count,
            SuccessfulExecutions = results.Count(r => r.Success),
            FailedExecutions = results.Count(r => !r.Success),
            Results = results
        };
    }

    /// <summary>
    /// Batch executes a prompt template against multiple variable sets
    /// </summary>
    /// <param name="templateId">The prompt template ID to execute</param>
    /// <param name="variableSets">List of variable sets to use</param>
    /// <param name="aiProvider">AI provider name</param>
    /// <param name="model">Model name</param>
    /// <returns>List of execution results with variables, resolved prompts, and any errors</returns>
    public async Task<List<IndividualExecutionResult>> BatchExecuteAsync(int templateId, List<Dictionary<string, string>> variableSets, string? aiProvider = null, string? model = null)
    {
        var template = await _promptTemplateService.GetPromptTemplateByIdAsync(templateId);
        if (template == null)
        {
            throw new ArgumentException($"Prompt template with ID {templateId} not found", nameof(templateId));
        }

        var results = new List<IndividualExecutionResult>();
        var executions = new List<PromptExecution>();

        foreach (var variableSet in variableSets)
        {
            try
            {
                if (!_promptTemplateService.ValidateVariables(template, variableSet))
                {
                    results.Add(new IndividualExecutionResult
                    {
                        Variables = variableSet,
                        ResolvedPrompt = string.Empty,
                        Success = false,
                        Error = "Missing required variables"
                    });
                    continue;
                }

                var resolvedPrompt = _promptTemplateService.ResolvePrompt(template, variableSet);
                
                // Create execution record
                var execution = new PromptExecution
                {
                    PromptTemplateId = templateId,
                    ResolvedPrompt = resolvedPrompt,
                    VariableValues = JsonSerializer.Serialize(variableSet),
                    ExecutedAt = DateTime.UtcNow,
                    AiProvider = aiProvider ?? "MCP Batch",
                    Model = model ?? "N/A"
                };

                executions.Add(execution);

                results.Add(new IndividualExecutionResult
                {
                    Variables = variableSet,
                    ResolvedPrompt = resolvedPrompt,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                results.Add(new IndividualExecutionResult
                {
                    Variables = variableSet,
                    ResolvedPrompt = string.Empty,
                    Success = false,
                    Error = ex.Message
                });
            }
        }

        // Save successful executions to database
        if (executions.Any())
        {
            _context.PromptExecutions.AddRange(executions);
            await _context.SaveChangesAsync();

            // Update results with execution IDs
            var successfulResults = results.Where(r => r.Success).ToList();
            for (int i = 0; i < successfulResults.Count && i < executions.Count; i++)
            {
                successfulResults[i].ExecutionId = executions[i].Id;
            }
        }

        return results;
    }

    #endregion

    #region Execution History

    /// <summary>
    /// Get execution history for prompts
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <returns>List of prompt executions</returns>
    public async Task<List<PromptExecution>> GetExecutionHistoryAsync(int? promptId = null, int limit = 50)
    {
        var query = _context.PromptExecutions
            .Include(e => e.PromptTemplate)
                .ThenInclude(pt => pt.PromptLibrary)
            .AsQueryable();

        if (promptId.HasValue)
        {
            query = query.Where(e => e.PromptTemplateId == promptId.Value);
        }

        return await query
            .OrderByDescending(e => e.ExecutedAt)
            .Take(limit)
            .ToListAsync();
    }

    /// <summary>
    /// Get execution history for prompts with pagination
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <returns>Paginated list of prompt executions</returns>
    public async Task<PagedResult<PromptExecution>> GetExecutionHistoryPagedAsync(int pageNumber, int pageSize, int? promptId = null)
    {
        var query = _context.PromptExecutions
            .Include(e => e.PromptTemplate)
                .ThenInclude(pt => pt.PromptLibrary)
            .AsQueryable();

        if (promptId.HasValue)
        {
            query = query.Where(e => e.PromptTemplateId == promptId.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.ExecutedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PromptExecution>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    /// <summary>
    /// Get execution by ID
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <returns>Execution details, or null if not found</returns>
    public async Task<PromptExecution?> GetExecutionByIdAsync(int executionId)
    {
        return await _context.PromptExecutions
            .Include(e => e.PromptTemplate)
                .ThenInclude(pt => pt.PromptLibrary)
            .FirstOrDefaultAsync(e => e.Id == executionId);
    }

    /// <summary>
    /// Get total count of executions
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <returns>Total count of executions</returns>
    public async Task<int> GetTotalExecutionsCountAsync(int? promptId = null)
    {
        var query = _context.PromptExecutions.AsQueryable();

        if (promptId.HasValue)
        {
            query = query.Where(e => e.PromptTemplateId == promptId.Value);
        }

        return await query.CountAsync();
    }

    #endregion

    #region Execution Analytics

    /// <summary>
    /// Get execution statistics for a prompt template
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="daysBack">Number of days to look back for statistics</param>
    /// <returns>Execution statistics</returns>
    public async Task<ExecutionStatistics> GetExecutionStatisticsAsync(int promptId, int daysBack = 30)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-daysBack);
        
        var executions = await _context.PromptExecutions
            .Where(e => e.PromptTemplateId == promptId && e.ExecutedAt >= cutoffDate)
            .OrderBy(e => e.ExecutedAt)
            .ToListAsync();

        var totalExecutions = executions.Count;
        var successfulExecutions = executions.Count; // All saved executions are considered successful
        var failedExecutions = 0; // We don't currently save failed executions

        var variableUsage = new Dictionary<string, int>();
        foreach (var execution in executions)
        {
            try
            {
                var variables = JsonSerializer.Deserialize<Dictionary<string, string>>(execution.VariableValues);
                if (variables != null)
                {
                    foreach (var variable in variables.Keys)
                    {
                        variableUsage[variable] = variableUsage.GetValueOrDefault(variable, 0) + 1;
                    }
                }
            }
            catch
            {
                // Skip invalid JSON
            }
        }

        return new ExecutionStatistics
        {
            TotalExecutions = totalExecutions,
            SuccessfulExecutions = successfulExecutions,
            FailedExecutions = failedExecutions,
            LastExecution = executions.LastOrDefault()?.ExecutedAt,
            FirstExecution = executions.FirstOrDefault()?.ExecutedAt,
            AverageExecutionsPerDay = daysBack > 0 ? (double)totalExecutions / daysBack : 0,
            VariableUsageCount = variableUsage
        };
    }

    /// <summary>
    /// Get execution statistics for a prompt library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="daysBack">Number of days to look back for statistics</param>
    /// <returns>Library execution statistics</returns>
    public async Task<LibraryExecutionStatistics> GetLibraryExecutionStatisticsAsync(int libraryId, int daysBack = 30)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-daysBack);

        var libraryData = await _context.PromptLibraries
            .Include(l => l.PromptTemplates)
                .ThenInclude(pt => pt.Executions.Where(e => e.ExecutedAt >= cutoffDate))
            .FirstOrDefaultAsync(l => l.Id == libraryId);

        if (libraryData == null)
        {
            throw new ArgumentException($"Library with ID {libraryId} not found", nameof(libraryId));
        }

        var totalTemplates = libraryData.PromptTemplates.Count;
        var executedTemplates = libraryData.PromptTemplates.Count(pt => pt.Executions.Any());
        var totalExecutions = libraryData.PromptTemplates.Sum(pt => pt.Executions.Count);
        var lastExecution = libraryData.PromptTemplates
            .SelectMany(pt => pt.Executions)
            .OrderByDescending(e => e.ExecutedAt)
            .FirstOrDefault()?.ExecutedAt;

        var templateStats = libraryData.PromptTemplates
            .Where(pt => pt.Executions.Any())
            .Select(pt => new TemplateExecutionSummary
            {
                TemplateId = pt.Id,
                TemplateName = pt.Name,
                ExecutionCount = pt.Executions.Count,
                LastExecution = pt.Executions.OrderByDescending(e => e.ExecutedAt).FirstOrDefault()?.ExecutedAt
            })
            .OrderByDescending(ts => ts.ExecutionCount)
            .ToList();

        return new LibraryExecutionStatistics
        {
            TotalExecutions = totalExecutions,
            ExecutedTemplatesCount = executedTemplates,
            TotalTemplatesCount = totalTemplates,
            AverageExecutionsPerTemplate = totalTemplates > 0 ? (double)totalExecutions / totalTemplates : 0,
            LastExecution = lastExecution,
            TemplateStatistics = templateStats
        };
    }

    /// <summary>
    /// Get most frequently executed templates
    /// </summary>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="limit">Maximum number of templates to return</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>List of templates with execution counts</returns>
    public async Task<List<TemplateExecutionSummary>> GetMostExecutedTemplatesAsync(int? libraryId = null, int limit = 10, int daysBack = 30)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-daysBack);

        var query = _context.PromptExecutions
            .Include(e => e.PromptTemplate)
            .Where(e => e.ExecutedAt >= cutoffDate)
            .AsQueryable();

        if (libraryId.HasValue)
        {
            query = query.Where(e => e.PromptTemplate.PromptLibraryId == libraryId.Value);
        }

        var templateStats = await query
            .GroupBy(e => new { e.PromptTemplateId, e.PromptTemplate.Name })
            .Select(g => new TemplateExecutionSummary
            {
                TemplateId = g.Key.PromptTemplateId,
                TemplateName = g.Key.Name,
                ExecutionCount = g.Count(),
                LastExecution = g.Max(e => e.ExecutedAt)
            })
            .OrderByDescending(ts => ts.ExecutionCount)
            .Take(limit)
            .ToListAsync();

        return templateStats;
    }

    #endregion

    #region Execution Management

    /// <summary>
    /// Save a list of prompt executions to the database
    /// </summary>
    /// <param name="executions">List of prompt executions</param>
    /// <returns>List of saved prompt executions</returns>
    public async Task<List<PromptExecution>> SavePromptExecutionsAsync(List<PromptExecution> executions)
    {
        if (executions == null || executions.Count == 0)
        {
            return executions ?? new List<PromptExecution>();
        }

        _context.PromptExecutions.AddRange(executions);
        await _context.SaveChangesAsync();
        return executions;
    }

    /// <summary>
    /// Delete execution by ID
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <returns>True if deleted successfully, false otherwise</returns>
    public async Task<bool> DeleteExecutionAsync(int executionId)
    {
        var execution = await _context.PromptExecutions.FindAsync(executionId);
        if (execution == null)
        {
            return false;
        }

        try
        {
            _context.PromptExecutions.Remove(execution);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Delete executions older than specified date
    /// </summary>
    /// <param name="olderThan">Date threshold</param>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <returns>Number of executions deleted</returns>
    public async Task<int> DeleteOldExecutionsAsync(DateTime olderThan, int? promptId = null)
    {
        var query = _context.PromptExecutions
            .Where(e => e.ExecutedAt < olderThan);

        if (promptId.HasValue)
        {
            query = query.Where(e => e.PromptTemplateId == promptId.Value);
        }

        var executionsToDelete = await query.ToListAsync();
        
        if (!executionsToDelete.Any())
        {
            return 0;
        }

        _context.PromptExecutions.RemoveRange(executionsToDelete);
        await _context.SaveChangesAsync();

        return executionsToDelete.Count;
    }

    #endregion
}
