using PromptStudio.Core.Domain;
using PromptStudio.Core.Attributes;

namespace PromptStudio.Core.Interfaces.Updated;

/// <summary>
/// Primary service interface for coordinating prompt operations across specialized services (Updated for Guid-based architecture)
/// This acts as a facade/coordinator for the underlying specialized services
/// </summary>
public interface IPromptService
{
    #region Modern Prompt Lab/Library Operations

    /// <summary>
    /// Get all prompt labs
    /// </summary>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="userId">Optional user ID to filter by access</param>
    /// <returns>List of all accessible prompt labs</returns>
    [McpExposed(Name = "GetPromptLabs", Description = "Get all accessible prompt labs")]
    Task<List<PromptLab>> GetPromptLabsAsync(Guid? tenantId = null, string? userId = null);

    /// <summary>
    /// Get a prompt lab by ID
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Prompt lab with libraries, or null if not found</returns>
    [McpExposed(Name = "GetPromptLab", Description = "Get prompt lab details with libraries")]
    Task<PromptLab?> GetPromptLabByIdAsync(Guid labId, Guid? tenantId = null);

    /// <summary>
    /// Get all prompt libraries
    /// </summary>
    /// <param name="labId">Optional lab ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>List of all accessible prompt libraries</returns>
    [McpExposed(Name = "GetPromptLibraries", Description = "Get all accessible prompt libraries")]
    Task<List<PromptLibrary>> GetPromptLibrariesAsync(Guid? labId = null, Guid? tenantId = null);

    /// <summary>
    /// Get a prompt library by ID
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Library with prompt templates, or null if not found</returns>
    [McpExposed(Name = "GetPromptLibrary", Description = "Get library details with prompt templates")]
    Task<PromptLibrary?> GetPromptLibraryByIdAsync(Guid libraryId, Guid? tenantId = null);

    /// <summary>
    /// Create a new prompt library
    /// </summary>
    /// <param name="name">Library name</param>
    /// <param name="labId">Lab ID</param>
    /// <param name="description">Optional library description</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="createdBy">User ID who created the library</param>
    /// <returns>Created library</returns>
    [McpExposed(Name = "CreatePromptLibrary", Description = "Create a new prompt library")]
    Task<PromptLibrary> CreatePromptLibraryAsync(string name, Guid labId, string? description = null, Guid? tenantId = null, string? createdBy = null);

    #endregion

    #region Modern Prompt Template Operations

    /// <summary>
    /// List all prompt templates, optionally filtered by library
    /// </summary>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>List of prompt templates</returns>
    [McpExposed(Name = "ListPromptTemplates", Description = "List all prompt templates, optionally filtered by library")]
    Task<List<PromptTemplate>> ListPromptTemplatesAsync(Guid? libraryId = null, Guid? tenantId = null);

    /// <summary>
    /// Get a specific prompt template
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Prompt template, or null if not found</returns>
    [McpExposed(Name = "GetPromptTemplate", Description = "Get a specific prompt template")]
    Task<PromptTemplate?> GetPromptTemplateAsync(Guid templateId, Guid? tenantId = null);

    /// <summary>
    /// Create a new prompt template
    /// </summary>
    /// <param name="name">Template name</param>
    /// <param name="content">Template content</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="description">Optional template description</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="createdBy">User ID who created the template</param>
    /// <returns>Created template</returns>
    [McpExposed(Name = "CreatePromptTemplate", Description = "Create a new prompt template")]
    Task<PromptTemplate> CreatePromptTemplateAsync(string name, string content, Guid libraryId, string? description = null, Guid? tenantId = null, string? createdBy = null);

    #endregion

    #region Execution Operations

    /// <summary>
    /// Execute a prompt template with provided variables
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variables">Variables as JSON string</param>
    /// <param name="aiProvider">Optional AI provider name</param>
    /// <param name="model">Optional model name</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="executedBy">User ID who executed the template</param>
    /// <returns>Execution result with resolved prompt</returns>
    [McpExposed(Name = "ExecutePromptTemplate", Description = "Execute a prompt template with variables", WrapInEnvelope = false)]
    Task<object> ExecutePromptTemplateAsync(Guid templateId, string variables, string? aiProvider = null, string? model = null, Guid? tenantId = null, string? executedBy = null);

    /// <summary>
    /// Execute a prompt template with variable dictionary
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <param name="aiProvider">Optional AI provider name</param>
    /// <param name="model">Optional model name</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="executedBy">User ID who executed the template</param>
    /// <returns>Execution result with resolved prompt</returns>
    [McpExposed(Name = "ExecutePromptWithVariables", Description = "Execute a prompt template with variable dictionary")]
    Task<ExecutionResult> ExecutePromptWithVariablesAsync(Guid templateId, Dictionary<string, string> variableValues, string? aiProvider = null, string? model = null, Guid? tenantId = null, string? executedBy = null);

    /// <summary>
    /// Execute batch processing with a variable collection
    /// </summary>
    /// <param name="collectionId">Variable collection ID</param>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="aiProvider">Optional AI provider name</param>
    /// <param name="model">Optional model name</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="executedBy">User ID who executed the batch</param>
    /// <returns>Batch execution result</returns>
    [McpExposed(Name = "ExecuteBatchProcessing", Description = "Execute prompt template with variable collection", WrapInEnvelope = false)]
    Task<object> ExecuteBatchProcessingAsync(Guid collectionId, Guid templateId, string? aiProvider = null, string? model = null, Guid? tenantId = null, string? executedBy = null);

    #endregion

    #region Execution History and Analytics

    /// <summary>
    /// Get execution history for prompts
    /// </summary>
    /// <param name="templateId">Optional template ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <returns>List of prompt executions</returns>
    [McpExposed(Name = "GetExecutionHistory", Description = "Get prompt execution history")]
    Task<List<PromptExecution>> GetExecutionHistoryAsync(Guid? templateId = null, Guid? tenantId = null, string? userId = null, int limit = 50);

    /// <summary>
    /// Get execution statistics for a template
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>Execution statistics</returns>
    [McpExposed(Name = "GetExecutionStatistics", Description = "Get execution statistics for a template")]
    Task<ExecutionStatistics> GetExecutionStatisticsAsync(Guid templateId, Guid? tenantId = null, int daysBack = 30);

    #endregion

    #region Variable Collections

    /// <summary>
    /// Get variable collections for a template
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>List of variable collections</returns>
    [McpExposed(Name = "GetVariableCollections", Description = "Get variable collections for a template")]
    Task<List<VariableCollection>> GetVariableCollectionsAsync(Guid templateId, Guid? tenantId = null);

    /// <summary>
    /// Create a variable collection from CSV data
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="templateId">Template ID</param>
    /// <param name="csvData">CSV data with variables</param>
    /// <param name="description">Optional collection description</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="createdBy">User ID who created the collection</param>
    /// <returns>Created variable collection</returns>
    [McpExposed(Name = "CreateVariableCollection", Description = "Create a variable collection from CSV data")]
    Task<VariableCollection> CreateVariableCollectionAsync(string name, Guid templateId, string csvData, string? description = null, Guid? tenantId = null, string? createdBy = null);

    /// <summary>
    /// Generate a CSV template for a prompt template
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>CSV template content</returns>
    [McpExposed(Name = "GenerateCsvTemplate", Description = "Generate CSV template for prompt variables")]
    Task<string> GenerateCsvTemplateAsync(Guid templateId, Guid? tenantId = null);

    #endregion

    #region Search and Discovery

    /// <summary>
    /// Search templates across all accessible libraries
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="limit">Maximum number of results to return</param>
    /// <returns>List of matching templates</returns>
    [McpExposed(Name = "SearchTemplates", Description = "Search templates by name or content")]
    Task<List<PromptTemplate>> SearchTemplatesAsync(string searchTerm, Guid? libraryId = null, Guid? tenantId = null, int limit = 50);

    /// <summary>
    /// Get popular templates (by execution count)
    /// </summary>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="limit">Maximum number of templates to return</param>
    /// <param name="daysBack">Number of days to look back for popularity stats</param>
    /// <returns>List of popular templates</returns>
    [McpExposed(Name = "GetPopularTemplates", Description = "Get most popular templates by execution count")]
    Task<List<PromptTemplate>> GetPopularTemplatesAsync(Guid? libraryId = null, Guid? tenantId = null, int limit = 10, int daysBack = 30);

    /// <summary>
    /// Get recently updated templates
    /// </summary>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="limit">Maximum number of templates to return</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>List of recently updated templates</returns>
    [McpExposed(Name = "GetRecentTemplates", Description = "Get recently updated templates")]
    Task<List<PromptTemplate>> GetRecentTemplatesAsync(Guid? libraryId = null, Guid? tenantId = null, int limit = 10, int daysBack = 7);

    #endregion

    #region Legacy Collection Support (Temporary - will be removed after migration)

    /// <summary>
    /// Get all collections (Legacy - will be replaced with PromptLibrary operations)
    /// </summary>
    /// <returns>List of all collections with their prompt templates</returns>
    [McpExposed(Name = "GetCollections", Description = "Get all prompt collections (Legacy)")]
    [Obsolete("Use GetPromptLibrariesAsync instead. This method will be removed in a future version.")]
    Task<List<Collection>> GetCollectionsAsync();

    /// <summary>
    /// Get a collection by ID (Legacy - will be replaced with PromptLibrary operations)
    /// </summary>
    /// <param name="id">Collection ID</param>
    /// <returns>Collection with prompt templates, or null if not found</returns>
    [McpExposed(Name = "GetCollection", Description = "Get collection details with prompts (Legacy)")]
    [Obsolete("Use GetPromptLibraryByIdAsync instead. This method will be removed in a future version.")]
    Task<Collection?> GetCollectionByIdAsync(int id);

    /// <summary>
    /// Create a new collection (Legacy - will be replaced with PromptLibrary operations)
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="description">Optional collection description</param>
    /// <returns>Created collection</returns>
    [McpExposed(Name = "CreateCollection", Description = "Create a new prompt collection (Legacy)")]
    [Obsolete("Use CreatePromptLibraryAsync instead. This method will be removed in a future version.")]
    Task<Collection> CreateCollectionAsync(string name, string? description = null);

    /// <summary>
    /// Import a collection from JSON (Legacy - will be replaced with PromptLibrary operations)
    /// </summary>
    /// <param name="jsonContent">JSON content representing the collection</param>
    /// <param name="importExecutionHistory">Whether to import execution history</param>
    /// <param name="overwriteExisting">Whether to overwrite existing collection</param>
    /// <returns>Imported collection</returns>
    [Obsolete("Use ImportLibraryAsync instead. This method will be removed in a future version.")]
    Task<Collection?> ImportCollectionFromJsonAsync(string jsonContent, bool importExecutionHistory, bool overwriteExisting);

    #endregion

    #region Import/Export Operations

    /// <summary>
    /// Export a library to JSON format
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="includeExecutionHistory">Whether to include execution history</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>JSON representation of the library</returns>
    [McpExposed(Name = "ExportLibrary", Description = "Export library to JSON format")]
    Task<string> ExportLibraryAsync(Guid libraryId, bool includeExecutionHistory = false, Guid? tenantId = null);

    /// <summary>
    /// Import a library from JSON format
    /// </summary>
    /// <param name="jsonContent">JSON content representing the library</param>
    /// <param name="labId">Target lab ID</param>
    /// <param name="importExecutionHistory">Whether to import execution history</param>
    /// <param name="overwriteExisting">Whether to overwrite existing library</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="importedBy">User ID who imported the library</param>
    /// <returns>Imported library or null if import failed</returns>
    [McpExposed(Name = "ImportLibrary", Description = "Import library from JSON format")]
    Task<PromptLibrary?> ImportLibraryAsync(string jsonContent, Guid labId, bool importExecutionHistory = false, bool overwriteExisting = false, Guid? tenantId = null, string? importedBy = null);

    #endregion

    #region Workflow Operations

    /// <summary>
    /// Get workflows for a lab
    /// </summary>
    /// <param name="labId">Lab ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>List of workflows</returns>
    [McpExposed(Name = "GetWorkflows", Description = "Get workflows for a lab")]
    Task<List<PromptFlow>> GetWorkflowsAsync(Guid labId, Guid? tenantId = null);

    /// <summary>
    /// Execute a workflow
    /// </summary>
    /// <param name="workflowId">Workflow ID</param>
    /// <param name="variables">Input variables</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="executedBy">User ID who executed the workflow</param>
    /// <returns>Workflow execution result</returns>
    [McpExposed(Name = "ExecuteWorkflow", Description = "Execute a workflow with input variables")]
    Task<FlowExecutionResult> ExecuteWorkflowAsync(Guid workflowId, Dictionary<string, object> variables, Guid? tenantId = null, string? executedBy = null);

    #endregion

    #region Health and Monitoring

    /// <summary>
    /// Get service health information
    /// </summary>
    /// <returns>Service health status</returns>
    [McpExposed(Name = "GetServiceHealth", Description = "Get service health information")]
    Task<ServiceHealthStatus> GetServiceHealthAsync();

    /// <summary>
    /// Get tenant usage statistics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>Tenant usage statistics</returns>
    [McpExposed(Name = "GetTenantUsage", Description = "Get tenant usage statistics")]
    Task<TenantUsageStatistics> GetTenantUsageAsync(Guid tenantId, int daysBack = 30);

    #endregion
}

/// <summary>
/// Service health status
/// </summary>
public class ServiceHealthStatus
{
    /// <summary>
    /// Whether the service is healthy
    /// </summary>
    public bool IsHealthy { get; set; }

    /// <summary>
    /// Health check timestamp
    /// </summary>
    public DateTime CheckedAt { get; set; }

    /// <summary>
    /// Service version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Database connection status
    /// </summary>
    public bool DatabaseConnected { get; set; }

    /// <summary>
    /// External service statuses
    /// </summary>
    public Dictionary<string, bool> ExternalServices { get; set; } = new();

    /// <summary>
    /// Performance metrics
    /// </summary>
    public Dictionary<string, object> PerformanceMetrics { get; set; } = new();

    /// <summary>
    /// Any health issues
    /// </summary>
    public List<string> Issues { get; set; } = new();
}

/// <summary>
/// Tenant usage statistics
/// </summary>
public class TenantUsageStatistics
{
    /// <summary>
    /// Tenant ID
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Number of labs
    /// </summary>
    public int LabCount { get; set; }

    /// <summary>
    /// Number of libraries
    /// </summary>
    public int LibraryCount { get; set; }

    /// <summary>
    /// Number of templates
    /// </summary>
    public int TemplateCount { get; set; }

    /// <summary>
    /// Number of workflows
    /// </summary>
    public int WorkflowCount { get; set; }

    /// <summary>
    /// Number of executions
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Number of active users
    /// </summary>
    public int ActiveUserCount { get; set; }

    /// <summary>
    /// Total token usage
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total cost
    /// </summary>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Usage period start
    /// </summary>
    public DateTime PeriodStart { get; set; }

    /// <summary>
    /// Usage period end
    /// </summary>
    public DateTime PeriodEnd { get; set; }

    /// <summary>
    /// Usage trends over time
    /// </summary>
    public List<TenantUsageTrendData> UsageTrends { get; set; } = new();
}

/// <summary>
/// Tenant usage trend data
/// </summary>
public class TenantUsageTrendData
{
    /// <summary>
    /// Time period
    /// </summary>
    public DateTime Period { get; set; }

    /// <summary>
    /// Number of executions in this period
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Number of active users in this period
    /// </summary>
    public int ActiveUsers { get; set; }

    /// <summary>
    /// Token usage for this period
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Cost for this period
    /// </summary>
    public decimal? Cost { get; set; }
}
