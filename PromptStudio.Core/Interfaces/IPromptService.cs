using PromptStudio.Core.Domain;
using PromptStudio.Core.Attributes;

namespace PromptStudio.Core.Interfaces;

public interface IPromptService
{
    #region Prompt Resolution

    /// <summary>
    /// Resolves a prompt template by substituting variables with provided values
    /// </summary>
    /// <param name="template">The prompt template to resolve</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <returns>The resolved prompt with variables substituted</returns>
    string ResolvePrompt(PromptTemplate template, Dictionary<string, string> variableValues);

    /// <summary>
    /// Extracts variable names from a prompt template content
    /// </summary>
    /// <param name="promptContent">The content to extract variables from</param>
    /// <returns>List of variable names found in the content</returns>
    List<string> ExtractVariableNames(string promptContent);

    /// <summary>
    /// Validates that all required variables have values
    /// </summary>
    /// <param name="template">The prompt template to validate</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <returns>True if all required variables have values, false otherwise</returns>
    bool ValidateVariables(PromptTemplate template, Dictionary<string, string> variableValues);

    #endregion

    #region CSV / Batch Operations

    /// <summary>
    /// Generates a sample CSV template for a prompt's variables
    /// </summary>
    /// <param name="template">The prompt template to generate CSV for</param>
    /// <returns>CSV content with headers for all variables</returns>
    string GenerateVariableCsvTemplate(PromptTemplate template);

    /// <summary>
    /// Parses CSV content into variable sets
    /// </summary>
    /// <param name="csvContent">The CSV content to parse</param>
    /// <param name="expectedVariables">List of expected variable names</param>
    /// <returns>List of dictionaries containing variable values for each row</returns>
    List<Dictionary<string, string>> ParseVariableCsv(string csvContent, List<string> expectedVariables);

    /// <summary>
    /// Batch executes a prompt template against multiple variable sets
    /// </summary>
    /// <param name="template">The prompt template to execute</param>
    /// <param name="variableSets">List of variable sets to use</param>
    /// <returns>List of execution results with variables, resolved prompts, and any errors</returns>
    List<(Dictionary<string, string> Variables, string ResolvedPrompt, bool Success, string? Error)> BatchExecute(
        PromptTemplate template, List<Dictionary<string, string>> variableSets);

    #endregion

    #region Collections

    /// <summary>
    /// Get all collections
    /// </summary>
    /// <returns>List of all collections with their prompt templates</returns>
    [McpExposed(Name = "GetCollections", Description = "Get all prompt collections")]
    Task<List<Collection>> GetCollectionsAsync();

    /// <summary>
    /// Get a collection by ID
    /// </summary>
    /// <param name="id">Collection ID</param>
    /// <returns>Collection with prompt templates, or null if not found</returns>
    [McpExposed(Name = "GetCollection", Description = "Get collection details with prompts")]
    Task<Collection?> GetCollectionByIdAsync(int id);

    /// <summary>
    /// Create a new collection
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="description">Optional collection description</param>
    /// <returns>Created collection</returns>
    [McpExposed(Name = "CreateCollection", Description = "Create a new prompt collection")]
    Task<Collection> CreateCollectionAsync(string name, string? description = null);

    /// <summary>
    /// Updates an existing collection.
    /// </summary>
    /// <param name="collectionId">The ID of the collection to update.</param>
    /// <param name="name">The new name for the collection.</param>
    /// <param name="description">The new optional description for the collection.</param>
    /// <returns>The updated collection, or null if not found or if a concurrency issue occurs.</returns>
    Task<Collection?> UpdateCollectionAsync(int collectionId, string name, string? description);

    /// <summary>
    /// Deletes a collection and its associated data (prompt templates, variables, executions).
    /// </summary>
    /// <param name="collectionId">The ID of the collection to delete.</param>
    /// <returns>True if the collection was successfully deleted, false otherwise (e.g., if not found).</returns>
    Task<bool> DeleteCollectionAsync(int collectionId);

    /// <summary>
    /// Imports a collection and its related data from a JSON string.
    /// </summary>
    /// <param name="jsonContent">The JSON content representing the collection, prompts, variables, and optionally execution history.</param>
    /// <param name="importExecutionHistory">A flag indicating whether to import execution history.</param>
    /// <param name="overwriteExisting">A flag indicating whether to overwrite an existing collection with the same name.</param>
    /// <returns>The imported (or updated/renamed) Collection object, or null if import failed.</returns>
    Task<Collection?> ImportCollectionFromJsonAsync(string jsonContent, bool importExecutionHistory, bool overwriteExisting);

    #endregion

    #region Prompt Management

    /// <summary>
    /// Get prompt templates, optionally filtered by collection
    /// </summary>
    /// <param name="collectionId">Optional collection ID to filter by</param>
    /// <returns>List of prompt templates</returns>
    [McpExposed(Name = "GetPromptTemplates", Description = "List prompt templates")]
    Task<List<PromptTemplate>> GetPromptTemplatesAsync(int? collectionId = null);

    /// <summary>
    /// Get a prompt template by ID
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <returns>Prompt template with related data, or null if not found</returns>
    [McpExposed(Name = "GetPromptTemplate", Description = "Get prompt template details")]
    Task<PromptTemplate?> GetPromptTemplateByIdAsync(int id);

    /// <summary>
    /// Create a new prompt template
    /// </summary>
    /// <param name="name">Prompt template name</param>
    /// <param name="content">Prompt template content</param>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="description">Optional prompt template description</param>
    /// <returns>Created prompt template</returns>
    [McpExposed(Name = "CreatePromptTemplate", Description = "Create a new prompt template")]
    Task<PromptTemplate> CreatePromptTemplateAsync(string name, string content, int collectionId, string? description = null);

    /// <summary>
    /// Updates an existing prompt template and its associated variables.
    /// </summary>
    /// <param name="promptTemplateId">The ID of the prompt template to update.</param>
    /// <param name="name">The new name for the prompt template.</param>
    /// <param name="content">The new content for the prompt template.</param>
    /// <param name="collectionId">The collection ID for the prompt template.</param>
    /// <param name="description">The new optional description for the prompt template.</param>
    /// <returns>The updated prompt template, or null if not found.</returns>
    Task<PromptTemplate?> UpdatePromptTemplateAsync(int promptTemplateId, string name, string content, int collectionId, string? description);

    /// <summary>
    /// Deletes a prompt template and its associated data.
    /// </summary>
    /// <param name="promptTemplateId">The ID of the prompt template to delete.</param>
    /// <returns>True if the template was successfully deleted, false otherwise.</returns>
    Task<bool> DeletePromptTemplateAsync(int promptTemplateId);

    #endregion

    #region Prompt Execution

    /// <summary>
    /// Execute a prompt template with provided variables
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <param name="variables">Variables as JSON string</param>
    /// <returns>Execution result with resolved prompt</returns>
    [McpExposed(Name = "ExecutePrompt", Description = "Execute a prompt template with variables", WrapInEnvelope = false)]
    Task<object> ExecutePromptTemplateAsync(int id, string variables);

    /// <summary>
    /// Get execution history for prompts
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <returns>List of prompt executions</returns>
    [McpExposed(Name = "GetExecutionHistory", Description = "Get prompt execution history")]
    Task<List<PromptExecution>> GetExecutionHistoryAsync(int? promptId = null, int limit = 50);

    /// <summary>
    /// Get a paginated list of execution history for prompts.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="promptId">Optional prompt ID to filter by.</param>
    /// <returns>A list of prompt executions for the specified page.</returns>
    Task<List<PromptExecution>> GetExecutionHistoryAsync(int pageNumber, int pageSize, int? promptId = null);

    /// <summary>
    /// Gets the total count of prompt executions.
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by.</param>
    /// <returns>The total number of prompt executions.</returns>
    Task<int> GetTotalExecutionsCountAsync(int? promptId = null);

    /// <summary>
    /// Execute batch processing with a variable collection
    /// </summary>
    /// <param name="collectionId">Variable collection ID</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>Batch execution result</returns>
    [McpExposed(Name = "ExecuteBatch", Description = "Execute prompt template with variable collection", WrapInEnvelope = false)]
    Task<object> ExecuteBatchAsync(int collectionId, int promptId);    /// <summary>
    /// Saves a list of prompt execution records.
    /// </summary>
    /// <param name="executions">The list of prompt executions to save.</param>
    /// <returns>The list of saved prompt executions, with their IDs populated.</returns>
    Task<List<PromptExecution>> SavePromptExecutionsAsync(List<PromptExecution> executions);

    #endregion

    #region MCP Compliance

    /// <summary>
    /// List all collections with their prompt count
    /// </summary>
    /// <returns>List of all collections with their prompt templates</returns>
    [McpExposed(Name = "ListCollections", Description = "List all collections with their prompt count")]
    Task<List<Collection>> ListCollectionsAsync();

    /// <summary>
    /// List all prompt templates, optionally filtered by collection
    /// </summary>
    /// <param name="collectionId">Optional collection ID to filter by</param>
    /// <returns>List of prompt templates</returns>
    [McpExposed(Name = "ListPrompts", Description = "List all prompt templates, optionally filtered by collection")]
    Task<List<PromptTemplate>> ListPromptsAsync(int? collectionId = null);

    /// <summary>
    /// Get a specific prompt template
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <returns>Prompt template, or null if not found</returns>
    [McpExposed(Name = "GetPrompt", Description = "Get a specific prompt template")]
    Task<PromptTemplate?> GetPromptAsync(int id);

    /// <summary>
    /// List variable collections for a prompt template
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>List of variable collections</returns>
    [McpExposed(Name = "ListVariableCollections", Description = "List variable collections for a prompt template")]
    Task<List<VariableCollection>> ListVariableCollectionsAsync(int promptId);    /// <summary>
    /// Generate a CSV template for a prompt template
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <returns>CSV template content</returns>
    [McpExposed(Name = "GenerateCsvTemplate", Description = "Generate a CSV template for a prompt template")]
    Task<string> GenerateCsvTemplateAsync(int templateId);

    /// <summary>
    /// Get variable collections for a prompt template
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>List of variable collections</returns>
    Task<List<VariableCollection>> GetVariableCollectionsAsync(int promptId);

    /// <summary>
    /// Create a variable collection from CSV data
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="csvData">CSV data with variables</param>
    /// <param name="description">Optional collection description</param>
    /// <returns>Created variable collection</returns>
    Task<VariableCollection> CreateVariableCollectionAsync(string name, int promptId, string csvData, string? description = null);

    #endregion
}