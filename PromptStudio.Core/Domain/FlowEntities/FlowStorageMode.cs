namespace PromptStudio.Core.Domain;

/// <summary>
/// Storage mode for workflow data
/// </summary>
public enum FlowStorageMode
{
    /// <summary>
    /// Store only JSON data for simple flows and React Flow compatibility
    /// </summary>
    JsonOnly = 0,
    
    /// <summary>
    /// Store only relational data (nodes/edges as separate entities)
    /// </summary>
    Relational = 1,
    
    /// <summary>
    /// Store both JSON and relational data (recommended for enterprise)
    /// Provides React Flow compatibility with query capabilities
    /// </summary>
    Hybrid = 2
}
