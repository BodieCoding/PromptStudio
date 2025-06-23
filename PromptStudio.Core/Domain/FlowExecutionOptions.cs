namespace PromptStudio.Core.Domain;

/// <summary>
/// Options for flow execution
/// </summary>
public class FlowExecutionOptions
{
    /// <summary>
    /// Execution timeout in milliseconds
    /// </summary>
    public int? Timeout { get; set; }

    /// <summary>
    /// Whether to run in debug mode with detailed trace
    /// </summary> 
    public bool Debug { get; set; } = false;

    /// <summary>
    /// Whether to only validate the flow without executing
    /// </summary>
    public bool ValidateOnly { get; set; } = false;
}
