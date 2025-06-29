using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Enterprise-grade service interface for managing prompt templates with Guid-based architecture
/// Supports multi-tenancy, audit trails, soft deletes, and advanced LLMOps features
/// </summary>
public interface IPromptTemplateService
{
    #region Template CRUD Operations

    /// <summary>
    /// Get prompt templates, optionally filtered by library with tenant isolation
    /// </summary>
    /// <param name="tenantId">Tenant ID for multi-tenant isolation</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="includeDeleted">Whether to include soft-deleted items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of prompt templates</returns>
    Task<List<PromptTemplate>> GetPromptTemplatesAsync(Guid tenantId, Guid? libraryId = null, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a prompt template by ID with tenant validation
    /// </summary>
    /// <param name="id">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID for security validation</param>
    /// <param name="includeDeleted">Whether to include soft-deleted items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Prompt template with related data, or null if not found</returns>
    Task<PromptTemplate?> GetPromptTemplateByIdAsync(Guid id, Guid tenantId, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new prompt template with full audit support
    /// </summary>
    /// <param name="name">Prompt template name</param>
    /// <param name="content">Prompt template content</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="createdBy">User ID who is creating the template</param>
    /// <param name="description">Optional prompt template description</param>
    /// <param name="tags">Optional tags for categorization</param>
    /// <param name="category">Optional category</param>
    /// <param name="version">Version number</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created prompt template</returns>
    Task<PromptTemplate> CreatePromptTemplateAsync(
        string name, 
        string content, 
        Guid libraryId, 
        Guid tenantId, 
        Guid createdBy,
        string? description = null,
        string? tags = null,
        string? category = null,
        string version = "1.0.0",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing prompt template with version management
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="updatedBy">User ID who is updating the template</param>
    /// <param name="name">Updated name</param>
    /// <param name="content">Updated content</param>
    /// <param name="libraryId">Updated library ID</param>
    /// <param name="description">Updated description</param>
    /// <param name="tags">Updated tags</param>
    /// <param name="category">Updated category</param>
    /// <param name="incrementVersion">Whether to increment version number</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated prompt template, or null if not found</returns>
    Task<PromptTemplate?> UpdatePromptTemplateAsync(
        Guid promptTemplateId, 
        Guid tenantId, 
        Guid updatedBy,
        string name, 
        string content, 
        Guid libraryId, 
        string? description = null,
        string? tags = null,
        string? category = null,
        bool incrementVersion = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft delete a prompt template by ID with audit trail
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="deletedBy">User ID who is deleting the template</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the prompt template was deleted, false otherwise</returns>
    Task<bool> SoftDeletePromptTemplateAsync(Guid promptTemplateId, Guid tenantId, Guid deletedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Permanently delete a prompt template (hard delete)
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the prompt template was permanently deleted, false otherwise</returns>
    Task<bool> HardDeletePromptTemplateAsync(Guid promptTemplateId, Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restore a soft-deleted prompt template
    /// </summary>
    /// <param name="promptTemplateId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="restoredBy">User ID who is restoring the template</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if restored successfully</returns>
    Task<bool> RestorePromptTemplateAsync(Guid promptTemplateId, Guid tenantId, Guid restoredBy, CancellationToken cancellationToken = default);

    #endregion

    #region Variable Management

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

    /// <summary>
    /// Resolves a prompt template by substituting variables with provided values
    /// </summary>
    /// <param name="template">The prompt template to resolve</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <returns>The resolved prompt with variables substituted</returns>
    string ResolvePrompt(PromptTemplate template, Dictionary<string, string> variableValues);

    /// <summary>
    /// Get all prompt variables for a template with usage analytics
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of prompt variables with usage statistics</returns>
    Task<List<PromptVariable>> GetTemplateVariablesAsync(Guid templateId, Guid tenantId, CancellationToken cancellationToken = default);

    #endregion

    #region Template Discovery & Search

    /// <summary>
    /// Search prompt templates by name or content with advanced filtering
    /// </summary>
    /// <param name="searchTerm">Search term to match against name or content</param>
    /// <param name="tenantId">Tenant ID for isolation</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="tags">Optional tags to filter by</param>
    /// <param name="category">Optional category filter</param>
    /// <param name="includeDeleted">Whether to include soft-deleted items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching prompt templates</returns>
    Task<List<PromptTemplate>> SearchPromptTemplatesAsync(
        string searchTerm, 
        Guid tenantId,
        Guid? libraryId = null,
        List<string>? tags = null,
        string? category = null,
        bool includeDeleted = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get prompt templates by tags with tenant isolation
    /// </summary>
    /// <param name="tags">Tags to filter by</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="libraryId">Optional library ID to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of matching prompt templates</returns>
    Task<List<PromptTemplate>> GetPromptTemplatesByTagsAsync(List<string> tags, Guid tenantId, Guid? libraryId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get recently used prompt templates for a tenant
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="userId">Optional user ID for personalized results</param>
    /// <param name="limit">Maximum number of templates to return</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of recently used templates</returns>
    Task<List<PromptTemplate>> GetRecentlyUsedTemplatesAsync(Guid tenantId, Guid? userId = null, int limit = 10, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get popular prompt templates based on usage analytics
    /// </summary>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="timeRange">Time range for popularity calculation</param>
    /// <param name="limit">Maximum number of templates to return</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of popular templates with usage metrics</returns>
    Task<List<PromptTemplate>> GetPopularTemplatesAsync(Guid tenantId, TimeSpan? timeRange = null, int limit = 10, CancellationToken cancellationToken = default);

    #endregion

    #region Template Validation & Quality

    /// <summary>
    /// Validate a prompt template's content and structure with enterprise rules
    /// </summary>
    /// <param name="content">Template content to validate</param>
    /// <param name="tenantId">Tenant ID for tenant-specific validation rules</param>
    /// <returns>Validation result with any errors, warnings, and quality score</returns>
    (bool IsValid, List<string> Errors, List<string> Warnings, double QualityScore) ValidateTemplateContent(string content, Guid tenantId);

    /// <summary>
    /// Check if a template name is unique within a library and tenant
    /// </summary>
    /// <param name="name">Template name to check</param>
    /// <param name="libraryId">Library ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="excludeTemplateId">Optional template ID to exclude from uniqueness check</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if name is unique, false otherwise</returns>
    Task<bool> IsTemplateNameUniqueAsync(string name, Guid libraryId, Guid tenantId, Guid? excludeTemplateId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyze template complexity and provide optimization suggestions
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Analysis result with complexity metrics and suggestions</returns>
    Task<TemplateAnalysisResult> AnalyzeTemplateAsync(Guid templateId, Guid tenantId, CancellationToken cancellationToken = default);

    #endregion

    #region Version Management

    /// <summary>
    /// Get version history for a prompt template
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of template versions</returns>
    Task<List<PromptTemplate>> GetTemplateVersionHistoryAsync(Guid templateId, Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revert template to a previous version
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="targetVersion">Target version to revert to</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="updatedBy">User performing the revert</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated template</returns>
    Task<PromptTemplate?> RevertToVersionAsync(Guid templateId, string targetVersion, Guid tenantId, Guid updatedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Compare two template versions
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="version1">First version</param>
    /// <param name="version2">Second version</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Comparison result</returns>
    Task<TemplateComparisonResult> CompareVersionsAsync(Guid templateId, string version1, string version2, Guid tenantId, CancellationToken cancellationToken = default);

    #endregion

    #region Analytics & Insights

    /// <summary>
    /// Get usage analytics for a template
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="timeRange">Time range for analytics</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Usage analytics</returns>
    Task<TemplateUsageAnalytics> GetTemplateAnalyticsAsync(Guid templateId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get performance metrics for a template across executions
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Performance metrics</returns>
    Task<TemplatePerformanceMetrics> GetTemplatePerformanceAsync(Guid templateId, Guid tenantId, CancellationToken cancellationToken = default);

    #endregion
}

# region Supporting Classes
/// <summary>
/// Result of template analysis with complexity metrics and optimization suggestions
/// </summary>
public class TemplateAnalysisResult
{
    public double ComplexityScore { get; set; }
    public int VariableCount { get; set; }
    public int TokenCount { get; set; }
    public List<string> OptimizationSuggestions { get; set; } = new();
    public List<string> QualityIssues { get; set; } = new();
    public Dictionary<string, object> Metrics { get; set; } = new();
}

/// <summary>
/// Result of comparing two template versions
/// </summary>
public class TemplateComparisonResult
{
    public string Version1 { get; set; } = string.Empty;
    public string Version2 { get; set; } = string.Empty;
    public List<string> ContentDifferences { get; set; } = new();
    public List<string> VariableChanges { get; set; } = new();
    public Dictionary<string, object> MetricChanges { get; set; } = new();
}

/// <summary>
/// Usage analytics for a template
/// </summary>
public class TemplateUsageAnalytics
{
    public Guid TemplateId { get; set; }
    public long TotalExecutions { get; set; }
    public long UniqueUsers { get; set; }
    public DateTime FirstUsed { get; set; }
    public DateTime LastUsed { get; set; }
    public double AverageExecutionTime { get; set; }
    public Dictionary<string, long> UsageByPeriod { get; set; } = new();
    public List<string> MostUsedVariables { get; set; } = new();
}

/// <summary>
/// Performance metrics for a template
/// </summary>
public class TemplatePerformanceMetrics
{
    public Guid TemplateId { get; set; }
    public double SuccessRate { get; set; }
    public double AverageLatency { get; set; }
    public double P95Latency { get; set; }
    public double P99Latency { get; set; }
    public long ErrorCount { get; set; }
    public Dictionary<string, long> ErrorsByType { get; set; } = new();
    public double QualityScore { get; set; }
    public Dictionary<string, double> PerformanceTrends { get; set; } = new();
}
#endregion // Supporting Classes