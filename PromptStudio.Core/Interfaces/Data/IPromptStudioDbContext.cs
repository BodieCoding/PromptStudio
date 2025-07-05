using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces.Data;

/// <summary>
/// Interface for the PromptStudio database context providing enterprise-grade data access operations.
/// This interface defines the contract for database operations across all domain entities,
/// supporting multi-tenancy, audit trails, soft deletion, and comprehensive entity management.
/// </summary>
/// <remarks>
/// This interface is designed to support:
/// <list type="bullet">
/// <item><description>Multi-tenant architecture with tenant isolation</description></item>
/// <item><description>Comprehensive audit logging and change tracking</description></item>
/// <item><description>Soft deletion capabilities across all entities</description></item>
/// <item><description>Advanced querying and filtering operations</description></item>
/// <item><description>Enterprise-grade transaction management</description></item>
/// <item><description>Performance optimization through strategic indexing</description></item>
/// <item><description>Data governance and compliance features</description></item>
/// </list>
/// </remarks>
public interface IPromptStudioDbContext : IDisposable, IAsyncDisposable
{
    #region Lab Management Entities
    
    /// <summary>
    /// Gets or sets the collection of prompt labs.
    /// Prompt labs serve as the top-level organizational units containing libraries and workflows.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="PromptLab"/> entities.</value>
    /// <remarks>
    /// Prompt labs provide:
    /// <list type="bullet">
    /// <item><description>Organizational hierarchy and workspace separation</description></item>
    /// <item><description>Access control and permission boundaries</description></item>
    /// <item><description>Resource allocation and quota management</description></item>
    /// <item><description>Collaboration and sharing capabilities</description></item>
    /// </list>
    /// </remarks>
    DbSet<PromptLab> PromptLabs { get; set; }

    #endregion

    #region Library Management Entities
    
    /// <summary>
    /// Gets or sets the collection of prompt libraries.
    /// Libraries organize prompt templates by domain, purpose, or team ownership.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="PromptLibrary"/> entities.</value>
    /// <remarks>
    /// Prompt libraries enable:
    /// <list type="bullet">
    /// <item><description>Categorization and organization of prompt templates</description></item>
    /// <item><description>Team-based collaboration and sharing</description></item>
    /// <item><description>Version control and template lifecycle management</description></item>
    /// <item><description>Discovery and reuse of proven prompts</description></item>
    /// </list>
    /// </remarks>
    DbSet<PromptLibrary> PromptLibraries { get; set; }

    /// <summary>
    /// Gets or sets the collection of library categories.
    /// Categories provide hierarchical organization and taxonomy for prompt libraries.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="LibraryCategory"/> entities.</value>
    /// <remarks>
    /// Library categories support:
    /// <list type="bullet">
    /// <item><description>Hierarchical organization with parent-child relationships</description></item>
    /// <item><description>Taxonomy-based navigation and discovery</description></item>
    /// <item><description>Automated categorization and tagging</description></item>
    /// <item><description>Cross-library content organization</description></item>
    /// </list>
    /// </remarks>

    DbSet<LibraryCategory> LibraryCategories { get; set; }

    /// <summary>
    /// Gets or sets the collection of library permissions.
    /// Defines granular access control for library operations and content.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="LibraryPermission"/> entities.</value>
    /// <remarks>
    /// Library permissions provide:
    /// <list type="bullet">
    /// <item><description>Role-based access control (RBAC)</description></item>
    /// <item><description>Granular operation-level permissions</description></item>
    /// <item><description>Inheritance and delegation capabilities</description></item>
    /// <item><description>Audit trails for permission changes</description></item>
    /// </list>
    /// </remarks>
    DbSet<LibraryPermission> LibraryPermissions { get; set; }

    #endregion

    #region Template Management Entities
    
    /// <summary>
    /// Gets or sets the collection of prompt templates.
    /// Templates are the core reusable prompt definitions with variable placeholders.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="PromptTemplate"/> entities.</value>
    /// <remarks>
    /// Prompt templates enable:
    /// <list type="bullet">
    /// <item><description>Standardized prompt structures with variables</description></item>
    /// <item><description>Reusability across different contexts and use cases</description></item>
    /// <item><description>Version control and change management</description></item>
    /// <item><description>Performance optimization and caching</description></item>
    /// <item><description>Quality assurance and testing frameworks</description></item>
    /// </list>
    /// </remarks>
    DbSet<PromptTemplate> PromptTemplates { get; set; }

    /// <summary>
    /// Gets or sets the collection of template versions.
    /// Maintains complete version history and change tracking for prompt templates.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="TemplateVersion"/> entities.</value>
    /// <remarks>
    /// Template versions support:
    /// <list type="bullet">
    /// <item><description>Complete change history and audit trails</description></item>
    /// <item><description>Rollback and restoration capabilities</description></item>
    /// <item><description>A/B testing and performance comparison</description></item>
    /// <item><description>Approval workflows and governance</description></item>
    /// </list>
    /// </remarks>
    DbSet<TemplateVersion> TemplateVersions { get; set; }

    /// <summary>
    /// Gets or sets the collection of template permissions.
    /// Defines fine-grained access control for individual prompt templates.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="TemplatePermission"/> entities.</value>
    /// <remarks>
    /// Template permissions provide:
    /// <list type="bullet">
    /// <item><description>Individual template access control</description></item>
    /// <item><description>Operation-specific permissions (read, write, execute, delete)</description></item>
    /// <item><description>Delegation and sharing capabilities</description></item>
    /// <item><description>Compliance and governance enforcement</description></item>
    /// </list>
    /// </remarks>
    DbSet<TemplatePermission> TemplatePermissions { get; set; }

    /// <summary>
    /// Gets or sets the collection of prompt content.
    /// Stores the actual content and metadata for prompt templates.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="PromptContent"/> entities.</value>
    /// <remarks>
    /// Prompt content manages:
    /// <list type="bullet">
    /// <item><description>Structured content storage with metadata</description></item>
    /// <item><description>Content validation and quality checks</description></item>
    /// <item><description>Localization and internationalization</description></item>
    /// <item><description>Content optimization and performance tuning</description></item>
    /// </list>
    /// </remarks>
    DbSet<PromptContent> PromptContents { get; set; }

    #endregion

    #region Variable Management Entities
    
    /// <summary>
    /// Gets or sets the collection of prompt variables.
    /// Defines the variable schema and constraints for prompt templates.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="PromptVariable"/> entities.</value>
    /// <remarks>
    /// Prompt variables enable:
    /// <list type="bullet">
    /// <item><description>Typed variable definitions with validation rules</description></item>
    /// <item><description>Default values and constraint enforcement</description></item>
    /// <item><description>Documentation and help text for users</description></item>
    /// <item><description>Dynamic validation and preprocessing</description></item>
    /// </list>
    /// </remarks>
    DbSet<PromptVariable> PromptVariables { get; set; }

    /// <summary>
    /// Gets or sets the collection of variable collections.
    /// Organizes sets of variable values for batch processing and testing.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="VariableCollection"/> entities.</value>
    /// <remarks>
    /// Variable collections support:
    /// <list type="bullet">
    /// <item><description>Bulk execution with predefined variable sets</description></item>
    /// <item><description>Test data management and scenarios</description></item>
    /// <item><description>Import/export capabilities for external data</description></item>
    /// <item><description>Performance benchmarking and comparison</description></item>
    /// </list>
    /// </remarks>
    DbSet<VariableCollection> VariableCollections { get; set; }

    #endregion

    #region Execution Management Entities
    
    /// <summary>
    /// Gets or sets the collection of prompt executions.
    /// Records all prompt execution instances with inputs, outputs, and metadata.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="PromptExecution"/> entities.</value>
    /// <remarks>
    /// Prompt executions provide:
    /// <list type="bullet">
    /// <item><description>Complete execution history and audit trails</description></item>
    /// <item><description>Performance metrics and cost tracking</description></item>
    /// <item><description>Error logging and debugging information</description></item>
    /// <item><description>Usage analytics and optimization insights</description></item>
    /// </list>
    /// </remarks>
    DbSet<PromptExecution> PromptExecutions { get; set; }

    #endregion

    #region Workflow Management Entities
    
    /// <summary>
    /// Gets or sets the collection of prompt flows.
    /// Defines complex multi-step workflows and orchestration logic.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="PromptFlow"/> entities.</value>
    /// <remarks>
    /// Prompt flows enable:
    /// <list type="bullet">
    /// <item><description>Complex multi-step workflow definition</description></item>
    /// <item><description>Conditional logic and branching</description></item>
    /// <item><description>Integration with external systems</description></item>
    /// <item><description>State management and persistence</description></item>
    /// </list>
    /// </remarks>
    DbSet<PromptFlow> PromptFlows { get; set; }

    /// <summary>
    /// Gets or sets the collection of flow executions.
    /// Records execution instances of prompt flows with state and results.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="FlowExecution"/> entities.</value>
    /// <remarks>
    /// Flow executions track:
    /// <list type="bullet">
    /// <item><description>Workflow execution state and progress</description></item>
    /// <item><description>Step-by-step execution logs</description></item>
    /// <item><description>Error handling and recovery</description></item>
    /// <item><description>Performance metrics and bottlenecks</description></item>
    /// </list>
    /// </remarks>
    DbSet<FlowExecution> FlowExecutions { get; set; }

    /// <summary>
    /// Gets or sets the collection of flow nodes.
    /// Defines individual steps or operations within prompt flows.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="FlowNode"/> entities.</value>
    /// <remarks>
    /// Flow nodes provide:
    /// <list type="bullet">
    /// <item><description>Granular workflow step definition</description></item>
    /// <item><description>Input/output mapping and transformation</description></item>
    /// <item><description>Conditional execution and routing</description></item>
    /// <item><description>Reusable component libraries</description></item>
    /// </list>
    /// </remarks>
    DbSet<FlowNode> FlowNodes { get; set; }

    /// <summary>
    /// Gets or sets the collection of flow variants.
    /// Manages different versions or configurations of workflow components.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="FlowVariant"/> entities.</value>
    /// <remarks>
    /// Flow variants support:
    /// <list type="bullet">
    /// <item><description>A/B testing of workflow configurations</description></item>
    /// <item><description>Environment-specific customizations</description></item>
    /// <item><description>Gradual rollout and deployment strategies</description></item>
    /// <item><description>Performance comparison and optimization</description></item>
    /// </list>
    /// </remarks>
    DbSet<FlowVariant> FlowVariants { get; set; }

    /// <summary>
    /// Gets or sets the collection of flow edges.
    /// Defines the connections and transitions between flow nodes in workflows.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="FlowEdge"/> entities.</value>
    /// <remarks>
    /// Flow edges provide:
    /// <list type="bullet">
    /// <item><description>Workflow routing and navigation logic</description></item>
    /// <item><description>Conditional transitions and branching</description></item>
    /// <item><description>Data flow and parameter passing</description></item>
    /// <item><description>Execution sequence control</description></item>
    /// </list>
    /// </remarks>
    DbSet<FlowEdge> FlowEdges { get; set; }

    /// <summary>
    /// Gets or sets the collection of node executions.
    /// Tracks individual node execution instances within workflow runs.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="NodeExecution"/> entities.</value>
    /// <remarks>
    /// Node executions track:
    /// <list type="bullet">
    /// <item><description>Individual step execution state and results</description></item>
    /// <item><description>Performance metrics for each node</description></item>
    /// <item><description>Error handling and retry logic</description></item>
    /// <item><description>Debugging and monitoring information</description></item>
    /// </list>
    /// </remarks>
    DbSet<NodeExecution> NodeExecutions { get; set; }

    /// <summary>
    /// Gets or sets the collection of workflow categories.
    /// Organizes workflows by domain, complexity, or business function.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="WorkflowCategory"/> entities.</value>
    /// <remarks>
    /// Workflow categories enable:
    /// <list type="bullet">
    /// <item><description>Hierarchical organization of workflows</description></item>
    /// <item><description>Discovery and navigation capabilities</description></item>
    /// <item><description>Template libraries and reuse patterns</description></item>
    /// <item><description>Governance and compliance classification</description></item>
    /// </list>
    /// </remarks>
    DbSet<WorkflowCategory> WorkflowCategories { get; set; }

    /// <summary>
    /// Gets or sets the collection of workflow library permissions.
    /// Defines access control for workflow libraries and templates.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="WorkflowLibraryPermission"/> entities.</value>
    /// <remarks>
    /// Workflow library permissions provide:
    /// <list type="bullet">
    /// <item><description>Granular access control for workflow resources</description></item>
    /// <item><description>Team-based collaboration and sharing</description></item>
    /// <item><description>Security and compliance enforcement</description></item>
    /// <item><description>Audit trails and governance reporting</description></item>
    /// </list>
    /// </remarks>
    DbSet<WorkflowLibraryPermission> WorkflowLibraryPermissions { get; set; }

    /// <summary>
    /// Gets or sets the collection of workflow template usage.
    /// Tracks usage patterns and analytics for workflow templates.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="WorkflowTemplateUsage"/> entities.</value>
    /// <remarks>
    /// Workflow template usage tracks:
    /// <list type="bullet">
    /// <item><description>Usage frequency and patterns</description></item>
    /// <item><description>Performance metrics and optimization opportunities</description></item>
    /// <item><description>User adoption and success rates</description></item>
    /// <item><description>Cost analysis and resource utilization</description></item>
    /// </list>
    /// </remarks>
    DbSet<WorkflowTemplateUsage> WorkflowTemplateUsages { get; set; }

    #endregion

    #region Testing and Experimentation Entities
    
    /// <summary>
    /// Gets or sets the collection of A/B tests.
    /// Manages experimental comparisons between different prompt variants.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="ABTest"/> entities.</value>
    /// <remarks>
    /// A/B tests enable:
    /// <list type="bullet">
    /// <item><description>Scientific comparison of prompt variations</description></item>
    /// <item><description>Statistical significance testing</description></item>
    /// <item><description>Performance and quality optimization</description></item>
    /// <item><description>Data-driven decision making</description></item>
    /// </list>
    /// </remarks>
    DbSet<ABTest> ABTests { get; set; }

    /// <summary>
    /// Gets or sets the collection of A/B test variants.
    /// Defines the different versions being compared in experiments.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="ABTestVariant"/> entities.</value>
    /// <remarks>
    /// A/B test variants provide:
    /// <list type="bullet">
    /// <item><description>Multiple version comparison capabilities</description></item>
    /// <item><description>Traffic allocation and distribution</description></item>
    /// <item><description>Performance isolation and measurement</description></item>
    /// <item><description>Gradual rollout and risk mitigation</description></item>
    /// </list>
    /// </remarks>
    DbSet<ABTestVariant> ABTestVariants { get; set; }

    /// <summary>
    /// Gets or sets the collection of A/B test results.
    /// Stores experimental outcomes and statistical analysis.
    /// </summary>
    /// <value>A <see cref="DbSet{TEntity}"/> of <see cref="ABTestResult"/> entities.</value>
    /// <remarks>
    /// A/B test results capture:
    /// <list type="bullet">
    /// <item><description>Statistical significance and confidence intervals</description></item>
    /// <item><description>Performance metrics and quality measures</description></item>
    /// <item><description>User feedback and satisfaction scores</description></item>
    /// <item><description>Business impact and ROI analysis</description></item>
    /// </list>
    /// </remarks>
    DbSet<ABTestResult> ABTestResults { get; set; }

    #endregion

    #region Data Access Operations

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous save operation. The task result contains
    /// the number of state entries written to the underlying database.
    /// </returns>
    /// <remarks>
    /// This method automatically handles:
    /// <list type="bullet">
    /// <item><description>Audit trail creation for all entity changes</description></item>
    /// <item><description>Soft deletion enforcement where applicable</description></item>
    /// <item><description>Multi-tenant data isolation</description></item>
    /// <item><description>Optimistic concurrency control</description></item>
    /// <item><description>Transaction management and rollback</description></item>
    /// </list>
    /// </remarks>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Synchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <returns>The number of state entries written to the underlying database.</returns>
    /// <remarks>
    /// This method automatically handles:
    /// <list type="bullet">
    /// <item><description>Audit trail creation for all entity changes</description></item>
    /// <item><description>Soft deletion enforcement where applicable</description></item>
    /// <item><description>Multi-tenant data isolation</description></item>
    /// <item><description>Optimistic concurrency control</description></item>
    /// <item><description>Transaction management and rollback</description></item>
    /// </list>
    /// </remarks>
    int SaveChanges();

    #endregion

    #region Change Tracking and Audit

    /// <summary>
    /// Gets the change tracker that manages entity state and change detection.
    /// </summary>
    /// <value>The <see cref="ChangeTracker"/> instance for this context.</value>
    /// <remarks>
    /// The change tracker provides:
    /// <list type="bullet">
    /// <item><description>Entity state management and monitoring</description></item>
    /// <item><description>Change detection and notification</description></item>
    /// <item><description>Conflict resolution and optimistic concurrency</description></item>
    /// <item><description>Performance optimization through selective tracking</description></item>
    /// </list>
    /// </remarks>
    ChangeTracker ChangeTracker { get; }

    #endregion

    #region Entity Management

    /// <summary>
    /// Creates a <see cref="DbSet{TEntity}"/> that can be used to query and save instances of <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
    /// <returns>A set for the given entity type.</returns>
    /// <remarks>
    /// This method enables:
    /// <list type="bullet">
    /// <item><description>Generic entity access patterns</description></item>
    /// <item><description>Repository pattern implementation</description></item>
    /// <item><description>Dynamic entity type handling</description></item>
    /// <item><description>Consistent data access interfaces</description></item>
    /// </list>
    /// </remarks>
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    #endregion

    #region Advanced Querying Support

    /// <summary>
    /// Executes the given SQL against the database and returns the number of rows affected.
    /// </summary>
    /// <param name="sql">The SQL command to execute.</param>
    /// <param name="parameters">Parameters to apply to the SQL command.</param>
    /// <returns>The number of rows affected.</returns>
    /// <remarks>
    /// This method supports:
    /// <list type="bullet">
    /// <item><description>Direct SQL execution for complex operations</description></item>
    /// <item><description>Parameterized queries for security</description></item>
    /// <item><description>Bulk operations and performance optimization</description></item>
    /// <item><description>Legacy system integration</description></item>
    /// </list>
    /// </remarks>
    Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);

    /// <summary>
    /// Executes the given SQL against the database and returns the results as a collection.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the result.</typeparam>
    /// <param name="sql">The SQL query to execute.</param>
    /// <param name="parameters">Parameters to apply to the SQL query.</param>
    /// <returns>An <see cref="IQueryable{T}"/> representing the results.</returns>
    /// <remarks>
    /// This method enables:
    /// <list type="bullet">
    /// <item><description>Complex analytical queries</description></item>
    /// <item><description>Reporting and business intelligence</description></item>
    /// <item><description>Performance-optimized data retrieval</description></item>
    /// <item><description>Integration with external data sources</description></item>
    /// </list>
    /// </remarks>
    IQueryable<T> FromSqlRaw<T>(string sql, params object[] parameters) where T : class;

    #endregion

    #region Transaction Management

    /// <summary>
    /// Begins a new transaction with the specified isolation level.
    /// </summary>
    /// <param name="isolationLevel">The isolation level for the transaction.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation that returns the transaction.</returns>
    /// <remarks>
    /// Transaction management provides:
    /// <list type="bullet">
    /// <item><description>ACID compliance and data consistency</description></item>
    /// <item><description>Isolation level control for concurrent operations</description></item>
    /// <item><description>Deadlock detection and resolution</description></item>
    /// <item><description>Distributed transaction coordination</description></item>
    /// </list>
    /// </remarks>
    Task<Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction> BeginTransactionAsync(
        System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);

    #endregion
}
