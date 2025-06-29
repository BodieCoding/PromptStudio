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
public class EnhancedPromptStudioDbContext : DbContext, IPromptStudioDbContext
{
    private readonly string? _currentUserId;
    private readonly Guid? _currentOrganizationId;
    
    /// <summary>
    /// Initializes a new instance of the EnhancedPromptStudioDbContext
    /// </summary>
    /// <param name="options">The options to be used by the context</param>
    /// <param name="currentUserId">Current user identifier for audit trails</param>
    /// <param name="currentOrganizationId">Current organization for multi-tenant data isolation</param>
    public EnhancedPromptStudioDbContext(
        DbContextOptions<EnhancedPromptStudioDbContext> options, 
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

        // Configure indexes for performance
        ConfigurePerformanceIndexes(modelBuilder);

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
        modelBuilder.Entity<PromptLab>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever(); // Guid generated in entity

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.LabId)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Owner)
                .HasMaxLength(100);

            entity.Property(e => e.Tags)
                .HasMaxLength(1000);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Visibility)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Concurrency control
            entity.Property(e => e.RowVersion)
                .IsRowVersion();

            // Indexes
            entity.HasIndex(e => e.LabId)
                .IsUnique()
                .HasDatabaseName("IX_PromptLabs_LabId");

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_PromptLabs_Name");

            entity.HasIndex(e => e.OrganizationId)
                .HasDatabaseName("IX_PromptLabs_OrganizationId");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_PromptLabs_Status");

            entity.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_PromptLabs_CreatedAt");
        });
    }

    private void ConfigurePromptLibrary(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PromptLibrary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Color)
                .HasMaxLength(7);

            entity.Property(e => e.Icon)
                .HasMaxLength(50);

            entity.Property(e => e.Tags)
                .HasMaxLength(1000);

            entity.Property(e => e.Category)
                .HasConversion<string>()
                .HasMaxLength(50);

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

            // Indexes
            entity.HasIndex(e => new { e.PromptLabId, e.Name })
                .IsUnique()
                .HasDatabaseName("IX_PromptLibraries_LabId_Name");

            entity.HasIndex(e => e.Category)
                .HasDatabaseName("IX_PromptLibraries_Category");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_PromptLibraries_Status");

            entity.HasIndex(e => e.LastActivityAt)
                .HasDatabaseName("IX_PromptLibraries_LastActivity");
        });
    }

    private void ConfigurePromptTemplate(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PromptTemplate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(20);

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
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PromptTemplates_BaseTemplate");

            entity.HasOne(e => e.Content)
                .WithOne(c => c.PromptTemplate)
                .HasForeignKey<PromptContent>(c => c.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptContents_PromptTemplate");

            // Indexes
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
        });
    }

    private void ConfigurePromptVariable(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PromptVariable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasMaxLength(200);

            entity.Property(e => e.Type)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Relationships
            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.Variables)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptVariables_PromptTemplate");

            // Indexes
            entity.HasIndex(e => new { e.PromptTemplateId, e.Name })
                .IsUnique()
                .HasDatabaseName("IX_PromptVariables_TemplateId_Name");
        });
    }

    private void ConfigurePromptExecution(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PromptExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.ResolvedPrompt)
                .IsRequired();

            entity.Property(e => e.AiProvider)
                .HasMaxLength(50);

            entity.Property(e => e.Model)
                .HasMaxLength(50);

            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10,4)");

            // Relationships
            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.Executions)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PromptExecutions_PromptTemplate");

            // Indexes
            entity.HasIndex(e => e.ExecutedAt)
                .HasDatabaseName("IX_PromptExecutions_ExecutedAt");

            entity.HasIndex(e => e.PromptTemplateId)
                .HasDatabaseName("IX_PromptExecutions_TemplateId");

            entity.HasIndex(e => e.AiProvider)
                .HasDatabaseName("IX_PromptExecutions_Provider");

            entity.HasIndex(e => e.Model)
                .HasDatabaseName("IX_PromptExecutions_Model");
        });
    }

    private void ConfigureVariableCollection(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VariableCollection>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.VariableSets)
                .IsRequired();

            // Relationships
            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.VariableCollections)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VariableCollections_PromptTemplate");

            // Indexes
            entity.HasIndex(e => e.PromptTemplateId)
                .HasDatabaseName("IX_VariableCollections_TemplateId");

            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_VariableCollections_Name");
        });
    }

    private void ConfigureWorkflowEngine(ModelBuilder modelBuilder)
    {
        // WorkflowLibrary configuration
        modelBuilder.Entity<WorkflowLibrary>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.HasOne(e => e.PromptLab)
                .WithMany(pl => pl.WorkflowLibraries)
                .HasForeignKey(e => e.PromptLabId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.WorkflowCategory)
                .WithMany()
                .HasForeignKey(e => e.WorkflowCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // WorkflowCategory configuration
        modelBuilder.Entity<WorkflowCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.IconName)
                .HasMaxLength(50);

            entity.Property(e => e.Color)
                .HasMaxLength(7);

            entity.Property(e => e.CategoryType)
                .HasConversion<string>()
                .HasMaxLength(50);

            // Self-referencing relationship for hierarchy
            entity.HasOne(e => e.ParentCategory)
                .WithMany(e => e.ChildCategories)
                .HasForeignKey(e => e.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_WorkflowCategories_Name");

            entity.HasIndex(e => e.ParentCategoryId)
                .HasDatabaseName("IX_WorkflowCategories_ParentCategory");

            entity.HasIndex(e => e.IsSystemDefined)
                .HasDatabaseName("IX_WorkflowCategories_IsSystemDefined");

            entity.HasIndex(e => e.CategoryType)
                .HasDatabaseName("IX_WorkflowCategories_CategoryType");
        });

        // PromptFlow configuration
        modelBuilder.Entity<PromptFlow>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Tags)
                .HasMaxLength(1000);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.ExpectedCost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.AverageCost)
                .HasColumnType("decimal(10,4)");

            entity.Property(e => e.QualityScore)
                .HasColumnType("decimal(3,2)");

            entity.Property(e => e.AiConfidenceScore)
                .HasColumnType("decimal(3,2)");

            entity.Property(e => e.FlowHash)
                .HasMaxLength(64);

            // Relationships
            entity.HasOne(e => e.WorkflowLibrary)
                .WithMany(wl => wl.PromptFlows)
                .HasForeignKey(e => e.WorkflowLibraryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.BaseFlow)
                .WithMany()
                .HasForeignKey(e => e.BaseFlowId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.WorkflowCategory)
                .WithMany(wc => wc.PromptFlows)
                .HasForeignKey(e => e.WorkflowCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            entity.HasIndex(e => e.Name)
                .HasDatabaseName("IX_PromptFlows_Name");

            entity.HasIndex(e => e.WorkflowCategoryId)
                .HasDatabaseName("IX_PromptFlows_WorkflowCategory");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_PromptFlows_Status");

            entity.HasIndex(e => e.LastExecutedAt)
                .HasDatabaseName("IX_PromptFlows_LastExecuted");
        });

        // FlowNode configuration
        modelBuilder.Entity<FlowNode>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.NodeType)
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(e => e.Label)
                .HasMaxLength(100);

            entity.HasOne(e => e.Flow)
                .WithMany(f => f.Nodes)
                .HasForeignKey(e => e.FlowId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.FlowId)
                .HasDatabaseName("IX_FlowNodes_FlowId");
        });

        // FlowEdge configuration
        modelBuilder.Entity<FlowEdge>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.SourceHandle)
                .HasMaxLength(50);

            entity.Property(e => e.TargetHandle)
                .HasMaxLength(50);

            entity.Property(e => e.Label)
                .HasMaxLength(100);

            entity.HasOne(e => e.Flow)
                .WithMany(f => f.Edges)
                .HasForeignKey(e => e.FlowId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.SourceNode)
                .WithMany(n => n.OutgoingEdges)
                .HasForeignKey(e => e.SourceNodeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.TargetNode)
                .WithMany(n => n.IncomingEdges)
                .HasForeignKey(e => e.TargetNodeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // FlowExecution configuration
        modelBuilder.Entity<FlowExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.InputVariables)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(2000);

            entity.HasOne(e => e.Flow)
                .WithMany(f => f.Executions)
                .HasForeignKey(e => e.FlowId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.FlowId)
                .HasDatabaseName("IX_FlowExecutions_FlowId");

            entity.HasIndex(e => e.StartedAt)
                .HasDatabaseName("IX_FlowExecutions_StartedAt");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_FlowExecutions_Status");
        });
    }

    private void ConfigureContentAndVersioning(ModelBuilder modelBuilder)
    {
        // PromptContent configuration
        modelBuilder.Entity<PromptContent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Content)
                .IsRequired();

            entity.HasIndex(e => e.PromptTemplateId)
                .IsUnique()
                .HasDatabaseName("IX_PromptContents_TemplateId");
        });

        // TemplateVersion configuration
        modelBuilder.Entity<TemplateVersion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.Content)
                .IsRequired();

            entity.Property(e => e.ChangeNotes)
                .HasMaxLength(1000);

            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.Versions)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.PromptTemplateId, e.Version })
                .IsUnique()
                .HasDatabaseName("IX_TemplateVersions_TemplateId_Version");
        });
    }

    private void ConfigureSecurityAndPermissions(ModelBuilder modelBuilder)
    {
        // LibraryPermission configuration
        modelBuilder.Entity<LibraryPermission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.PrincipalId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PrincipalType)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Permission)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.HasOne(e => e.Library)
                .WithMany(l => l.Permissions)
                .HasForeignKey(e => e.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.LibraryId, e.PrincipalId })
                .IsUnique()
                .HasDatabaseName("IX_LibraryPermissions_LibraryId_PrincipalId");
        });

        // TemplatePermission configuration
        modelBuilder.Entity<TemplatePermission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.PrincipalId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PrincipalType)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.Permission)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.HasOne(e => e.PromptTemplate)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(e => e.PromptTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureAnalyticsAndTesting(ModelBuilder modelBuilder)
    {
        // ABTest configuration
        modelBuilder.Entity<ABTest>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(500);

            entity.Property(e => e.Hypothesis)
                .HasMaxLength(1000);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(e => e.ConfidenceLevel)
                .HasColumnType("decimal(3,2)");

            entity.HasIndex(e => e.Status)
                .HasDatabaseName("IX_ABTests_Status");

            entity.HasIndex(e => e.StartDate)
                .HasDatabaseName("IX_ABTests_StartDate");
        });

        // QualityMetric configuration
        modelBuilder.Entity<QualityMetric>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.MetricType)
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(e => e.Score)
                .HasColumnType("decimal(5,2)");

            entity.HasIndex(e => new { e.EntityType, e.EntityId })
                .HasDatabaseName("IX_QualityMetrics_Entity");

            entity.HasIndex(e => e.MetricType)
                .HasDatabaseName("IX_QualityMetrics_Type");
        });
    }

    private void ConfigureManagementAndOptimization(ModelBuilder modelBuilder)
    {
        // ModelProviderConfig configuration
        modelBuilder.Entity<ModelProviderConfig>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.ProviderName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.ModelName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.ProviderType)
                .HasConversion<string>()
                .HasMaxLength(30);

            entity.HasIndex(e => new { e.ProviderName, e.ModelName })
                .IsUnique()
                .HasDatabaseName("IX_ModelProviderConfigs_Provider_Model");
        });
    }

    private void ConfigurePerformanceIndexes(ModelBuilder modelBuilder)
    {
        // Additional performance indexes for common query patterns
        
        // Multi-column indexes for complex queries
        modelBuilder.Entity<PromptTemplate>()
            .HasIndex(e => new { e.PromptLibraryId, e.Status, e.Category })
            .HasDatabaseName("IX_PromptTemplates_Complex_Query");

        modelBuilder.Entity<PromptExecution>()
            .HasIndex(e => new { e.PromptTemplateId, e.ExecutedAt, e.AiProvider })
            .HasDatabaseName("IX_PromptExecutions_Complex_Query");

        // Covering indexes for frequent read operations
        modelBuilder.Entity<PromptLibrary>()
            .HasIndex(e => new { e.PromptLabId, e.Status })
            .IncludeProperties(e => new { e.Name, e.Description, e.Category })
            .HasDatabaseName("IX_PromptLibraries_Covering");
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
        ConfigureEnumConversion<SuggestionType>(modelBuilder);
        ConfigureEnumConversion<SuggestionStatus>(modelBuilder);
        ConfigureEnumConversion<SuggestionPriority>(modelBuilder);
        
        // Template/Prompt enums
        ConfigureEnumConversion<TemplateCategory>(modelBuilder);
        ConfigureEnumConversion<TemplateStatus>(modelBuilder);
        ConfigureEnumConversion<TemplateSize>(modelBuilder);
        ConfigureEnumConversion<ExecutionStatus>(modelBuilder);
        ConfigureEnumConversion<CollectionStatus>(modelBuilder);
        ConfigureEnumConversion<TrendGranularity>(modelBuilder);
        
        // Library/Lab enums
        ConfigureEnumConversion<LibraryCategory>(modelBuilder);
        ConfigureEnumConversion<LibraryStatus>(modelBuilder);
        ConfigureEnumConversion<LibraryVisibility>(modelBuilder);
        ConfigureEnumConversion<LabStatus>(modelBuilder);
        ConfigureEnumConversion<LabVisibility>(modelBuilder);
        ConfigureEnumConversion<LabMemberRole>(modelBuilder);
        
        // Variable/Version enums
        ConfigureEnumConversion<VariableType>(modelBuilder);
        ConfigureEnumConversion<VersionApprovalStatus>(modelBuilder);
        ConfigureEnumConversion<VersionChangeType>(modelBuilder);
        
        // Security/Permission enums
        ConfigureEnumConversion<PermissionLevel>(modelBuilder);
        ConfigureEnumConversion<PrincipalType>(modelBuilder);
        ConfigureEnumConversion<DataClassification>(modelBuilder);
        
        // Testing/Analytics enums
        ConfigureEnumConversion<ABTestStatus>(modelBuilder);
        ConfigureEnumConversion<TestEntityType>(modelBuilder);
        ConfigureEnumConversion<VariantStatus>(modelBuilder);
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

    #region Helper Methods for Global Filters

    private LambdaExpression CreateSoftDeleteFilter(Type entityType)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, nameof(AuditableEntity.DeletedAt));
        var condition = Expression.Equal(property, Expression.Constant(null, typeof(DateTime?)));
        return Expression.Lambda(condition, parameter);
    }

    private LambdaExpression CreateTenancyFilter(Type entityType, Guid organizationId)
    {
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, nameof(AuditableEntity.OrganizationId));
        var condition = Expression.Equal(property, Expression.Constant(organizationId, typeof(Guid?)));
        return Expression.Lambda(condition, parameter);
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
