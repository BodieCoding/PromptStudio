using Microsoft.Extensions.Logging;
using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Attributes;
using System.ComponentModel;
using System.Reflection;
using ModelContextProtocol.Server;

namespace PromptStudio.Mcp.Tools;

/// <summary>
/// Dynamic MCP tools that automatically expose IPromptService methods marked with [McpExposed]
/// </summary>
[McpServerToolType]
public class DynamicMcpTools
{
    private readonly IPromptService _promptService;
    private readonly ILogger<DynamicMcpTools> _logger;

    public DynamicMcpTools(IPromptService promptService, ILogger<DynamicMcpTools> logger)
    {
        _promptService = promptService ?? throw new ArgumentNullException(nameof(promptService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // Automatically generate methods at runtime by scanning for [McpExposed] attributes
    private async Task<object> InvokeExposedMethodAsync(string methodName, params object[] parameters)
    {
        try
        {
            var method = GetExposedMethod(methodName);
            if (method == null)
            {
                return new { success = false, error = $"Method '{methodName}' not found or not exposed to MCP" };
            }

            var attribute = method.GetCustomAttribute<McpExposedAttribute>();
            _logger.LogInformation("Invoking {MethodName} with {ParameterCount} parameters", methodName, parameters.Length);

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
        }        catch (Exception ex)
        {
            _logger.LogError(ex, "Error invoking method {MethodName}", methodName);
            
            // Get the innermost exception for better debugging
            var innerEx = ex;
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }
            
            return new { success = false, error = innerEx.Message, exceptionType = innerEx.GetType().Name, stackTrace = innerEx.StackTrace };
        }
    }

    private MethodInfo? GetExposedMethod(string methodName)
    {
        return typeof(IPromptService)
            .GetMethods()
            .FirstOrDefault(m =>
            {
                var attr = m.GetCustomAttribute<McpExposedAttribute>();
                return attr != null && (attr.Name == methodName || m.Name == methodName);
            });
    }

    // Static MCP tool methods that delegate to the dynamic invoker

    [McpServerTool, Description("Get all prompt collections")]
    public async Task<object> GetCollections()
    {
        return await InvokeExposedMethodAsync("GetCollections");
    }

    [McpServerTool, Description("Get collection details with prompts")]
    public async Task<object> GetCollection(
        [Description("Collection ID")] int id)
    {
        return await InvokeExposedMethodAsync("GetCollection", id);
    }
    [McpServerTool, Description("List prompt templates")]
    public async Task<object> GetPromptTemplates(
        [Description("Optional collection ID to filter by")] int? collectionId = null)
    {
        return await InvokeExposedMethodAsync("GetPromptTemplates", collectionId!);
    }

    [McpServerTool, Description("Get prompt template details")]
    public async Task<object> GetPromptTemplate(
        [Description("Prompt template ID")] int id)
    {
        return await InvokeExposedMethodAsync("GetPromptTemplate", id);
    }

    [McpServerTool, Description("Execute a prompt template with variables")]
    public async Task<object> ExecutePrompt(
        [Description("Prompt template ID")] int id,
        [Description("Variables as JSON object")] string variables)
    {
        return await InvokeExposedMethodAsync("ExecutePrompt", id, variables);
    }
    [McpServerTool, Description("Get prompt execution history")]
    public async Task<object> GetExecutionHistory(
        [Description("Optional prompt ID to filter by")] int? promptId = null,
        [Description("Maximum number of executions to return")] int limit = 50)
    {
        return await InvokeExposedMethodAsync("GetExecutionHistory", promptId!, limit);
    }
    [McpServerTool, Description("Create a new prompt collection")]
    public async Task<object> CreateCollection(
        [Description("Collection name")] string name,
        [Description("Collection description")] string? description = null)
    {
        return await InvokeExposedMethodAsync("CreateCollection", name, description!);
    }
    [McpServerTool, Description("Create a new prompt template")]
    public async Task<object> CreatePromptTemplate(
        [Description("Prompt template name")] string name,
        [Description("Prompt template content")] string content,
        [Description("Collection ID")] int collectionId,
        [Description("Prompt template description")] string? description = null)
    {
        return await InvokeExposedMethodAsync("CreatePromptTemplate", name, content, collectionId, description!);
    }

    [McpServerTool, Description("List variable collections for a prompt")]
    public async Task<object> GetVariableCollections(
        [Description("Prompt template ID")] int promptId)
    {
        return await InvokeExposedMethodAsync("GetVariableCollections", promptId);
    }
    [McpServerTool, Description("Create a variable collection from CSV data")]
    public async Task<object> CreateVariableCollection(
        [Description("Collection name")] string name,
        [Description("Prompt template ID")] int promptId,
        [Description("CSV data with variables")] string csvData,
        [Description("Collection description")] string? description = null)
    {
        return await InvokeExposedMethodAsync("CreateVariableCollection", name, promptId, csvData, description!);
    }

    [McpServerTool, Description("Execute prompt template with variable collection")]
    public async Task<object> ExecuteBatch(
        [Description("Variable collection ID")] int collectionId,
        [Description("Prompt template ID")] int promptId)
    {
        return await InvokeExposedMethodAsync("ExecuteBatch", collectionId, promptId);
    }
}
