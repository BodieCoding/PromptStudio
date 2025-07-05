namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the types of connections between workflow nodes, enabling sophisticated control flow and execution patterns.
/// 
/// <para><strong>Business Context:</strong></para>
/// Edge types enable the creation of complex business process patterns including conditional routing,
/// parallel processing, error handling, and event-driven workflows. These connection types provide
/// the foundation for building robust, scalable workflow orchestrations that can handle real-world
/// business complexity and operational requirements.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Each edge type implements specific execution semantics and data flow patterns within the workflow
/// runtime engine. Edge types determine how data flows between nodes, when execution occurs, and how
/// different execution paths are coordinated to ensure reliable and predictable workflow behavior.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Sophisticated workflow control flow patterns
/// - Reliable error handling and recovery mechanisms
/// - Scalable parallel and concurrent execution
/// - Event-driven and reactive workflow capabilities
/// </summary>
/// <remarks>
/// <para><strong>Edge Categories:</strong></para>
/// - Data Flow: Normal connections for standard processing flow
/// - Control Flow: Conditional, Loop for decision-making and iteration
/// - Concurrency: Parallel, Synchronize for concurrent execution patterns
/// - Error Management: ErrorHandler for failure recovery
/// - Event Processing: Event, Timeout for reactive patterns
/// 
/// <para><strong>Execution Semantics:</strong></para>
/// - Normal: Sequential data flow with immediate execution
/// - Conditional: Execution based on evaluation of conditions
/// - Loop: Iterative execution with termination conditions
/// - Parallel: Concurrent execution of multiple paths
/// - ErrorHandler: Execution triggered by error conditions
/// - Event: Execution triggered by external events
/// - Timeout: Time-based execution control
/// 
/// <para><strong>Design Considerations:</strong></para>
/// Edge configuration should include appropriate metadata for execution control,
/// such as conditions for conditional edges, timeout values for timeout edges,
/// and synchronization requirements for parallel edges.
/// </remarks>
/// <example>
/// A conditional edge might route high-priority requests to an expedited processing path,
/// while an error handler edge provides fallback processing for failed operations.
/// </example>
public enum EdgeType
{
    /// <summary>
    /// Standard sequential data flow connection between nodes.
    /// Represents the default execution path where data flows directly from source to target
    /// node with immediate execution upon source completion.
    /// </summary>
    Normal = 0,
    
    /// <summary>
    /// Conditional execution connection based on evaluation of specified conditions.
    /// Enables dynamic workflow routing based on data content, AI analysis results,
    /// or business rule evaluation, supporting complex decision-making patterns.
    /// </summary>
    Conditional = 1,
    
    /// <summary>
    /// Iterative execution connection that creates loops for repeated processing.
    /// Supports collection processing, iterative refinement workflows, and recursive
    /// patterns with configurable termination conditions.
    /// </summary>
    Loop = 2,
    
    /// <summary>
    /// Error handling connection activated when source node encounters failures.
    /// Provides fault tolerance and recovery mechanisms by routing execution to
    /// error handling nodes when exceptions or failures occur.
    /// </summary>
    ErrorHandler = 3,
    
    /// <summary>
    /// Parallel execution connection for concurrent processing of multiple workflow paths.
    /// Enables simultaneous execution of independent operations to improve performance
    /// and support complex workflow patterns requiring concurrent processing.
    /// </summary>
    Parallel = 4,
    
    /// <summary>
    /// Synchronization connection that coordinates multiple parallel execution paths.
    /// Ensures proper coordination of concurrent workflows by waiting for multiple
    /// parallel paths to complete before proceeding to subsequent processing stages.
    /// </summary>
    Synchronize = 5,
    
    /// <summary>
    /// Event-driven connection triggered by external events or system notifications.
    /// Enables reactive workflow patterns that respond to external stimuli, system
    /// events, or asynchronous notifications for real-time processing capabilities.
    /// </summary>
    Event = 6,
    
    /// <summary>
    /// Time-based connection with configurable timeout and delay mechanisms.
    /// Supports scheduled execution, delayed processing, and timeout-based workflow
    /// control for time-sensitive operations and SLA management.
    /// </summary>
    Timeout = 7
}
