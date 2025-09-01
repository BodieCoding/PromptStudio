using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;
using System.Linq.Expressions;

namespace PromptStudio.Data;

/// <summary>
/// Enhanced Database context for the PromptStudio application
/// Comprehensive enterprise-grade implementation with multi-tenancy, audit trails, and advanced features
/// This replaces the original PromptStudioDbContext with Guid-based architecture and enterprise capabilities
/// 
/// NOTE: This DbContext includes entities that have been extracted to separate files.
/// Some advanced entities (QualityMetric, UsageAnalytic, ModelProviderConfig, etc.) are marked as TODO
/// and should be implemented when those features are developed.
/// </summary>
public class PromptStudioDbContext : DbContext
{
    private readonly string? _currentUserId;
    private readonly Guid? _currentOrganizationId;
    
    /// <summary>
    /// Initializes a new instance of the EnhancedPromptStudioDbContext
    /// </summary>
    /// <param name="options">The options to be used by the context</param>
    /// <param name="currentUserId">Current user identifier for audit trails</param>
    /// <param name="currentOrganizationId">Current organization for multi-tenant data isolation</param>
    public PromptStudioDbContext(
        DbContextOptions<PromptStudioDbContext> options, 
        string? currentUserId = null,
        Guid? currentOrganizationId = null) : base(options)
    {
        _currentUserId = currentUserId;
        _currentOrganizationId = currentOrganizationId;
    }

    #region Core Domain Entities

    /// <summary>
    /// Prompt Labs - Top-level organizational units
    /// </summary>
    public DbSet<PromptLab> PromptLabs { get; set; }

    /// <summary>
    /// Prompt Libraries - Organized collections within labs
    /// </summary>
    public DbSet<PromptLibrary> PromptLibraries { get; set; }

    /// <summary>
    /// Prompt Templates - Reusable prompts with variables
    /// </summary>
    public DbSet<PromptTemplate> PromptTemplates { get; set; }

    /// <summary>
    /// Prompt Variables - Template variable definitions
    /// </summary>
    public DbSet<PromptVariable> PromptVariables { get; set; }

    /// <summary>
    /// Prompt Executions - Execution history and results
    /// </summary>
    public DbSet<PromptExecution> PromptExecutions { get; set; }

    /// <summary>
    /// Variable Collections - Batch variable sets for testing
    /// </summary>
    public DbSet<VariableCollection> VariableCollections { get; set; }

    #endregion

    #region Workflow Engine Entities

    /// <summary>
    /// Prompt Flows - Visual workflow definitions
    /// </summary>
    public DbSet<PromptFlow> PromptFlows { get; set; }

    /// <summary>
    /// Workflow Libraries - Organizational structure for workflows
    /// </summary>
    public DbSet<WorkflowLibrary> WorkflowLibraries { get; set; }

    /// <summary>
    /// Workflow Categories - Flexible categorization system for workflows
    /// </summary>
    public DbSet<WorkflowCategory> WorkflowCategories { get; set; }

    /// <summary>
    /// Flow Nodes - Individual workflow components
    /// </summary>
    public DbSet<FlowNode> FlowNodes { get; set; }

    /// <summary>
    /// Flow Edges - Connections between workflow nodes
    /// </summary>
    public DbSet<FlowEdge> FlowEdges { get; set; }

    /// <summary>
    /// Flow Executions - Workflow execution instances
    /// </summary>
    public DbSet<FlowExecution> FlowExecutions { get; set; }

    /// <summary>
    /// Node Executions - Individual node execution records
    /// </summary>
    public DbSet<NodeExecution> NodeExecutions { get; set; }

    /// <summary>
    /// Edge Traversals - Edge traversal records during execution
    /// </summary>
    public DbSet<EdgeTraversal> EdgeTraversals { get; set; }

    #endregion

    #region Content and Versioning

    /// <summary>
    /// Prompt Content - Separates content from metadata for performance
    /// </summary>
    public DbSet<PromptContent> PromptContents { get; set; }

    /// <summary>
    /// Template Versions - Version history for templates
    /// </summary>
    public DbSet<TemplateVersion> TemplateVersions { get; set; }

    // TODO: Create FlowVersion entity when implementing workflow versioning
    // public DbSet<FlowVersion> FlowVersions { get; set; }

    #endregion

    #region Security and Permissions

    /// <summary>
    /// Library Permissions - Granular access control for libraries
    /// </summary>
    public DbSet<LibraryPermission> LibraryPermissions { get; set; }

    /// <summary>
    /// Template Permissions - Granular access control for templates
    /// </summary>
    public DbSet<TemplatePermission> TemplatePermissions { get; set; }

    /// <summary>
    /// Workflow Library Permissions - Granular access control for workflow libraries
    /// </summary>
    public DbSet<WorkflowLibraryPermission> WorkflowLibraryPermissions { get; set; }

    // TODO: Create FlowPermission entity when implementing workflow permissions
    // public DbSet<FlowPermission> FlowPermissions { get; set; }

    #endregion

    #region Analytics and Testing

    /// <summary>
    /// A/B Tests - Testing framework for prompts and workflows
    /// </summary>
    public DbSet<ABTest> ABTests { get; set; }

    /// <summary>
    /// A/B Test Variants - Individual test variations
    /// </summary>
    public DbSet<ABTestVariant> ABTestVariants { get; set; }

    /// <summary>
    /// A/B Test Results - Captured metrics and outcomes
    /// </summary>
    public DbSet<ABTestResult> ABTestResults { get; set; }

    // TODO: Create these entities when implementing advanced analytics
    // public DbSet<QualityMetric> QualityMetrics { get; set; }
    // public DbSet<UsageAnalytic> UsageAnalytics { get; set; }

    #endregion

    #region Configuration and Management

    // TODO: Create these entities when implementing provider management
    // public DbSet<ModelProviderConfig> ModelProviderConfigs { get; set; }
    // public DbSet<RateLimitPolicy> RateLimitPolicies { get; set; }
    // public DbSet<CachePolicy> CachePolicies { get; set; }

    #endregion

    /// <summary>
    /// Configures the database model with enterprise-grade features
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply global query filters for multi-tenancy and soft delete
        ConfigureGlobalFilters(modelBuilder);

        // Configure core domain entities
        ConfigurePromptLab(modelBuilder);
        ConfigurePromptLibrary(modelBuilder);
        ConfigurePromptTemplate(modelBuilder);
        ConfigurePromptVariable(modelBuilder);
        ConfigurePromptExecution(modelBuilder);
        ConfigureVariableCollection(modelBuilder);

        // Configure workflow engine
        ConfigureWorkflowEngine(modelBuilder);

        // Configure content and versioning
        ConfigureContentAndVersioning(modelBuilder);

        // Configure security and permissions
        ConfigureSecurityAndPermissions(modelBuilder);

        // Configure analytics and testing
        ConfigureAnalyticsAndTesting(modelBuilder);

        // Configure management and optimization
        ConfigureManagementAndOptimization(modelBuilder);

        // Configure enum conversions
        ConfigureEnumConversions(modelBuilder);
    }

    #region Entity Configuration Methods

    private void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        // Configure global query filters for all AuditableEntity descendants
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                // Soft delete filter
                var softDeleteFilter = CreateSoftDeleteFilter(entityType.ClrType);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(softDeleteFilter);

                // Multi-tenancy filter (if organization context is available)
                if (_currentOrganizationId.HasValue)
                {
                    var tenancyFilter = CreateTenancyFilter(entityType.ClrType, _currentOrganizationId.Value);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(tenancyFilter);
                }
            }
        }
    }

    private void ConfigurePromptLab(ModelBuilder modelBuilder)
    {
        // PromptLab configuration
        modelBuilder.Entity<PromptLab>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever(); // Guid generated in entity

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.LabId)
                .IsRequired()
                .HasMaxLength(50);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Owner)
                .HasMaxLength(100);

            entity.Property(e => e.Tags)
                .HasMaxLength(1000);

            // Enum conversions
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Visibility)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Concurrency control
            entity.Property(e => e.RowVersion)
                .IsRowVersion();

            // Indexes for performance
            entity.HasIndex(e => e.LabId)
                .IsUnique()
                .HasDatabaseName("IX_PromptLabs_LabId");

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_PromptLabs_Name");

            entity.HasIndex(e => e.OrganizationId)
                .HasDatabaseName("IX_PromptLabs_OrganizationId");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_PromptLabs_Status");

            entity.HasIndex(e => e.Visibility)
                .HasDatabaseName("IX_PromptLabs_Visibility");

            entity.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_PromptLabs_CreatedAt");

            entity.HasIndex(e => e.Owner)
                .HasDatabaseName("IX_PromptLabs_Owner");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.OrganizationId, e.Status, e.Visibility })
                .HasDatabaseName("IX_PromptLabs_Org_Status_Visibility");

            entity.HasIndex(e => new { e.Status, e.CreatedAt })
                .HasDatabaseName("IX_PromptLabs_Status_CreatedAt");

            entity.HasIndex(e => new { e.Owner, e.Status })
                .HasDatabaseName("IX_PromptLabs_Owner_Status");

            entity.HasIndex(e => new { e.Visibility, e.Status, e.CreatedAt })
                .HasDatabaseName("IX_PromptLabs_Visibility_Status_CreatedAt");

            // Covering index for lab listing queries
            entity.HasIndex(e => new { e.OrganizationId, e.Status })
                .IncludeProperties(e => new { e.Name, e.Description, e.LabId, e.Owner, e.Visibility, e.CreatedAt })
                .HasDatabaseName("IX_PromptLabs_Listing_Covering");
        });
    }

    private void ConfigurePromptLibrary(ModelBuilder modelBuilder)
    {
        // PromptLibrary configuration
        modelBuilder.Entity<PromptLibrary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Color)
                .HasMaxLength(7);

            entity.Property(e => e.Icon)
                .HasMaxLength(50);

            entity.Property(e => e.Tags)
                .HasMaxLength(1000);

            // Enum conversions - Note: Category is a complex entity, not an enum
            entity.Property(e => e.Visibility)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Relationships
            entity.HasOne(e => e.PromptLab)
                .WithMany(pl => pl.PromptLibraries)
                .HasForeignKey(e => e.PromptLabId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptLibraries_PromptLab");

            // Note: Category is a navigation property to LibraryCategory entity
            // This relationship should be configured separately if needed

            // Indexes for performance
            entity.HasIndex(e => new { e.PromptLabId, e.Name })
                .IsUnique()
                .HasDatabaseName("IX_PromptLibraries_LabId_Name");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_PromptLibraries_Status");

            entity.HasIndex(e => e.Visibility)
                .HasDatabaseName("IX_PromptLibraries_Visibility");

            entity.HasIndex(e => e.LastActivityAt)
                .HasDatabaseName("IX_PromptLibraries_LastActivity");

            entity.HasIndex(e => e.SortOrder)
                .HasDatabaseName("IX_PromptLibraries_SortOrder");

            entity.HasIndex(e => e.IsPinned)
                .HasDatabaseName("IX_PromptLibraries_IsPinned");

            entity.HasIndex(e => e.RequiresApproval)
                .HasDatabaseName("IX_PromptLibraries_RequiresApproval");

            entity.HasIndex(e => e.TemplateCount)
                .HasDatabaseName("IX_PromptLibraries_TemplateCount");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.PromptLabId, e.Status, e.Visibility })
                .HasDatabaseName("IX_PromptLibraries_Lab_Status_Visibility");

            entity.HasIndex(e => new { e.Status, e.Visibility, e.LastActivityAt })
                .HasDatabaseName("IX_PromptLibraries_Status_Visibility_Activity");

            entity.HasIndex(e => new { e.IsPinned, e.SortOrder })
                .HasDatabaseName("IX_PromptLibraries_Pinned_SortOrder");

            entity.HasIndex(e => new { e.RequiresApproval, e.Status })
                .HasDatabaseName("IX_PromptLibraries_Approval_Status");

            // Covering index for library listing queries
            entity.HasIndex(e => new { e.PromptLabId, e.Status })
                .IncludeProperties(e => new { e.Name, e.Description, e.Color, e.Icon, e.TemplateCount, e.LastActivityAt })
                .HasDatabaseName("IX_PromptLibraries_Listing_Covering");
        });
    }

    private void ConfigurePromptTemplate(ModelBuilder modelBuilder)
    {
        // PromptTemplate configuration
        modelBuilder.Entity<PromptTemplate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(20);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Tags)
                .HasMaxLength(1000);

            entity.Property(e => e.OutputLanguage)
                .HasMaxLength(10);

            entity.Property(e => e.RecommendedProviders)
                .HasMaxLength(200);

            entity.Property(e => e.ContentHash)
                .HasMaxLength(64);

            entity.Property(e => e.License)
                .HasMaxLength(50);

            // Enum conversions
            entity.Property(e => e.Category)
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Size)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal precision for costs and scores
            entity.Property(e => e.AverageCost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.QualityScore)
                .HasColumnType("decimal(3,2)");

            // Relationships
            entity.HasOne(e => e.PromptLibrary)
                .WithMany(pl => pl.PromptTemplates)
                .HasForeignKey(e => e.PromptLibraryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptTemplates_PromptLibrary");

            entity.HasOne(e => e.BaseTemplate)
                .WithMany()
                .HasForeignKey(e => e.BaseTemplateId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_PromptTemplates_BaseTemplate");

            // Content relationship is configured in PromptContent configuration

            // Indexes for performance
            entity.HasIndex(e => new { e.PromptLibraryId, e.Name })
                .HasDatabaseName("IX_PromptTemplates_LibraryId_Name");

            entity.HasIndex(e => e.Category)
                .HasDatabaseName("IX_PromptTemplates_Category");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_PromptTemplates_Status");

            entity.HasIndex(e => e.ContentHash)
                .HasDatabaseName("IX_PromptTemplates_ContentHash");

            entity.HasIndex(e => e.LastExecutedAt)
                .HasDatabaseName("IX_PromptTemplates_LastExecuted");

            entity.HasIndex(e => e.QualityScore)
                .HasDatabaseName("IX_PromptTemplates_QualityScore");

            entity.HasIndex(e => e.Size)
                .HasDatabaseName("IX_PromptTemplates_Size");

            entity.HasIndex(e => e.RequiresApproval)
                .HasDatabaseName("IX_PromptTemplates_RequiresApproval");

            entity.HasIndex(e => e.ExecutionCount)
                .HasDatabaseName("IX_PromptTemplates_ExecutionCount");

            entity.HasIndex(e => e.AverageResponseTimeMs)
                .HasDatabaseName("IX_PromptTemplates_AverageResponseTime");

            entity.HasIndex(e => e.BaseTemplateId)
                .HasDatabaseName("IX_PromptTemplates_BaseTemplateId");

            entity.HasIndex(e => e.OutputLanguage)
                .HasDatabaseName("IX_PromptTemplates_OutputLanguage");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.PromptLibraryId, e.Status, e.Category })
                .HasDatabaseName("IX_PromptTemplates_Library_Status_Category");

            entity.HasIndex(e => new { e.Status, e.RequiresApproval })
                .HasDatabaseName("IX_PromptTemplates_Status_RequiresApproval");

            entity.HasIndex(e => new { e.Category, e.Size, e.QualityScore })
                .HasDatabaseName("IX_PromptTemplates_Category_Size_Quality");

            entity.HasIndex(e => new { e.ExecutionCount, e.LastExecutedAt })
                .HasDatabaseName("IX_PromptTemplates_ExecutionCount_LastExecuted");

            entity.HasIndex(e => new { e.BaseTemplateId, e.Version })
                .HasDatabaseName("IX_PromptTemplates_BaseTemplate_Version");

            entity.HasIndex(e => new { e.OutputLanguage, e.Category })
                .HasDatabaseName("IX_PromptTemplates_Language_Category");

            // Covering index for template listing queries
            entity.HasIndex(e => new { e.PromptLibraryId, e.Status })
                .IncludeProperties(e => new { e.Name, e.Description, e.Category, e.Version, e.QualityScore, e.ExecutionCount, e.LastExecutedAt })
                .HasDatabaseName("IX_PromptTemplates_Listing_Covering");
        });
    }

    private void ConfigurePromptVariable(ModelBuilder modelBuilder)
    {
        // PromptVariable configuration
        modelBuilder.Entity<PromptVariable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(200);

            entity.Property(e => e.DefaultValue);

            entity.Property(e => e.ValidationRules)
                .HasMaxLength(1000);

            entity.Property(e => e.HelpText)
                .HasMaxLength(500);

            entity.Property(e => e.ExampleValues)
                .HasMaxLength(1000);

            // Enum conversions
            entity.Property(e => e.Type)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Relationships
            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.Variables)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptVariables_PromptTemplate");

            // Indexes for performance
            entity.HasIndex(e => new { e.PromptTemplateId, e.Name })
                .IsUnique()
                .HasDatabaseName("IX_PromptVariables_TemplateId_Name");

            entity.HasIndex(e => e.Type)
                .HasDatabaseName("IX_PromptVariables_Type");

            entity.HasIndex(e => e.IsRequired)
                .HasDatabaseName("IX_PromptVariables_IsRequired");

            entity.HasIndex(e => e.SortOrder)
                .HasDatabaseName("IX_PromptVariables_SortOrder");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.PromptTemplateId, e.SortOrder })
                .HasDatabaseName("IX_PromptVariables_Template_SortOrder");

            entity.HasIndex(e => new { e.PromptTemplateId, e.Type, e.IsRequired })
                .HasDatabaseName("IX_PromptVariables_Template_Type_Required");

            entity.HasIndex(e => new { e.Type, e.IsRequired })
                .HasDatabaseName("IX_PromptVariables_Type_Required");

            // Covering index for variable form rendering
            entity.HasIndex(e => new { e.PromptTemplateId, e.SortOrder })
                .IncludeProperties(e => new { e.Name, e.Description, e.Type, e.IsRequired, e.DefaultValue, e.HelpText })
                .HasDatabaseName("IX_PromptVariables_Form_Covering");
        });
    }

    private void ConfigurePromptExecution(ModelBuilder modelBuilder)
    {
        // PromptExecution configuration
        modelBuilder.Entity<PromptExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.ResolvedPrompt)
                .IsRequired();

            // Optional string properties with proper lengths
            entity.Property(e => e.VariableValues);

            entity.Property(e => e.AiProvider)
                .HasMaxLength(50);

            entity.Property(e => e.Model)
                .HasMaxLength(50);

            entity.Property(e => e.Response);

            entity.Property(e => e.ErrorMessage);

            entity.Property(e => e.ExecutedBy)
                .HasMaxLength(100);

            entity.Property(e => e.ExecutionContext)
                .HasMaxLength(50);

            // Enum conversions
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.QualityScore)
                .HasColumnType("decimal(3,2)");

            // Relationships
            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.Executions)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptExecutions_PromptTemplate");

            entity.HasOne(e => e.VariableCollection)
                .WithMany(vc => vc.Executions)
                .HasForeignKey(e => e.VariableCollectionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PromptExecutions_VariableCollection");

            // Indexes for performance
            entity.HasIndex(e => e.ExecutedAt)
                .HasDatabaseName("IX_PromptExecutions_ExecutedAt");

            entity.HasIndex(e => e.PromptTemplateId)
                .HasDatabaseName("IX_PromptExecutions_TemplateId");

            entity.HasIndex(e => e.AiProvider)
                .HasDatabaseName("IX_PromptExecutions_Provider");

            entity.HasIndex(e => e.Model)
                .HasDatabaseName("IX_PromptExecutions_Model");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_PromptExecutions_Status");

            entity.HasIndex(e => e.ExecutedBy)
                .HasDatabaseName("IX_PromptExecutions_ExecutedBy");

            entity.HasIndex(e => e.ExecutionContext)
                .HasDatabaseName("IX_PromptExecutions_ExecutionContext");

            entity.HasIndex(e => e.ResponseTimeMs)
                .HasDatabaseName("IX_PromptExecutions_ResponseTimeMs");

            entity.HasIndex(e => e.TokensUsed)
                .HasDatabaseName("IX_PromptExecutions_TokensUsed");

            entity.HasIndex(e => e.QualityScore)
                .HasDatabaseName("IX_PromptExecutions_QualityScore");

            entity.HasIndex(e => e.VariableCollectionId)
                .HasDatabaseName("IX_PromptExecutions_VariableCollectionId");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.PromptTemplateId, e.Status, e.ExecutedAt })
                .HasDatabaseName("IX_PromptExecutions_Template_Status_ExecutedAt");

            entity.HasIndex(e => new { e.AiProvider, e.Model, e.ExecutedAt })
                .HasDatabaseName("IX_PromptExecutions_Provider_Model_ExecutedAt");

            entity.HasIndex(e => new { e.ExecutedBy, e.ExecutionContext, e.ExecutedAt })
                .HasDatabaseName("IX_PromptExecutions_User_Context_ExecutedAt");

            entity.HasIndex(e => new { e.Status, e.ExecutedAt })
                .HasDatabaseName("IX_PromptExecutions_Status_ExecutedAt");

            entity.HasIndex(e => new { e.VariableCollectionId, e.ExecutedAt })
                .HasDatabaseName("IX_PromptExecutions_Collection_ExecutedAt");

            // Covering index for analytics queries
            entity.HasIndex(e => new { e.PromptTemplateId, e.ExecutedAt })
                .IncludeProperties(e => new { e.Status, e.ResponseTimeMs, e.TokensUsed, e.Cost, e.QualityScore })
                .HasDatabaseName("IX_PromptExecutions_Analytics_Covering");
        });
    }

    private void ConfigureVariableCollection(ModelBuilder modelBuilder)
    {
        // VariableCollection configuration
        modelBuilder.Entity<VariableCollection>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.VariableSets)
                .IsRequired();

            entity.Property(e => e.Source)
                .IsRequired()
                .HasMaxLength(50);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.OriginalCsvData);

            entity.Property(e => e.Tags)
                .HasMaxLength(500);

            // Enum conversions
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Relationships
            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.VariableCollections)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VariableCollections_PromptTemplate");

            // Indexes for performance
            entity.HasIndex(e => e.PromptTemplateId)
                .HasDatabaseName("IX_VariableCollections_TemplateId");

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_VariableCollections_Name");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_VariableCollections_Status");

            entity.HasIndex(e => e.Source)
                .HasDatabaseName("IX_VariableCollections_Source");

            entity.HasIndex(e => e.IsArchived)
                .HasDatabaseName("IX_VariableCollections_IsArchived");

            entity.HasIndex(e => e.LastUsedAt)
                .HasDatabaseName("IX_VariableCollections_LastUsedAt");

            entity.HasIndex(e => e.UsageCount)
                .HasDatabaseName("IX_VariableCollections_UsageCount");

            entity.HasIndex(e => e.VariableSetCount)
                .HasDatabaseName("IX_VariableCollections_VariableSetCount");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.PromptTemplateId, e.Status, e.IsArchived })
                .HasDatabaseName("IX_VariableCollections_Template_Status_Archived");

            entity.HasIndex(e => new { e.Status, e.IsArchived, e.LastUsedAt })
                .HasDatabaseName("IX_VariableCollections_Status_Archived_LastUsed");

            entity.HasIndex(e => new { e.Source, e.Status })
                .HasDatabaseName("IX_VariableCollections_Source_Status");

            entity.HasIndex(e => new { e.UsageCount, e.LastUsedAt })
                .HasDatabaseName("IX_VariableCollections_Usage_LastUsed");

            // Covering index for collection listing queries
            entity.HasIndex(e => new { e.PromptTemplateId, e.Status })
                .IncludeProperties(e => new { e.Name, e.Description, e.VariableSetCount, e.LastUsedAt, e.UsageCount })
                .HasDatabaseName("IX_VariableCollections_Listing_Covering");
        });
    }

    private void ConfigureWorkflowEngine(ModelBuilder modelBuilder)
    {
        // WorkflowLibrary configuration
        modelBuilder.Entity<WorkflowLibrary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Color)
                .HasMaxLength(7);

            entity.Property(e => e.Icon)
                .HasMaxLength(50);

            entity.Property(e => e.Tags)
                .HasMaxLength(1000);

            // Enum conversions
            entity.Property(e => e.Visibility)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.DefaultCostLimit)
                .HasColumnType("decimal(10,4)");

            // Relationships
            entity.HasOne(e => e.PromptLab)
                .WithMany(pl => pl.WorkflowLibraries)
                .HasForeignKey(e => e.PromptLabId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_WorkflowLibraries_PromptLab");

            entity.HasOne(e => e.WorkflowCategory)
                .WithMany()
                .HasForeignKey(e => e.WorkflowCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_WorkflowLibraries_WorkflowCategory");

            // Indexes for performance
            entity.HasIndex(e => e.PromptLabId)
                .HasDatabaseName("IX_WorkflowLibraries_PromptLabId");

            entity.HasIndex(e => e.WorkflowCategoryId)
                .HasDatabaseName("IX_WorkflowLibraries_WorkflowCategoryId");

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_WorkflowLibraries_Name");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_WorkflowLibraries_Status");

            entity.HasIndex(e => e.Visibility)
                .HasDatabaseName("IX_WorkflowLibraries_Visibility");

            entity.HasIndex(e => e.RequiresApproval)
                .HasDatabaseName("IX_WorkflowLibraries_RequiresApproval");

            entity.HasIndex(e => e.IsPinned)
                .HasDatabaseName("IX_WorkflowLibraries_IsPinned");

            entity.HasIndex(e => e.SortOrder)
                .HasDatabaseName("IX_WorkflowLibraries_SortOrder");

            entity.HasIndex(e => e.LastActivityAt)
                .HasDatabaseName("IX_WorkflowLibraries_LastActivityAt");

            entity.HasIndex(e => e.WorkflowCount)
                .HasDatabaseName("IX_WorkflowLibraries_WorkflowCount");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.PromptLabId, e.Status, e.Visibility })
                .HasDatabaseName("IX_WorkflowLibraries_Lab_Status_Visibility");

            entity.HasIndex(e => new { e.WorkflowCategoryId, e.Status })
                .HasDatabaseName("IX_WorkflowLibraries_Category_Status");

            entity.HasIndex(e => new { e.PromptLabId, e.SortOrder })
                .HasDatabaseName("IX_WorkflowLibraries_Lab_SortOrder");

            entity.HasIndex(e => new { e.IsPinned, e.LastActivityAt })
                .HasDatabaseName("IX_WorkflowLibraries_Pinned_LastActivity");

            entity.HasIndex(e => new { e.Status, e.LastActivityAt, e.WorkflowCount })
                .HasDatabaseName("IX_WorkflowLibraries_Status_Activity_Count");

            // Covering index for library listing queries
            entity.HasIndex(e => new { e.PromptLabId, e.Status })
                .IncludeProperties(e => new { e.Name, e.Description, e.Color, e.Icon, e.WorkflowCount, e.LastActivityAt })
                .HasDatabaseName("IX_WorkflowLibraries_Listing_Covering");
        });

        // WorkflowCategory configuration
        modelBuilder.Entity<WorkflowCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.IconName)
                .HasMaxLength(50);

            entity.Property(e => e.Color)
                .HasMaxLength(7);

            // Enum conversions
            entity.Property(e => e.CategoryType)
                .HasConversion<string>()
                .HasMaxLength(50);

            // Self-referencing relationship for hierarchy
            entity.HasOne(e => e.ParentCategory)
                .WithMany(e => e.ChildCategories)
                .HasForeignKey(e => e.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_WorkflowCategories_ParentCategory");

            // Indexes for performance
            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_WorkflowCategories_Name");

            entity.HasIndex(e => e.ParentCategoryId)
                .HasDatabaseName("IX_WorkflowCategories_ParentCategory");

            entity.HasIndex(e => e.IsSystemDefined)
                .HasDatabaseName("IX_WorkflowCategories_IsSystemDefined");

            entity.HasIndex(e => e.CategoryType)
                .HasDatabaseName("IX_WorkflowCategories_CategoryType");

            entity.HasIndex(e => e.DisplayOrder)
                .HasDatabaseName("IX_WorkflowCategories_DisplayOrder");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.ParentCategoryId, e.DisplayOrder })
                .HasDatabaseName("IX_WorkflowCategories_Parent_DisplayOrder");

            entity.HasIndex(e => new { e.IsSystemDefined, e.CategoryType })
                .HasDatabaseName("IX_WorkflowCategories_SystemDefined_Type");

            entity.HasIndex(e => new { e.ParentCategoryId, e.Name })
                .HasDatabaseName("IX_WorkflowCategories_Parent_Name");

            entity.HasIndex(e => new { e.CategoryType, e.IsSystemDefined, e.DisplayOrder })
                .HasDatabaseName("IX_WorkflowCategories_Type_System_Order");

            // Covering index for hierarchy queries
            entity.HasIndex(e => e.ParentCategoryId)
                .IncludeProperties(e => new { e.Name, e.DisplayOrder, e.IsSystemDefined, e.IconName, e.Color })
                .HasDatabaseName("IX_WorkflowCategories_Hierarchy_Covering");
        });

        // PromptFlow configuration
        modelBuilder.Entity<PromptFlow>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.FlowData)
                .IsRequired();

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Tags)
                .HasMaxLength(1000);

            entity.Property(e => e.GenerationPrompt);

            entity.Property(e => e.FlowHash)
                .HasMaxLength(64);

            // Enum conversions
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.StorageMode)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.ExpectedCost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.AverageCost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.QualityScore)
                .HasColumnType("decimal(3,2)");

            entity.Property(e => e.AiConfidenceScore)
                .HasColumnType("decimal(3,2)");

            // Relationships
            entity.HasOne(e => e.WorkflowLibrary)
                .WithMany(wl => wl.PromptFlows)
                .HasForeignKey(e => e.WorkflowLibraryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptFlows_WorkflowLibrary");

            entity.HasOne(e => e.BaseFlow)
                .WithMany()
                .HasForeignKey(e => e.BaseFlowId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_PromptFlows_BaseFlow");

            entity.HasOne(e => e.WorkflowCategory)
                .WithMany(wc => wc.PromptFlows)
                .HasForeignKey(e => e.WorkflowCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_PromptFlows_WorkflowCategory");

            // Indexes for performance
            entity.HasIndex(e => e.WorkflowLibraryId)
                .HasDatabaseName("IX_PromptFlows_WorkflowLibraryId");

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_PromptFlows_Name");

            entity.HasIndex(e => e.WorkflowCategoryId)
                .HasDatabaseName("IX_PromptFlows_WorkflowCategory");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_PromptFlows_Status");

            entity.HasIndex(e => e.Version)
                .HasDatabaseName("IX_PromptFlows_Version");

            entity.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_PromptFlows_IsActive");

            entity.HasIndex(e => e.IsAiGenerated)
                .HasDatabaseName("IX_PromptFlows_IsAiGenerated");

            entity.HasIndex(e => e.RequiresApproval)
                .HasDatabaseName("IX_PromptFlows_RequiresApproval");

            entity.HasIndex(e => e.LastExecutedAt)
                .HasDatabaseName("IX_PromptFlows_LastExecuted");

            entity.HasIndex(e => e.ExecutionCount)
                .HasDatabaseName("IX_PromptFlows_ExecutionCount");

            entity.HasIndex(e => e.QualityScore)
                .HasDatabaseName("IX_PromptFlows_QualityScore");

            entity.HasIndex(e => e.ComplexityScore)
                .HasDatabaseName("IX_PromptFlows_ComplexityScore");

            entity.HasIndex(e => e.IsInExperiment)
                .HasDatabaseName("IX_PromptFlows_IsInExperiment");

            entity.HasIndex(e => e.StorageMode)
                .HasDatabaseName("IX_PromptFlows_StorageMode");

            entity.HasIndex(e => e.FlowHash)
                .HasDatabaseName("IX_PromptFlows_FlowHash");

            entity.HasIndex(e => e.BaseFlowId)
                .HasDatabaseName("IX_PromptFlows_BaseFlowId");

            entity.HasIndex(e => e.LastSyncAt)
                .HasDatabaseName("IX_PromptFlows_LastSyncAt");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.WorkflowLibraryId, e.Status, e.IsActive })
                .HasDatabaseName("IX_PromptFlows_Library_Status_Active");

            entity.HasIndex(e => new { e.WorkflowCategoryId, e.Status })
                .HasDatabaseName("IX_PromptFlows_Category_Status");

            entity.HasIndex(e => new { e.IsAiGenerated, e.QualityScore })
                .HasDatabaseName("IX_PromptFlows_AiGenerated_Quality");

            entity.HasIndex(e => new { e.Status, e.IsActive, e.LastExecutedAt })
                .HasDatabaseName("IX_PromptFlows_Status_Active_LastExecuted");

            entity.HasIndex(e => new { e.ComplexityScore, e.ExecutionCount, e.AverageCost })
                .HasDatabaseName("IX_PromptFlows_Complexity_Usage_Cost");

            entity.HasIndex(e => new { e.BaseFlowId, e.Version })
                .HasDatabaseName("IX_PromptFlows_BaseFlow_Version");

            // Covering index for analytics queries
            entity.HasIndex(e => new { e.WorkflowLibraryId, e.Status })
                .IncludeProperties(e => new { e.Name, e.Version, e.ExecutionCount, e.AverageCost, e.QualityScore })
                .HasDatabaseName("IX_PromptFlows_Analytics_Covering");
        });

        // FlowNode configuration
        modelBuilder.Entity<FlowNode>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.NodeKey)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.NodeData)
                .IsRequired();

            // Optional string properties with proper lengths
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.TemplateVersion)
                .HasMaxLength(20);

            entity.Property(e => e.TemplateRole)
                .HasMaxLength(50);

            entity.Property(e => e.StyleData);

            entity.Property(e => e.ValidationMessages);

            // Enum conversions
            entity.Property(e => e.NodeType)
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(e => e.ValidationStatus)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Position and visual properties with proper precision
            entity.Property(e => e.PositionX)
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.PositionY)
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.Width)
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.Height)
                .HasColumnType("decimal(10,2)");

            // Decimal properties with proper precision
            entity.Property(e => e.AverageCost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.SuccessRate)
                .HasColumnType("decimal(5,4)");

            // Relationships
            entity.HasOne(e => e.Flow)
                .WithMany(f => f.Nodes)
                .HasForeignKey(e => e.FlowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FlowNodes_PromptFlow");

            entity.HasOne(e => e.PromptTemplate)
                .WithMany()
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_FlowNodes_PromptTemplate");

            // Indexes for performance
            entity.HasIndex(e => e.FlowId)
                .HasDatabaseName("IX_FlowNodes_FlowId");

            entity.HasIndex(e => new { e.FlowId, e.NodeKey })
                .IsUnique()
                .HasDatabaseName("IX_FlowNodes_FlowId_NodeKey");

            entity.HasIndex(e => e.NodeType)
                .HasDatabaseName("IX_FlowNodes_NodeType");

            entity.HasIndex(e => e.ValidationStatus)
                .HasDatabaseName("IX_FlowNodes_ValidationStatus");

            entity.HasIndex(e => e.PromptTemplateId)
                .HasDatabaseName("IX_FlowNodes_PromptTemplateId");

            entity.HasIndex(e => e.IsEnabled)
                .HasDatabaseName("IX_FlowNodes_IsEnabled");

            entity.HasIndex(e => e.LastExecutedAt)
                .HasDatabaseName("IX_FlowNodes_LastExecuted");

            entity.HasIndex(e => e.ExecutionCount)
                .HasDatabaseName("IX_FlowNodes_ExecutionCount");

            entity.HasIndex(e => e.Priority)
                .HasDatabaseName("IX_FlowNodes_Priority");

            entity.HasIndex(e => e.SuccessRate)
                .HasDatabaseName("IX_FlowNodes_SuccessRate");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.FlowId, e.NodeType, e.IsEnabled })
                .HasDatabaseName("IX_FlowNodes_Flow_Type_Enabled");

            entity.HasIndex(e => new { e.PromptTemplateId, e.TemplateVersion })
                .HasDatabaseName("IX_FlowNodes_Template_Version");

            entity.HasIndex(e => new { e.ValidationStatus, e.IsEnabled })
                .HasDatabaseName("IX_FlowNodes_Validation_Enabled");

            entity.HasIndex(e => new { e.FlowId, e.Priority, e.ExecutionCount })
                .HasDatabaseName("IX_FlowNodes_Flow_Priority_Count");

            // Covering index for analytics queries
            entity.HasIndex(e => new { e.FlowId, e.NodeType })
                .IncludeProperties(e => new { e.NodeKey, e.IsEnabled, e.ExecutionCount, e.AverageCost })
                .HasDatabaseName("IX_FlowNodes_Analytics_Covering");
        });

        // FlowEdge configuration
        modelBuilder.Entity<FlowEdge>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // String properties with proper lengths
            entity.Property(e => e.SourceHandle)
                .HasMaxLength(50);

            entity.Property(e => e.TargetHandle)
                .HasMaxLength(50);

            entity.Property(e => e.Label)
                .HasMaxLength(100);

            // Enum conversion
            entity.Property(e => e.Type)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.ValidationStatus)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.SuccessRate)
                .HasColumnType("decimal(5,4)");

            entity.Property(e => e.ConditionSuccessRate)
                .HasColumnType("decimal(5,4)");

            // Relationships - FIX: Changed to prevent cascade cycles
            entity.HasOne(e => e.Flow)
                .WithMany(f => f.Edges)
                .HasForeignKey(e => e.FlowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FlowEdges_PromptFlow");

            entity.HasOne(e => e.SourceNode)
                .WithMany(n => n.OutgoingEdges)
                .HasForeignKey(e => e.SourceNodeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FlowEdges_FlowNodes_SourceNodeId");

            entity.HasOne(e => e.TargetNode)
                .WithMany(n => n.IncomingEdges)
                .HasForeignKey(e => e.TargetNodeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_FlowEdges_TargetNode");

            // Rest of indexes stay the same...
        });

        // FlowExecution configuration
        modelBuilder.Entity<FlowExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.FlowVersion)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.InputVariables)
                .IsRequired();

            entity.Property(e => e.OutputResult)
                .IsRequired();

            entity.Property(e => e.ExecutionContext)
                .IsRequired();

            // Optional string properties with proper lengths
            entity.Property(e => e.ExecutedBy)
                .HasMaxLength(100);

            entity.Property(e => e.Environment)
                .HasMaxLength(50);

            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(2000);

            entity.Property(e => e.UserFeedback)
                .HasMaxLength(1000);

            // Enum conversion
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.TotalCost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.QualityScore)
                .HasColumnType("decimal(3,2)");

            // Relationships
            entity.HasOne(e => e.Flow)
                .WithMany(f => f.Executions)
                .HasForeignKey(e => e.FlowId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FlowExecutions_PromptFlow");

            entity.HasOne(e => e.Variant)
                .WithMany()
                .HasForeignKey(e => e.VariantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_FlowExecutions_FlowVariant");

            // Indexes for performance
            entity.HasIndex(e => e.FlowId)
                .HasDatabaseName("IX_FlowExecutions_FlowId");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_FlowExecutions_Status");

            entity.HasIndex(e => e.Environment)
                .HasDatabaseName("IX_FlowExecutions_Environment");

            entity.HasIndex(e => e.ExecutedBy)
                .HasDatabaseName("IX_FlowExecutions_ExecutedBy");

            entity.HasIndex(e => e.ExperimentId)
                .HasDatabaseName("IX_FlowExecutions_ExperimentId");

            entity.HasIndex(e => e.VariantId)
                .HasDatabaseName("IX_FlowExecutions_VariantId");

            entity.HasIndex(e => e.QualityScore)
                .HasDatabaseName("IX_FlowExecutions_QualityScore");

            entity.HasIndex(e => e.UserRating)
                .HasDatabaseName("IX_FlowExecutions_UserRating");

            entity.HasIndex(e => e.FeedbackAt)
                .HasDatabaseName("IX_FlowExecutions_FeedbackAt");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.FlowId, e.Status, e.CreatedAt })
                .HasDatabaseName("IX_FlowExecutions_Flow_Status_Created");

            entity.HasIndex(e => new { e.ExecutedBy, e.Environment, e.CreatedAt })
                .HasDatabaseName("IX_FlowExecutions_User_Env_Created");

            entity.HasIndex(e => new { e.ExperimentId, e.VariantId })
                .HasDatabaseName("IX_FlowExecutions_Experiment_Variant");
        });

        // NodeExecution configuration
        modelBuilder.Entity<NodeExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.NodeKey)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.InputData)
                .IsRequired();

            // Optional string properties with proper lengths
            entity.Property(e => e.OutputData);

            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(2000);

            entity.Property(e => e.ErrorStackTrace);

            entity.Property(e => e.AiProvider)
                .HasMaxLength(50);

            entity.Property(e => e.AiModel)
                .HasMaxLength(100);

            entity.Property(e => e.TemplateVersion)
                .HasMaxLength(20);

            entity.Property(e => e.PerformanceMetrics);

            entity.Property(e => e.DebugInfo);

            entity.Property(e => e.CacheKey)
                .HasMaxLength(256);

            // Enum conversions
            entity.Property(e => e.NodeType)
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.QualityScore)
                .HasColumnType("decimal(3,2)");

            entity.Property(e => e.ConfidenceLevel)
                .HasColumnType("decimal(3,2)");

            // Relationships
            entity.HasOne(e => e.FlowExecution)
                .WithMany(fe => fe.NodeExecutions)
                .HasForeignKey(e => e.FlowExecutionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_NodeExecutions_FlowExecution");

            entity.HasOne(e => e.Node)
                .WithMany()
                .HasForeignKey(e => e.NodeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_NodeExecutions_FlowNode");

            entity.HasOne(e => e.PromptTemplate)
                .WithMany()
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_NodeExecutions_PromptTemplate");

            entity.HasOne(e => e.IncomingEdge)
                .WithMany()
                .HasForeignKey(e => e.IncomingEdgeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_NodeExecutions_IncomingEdge");

            // Indexes for performance
            entity.HasIndex(e => e.FlowExecutionId)
                .HasDatabaseName("IX_NodeExecutions_FlowExecutionId");

            entity.HasIndex(e => e.NodeId)
                .HasDatabaseName("IX_NodeExecutions_NodeId");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_NodeExecutions_Status");

            entity.HasIndex(e => e.NodeType)
                .HasDatabaseName("IX_NodeExecutions_NodeType");

            entity.HasIndex(e => e.ExecutionOrder)
                .HasDatabaseName("IX_NodeExecutions_ExecutionOrder");

            entity.HasIndex(e => e.StartTime)
                .HasDatabaseName("IX_NodeExecutions_StartTime");

            entity.HasIndex(e => e.EndTime)
                .HasDatabaseName("IX_NodeExecutions_EndTime");

            entity.HasIndex(e => e.AiProvider)
                .HasDatabaseName("IX_NodeExecutions_AiProvider");

            entity.HasIndex(e => e.AiModel)
                .HasDatabaseName("IX_NodeExecutions_AiModel");

            entity.HasIndex(e => e.PromptTemplateId)
                .HasDatabaseName("IX_NodeExecutions_PromptTemplateId");

            entity.HasIndex(e => e.QualityScore)
                .HasDatabaseName("IX_NodeExecutions_QualityScore");

            entity.HasIndex(e => e.CacheHit)
                .HasDatabaseName("IX_NodeExecutions_CacheHit");

            entity.HasIndex(e => e.RetryCount)
                .HasDatabaseName("IX_NodeExecutions_RetryCount");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.FlowExecutionId, e.ExecutionOrder })
                .HasDatabaseName("IX_NodeExecutions_Flow_Order");

            entity.HasIndex(e => new { e.NodeId, e.Status, e.StartTime })
                .HasDatabaseName("IX_NodeExecutions_Node_Status_Time");

            entity.HasIndex(e => new { e.AiProvider, e.AiModel, e.StartTime })
                .HasDatabaseName("IX_NodeExecutions_Provider_Model_Time");

            entity.HasIndex(e => new { e.PromptTemplateId, e.TemplateVersion, e.StartTime })
                .HasDatabaseName("IX_NodeExecutions_Template_Version_Time");

            entity.HasIndex(e => new { e.Status, e.StartTime, e.DurationMs })
                .HasDatabaseName("IX_NodeExecutions_Status_Time_Duration");
        });

        // ExecutionMetric configuration
        modelBuilder.Entity<ExecutionMetric>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.MetricName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.MetricValue)
                .IsRequired();

            entity.Property(e => e.MetricType)
                .IsRequired()
                .HasMaxLength(20);

            // Optional string properties with proper lengths
            entity.Property(e => e.MetricUnit)
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasMaxLength(200);

            // Relationships
            entity.HasOne(e => e.FlowExecution)
                .WithMany(fe => fe.Metrics)
                .HasForeignKey(e => e.FlowExecutionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ExecutionMetrics_FlowExecution");

            // Indexes for performance
            entity.HasIndex(e => e.FlowExecutionId)
                .HasDatabaseName("IX_ExecutionMetrics_FlowExecutionId");

            entity.HasIndex(e => e.MetricName)
                .HasDatabaseName("IX_ExecutionMetrics_MetricName");

            entity.HasIndex(e => e.MetricType)
                .HasDatabaseName("IX_ExecutionMetrics_MetricType");

            entity.HasIndex(e => e.MetricUnit)
                .HasDatabaseName("IX_ExecutionMetrics_MetricUnit");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.FlowExecutionId, e.MetricName })
                .HasDatabaseName("IX_ExecutionMetrics_Execution_Name");

            entity.HasIndex(e => new { e.MetricType, e.MetricName, e.CreatedAt })
                .HasDatabaseName("IX_ExecutionMetrics_Type_Name_Created");

            entity.HasIndex(e => new { e.FlowExecutionId, e.MetricType })
                .HasDatabaseName("IX_ExecutionMetrics_Execution_Type");

            // Covering index for analytics queries
            entity.HasIndex(e => new { e.MetricName, e.MetricType })
                .IncludeProperties(e => new { e.MetricValue, e.MetricUnit, e.CreatedAt })
                .HasDatabaseName("IX_ExecutionMetrics_Analytics_Covering");
        });

        // EdgeTraversal configuration
        modelBuilder.Entity<EdgeTraversal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Optional string properties with proper lengths
            entity.Property(e => e.ConditionResult)
                .HasMaxLength(500);

            entity.Property(e => e.TraversalData)
                .HasMaxLength(2000);

            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(1000);

            // Relationships
            entity.HasOne(e => e.NodeExecution)
                .WithMany(ne => ne.OutgoingTraversals)
                .HasForeignKey(e => e.NodeExecutionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EdgeTraversals_NodeExecution");

            entity.HasOne(e => e.Edge)
                .WithMany()
                .HasForeignKey(e => e.EdgeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_EdgeTraversals_FlowEdge");

            // Indexes for performance
            entity.HasIndex(e => e.NodeExecutionId)
                .HasDatabaseName("IX_EdgeTraversals_NodeExecutionId");

            entity.HasIndex(e => e.EdgeId)
                .HasDatabaseName("IX_EdgeTraversals_EdgeId");

            entity.HasIndex(e => e.TraversalTime)
                .HasDatabaseName("IX_EdgeTraversals_TraversalTime");

            entity.HasIndex(e => e.Success)
                .HasDatabaseName("IX_EdgeTraversals_Success");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.NodeExecutionId, e.TraversalTime })
                .HasDatabaseName("IX_EdgeTraversals_NodeExecution_Time");

            entity.HasIndex(e => new { e.EdgeId, e.Success, e.TraversalTime })
                .HasDatabaseName("IX_EdgeTraversals_Edge_Success_Time");

            entity.HasIndex(e => new { e.Success, e.TraversalTime })
                .HasDatabaseName("IX_EdgeTraversals_Success_Time");

            // Covering index for analytics queries
            entity.HasIndex(e => new { e.EdgeId, e.Success })
                .IncludeProperties(e => new { e.TraversalTime, e.ConditionResult })
                .HasDatabaseName("IX_EdgeTraversals_Analytics_Covering");
        });
    }

    private void ConfigureContentAndVersioning(ModelBuilder modelBuilder)
    {
        // PromptContent configuration
        modelBuilder.Entity<PromptContent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Content)
                .IsRequired();

            entity.Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Encoding)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("UTF-8");

            entity.Property(e => e.Language)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("en");

            // Optional properties with proper configurations
            entity.Property(e => e.CompressedContent);

            entity.Property(e => e.ContentHash)
                .HasMaxLength(64);

            // Relationships
            entity.HasOne(e => e.PromptTemplate)
                .WithOne(pt => pt.Content)
                .HasForeignKey<PromptContent>(c => c.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptContents_PromptTemplate");

            // Indexes for performance
            entity.HasIndex(e => e.PromptTemplateId)
                .IsUnique()
                .HasDatabaseName("IX_PromptContents_Metadata_Covering");

            entity.HasIndex(e => e.ContentHash)
                .HasDatabaseName("IX_PromptContents_ContentHash");

            entity.HasIndex(e => e.ContentType)
                .HasDatabaseName("IX_PromptContents_ContentType");

            entity.HasIndex(e => e.ContentSize)
                .HasDatabaseName("IX_PromptContents_ContentSize");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.ContentType, e.ContentSize })
                .HasDatabaseName("IX_PromptContents_Type_Size");

            entity.HasIndex(e => new { e.ContentHash, e.ContentSize })
                .HasDatabaseName("IX_PromptContents_Hash_Size");

            // Covering index for content metadata queries
            entity.HasIndex(e => e.PromptTemplateId)
                .IncludeProperties(e => new { e.ContentType, e.ContentSize, e.ContentHash, e.CreatedAt })
                .HasDatabaseName("IX_PromptContents_Metadata_Covering");
        });

        // TemplateVersion configuration
        modelBuilder.Entity<TemplateVersion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Content)
                .IsRequired();

            // Optional string properties with proper lengths
            entity.Property(e => e.VersionName)
                .HasMaxLength(100);

            entity.Property(e => e.MetadataSnapshot);

            entity.Property(e => e.VariablesSnapshot);

            entity.Property(e => e.ChangeNotes)
                .HasMaxLength(1000);

            entity.Property(e => e.ContentHash)
                .HasMaxLength(64);

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(100);

            // Enum conversions
            entity.Property(e => e.ChangeType)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.ApprovalStatus)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.AverageCost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.QualityScore)
                .HasColumnType("decimal(3,2)");

            // Relationships
            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.Versions)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TemplateVersions_PromptTemplate");

            entity.HasOne(e => e.ParentVersion)
                .WithMany()
                .HasForeignKey(e => e.ParentVersionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TemplateVersions_ParentVersion");

            // Indexes for performance
            entity.HasIndex(e => new { e.PromptTemplateId, e.Version })
                .IsUnique()
                .HasDatabaseName("IX_TemplateVersions_TemplateId_Version");

            entity.HasIndex(e => e.ApprovalStatus)
                .HasDatabaseName("IX_TemplateVersions_ApprovalStatus");

            entity.HasIndex(e => e.ChangeType)
                .HasDatabaseName("IX_TemplateVersions_ChangeType");

            entity.HasIndex(e => e.IsStable)
                .HasDatabaseName("IX_TemplateVersions_IsStable");

            entity.HasIndex(e => e.IsCurrent)
                .HasDatabaseName("IX_TemplateVersions_IsCurrent");

            entity.HasIndex(e => e.ContentHash)
                .HasDatabaseName("IX_TemplateVersions_ContentHash");

            entity.HasIndex(e => e.ApprovedAt)
                .HasDatabaseName("IX_TemplateVersions_ApprovedAt");

            entity.HasIndex(e => e.ApprovedBy)
                .HasDatabaseName("IX_TemplateVersions_ApprovedBy");

            entity.HasIndex(e => e.ExecutionCount)
                .HasDatabaseName("IX_TemplateVersions_ExecutionCount");

            entity.HasIndex(e => e.ParentVersionId)
                .HasDatabaseName("IX_TemplateVersions_ParentVersionId");

            entity.HasIndex(e => e.ContentSize)
                .HasDatabaseName("IX_TemplateVersions_ContentSize");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.PromptTemplateId, e.ApprovalStatus, e.IsStable })
                .HasDatabaseName("IX_TemplateVersions_Template_Approval_Stable");

            entity.HasIndex(e => new { e.ApprovalStatus, e.ApprovedAt })
                .HasDatabaseName("IX_TemplateVersions_Approval_Date");

            entity.HasIndex(e => new { e.PromptTemplateId, e.IsCurrent })
                .HasDatabaseName("IX_TemplateVersions_Template_Current");

            entity.HasIndex(e => new { e.ChangeType, e.ApprovalStatus })
                .HasDatabaseName("IX_TemplateVersions_ChangeType_Approval");

            entity.HasIndex(e => new { e.ExecutionCount, e.QualityScore })
                .HasDatabaseName("IX_TemplateVersions_Usage_Quality");

            entity.HasIndex(e => new { e.ParentVersionId, e.Version })
                .HasDatabaseName("IX_TemplateVersions_Parent_Version");

            // Covering index for version listing queries
            entity.HasIndex(e => new { e.PromptTemplateId, e.ApprovalStatus })
                .IncludeProperties(e => new { e.Version, e.VersionName, e.ChangeType, e.IsStable, e.IsCurrent, e.ExecutionCount, e.QualityScore, e.CreatedAt })
                .HasDatabaseName("IX_TemplateVersions_Listing_Covering");

            // Covering index for approval workflow queries
            entity.HasIndex(e => e.ApprovalStatus)
                .IncludeProperties(e => new { e.PromptTemplateId, e.Version, e.VersionName, e.ChangeType, e.ApprovedBy, e.ApprovedAt })
                .HasDatabaseName("IX_TemplateVersions_Approval_Covering");
        });
    }

    private void ConfigureSecurityAndPermissions(ModelBuilder modelBuilder)
    {
        // LibraryPermission configuration
        modelBuilder.Entity<LibraryPermission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.PrincipalId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PrincipalType)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Permission)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Optional string properties with proper lengths
            entity.Property(e => e.GrantReason)
                .HasMaxLength(500);

            entity.Property(e => e.Capabilities)
                .HasMaxLength(500);

            // Relationships
            entity.HasOne(e => e.Library)
                .WithMany(l => l.Permissions)
                .HasForeignKey(e => e.LibraryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LibraryPermissions_Library");

            entity.HasOne(e => e.DelegatedFrom)
                .WithMany()
                .HasForeignKey(e => e.DelegatedFromId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_LibraryPermissions_DelegatedFrom");

            // Indexes for performance
            entity.HasIndex(e => new { e.LibraryId, e.PrincipalId })
                .IsUnique()
                .HasDatabaseName("IX_LibraryPermissions_Evaluation_Covering");

            entity.HasIndex(e => e.PrincipalType)
                .HasDatabaseName("IX_LibraryPermissions_PrincipalType");

            entity.HasIndex(e => e.Permission)
                .HasDatabaseName("IX_LibraryPermissions_Permission");

            entity.HasIndex(e => e.ExpiresAt)
                .HasDatabaseName("IX_LibraryPermissions_ExpiresAt");

            entity.HasIndex(e => e.DelegatedFromId)
                .HasDatabaseName("IX_LibraryPermissions_DelegatedFromId");

            entity.HasIndex(e => e.CanDelegate)
                .HasDatabaseName("IX_LibraryPermissions_CanDelegate");

            entity.HasIndex(e => e.MaxDelegationDepth)
                .HasDatabaseName("IX_LibraryPermissions_MaxDelegationDepth");

            entity.HasIndex(e => e.DelegationDepth)
                .HasDatabaseName("IX_LibraryPermissions_DelegationDepth");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.LibraryId, e.Permission })
                .HasDatabaseName("IX_LibraryPermissions_Library_Permission");

            entity.HasIndex(e => new { e.PrincipalType, e.Permission })
                .HasDatabaseName("IX_LibraryPermissions_Principal_Permission");

            entity.HasIndex(e => new { e.DelegatedFromId, e.DelegationDepth })
                .HasDatabaseName("IX_LibraryPermissions_Delegation_Depth");

            entity.HasIndex(e => new { e.Permission, e.CanDelegate })
                .HasDatabaseName("IX_LibraryPermissions_Permission_Delegate");

            // Covering index for permission evaluation queries
            entity.HasIndex(e => new { e.LibraryId, e.PrincipalId })
                .IncludeProperties(e => new { e.Permission, e.ExpiresAt, e.Capabilities, e.CanDelegate })
                .HasDatabaseName("IX_LibraryPermissions_Evaluation_Covering");

            // Covering index for delegation chain queries
            entity.HasIndex(e => e.DelegatedFromId)
                .IncludeProperties(e => new { e.PrincipalId, e.Permission, e.DelegationDepth, e.CanDelegate, e.MaxDelegationDepth })
                .HasDatabaseName("IX_LibraryPermissions_Delegation_Covering");
        });

        // TemplatePermission configuration
        modelBuilder.Entity<TemplatePermission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.PrincipalId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PrincipalType)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Permission)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Optional string properties with proper lengths
            entity.Property(e => e.Capabilities)
                .HasMaxLength(500);

            entity.Property(e => e.Conditions)
                .HasMaxLength(1000);

            entity.Property(e => e.InheritedFromType)
                .HasMaxLength(50);

            // Relationships
            entity.HasOne(e => e.Template)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(e => e.TemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TemplatePermissions_PromptTemplate");

            // Indexes for performance
            entity.HasIndex(e => new { e.TemplateId, e.PrincipalId })
                .IsUnique()
                .HasDatabaseName("IX_TemplatePermissions_Evaluation_Covering");

            entity.HasIndex(e => e.PrincipalType)
                .HasDatabaseName("IX_TemplatePermissions_PrincipalType");

            entity.HasIndex(e => e.Permission)
                .HasDatabaseName("IX_TemplatePermissions_Permission");

            entity.HasIndex(e => e.ExpiresAt)
                .HasDatabaseName("IX_TemplatePermissions_ExpiresAt");

            entity.HasIndex(e => e.IsInherited)
                .HasDatabaseName("IX_TemplatePermissions_IsInherited");

            entity.HasIndex(e => e.InheritedFromId)
                .HasDatabaseName("IX_TemplatePermissions_InheritedFromId");

            entity.HasIndex(e => e.InheritedFromType)
                .HasDatabaseName("IX_TemplatePermissions_InheritedFromType");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.TemplateId, e.Permission })
                .HasDatabaseName("IX_TemplatePermissions_Template_Permission");

            entity.HasIndex(e => new { e.PrincipalType, e.Permission })
                .HasDatabaseName("IX_TemplatePermissions_Principal_Permission");

            entity.HasIndex(e => new { e.IsInherited, e.InheritedFromId })
                .HasDatabaseName("IX_TemplatePermissions_Inherited_From");

            entity.HasIndex(e => new { e.Permission, e.ExpiresAt })
                .HasDatabaseName("IX_TemplatePermissions_Permission_Expires");

            entity.HasIndex(e => new { e.InheritedFromType, e.InheritedFromId })
                .HasDatabaseName("IX_TemplatePermissions_InheritanceSource");

            // Covering index for permission evaluation queries
            entity.HasIndex(e => new { e.TemplateId, e.PrincipalId })
                .IncludeProperties(e => new { e.Permission, e.ExpiresAt, e.Capabilities, e.IsInherited })
                .HasDatabaseName("IX_TemplatePermissions_Evaluation_Covering");

            // Covering index for inheritance chain queries
            entity.HasIndex(e => e.InheritedFromId)
                .IncludeProperties(e => new { e.TemplateId, e.PrincipalId, e.Permission, e.InheritedFromType })
                .HasDatabaseName("IX_TemplatePermissions_Inheritance_Covering");

            // Covering index for principal-based queries
            entity.HasIndex(e => new { e.PrincipalId, e.PrincipalType })
                .IncludeProperties(e => new { e.TemplateId, e.Permission, e.ExpiresAt, e.IsInherited })
                .HasDatabaseName("IX_TemplatePermissions_Principal_Covering");
        });

        // WorkflowLibraryPermission configuration
        modelBuilder.Entity<WorkflowLibraryPermission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.PrincipalId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PrincipalType)
                .HasConversion<string>();

            entity.Property(e => e.Permission)
                .HasConversion<string>();

            // Optional string properties with proper lengths
            entity.Property(e => e.Capabilities)
                .HasMaxLength(500);

            entity.Property(e => e.AllowedContexts)
                .HasMaxLength(200);

            entity.Property(e => e.GrantReason)
                .HasMaxLength(500);

            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(100);

            // Decimal properties
            entity.Property(e => e.CostLimit)
                .HasColumnType("decimal(18,2)");

            // Relationships
            entity.HasOne(e => e.WorkflowLibrary)
                .WithMany(wl => wl.Permissions)
                .HasForeignKey(e => e.WorkflowLibraryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_WorkflowLibraryPermissions_WorkflowLibraries_WorkflowLibraryId");

            // Indexes for performance
            entity.HasIndex(e => e.WorkflowLibraryId)
                .HasDatabaseName("IX_WorkflowLibraryPermissions_WorkflowLibraryId");
        });
    }

    private void ConfigureAnalyticsAndTesting(ModelBuilder modelBuilder)
    {
        // ABTest configuration
        modelBuilder.Entity<ABTest>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Hypothesis)
                .HasMaxLength(1000);

            entity.Property(e => e.PrimaryMetric)
                .HasMaxLength(100);

            entity.Property(e => e.SecondaryMetrics)
                .HasMaxLength(500);

            entity.Property(e => e.TestOwner)
                .HasMaxLength(100);

            entity.Property(e => e.Tags)
                .HasMaxLength(500);

            entity.Property(e => e.Conclusions)
                .HasMaxLength(2000);

            // JSON/Text field configurations
            entity.Property(e => e.TestConfiguration);
            entity.Property(e => e.ResultsSummary);
            entity.Property(e => e.StatisticalAnalysis);

            // Enum conversions
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.EntityType)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.ConfidenceLevel)
                .HasColumnType("decimal(3,2)");

            entity.Property(e => e.SignificanceThreshold)
                .HasColumnType("decimal(3,2)");

            entity.Property(e => e.TrafficAllocation)
                .HasColumnType("decimal(5,2)");

            // Relationships
            entity.HasOne(e => e.WinnerVariant)
                .WithMany()
                .HasForeignKey(e => e.WinnerVariantId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ABTests_WinnerVariant");

            // Indexes for performance
            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_ABTests_Status");

            entity.HasIndex(e => e.EntityType)
                .HasDatabaseName("IX_ABTests_EntityType");

            entity.HasIndex(e => e.StartDate)
                .HasDatabaseName("IX_ABTests_StartDate");

            entity.HasIndex(e => e.EndDate)
                .HasDatabaseName("IX_ABTests_EndDate");

            entity.HasIndex(e => e.TestOwner)
                .HasDatabaseName("IX_ABTests_TestOwner");

            entity.HasIndex(e => e.TrafficAllocation)
                .HasDatabaseName("IX_ABTests_TrafficAllocation");

            entity.HasIndex(e => e.ConfidenceLevel)
                .HasDatabaseName("IX_ABTests_ConfidenceLevel");

            entity.HasIndex(e => e.WinnerVariantId)
                .HasDatabaseName("IX_ABTests_WinnerVariantId");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.Status, e.EntityType })
                .HasDatabaseName("IX_ABTests_Listing_Covering");

            entity.HasIndex(e => new { e.TestOwner, e.Status })
                .HasDatabaseName("IX_ABTests_Owner_Status");

            entity.HasIndex(e => new { e.StartDate, e.EndDate, e.Status })
                .HasDatabaseName("IX_ABTests_DateRange_Status");

            entity.HasIndex(e => new { e.EntityType, e.Status, e.TrafficAllocation })
                .HasDatabaseName("IX_ABTests_Entity_Status_Traffic");

            entity.HasIndex(e => new { e.CurrentSampleSize, e.TargetSampleSize })
                .HasDatabaseName("IX_ABTests_SampleProgress");

            // Covering index for test listing queries
            entity.HasIndex(e => new { e.Status, e.EntityType })
                .IncludeProperties(e => new { e.Name, e.Description, e.TestOwner, e.StartDate, e.EndDate, e.CurrentSampleSize, e.TargetSampleSize })
                .HasDatabaseName("IX_ABTests_Listing_Covering");

            // Covering index for analytics queries
            entity.HasIndex(e => new { e.EntityType, e.Status })
                .IncludeProperties(e => new { e.ConfidenceLevel, e.SignificanceThreshold, e.TrafficAllocation, e.WinnerVariantId })
                .HasDatabaseName("IX_ABTests_Analytics_Covering");
        });

        // ABTestVariant configuration
        modelBuilder.Entity<ABTestVariant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Optional string properties with proper lengths
            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.EntityVersion)
                .HasMaxLength(20);

            entity.Property(e => e.Configuration);

            // Enum conversion
            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.TrafficWeight)
                .HasColumnType("decimal(5,2)");

            entity.Property(e => e.ConversionRate)
                .HasColumnType("decimal(5,4)");

            entity.Property(e => e.AverageScore)
                .HasColumnType("decimal(5,2)");

            // Relationships
            entity.HasOne(e => e.ABTest)
                .WithMany(t => t.Variants)
                .HasForeignKey(e => e.ABTestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ABTestVariants_ABTest");

            // Indexes for performance
            entity.HasIndex(e => e.ABTestId)
                .HasDatabaseName("IX_ABTestVariants_Analysis_Covering");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_ABTestVariants_Status");

            entity.HasIndex(e => e.IsControl)
                .HasDatabaseName("IX_ABTestVariants_IsControl");

            entity.HasIndex(e => e.ConversionRate)
                .HasDatabaseName("IX_ABTestVariants_ConversionRate");

            entity.HasIndex(e => e.ExecutionCount)
                .HasDatabaseName("IX_ABTestVariants_ExecutionCount");

            entity.HasIndex(e => e.EntityId)
                .HasDatabaseName("IX_ABTestVariants_EntityId");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.ABTestId, e.Status })
                .HasDatabaseName("IX_ABTestVariants_Test_Status");

            entity.HasIndex(e => new { e.ABTestId, e.IsControl })
                .HasDatabaseName("IX_ABTestVariants_Test_Control");

            entity.HasIndex(e => new { e.ExecutionCount, e.ConversionRate })
                .HasDatabaseName("IX_ABTestVariants_Execution_Conversion");

            entity.HasIndex(e => new { e.EntityId, e.EntityVersion })
                .HasDatabaseName("IX_ABTestVariants_Entity_Version");

            // Covering index for variant analysis queries
            entity.HasIndex(e => e.ABTestId)
                .IncludeProperties(e => new { e.Name, e.IsControl, e.TrafficWeight, e.ExecutionCount, e.SuccessCount, e.ConversionRate, e.AverageScore })
                .HasDatabaseName("IX_ABTestVariants_Analysis_Covering");
        });

        // ABTestResult configuration
        modelBuilder.Entity<ABTestResult>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // String properties with proper lengths
            entity.Property(e => e.SessionId)
                .HasMaxLength(100);

            entity.Property(e => e.UserId)
                .HasMaxLength(100);

            entity.Property(e => e.SecondaryMetricValues);
            entity.Property(e => e.ExecutionContext);
            entity.Property(e => e.ErrorMessage);

            // Decimal properties with proper precision
            entity.Property(e => e.PrimaryMetricValue)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.QualityScore)
                .HasColumnType("decimal(3,2)");

            // Relationships
            entity.HasOne(e => e.ABTest)
                .WithMany(t => t.Results)
                .HasForeignKey(e => e.ABTestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ABTestResults_ABTest");

            entity.HasOne(e => e.Variant)
                .WithMany(v => v.Results)
                .HasForeignKey(e => e.VariantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ABTestResults_ABTestVariant");

            // Indexes for performance
            entity.HasIndex(e => e.ABTestId)
                .HasDatabaseName("IX_ABTestResults_ABTestId");

            entity.HasIndex(e => e.VariantId)
                .HasDatabaseName("IX_ABTestResults_VariantId");

            entity.HasIndex(e => e.ExecutionTime)
                .HasDatabaseName("IX_ABTestResults_ExecutionTime");

            entity.HasIndex(e => e.Success)
                .HasDatabaseName("IX_ABTestResults_Success");

            entity.HasIndex(e => e.SessionId)
                .HasDatabaseName("IX_ABTestResults_SessionId");

            entity.HasIndex(e => e.UserId)
                .HasDatabaseName("IX_ABTestResults_UserId");

            entity.HasIndex(e => e.QualityScore)
                .HasDatabaseName("IX_ABTestResults_QualityScore");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.ABTestId, e.VariantId, e.ExecutionTime })
                .HasDatabaseName("IX_ABTestResults_Test_Variant_Time");

            entity.HasIndex(e => new { e.VariantId, e.Success, e.ExecutionTime })
                .HasDatabaseName("IX_ABTestResults_Variant_Success_Time");

            entity.HasIndex(e => new { e.SessionId, e.ExecutionTime })
                .HasDatabaseName("IX_ABTestResults_Session_Time");

            entity.HasIndex(e => new { e.UserId, e.ExecutionTime })
                .HasDatabaseName("IX_ABTestResults_User_Time");

            // Covering index for analytics queries
            entity.HasIndex(e => new { e.VariantId, e.ExecutionTime })
                .IncludeProperties(e => new { e.Success, e.PrimaryMetricValue, e.DurationMs, e.Cost, e.QualityScore })
                .HasDatabaseName("IX_ABTestResults_Analytics_Covering");

            // Covering index for real-time aggregation
            entity.HasIndex(e => new { e.ABTestId, e.Success })
                .IncludeProperties(e => new { e.VariantId, e.ExecutionTime, e.PrimaryMetricValue })
                .HasDatabaseName("IX_ABTestResults_Aggregation_Covering");
        });

        // QualityMetric configuration
        modelBuilder.Entity<QualityMetric>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.EntityType)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.MetricName)
                .IsRequired()
                .HasMaxLength(100);

            // Optional string properties with proper lengths
            entity.Property(e => e.Unit)
                .HasMaxLength(20);

            entity.Property(e => e.Source)
                .HasMaxLength(50);

            entity.Property(e => e.Context);

            // Enum conversion
            entity.Property(e => e.MetricType)
                .HasConversion<string>()
                .HasMaxLength(30);

            // Decimal properties with proper precision
            entity.Property(e => e.Score)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.Confidence)
                .HasColumnType("decimal(3,2)");

            // DateTimeOffset for precise time tracking
            entity.Property(e => e.MeasuredAt)
                .IsRequired();

            // Indexes for performance
            entity.HasIndex(e => new { e.EntityType, e.EntityId })
                .HasDatabaseName("IX_QualityMetrics_Entity_Covering");

            entity.HasIndex(e => e.MetricType)
                .HasDatabaseName("IX_QualityMetrics_Type");

            entity.HasIndex(e => e.MetricName)
                .HasDatabaseName("IX_QualityMetrics_Name");

            entity.HasIndex(e => e.MeasuredAt)
                .HasDatabaseName("IX_QualityMetrics_MeasuredAt");

            entity.HasIndex(e => e.Score)
                .HasDatabaseName("IX_QualityMetrics_Score");

            entity.HasIndex(e => e.Source)
                .HasDatabaseName("IX_QualityMetrics_Source");

            entity.HasIndex(e => e.Confidence)
                .HasDatabaseName("IX_QualityMetrics_Confidence");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.EntityType, e.EntityId, e.MetricType })
                .HasDatabaseName("IX_QualityMetrics_Analytics_Covering");

            entity.HasIndex(e => new { e.MetricType, e.MetricName, e.MeasuredAt })
                .HasDatabaseName("IX_QualityMetrics_Type_Name_Time");

            entity.HasIndex(e => new { e.EntityType, e.EntityId, e.MeasuredAt })
                .HasDatabaseName("IX_QualityMetrics_Entity_Time");

            entity.HasIndex(e => new { e.Score, e.MeasuredAt })
                .HasDatabaseName("IX_QualityMetrics_Score_Time");

            entity.HasIndex(e => new { e.Source, e.MetricType, e.MeasuredAt })
                .HasDatabaseName("IX_QualityMetrics_Source_Type_Time");

            entity.HasIndex(e => new { e.Confidence, e.Score })
                .HasDatabaseName("IX_QualityMetrics_Confidence_Score");

            // Covering index for analytics queries
            entity.HasIndex(e => new { e.EntityType, e.EntityId, e.MetricType })
                .IncludeProperties(e => new { e.MetricName, e.Score, e.Unit, e.MeasuredAt, e.Confidence, e.Source })
                .HasDatabaseName("IX_QualityMetrics_Analytics_Covering");

            // Covering index for time-series analysis
            entity.HasIndex(e => new { e.MetricType, e.MeasuredAt })
                .IncludeProperties(e => new { e.EntityType, e.EntityId, e.Score, e.Confidence })
                .HasDatabaseName("IX_QualityMetrics_TimeSeries_Covering");

            // Covering index for entity-specific metrics
            entity.HasIndex(e => new { e.EntityType, e.EntityId })
                .IncludeProperties(e => new { e.MetricType, e.MetricName, e.Score, e.MeasuredAt, e.Unit })
                .HasDatabaseName("IX_QualityMetrics_Entity_Covering");

            // Covering index for quality trend analysis
            entity.HasIndex(e => new { e.MetricName, e.MeasuredAt })
                .IncludeProperties(e => new { e.Score, e.Confidence, e.EntityType, e.EntityId })
                .HasDatabaseName("IX_QualityMetrics_Trend_Covering");
        });
    }

    private void ConfigureManagementAndOptimization(ModelBuilder modelBuilder)
    {
        // ModelProviderConfig configuration
        modelBuilder.Entity<ModelProviderConfig>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            // Required string properties with proper lengths
            entity.Property(e => e.ProviderName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.ModelName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.ApiEndpoint)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.ApiKey)
                .IsRequired()
                .HasMaxLength(500);

            // Optional string properties with proper lengths
            entity.Property(e => e.Configuration)
                .HasMaxLength(2000);

            entity.Property(e => e.Notes)
                .HasMaxLength(500);

            // Enum conversions
            entity.Property(e => e.ModelProviderType)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.HealthStatus)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Decimal properties with proper precision
            entity.Property(e => e.MonthlyBudgetLimit)
                .HasColumnType("decimal(10,2)");

            // TimeSpan conversion for timeout duration
            entity.Property(e => e.TimeoutDuration)
                .HasConversion(
                    v => v.TotalSeconds,
                    v => TimeSpan.FromSeconds(v));

            // Configure table with check constraints
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_ModelProviderConfig_Priority", "[Priority] >= 1 AND [Priority] <= 100");
                t.HasCheckConstraint("CK_ModelProviderConfig_MaxRetries", "[MaxRetries] >= 0 AND [MaxRetries] <= 10");
                t.HasCheckConstraint("CK_ModelProviderConfig_RateLimit", "[RateLimitPerMinute] IS NULL OR [RateLimitPerMinute] > 0");
                t.HasCheckConstraint("CK_ModelProviderConfig_TokenLimit", "[TokenLimitPerMinute] IS NULL OR [TokenLimitPerMinute] > 0");
                t.HasCheckConstraint("CK_ModelProviderConfig_Budget", "[MonthlyBudgetLimit] IS NULL OR [MonthlyBudgetLimit] > 0");
            });

            // Unique constraint for provider-model combination
            entity.HasIndex(e => new { e.ProviderName, e.ModelName })
                .IsUnique()
                .HasDatabaseName("IX_ModelProviderConfigs_Provider_Model");

            // Performance indexes
            entity.HasIndex(e => e.ModelProviderType)
                .HasDatabaseName("IX_ModelProviderConfigs_Type");

            entity.HasIndex(e => e.IsEnabled)
                .HasDatabaseName("IX_ModelProviderConfigs_IsEnabled");

            entity.HasIndex(e => e.Priority)
                .HasDatabaseName("IX_ModelProviderConfigs_Priority");

            entity.HasIndex(e => e.HealthStatus)
                .HasDatabaseName("IX_ModelProviderConfigs_HealthStatus");

            entity.HasIndex(e => e.LastHealthCheck)
                .HasDatabaseName("IX_ModelProviderConfigs_LastHealthCheck");

            entity.HasIndex(e => e.MonthlyBudgetLimit)
                .HasDatabaseName("IX_ModelProviderConfigs_BudgetLimit");

            entity.HasIndex(e => e.RateLimitPerMinute)
                .HasDatabaseName("IX_ModelProviderConfigs_RateLimit");

            entity.HasIndex(e => e.TokenLimitPerMinute)
                .HasDatabaseName("IX_ModelProviderConfigs_TokenLimit");

            // Composite indexes for complex queries
            entity.HasIndex(e => new { e.IsEnabled, e.Priority })
                .HasDatabaseName("IX_ModelProviderConfigs_Enabled_Priority");

            entity.HasIndex(e => new { e.ModelProviderType, e.IsEnabled })
                .HasDatabaseName("IX_ModelProviderConfigs_Management_Covering");

            entity.HasIndex(e => new { e.HealthStatus, e.IsEnabled })
                .HasDatabaseName("IX_ModelProviderConfigs_Health_Enabled");

            entity.HasIndex(e => new { e.Priority, e.HealthStatus, e.IsEnabled })
                .HasDatabaseName("IX_ModelProviderConfigs_Priority_Health_Enabled");

            entity.HasIndex(e => new { e.ModelProviderType, e.HealthStatus, e.LastHealthCheck })
                .HasDatabaseName("IX_ModelProviderConfigs_Type_Health_LastCheck");

            entity.HasIndex(e => new { e.MonthlyBudgetLimit, e.IsEnabled })
                .HasDatabaseName("IX_ModelProviderConfigs_Cost_Covering");

            entity.HasIndex(e => new { e.RateLimitPerMinute, e.TokenLimitPerMinute })
                .HasDatabaseName("IX_ModelProviderConfigs_RateLimit_TokenLimit");

            // Covering index for provider selection queries
            entity.HasIndex(e => new { e.IsEnabled, e.Priority, e.HealthStatus })
                .IncludeProperties(e => new { e.ProviderName, e.ModelName, e.ModelProviderType, e.ApiEndpoint, e.Configuration, e.TimeoutDuration, e.MaxRetries })
                .HasDatabaseName("IX_ModelProviderConfigs_Selection_Covering");

            // Covering index for health monitoring queries
            entity.HasIndex(e => new { e.HealthStatus, e.LastHealthCheck })
                .IncludeProperties(e => new { e.ProviderName, e.ModelName, e.IsEnabled, e.ApiEndpoint })
                .HasDatabaseName("IX_ModelProviderConfigs_Health_Covering");

            // Covering index for cost management queries
            entity.HasIndex(e => new { e.MonthlyBudgetLimit, e.IsEnabled })
                .IncludeProperties(e => new { e.ProviderName, e.ModelName, e.RateLimitPerMinute, e.TokenLimitPerMinute })
                .HasDatabaseName("IX_ModelProviderConfigs_Cost_Covering");

            // Covering index for configuration management queries
            entity.HasIndex(e => new { e.ModelProviderType, e.IsEnabled })
                .IncludeProperties(e => new { e.ProviderName, e.ModelName, e.Priority, e.HealthStatus, e.LastHealthCheck, e.Configuration })
                .HasDatabaseName("IX_ModelProviderConfigs_Management_Covering");

            // Covering index for operational queries
            entity.HasIndex(e => new { e.Priority, e.IsEnabled })
                .IncludeProperties(e => new { e.ProviderName, e.ModelName, e.HealthStatus, e.RateLimitPerMinute, e.TokenLimitPerMinute, e.TimeoutDuration, e.MaxRetries })
                .HasDatabaseName("IX_ModelProviderConfigs_Operations_Covering");
        });

        // Additional management entities would be configured here when implemented
        // TODO: Add WorkflowTemplateUsage configuration when entity is implemented
        // TODO: Add other management entities as they are developed
    }

    private void ConfigureEnumConversions(ModelBuilder modelBuilder)
    {
        // Configure enum-to-string conversions for better database portability

        // Flow/Workflow enums
        ConfigureEnumConversion<EdgeType>(modelBuilder);
        ConfigureEnumConversion<EdgeValidationStatus>(modelBuilder);
        ConfigureEnumConversion<FlowExecutionStatus>(modelBuilder);
        ConfigureEnumConversion<FlowNodeType>(modelBuilder);
        ConfigureEnumConversion<FlowStorageMode>(modelBuilder);
        ConfigureEnumConversion<NodeExecutionStatus>(modelBuilder);
        ConfigureEnumConversion<NodeValidationStatus>(modelBuilder);
        ConfigureEnumConversion<WorkflowStatus>(modelBuilder);
        ConfigureEnumConversion<WorkflowCategoryType>(modelBuilder);

        // Template/Prompt enums
        ConfigureEnumConversion<TemplateCategory>(modelBuilder);
        ConfigureEnumConversion<TemplateStatus>(modelBuilder);
        ConfigureEnumConversion<TemplateSize>(modelBuilder);
        ConfigureEnumConversion<ExecutionStatus>(modelBuilder);
        ConfigureEnumConversion<CollectionStatus>(modelBuilder);
        ConfigureEnumConversion<VariableType>(modelBuilder);

        // Library/Lab enums
        ConfigureEnumConversion<LibraryCategoryType>(modelBuilder);
        ConfigureEnumConversion<LibraryStatus>(modelBuilder);
        ConfigureEnumConversion<LibraryVisibility>(modelBuilder);
        ConfigureEnumConversion<LabStatus>(modelBuilder);
        ConfigureEnumConversion<LabVisibility>(modelBuilder);

        // Variable/Version enums
        ConfigureEnumConversion<VersionApprovalStatus>(modelBuilder);
        ConfigureEnumConversion<VersionChangeType>(modelBuilder);

        // Security/Permission enums
        ConfigureEnumConversion<PermissionLevel>(modelBuilder);
        ConfigureEnumConversion<PrincipalType>(modelBuilder);

        // Testing/Analytics enums
        ConfigureEnumConversion<ABTestStatus>(modelBuilder);
        ConfigureEnumConversion<TestEntityType>(modelBuilder);
        ConfigureEnumConversion<VariantStatus>(modelBuilder);
        ConfigureEnumConversion<QualityMetricType>(modelBuilder);

        // Provider/Configuration enums  
        ConfigureEnumConversion<ModelProviderType>(modelBuilder);
        ConfigureEnumConversion<ModelProviderHealthStatus>(modelBuilder);

        ConfigureEnumConversion<SuggestionType>(modelBuilder);
        ConfigureEnumConversion<SuggestionStatus>(modelBuilder);
        ConfigureEnumConversion<SuggestionPriority>(modelBuilder);
        ConfigureEnumConversion<TrendGranularity>(modelBuilder);
        ConfigureEnumConversion<LabMemberRole>(modelBuilder);
        ConfigureEnumConversion<DataClassification>(modelBuilder);
    }

    private void ConfigureEnumConversion<TEnum>(ModelBuilder modelBuilder) where TEnum : struct, Enum
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                if (property.ClrType == typeof(TEnum) || property.ClrType == typeof(TEnum?))
                {
                    property.SetProviderClrType(typeof(string));
                }
            }
        }
    }

    #endregion

    #region Audit and Change Tracking

    /// <summary>
    /// Override SaveChanges to automatically handle audit fields
    /// </summary>
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    /// <summary>
    /// Creates a lambda expression that filters out entities marked as soft-deleted.
    /// </summary>
    /// <remarks>This method is typically used to create a filter for soft-deletion scenarios, where entities
    /// are not physically deleted from the database but are instead marked as deleted using an <c>IsDeleted</c>
    /// property.</remarks>
    /// <param name="entityType">The type of the entity for which the filter is being created. The entity type must have a boolean property named
    /// <c>IsDeleted</c>.</param>
    /// <returns>A <see cref="LambdaExpression"/> that evaluates to <see langword="true"/> for entities where the
    /// <c>IsDeleted</c> property is <see langword="false"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the specified <paramref name="entityType"/> does not have a boolean property named <c>IsDeleted</c>.</exception>
    private static LambdaExpression CreateSoftDeleteFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var isDeletedProperty = entityType.GetProperty("IsDeleted");
        if (isDeletedProperty == null || isDeletedProperty.PropertyType != typeof(bool))
            throw new InvalidOperationException($"Entity '{entityType.Name}' does not have a bool IsDeleted property.");
        var propertyAccess = Expression.Property(parameter, isDeletedProperty);
        var notDeleted = Expression.Equal(propertyAccess, Expression.Constant(false));
        return Expression.Lambda(notDeleted, parameter);
    }

    /// <summary>
    /// Creates a tenancy filter expression for the specified entity type and organization ID.
    /// </summary>
    /// <param name="entityType">The type of the entity for which the filter is being created. The entity must have a nullable <see cref="Guid"/>
    /// property named <c>OrganizationId</c>.</param>
    /// <param name="organizationId">The organization ID to filter entities by. Only entities with a matching <c>OrganizationId</c> will satisfy the
    /// filter.</param>
    /// <returns>A <see cref="LambdaExpression"/> representing the filter. The expression checks if the entity's
    /// <c>OrganizationId</c> property matches the specified <paramref name="organizationId"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the specified <paramref name="entityType"/> does not have a nullable <see cref="Guid"/> property named
    /// <c>OrganizationId</c>.</exception>
    private static LambdaExpression CreateTenancyFilter(Type entityType, Guid organizationId)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var orgIdProperty = entityType.GetProperty("OrganizationId");
        if (orgIdProperty == null || orgIdProperty.PropertyType != typeof(Guid?))
            throw new InvalidOperationException($"Entity '{entityType.Name}' does not have a Guid? OrganizationId property.");
        var propertyAccess = Expression.Property(parameter, orgIdProperty);
        var orgMatch = Expression.Equal(propertyAccess, Expression.Constant(organizationId, typeof(Guid?)));
        return Expression.Lambda(orgMatch, parameter);
    }


    /// <summary>
    /// Override SaveChangesAsync to automatically handle audit fields
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var now = DateTime.UtcNow;
        
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.CreatedBy = _currentUserId;
                    entry.Entity.UpdatedBy = _currentUserId;
                    entry.Entity.OrganizationId ??= _currentOrganizationId;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = _currentUserId;
                    // Prevent modification of audit fields
                    entry.Property(e => e.CreatedAt).IsModified = false;
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Property(e => e.OrganizationId).IsModified = false;
                    break;
            }
        }
    }

    #endregion

    #region Transaction Management

    /// <summary>
    /// Execute multiple operations within a single transaction
    /// </summary>
    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
    {
        using var transaction = await Database.BeginTransactionAsync();
        try
        {
            var result = await operation();
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    #endregion

    #region Performance and Monitoring

    /// <summary>
    /// Configure command timeout for long-running operations
    /// </summary>
    public void SetCommandTimeout(int timeoutSeconds)
    {
        Database.SetCommandTimeout(timeoutSeconds);
    }

    /// <summary>
    /// Get database connection statistics
    /// </summary>
    public async Task<Dictionary<string, object>> GetConnectionInfoAsync()
    {
        var connectionState = Database.GetDbConnection().State;
        var serverVersion = Database.ProviderName;
        
        return new Dictionary<string, object>
        {
            ["ConnectionState"] = connectionState.ToString(),
            ["ProviderName"] = serverVersion ?? "Unknown",
            ["DatabaseName"] = Database.GetDbConnection().Database,
            ["CurrentUser"] = _currentUserId ?? "Unknown",
            ["OrganizationId"] = _currentOrganizationId?.ToString() ?? "None"
        };
    }

    #endregion
}
