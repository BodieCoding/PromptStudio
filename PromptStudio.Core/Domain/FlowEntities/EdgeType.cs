namespace PromptStudio.Core.Domain;

/// <summary>
/// Types of edges in workflow connections
/// </summary>
public enum EdgeType
{
    /// <summary>
    /// Standard data flow connection
    /// </summary>
    Normal = 0,
    
    /// <summary>
    /// Conditional branch connection
    /// </summary>
    Conditional = 1,
    
    /// <summary>
    /// Loop back connection
    /// </summary>
    Loop = 2,
    
    /// <summary>
    /// Error handling connection
    /// </summary>
    ErrorHandler = 3,
    
    /// <summary>
    /// Parallel execution connection
    /// </summary>
    Parallel = 4,
    
    /// <summary>
    /// Synchronization point connection
    /// </summary>
    Synchronize = 5,
    
    /// <summary>
    /// Event-driven connection
    /// </summary>
    Event = 6,
    
    /// <summary>
    /// Timeout or time-based connection
    /// </summary>
    Timeout = 7
}
