using PromptStudio.Core.Domain;
using PromptStudio.Core.Attributes;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Primary service interface for coordinating prompt operations across specialized services
/// This acts as a facade/coordinator for the underlying specialized services
/// </summary>
public interface IPromptService
{
    #region Legacy Collection Support (Temporary - will be removed after migration)

    /// <summary>
    /// Get all collections (Legacy - will be replaced with PromptLibrary operations)
    /// </summary>
    /// <returns>List of all collections with their prompt templates</returns>
    [McpExposed(Name = "GetCollections", Description = "Get all prompt collections")]
    Task<List<Collection>> GetCollectionsAsync();

    /// <summary>
    /// Get a collection by ID (Legacy - will be replaced with PromptLibrary operations)
    /// </summary>
    /// <param name="id">Collection ID</param>
    /// <returns>Collection with prompt templates, or null if not found</returns>
    [McpExposed(Name = "GetCollection", Description = "Get collection details with prompts")]
    Task<Collection?> GetCollectionByIdAsync(int id);

    /// <summary>
    /// Create a new collection (Legacy - will be replaced with PromptLibrary operations)
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="description">Optional collection description</param>
    /// <returns>Created collection</returns>
    [McpExposed(Name = "CreateCollection", Description = "Create a new prompt collection")]
    Task<Collection> CreateCollectionAsync(string name, string? description = null);

    /// <summary>
    /// Import a collection from JSON (Legacy - will be replaced with PromptLibrary operations)
    /// </summary>
    /// <param name="jsonContent">JSON content representing the collection</param>
    /// <param name="importExecutionHistory">Whether to import execution history</param>
    /// <param name="overwriteExisting">Whether to overwrite existing collection</param>
    /// <returns>Imported collection</returns>
    Task<Collection?> ImportCollectionFromJsonAsync(string jsonContent, bool importExecutionHistory, bool overwriteExisting);

    #endregion

    #region MCP Service Facade (delegates to specialized services)

    /// <summary>
    /// List all collections with their prompt count (MCP Facade)
    /// </summary>
    /// <returns>List of all collections with their prompt templates</returns>
    [McpExposed(Name = "ListCollections", Description = "List all collections with their prompt count")]
    Task<List<Collection>> ListCollectionsAsync();

    /// <summary>
    /// List all prompt templates, optionally filtered by collection (MCP Facade)
    /// </summary>
    /// <param name="collectionId">Optional collection ID to filter by</param>
    /// <returns>List of prompt templates</returns>
    [McpExposed(Name = "ListPrompts", Description = "List all prompt templates, optionally filtered by collection")]
    Task<List<PromptTemplate>> ListPromptsAsync(int? collectionId = null);

    /// <summary>
    /// Get a specific prompt template (MCP Facade)
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <returns>Prompt template, or null if not found</returns>
    [McpExposed(Name = "GetPrompt", Description = "Get a specific prompt template")]
    Task<PromptTemplate?> GetPromptAsync(int id);

    /// <summary>
    /// Execute a prompt template with provided variables (MCP Facade)
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <param name="variables">Variables as JSON string</param>
    /// <returns>Execution result with resolved prompt</returns>
    [McpExposed(Name = "ExecutePrompt", Description = "Execute a prompt template with variables", WrapInEnvelope = false)]
    Task<object> ExecutePromptTemplateAsync(int id, string variables);

    /// <summary>
    /// Get execution history for prompts (MCP Facade)
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <returns>List of prompt executions</returns>
    [McpExposed(Name = "GetExecutionHistory", Description = "Get prompt execution history")]
    Task<List<PromptExecution>> GetExecutionHistoryAsync(int? promptId = null, int limit = 50);

    /// <summary>
    /// Execute batch processing with a variable collection (MCP Facade)
    /// </summary>
    /// <param name="collectionId">Variable collection ID</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>Batch execution result</returns>
    [McpExposed(Name = "ExecuteBatch", Description = "Execute prompt template with variable collection", WrapInEnvelope = false)]
    Task<object> ExecuteBatchAsync(int collectionId, int promptId);

    /// <summary>
    /// Generate a CSV template for a prompt template (MCP Facade)
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <returns>CSV template content</returns>
    [McpExposed(Name = "GenerateCsvTemplate", Description = "Generate a CSV template for a prompt template")]
    Task<string> GenerateCsvTemplateAsync(int templateId);

    /// <summary>
    /// List variable collections for a prompt template (MCP Facade)
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>List of variable collections</returns>
    [McpExposed(Name = "ListVariableCollections", Description = "List variable collections for a prompt template")]
    Task<List<VariableCollection>> ListVariableCollectionsAsync(int promptId);

    #endregion
}