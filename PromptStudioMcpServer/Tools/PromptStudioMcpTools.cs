using Microsoft.Extensions.Logging;
using PromptStudio.Core.Interfaces;
using PromptStudio.Core.Attributes;
using System.ComponentModel;
using System.Reflection;
using ModelContextProtocol.Server;

namespace PromptStudioMcpServer.Tools;

[McpServerToolType]
public class PromptStudioMcpTools
{
    private readonly IPromptService _promptService;
    private readonly ILogger<PromptStudioMcpTools> _logger;

    public PromptStudioMcpTools(IPromptService promptService, ILogger<PromptStudioMcpTools> logger)
    {
        _promptService = promptService ?? throw new ArgumentNullException(nameof(promptService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private async Task<object> InvokeExposedMethodAsync(string methodName, params object[] parameters)
    {
        try
        {
            var method = typeof(IPromptService)
                .GetMethods()
                .FirstOrDefault(m =>
                {
                    var attr = m.GetCustomAttribute<McpExposedAttribute>();
                    return attr != null && (attr.Name == methodName || m.Name == methodName);
                });
            if (method == null)
            {
                return new { success = false, error = $"Method '{methodName}' not found or not exposed to MCP" };
            }
            var attribute = method.GetCustomAttribute<McpExposedAttribute>();
            _logger.LogInformation("Invoking {MethodName} with {ParameterCount} parameters", methodName, parameters.Length);
            var result = method.Invoke(_promptService, parameters);
            if (result is Task task)
            {
                await task;
                if (task.GetType().IsGenericType)
                {
                    var resultProperty = task.GetType()
                                             .GetProperty("Result");
                    var actualResult = resultProperty?.GetValue(task);
                    return attribute?.WrapInEnvelope == true
                        ? new { success = true, data = actualResult }
                        : actualResult ?? new { };
                }
                return attribute?.WrapInEnvelope == true
                    ? new { success = true }
                    : new { };
            }
            return attribute?.WrapInEnvelope == true
                ? new { success = true, data = result }
                : result ?? new { };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error invoking method {MethodName}", methodName);
            var innerEx = ex;
            while (innerEx.InnerException != null)
            {
                innerEx = innerEx.InnerException;
            }
            return new { success = false, error = innerEx.Message, exceptionType = innerEx.GetType().Name, stackTrace = innerEx.StackTrace };
        }
    }

    [McpServerTool, Description("Get all prompt collections")]
    public async Task<object> GetCollections()
        => await InvokeExposedMethodAsync("GetCollections");

    [McpServerTool, Description("Get collection details with prompts")]
    public async Task<object> GetCollection(
        [Description("Collection ID")] int id)
        => await InvokeExposedMethodAsync("GetCollection", id);

    [McpServerTool, Description("List prompt templates")]
    public async Task<object> GetPromptTemplates(
        [Description("Optional collection ID to filter by")] int? collectionId = null)
        => await InvokeExposedMethodAsync("GetPromptTemplates", collectionId!);

    [McpServerTool, Description("Get prompt template details")]
    public async Task<object> GetPromptTemplate(
        [Description("Prompt template ID")] int id)
        => await InvokeExposedMethodAsync("GetPromptTemplate", id);

    [McpServerTool, Description("Execute a prompt template with variables")]
    public async Task<object> ExecutePrompt(
        [Description("Prompt template ID")] int id,
        [Description("Variables as JSON object")] string variables)
        => await InvokeExposedMethodAsync("ExecutePrompt", id, variables);

    [McpServerTool, Description("Get prompt execution history")]
    public async Task<object> GetExecutionHistory(
        [Description("Optional prompt ID to filter by")] int? promptId = null,
        [Description("Maximum number of executions to return")] int limit = 50)
        => await InvokeExposedMethodAsync("GetExecutionHistory", promptId!, limit);

    [McpServerTool, Description("Create a new prompt collection")]
    public async Task<object> CreateCollection(
        [Description("Collection name")] string name,
        [Description("Collection description")] string? description = null)
        => await InvokeExposedMethodAsync("CreateCollection", name, description!);

    [McpServerTool, Description("Create a new prompt template")]
    public async Task<object> CreatePromptTemplate(
        [Description("Prompt template name")] string name,
        [Description("Prompt template content")] string content,
        [Description("Collection ID")] int collectionId,
        [Description("Prompt template description")] string? description = null)
        => await InvokeExposedMethodAsync("CreatePromptTemplate", name, content, collectionId, description!);

    [McpServerTool, Description("List variable collections for a prompt")]
    public async Task<object> GetVariableCollections(
        [Description("Prompt template ID")] int promptId)
        => await InvokeExposedMethodAsync("GetVariableCollections", promptId);

    [McpServerTool, Description("Create a variable collection from CSV data")]
    public async Task<object> CreateVariableCollection(
        [Description("Collection name")] string name,
        [Description("Prompt template ID")] int promptId,
        [Description("CSV data with variables")] string csvData,
        [Description("Collection description")] string? description = null)
        => await InvokeExposedMethodAsync("CreateVariableCollection", name, promptId, csvData, description ?? (object)DBNull.Value);

    [McpServerTool, Description("Execute prompt template with variable collection")]
    public async Task<object> ExecuteBatch(
        [Description("Variable collection ID")] int collectionId,
        [Description("Prompt template ID")] int promptId)
        => await InvokeExposedMethodAsync("ExecuteBatch", collectionId, promptId);

    [McpServerTool, Description("List all collections with their prompt count")]
    public async Task<object> ListCollections() =>
        await InvokeExposedMethodAsync("ListCollections");

    [McpServerTool, Description("List all prompt templates, optionally filtered by collection")]
    public Task<object> ListPrompts(
        [Description("Collection ID")]int? collectionId = null)=>
        InvokeExposedMethodAsync("ListPrompts", collectionId ?? (object)DBNull.Value);

    [McpServerTool, Description("Get a specific prompt template")]
    public Task<object> GetPrompt(
        [Description("Prompt template ID")] int id) =>
        InvokeExposedMethodAsync("GetPrompt", id);

    [McpServerTool, Description(" List variable collections for a prompt template")]
    public Task<object> ListVariableCollections(
        [Description("Prompt template ID")] int promptId) =>
        InvokeExposedMethodAsync("ListVariableCollections", promptId);

    [McpServerTool, Description("Generate a CSV template for a prompt template")]
    public Task<object> GenerateCsvTemplate(
        [Description("Prompt template ID")] int templateId) =>
        InvokeExposedMethodAsync("GenerateCsvTemplate", templateId);
}
