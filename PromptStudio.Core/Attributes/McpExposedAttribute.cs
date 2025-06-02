using System;

namespace PromptStudio.Core.Attributes;

/// <summary>
/// Marks a method as exposed to MCP server with automatic object conversion
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class McpExposedAttribute : Attribute
{
    /// <summary>
    /// The name to expose in MCP (if different from method name)
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Description for MCP documentation
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Whether to wrap the result in a success/error envelope
    /// </summary>
    public bool WrapInEnvelope { get; set; } = true;
}
