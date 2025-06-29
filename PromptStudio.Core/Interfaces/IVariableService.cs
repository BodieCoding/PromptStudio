using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Enterprise-grade service for handling prompt variables, CSV operations, and variable collections with Guid-based architecture
/// Supports multi-tenancy, audit trails, soft deletes, and advanced analytics
/// </summary>
public interface IVariableService
{
    #region Variable Extraction & Validation

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
    /// Validates variable format and constraints with tenant-specific rules
    /// </summary>
    /// <param name="variableName">Variable name</param>
    /// <param name="variableValue">Variable value</param>
    /// <param name="tenantId">Tenant ID for tenant-specific validation</param>
    /// <returns>Validation result</returns>
    (bool IsValid, List<string> Errors) ValidateVariable(string variableName, string variableValue, Guid tenantId);

    #endregion

    #region CSV Operations

    /// <summary>
    /// Generates a sample CSV template for a prompt's variables
    /// </summary>
    /// <param name="template">The prompt template to generate CSV for</param>
    /// <returns>CSV content with headers for all variables</returns>
    string GenerateVariableCsvTemplate(PromptTemplate template);

    /// <summary>
    /// Generate a CSV template for a prompt template by ID
    /// </summary>
    /// <param name="templateId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>CSV template content</returns>
    Task<string> GenerateCsvTemplateAsync(Guid templateId, Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Parses CSV content into variable sets with validation
    /// </summary>
    /// <param name="csvContent">The CSV content to parse</param>
    /// <param name="expectedVariables">List of expected variable names</param>
    /// <param name="tenantId">Tenant ID for validation</param>
    /// <returns>Parsed variable sets with validation results</returns>
    (List<Dictionary<string, string>> VariableSets, List<string> Errors, List<string> Warnings) ParseVariableCsv(string csvContent, List<string> expectedVariables, Guid tenantId);

    /// <summary>
    /// Advanced CSV parsing with data type inference and validation
    /// </summary>
    /// <param name="csvContent">CSV content</param>
    /// <param name="templateId">Template ID for context</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="options">Parsing options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Enhanced parsing result</returns>
    Task<CsvParsingResult> ParseCsvAdvancedAsync(string csvContent, Guid templateId, Guid tenantId, CsvParsingOptions? options = null, CancellationToken cancellationToken = default);

    #endregion

    #region Batch Execution

    /// <summary>
    /// Batch executes a prompt template against multiple variable sets
    /// </summary>
    /// <param name="template">The prompt template to execute</param>
    /// <param name="variableSets">List of variable sets to use</param>
    /// <returns>List of execution results with variables, resolved prompts, and any errors</returns>
    List<(Dictionary<string, string> Variables, string ResolvedPrompt, bool Success, string? Error)> BatchExecute(
        PromptTemplate template, List<Dictionary<string, string>> variableSets);

    /// <summary>
    /// Enhanced batch execution with progress tracking and analytics
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="variableSets">Variable sets to execute</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="executedBy">User performing the execution</param>
    /// <param name="options">Execution options</param>
    /// <param name="progress">Progress reporter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Batch execution result</returns>
    Task<BatchExecutionResult> BatchExecuteAdvancedAsync(
        Guid templateId,
        List<Dictionary<string, string>> variableSets,
        Guid tenantId,
        Guid executedBy,
        BatchExecutionOptions? options = null,
        IProgress<BatchExecutionProgress>? progress = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Variable Collections Management

    /// <summary>
    /// Get variable collections for a prompt template with tenant isolation
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="includeDeleted">Whether to include soft-deleted collections</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of variable collections</returns>
    Task<List<VariableCollection>> GetVariableCollectionsAsync(Guid promptId, Guid tenantId, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a variable collection by ID with tenant validation
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="includeDeleted">Whether to include soft-deleted items</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Variable collection or null if not found</returns>
    Task<VariableCollection?> GetVariableCollectionByIdAsync(Guid collectionId, Guid tenantId, bool includeDeleted = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a variable collection from CSV data with full audit support
    /// </summary>
    /// <param name="name">Collection name</param>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="csvData">CSV data with variables</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="createdBy">User creating the collection</param>
    /// <param name="description">Optional collection description</param>
    /// <param name="tags">Optional tags</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created variable collection</returns>
    Task<VariableCollection> CreateVariableCollectionAsync(
        string name, 
        Guid promptId, 
        string csvData, 
        Guid tenantId, 
        Guid createdBy,
        string? description = null,
        string? tags = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a variable collection with audit trail
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="updatedBy">User updating the collection</param>
    /// <param name="name">Updated name</param>
    /// <param name="description">Updated description</param>
    /// <param name="csvData">Updated CSV data</param>
    /// <param name="tags">Updated tags</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated collection or null if not found</returns>
    Task<VariableCollection?> UpdateVariableCollectionAsync(
        Guid collectionId, 
        Guid tenantId, 
        Guid updatedBy,
        string? name = null,
        string? description = null,
        string? csvData = null,
        string? tags = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft delete a variable collection
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="deletedBy">User deleting the collection</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> SoftDeleteVariableCollectionAsync(Guid collectionId, Guid tenantId, Guid deletedBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restore a soft-deleted variable collection
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="restoredBy">User restoring the collection</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if restored successfully</returns>
    Task<bool> RestoreVariableCollectionAsync(Guid collectionId, Guid tenantId, Guid restoredBy, CancellationToken cancellationToken = default);

    /// <summary>
    /// List variable collections for a prompt template with filtering and pagination
    /// </summary>
    /// <param name="promptId">Prompt template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="searchTerm">Optional search term</param>
    /// <param name="tags">Optional tags filter</param>
    /// <param name="status">Optional status filter</param>
    /// <param name="skip">Number of items to skip</param>
    /// <param name="take">Number of items to take</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of variable collections</returns>
    Task<(List<VariableCollection> Collections, int TotalCount)> ListVariableCollectionsAsync(
        Guid promptId, 
        Guid tenantId,
        string? searchTerm = null,
        List<string>? tags = null,
        CollectionStatus? status = null,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default);

    #endregion

    #region Variable Analytics

    /// <summary>
    /// Get variable usage analytics for a template
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="timeRange">Time range for analytics</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Variable usage analytics</returns>
    Task<VariableUsageAnalytics> GetVariableAnalyticsAsync(Guid templateId, Guid tenantId, TimeSpan? timeRange = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get collection performance metrics
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Performance metrics</returns>
    Task<CollectionPerformanceMetrics> GetCollectionPerformanceAsync(Guid collectionId, Guid tenantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Suggest optimal variable combinations based on historical data
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="targetMetric">Target optimization metric</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Variable optimization suggestions</returns>
    Task<VariableOptimizationSuggestions> GetVariableOptimizationAsync(Guid templateId, Guid tenantId, string targetMetric = "success_rate", CancellationToken cancellationToken = default);

    #endregion

    #region Import/Export

    /// <summary>
    /// Export variable collection to various formats
    /// </summary>
    /// <param name="collectionId">Collection ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="format">Export format (csv, json, excel)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Exported data as byte array</returns>
    Task<byte[]> ExportCollectionAsync(Guid collectionId, Guid tenantId, string format = "csv", CancellationToken cancellationToken = default);

    /// <summary>
    /// Import variable collection from file with advanced parsing
    /// </summary>
    /// <param name="templateId">Template ID</param>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="createdBy">User importing the collection</param>
    /// <param name="fileName">Original file name</param>
    /// <param name="fileContent">File content</param>
    /// <param name="options">Import options</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Import result</returns>
    Task<VariableImportResult> ImportCollectionAsync(
        Guid templateId,
        Guid tenantId,
        Guid createdBy,
        string fileName,
        byte[] fileContent,
        ImportOptions? options = null,
        CancellationToken cancellationToken = default);

    #endregion
}

#region Supporting Classes

/// <summary>
/// Options for CSV parsing
/// </summary>
public class CsvParsingOptions
{
    public char Delimiter { get; set; } = ',';
    public bool HasHeaders { get; set; } = true;
    public bool TrimWhitespace { get; set; } = true;
    public bool SkipEmptyRows { get; set; } = true;
    public int MaxRows { get; set; } = 10000;
    public Dictionary<string, Type> ColumnTypes { get; set; } = new();
}

/// <summary>
/// Result of CSV parsing operation
/// </summary>
public class CsvParsingResult
{
    public List<Dictionary<string, string>> VariableSets { get; set; } = new();
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public Dictionary<string, Type> InferredTypes { get; set; } = new();
    public int TotalRows { get; set; }
    public int ValidRows { get; set; }
    public Dictionary<string, object> Statistics { get; set; } = new();
}

/// <summary>
/// Options for batch execution
/// </summary>
public class BatchExecutionOptions
{
    public int MaxConcurrency { get; set; } = 10;
    public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);
    public bool ContinueOnError { get; set; } = true;
    public bool SaveIntermediateResults { get; set; } = true;
    public Dictionary<string, object> ExecutionMetadata { get; set; } = new();
}

/// <summary>
/// Progress information for batch execution
/// </summary>
public class BatchExecutionProgress
{
    public int TotalItems { get; set; }
    public int CompletedItems { get; set; }
    public int SuccessfulItems { get; set; }
    public int FailedItems { get; set; }
    public double PercentComplete => TotalItems > 0 ? (double)CompletedItems / TotalItems * 100 : 0;
    public TimeSpan ElapsedTime { get; set; }
    public TimeSpan? EstimatedTimeRemaining { get; set; }
    public string? CurrentItem { get; set; }
}

/// <summary>
/// Result of batch execution
/// </summary>
public class BatchExecutionResult
{
    public Guid ExecutionId { get; set; }
    public int TotalVariableSets { get; set; }
    public int SuccessfulExecutions { get; set; }
    public int FailedExecutions { get; set; }
    public TimeSpan TotalExecutionTime { get; set; }
    public List<VariableSetExecutionResult> Results { get; set; } = new();
    public Dictionary<string, object> ExecutionMetrics { get; set; } = new();
}

/// <summary>
/// Result of executing a single variable set
/// </summary>
public class VariableSetExecutionResult
{
    public int Index { get; set; }
    public Dictionary<string, string> Variables { get; set; } = new();
    public string? ResolvedPrompt { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Variable usage analytics
/// </summary>
public class VariableUsageAnalytics
{
    public Guid TemplateId { get; set; }
    public Dictionary<string, long> VariableUsageCount { get; set; } = new();
    public Dictionary<string, List<string>> MostCommonValues { get; set; } = new();
    public Dictionary<string, double> VariableSuccessRates { get; set; } = new();
    public Dictionary<string, TimeSpan> VariablePerformance { get; set; } = new();
    public List<string> UnusedVariables { get; set; } = new();
    public List<string> HighPerformingVariables { get; set; } = new();
}

/// <summary>
/// Collection performance metrics
/// </summary>
public class CollectionPerformanceMetrics
{
    public Guid CollectionId { get; set; }
    public long TotalExecutions { get; set; }
    public double SuccessRate { get; set; }
    public TimeSpan AverageExecutionTime { get; set; }
    public Dictionary<string, double> VariablePerformance { get; set; } = new();
    public List<string> TopPerformingVariableSets { get; set; } = new();
    public List<string> ProblematicVariableSets { get; set; } = new();
}

/// <summary>
/// Variable optimization suggestions
/// </summary>
public class VariableOptimizationSuggestions
{
    public Guid TemplateId { get; set; }
    public List<VariableOptimization> Suggestions { get; set; } = new();
    public Dictionary<string, object> OptimizationMetrics { get; set; } = new();
}

/// <summary>
/// Individual variable optimization suggestion
/// </summary>
public class VariableOptimization
{
    public string VariableName { get; set; } = string.Empty;
    public string OptimizationType { get; set; } = string.Empty; // replace, enhance, remove, etc.
    public string CurrentValue { get; set; } = string.Empty;
    public string SuggestedValue { get; set; } = string.Empty;
    public double ExpectedImprovement { get; set; }
    public string Reasoning { get; set; } = string.Empty;
    public double Confidence { get; set; }
}

/// <summary>
/// Import options
/// </summary>
public class ImportOptions
{
    public string CollectionName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public CsvParsingOptions? CsvOptions { get; set; }
    public bool ValidateData { get; set; } = true;
    public bool CreateBackup { get; set; } = true;
}

/// <summary>
/// Variable import result
/// </summary>
public class VariableImportResult
{
    public VariableCollection? Collection { get; set; }
    public bool Success { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public int ImportedRows { get; set; }
    public int SkippedRows { get; set; }
    public Dictionary<string, object> ImportStatistics { get; set; } = new();
}

#endregion
