using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Flow;

namespace PromptStudio.Core.Interfaces.Flow;

/// <summary>
/// MVP-focused service interface for visual prompt flow operations
/// Provides core flow operations without enterprise complexity
/// </summary>
public interface IFlowService
{
    #region Flow CRUD Operations

    /// <summary>
    /// Get flows with optional filtering
    /// </summary>
    Task<IEnumerable<PromptFlow>> GetFlowsAsync(
        string? searchTerm = null, 
        string? userId = null, 
        Guid? libraryId = null, 
        string? category = null, 
        string? status = null, 
        bool includeDeleted = false);

    /// <summary>
    /// Get a flow by ID
    /// </summary>
    Task<PromptFlow?> GetFlowByIdAsync(Guid flowId);

    /// <summary>
    /// Create a new flow
    /// </summary>
    Task<PromptFlow> CreateFlowAsync(PromptFlow flow);

    /// <summary>
    /// Update an existing flow
    /// </summary>
    Task<PromptFlow> UpdateFlowAsync(PromptFlow flow);

    /// <summary>
    /// Delete a flow (soft delete)
    /// </summary>
    Task<bool> DeleteFlowAsync(Guid flowId);

    #endregion

    #region Node Operations

    /// <summary>
    /// Add node to flow
    /// </summary>
    Task<FlowNode> AddNodeAsync(Guid flowId, FlowNode node);

    /// <summary>
    /// Update node in flow
    /// </summary>
    Task<FlowNode> UpdateNodeAsync(Guid flowId, FlowNode node);

    /// <summary>
    /// Remove node from flow
    /// </summary>
    Task<bool> RemoveNodeAsync(Guid flowId, Guid nodeId);

    #endregion

    #region Edge Operations

    /// <summary>
    /// Add edge to flow
    /// </summary>
    Task<FlowEdge> AddEdgeAsync(Guid flowId, FlowEdge edge);

    /// <summary>
    /// Update edge in flow
    /// </summary>
    Task<FlowEdge> UpdateEdgeAsync(Guid flowId, FlowEdge edge);

    /// <summary>
    /// Remove edge from flow
    /// </summary>
    Task<bool> RemoveEdgeAsync(Guid flowId, Guid edgeId);

    #endregion

    #region Flow Execution

    /// <summary>
    /// Execute a flow
    /// </summary>
    Task<FlowExecutionResult> ExecuteFlowAsync(Guid flowId, Dictionary<string, object>? inputs = null);

    /// <summary>
    /// Get execution status
    /// </summary>
    Task<Domain.FlowExecutionStatus> GetExecutionStatusAsync(Guid executionId);

    /// <summary>
    /// Cancel flow execution
    /// </summary>
    Task<bool> CancelExecutionAsync(Guid executionId);

    /// <summary>
    /// Subscribe to execution updates
    /// </summary>
    Task SubscribeToExecutionUpdatesAsync(Guid executionId, Action<FlowExecutionUpdate> onUpdate);

    #endregion

    #region Flow Validation

    /// <summary>
    /// Validate flow structure and configuration
    /// </summary>
    Task<FlowValidationResult> ValidateFlowAsync(Guid flowId);

    /// <summary>
    /// Validate flow data before saving
    /// </summary>
    Task<FlowValidationResult> ValidateFlowDataAsync(PromptFlow flow);

    #endregion

    #region Flow Analytics

    /// <summary>
    /// Get flow execution history
    /// </summary>
    Task<IEnumerable<FlowExecution>> GetExecutionHistoryAsync(Guid flowId, int pageSize = 50, int pageNumber = 1);

    /// <summary>
    /// Get flow performance metrics
    /// </summary>
    Task<FlowMetrics> GetFlowMetricsAsync(Guid flowId, DateTime? startDate = null, DateTime? endDate = null);

    /// <summary>
    /// Get flow performance summary
    /// </summary>
    Task<FlowPerformanceMetrics> GetFlowPerformanceAsync(Guid flowId);

    /// <summary>
    /// Get flow statistics
    /// </summary>
    Task<FlowStatistics> GetFlowStatisticsAsync(Guid flowId);

    #endregion

    #region Flow Templates

    /// <summary>
    /// Create flow from template
    /// </summary>
    Task<PromptFlow> CreateFlowFromTemplateAsync(Guid templateId, string flowName, string? userId = null);

    /// <summary>
    /// Save flow as template
    /// </summary>
    Task<PromptFlow> SaveAsTemplateAsync(Guid flowId, string templateName);

    #endregion

    #region Flow Import/Export

    /// <summary>
    /// Export flow data
    /// </summary>
    Task<string> ExportFlowAsync(Guid flowId);

    /// <summary>
    /// Import flow data
    /// </summary>
    Task<PromptFlow> ImportFlowAsync(string flowData, string flowName, string? userId = null);

    #endregion
}
