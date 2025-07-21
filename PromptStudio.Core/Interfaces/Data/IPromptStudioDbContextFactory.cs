using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Interfaces.Data;

namespace PromptStudio.Core.Interfaces.Data;

/// <summary>
/// Factory interface for creating PromptStudio database context instances.
/// Provides controlled creation of DbContext instances for various scenarios including
/// multi-tenancy, background services, and parallel processing operations.
/// 
/// <para><strong>Use Cases:</strong></para>
/// <para>This factory is essential for scenarios requiring multiple DbContext instances,
/// long-running background services, parallel processing operations, or multi-tenant
/// applications with dynamic connection strings per organization.</para>
/// 
/// <para><strong>Benefits:</strong></para>
/// <para>Enables proper DbContext lifecycle management in complex scenarios, supports
/// multi-tenant architecture with organization-specific contexts, and provides clean
/// separation for testing different database configurations.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Register as Singleton in DI container for factory pattern</description></item>
/// <item><description>DbContext instances should be disposed after use (using statements)</description></item>
/// <item><description>Support both default and tenant-specific context creation</description></item>
/// <item><description>Enable configuration-based connection string selection</description></item>
/// <item><description>Provide testing overrides for test database scenarios</description></item>
/// </list>
/// 
/// <para><strong>Multi-tenant Considerations:</strong></para>
/// <list type="bullet">
/// <item><description>Different database per tenant (isolated data)</description></item>
/// <item><description>Single database with tenant filtering (shared infrastructure)</description></item>
/// <item><description>Dynamic connection string resolution based on organization</description></item>
/// <item><description>Proper user and organization context injection</description></item>
/// </list>
/// </remarks>
public interface IPromptStudioDbContextFactory
{
    /// <summary>
    /// Creates a new PromptStudio database context instance with default configuration.
    /// Uses the default connection string and current user context.
    /// </summary>
    /// <returns>A new configured database context instance</returns>
    /// <exception cref="InvalidOperationException">Thrown when context creation fails</exception>
    /// <remarks>
    /// <para><strong>Usage Pattern:</strong></para>
    /// <code>
    /// using var context = _contextFactory.CreateDbContext();
    /// var templates = await context.PromptTemplates.ToListAsync();
    /// </code>
    /// 
    /// <para><strong>Important:</strong></para>
    /// <para>The returned context must be disposed after use. Use 'using' statements
    /// or manual disposal to prevent memory leaks and connection pool exhaustion.</para>
    /// </remarks>
    IPromptStudioDbContext CreateDbContext();

    /// <summary>
    /// Creates a new PromptStudio database context instance with specific user context.
    /// Enables audit trail functionality with proper user attribution.
    /// </summary>
    /// <param name="userId">User identifier for audit trail purposes</param>
    /// <returns>A new configured database context instance with user context</returns>
    /// <exception cref="ArgumentException">Thrown when userId is null or empty</exception>
    /// <exception cref="InvalidOperationException">Thrown when context creation fails</exception>
    /// <remarks>
    /// <para><strong>Audit Benefits:</strong></para>
    /// <para>All entity changes (Create, Update, Delete) will be automatically attributed
    /// to the specified user in audit fields (CreatedBy, UpdatedBy).</para>
    /// 
    /// <para><strong>Usage Example:</strong></para>
    /// <code>
    /// using var context = _contextFactory.CreateDbContext(currentUserId);
    /// var template = new PromptTemplate { Name = "Test" };
    /// context.PromptTemplates.Add(template);
    /// await context.SaveChangesAsync(); // CreatedBy will be set to currentUserId
    /// </code>
    /// </remarks>
    IPromptStudioDbContext CreateDbContext(string userId);

    /// <summary>
    /// Creates a new PromptStudio database context instance with full multi-tenant context.
    /// Enables proper tenant isolation and audit functionality.
    /// </summary>
    /// <param name="userId">User identifier for audit trail purposes</param>
    /// <param name="organizationId">Organization identifier for multi-tenant isolation</param>
    /// <returns>A new configured database context instance with full context</returns>
    /// <exception cref="ArgumentException">Thrown when userId is null or empty</exception>
    /// <exception cref="ArgumentException">Thrown when organizationId is empty</exception>
    /// <exception cref="InvalidOperationException">Thrown when context creation fails</exception>
    /// <remarks>
    /// <para><strong>Multi-tenant Benefits:</strong></para>
    /// <list type="bullet">
    /// <item><description>Automatic tenant filtering through global query filters</description></item>
    /// <item><description>New entities automatically tagged with organization context</description></item>
    /// <item><description>Complete audit trail with user and organization attribution</description></item>
    /// <item><description>Data isolation ensuring no cross-tenant data leakage</description></item>
    /// </list>
    /// 
    /// <para><strong>Security Considerations:</strong></para>
    /// <para>The provided organizationId must be validated against user permissions
    /// before creating the context to prevent unauthorized cross-tenant access.</para>
    /// </remarks>
    IPromptStudioDbContext CreateDbContext(string userId, Guid organizationId);

    /// <summary>
    /// Creates a new PromptStudio database context instance with custom connection configuration.
    /// Enables dynamic database selection for advanced scenarios like read replicas or tenant-specific databases.
    /// </summary>
    /// <param name="connectionString">Custom database connection string</param>
    /// <param name="userId">User identifier for audit trail purposes</param>
    /// <param name="organizationId">Organization identifier for multi-tenant isolation</param>
    /// <returns>A new configured database context instance with custom connection</returns>
    /// <exception cref="ArgumentException">Thrown when connectionString is null or empty</exception>
    /// <exception cref="ArgumentException">Thrown when userId is null or empty</exception>
    /// <exception cref="InvalidOperationException">Thrown when context creation fails</exception>
    /// <remarks>
    /// <para><strong>Advanced Scenarios:</strong></para>
    /// <list type="bullet">
    /// <item><description>Read replica usage for query-heavy operations</description></item>
    /// <item><description>Tenant-specific databases (database per tenant model)</description></item>
    /// <item><description>Test database connections for integration testing</description></item>
    /// <item><description>Disaster recovery or failover database connections</description></item>
    /// </list>
    /// 
    /// <para><strong>Security Warning:</strong></para>
    /// <para>Connection strings should be validated and sanitized before use.
    /// Never accept connection strings directly from untrusted user input.</para>
    /// </remarks>
    IPromptStudioDbContext CreateDbContext(string connectionString, string userId, Guid? organizationId = null);

    /// <summary>
    /// Creates a new read-only PromptStudio database context instance optimized for queries.
    /// Useful for reporting, analytics, and read-heavy operations that don't need change tracking.
    /// </summary>
    /// <param name="userId">User identifier for audit trail purposes</param>
    /// <param name="organizationId">Organization identifier for multi-tenant isolation</param>
    /// <returns>A new read-only configured database context instance</returns>
    /// <exception cref="ArgumentException">Thrown when userId is null or empty</exception>
    /// <exception cref="InvalidOperationException">Thrown when context creation fails</exception>
    /// <remarks>
    /// <para><strong>Performance Benefits:</strong></para>
    /// <list type="bullet">
    /// <item><description>Disabled change tracking for better query performance</description></item>
    /// <item><description>Optimized for read-only scenarios like reporting and analytics</description></item>
    /// <item><description>Reduced memory usage for large data set processing</description></item>
    /// <item><description>Can be configured to use read replica connections</description></item>
    /// </list>
    /// 
    /// <para><strong>Limitations:</strong></para>
    /// <para>SaveChanges operations will throw exceptions. Use only for read operations.</para>
    /// </remarks>
    IPromptStudioDbContext CreateReadOnlyDbContext(string userId, Guid organizationId);
}
