using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Variables;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.Interfaces.Variables;

/// <summary>
/// Enterprise-grade service interface for comprehensive variable management, including variable definition, 
/// validation, type management, and integration with template systems within the PromptStudio platform.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// Core service layer component responsible for all variable-related operations including CRUD operations,
/// validation, type management, and integration with template and execution systems. Implements enterprise
/// patterns for multi-tenancy, audit trails, soft deletes, and comprehensive business rule enforcement
/// for variable lifecycle management and template integration workflows.
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// Implements comprehensive variable management with strict business rule enforcement, data validation,
/// and integration patterns. Supports variable definition, type management, validation rules, and
/// template integration with full audit capabilities, tenant isolation, and performance optimization
/// for high-volume variable operations and template execution scenarios.
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
///   <item>All operations must enforce tenant isolation and security boundaries</item>
///   <item>Variable validation must include type checking, constraint validation, and business rules</item>
///   <item>Implement comprehensive audit logging for all variable operations</item>
///   <item>Support variable versioning and change tracking for template compatibility</item>
///   <item>Optimize for high-frequency variable access during template execution</item>
///   <item>Implement caching strategies for frequently accessed variable definitions</item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
///   <item>Integrates with IPromptTemplateService for variable discovery and validation</item>
///   <item>Coordinates with execution services for variable resolution and validation</item>
///   <item>Works with audit services for comprehensive change tracking</item>
///   <item>Supports notification services for variable change alerts</item>
///   <item>Implements Unit of Work pattern for transactional variable operations</item>
///   <item>Uses repository pattern for optimized variable data access</item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
///   <item>Mock variable validation for unit testing template operations</item>
///   <item>Test variable type conversion and constraint validation thoroughly</item>
///   <item>Verify tenant isolation in multi-tenant test scenarios</item>
///   <item>Performance test variable operations under high concurrency</item>
///   <item>Test variable change impact on dependent templates</item>
/// </list>
/// </remarks>
public interface IVariableManagementService
{
    #region Variable CRUD Operations

    /// <summary>
    /// Creates a new prompt variable with comprehensive validation and type checking.
    /// Enforces business rules for variable naming, type constraints, and template integration requirements.
    /// </summary>
    /// <param name="createRequest">Variable creation request with validation details</param>
    /// <param name="tenantId">Tenant identifier for multi-tenant isolation</param>
    /// <param name="createdBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Created variable entity with generated metadata</returns>
    /// <exception cref="ArgumentException">Thrown when validation rules are violated</exception>
    /// <exception cref="InvalidOperationException">Thrown when business rules prevent creation</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Variable names must be unique within template scope</item>
    ///   <item>Variable types must be supported by the system</item>
    ///   <item>Default values must conform to variable type constraints</item>
    ///   <item>Description is required for documentation and discovery</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates variable name against reserved keywords</item>
    ///   <item>Enforces type-specific validation rules</item>
    ///   <item>Creates audit trail entry for variable creation</item>
    ///   <item>Triggers notifications to dependent systems</item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// var createRequest = new CreateVariableRequest
    /// {
    ///     Name = "customerName",
    ///     Type = VariableType.Text,
    ///     Description = "Customer name for personalization",
    ///     DefaultValue = "Valued Customer"
    /// };
    /// var variable = await service.CreateVariableAsync(createRequest, tenantId, userId);
    /// </code>
    /// </remarks>
    Task<PromptVariable> CreateVariableAsync(
        CreateVariableRequest createRequest, 
        Guid tenantId, 
        Guid createdBy, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a variable by its unique identifier with comprehensive metadata and validation information.
    /// Includes variable definition, type information, constraints, and usage statistics for optimization.
    /// </summary>
    /// <param name="variableId">Unique variable identifier</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Variable entity with metadata, or null if not found or not accessible</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Only variables within tenant scope are accessible</item>
    ///   <item>Soft-deleted variables are excluded from results</item>
    ///   <item>Access permissions are enforced based on tenant membership</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements caching for frequently accessed variables</item>
    ///   <item>Includes eager loading for related entities when needed</item>
    ///   <item>Logs access for usage analytics and optimization</item>
    /// </list>
    /// </remarks>
    Task<PromptVariable?> GetVariableByIdAsync(
        Guid variableId, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves variables associated with a specific template with filtering and pagination support.
    /// Enables efficient variable discovery and management within template development workflows.
    /// </summary>
    /// <param name="templateId">Template identifier for variable association</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="filters">Optional filtering criteria for variable selection</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated list of variables with metadata</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Variables are filtered by tenant and template association</item>
    ///   <item>Results include only active (non-deleted) variables</item>
    ///   <item>Pagination limits are enforced for performance</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Optimized queries for large template variable collections</item>
    ///   <item>Supports advanced filtering by type, usage, and metadata</item>
    ///   <item>Includes variable usage statistics for optimization insights</item>
    /// </list>
    /// </remarks>
    Task<PagedResult<PromptVariable>> GetVariablesByTemplateAsync(
        Guid templateId, 
        Guid tenantId, 
        VariableFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing variable with comprehensive validation and change tracking.
    /// Maintains variable integrity and validates impact on dependent templates and executions.
    /// </summary>
    /// <param name="variableId">Variable identifier to update</param>
    /// <param name="updateRequest">Update request with modified variable data</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="updatedBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Updated variable entity with new metadata</returns>
    /// <exception cref="NotFoundException">Thrown when variable is not found</exception>
    /// <exception cref="ArgumentException">Thrown when validation rules are violated</exception>
    /// <exception cref="InvalidOperationException">Thrown when update conflicts with business rules</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Variable type changes require compatibility validation</item>
    ///   <item>Name changes must maintain uniqueness within template scope</item>
    ///   <item>Default value changes must conform to type constraints</item>
    ///   <item>Updates that break template compatibility require confirmation</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates impact on dependent templates before update</item>
    ///   <item>Creates comprehensive audit trail for change tracking</item>
    ///   <item>Triggers cache invalidation for updated variables</item>
    ///   <item>Notifies dependent systems of variable changes</item>
    /// </list>
    /// </remarks>
    Task<PromptVariable> UpdateVariableAsync(
        Guid variableId, 
        UpdateVariableRequest updateRequest, 
        Guid tenantId, 
        Guid updatedBy, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs soft delete of a variable with dependency validation and cleanup procedures.
    /// Ensures data integrity and validates impact on dependent templates and executions.
    /// </summary>
    /// <param name="variableId">Variable identifier to delete</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="deletedBy">User identifier for audit and tracking</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Operation result with deletion status and impact analysis</returns>
    /// <exception cref="NotFoundException">Thrown when variable is not found</exception>
    /// <exception cref="InvalidOperationException">Thrown when dependencies prevent deletion</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Variables with active template dependencies cannot be hard deleted</item>
    ///   <item>Soft delete preserves data integrity and audit trails</item>
    ///   <item>Deletion impact analysis is provided before confirmation</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Performs dependency analysis before deletion</item>
    ///   <item>Implements soft delete with comprehensive audit trail</item>
    ///   <item>Triggers cleanup of associated cache entries</item>
    ///   <item>Notifies dependent systems of variable removal</item>
    /// </list>
    /// </remarks>
    Task<OperationResult> DeleteVariableAsync(
        Guid variableId, 
        Guid tenantId, 
        Guid deletedBy, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Variable Validation & Type Management

    /// <summary>
    /// Extracts variable names from template content using advanced parsing algorithms.
    /// Identifies variable placeholders, validates syntax, and provides discovery capabilities
    /// for template development and validation workflows.
    /// </summary>
    /// <param name="templateContent">Template content to analyze for variables</param>
    /// <param name="tenantId">Tenant identifier for tenant-specific parsing rules</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Collection of discovered variable names with metadata</returns>
    /// <exception cref="ArgumentException">Thrown when template content is invalid</exception>
    /// <remarks>
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Uses regex and parsing algorithms for variable discovery</item>
    ///   <item>Validates variable name syntax and conventions</item>
    ///   <item>Supports multiple variable placeholder formats</item>
    ///   <item>Provides detailed parsing results with position information</item>
    /// </list>
    /// </remarks>
    Task<VariableDiscoveryResult> ExtractVariablesFromContentAsync(
        string templateContent, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates variable values against defined constraints and business rules.
    /// Performs comprehensive type checking, constraint validation, and business rule enforcement
    /// for variable values within template execution contexts.
    /// </summary>
    /// <param name="variableId">Variable identifier for validation context</param>
    /// <param name="value">Variable value to validate</param>
    /// <param name="tenantId">Tenant identifier for tenant-specific validation rules</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Validation result with detailed error and warning information</returns>
    /// <exception cref="NotFoundException">Thrown when variable is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Values must conform to variable type constraints</item>
    ///   <item>Required variables must have non-empty values</item>
    ///   <item>Enum variables must use valid enumeration values</item>
    ///   <item>Numeric variables must be within defined ranges</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Implements type-specific validation logic</item>
    ///   <item>Supports custom validation rules and constraints</item>
    ///   <item>Provides detailed validation messages for user guidance</item>
    ///   <item>Caches validation rules for performance optimization</item>
    /// </list>
    /// </remarks>
    Task<VariableValidationResult> ValidateVariableValueAsync(
        Guid variableId, 
        string value, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates a complete set of variable values for template execution.
    /// Performs comprehensive validation of all variables required for template execution,
    /// including cross-variable validation and business rule enforcement.
    /// </summary>
    /// <param name="templateId">Template identifier for validation context</param>
    /// <param name="variableValues">Dictionary of variable names and values to validate</param>
    /// <param name="tenantId">Tenant identifier for tenant-specific validation rules</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive validation result with variable-specific errors</returns>
    /// <exception cref="NotFoundException">Thrown when template is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>All required variables must have valid values</item>
    ///   <item>Cross-variable dependencies must be satisfied</item>
    ///   <item>Variable combinations must comply with template rules</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates individual variables and cross-variable rules</item>
    ///   <item>Provides aggregated validation results with detailed messages</item>
    ///   <item>Optimizes validation for performance in batch operations</item>
    /// </list>
    /// </remarks>
    Task<TemplateVariableValidationResult> ValidateVariableSetAsync(
        Guid templateId, 
        Dictionary<string, string> variableValues, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Variable Analytics & Usage

    /// <summary>
    /// Retrieves comprehensive usage analytics for variables within the tenant scope.
    /// Provides insights into variable utilization patterns, performance metrics, and optimization
    /// opportunities for template development and system optimization workflows.
    /// </summary>
    /// <param name="tenantId">Tenant identifier for analytics scope</param>
    /// <param name="filters">Optional filtering criteria for analytics data</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive variable usage analytics with performance metrics</returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Analytics Capabilities:</strong></para>
    /// <list type="bullet">
    ///   <item>Variable usage frequency and patterns</item>
    ///   <item>Template integration and dependency analysis</item>
    ///   <item>Performance metrics and optimization insights</item>
    ///   <item>Error rates and validation failure analysis</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Aggregates data from multiple sources for comprehensive insights</item>
    ///   <item>Supports time-based filtering and trend analysis</item>
    ///   <item>Implements caching for dashboard responsiveness</item>
    ///   <item>Provides exportable analytics data for reporting</item>
    /// </list>
    /// </remarks>
    Task<VariableUsageAnalytics> GetVariableAnalyticsAsync(
        Guid tenantId, 
        AnalyticsFilterOptions? filters = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves variables that are similar to or related to a specified variable.
    /// Enables variable discovery, pattern identification, and reuse recommendations
    /// for template development and optimization workflows.
    /// </summary>
    /// <param name="variableId">Reference variable identifier for similarity analysis</param>
    /// <param name="tenantId">Tenant identifier for search scope</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Collection of related variables with similarity scores and recommendations</returns>
    /// <exception cref="NotFoundException">Thrown when reference variable is not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Similarity Analysis:</strong></para>
    /// <list type="bullet">
    ///   <item>Analyzes variable types, names, and usage patterns</item>
    ///   <item>Considers template associations and execution contexts</item>
    ///   <item>Provides similarity scores and recommendation rationale</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Uses machine learning for similarity scoring</item>
    ///   <item>Implements caching for frequently requested similarities</item>
    ///   <item>Supports configurable similarity thresholds</item>
    /// </list>
    /// </remarks>
    Task<IEnumerable<VariableSimilarityResult>> GetSimilarVariablesAsync(
        Guid variableId, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    #endregion

    #region Variable Processing Methods

    /// <summary>
    /// Extracts variable names from a prompt template content.
    /// Parses template syntax to identify all variable placeholders and their properties.
    /// </summary>
    /// <param name="promptContent">The prompt template content to analyze</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Collection of variable names found in the template</returns>
    /// <exception cref="ArgumentException">Thrown when prompt content is invalid</exception>
    /// <remarks>
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Supports multiple template syntax formats ({{variable}}, {variable}, etc.)</item>
    ///   <item>Handles nested and conditional variable expressions</item>
    ///   <item>Validates variable name syntax and constraints</item>
    ///   <item>Provides detailed parsing diagnostics for template validation</item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// var content = "Hello {{customerName}}, your order {{orderId}} is ready!";
    /// var variables = await service.ExtractVariableNamesAsync(content);
    /// // Returns: ["customerName", "orderId"]
    /// </code>
    /// </remarks>
    Task<IEnumerable<string>> ExtractVariableNamesAsync(
        string promptContent, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates variable values against their definitions and constraints.
    /// Performs comprehensive validation including type checking, range validation, and business rules.
    /// </summary>
    /// <param name="variableValues">Dictionary of variable names and their proposed values</param>
    /// <param name="templateId">Template identifier for context-specific validation</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Validation result with detailed error and warning information</returns>
    /// <exception cref="NotFoundException">Thrown when template or variables are not found</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Validation Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>All required variables must have values</item>
    ///   <item>Variable values must conform to their defined types</item>
    ///   <item>Values must satisfy range and constraint validations</item>
    ///   <item>Cross-variable dependencies are validated</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Uses variable definitions from template association</item>
    ///   <item>Performs type conversion and validation</item>
    ///   <item>Provides detailed error messages for validation failures</item>
    ///   <item>Supports custom validation rules and business logic</item>
    /// </list>
    /// </remarks>
    Task<VariableValidationResult> ValidateVariablesAsync(
        Dictionary<string, string> variableValues, 
        Guid templateId, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Resolves variable placeholders in a prompt template with provided values.
    /// Performs variable substitution with comprehensive error handling and validation.
    /// </summary>
    /// <param name="promptContent">The template content containing variable placeholders</param>
    /// <param name="variableValues">Dictionary of variable names and their values</param>
    /// <param name="tenantId">Tenant identifier for access control</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Resolved prompt content with variables substituted</returns>
    /// <exception cref="ArgumentException">Thrown when prompt content or variable values are invalid</exception>
    /// <exception cref="InvalidOperationException">Thrown when variable resolution fails</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when tenant access is denied</exception>
    /// <remarks>
    /// <para><strong>Resolution Process:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates all required variables are provided</item>
    ///   <item>Performs type conversion and formatting as needed</item>
    ///   <item>Applies variable transformations and filters</item>
    ///   <item>Handles missing variables with default values or errors</item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    ///   <item>Supports multiple template syntax formats</item>
    ///   <item>Handles conditional and nested variable expressions</item>
    ///   <item>Provides detailed error reporting for resolution failures</item>
    ///   <item>Implements caching for frequently used templates</item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// var content = "Hello {{customerName}}, your order {{orderId}} is ready!";
    /// var values = new Dictionary&lt;string, string&gt;
    /// {
    ///     ["customerName"] = "John Doe",
    ///     ["orderId"] = "ORD-12345"
    /// };
    /// var resolved = await service.ResolvePromptAsync(content, values, tenantId);
    /// // Returns: "Hello John Doe, your order ORD-12345 is ready!"
    /// </code>
    /// </remarks>
    Task<string> ResolvePromptAsync(
        string promptContent, 
        Dictionary<string, string> variableValues, 
        Guid tenantId, 
        CancellationToken cancellationToken = default);

    #endregion
}
