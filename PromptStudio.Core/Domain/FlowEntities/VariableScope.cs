namespace PromptStudio.Core.Domain;

/// <summary>
/// Enumeration of variable scope levels within flow execution.
/// </summary>
public enum VariableScope
{
    /// <summary>
    /// Local scope - variable is only accessible within the current node.
    /// </summary>
    Local,

    /// <summary>
    /// Flow scope - variable is accessible throughout the current flow execution.
    /// </summary>
    Flow,

    /// <summary>
    /// Global scope - variable is accessible across all flows and executions.
    /// </summary>
    Global,

    /// <summary>
    /// Input scope - variable is provided as input to the flow.
    /// </summary>
    Input,

    /// <summary>
    /// Output scope - variable represents output from the flow.
    /// </summary>
    Output
}
