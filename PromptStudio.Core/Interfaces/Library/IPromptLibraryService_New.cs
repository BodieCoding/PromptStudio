using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Statistics;

namespace PromptStudio.Core.Interfaces.Library;

/// <summary>
/// Service interface for managing prompt libraries (MVP focused)
/// Provides core library operations without enterprise complexity
/// </summary>
public interface IPromptLibraryService
{
    #region Library CRUD Operations

    /// <summary>
    /// Get all prompt libraries
    /// </summary>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <returns>List of prompt libraries with their prompt templates</returns>
    Task<List<PromptLibrary>> GetLibrariesAsync(Guid? promptLabId = null);

    /// <summary>
    /// Get a prompt library by ID
    /// </summary>
    /// <param name="id">Library ID</param>
    /// <returns>Library with prompt templates, or null if not found</returns>
    Task<PromptLibrary?> GetLibraryByIdAsync(Guid id);

    /// <summary>
    /// Create a new prompt library
    /// </summary>
    /// <param name="name">Library name</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="description">Optional library description</param>
    /// <returns>Created library</returns>
    Task<PromptLibrary> CreateLibraryAsync(string name, Guid promptLabId, string? description = null);

    /// <summary>
    /// Update an existing prompt library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <param name="name">Updated name</param>
    /// <param name="description">Updated description</param>
    /// <param name="promptLabId">Updated prompt lab ID</param>
    /// <returns>Updated library, or null if not found</returns>
    Task<PromptLibrary?> UpdateLibraryAsync(Guid libraryId, string name, string? description, Guid? promptLabId = null);

    /// <summary>
    /// Delete a prompt library by ID (soft delete)
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <returns>True if the library was deleted, false otherwise</returns>
    Task<bool> DeleteLibraryAsync(Guid libraryId);

    #endregion

    #region Library Discovery

    /// <summary>
    /// Search prompt libraries by name or description
    /// </summary>
    /// <param name="searchTerm">Search term to match against name or description</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <returns>List of matching prompt libraries</returns>
    Task<List<PromptLibrary>> SearchLibrariesAsync(string searchTerm, Guid? promptLabId = null);

    /// <summary>
    /// Get libraries by visibility level
    /// </summary>
    /// <param name="visibility">Visibility level to filter by</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <returns>List of libraries matching visibility criteria</returns>
    Task<List<PromptLibrary>> GetLibrariesByVisibilityAsync(LibraryVisibility visibility, Guid? promptLabId = null);

    /// <summary>
    /// Get recently updated libraries
    /// </summary>
    /// <param name="daysBack">Number of days to look back for updates</param>
    /// <param name="promptLabId">Optional prompt lab ID to filter by</param>
    /// <returns>List of recently updated libraries</returns>
    Task<List<PromptLibrary>> GetRecentlyUpdatedLibrariesAsync(int daysBack = 7, Guid? promptLabId = null);

    #endregion

    #region Library Statistics

    /// <summary>
    /// Get library statistics including template count, execution count, etc.
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <returns>Library statistics</returns>
    Task<LibraryStatistics> GetLibraryStatisticsAsync(Guid libraryId);

    /// <summary>
    /// Get template count for a library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <returns>Number of templates in the library</returns>
    Task<int> GetTemplateCountAsync(Guid libraryId);

    /// <summary>
    /// Get execution count for all templates in a library
    /// </summary>
    /// <param name="libraryId">Library ID</param>
    /// <returns>Total number of executions for templates in the library</returns>
    Task<int> GetExecutionCountAsync(Guid libraryId);

    #endregion

    #region Library Validation

    /// <summary>
    /// Check if a library name is unique within a prompt lab
    /// </summary>
    /// <param name="name">Library name to check</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="excludeLibraryId">Optional library ID to exclude from uniqueness check</param>
    /// <returns>True if name is unique, false otherwise</returns>
    Task<bool> IsLibraryNameUniqueAsync(string name, Guid promptLabId, Guid? excludeLibraryId = null);

    /// <summary>
    /// Validate library creation/update data
    /// </summary>
    /// <param name="name">Library name</param>
    /// <param name="promptLabId">Prompt lab ID</param>
    /// <param name="excludeLibraryId">Optional library ID to exclude from validation</param>
    /// <returns>Validation result</returns>
    Task<(bool IsValid, List<string> Errors)> ValidateLibraryDataAsync(string name, Guid promptLabId, Guid? excludeLibraryId = null);

    #endregion

    #region Import/Export

    /// <summary>
    /// Import a prompt library from JSON data
    /// </summary>
    /// <param name="jsonContent">JSON content representing the library and its templates</param>
    /// <param name="promptLabId">Target prompt lab ID</param>
    /// <param name="importExecutionHistory">Whether to import execution history</param>
    /// <param name="overwriteExisting">Whether to overwrite existing library with same name</param>
    /// <returns>Imported library</returns>
    Task<PromptLibrary?> ImportLibraryFromJsonAsync(string jsonContent, Guid promptLabId, bool importExecutionHistory, bool overwriteExisting);

    /// <summary>
    /// Export a prompt library to JSON
    /// </summary>
    /// <param name="libraryId">Library ID to export</param>
    /// <param name="includeExecutionHistory">Whether to include execution history</param>
    /// <returns>JSON representation of the library</returns>
    Task<string> ExportLibraryToJsonAsync(Guid libraryId, bool includeExecutionHistory = false);

    #endregion
}
