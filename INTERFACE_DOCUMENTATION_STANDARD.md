# Interface Documentation Standard for PromptStudio.Core

## Overview

This document defines the comprehensive documentation standard for all interfaces in the PromptStudio.Core project. Interfaces are critical contracts that define the boundaries between different layers and components of the system, and as such require detailed, enterprise-grade documentation that supports maintainability, testability, and integration workflows.

## Interface Documentation Framework

### Interface-Level Documentation

Every interface must include comprehensive class-level XML documentation following this structure:

```csharp
/// <summary>
/// [Brief description of interface purpose and primary responsibility]
/// 
/// <para><strong>Service Architecture:</strong></para>
/// [Description of where this interface fits in the overall architecture]
/// [Layer information (e.g., Service Layer, Repository Layer, Provider Layer)]
/// [Integration patterns and dependencies]
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// [Key behavioral contracts and expectations]
/// [Error handling requirements]
/// [Transaction and concurrency expectations]
/// [Performance and scalability considerations]
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
///   <item>[Specific implementation requirement 1]</item>
///   <item>[Specific implementation requirement 2]</item>
///   <item>[Threading and async considerations]</item>
///   <item>[Resource management expectations]</item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
///   <item>[How this interface integrates with other services]</item>
///   <item>[Dependency injection and lifecycle management]</item>
///   <item>[Cross-cutting concerns (logging, caching, security)]</item>
///   <item>[Enterprise patterns (Unit of Work, Repository, etc.)]</item>
/// </list>
/// 
/// <para><strong>Testing Considerations:</strong></para>
/// <list type="bullet">
///   <item>[Mock/stub guidance for testing]</item>
///   <item>[Integration testing requirements]</item>
///   <item>[Performance testing considerations]</item>
///   <item>[Error scenario testing requirements]</item>
/// </list>
/// </remarks>
public interface IExampleService
```

### Method Documentation Standard

Every interface method must include detailed XML documentation with the following elements:

```csharp
/// <summary>
/// [Clear, concise description of what the method does]
/// [Business context and use cases]
/// [Expected behavior and side effects]
/// </summary>
/// <param name="parameterName">[Parameter purpose and validation requirements]</param>
/// <param name="anotherParam">[Include nullability, range constraints, business rules]</param>
/// <param name="cancellationToken">[Standard: Cancellation token for async operation control]</param>
/// <returns>[Detailed description of return value, including null scenarios]</returns>
/// <exception cref="ArgumentException">Thrown when [specific condition]</exception>
/// <exception cref="InvalidOperationException">Thrown when [business rule violation]</exception>
/// <exception cref="NotFoundException">Thrown when [entity not found scenario]</exception>
/// <remarks>
/// <para><strong>Business Rules:</strong></para>
/// <list type="bullet">
///   <item>[Key business rule or constraint]</item>
///   <item>[Data validation requirements]</item>
///   <item>[Authorization and security considerations]</item>
/// </list>
/// 
/// <para><strong>Implementation Notes:</strong></para>
/// <list type="bullet">
///   <item>[Performance considerations or optimization hints]</item>
///   <item>[Caching behavior or database interaction patterns]</item>
///   <item>[Transaction requirements or side effects]</item>
/// </list>
/// 
/// <para><strong>Usage Examples:</strong></para>
/// <code>
/// // Standard usage pattern
/// var result = await service.MethodAsync(param1, param2, cancellationToken);
/// if (result != null) {
///     await ProcessResultAsync(result);
/// }
/// </code>
/// </remarks>
Task<ResultType> MethodAsync(string parameterName, Guid anotherParam, CancellationToken cancellationToken = default);
```

## Interface Categories and Specific Requirements

### 1. Service Layer Interfaces

**Characteristics:**
- High-level business operations
- Complex orchestration logic
- Cross-cutting concerns integration
- Rich business context

**Additional Documentation Requirements:**
- Business process context
- Transaction boundaries
- Security authorization requirements
- Audit and compliance considerations
- Integration with external systems

### 2. Repository Layer Interfaces

**Characteristics:**
- Data access abstractions
- CRUD operations with business context
- Query and filtering capabilities
- Performance optimization opportunities

**Additional Documentation Requirements:**
- Data consistency requirements
- Query performance characteristics
- Caching strategies and invalidation
- Connection and transaction management
- Entity relationship handling

### 3. Provider/Integration Interfaces

**Characteristics:**
- External system integration
- Protocol-specific implementations
- Error handling and resilience
- Configuration and connection management

**Additional Documentation Requirements:**
- External dependency requirements
- Failure modes and retry strategies
- Configuration and setup requirements
- Protocol-specific behaviors
- Rate limiting and throttling considerations

### 4. Domain Service Interfaces

**Characteristics:**
- Core business logic
- Domain rule enforcement
- Entity lifecycle management
- Business invariant maintenance

**Additional Documentation Requirements:**
- Domain rule documentation
- Entity state management
- Business invariant descriptions
- Domain event implications
- Aggregate boundary considerations

## Method Organization Standards

### Logical Grouping with Regions

Use `#region` blocks to organize related methods:

```csharp
public interface IExampleService
{
    #region Core Operations
    // Primary business operations
    #endregion
    
    #region Query Operations  
    // Data retrieval and search operations
    #endregion
    
    #region Management Operations
    // Administrative and lifecycle operations
    #endregion
    
    #region Analytics and Reporting
    // Metrics, analytics, and reporting operations
    #endregion
    
    #region Integration Operations
    // External system integration methods
    #endregion
}
```

### Method Ordering Within Regions

1. **Most Common Operations First** - Place frequently used methods at the top
2. **Logical Operation Flow** - Order methods to follow typical usage patterns
3. **Complexity Progression** - Simple operations before complex orchestrations
4. **Dependency Relationships** - Methods that depend on others should follow their dependencies

## Parameter Documentation Standards

### Required Elements for Each Parameter

1. **Purpose and Context**: What the parameter represents in business terms
2. **Validation Requirements**: Constraints, formats, and validation rules
3. **Nullability**: Clear indication of null handling
4. **Default Values**: Explanation of default behavior
5. **Business Rules**: Any business-specific constraints or behaviors

### Standard Parameter Patterns

```csharp
/// <param name="tenantId">Tenant identifier for multi-tenant data isolation. Must be valid and accessible to the current user context.</param>
/// <param name="userId">User identifier for authorization and audit trails. Used for access control validation and ownership verification.</param>
/// <param name="includeDeleted">Determines whether soft-deleted entities are included in results. Default false maintains standard user experience.</param>
/// <param name="cancellationToken">Cancellation token for async operation control and graceful shutdown support.</param>
```

## Parameter Documentation Guidelines

### Standard Parameter Categories

#### Required Business Parameters
```csharp
/// <param name="entityId">Unique identifier for the target entity (must be valid Guid, non-empty)</param>
/// <param name="tenantId">Tenant ID for multi-tenant security isolation (must be valid active tenant)</param>
/// <param name="userId">User ID for audit trail and authorization (must have required permissions)</param>
```

#### Optional Configuration Parameters
```csharp
/// <param name="includeDeleted">Whether to include soft-deleted entities in results (default: false)</param>
/// <param name="pageSize">Number of items per page for pagination (default: 50, range: 1-500)</param>
/// <param name="searchTerm">Optional search term for filtering results (supports wildcards and operators)</param>
```

#### Enterprise Feature Parameters
```csharp
/// <param name="auditContext">Additional context for audit trail entries (user session, request ID, etc.)</param>
/// <param name="cacheOptions">Caching behavior configuration (bypass, refresh, duration override)</param>
/// <param name="cancellationToken">Cancellation token for operation control and timeout handling</param>
```

### Parameter Validation Documentation

Always document:
- **Null handling**: Whether nulls are allowed and behavior
- **Range constraints**: Min/max values, length limits, format requirements
- **Business rules**: Tenant-specific validation, authorization requirements
- **Performance implications**: Large collection handling, query optimization impact

```csharp
/// <param name="templateContent">
/// Prompt template content with variable placeholders in {{variable}} format.
/// Length: 1-10,000 characters. Must contain at least one variable placeholder.
/// Validates against XSS patterns and injection attacks.
/// Performance: Large templates (&gt;5KB) may impact rendering performance.
/// </param>
```

## Return Value Documentation

### Success Scenarios
```csharp
/// <returns>
/// Successfully created entity with:
/// - Generated unique ID and audit timestamps
/// - All business rule validations passed
/// - Related entity associations established
/// - Cache entries updated for dependent queries
/// </returns>
```

### Failure Scenarios
```csharp
/// <returns>
/// Returns null when:
/// - Entity not found within tenant scope
/// - Soft-deleted entity accessed without includeDeleted flag
/// - User lacks read permissions for entity type
/// - Business validation rules prevent access
/// </returns>
```

### Collection Returns
```csharp
/// <returns>
/// Collection of entities matching criteria:
/// - Filtered by tenant boundary and user permissions
/// - Ordered by relevance score (descending) or specified sort criteria
/// - Includes navigation properties based on performance optimization
/// - Empty collection (never null) when no matches found
/// - Paginated automatically when result set exceeds configured limits
/// </returns>
```

## Exception Documentation

### Standard Exception Categories

#### Security Exceptions
```csharp
/// <exception cref="UnauthorizedAccessException">
/// Thrown when:
/// - User lacks required permissions for operation
/// - Tenant boundary violation detected
/// - Resource access denied by security policy
/// - Authentication token expired or invalid
/// </exception>
```

#### Validation Exceptions
```csharp
/// <exception cref="ValidationException">
/// Thrown when:
/// - Required fields are missing or invalid format
/// - Business rule validation fails (duplicate names, invalid relationships)
/// - Data integrity constraints violated
/// - Tenant-specific validation rules not met
/// </exception>
/// <exception cref="ArgumentException">
/// Thrown when:
/// - Parameter values are outside acceptable ranges
/// - String parameters contain invalid characters or formats
/// - Guid parameters are empty or invalid
/// - Enum parameters contain unsupported values
/// </exception>
```

#### Infrastructure Exceptions
```csharp
/// <exception cref="InvalidOperationException">
/// Thrown when:
/// - Service is in invalid state for requested operation
/// - Concurrent modification conflicts detected
/// - Transaction rollback required due to business rule violations
/// - External dependencies unavailable
/// </exception>
/// <exception cref="OperationCanceledException">
/// Thrown when operation is cancelled via cancellation token or timeout
/// </exception>
/// <exception cref="ConcurrencyException">
/// Thrown when optimistic concurrency check fails during update operations
/// </exception>
```

## Cross-Reference and Integration Guidelines

### Service Dependencies
```csharp
/// <remarks>
/// <para><strong>Service Dependencies:</strong></para>
/// <list type="bullet">
/// <item><description><see cref="IValidationService"/> - Data validation and business rules</description></item>
/// <item><description><see cref="IAuditService"/> - Audit trail and compliance logging</description></item>
/// <item><description><see cref="ICacheService"/> - Performance optimization and caching</description></item>
/// <item><description><see cref="ISecurityService"/> - Authorization and tenant isolation</description></item>
/// </list>
/// </remarks>
```

### Related Entities
```csharp
/// <summary>
/// Retrieves prompt template with related <see cref="PromptLibrary"/> and <see cref="VariableCollection"/> data
/// </summary>
/// <param name="templateId">See <see cref="PromptTemplate.Id"/> for identifier format requirements</param>
/// <returns>
/// <see cref="PromptTemplate"/> entity with navigation properties populated,
/// or null if not found within tenant scope
/// </returns>
```

### Integration Points
```csharp
/// <remarks>
/// <para><strong>LLMOps Integration:</strong></para>
/// <list type="bullet">
/// <item><description>Compatible with <see cref="IModelProvider"/> for AI model execution</description></item>
/// <item><description>Supports <see cref="IPromptExecutionService"/> workflow integration</description></item>
/// <item><description>Integrates with <see cref="IAnalyticsService"/> for performance metrics</description></item>
/// <item><description>Provides hooks for <see cref="IWorkflowService"/> automation</description></item>
/// </list>
/// </remarks>
```

## Advanced Documentation Patterns

### Enterprise Feature Documentation

#### Multi-Tenancy Patterns
```csharp
/// <remarks>
/// <para><strong>Multi-Tenancy Architecture:</strong></para>
/// <list type="bullet">
/// <item><description>Row-Level Security: All queries automatically filtered by tenant ID</description></item>
/// <item><description>Data Isolation: Cross-tenant access prevented at database and service layers</description></item>
/// <item><description>Resource Quotas: Operations respect tenant-specific limits and quotas</description></item>
/// <item><description>Configuration Inheritance: Supports tenant-specific and global configurations</description></item>
/// </list>
/// </remarks>
```

#### Performance and Scalability
```csharp
/// <remarks>
/// <para><strong>Performance Characteristics:</strong></para>
/// <list type="bullet">
/// <item><description>Caching: Results cached for 5 minutes with dependency-based invalidation</description></item>
/// <item><description>Pagination: Large result sets automatically paginated (default: 50 items)</description></item>
/// <item><description>Query Optimization: Uses optimized queries with selective column loading</description></item>
/// <item><description>Scalability: Supports horizontal scaling through stateless design</description></item>
/// </list>
/// </remarks>
```

#### Audit and Compliance
```csharp
/// <remarks>
/// <para><strong>Audit and Compliance:</strong></para>
/// <list type="bullet">
/// <item><description>Audit Trail: All operations logged with user context and timestamps</description></item>
/// <item><description>Data Retention: Respects tenant-specific retention policies</description></item>
/// <item><description>GDPR Compliance: Supports data portability and deletion requests</description></item>
/// <item><description>SOX Compliance: Maintains immutable audit logs for financial data</description></item>
/// </list>
/// </remarks>
```

### LLMOps-Specific Documentation

#### AI Model Integration
```csharp
/// <remarks>
/// <para><strong>AI Model Integration:</strong></para>
/// <list type="bullet">
/// <item><description>Model Compatibility: Supports OpenAI, Azure OpenAI, and local models</description></item>
/// <item><description>Prompt Engineering: Automatic variable extraction and validation</description></item>
/// <item><description>Token Management: Tracks and optimizes token usage across executions</description></item>
/// <item><description>Response Caching: Intelligent caching based on prompt similarity</description></item>
/// </list>
/// </remarks>
```

#### Workflow Integration
```csharp
/// <remarks>
/// <para><strong>Workflow Integration:</strong></para>
/// <list type="bullet">
/// <item><description>Flow Execution: Supports visual flow-based prompt orchestration</description></item>
/// <item><description>Variable Propagation: Automatic variable mapping between flow nodes</description></item>
/// <item><description>Error Handling: Configurable retry policies and fallback strategies</description></item>
/// <item><description>Analytics Integration: Execution metrics and performance monitoring</description></item>
/// </list>
/// </remarks>
```

## Quality Assurance and Validation

### Documentation Validation Checklist

#### Interface-Level Validation
- [ ] Summary clearly states interface purpose and business context
- [ ] Architecture integration and service layer positioning documented
- [ ] Implementation contract and behavioral expectations specified
- [ ] Integration patterns and enterprise concerns addressed
- [ ] Testing considerations and mock guidance provided

#### Method-Level Validation
- [ ] Business purpose and use cases clearly stated
- [ ] All parameters documented with validation requirements
- [ ] Return values specify success and failure scenarios
- [ ] All possible exceptions documented with specific conditions
- [ ] Performance and security implications addressed
- [ ] Code examples provided for complex operations

#### Cross-Reference Validation
- [ ] Service dependencies accurately identified and linked
- [ ] Related entity references use proper XML documentation syntax
- [ ] Integration points are comprehensive and current
- [ ] Links to domain models and DTOs are accurate

### Automated Documentation Tools

#### Static Analysis Integration
```xml
<!-- In .editorconfig or analyzer configuration -->
<Rule Id="CS1591" Action="Error" /> <!-- Missing XML documentation -->
<Rule Id="SA1600" Action="Error" /> <!-- Elements should be documented -->
<Rule Id="SA1615" Action="Error" /> <!-- Element return value should be documented -->
<Rule Id="SA1618" Action="Error" /> <!-- Generic type parameters should be documented -->
```

#### Documentation Generation
- **Sandcastle**: Generate comprehensive API documentation
- **DocFX**: Create documentation websites with cross-references
- **Custom Tools**: Generate service integration guides and workflow documentation

#### Quality Metrics
- **Coverage**: Percentage of public members with complete documentation
- **Consistency**: Terminology and format standardization across interfaces
- **Accuracy**: Cross-reference validation and link checking
- **Completeness**: Business context and enterprise feature coverage

## Implementation Timeline and Maintenance

### Phase 1: Documentation Audit (Week 1)
- [ ] Review all existing interfaces for documentation completeness
- [ ] Identify gaps in business context and enterprise feature documentation
- [ ] Create prioritized remediation list based on interface usage and criticality
- [ ] Establish baseline metrics for documentation quality

### Phase 2: Standard Application (Weeks 2-3)
- [ ] Apply documentation standard to high-priority service interfaces
- [ ] Update method signatures for consistency with enterprise patterns
- [ ] Add missing exception documentation and business context
- [ ] Implement cross-reference links and integration documentation

### Phase 3: Quality Assurance (Week 4)
- [ ] Implement automated documentation validation rules
- [ ] Conduct comprehensive peer reviews of updated documentation
- [ ] Validate IntelliSense display and functionality across development environments
- [ ] Generate API documentation and validate completeness

### Phase 4: Maintenance and Evolution (Ongoing)
- [ ] Establish documentation review processes for new interfaces
- [ ] Create templates and guidelines for different interface categories
- [ ] Monitor documentation quality metrics and address degradation
- [ ] Update standards based on feedback and evolving enterprise requirements

### Continuous Improvement Process
1. **Monthly Reviews**: Assess documentation quality and consistency
2. **Template Updates**: Evolve templates based on new patterns and requirements
3. **Tool Integration**: Enhance automated validation and generation capabilities
4. **Training Programs**: Ensure development team understands and applies standards
5. **Stakeholder Feedback**: Incorporate input from consumers of interface documentation

## Complete Interface Example

Here's a comprehensive example that demonstrates all the documentation standards in practice:

```csharp
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Templates;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Enterprise-grade service for managing prompt templates with advanced LLMOps capabilities.
/// Supports multi-tenancy, audit trails, version management, AI model integration, and workflow orchestration.
/// Provides comprehensive template lifecycle management from creation through execution and analytics.
/// </summary>
/// <remarks>
/// <para><strong>Service Architecture:</strong></para>
/// Service layer component that orchestrates template management operations across repository,
/// validation, audit, and integration layers. Implements enterprise patterns including Unit of Work,
/// Repository, and Domain Service patterns for robust data management.
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// <list type="bullet">
///   <item>All operations must respect tenant boundaries and security constraints</item>
///   <item>Automatic audit trail creation for all write operations</item>
///   <item>Comprehensive validation including business rules and data integrity</item>
///   <item>Performance optimized with intelligent caching and query optimization</item>
///   <item>Thread-safe operations supporting concurrent access scenarios</item>
/// </list>
/// 
/// <para><strong>Multi-Tenancy Architecture:</strong></para>
/// <list type="bullet">
///   <item>Row-Level Security: All queries automatically filtered by tenant ID</item>
///   <item>Data Isolation: Cross-tenant access prevented at database and service layers</item>
///   <item>Resource Quotas: Operations respect tenant-specific limits and quotas</item>
///   <item>Configuration Inheritance: Supports tenant-specific and global configurations</item>
/// </list>
/// 
/// <para><strong>Performance Characteristics:</strong></para>
/// <list type="bullet">
///   <item>Caching: Template metadata cached for 5 minutes with dependency-based invalidation</item>
///   <item>Pagination: Large result sets automatically paginated (default: 50 items)</item>
///   <item>Query Optimization: Uses optimized queries with selective column loading</item>
///   <item>Scalability: Supports horizontal scaling through stateless design</item>
/// </list>
/// 
/// <para><strong>LLMOps Integration:</strong></para>
/// <list type="bullet">
///   <item>Model Compatibility: Supports OpenAI, Azure OpenAI, Anthropic, and local models</item>
///   <item>Prompt Engineering: Automatic variable extraction and validation</item>
///   <item>Token Management: Tracks and optimizes token usage across executions</item>
///   <item>Response Caching: Intelligent caching based on prompt similarity hashing</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Basic template management
/// var service = serviceProvider.GetRequiredService&lt;IPromptTemplateService&gt;();
/// 
/// // Create a new template with business context
/// var template = await service.CreatePromptTemplateAsync(
///     "Customer Support Response Generator",
///     "Hello {{customerName}}, thank you for contacting us about {{issueType}}. {{responseBody}}",
///     libraryId: supportLibraryId,
///     tenantId: currentTenantId,
///     createdBy: currentUserId,
///     description: "Generates personalized customer support responses",
///     tags: "customer-support,personalization,ai-generated",
///     category: "Customer Service");
/// 
/// // Execute template with variables
/// var variables = new Dictionary&lt;string, object&gt;
/// {
///     { "customerName", "Jane Smith" },
///     { "issueType", "billing inquiry" },
///     { "responseBody", "I'll help you resolve this billing question promptly." }
/// };
/// 
/// var executionResult = await service.ExecuteTemplateAsync(
///     template.Id, 
///     variables, 
///     currentTenantId,
///     modelProvider: "azure-openai-gpt4",
///     cancellationToken: cancellationToken);
/// </code>
/// </example>
public interface IPromptTemplateService
{
    #region Core CRUD Operations
    /// <summary>
    /// Core template lifecycle management with enterprise audit and security features.
    /// Supports creation, retrieval, updates, and soft deletion with full tenant isolation.
    /// </summary>

    /// <summary>
    /// Asynchronously creates a new prompt template with comprehensive validation and audit support.
    /// Validates template content, extracts variables, establishes library relationships, and creates audit trail.
    /// </summary>
    /// <param name="name">
    /// Template name (required, 1-200 characters, must be unique within tenant and library scope).
    /// Validated for XSS patterns and reserved keywords. Supports internationalization.
    /// </param>
    /// <param name="content">
    /// Template content with variable placeholders in {{variable}} format.
    /// Length: 1-50,000 characters. Automatically validated for security and syntax.
    /// Supports nested variables and conditional logic expressions.
    /// </param>
    /// <param name="libraryId">
    /// Parent library ID for organizational hierarchy (must exist and be accessible to tenant).
    /// Used for permission inheritance and categorization. Cannot be changed after creation.
    /// </param>
    /// <param name="tenantId">
    /// Tenant ID for multi-tenant security isolation (must be valid active tenant).
    /// Used for data isolation, quota enforcement, and configuration inheritance.
    /// </param>
    /// <param name="createdBy">
    /// User ID for audit trail and authorization (must have create permissions in specified library).
    /// Used for ownership assignment, permission validation, and audit context.
    /// </param>
    /// <param name="description">
    /// Optional detailed description of template purpose and usage (max 2000 characters).
    /// Supports markdown formatting for rich documentation. Indexed for search operations.
    /// </param>
    /// <param name="tags">
    /// Optional comma-separated tags for categorization and search (max 500 characters total).
    /// Individual tags limited to 50 characters. Used for filtering and discovery.
    /// </param>
    /// <param name="category">
    /// Optional category for organizational grouping (max 100 characters).
    /// Must match existing category or be pre-approved for tenant. Used in analytics.
    /// </param>
    /// <param name="version">
    /// Semantic version number (default: "1.0.0", must follow semver format: major.minor.patch).
    /// Used for template evolution tracking and deployment management.
    /// </param>
    /// <param name="cancellationToken">Cancellation token for async operation control and timeout handling</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains:
    /// - Newly created PromptTemplate with generated ID, timestamps, and audit information
    /// - All extracted variable definitions with type inference and validation rules
    /// - Library relationship establishment and permission inheritance
    /// - Initial version metadata and change tracking setup
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when required parameters (name, content, libraryId, tenantId, createdBy) are null or empty
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when:
    /// - Name exceeds length limits or contains invalid characters
    /// - Content contains malformed variable syntax or exceeds size limits
    /// - Version string doesn't follow semantic versioning format
    /// - Tags contain invalid characters or exceed individual/total length limits
    /// </exception>
    /// <exception cref="ValidationException">
    /// Thrown when business validation fails:
    /// - Duplicate template name within tenant and library scope
    /// - Library doesn't exist or is not accessible to tenant
    /// - Template content fails security validation (XSS, injection patterns)
    /// - Variable syntax validation errors in template content
    /// - Category doesn't exist and auto-creation is disabled for tenant
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when:
    /// - User lacks create permissions in specified library
    /// - Tenant access is denied or suspended
    /// - Library access restrictions prevent template creation
    /// - Quota limits exceeded for tenant template creation
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Thrown when operation is cancelled via cancellation token or exceeds timeout limits
    /// </exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    ///   <item>Template names must be unique within tenant and library scope</item>
    ///   <item>Content undergoes security validation to prevent XSS and injection attacks</item>
    ///   <item>Variable extraction creates metadata for type inference and validation</item>
    ///   <item>Library permissions are inherited and enforced for all operations</item>
    ///   <item>Audit trail includes user context, IP address, and operation metadata</item>
    /// </list>
    /// 
    /// <para><strong>Security:</strong></para>
    /// <list type="bullet">
    ///   <item>Validates tenant boundaries and prevents cross-tenant data access</item>
    ///   <item>Enforces library-level permissions and access control policies</item>
    ///   <item>Content sanitization prevents script injection and malicious code</item>
    ///   <item>Creates comprehensive audit trail with security context</item>
    /// </list>
    /// 
    /// <para><strong>Performance:</strong></para>
    /// <list type="bullet">
    ///   <item>Optimized database queries with minimal round trips</item>
    ///   <item>Asynchronous variable extraction and validation processing</item>
    ///   <item>Intelligent caching of library metadata and permissions</item>
    ///   <item>Batch processing for related entity creation and updates</item>
    /// </list>
    /// 
    /// <para><strong>Side Effects:</strong></para>
    /// <list type="bullet">
    ///   <item>Creates audit log entry with creation timestamp and user context</item>
    ///   <item>Updates library statistics and usage metrics</item>
    ///   <item>Triggers cache invalidation for affected library and search queries</item>
    ///   <item>Initiates background variable analysis and type inference</item>
    ///   <item>Sends creation notification events to workflow system</item>
    /// </list>
    /// 
    /// <para><strong>Integration:</strong></para>
    /// <list type="bullet">
    ///   <item>Integrates with <see cref="IValidationService"/> for content and business rule validation</item>
    ///   <item>Uses <see cref="IAuditService"/> for comprehensive audit trail creation</item>
    ///   <item>Leverages <see cref="ICacheService"/> for performance optimization</item>
    ///   <item>Coordinates with <see cref="IWorkflowService"/> for automation triggers</item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create a comprehensive customer service template
    /// var template = await service.CreatePromptTemplateAsync(
    ///     name: "Customer Issue Resolution Template",
    ///     content: @"Dear {{customerName}},
    ///     
    ///     Thank you for contacting us regarding {{issueCategory}}. 
    ///     
    ///     {{#if urgentIssue}}
    ///     We understand this is urgent and will prioritize your request.
    ///     {{/if}}
    ///     
    ///     Based on your description: {{issueDescription}}
    ///     
    ///     Our recommended solution: {{proposedSolution}}
    ///     
    ///     Next steps: {{nextSteps}}
    ///     
    ///     Best regards,
    ///     {{agentName}}",
    ///     libraryId: customerServiceLibraryId,
    ///     tenantId: enterpriseTenantId,
    ///     createdBy: managerUserId,
    ///     description: "Comprehensive template for customer issue resolution with conditional logic and personalization",
    ///     tags: "customer-service,issue-resolution,personalized,conditional",
    ///     category: "Customer Support",
    ///     version: "2.1.0");
    /// 
    /// // Verify creation and log success
    /// if (template != null)
    /// {
    ///     logger.LogInformation("Template created successfully: {TemplateId} for tenant {TenantId}", 
    ///         template.Id, tenantId);
    ///     
    ///     // Template is now ready for execution and workflow integration
    ///     await NotifyTeamOfNewTemplate(template);
    /// }
    /// </code>
    /// </example>
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

    #endregion

    #region Query and Search Operations
    /// <summary>
    /// Advanced template discovery and filtering with performance optimization.
    /// Supports complex queries, full-text search, intelligent caching, and analytics integration.
    /// </summary>

    // Additional query methods would be documented here following the same pattern...

    #endregion

    #region Execution and LLMOps
    /// <summary>
    /// AI model integration and prompt execution capabilities.
    /// Supports multiple model providers, execution analytics, and workflow integration.
    /// </summary>

    // Execution methods would be documented here following the same pattern...

    #endregion
}
```

## Conclusion

This comprehensive documentation standard ensures that all interfaces in PromptStudio.Core provide enterprise-grade documentation that supports:

### Business Value
- **Clear Communication**: Business context and value proposition for each interface
- **Integration Guidance**: How interfaces fit into the overall system architecture
- **Usage Patterns**: Real-world examples and best practices for implementation

### Technical Excellence
- **Complete Coverage**: Every public member documented with comprehensive details
- **Consistency**: Standardized terminology and formatting across all interfaces
- **Maintainability**: Documentation that evolves with code changes and remains accurate

### Enterprise Requirements
- **Security Documentation**: Clear guidance on authorization, tenant isolation, and data protection
- **Performance Characteristics**: Caching strategies, scalability considerations, and optimization notes
- **Compliance Support**: Audit trails, data governance, and regulatory compliance features

### Developer Experience
- **IntelliSense Optimization**: Rich IDE support with comprehensive parameter and return value documentation
- **Error Handling**: Complete exception documentation with specific conditions and resolution guidance
- **Integration Support**: Cross-reference links and dependency documentation for effective development

### Quality Assurance
- **Validation Framework**: Automated tools and manual review processes to ensure documentation quality
- **Continuous Improvement**: Regular reviews and updates to maintain relevance and accuracy
- **Stakeholder Feedback**: Mechanisms for incorporating input from documentation consumers

By following this standard, PromptStudio.Core interfaces become self-documenting, enterprise-ready components that facilitate successful integration, reduce development time, and support advanced LLMOps scenarios across the entire platform.

The documentation standard serves as both a reference guide and a living document that will evolve with the platform's growth and the team's increasing understanding of enterprise documentation needs.

## Default Interface Methods (C# 8.0+)

### Overview

C# 8.0 introduced default interface methods, allowing interfaces to contain concrete implementations alongside traditional abstract method declarations. This feature enables API evolution without breaking existing implementations and supports advanced patterns like traits and mixins.

### Documentation Standards for Default Interface Methods

#### Default Method Documentation Template

```csharp
/// <summary>
/// [Method description with emphasis on being a default implementation]
/// [Business context and rationale for providing default behavior]
/// [Guidance on when implementing classes should override vs use default]
/// </summary>
/// <param name="parameterName">Parameter description with validation requirements</param>
/// <param name="cancellationToken">Cancellation token for async operation control</param>
/// <returns>
/// Default implementation returns:
/// - [Description of default behavior and return value]
/// - [Conditions under which default behavior is appropriate]
/// - [Guidance for implementers on customization needs]
/// </returns>
/// <exception cref="ExceptionType">Thrown when [specific condition in default implementation]</exception>
/// <remarks>
/// <para><strong>Default Implementation:</strong></para>
/// <list type="bullet">
///   <item>Provides baseline functionality suitable for most common scenarios</item>
///   <item>Can be overridden in implementing classes for specialized behavior</item>
///   <item>Maintains backward compatibility with existing interface implementations</item>
/// </list>
/// 
/// <para><strong>Override Guidance:</strong></para>
/// <list type="bullet">
///   <item>Override when: [specific scenarios requiring custom implementation]</item>
///   <item>Use default when: [scenarios where default behavior is sufficient]</item>
///   <item>Performance considerations: [implications of default vs custom implementation]</item>
/// </list>
/// 
/// <para><strong>Implementation Notes:</strong></para>
/// <list type="bullet">
///   <item>Default behavior: [detailed description of what the default does]</item>
///   <item>Dependencies: [other interface members or external dependencies used]</item>
///   <item>Side effects: [any state changes or external interactions]</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Using default implementation
/// var instance = new MyImplementation();
/// var result = instance.DefaultMethod(parameter);
/// 
/// // Custom override example
/// public override ReturnType DefaultMethod(ParameterType parameter)
/// {
///     // Custom logic here
///     return customResult;
/// }
/// </code>
/// </example>
public virtual ReturnType DefaultMethod(ParameterType parameter, CancellationToken cancellationToken = default)
{
    // Default implementation
    return DefaultBehavior(parameter);
}
```

#### Static and Private Helper Methods

Default interface methods often require helper methods. Document these appropriately:

```csharp
/// <summary>
/// Static helper method supporting default interface implementations.
/// Provides common functionality that can be shared across default methods.
/// </summary>
/// <param name="data">Input data for processing</param>
/// <returns>Processed result used by default implementations</returns>
/// <remarks>
/// <para><strong>Usage:</strong> Internal helper for default interface methods.</para>
/// <para><strong>Visibility:</strong> Static method accessible to all interface implementations.</para>
/// </remarks>
public static ResultType SharedHelper(DataType data)
{
    // Helper implementation
}

/// <summary>
/// Private helper method for internal default implementation logic.
/// Encapsulates complex operations to maintain clean default method implementations.
/// </summary>
/// <param name="input">Internal processing input</param>
/// <returns>Internal processing result</returns>
private static ProcessedType PrivateHelper(InputType input)
{
    // Private helper implementation
}
```

### Advanced Default Interface Method Patterns

#### Versioning and Evolution Documentation

```csharp
/// <summary>
/// New method added in version 2.0 with default implementation for backward compatibility.
/// Provides enhanced functionality while maintaining compatibility with v1.0 implementations.
/// </summary>
/// <param name="enhancedParam">New parameter for enhanced functionality</param>
/// <returns>Enhanced result with backward-compatible behavior</returns>
/// <remarks>
/// <para><strong>Version History:</strong></para>
/// <list type="bullet">
///   <item>v2.0: Added with default implementation for backward compatibility</item>
///   <item>Future versions may require explicit implementation</item>
/// </list>
/// 
/// <para><strong>Migration Guidance:</strong></para>
/// <list type="bullet">
///   <item>Existing implementations: No changes required, default behavior applies</item>
///   <item>New implementations: Consider overriding for optimized behavior</item>
///   <item>Performance-critical scenarios: Override recommended for custom optimization</item>
/// </list>
/// </remarks>
public virtual EnhancedResult NewMethodWithDefault(EnhancedParameter enhancedParam)
{
    // Default implementation providing backward compatibility
    return StandardBehavior(enhancedParam);
}
```

#### Trait-Like Behavior Documentation

```csharp
/// <summary>
/// Trait-like default implementation providing cross-cutting functionality.
/// Implements common behavior pattern that can be shared across multiple interface hierarchies.
/// </summary>
/// <returns>Standard trait behavior result</returns>
/// <remarks>
/// <para><strong>Trait Pattern:</strong></para>
/// <list type="bullet">
///   <item>Provides reusable behavior across multiple implementing types</item>
///   <item>Can be composed with other trait-like interfaces</item>
///   <item>Supports multiple inheritance scenarios through interface composition</item>
/// </list>
/// 
/// <para><strong>Composition Guidance:</strong></para>
/// <list type="bullet">
///   <item>Safe to use alongside other trait interfaces</item>
///   <item>No state dependencies - purely behavioral</item>
///   <item>Override for type-specific optimizations</item>
/// </list>
/// </remarks>
public virtual TraitResult TraitBehavior()
{
    // Trait implementation
    return CommonTraitLogic();
}
```

### Default Interface Method Categories

#### 1. Convenience Methods
Methods that provide common functionality built on top of core interface methods:

```csharp
/// <summary>
/// Convenience method that combines multiple core operations for common scenarios.
/// Default implementation uses existing interface methods to provide composite functionality.
/// </summary>
/// <param name="parameters">Combined parameters for composite operation</param>
/// <returns>Result of composite operation using core interface methods</returns>
/// <remarks>
/// <para><strong>Implementation Strategy:</strong> Composes existing interface methods</para>
/// <para><strong>Override Recommendation:</strong> Override for performance optimization or custom logic</para>
/// </remarks>
public virtual CompositeResult ConvenienceMethod(CompositeParameters parameters)
{
    // Implementation using other interface methods
    var step1 = CoreMethod1(parameters.Part1);
    var step2 = CoreMethod2(parameters.Part2);
    return CombineResults(step1, step2);
}
```

#### 2. Extension Methods
Methods that extend interface functionality without breaking existing implementations:

```csharp
/// <summary>
/// Extension method added for enhanced functionality while maintaining backward compatibility.
/// Provides new capabilities that weren't available in the original interface design.
/// </summary>
/// <param name="newFeatureParam">Parameter for new feature functionality</param>
/// <returns>Extended functionality result</returns>
/// <remarks>
/// <para><strong>Compatibility:</strong> Added without breaking existing implementations</para>
/// <para><strong>Default Behavior:</strong> Provides reasonable fallback for all implementations</para>
/// </remarks>
public virtual ExtendedResult ExtensionMethod(NewFeatureParameter newFeatureParam)
{
    // Default implementation providing reasonable behavior
    return DefaultExtensionBehavior(newFeatureParam);
}
```

#### 3. Template Methods
Methods that define algorithmic structure with customizable steps:

```csharp
/// <summary>
/// Template method defining standard algorithm with customizable steps.
/// Default implementation provides complete workflow with extension points for customization.
/// </summary>
/// <param name="workflowInput">Input for the standard workflow</param>
/// <returns>Workflow result following standard algorithm</returns>
/// <remarks>
/// <para><strong>Template Pattern:</strong></para>
/// Defines standard algorithm with customization points
/// <para><strong>Customization:</strong> Override specific steps or entire method as needed</para>
/// </remarks>
public virtual WorkflowResult TemplateMethod(WorkflowInput workflowInput)
{
    // Template method implementation
    var prepared = PrepareInput(workflowInput);     // Virtual step
    var processed = ProcessCore(prepared);           // Abstract step
    var finalized = FinalizeResult(processed);      // Virtual step
    return finalized;
}

/// <summary>
/// Customizable preparation step in template method workflow.
/// Default implementation provides standard preparation logic.
/// </summary>
protected virtual PreparedInput PrepareInput(WorkflowInput input)
{
    // Default preparation logic
    return StandardPreparation(input);
}
```

### Documentation Requirements for Default Interface Methods

#### Required Documentation Elements

1. **Purpose and Rationale**: Why this method has a default implementation
2. **Default Behavior Description**: What the default implementation does
3. **Override Guidance**: When and why to override the default
4. **Compatibility Information**: How this affects existing implementations
5. **Performance Implications**: Cost of default vs custom implementation
6. **Dependencies**: What other interface members the default uses

#### Virtual vs Sealed Default Methods

```csharp
/// <summary>
/// Virtual default method that can be overridden by implementing classes.
/// Provides standard behavior while allowing customization for specific needs.
/// </summary>
/// <remarks>
/// <para><strong>Virtuality:</strong> Can be overridden in implementing classes</para>
/// <para><strong>Default Usage:</strong> Suitable for most common scenarios</para>
/// </remarks>
public virtual Result VirtualDefaultMethod()
{
    // Virtual default implementation
}

/// <summary>
/// Sealed default method providing fixed implementation that cannot be overridden.
/// Ensures consistent behavior across all implementations of this interface.
/// </summary>
/// <remarks>
/// <para><strong>Sealed Behavior:</strong> Cannot be overridden - provides guaranteed behavior</para>
/// <para><strong>Rationale:</strong> Critical functionality that must remain consistent</para>
/// </remarks>
public sealed Result SealedDefaultMethod()
{
    // Sealed default implementation
}
```

### Integration with Existing Documentation Standards

Default interface methods should follow all existing documentation standards with these additional considerations:

1. **Backward Compatibility**: Always document impact on existing implementations
2. **Evolution Strategy**: Explain how the interface can evolve over time
3. **Implementation Guidance**: Provide clear guidance on when to override
4. **Performance Characteristics**: Document performance implications of defaults
5. **Testing Considerations**: Include guidance for testing default implementations

### Example: Complete Default Interface Method Documentation

```csharp
/// <summary>
/// Enterprise-grade caching interface with default implementations for common scenarios.
/// Provides standard caching operations with sensible defaults while allowing customization.
/// Supports evolution of caching strategies without breaking existing implementations.
/// </summary>
/// <remarks>
/// <para><strong>Default Implementation Strategy:</strong></para>
/// Default methods provide in-memory caching with basic eviction policies.
/// Suitable for development and small-scale scenarios.
/// 
/// <para><strong>Production Recommendations:</strong></para>
/// <list type="bullet">
///   <item>Override for distributed caching (Redis, etc.)</item>
///   <item>Implement custom eviction policies for specific needs</item>
///   <item>Add metrics and monitoring in production implementations</item>
/// </list>
/// 
/// <para><strong>Evolution Support:</strong></para>
/// New caching features can be added with default implementations without breaking
/// existing implementations, supporting gradual adoption of new capabilities.
/// </remarks>
public interface ICacheService
{
    #region Core Abstract Methods
    /// <summary>
    /// Core cache retrieval method - must be implemented by all cache providers.
    /// </summary>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Core cache storage method - must be implemented by all cache providers.
    /// </summary>
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    #endregion

    #region Default Convenience Methods
    /// <summary>
    /// Gets cached value or computes and caches it if not present.
    /// Default implementation combines Get and Set operations for convenience.
    /// </summary>
    /// <typeparam name="T">Type of cached value</typeparam>
    /// <param name="key">Cache key identifier</param>
    /// <param name="factory">Function to compute value if not cached</param>
    /// <param name="expiration">Optional cache expiration time</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Cached or newly computed value</returns>
    /// <remarks>
    /// <para><strong>Default Implementation:</strong></para>
    /// Uses GetAsync to check for existing value, calls factory if missing,
    /// then uses SetAsync to cache the result.
    /// 
    /// <para><strong>Override Recommendations:</strong></para>
    /// <list type="bullet">
    ///   <item>Redis implementations: Use GET/SET with NX flag for atomicity</item>
    ///   <item>High-concurrency scenarios: Implement with proper locking</item>
    ///   <item>Metrics requirements: Add timing and hit/miss tracking</item>
    /// </list>
    /// 
    /// <para><strong>Performance:</strong> Default implementation makes 1-2 cache operations</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Using default implementation
    /// var user = await cache.GetOrSetAsync(
    ///     $"user:{userId}",
    ///     () => userService.GetUserAsync(userId),
    ///     TimeSpan.FromMinutes(30));
    /// 
    /// // Custom override for Redis
    /// public override async Task&lt;T&gt; GetOrSetAsync&lt;T&gt;(string key, Func&lt;Task&lt;T&gt;&gt; factory, TimeSpan? expiration, CancellationToken cancellationToken)
    /// {
    ///     // Redis-specific atomic implementation
    ///     return await redisDatabase.HashGetOrSetAsync(key, factory, expiration);
    /// }
    /// </code>
    /// </example>
    public virtual async Task<T> GetOrSetAsync<T>(
        string key, 
        Func<Task<T>> factory, 
        TimeSpan? expiration = null, 
        CancellationToken cancellationToken = default)
    {
        var cached = await GetAsync<T>(key, cancellationToken);
        if (cached != null)
        {
            return cached;
        }

        var value = await factory();
        await SetAsync(key, value, expiration, cancellationToken);
        return value;
    }
    #endregion

    #region Extension Methods (Added in v2.0)
    /// <summary>
    /// Bulk cache retrieval method added in v2.0 with default implementation.
    /// Provides efficient multi-key retrieval with backward compatibility.
    /// </summary>
    /// <typeparam name="T">Type of cached values</typeparam>
    /// <param name="keys">Collection of cache keys to retrieve</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Dictionary of key-value pairs for found cache entries</returns>
    /// <remarks>
    /// <para><strong>Version 2.0 Addition:</strong></para>
    /// Added with default implementation to maintain backward compatibility.
    /// Existing implementations get functional behavior without modification.
    /// 
    /// <para><strong>Default Implementation:</strong></para>
    /// Uses multiple GetAsync calls - functional but not optimized for bulk operations.
    /// 
    /// <para><strong>Override for Performance:</strong></para>
    /// Recommended for implementations supporting native bulk operations (Redis MGET, etc.)
    /// </remarks>
    public virtual async Task<Dictionary<string, T>> GetManyAsync<T>(
        IEnumerable<string> keys, 
        CancellationToken cancellationToken = default)
    {
        var result = new Dictionary<string, T>();
        foreach (var key in keys)
        {
            var value = await GetAsync<T>(key, cancellationToken);
            if (value != null)
            {
                result[key] = value;
            }
        }
        return result;
    }
    #endregion
}
```

This comprehensive documentation approach for default interface methods ensures that developers understand not only what the methods do, but also when and how to customize them for their specific needs.
