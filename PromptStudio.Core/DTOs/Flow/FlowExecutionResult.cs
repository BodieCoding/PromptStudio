namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents the complete result of a flow execution, including success status, outputs, and detailed execution information.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Primary result DTO for flow execution services, providing comprehensive execution results for workflow engines,
/// monitoring systems, and client applications. Essential for tracking execution success, debugging failures, and performance analysis.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Complete execution result package with success indicators, outputs, timing, and detailed node execution traces.
/// Designed for efficient transfer of execution results while maintaining detailed audit trail capabilities.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Workflow execution result processing and response handling</item>
/// <item>Execution monitoring and performance analysis</item>
/// <item>Error handling and debugging workflow failures</item>
/// <item>Audit trail generation and compliance reporting</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Contains nested node execution collections that may grow large for complex flows.
/// Object output should be serialized efficiently. Consider truncating node execution details for high-frequency scenarios
/// where detailed tracing is not required.</para>
/// </remarks>
public class FlowExecutionResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the flow execution completed successfully.
    /// </summary>
    /// <value>True if the execution completed without errors; otherwise, false.</value>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for this execution instance.
    /// </summary>
    /// <value>A GUID that uniquely identifies this execution for tracking and correlation purposes.</value>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Gets or sets the final output produced by the flow execution.
    /// </summary>
    /// <value>The final result object from the flow, or null if no output was produced or execution failed.</value>
    public object? Output { get; set; }

    /// <summary>
    /// Gets or sets the total execution time in milliseconds.
    /// </summary>
    /// <value>A positive integer representing the total time taken to execute the flow.</value>
    public long ExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the error message if the execution failed.
    /// </summary>
    /// <value>A descriptive error message explaining the failure, or null if execution was successful.</value>
    public string? Error { get; set; }

    /// <summary>
    /// Gets or sets the collection of individual node execution details.
    /// </summary>
    /// <value>A list of node execution information providing detailed execution trace for debugging and monitoring.</value>
    public List<NodeExecutionInfo> NodeExecutions { get; set; } = [];
}
