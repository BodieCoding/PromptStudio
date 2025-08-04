using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Service interface for prompt operations that coordinates across specialized services.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// <para>Primary facade service that coordinates prompt operations across specialized services,
/// acting as a single entry point for prompt management, template operations, execution,
/// and variable handling. Maintains backward compatibility for MCP and legacy operations
/// while providing a unified interface for all prompt-related functionality.</para>
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// <para>Implementations must coordinate with underlying specialized services while providing
/// comprehensive prompt management capabilities including template resolution, batch operations,
/// execution tracking, and collection management. All operations must maintain data integrity
/// and provide proper error handling across service boundaries.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
/// <item><description>Coordinate with specialized services while maintaining unified interface</description></item>
/// <item><description>Provide comprehensive variable resolution and validation</description></item>
/// <item><description>Support batch operations and CSV-based variable management</description></item>
/// <item><description>Maintain execution history and audit trails</description></item>
/// <item><description>Ensure backward compatibility with existing MCP operations</description></item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Coordinates with IPromptTemplateService for template management</description></item>
/// <item><description>Integrates with IPromptLibraryService for library operations</description></item>
/// <item><description>Utilizes IPromptExecutionService for execution coordination</description></item>
/// <item><description>Leverages IVariableService for variable management</description></item>
/// <item><description>Maintains IPromptStudioDbContext for direct data access when needed</description></item>
/// </list>
/// </remarks>
public interface IPromptService
{
    #region Prompt Resolution

    /// <summary>
    /// Resolves a prompt template by substituting variables with provided values.
    /// Provides the core template resolution functionality for variable substitution.
    /// </summary>
    /// <param name="template">The prompt template to resolve</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <returns>The resolved prompt with variables substituted</returns>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Variables use {{variableName}} placeholder format</description></item>
    /// <item><description>Missing variables are replaced with default values or empty strings</description></item>
    /// <item><description>Variable resolution is case-sensitive</description></item>
    /// <item><description>Nested variable references are not supported</description></item>
    /// </list>
    /// </remarks>
    string ResolvePrompt(PromptTemplate template, Dictionary<string, string> variableValues);

    /// <summary>
    /// Extracts variable names from a prompt template content.
    /// Analyzes template content to identify all variable placeholders.
    /// </summary>
    /// <param name="promptContent">The content to extract variables from</param>
    /// <returns>List of variable names found in the content</returns>
    /// <remarks>
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Parses {{variableName}} format placeholders</description></item>
    /// <item><description>Returns unique variable names (no duplicates)</description></item>
    /// <item><description>Handles malformed placeholders gracefully</description></item>
    /// <item><description>Trims whitespace from variable names</description></item>
    /// </list>
    /// </remarks>
    List<string> ExtractVariableNames(string promptContent);

    /// <summary>
    /// Validates that all required variables have values.
    /// Ensures template can be properly resolved with provided variable values.
    /// </summary>
    /// <param name="template">The prompt template to validate</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <returns>True if all required variables have values, false otherwise</returns>
    /// <remarks>
    /// <para><strong>Validation Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Required variables must have values in the dictionary</description></item>
    /// <item><description>Variables with default values are considered optional</description></item>
    /// <item><description>Empty string values are considered valid</description></item>
    /// <item><description>Null values are treated as missing</description></item>
    /// </list>
    /// </remarks>
    bool ValidateVariables(PromptTemplate template, Dictionary<string, string> variableValues);

    #endregion

    #region CSV / Batch Operations

    /// <summary>
    /// Generates a sample CSV template for a prompt's variables.
    /// Creates CSV structure with headers and sample data for variable import.
    /// </summary>
    /// <param name="template">The prompt template to generate CSV for</param>
    /// <returns>CSV content with headers for all variables</returns>
    /// <remarks>
    /// <para><strong>CSV Format:</strong></para>
    /// <list type="bullet">
    /// <item><description>First row contains variable names as headers</description></item>
    /// <item><description>Subsequent rows contain sample/default values</description></item>
    /// <item><description>Values are properly quoted for CSV compliance</description></item>
    /// <item><description>Returns informative message if no variables found</description></item>
    /// </list>
    /// </remarks>
    string GenerateVariableCsvTemplate(PromptTemplate template);

    /// <summary>
    /// Parses CSV content into variable sets for batch processing.
    /// Converts CSV data into structured variable collections for template execution.
    /// </summary>
    /// <param name="csvContent">The CSV content to parse</param>
    /// <param name="expectedVariables">List of expected variable names</param>
    /// <returns>List of dictionaries containing variable values for each row</returns>
    /// <exception cref="ArgumentException">Thrown when CSV format is invalid or missing required variables</exception>
    /// <remarks>
    /// <para><strong>CSV Requirements:</strong></para>
    /// <list type="bullet">
    /// <item><description>Must contain header row with variable names</description></item>
    /// <item><description>Must include all expected variables as headers</description></item>
    /// <item><description>Data rows must match header column count</description></item>
    /// <item><description>Handles quoted fields and embedded commas</description></item>
    /// </list>
    /// </remarks>
    List<Dictionary<string, string>> ParseVariableCsv(string csvContent, List<string> expectedVariables);

    /// <summary>
    /// Batch executes a prompt template against multiple variable sets.
    /// Processes multiple variable combinations and returns comprehensive results.
    /// </summary>
    /// <param name="template">The prompt template to execute</param>
    /// <param name="variableSets">List of variable sets to use</param>
    /// <returns>List of execution results with variables, resolved prompts, and any errors</returns>
    /// <remarks>
    /// <para><strong>Batch Processing:</strong></para>
    /// <list type="bullet">
    /// <item><description>Processes each variable set independently</description></item>
    /// <item><description>Continues processing even if individual sets fail</description></item>
    /// <item><description>Returns success/failure status for each execution</description></item>
    /// <item><description>Captures detailed error information for failed executions</description></item>
    /// </list>
    /// </remarks>
    List<(Dictionary<string, string> Variables, string ResolvedPrompt, bool Success, string? Error)> BatchExecute(
        PromptTemplate template, List<Dictionary<string, string>> variableSets);

    #endregion

    #region Collections

    /// <summary>
    /// Retrieves all collections with their prompt templates.
    /// Provides complete collection hierarchy for management interfaces.
    /// </summary>
    /// <returns>List of all collections with their prompt templates</returns>
    Task<List<Collection>> GetCollectionsAsync();

    /// <summary>
    /// Retrieves a collection by its unique identifier.
    /// Loads complete collection data including related templates and variables.
    /// </summary>
    /// <param name="id">Collection ID</param>
    /// <returns>Collection with prompt templates, or null if not found</returns>
    Task<Collection?> GetCollectionByIdAsync(int id);

    /// <summary>
    /// Creates a new collection with the specified parameters.
    /// Establishes new collection container for organizing prompt templates.
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="description">Optional collection description</param>
    /// <returns>Created collection</returns>
    Task<Collection> CreateCollectionAsync(string name, string? description = null);

    /// <summary>
    /// Updates an existing collection's properties.
    /// Modifies collection metadata while preserving template relationships.
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="name">Updated name</param>
    /// <param name="description">Updated description</param>
    /// <returns>Updated collection, or null if not found</returns>
    Task<Collection?> UpdateCollectionAsync(int collectionId, string name, string? description);

    /// <summary>
    /// Deletes a collection and all its associated data.
    /// Removes collection including all templates, variables, and execution history.
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <returns>True if the collection was deleted, false otherwise</returns>
    Task<bool> DeleteCollectionAsync(int collectionId);

    /// <summary>
    /// Imports a collection and its related data from JSON content.
    /// Supports comprehensive collection import with templates, variables, and execution history.
    /// </summary>
    /// <param name="jsonContent">JSON content representing the collection data</param>
    /// <param name="importExecutionHistory">Whether to import execution history</param>
    /// <param name="overwriteExisting">Whether to overwrite existing collection with same name</param>
    /// <returns>The imported Collection object, or null if import failed</returns>
    /// <exception cref="ArgumentException">Thrown when JSON format is invalid or collection data is missing</exception>
    Task<Collection?> ImportCollectionFromJsonAsync(string jsonContent, bool importExecutionHistory, bool overwriteExisting);

    #endregion

    #region Prompt Management

    /// <summary>
    /// Retrieves prompt templates, optionally filtered by collection.
    /// Provides template discovery and browsing capabilities.
    /// </summary>
    /// <param name="collectionId">Optional collection ID to filter by</param>
    /// <returns>List of prompt templates</returns>
    Task<List<PromptTemplate>> GetPromptTemplatesAsync(int? collectionId = null);

    /// <summary>
    /// Retrieves a prompt template by its unique identifier.
    /// Loads complete template data including variables and collection information.
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <returns>Prompt template with related data, or null if not found</returns>
    Task<PromptTemplate?> GetPromptTemplateByIdAsync(int id);

    /// <summary>
    /// Creates a new prompt template with automatic variable extraction.
    /// Establishes new template and automatically creates variables from content.
    /// </summary>
    /// <param name="name">Prompt template name</param>
    /// <param name="content">Prompt template content</param>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="description">Optional prompt template description</param>
    /// <returns>Created prompt template</returns>
    /// <exception cref="ArgumentException">Thrown when collection ID is invalid</exception>
    Task<PromptTemplate> CreatePromptTemplateAsync(string name, string content, int collectionId, string? description = null);

    /// <summary>
    /// Updates an existing prompt template with variable synchronization.
    /// Modifies template and synchronizes variables with content changes.
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <param name="name">Updated name</param>
    /// <param name="content">Updated content</param>
    /// <param name="collectionId">Updated collection ID</param>
    /// <param name="description">Updated description</param>
    /// <returns>Updated prompt template, or null if not found</returns>
    /// <exception cref="ArgumentException">Thrown when collection ID is invalid</exception>
    Task<PromptTemplate?> UpdatePromptTemplateAsync(int promptTemplateId, string name, string content, int collectionId, string? description);

    /// <summary>
    /// Deletes a prompt template and all associated data.
    /// Removes template including variables and execution history.
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <returns>True if the prompt template was deleted, false otherwise</returns>
    Task<bool> DeletePromptTemplateAsync(int promptTemplateId);

    #endregion

    #region Prompt Execution

    /// <summary>
    /// Executes a prompt template with provided variables and records execution.
    /// Resolves template, validates variables, and stores execution history.
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <param name="variables">Variables as JSON string</param>
    /// <returns>Execution result with resolved prompt</returns>
    /// <exception cref="ArgumentException">Thrown when template not found or variables are invalid</exception>
    Task<object> ExecutePromptTemplateAsync(int id, string variables);

    /// <summary>
    /// Retrieves execution history for prompts with optional filtering.
    /// Provides access to historical execution data for analysis.
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <param name="limit">Maximum number of executions to return</param>
    /// <returns>List of prompt executions</returns>
    Task<List<PromptExecution>> GetExecutionHistoryAsync(int? promptId = null, int limit = 50);

    /// <summary>
    /// Retrieves execution history with pagination support.
    /// Provides paginated access to execution history for large datasets.
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <returns>List of prompt executions</returns>
    Task<List<PromptExecution>> GetExecutionHistoryAsync(int pageNumber, int pageSize, int? promptId = null);

    /// <summary>
    /// Retrieves total count of executions for pagination.
    /// Provides execution count for pagination calculation.
    /// </summary>
    /// <param name="promptId">Optional prompt ID to filter by</param>
    /// <returns>Total count of executions</returns>
    Task<int> GetTotalExecutionsCountAsync(int? promptId = null);

    /// <summary>
    /// Executes batch processing with a variable collection.
    /// Processes multiple variable sets from stored collection.
    /// </summary>
    /// <param name="collectionId">Variable collection ID</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>Batch execution result</returns>
    /// <exception cref="ArgumentException">Thrown when collection or template not found</exception>
    Task<object> ExecuteBatchAsync(int collectionId, int promptId);

    /// <summary>
    /// Saves a list of prompt executions to the database.
    /// Persists execution results for audit and analysis purposes.
    /// </summary>
    /// <param name="executions">List of prompt executions</param>
    /// <returns>List of saved prompt executions with generated IDs</returns>
    Task<List<PromptExecution>> SavePromptExecutionsAsync(List<PromptExecution> executions);

    #endregion

    #region Variable Collections

    /// <summary>
    /// Retrieves variable collections for a prompt template.
    /// Provides access to stored variable sets for batch processing.
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>List of variable collections</returns>
    Task<List<VariableCollection>> GetVariableCollectionsAsync(int promptId);

    /// <summary>
    /// Creates a variable collection from CSV data.
    /// Parses CSV data and creates stored variable collection for reuse.
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="csvData">CSV data with variables</param>
    /// <param name="description">Optional collection description</param>
    /// <returns>Created variable collection</returns>
    /// <exception cref="ArgumentException">Thrown when template not found or CSV format is invalid</exception>
    Task<VariableCollection> CreateVariableCollectionAsync(string name, int promptId, string csvData, string? description = null);

    #endregion

    #region MCP Service Methods

    /// <summary>
    /// Lists all collections with their prompt count for MCP compatibility.
    /// Provides collection overview for Model Context Protocol integration.
    /// </summary>
    /// <returns>List of all collections with their prompt templates</returns>
    Task<List<Collection>> ListCollectionsAsync();

    /// <summary>
    /// Lists all prompt templates with optional collection filtering for MCP compatibility.
    /// Provides template discovery for Model Context Protocol integration.
    /// </summary>
    /// <param name="collectionId">Optional collection ID to filter by</param>
    /// <returns>List of prompt templates</returns>
    Task<List<PromptTemplate>> ListPromptsAsync(int? collectionId = null);

    /// <summary>
    /// Retrieves a specific prompt template for MCP compatibility.
    /// Provides template access for Model Context Protocol integration.
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <returns>Prompt template, or null if not found</returns>
    Task<PromptTemplate?> GetPromptAsync(int id);

    /// <summary>
    /// Lists variable collections for a prompt template for MCP compatibility.
    /// Provides variable collection access for Model Context Protocol integration.
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <returns>List of variable collections</returns>
    Task<List<VariableCollection>> ListVariableCollectionsAsync(int promptId);

    /// <summary>
    /// Generates a CSV template for a prompt template for MCP compatibility.
    /// Provides CSV template generation for Model Context Protocol integration.
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <returns>CSV template content</returns>
    /// <exception cref="ArgumentException">Thrown when template not found</exception>
    Task<string> GenerateCsvTemplateAsync(int templateId);

    #endregion
}
