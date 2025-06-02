using System.Reflection;
using PromptStudio.Core.Attributes;
using PromptStudio.Core.Interfaces;

namespace PromptStudio.Core.Services;

/// <summary>
/// Dynamic proxy that wraps IPromptService methods to provide MCP-compatible returns
/// </summary>
public class McpPromptServiceProxy
{
    private readonly IPromptService _promptService;
    
    public McpPromptServiceProxy(IPromptService promptService)
    {
        _promptService = promptService ?? throw new ArgumentNullException(nameof(promptService));
    }

    /// <summary>
    /// Dynamically invoke a method and return it as Task<object>
    /// </summary>
    public async Task<object> InvokeAsync(string methodName, params object[] parameters)
    {
        try
        {
            var method = GetMcpExposedMethod(methodName);
            if (method == null)
            {
                return new { success = false, error = $"Method '{methodName}' not found or not exposed to MCP" };
            }

            var attribute = method.GetCustomAttribute<McpExposedAttribute>();
            var result = method.Invoke(_promptService, parameters);

            // Handle async methods
            if (result is Task task)
            {
                await task;
                
                // Get the result from Task<T>
                if (task.GetType().IsGenericType)
                {
                    var resultProperty = task.GetType().GetProperty("Result");
                    var actualResult = resultProperty?.GetValue(task);
                    
                    return attribute?.WrapInEnvelope == true 
                        ? new { success = true, data = actualResult }
                        : actualResult ?? new { };
                }
                
                return attribute?.WrapInEnvelope == true 
                    ? new { success = true }
                    : new { };
            }

            // Handle synchronous methods
            return attribute?.WrapInEnvelope == true 
                ? new { success = true, data = result }
                : result ?? new { };
        }
        catch (Exception ex)
        {
            return new { success = false, error = ex.Message };
        }
    }

    /// <summary>
    /// Get all methods exposed to MCP
    /// </summary>
    public IEnumerable<(string Name, string Description, MethodInfo Method)> GetExposedMethods()
    {
        return typeof(IPromptService)
            .GetMethods()
            .Where(m => m.GetCustomAttribute<McpExposedAttribute>() != null)
            .Select(m =>
            {
                var attr = m.GetCustomAttribute<McpExposedAttribute>()!;
                return (
                    Name: attr.Name ?? m.Name,
                    Description: attr.Description ?? $"Executes {m.Name}",
                    Method: m
                );
            });
    }

    private MethodInfo? GetMcpExposedMethod(string methodName)
    {
        return typeof(IPromptService)
            .GetMethods()
            .FirstOrDefault(m =>
            {
                var attr = m.GetCustomAttribute<McpExposedAttribute>();
                return attr != null && (attr.Name == methodName || m.Name == methodName);
            });
    }
}
