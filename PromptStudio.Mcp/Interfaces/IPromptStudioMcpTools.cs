namespace PromptStudio.Mcp.Interfaces;

/// <summary>
/// Interface for MCP tools operations
/// </summary>
public interface IPromptStudioMcpTools
{
    /// <summary>
    /// List all collections with their prompt count
    /// </summary>
    Task<object> ListCollections();

    /// <summary>
    /// Get a specific collection with its prompts
    /// </summary>
    Task<object> GetCollection(int id);

    /// <summary>
    /// List all prompt templates, optionally filtered by collection
    /// </summary>
    Task<object> ListPrompts(int? collectionId = null);

    /// <summary>
    /// Get a specific prompt template
    /// </summary>
    Task<object> GetPrompt(int id);

    /// <summary>
    /// Execute a prompt template with provided variables
    /// </summary>
    Task<object> ExecutePrompt(int id, string variables);

    /// <summary>
    /// Get execution history for prompts
    /// </summary>
    Task<object> GetExecutionHistory(int? promptId = null, int limit = 50);

    /// <summary>
    /// Create a new collection
    /// </summary>
    Task<object> CreateCollection(string name, string? description = null);

    /// <summary>
    /// Create a new prompt template
    /// </summary>
    Task<object> CreatePromptTemplate(string name, string content, int collectionId, string? description = null);

    /// <summary>
    /// List variable collections for a prompt template
    /// </summary>
    Task<object> ListVariableCollections(int promptId);

    /// <summary>
    /// Create a variable collection from CSV data
    /// </summary>
    Task<object> CreateVariableCollection(string name, int promptId, string csvData, string? description = null);

    /// <summary>
    /// Execute batch processing with a variable collection
    /// </summary>
    Task<object> ExecuteBatch(int collectionId, int promptId);

    /// <summary>
    /// Generate a CSV template for a prompt template
    /// </summary>
    Task<object> GenerateCsvTemplate(int templateId);
}
