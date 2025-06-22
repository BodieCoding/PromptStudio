using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Interface for visual prompt flow operations
/// </summary>
public interface IFlowService
{
    /// <summary>
    /// Get all flows for a user with optional filtering
    /// </summary>
    Task<IEnumerable<PromptFlow>> GetFlowsAsync(string? userId = null, string? tag = null, string? search = null);

    /// <summary>
    /// Get a specific flow by ID
    /// </summary>
    Task<PromptFlow?> GetFlowByIdAsync(Guid flowId);

    /// <summary>
    /// Create a new prompt flow
    /// </summary>
    Task<PromptFlow> CreateFlowAsync(PromptFlow flow);

    /// <summary>
    /// Update an existing prompt flow
    /// </summary>
    Task<PromptFlow> UpdateFlowAsync(PromptFlow flow);

    /// <summary>
    /// Delete a prompt flow
    /// </summary>
    Task<bool> DeleteFlowAsync(Guid flowId);

    /// <summary>
    /// Execute a prompt flow with given variables
    /// </summary>
    Task<FlowExecutionResult> ExecuteFlowAsync(Guid flowId, Dictionary<string, object> variables, FlowExecutionOptions? options = null);

    /// <summary>
    /// Validate a prompt flow structure
    /// </summary>
    Task<FlowValidationResult> ValidateFlowAsync(PromptFlow flow);

    /// <summary>
    /// Get execution history for a flow
    /// </summary>
    Task<IEnumerable<FlowExecution>> GetExecutionHistoryAsync(Guid flowId, int limit = 50);
}
