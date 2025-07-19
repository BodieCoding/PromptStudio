using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces.Updated;

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
}

/// <summary>
/// Flow execution result (Updated for Guid-based architecture)
/// </summary>
public class FlowExecutionResult
{
    /// <summary>
    /// Execution ID
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Flow ID that was executed
    /// </summary>
    public Guid FlowId { get; set; }

    /// <summary>
    /// Flow name
    /// </summary>
    public string FlowName { get; set; } = string.Empty;

    /// <summary>
    /// Whether the execution was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Execution start time
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Execution completion time
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Total execution duration
    /// </summary>
    public TimeSpan? Duration => CompletedAt?.Subtract(StartedAt);

    /// <summary>
    /// Input variables provided to the flow
    /// </summary>
    public Dictionary<string, object> InputVariables { get; set; } = new();

    /// <summary>
    /// Output variables produced by the flow
    /// </summary>
    public Dictionary<string, object> OutputVariables { get; set; } = new();

    /// <summary>
    /// Individual node execution results
    /// </summary>
    public List<NodeExecutionResult> NodeExecutions { get; set; } = new();

    /// <summary>
    /// Execution status
    /// </summary>
    public FlowExecutionStatus Status { get; set; }

    /// <summary>
    /// User who executed the flow
    /// </summary>
    public string? ExecutedBy { get; set; }

    /// <summary>
    /// Tenant ID for multi-tenancy
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Total token usage across all nodes
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total cost across all nodes
    /// </summary>
    public decimal? TotalCost { get; set; }

    /// <summary>
    /// Additional metadata
    /// </summary>
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Node execution result
/// </summary>
public class NodeExecutionResult
{
    /// <summary>
    /// Node execution ID
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Node ID that was executed
    /// </summary>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Node name
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Node type
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// Whether the node execution was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Node execution start time
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Node execution completion time
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Node execution duration
    /// </summary>
    public TimeSpan? Duration => CompletedAt?.Subtract(StartedAt);

    /// <summary>
    /// Input data provided to the node
    /// </summary>
    public Dictionary<string, object> InputData { get; set; } = new();

    /// <summary>
    /// Output data produced by the node
    /// </summary>
    public Dictionary<string, object> OutputData { get; set; } = new();

    /// <summary>
    /// Node execution status
    /// </summary>
    public NodeExecutionStatus Status { get; set; }

    /// <summary>
    /// Token usage for this node
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Cost for this node execution
    /// </summary>
    public decimal? Cost { get; set; }
}

/// <summary>
/// Flow execution options
/// </summary>
public class FlowExecutionOptions
{
    /// <summary>
    /// Whether to execute in debug mode
    /// </summary>
    public bool DebugMode { get; set; } = false;

    /// <summary>
    /// Whether to stop on first error
    /// </summary>
    public bool StopOnError { get; set; } = true;

    /// <summary>
    /// Maximum execution timeout
    /// </summary>
    public TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Whether to save execution history
    /// </summary>
    public bool SaveExecutionHistory { get; set; } = true;

    /// <summary>
    /// Whether to enable parallel execution where possible
    /// </summary>
    public bool EnableParallelExecution { get; set; } = true;

    /// <summary>
    /// Custom execution parameters
    /// </summary>
    public Dictionary<string, object>? CustomParameters { get; set; }
}

/// <summary>
/// Flow execution update for streaming
/// </summary>
public class FlowExecutionUpdate
{
    /// <summary>
    /// Execution ID
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Update type
    /// </summary>
    public FlowExecutionUpdateType UpdateType { get; set; }

    /// <summary>
    /// Node ID (if update is node-specific)
    /// </summary>
    public Guid? NodeId { get; set; }

    /// <summary>
    /// Update message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Update data
    /// </summary>
    public Dictionary<string, object>? Data { get; set; }

    /// <summary>
    /// Update timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Whether this is the final update
    /// </summary>
    public bool IsComplete { get; set; }
}

/// <summary>
/// Flow execution update types
/// </summary>
public enum FlowExecutionUpdateType
{
    Started,
    NodeStarted,
    NodeCompleted,
    NodeFailed,
    Progress,
    Completed,
    Failed,
    Cancelled
}

/// <summary>
/// Flow execution status
/// </summary>
public enum FlowExecutionStatus
{
    Pending,
    Running,
    Completed,
    Failed,
    Cancelled,
    Timeout
}

/// <summary>
/// Node execution status
/// </summary>
public enum NodeExecutionStatus
{
    Pending,
    Running,
    Completed,
    Failed,
    Skipped,
    Cancelled
}

/// <summary>
/// Flow validation result
/// </summary>
public class FlowValidationResult
{
    /// <summary>
    /// Whether the flow is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Validation errors (critical issues)
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Validation warnings (non-critical issues)
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Validation recommendations
    /// </summary>
    public List<string> Recommendations { get; set; } = new();

    /// <summary>
    /// Node validation results
    /// </summary>
    public List<NodeValidationResult> NodeValidations { get; set; } = new();

    /// <summary>
    /// Whether circular dependencies exist
    /// </summary>
    public bool HasCircularDependencies { get; set; }

    /// <summary>
    /// Unreachable nodes
    /// </summary>
    public List<Guid> UnreachableNodes { get; set; } = new();
}

/// <summary>
/// Node validation result
/// </summary>
public class NodeValidationResult
{
    /// <summary>
    /// Node ID
    /// </summary>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Node name
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Whether the node is valid
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Node-specific errors
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Node-specific warnings
    /// </summary>
    public List<string> Warnings { get; set; } = new();
}

/// <summary>
/// Flow execution statistics
/// </summary>
public class FlowExecutionStatistics
{
    /// <summary>
    /// Flow ID
    /// </summary>
    public Guid FlowId { get; set; }

    /// <summary>
    /// Total number of executions
    /// </summary>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Number of successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Number of failed executions
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Success rate as a percentage
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Average execution duration
    /// </summary>
    public TimeSpan? AverageExecutionDuration { get; set; }

    /// <summary>
    /// Most recent execution date
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// First execution date in the period
    /// </summary>
    public DateTime? FirstExecution { get; set; }

    /// <summary>
    /// Number of unique users who executed the flow
    /// </summary>
    public int UniqueUsers { get; set; }

    /// <summary>
    /// Total token usage across all executions
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total cost across all executions
    /// </summary>
    public decimal? TotalCost { get; set; }
}

/// <summary>
/// Flow performance metrics
/// </summary>
public class FlowPerformanceMetrics
{
    /// <summary>
    /// Flow ID
    /// </summary>
    public Guid FlowId { get; set; }

    /// <summary>
    /// Average execution time
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Median execution time
    /// </summary>
    public TimeSpan MedianExecutionTime { get; set; }

    /// <summary>
    /// 95th percentile execution time
    /// </summary>
    public TimeSpan P95ExecutionTime { get; set; }

    /// <summary>
    /// 99th percentile execution time
    /// </summary>
    public TimeSpan P99ExecutionTime { get; set; }

    /// <summary>
    /// Fastest execution time
    /// </summary>
    public TimeSpan FastestExecutionTime { get; set; }

    /// <summary>
    /// Slowest execution time
    /// </summary>
    public TimeSpan SlowestExecutionTime { get; set; }

    /// <summary>
    /// Throughput (executions per hour)
    /// </summary>
    public double Throughput { get; set; }

    /// <summary>
    /// Bottleneck nodes (nodes that take the longest time)
    /// </summary>
    public List<NodePerformanceMetrics> BottleneckNodes { get; set; } = new();
}

/// <summary>
/// Node performance metrics
/// </summary>
public class NodePerformanceMetrics
{
    /// <summary>
    /// Node ID
    /// </summary>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Node name
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Node type
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// Average execution time for this node
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Number of times this node was executed
    /// </summary>
    public int ExecutionCount { get; set; }

    /// <summary>
    /// Success rate for this node
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Token usage for this node
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Total cost for this node
    /// </summary>
    public decimal? TotalCost { get; set; }
}

/// <summary>
/// Node execution statistics
/// </summary>
public class NodeExecutionStatistics
{
    /// <summary>
    /// Node ID
    /// </summary>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Node name
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Node type
    /// </summary>
    public string NodeType { get; set; } = string.Empty;

    /// <summary>
    /// Total number of executions
    /// </summary>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Number of successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }

    /// <summary>
    /// Number of failed executions
    /// </summary>
    public int FailedExecutions { get; set; }

    /// <summary>
    /// Success rate as a percentage
    /// </summary>
    public double SuccessRate => TotalExecutions > 0 ? (double)SuccessfulExecutions / TotalExecutions * 100 : 0;

    /// <summary>
    /// Average execution duration
    /// </summary>
    public TimeSpan? AverageExecutionDuration { get; set; }

    /// <summary>
    /// Total token usage
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Total cost
    /// </summary>
    public decimal? TotalCost { get; set; }
}
