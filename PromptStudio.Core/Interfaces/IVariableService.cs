using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces
{
    /// <summary>
    /// Service interface for variable operations in prompt templates.
    /// Handles variable extraction, resolution, validation, and CSV collection management.
    /// </summary>
    public interface IVariableService
    {
        #region Variable Extraction & Resolution

        /// <summary>
        /// Extracts variable names from a prompt template content
        /// </summary>
        /// <param name="template">The prompt template content to analyze</param>
        /// <returns>List of variable names found in the template</returns>
        List<string> ExtractVariableNames(string template);

        /// <summary>
        /// Resolves variables in a prompt template with provided values
        /// </summary>
        /// <param name="template">The prompt template containing variable placeholders</param>
        /// <param name="variables">Dictionary of variable names and their values</param>
        /// <returns>The resolved prompt with variables substituted</returns>
        Task<string> ResolveVariablesAsync(string template, Dictionary<string, object> variables);

        /// <summary>
        /// Validates that all required variables are provided for a template
        /// </summary>
        /// <param name="template">The prompt template to validate against</param>
        /// <param name="variables">Dictionary of variable names and their values</param>
        /// <returns>True if all required variables have values</returns>
        bool ValidateVariables(PromptTemplate template, Dictionary<string, string> variables);

        #endregion

        #region CSV Operations

        /// <summary>
        /// Generates a sample CSV template for a prompt's variables
        /// </summary>
        /// <param name="template">The prompt template to generate CSV for</param>
        /// <returns>CSV content with headers and sample data</returns>
        string GenerateVariableCsvTemplate(PromptTemplate template);

        /// <summary>
        /// Parses CSV content into variable sets for batch execution
        /// </summary>
        /// <param name="csvContent">The CSV content to parse</param>
        /// <param name="expectedVariables">List of expected variable names</param>
        /// <returns>List of dictionaries containing variable values for each row</returns>
        List<Dictionary<string, string>> ParseVariableCsv(string csvContent, List<string> expectedVariables);

        #endregion

        #region Variable Collections

        /// <summary>
        /// Creates a variable collection from CSV data
        /// </summary>
        /// <param name="name">Collection name</param>
        /// <param name="csvData">CSV data with variable values</param>
        /// <param name="promptId">Associated prompt template ID</param>
        /// <param name="description">Optional collection description</param>
        /// <returns>The created variable collection</returns>
        Task<VariableCollection> CreateVariableCollectionFromCsvAsync(string name, string csvData, 
            Guid promptId, string? description = null);

        /// <summary>
        /// Gets a variable collection by ID
        /// </summary>
        /// <param name="collectionId">ID of the collection to retrieve</param>
        /// <returns>The variable collection if found</returns>
        Task<VariableCollection?> GetVariableCollectionAsync(Guid collectionId);

        /// <summary>
        /// Gets all variable collections for a prompt template
        /// </summary>
        /// <param name="promptId">Prompt template ID to filter by</param>
        /// <returns>List of variable collections</returns>
        Task<List<VariableCollection>> GetVariableCollectionsAsync(Guid? promptId = null);

        /// <summary>
        /// Deletes a variable collection
        /// </summary>
        /// <param name="collectionId">ID of the collection to delete</param>
        /// <returns>True if deletion was successful</returns>
        Task<bool> DeleteVariableCollectionAsync(Guid collectionId);

        #endregion
    }
}
