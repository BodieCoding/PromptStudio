using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces.Updated;

/// <summary>
/// Interface for the PromptStudio database context (Updated for Guid-based architecture)
/// </summary>
public interface IPromptStudioDbContext : IDisposable
{
    #region Core Entities

    /// <summary>
    /// Prompt labs collection
    /// </summary>
    DbSet<PromptLab> PromptLabs { get; set; }

    /// <summary>
    /// Prompt libraries collection
    /// </summary>
    DbSet<PromptLibrary> PromptLibraries { get; set; }

    /// <summary>
    /// Prompt templates collection
    /// </summary>
    DbSet<PromptTemplate> PromptTemplates { get; set; }

    /// <summary>
    /// Prompt variables collection
    /// </summary>
    DbSet<PromptVariable> PromptVariables { get; set; }

    /// <summary>
    /// Prompt executions collection
    /// </summary>
    DbSet<PromptExecution> PromptExecutions { get; set; }

    /// <summary>
    /// Variable collections
    /// </summary>
    DbSet<VariableCollection> VariableCollections { get; set; }

    #endregion

    #region Workflow Entities

    /// <summary>
    /// Prompt flows collection
    /// </summary>
    DbSet<PromptFlow> PromptFlows { get; set; }

    /// <summary>
    /// Flow nodes collection
    /// </summary>
    DbSet<FlowNode> FlowNodes { get; set; }

    /// <summary>
    /// Flow edges collection
    /// </summary>
    DbSet<FlowEdge> FlowEdges { get; set; }

    /// <summary>
    /// Flow executions collection
    /// </summary>
    DbSet<FlowExecution> FlowExecutions { get; set; }

    /// <summary>
    /// Node executions collection
    /// </summary>
    DbSet<NodeExecution> NodeExecutions { get; set; }

    #endregion

    #region Permission and Security Entities

    /// <summary>
    /// Template permissions collection
    /// </summary>
    DbSet<TemplatePermission> TemplatePermissions { get; set; }

    /// <summary>
    /// Library permissions collection
    /// </summary>
    DbSet<LibraryPermission> LibraryPermissions { get; set; }

    /// <summary>
    /// Lab permissions collection
    /// </summary>
    DbSet<LabPermission> LabPermissions { get; set; }

    #endregion

    #region Analytics and Testing Entities

    /// <summary>
    /// A/B tests collection
    /// </summary>
    DbSet<ABTest> ABTests { get; set; }

    /// <summary>
    /// A/B test variants collection
    /// </summary>
    DbSet<ABTestVariant> ABTestVariants { get; set; }

    /// <summary>
    /// A/B test results collection
    /// </summary>
    DbSet<ABTestResult> ABTestResults { get; set; }

    /// <summary>
    /// Performance metrics collection
    /// </summary>
    DbSet<PerformanceMetric> PerformanceMetrics { get; set; }

    /// <summary>
    /// Usage analytics collection
    /// </summary>
    DbSet<UsageAnalytics> UsageAnalytics { get; set; }

    #endregion

    #region Collaboration Entities

    /// <summary>
    /// Workflow libraries collection
    /// </summary>
    DbSet<WorkflowLibrary> WorkflowLibraries { get; set; }

    /// <summary>
    /// Template versions collection
    /// </summary>
    DbSet<TemplateVersion> TemplateVersions { get; set; }

    /// <summary>
    /// Collaboration comments collection
    /// </summary>
    DbSet<CollaborationComment> CollaborationComments { get; set; }

    /// <summary>
    /// Review requests collection
    /// </summary>
    DbSet<ReviewRequest> ReviewRequests { get; set; }

    #endregion

    #region Legacy Support (will be removed after migration)

    /// <summary>
    /// Collections (Legacy - will be removed)
    /// </summary>
    [Obsolete("Use PromptLibraries instead. This property will be removed in a future version.")]
    DbSet<Collection> Collections { get; set; }

    #endregion

    #region Database Operations

    /// <summary>
    /// Save changes asynchronously with optional cancellation token
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entities written to the database</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Save changes synchronously
    /// </summary>
    /// <returns>Number of entities written to the database</returns>
    int SaveChanges();

    /// <summary>
    /// Save changes with audit information
    /// </summary>
    /// <param name="userId">User ID performing the operation</param>
    /// <param name="tenantId">Optional tenant ID for multi-tenancy</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entities written to the database</returns>
    Task<int> SaveChangesWithAuditAsync(string? userId = null, Guid? tenantId = null, CancellationToken cancellationToken = default);

    #endregion

    #region Transaction Support

    /// <summary>
    /// Begin a database transaction
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Database transaction</returns>
    Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin a database transaction synchronously
    /// </summary>
    /// <returns>Database transaction</returns>
    Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction BeginTransaction();

    #endregion

    #region Bulk Operations

    /// <summary>
    /// Bulk insert entities
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entities">Entities to insert</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task representing the operation</returns>
    Task BulkInsertAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Bulk update entities
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entities">Entities to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entities updated</returns>
    Task<int> BulkUpdateAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Bulk delete entities
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entities">Entities to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entities deleted</returns>
    Task<int> BulkDeleteAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;

    #endregion

    #region Entity State Management

    /// <summary>
    /// Attach an entity to the context
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entity">Entity to attach</param>
    /// <returns>Entity entry</returns>
    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> Attach<T>(T entity) where T : class;

    /// <summary>
    /// Detach an entity from the context
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entity">Entity to detach</param>
    void Detach<T>(T entity) where T : class;

    /// <summary>
    /// Mark an entity as deleted (for soft delete)
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entity">Entity to mark as deleted</param>
    /// <param name="deletedBy">User ID who deleted the entity</param>
    void MarkAsDeleted<T>(T entity, string? deletedBy = null) where T : AuditableEntity;

    /// <summary>
    /// Restore a soft-deleted entity
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="entity">Entity to restore</param>
    /// <param name="restoredBy">User ID who restored the entity</param>
    void RestoreDeleted<T>(T entity, string? restoredBy = null) where T : AuditableEntity;

    #endregion

    #region Query Helpers

    /// <summary>
    /// Get all entities including soft-deleted ones
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <returns>Queryable including deleted entities</returns>
    IQueryable<T> SetIncludingDeleted<T>() where T : AuditableEntity;

    /// <summary>
    /// Get only active (non-deleted) entities
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <returns>Queryable excluding deleted entities</returns>
    IQueryable<T> SetActiveOnly<T>() where T : AuditableEntity;

    /// <summary>
    /// Get entities for a specific tenant
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="tenantId">Tenant ID</param>
    /// <param name="includeDeleted">Whether to include soft-deleted entities</param>
    /// <returns>Queryable filtered by tenant</returns>
    IQueryable<T> SetForTenant<T>(Guid tenantId, bool includeDeleted = false) where T : AuditableEntity;

    #endregion

    #region Migration and Schema

    /// <summary>
    /// Ensure the database is created
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the database was created, false if it already existed</returns>
    Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Apply pending migrations
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task representing the operation</returns>
    Task MigrateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get pending migrations
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of pending migration names</returns>
    Task<IEnumerable<string>> GetPendingMigrationsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get applied migrations
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of applied migration names</returns>
    Task<IEnumerable<string>> GetAppliedMigrationsAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Auditing and Change Tracking

    /// <summary>
    /// Get change tracker information
    /// </summary>
    /// <returns>Change tracker</returns>
    Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker GetChangeTracker();

    /// <summary>
    /// Get audit entries for pending changes
    /// </summary>
    /// <returns>List of audit entries</returns>
    List<AuditEntry> GetAuditEntries();

    /// <summary>
    /// Enable or disable automatic audit tracking
    /// </summary>
    /// <param name="enabled">Whether to enable audit tracking</param>
    void SetAuditTrackingEnabled(bool enabled);

    #endregion

    #region Performance and Monitoring

    /// <summary>
    /// Get database connection string (for monitoring)
    /// </summary>
    /// <returns>Database connection string</returns>
    string GetConnectionString();

    /// <summary>
    /// Get database provider name
    /// </summary>
    /// <returns>Database provider name</returns>
    string GetProviderName();

    /// <summary>
    /// Get query execution statistics
    /// </summary>
    /// <returns>Query execution statistics</returns>
    DbQueryStatistics GetQueryStatistics();

    /// <summary>
    /// Reset query execution statistics
    /// </summary>
    void ResetQueryStatistics();

    #endregion
}

/// <summary>
/// Audit entry for change tracking
/// </summary>
public class AuditEntry
{
    /// <summary>
    /// Entity that was changed
    /// </summary>
    public object Entity { get; set; } = null!;

    /// <summary>
    /// Entity name
    /// </summary>
    public string EntityName { get; set; } = string.Empty;

    /// <summary>
    /// Type of change
    /// </summary>
    public string ChangeType { get; set; } = string.Empty;

    /// <summary>
    /// Property changes
    /// </summary>
    public Dictionary<string, object?> Changes { get; set; } = new();

    /// <summary>
    /// Original values
    /// </summary>
    public Dictionary<string, object?> OriginalValues { get; set; } = new();

    /// <summary>
    /// New values
    /// </summary>
    public Dictionary<string, object?> NewValues { get; set; } = new();

    /// <summary>
    /// Timestamp of the change
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// User who made the change
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Tenant ID for multi-tenancy
    /// </summary>
    public Guid? TenantId { get; set; }
}

/// <summary>
/// Database query statistics
/// </summary>
public class DbQueryStatistics
{
    /// <summary>
    /// Total number of queries executed
    /// </summary>
    public long TotalQueries { get; set; }

    /// <summary>
    /// Average query execution time
    /// </summary>
    public TimeSpan AverageQueryTime { get; set; }

    /// <summary>
    /// Longest query execution time
    /// </summary>
    public TimeSpan LongestQueryTime { get; set; }

    /// <summary>
    /// Number of failed queries
    /// </summary>
    public long FailedQueries { get; set; }

    /// <summary>
    /// Statistics collection start time
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Most frequent query types
    /// </summary>
    public Dictionary<string, long> QueryTypeCounts { get; set; } = new();

    /// <summary>
    /// Slow queries (queries that took longer than threshold)
    /// </summary>
    public List<SlowQueryInfo> SlowQueries { get; set; } = new();
}

/// <summary>
/// Information about slow queries
/// </summary>
public class SlowQueryInfo
{
    /// <summary>
    /// Query SQL
    /// </summary>
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// Query execution time
    /// </summary>
    public TimeSpan ExecutionTime { get; set; }

    /// <summary>
    /// Query parameters
    /// </summary>
    public Dictionary<string, object?>? Parameters { get; set; }

    /// <summary>
    /// When the query was executed
    /// </summary>
    public DateTime ExecutedAt { get; set; }

    /// <summary>
    /// Stack trace (if available)
    /// </summary>
    public string? StackTrace { get; set; }
}
