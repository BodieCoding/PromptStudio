using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces.Data;

/// <summary>
/// Interface for the PromptStudio database context providing data access abstraction.
/// Enables dependency injection, unit testing, and loose coupling between business services and data layer.
/// 
/// <para><strong>Architecture Benefits:</strong></para>
/// <para>This interface provides clean separation between business logic and data persistence,
/// enabling comprehensive unit testing through mocking, flexible deployment scenarios with
/// different database providers, and maintainable code through dependency injection patterns.</para>
/// 
/// <para><strong>Testing Benefits:</strong></para>
/// <para>Business services can be unit tested without database dependencies by mocking this interface,
/// significantly improving test performance and reliability while enabling true isolated unit tests.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
/// <item><description>Implementations must support multi-tenancy through organization isolation</description></item>
/// <item><description>All entities must implement audit trail functionality (CreatedAt, UpdatedAt, etc.)</description></item>
/// <item><description>Soft delete patterns must be supported through global query filters</description></item>
/// <item><description>Transaction management must be available for complex operations</description></item>
/// <item><description>Performance monitoring and connection management should be provided</description></item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Inject into business services for data access operations</description></item>
/// <item><description>Use Repository pattern on top of this interface for additional abstraction</description></item>
/// <item><description>Mock this interface for unit testing business logic</description></item>
/// <item><description>Leverage for integration testing with test database instances</description></item>
/// </list>
/// </remarks>
public interface IPromptStudioDbContext : IDisposable
{
    #region Core Domain Entities

    /// <summary>
    /// Prompt Labs - Top-level organizational units for prompt management
    /// </summary>
    DbSet<PromptLab> PromptLabs { get; set; }

    /// <summary>
    /// Prompt Libraries - Organized collections within labs for categorization
    /// </summary>
    DbSet<PromptLibrary> PromptLibraries { get; set; }

    /// <summary>
    /// Prompt Templates - Reusable prompts with variable placeholders
    /// </summary>
    DbSet<PromptTemplate> PromptTemplates { get; set; }

    /// <summary>
    /// Prompt Variables - Template variable definitions and constraints
    /// </summary>
    DbSet<PromptVariable> PromptVariables { get; set; }

    /// <summary>
    /// Prompt Executions - Execution history and results for analytics
    /// </summary>
    DbSet<PromptExecution> PromptExecutions { get; set; }

    /// <summary>
    /// Variable Collections - Batch variable sets for testing and automation
    /// </summary>
    DbSet<VariableCollection> VariableCollections { get; set; }

    #endregion

    #region Workflow Engine Entities

    /// <summary>
    /// Prompt Flows - Visual workflow definitions for complex operations
    /// </summary>
    DbSet<PromptFlow> PromptFlows { get; set; }

    /// <summary>
    /// Workflow Libraries - Organizational structure for workflows
    /// </summary>
    DbSet<WorkflowLibrary> WorkflowLibraries { get; set; }

    /// <summary>
    /// Workflow Categories - Flexible categorization system for workflows
    /// </summary>
    DbSet<WorkflowCategory> WorkflowCategories { get; set; }

    /// <summary>
    /// Flow Nodes - Individual workflow components and their configurations
    /// </summary>
    DbSet<FlowNode> FlowNodes { get; set; }

    /// <summary>
    /// Flow Edges - Connections between workflow nodes for flow control
    /// </summary>
    DbSet<FlowEdge> FlowEdges { get; set; }

    /// <summary>
    /// Flow Executions - Workflow execution instances with status tracking
    /// </summary>
    DbSet<FlowExecution> FlowExecutions { get; set; }

    /// <summary>
    /// Node Executions - Individual node execution records for debugging
    /// </summary>
    DbSet<NodeExecution> NodeExecutions { get; set; }

    /// <summary>
    /// Edge Traversals - Edge traversal records during execution for analytics
    /// </summary>
    DbSet<EdgeTraversal> EdgeTraversals { get; set; }

    #endregion

    #region Content and Versioning

    /// <summary>
    /// Prompt Content - Separates content from metadata for performance optimization
    /// </summary>
    DbSet<PromptContent> PromptContents { get; set; }

    /// <summary>
    /// Template Versions - Version history for templates with change tracking
    /// </summary>
    DbSet<TemplateVersion> TemplateVersions { get; set; }

    #endregion

    #region Security and Permissions

    /// <summary>
    /// Library Permissions - Granular access control for libraries
    /// </summary>
    DbSet<LibraryPermission> LibraryPermissions { get; set; }

    /// <summary>
    /// Template Permissions - Granular access control for templates
    /// </summary>
    DbSet<TemplatePermission> TemplatePermissions { get; set; }

    /// <summary>
    /// Workflow Library Permissions - Granular access control for workflow libraries
    /// </summary>
    DbSet<WorkflowLibraryPermission> WorkflowLibraryPermissions { get; set; }

    #endregion

    #region Analytics and Testing

    /// <summary>
    /// A/B Tests - Testing framework for prompts and workflows
    /// </summary>
    DbSet<ABTest> ABTests { get; set; }

    /// <summary>
    /// A/B Test Variants - Individual test variations with configuration
    /// </summary>
    DbSet<ABTestVariant> ABTestVariants { get; set; }

    /// <summary>
    /// A/B Test Results - Captured metrics and outcomes for analysis
    /// </summary>
    DbSet<ABTestResult> ABTestResults { get; set; }

    /// <summary>
    /// Quality Metrics - Quality measurements for templates and workflows
    /// </summary>
    DbSet<QualityMetric> QualityMetrics { get; set; }

    #endregion

    #region Configuration and Management

    /// <summary>
    /// Model Provider Configurations - AI model provider settings and credentials
    /// </summary>
    DbSet<ModelProviderConfig> ModelProviderConfigs { get; set; }

    #endregion

    #region Core EF Core Operations

    /// <summary>
    /// Asynchronously saves all changes made in this context to the database.
    /// Automatically handles audit field updates (CreatedAt, UpdatedAt, CreatedBy, UpdatedBy).
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>The number of state entries written to the database</returns>
    /// <exception cref="DbUpdateException">Thrown when an error occurs updating the database</exception>
    /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency violation is encountered</exception>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// Automatically handles audit field updates (CreatedAt, UpdatedAt, CreatedBy, UpdatedBy).
    /// </summary>
    /// <returns>The number of state entries written to the database</returns>
    /// <exception cref="DbUpdateException">Thrown when an error occurs updating the database</exception>
    /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency violation is encountered</exception>
    int SaveChanges();

    #endregion

    #region Entity Framework Context Properties

    /// <summary>
    /// Provides access to database-related information and operations for this context.
    /// Enables transaction management, connection control, and database operations.
    /// </summary>
    DatabaseFacade Database { get; }

    /// <summary>
    /// Provides access to change tracking information and operations for entity instances.
    /// Essential for audit operations and understanding entity state changes.
    /// </summary>
    ChangeTracker ChangeTracker { get; }

    #endregion

    #region Transaction Management

    /// <summary>
    /// Execute multiple operations within a single database transaction.
    /// Provides atomicity for complex operations spanning multiple entities.
    /// </summary>
    /// <typeparam name="T">The type of result returned by the operation</typeparam>
    /// <param name="operation">The operation to execute within the transaction</param>
    /// <returns>The result of the operation</returns>
    /// <exception cref="InvalidOperationException">Thrown when transaction management fails</exception>
    /// <remarks>
    /// <para><strong>Usage Example:</strong></para>
    /// <code>
    /// var result = await _context.ExecuteInTransactionAsync(async () =>
    /// {
    ///     // Multiple database operations here
    ///     await _context.PromptTemplates.AddAsync(template);
    ///     await _context.PromptVariables.AddRangeAsync(variables);
    ///     await _context.SaveChangesAsync();
    ///     return template.Id;
    /// });
    /// </code>
    /// </remarks>
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);

    #endregion

    #region Performance and Configuration

    /// <summary>
    /// Configure command timeout for long-running database operations.
    /// Useful for bulk operations, complex queries, or maintenance tasks.
    /// </summary>
    /// <param name="timeoutSeconds">Timeout in seconds for database commands</param>
    /// <remarks>
    /// <para><strong>Usage Guidelines:</strong></para>
    /// <list type="bullet">
    /// <item><description>Use for bulk import/export operations</description></item>
    /// <item><description>Increase for complex analytical queries</description></item>
    /// <item><description>Set to appropriate values for migration operations</description></item>
    /// </list>
    /// </remarks>
    void SetCommandTimeout(int timeoutSeconds);

    /// <summary>
    /// Get database connection statistics and health information.
    /// Provides monitoring data for performance analysis and debugging.
    /// </summary>
    /// <returns>Dictionary containing connection state, provider info, and context metadata</returns>
    /// <exception cref="InvalidOperationException">Thrown when connection information is unavailable</exception>
    /// <remarks>
    /// <para><strong>Returned Information:</strong></para>
    /// <list type="bullet">
    /// <item><description>ConnectionState - Current database connection state</description></item>
    /// <item><description>ProviderName - Database provider being used</description></item>
    /// <item><description>DatabaseName - Name of the connected database</description></item>
    /// <item><description>CurrentUser - Current user context for audit purposes</description></item>
    /// <item><description>OrganizationId - Current organization for multi-tenant scenarios</description></item>
    /// </list>
    /// </remarks>
    Task<Dictionary<string, object>> GetConnectionInfoAsync();

    #endregion
}
