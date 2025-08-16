namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents a checkpoint during flow execution for debugging and monitoring.
/// Captures the state of execution at specific points for analysis and recovery.
/// </summary>
/// <remarks>
/// <para><strong>Checkpoint Functionality:</strong></para>
/// <para>Provides state snapshots during flow execution for debugging, monitoring,
/// and potential recovery operations. Essential for understanding execution flow,
/// performance analysis, and troubleshooting complex workflow scenarios.</para>
/// </remarks>
public class ExecutionCheckpoint
{
    /// <summary>
    /// Gets or sets the unique identifier for this checkpoint instance.
    /// Enables correlation across monitoring and debugging systems.
    /// </summary>
    /// <value>
    /// A unique identifier for this checkpoint occurrence.
    /// </value>
    public Guid CheckpointId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the name or identifier of this checkpoint.
    /// Provides human-readable identification for the checkpoint location.
    /// </summary>
    /// <value>
    /// A descriptive name for this checkpoint (e.g., "BeforeAPICall", "AfterValidation").
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of what this checkpoint represents.
    /// Provides context about the execution state at this point.
    /// </summary>
    /// <value>
    /// A description of the checkpoint's purpose and context.
    /// </value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this checkpoint was reached.
    /// Enables temporal analysis of execution flow and performance.
    /// </summary>
    /// <value>
    /// The UTC timestamp when this checkpoint was recorded.
    /// </value>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the identifier of the node where this checkpoint occurred.
    /// Provides context for checkpoint location within the flow.
    /// </summary>
    /// <value>
    /// The unique identifier of the flow node at this checkpoint.
    /// </value>
    public Guid? NodeId { get; set; }

    /// <summary>
    /// Gets or sets the name of the node where this checkpoint occurred.
    /// Provides human-readable context for checkpoint location.
    /// </summary>
    /// <value>
    /// The name of the flow node at this checkpoint.
    /// </value>
    public string? NodeName { get; set; }

    /// <summary>
    /// Gets or sets the execution step or phase when this checkpoint was reached.
    /// Indicates the logical position in the execution sequence.
    /// </summary>
    /// <value>
    /// The execution step number or phase identifier.
    /// </value>
    public int Step { get; set; }

    /// <summary>
    /// Gets or sets the current execution status at this checkpoint.
    /// Indicates the state of execution when the checkpoint was recorded.
    /// </summary>
    /// <value>
    /// The execution status at checkpoint time.
    /// </value>
    public FlowExecutionStatus Status { get; set; } = FlowExecutionStatus.Running;

    /// <summary>
    /// Gets or sets the elapsed time since execution began.
    /// Provides timing information for performance analysis.
    /// </summary>
    /// <value>
    /// The time elapsed from execution start to this checkpoint.
    /// </value>
    public TimeSpan ElapsedTime { get; set; }

    /// <summary>
    /// Gets or sets the variables and their values at this checkpoint.
    /// Captures the variable state for debugging and analysis.
    /// </summary>
    /// <value>
    /// A dictionary of variable names and their values at checkpoint time.
    /// </value>
    public Dictionary<string, object> Variables { get; set; } = [];

    /// <summary>
    /// Gets or sets the execution context information at this checkpoint.
    /// Provides environmental and configuration data.
    /// </summary>
    /// <value>
    /// A dictionary containing execution context information.
    /// </value>
    public Dictionary<string, object> Context { get; set; } = [];

    /// <summary>
    /// Gets or sets additional metadata about this checkpoint.
    /// Provides extensible storage for checkpoint-specific information.
    /// </summary>
    /// <value>
    /// A dictionary containing additional checkpoint metadata.
    /// </value>
    public Dictionary<string, object> Metadata { get; set; } = [];

    /// <summary>
    /// Gets or sets the memory usage at this checkpoint.
    /// Tracks resource consumption for performance monitoring.
    /// </summary>
    /// <value>
    /// Memory usage in bytes at checkpoint time, if available.
    /// </value>
    public long? MemoryUsage { get; set; }

    /// <summary>
    /// Gets or sets the CPU usage percentage at this checkpoint.
    /// Tracks processor utilization for performance monitoring.
    /// </summary>
    /// <value>
    /// CPU usage percentage at checkpoint time, if available.
    /// </value>
    public double? CpuUsage { get; set; }

    /// <summary>
    /// Gets or sets any warnings collected up to this checkpoint.
    /// Provides insight into non-critical issues encountered.
    /// </summary>
    /// <value>
    /// A list of warnings that occurred before this checkpoint.
    /// </value>
    public List<ExecutionWarning> Warnings { get; set; } = [];

    /// <summary>
    /// Gets or sets the type or category of this checkpoint.
    /// Groups checkpoints by purpose for filtering and analysis.
    /// </summary>
    /// <value>
    /// The checkpoint type for organizational purposes.
    /// </value>
    public CheckpointType Type { get; set; } = CheckpointType.Manual;

    /// <summary>
    /// Gets or sets the data or output generated at this checkpoint.
    /// Captures intermediate results for analysis.
    /// </summary>
    /// <value>
    /// Data or output produced at this checkpoint, if any.
    /// </value>
    public object? Data { get; set; }

    /// <summary>
    /// Gets or sets whether this checkpoint can be used for execution recovery.
    /// Indicates if execution can resume from this point.
    /// </summary>
    /// <value>
    /// True if execution can be resumed from this checkpoint, false otherwise.
    /// </value>
    public bool IsRecoverable { get; set; }

    /// <summary>
    /// Initializes a new instance of the ExecutionCheckpoint class with default values.
    /// </summary>
    public ExecutionCheckpoint()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ExecutionCheckpoint class with a name.
    /// </summary>
    /// <param name="name">The checkpoint name</param>
    public ExecutionCheckpoint(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the ExecutionCheckpoint class with name and step.
    /// </summary>
    /// <param name="name">The checkpoint name</param>
    /// <param name="step">The execution step number</param>
    public ExecutionCheckpoint(string name, int step)
    {
        Name = name;
        Step = step;
    }

    /// <summary>
    /// Adds a variable value to this checkpoint.
    /// </summary>
    /// <param name="name">The variable name</param>
    /// <param name="value">The variable value</param>
    /// <returns>This checkpoint instance for method chaining</returns>
    public ExecutionCheckpoint AddVariable(string name, object value)
    {
        Variables[name] = value;
        return this;
    }

    /// <summary>
    /// Adds context information to this checkpoint.
    /// </summary>
    /// <param name="key">The context key</param>
    /// <param name="value">The context value</param>
    /// <returns>This checkpoint instance for method chaining</returns>
    public ExecutionCheckpoint AddContext(string key, object value)
    {
        Context[key] = value;
        return this;
    }

    /// <summary>
    /// Adds metadata to this checkpoint.
    /// </summary>
    /// <param name="key">The metadata key</param>
    /// <param name="value">The metadata value</param>
    /// <returns>This checkpoint instance for method chaining</returns>
    public ExecutionCheckpoint AddMetadata(string key, object value)
    {
        Metadata[key] = value;
        return this;
    }

    /// <summary>
    /// Returns a string representation of the execution checkpoint.
    /// </summary>
    /// <returns>String representation including checkpoint name and timing</returns>
    public override string ToString()
    {
        var nodeContext = !string.IsNullOrEmpty(NodeName) ? $" in {NodeName}" : "";
        var stepContext = Step > 0 ? $" (Step {Step})" : "";
        return $"Checkpoint '{Name}'{nodeContext}{stepContext} at {Timestamp:HH:mm:ss.fff}";
    }
}

/// <summary>
/// Enumeration of checkpoint types for categorization.
/// </summary>
public enum CheckpointType
{
    /// <summary>
    /// Manually created checkpoint for specific debugging or analysis.
    /// </summary>
    Manual = 0,

    /// <summary>
    /// Automatic checkpoint created at key execution points.
    /// </summary>
    Automatic = 1,

    /// <summary>
    /// Checkpoint created before starting a node execution.
    /// </summary>
    BeforeNode = 2,

    /// <summary>
    /// Checkpoint created after completing a node execution.
    /// </summary>
    AfterNode = 3,

    /// <summary>
    /// Checkpoint created before calling external APIs or services.
    /// </summary>
    BeforeAPI = 4,

    /// <summary>
    /// Checkpoint created after receiving API or service responses.
    /// </summary>
    AfterAPI = 5,

    /// <summary>
    /// Checkpoint created during error handling or recovery.
    /// </summary>
    Error = 6,

    /// <summary>
    /// Checkpoint created for performance monitoring and analysis.
    /// </summary>
    Performance = 7,

    /// <summary>
    /// Checkpoint created at decision points or branching logic.
    /// </summary>
    Decision = 8,

    /// <summary>
    /// Checkpoint created at loop iterations or repetitive operations.
    /// </summary>
    Loop = 9
}
