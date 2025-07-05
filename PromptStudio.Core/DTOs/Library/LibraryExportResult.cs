namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Encapsulates the outcome of library export operations for service layer integration and client feedback.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by library export services to return structured results including success status, exported content,
/// and detailed feedback messages. Enables consistent error handling and success communication across
/// different export scenarios (JSON, XML, custom formats) and client interfaces (API, UI, CLI).
/// 
/// <para><strong>Data Contract:</strong></para>
/// Provides standardized export operation results with success indication, content payload, and 
/// operational feedback. Supports both successful exports with content and failed exports with 
/// diagnostic information for troubleshooting and user guidance.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Export services populate this DTO with operation results
/// - API controllers use this for consistent response formatting
/// - UI services consume this for user feedback and content download
/// - Audit services log export operations using success status and messages
/// 
/// <para><strong>Error Handling Patterns:</strong></para>
/// Success=false should include descriptive Message for troubleshooting.
/// JsonContent should be null for failed operations to prevent invalid data usage.
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// JsonContent can be large for complex libraries - consider streaming for very large exports.
/// Message should be concise but informative for optimal user experience.
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage
/// var result = await libraryExportService.ExportAsync(libraryId);
/// if (result.Success) {
///     await fileService.SaveAsync(result.JsonContent, filename);
/// } else {
///     logger.LogError("Export failed: {{Message}}", result.Message);
/// }
/// </code>
/// </example>
public class LibraryExportResult
{
    /// <summary>
    /// Indicates whether the export operation completed successfully.
    /// Service layers should check this before processing JsonContent.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// JSON representation of the exported library content.
    /// Only populated when Success is true; null for failed operations.
    /// Contains complete library data including templates, metadata, and relationships.
    /// </summary>
    public string? JsonContent { get; set; }

    /// <summary>
    /// Descriptive message providing operation feedback and error details.
    /// For successful operations: summary of exported content.
    /// For failed operations: specific error information for troubleshooting.
    /// </summary>
    public string? Message { get; set; }
}
