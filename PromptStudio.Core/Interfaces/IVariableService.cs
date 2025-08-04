using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs;

namespace PromptStudio.Core.Interfaces
{
    /// <summary>
    /// Interface for managing variables, variable resolution, and variable-related operations in the prompt system.
    /// Provides comprehensive variable management including dynamic resolution, type conversion, validation, and caching.
    /// </summary>
    public interface IVariableService
    {
        #region Variable Resolution

        /// <summary>
        /// Resolves variables in a prompt template with provided values
        /// </summary>
        /// <param name="template">The prompt template containing variable placeholders</param>
        /// <param name="variables">Dictionary of variable names and their values</param>
        /// <returns>The resolved prompt with variables substituted</returns>
        Task<string> ResolveVariablesAsync(string template, Dictionary<string, object> variables);

        /// <summary>
        /// Extracts variable names from a prompt template
        /// </summary>
        /// <param name="template">The prompt template to analyze</param>
        /// <returns>List of variable names found in the template</returns>
        List<string> ExtractVariableNames(string template);

        /// <summary>
        /// Validates that all required variables are provided
        /// </summary>
        /// <param name="template">The prompt template to validate against</param>
        /// <param name="variables">The variables to validate</param>
        /// <returns>Validation result with any missing variables</returns>
        Task<VariableValidationResult> ValidateVariablesAsync(string template, Dictionary<string, object> variables);

        #endregion

        #region Variable Management

        /// <summary>
        /// Creates a new variable definition
        /// </summary>
        /// <param name="name">Variable name</param>
        /// <param name="type">Variable type</param>
        /// <param name="defaultValue">Default value for the variable</param>
        /// <param name="description">Variable description</param>
        /// <param name="isRequired">Whether the variable is required</param>
        /// <param name="validationRules">Optional validation rules</param>
        /// <param name="userId">User creating the variable</param>
        /// <returns>The created variable definition</returns>
        Task<Variable> CreateVariableAsync(string name, string type, object? defaultValue = null, 
            string? description = null, bool isRequired = true, string? validationRules = null, Guid? userId = null);

        /// <summary>
        /// Updates an existing variable definition
        /// </summary>
        /// <param name="variableId">ID of the variable to update</param>
        /// <param name="name">New variable name</param>
        /// <param name="type">New variable type</param>
        /// <param name="defaultValue">New default value</param>
        /// <param name="description">New description</param>
        /// <param name="isRequired">Whether the variable is required</param>
        /// <param name="validationRules">New validation rules</param>
        /// <param name="userId">User making the update</param>
        /// <returns>The updated variable definition</returns>
        Task<Variable> UpdateVariableAsync(Guid variableId, string name, string type, object? defaultValue = null,
            string? description = null, bool isRequired = true, string? validationRules = null, Guid? userId = null);

        /// <summary>
        /// Deletes a variable definition
        /// </summary>
        /// <param name="variableId">ID of the variable to delete</param>
        /// <param name="userId">User performing the deletion</param>
        /// <returns>True if deletion was successful</returns>
        Task<bool> DeleteVariableAsync(Guid variableId, Guid? userId = null);

        /// <summary>
        /// Gets a variable definition by ID
        /// </summary>
        /// <param name="variableId">ID of the variable to retrieve</param>
        /// <param name="userId">User requesting the variable</param>
        /// <returns>The variable definition if found</returns>
        Task<Variable?> GetVariableAsync(Guid variableId, Guid? userId = null);

        /// <summary>
        /// Gets all variables for a user or globally
        /// </summary>
        /// <param name="userId">User ID to filter by, null for global variables</param>
        /// <param name="includeGlobal">Whether to include global variables when filtering by user</param>
        /// <returns>List of variable definitions</returns>
        Task<List<Variable>> GetVariablesAsync(Guid? userId = null, bool includeGlobal = true);

        #endregion

        #region Variable Collections

        /// <summary>
        /// Creates a variable collection from CSV data
        /// </summary>
        /// <param name="name">Collection name</param>
        /// <param name="csvData">CSV data with variable values</param>
        /// <param name="description">Collection description</param>
        /// <param name="userId">User creating the collection</param>
        /// <returns>The created variable collection</returns>
        Task<VariableCollection> CreateVariableCollectionFromCsvAsync(string name, string csvData, 
            string? description = null, Guid? userId = null);

        /// <summary>
        /// Gets a variable collection by ID
        /// </summary>
        /// <param name="collectionId">ID of the collection to retrieve</param>
        /// <param name="userId">User requesting the collection</param>
        /// <returns>The variable collection if found</returns>
        Task<VariableCollection?> GetVariableCollectionAsync(Guid collectionId, Guid? userId = null);

        /// <summary>
        /// Gets all variable collections for a user
        /// </summary>
        /// <param name="userId">User ID to filter by</param>
        /// <returns>List of variable collections</returns>
        Task<List<VariableCollection>> GetVariableCollectionsAsync(Guid? userId = null);

        /// <summary>
        /// Updates a variable collection
        /// </summary>
        /// <param name="collectionId">ID of the collection to update</param>
        /// <param name="name">New collection name</param>
        /// <param name="description">New description</param>
        /// <param name="csvData">New CSV data</param>
        /// <param name="userId">User making the update</param>
        /// <returns>The updated variable collection</returns>
        Task<VariableCollection> UpdateVariableCollectionAsync(Guid collectionId, string name, 
            string? description = null, string? csvData = null, Guid? userId = null);

        /// <summary>
        /// Deletes a variable collection
        /// </summary>
        /// <param name="collectionId">ID of the collection to delete</param>
        /// <param name="userId">User performing the deletion</param>
        /// <returns>True if deletion was successful</returns>
        Task<bool> DeleteVariableCollectionAsync(Guid collectionId, Guid? userId = null);

        #endregion

        #region Variable Type Conversion

        /// <summary>
        /// Converts a variable value to the specified type
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <param name="targetType">Target type for conversion</param>
        /// <returns>Converted value</returns>
        object? ConvertVariableValue(object? value, string targetType);

        /// <summary>
        /// Validates a variable value against its type and validation rules
        /// </summary>
        /// <param name="variable">Variable definition with validation rules</param>
        /// <param name="value">Value to validate</param>
        /// <returns>Validation result</returns>
        Task<VariableValidationResult> ValidateVariableValueAsync(Variable variable, object? value);

        /// <summary>
        /// Gets supported variable types
        /// </summary>
        /// <returns>List of supported variable types</returns>
        List<string> GetSupportedVariableTypes();

        #endregion

        #region Batch Operations

        /// <summary>
        /// Resolves variables for multiple prompt templates
        /// </summary>
        /// <param name="templates">List of templates with their variable sets</param>
        /// <returns>List of resolved prompts</returns>
        Task<List<BatchVariableResolutionResult>> ResolveBatchVariablesAsync(
            List<(string template, Dictionary<string, object> variables)> templates);

        /// <summary>
        /// Validates variables for multiple templates
        /// </summary>
        /// <param name="templates">List of templates with their variable sets</param>
        /// <returns>List of validation results</returns>
        Task<List<VariableValidationResult>> ValidateBatchVariablesAsync(
            List<(string template, Dictionary<string, object> variables)> templates);

        #endregion

        #region Caching and Performance

        /// <summary>
        /// Clears the variable resolution cache
        /// </summary>
        Task ClearVariableCacheAsync();

        /// <summary>
        /// Gets variable resolution statistics
        /// </summary>
        /// <param name="userId">User to get statistics for</param>
        /// <param name="days">Number of days to look back</param>
        /// <returns>Variable usage statistics</returns>
        Task<VariableStatistics> GetVariableStatisticsAsync(Guid? userId = null, int days = 30);

        #endregion
    }

    #region Supporting Types

    /// <summary>
    /// Result of variable validation operations
    /// </summary>
    public class VariableValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> MissingVariables { get; set; } = new();
        public List<string> InvalidVariables { get; set; } = new();
        public List<string> ValidationErrors { get; set; } = new();
        public Dictionary<string, string> TypeMismatches { get; set; } = new();
    }

    /// <summary>
    /// Result of batch variable resolution
    /// </summary>
    public class BatchVariableResolutionResult
    {
        public string ResolvedPrompt { get; set; } = string.Empty;
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
        public Dictionary<string, object> ResolvedVariables { get; set; } = new();
    }

    /// <summary>
    /// Variable usage statistics
    /// </summary>
    public class VariableStatistics
    {
        public int TotalVariableResolutions { get; set; }
        public int UniqueVariablesUsed { get; set; }
        public Dictionary<string, int> MostUsedVariables { get; set; } = new();
        public Dictionary<string, int> VariableTypeUsage { get; set; } = new();
        public int CacheHitRate { get; set; }
        public TimeSpan AverageResolutionTime { get; set; }
    }

    #endregion
}
