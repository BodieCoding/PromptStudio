using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Flow;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Interface for visual prompt flow operations (Updated for Guid-based architecture)
/// </summary>
public interface IFlowService
{
    #region Flow CRUD Operations

    /// <summary>
    /// Get all flows for a user with optional filtering
    /// </summary>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="labId">Optional lab ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="tag">Optional tag to filter by</param>
    /// <param name="search">Optional search term</param>
    /// <param name="includeDeleted">Whether to include soft-deleted flows</param>
    /// <returns>List of prompt flows</returns>
    Task<IEnumerable<PromptFlow>> GetFlowsAsync(string? userId = null, Guid? labId = null, 
        Guid? tenantId = null, string? tag = null, string? search = null, bool includeDeleted = false);

    /// <summary>
    /// Get a specific flow by ID
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="includeDeleted">Whether to include soft-deleted flows</param>
    /// <param name="includeNodes">Whether to include flow nodes and edges</param>
    /// <returns>Prompt flow or null if not found</returns>
    Task<PromptFlow?> GetFlowByIdAsync(Guid flowId, Guid? tenantId = null, bool includeDeleted = false, bool includeNodes = true);

    /// <summary>
    /// Create a new prompt flow
    /// </summary>
    /// <param name="name">Flow name</param>
    /// <param name="description">Optional flow description</param>
    /// <param name="labId">Lab ID where the flow belongs</param>
    /// <param name="isTemplate">Whether this flow is a template</param>
    /// <param name="tags">Optional tags</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="createdBy">User ID who created the flow</param>
    /// <returns>Created prompt flow</returns>
    Task<PromptFlow> CreateFlowAsync(string name, string? description = null, Guid? labId = null, 
        bool isTemplate = false, List<string>? tags = null, Guid? tenantId = null, string? createdBy = null);

    /// <summary>
    /// Update an existing prompt flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="name">Updated name</param>
    /// <param name="description">Updated description</param>
    /// <param name="isTemplate">Updated template status</param>
    /// <param name="tags">Updated tags</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="updatedBy">User ID who updated the flow</param>
    /// <returns>Updated prompt flow or null if not found</returns>
    Task<PromptFlow?> UpdateFlowAsync(Guid flowId, string name, string? description = null, 
        bool? isTemplate = null, List<string>? tags = null, Guid? tenantId = null, string? updatedBy = null);

    /// <summary>
    /// Delete a prompt flow (soft delete)
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="deletedBy">User ID who deleted the flow</param>
    /// <returns>True if deleted successfully, false otherwise</returns>
    Task<bool> DeleteFlowAsync(Guid flowId, Guid? tenantId = null, string? deletedBy = null);

    /// <summary>
    /// Permanently delete a prompt flow (hard delete)
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>True if permanently deleted successfully, false otherwise</returns>
    Task<bool> PermanentlyDeleteFlowAsync(Guid flowId, Guid? tenantId = null);

    /// <summary>
    /// Restore a soft-deleted prompt flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="restoredBy">User ID who restored the flow</param>
    /// <returns>True if restored successfully, false otherwise</returns>
    Task<bool> RestoreFlowAsync(Guid flowId, Guid? tenantId = null, string? restoredBy = null);

    #endregion

    #region Flow Node Management

    /// <summary>
    /// Add a node to a flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="node">Node to add</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="addedBy">User ID who added the node</param>
    /// <returns>Added flow node</returns>
    Task<FlowNode> AddFlowNodeAsync(Guid flowId, FlowNode node, Guid? tenantId = null, string? addedBy = null);

    /// <summary>
    /// Update a flow node
    /// </summary>
    /// <param name="nodeId">Node ID</param>
    /// <param name="node">Updated node data</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="updatedBy">User ID who updated the node</param>
    /// <returns>Updated flow node or null if not found</returns>
    Task<FlowNode?> UpdateFlowNodeAsync(Guid nodeId, FlowNode node, Guid? tenantId = null, string? updatedBy = null);

    /// <summary>
    /// Remove a node from a flow
    /// </summary>
    /// <param name="nodeId">Node ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="removedBy">User ID who removed the node</param>
    /// <returns>True if removed successfully, false otherwise</returns>
    Task<bool> RemoveFlowNodeAsync(Guid nodeId, Guid? tenantId = null, string? removedBy = null);

    /// <summary>
    /// Add an edge between two nodes
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="edge">Edge to add</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="addedBy">User ID who added the edge</param>
    /// <returns>Added flow edge</returns>
    Task<FlowEdge> AddFlowEdgeAsync(Guid flowId, FlowEdge edge, Guid? tenantId = null, string? addedBy = null);

    /// <summary>
    /// Remove an edge between two nodes
    /// </summary>
    /// <param name="edgeId">Edge ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="removedBy">User ID who removed the edge</param>
    /// <returns>True if removed successfully, false otherwise</returns>
    Task<bool> RemoveFlowEdgeAsync(Guid edgeId, Guid? tenantId = null, string? removedBy = null);

    #endregion

    #region Flow Execution

    /// <summary>
    /// Execute a prompt flow with given variables
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="variables">Input variables</param>
    /// <param name="options">Optional execution options</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="executedBy">User ID who executed the flow</param>
    /// <returns>Flow execution result</returns>
    Task<FlowExecutionResult> ExecuteFlowAsync(Guid flowId, Dictionary<string, object> variables, 
        FlowExecutionOptions? options = null, Guid? tenantId = null, string? executedBy = null);

    /// <summary>
    /// Execute a flow node individually (for testing/debugging)
    /// </summary>
    /// <param name="nodeId">Node ID</param>
    /// <param name="inputData">Input data for the node</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="executedBy">User ID who executed the node</param>
    /// <returns>Node execution result</returns>
    Task<NodeExecutionResult> ExecuteFlowNodeAsync(Guid nodeId, Dictionary<string, object> inputData, 
        Guid? tenantId = null, string? executedBy = null);

    /// <summary>
    /// Execute a flow with streaming results
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="variables">Input variables</param>
    /// <param name="options">Optional execution options</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="executedBy">User ID who executed the flow</param>
    /// <returns>Streaming flow execution results</returns>
    IAsyncEnumerable<FlowExecutionUpdate> ExecuteFlowStreamingAsync(Guid flowId, Dictionary<string, object> variables, 
        FlowExecutionOptions? options = null, Guid? tenantId = null, string? executedBy = null);

    /// <summary>
    /// Stop a running flow execution
    /// </summary>
    /// <param name="executionId">Execution ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="stoppedBy">User ID who stopped the execution</param>
    /// <returns>True if stopped successfully, false otherwise</returns>
    Task<bool> StopFlowExecutionAsync(Guid executionId, Guid? tenantId = null, string? stoppedBy = null);

    #endregion

    #region Flow Validation

    /// <summary>
    /// Validate a prompt flow structure
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Flow validation result</returns>
    Task<FlowValidationResult> ValidateFlowAsync(Guid flowId, Guid? tenantId = null);

    /// <summary>
    /// Validate flow definition before saving
    /// </summary>
    /// <param name="flow">Flow to validate</param>
    /// <returns>Flow validation result</returns>
    Task<FlowValidationResult> ValidateFlowDefinitionAsync(PromptFlow flow);

    /// <summary>
    /// Check for circular dependencies in a flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>True if circular dependencies exist, false otherwise</returns>
    Task<bool> HasCircularDependenciesAsync(Guid flowId, Guid? tenantId = null);

    #endregion

    #region Flow History and Analytics

    /// <summary>
    /// Get execution history for a flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <param name="includeNodeExecutions">Whether to include individual node executions</param>
    /// <returns>List of flow executions</returns>
    Task<IEnumerable<FlowExecution>> GetExecutionHistoryAsync(Guid flowId, Guid? tenantId = null, 
        int limit = 50, bool includeNodeExecutions = false);

    /// <summary>
    /// Get flow execution statistics
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>Flow execution statistics</returns>
    Task<FlowExecutionStatistics> GetFlowExecutionStatisticsAsync(Guid flowId, Guid? tenantId = null, int daysBack = 30);

    /// <summary>
    /// Get performance metrics for a flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>Flow performance metrics</returns>
    Task<FlowPerformanceMetrics> GetFlowPerformanceMetricsAsync(Guid flowId, Guid? tenantId = null, int daysBack = 30);

    /// <summary>
    /// Get node execution statistics for a flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>Node execution statistics</returns>
    Task<List<NodeExecutionStatistics>> GetNodeExecutionStatisticsAsync(Guid flowId, Guid? tenantId = null, int daysBack = 30);

    #endregion

    #region Flow Templates and Sharing

    /// <summary>
    /// Create a template from an existing flow
    /// </summary>
    /// <param name="sourceFlowId">Source flow ID</param>
    /// <param name="templateName">Template name</param>
    /// <param name="templateDescription">Template description</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="createdBy">User ID who created the template</param>
    /// <returns>Created flow template</returns>
    Task<PromptFlow> CreateFlowTemplateAsync(Guid sourceFlowId, string templateName, string? templateDescription = null, 
        Guid? tenantId = null, string? createdBy = null);

    /// <summary>
    /// Create a flow from a template
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="newFlowName">New flow name</param>
    /// <param name="labId">Lab ID where the flow will be created</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="createdBy">User ID who created the flow</param>
    /// <returns>Created flow from template</returns>
    Task<PromptFlow> CreateFlowFromTemplateAsync(Guid templateId, string newFlowName, Guid labId, 
        Guid? tenantId = null, string? createdBy = null);

    /// <summary>
    /// Clone a flow
    /// </summary>
    /// <param name="sourceFlowId">Source flow ID</param>
    /// <param name="newFlowName">New flow name</param>
    /// <param name="targetLabId">Optional target lab ID (if different from source)</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="clonedBy">User ID who cloned the flow</param>
    /// <returns>Cloned flow</returns>
    Task<PromptFlow> CloneFlowAsync(Guid sourceFlowId, string newFlowName, Guid? targetLabId = null, 
        Guid? tenantId = null, string? clonedBy = null);

    /// <summary>
    /// Get flow templates
    /// </summary>
    /// <param name="category">Optional category to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="includePublic">Whether to include public templates</param>
    /// <returns>List of flow templates</returns>
    Task<List<PromptFlow>> GetFlowTemplatesAsync(string? category = null, Guid? tenantId = null, bool includePublic = true);

    #endregion

    #region Flow Import/Export

    /// <summary>
    /// Export a flow to JSON format
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="includeExecutionHistory">Whether to include execution history</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>JSON representation of the flow</returns>
    Task<string> ExportFlowAsync(Guid flowId, bool includeExecutionHistory = false, Guid? tenantId = null);

    /// <summary>
    /// Import a flow from JSON format
    /// </summary>
    /// <param name="jsonContent">JSON content representing the flow</param>
    /// <param name="labId">Target lab ID</param>
    /// <param name="importExecutionHistory">Whether to import execution history</param>
    /// <param name="overwriteExisting">Whether to overwrite existing flow</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="importedBy">User ID who imported the flow</param>
    /// <returns>Imported flow or null if import failed</returns>
    Task<PromptFlow?> ImportFlowAsync(string jsonContent, Guid labId, bool importExecutionHistory = false, 
        bool overwriteExisting = false, Guid? tenantId = null, string? importedBy = null);

    #endregion

    #region Flow Discovery and Search

    /// <summary>
    /// Search flows by name, description, or tags
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <param name="labId">Optional lab ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="includeTemplates">Whether to include flow templates</param>
    /// <param name="limit">Maximum number of results to return</param>
    /// <returns>List of matching flows</returns>
    Task<List<PromptFlow>> SearchFlowsAsync(string searchTerm, Guid? labId = null, Guid? tenantId = null, 
        bool includeTemplates = false, int limit = 50);

    /// <summary>
    /// Get flows by tags
    /// </summary>
    /// <param name="tags">Tags to filter by</param>
    /// <param name="labId">Optional lab ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>List of flows matching the tags</returns>
    Task<List<PromptFlow>> GetFlowsByTagsAsync(List<string> tags, Guid? labId = null, Guid? tenantId = null);

    /// <summary>
    /// Get recently executed flows
    /// </summary>
    /// <param name="userId">Optional user ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="limit">Maximum number of flows to return</param>
    /// <param name="daysBack">Number of days to look back</param>
    /// <returns>List of recently executed flows</returns>
    Task<List<PromptFlow>> GetRecentlyExecutedFlowsAsync(string? userId = null, Guid? tenantId = null, 
        int limit = 10, int daysBack = 7);

    /// <summary>
    /// Get popular flows (by execution count)
    /// </summary>
    /// <param name="labId">Optional lab ID to filter by</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="limit">Maximum number of flows to return</param>
    /// <param name="daysBack">Number of days to look back for popularity stats</param>
    /// <returns>List of popular flows</returns>
    Task<List<PromptFlow>> GetPopularFlowsAsync(Guid? labId = null, Guid? tenantId = null, 
        int limit = 10, int daysBack = 30);

    #endregion

    #region Low-Level Repository Operations

    /// <summary>
    /// Synchronize flow JSON data with relational data (for performance optimization)
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Task</returns>
    Task SyncFlowDataAsync(Guid flowId, Guid? tenantId = null);

    /// <summary>
    /// Get detailed flow metrics with custom date range
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="from">Start date (optional)</param>
    /// <param name="to">End date (optional)</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Flow metrics</returns>
    Task<FlowMetrics> GetFlowMetricsAsync(Guid flowId, DateTime? from = null, DateTime? to = null, Guid? tenantId = null);

    /// <summary>
    /// Update cached flow metrics
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="metrics">Metrics to update</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Task</returns>
    Task UpdateFlowMetricsAsync(Guid flowId, FlowMetrics metrics, Guid? tenantId = null);

    /// <summary>
    /// Get detailed execution analytics with custom date range
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="from">Start date (optional)</param>
    /// <param name="to">End date (optional)</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Execution analytics</returns>
    Task<ExecutionAnalytics> GetExecutionAnalyticsAsync(Guid flowId, DateTime? from = null, DateTime? to = null, Guid? tenantId = null);

    /// <summary>
    /// Create a flow validation session for caching
    /// </summary>
    /// <param name="session">Validation session to create</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Created validation session</returns>
    Task<FlowValidationSession> CreateValidationSessionAsync(FlowValidationSession session, Guid? tenantId = null);

    /// <summary>
    /// Get cached validation session
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="version">Flow version</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Validation session or null if not found</returns>
    Task<FlowValidationSession?> GetValidationSessionAsync(Guid flowId, string version, Guid? tenantId = null);

    /// <summary>
    /// Get validation history for a flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>List of validation sessions</returns>
    Task<IEnumerable<FlowValidationSession>> GetValidationHistoryAsync(Guid flowId, Guid? tenantId = null);

    /// <summary>
    /// Invalidate validation cache for a flow
    /// </summary>
    /// <param name="flowId">Flow ID</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <returns>Task</returns>
    Task InvalidateValidationCacheAsync(Guid flowId, Guid? tenantId = null);

    #endregion
}
