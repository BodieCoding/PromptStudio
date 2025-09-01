using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;
using System.Text.Json;

namespace PromptStudio.Data.Seeding;

/// <summary>
/// Provides comprehensive seeding functionality for the enhanced PromptStudio database context.
/// Creates initial data with Guid-based identifiers, enterprise audit trails, and multi-tenant support.
/// </summary>
/// <remarks>
/// <para><strong>Enhanced Features:</strong></para>
/// <para>The seeding includes enterprise-grade audit trails, multi-tenant organization support,
/// content separation for performance optimization, and comprehensive workflow categories.
/// All entities use Guid identifiers for global uniqueness and security.</para>
/// </remarks>
public static class PromptStudioSeeding
{
    #region Deterministic GUIDs for Consistent Seeding

    // Organization and Lab GUIDs
    private static readonly Guid DefaultOrganizationId = new("11111111-1111-1111-1111-111111111111");
    private static readonly Guid DefaultLabId = new("22222222-2222-2222-2222-222222222222");
    
    // Library GUIDs
    private static readonly Guid SampleLibraryId = new("33333333-3333-3333-3333-333333333333");
    private static readonly Guid AIAgentLibraryId = new("44444444-4444-4444-4444-444444444444");
    
    // Template GUIDs
    private static readonly Guid CodeReviewTemplateId = new("55555555-5555-5555-5555-555555555555");
    private static readonly Guid AdvancedCodeAnalysisTemplateId = new("66666666-6666-6666-6666-666666666666");
    
    // Variable GUIDs
    private static readonly Guid LanguageVariableId = new("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
    private static readonly Guid CodeVariableId = new("cccccccc-cccc-cccc-cccc-cccccccccccc");
    
    // Content GUIDs
    private static readonly Guid CodeReviewContentId = new("10101010-1010-1010-1010-101010101010");
    private static readonly Guid AdvancedCodeAnalysisContentId = new("20202020-2020-2020-2020-202020202020");
    
    // Workflow Category GUIDs
    private static readonly Guid GeneralCategoryId = new("dddddddd-dddd-dddd-dddd-dddddddddddd");
    private static readonly Guid DevelopmentCategoryId = new("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
    private static readonly Guid AutomationCategoryId = new("ffffffff-ffff-ffff-ffff-ffffffffffff");
    
    // Common timestamps
    private static readonly DateTime SeedTimestamp = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    #endregion

    /// <summary>
    /// Seeds the database with comprehensive initial data including labs, libraries, templates, and variables.
    /// Creates a complete sample environment with realistic AI workflow examples using enhanced domain model.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance used for seeding</param>
    /// <param name="organizationId">Optional organization ID for multi-tenant seeding. Uses default if null.</param>
    public static void SeedData(ModelBuilder modelBuilder, Guid? organizationId = null)
    {
        var orgId = organizationId ?? DefaultOrganizationId;
        
        SeedWorkflowCategories(modelBuilder, orgId);
        SeedPromptLabs(modelBuilder, orgId);
        SeedPromptLibraries(modelBuilder, orgId);
        SeedPromptTemplates(modelBuilder, orgId);
        SeedPromptContents(modelBuilder, orgId);
        SeedPromptVariables(modelBuilder, orgId);
    }

    /// <summary>
    /// Seeds system-defined workflow categories for organizational structure.
    /// </summary>
    private static void SeedWorkflowCategories(ModelBuilder modelBuilder, Guid organizationId)
    {
        modelBuilder.Entity<WorkflowCategory>().HasData(
            new WorkflowCategory
            {
                Id = GeneralCategoryId,
                Name = "General Workflows",
                Description = "General-purpose workflows and common automation patterns",
                CategoryType = WorkflowCategoryType.General,
                IsSystemDefined = true,
                DisplayOrder = 10,
                IconName = "workflow",
                Color = "#6366f1",
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            },
            new WorkflowCategory
            {
                Id = DevelopmentCategoryId,
                Name = "Development & Engineering",
                Description = "Software development, code analysis, and engineering workflows",
                CategoryType = WorkflowCategoryType.Development,
                IsSystemDefined = true,
                DisplayOrder = 20,
                IconName = "code",
                Color = "#10b981",
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            },
            new WorkflowCategory
            {
                Id = AutomationCategoryId,
                Name = "Process Automation",
                Description = "Business process automation and intelligent workflow orchestration",
                CategoryType = WorkflowCategoryType.Automation,
                IsSystemDefined = true,
                DisplayOrder = 30,
                IconName = "automation",
                Color = "#f59e0b",
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            }
        );
    }

    /// <summary>
    /// Seeds initial prompt labs with default system lab using enhanced domain model.
    /// </summary>
    private static void SeedPromptLabs(ModelBuilder modelBuilder, Guid organizationId)
    {
        modelBuilder.Entity<PromptLab>().HasData(
            new PromptLab
            {
                Id = DefaultLabId,
                Name = "Default Lab",
                Description = "Default prompt lab for getting started with enterprise LLMOps",
                LabId = "default-lab",
                Owner = "system",
                Status = LabStatus.Active,
                Visibility = LabVisibility.Private,
                Tags = JsonSerializer.Serialize(new[] { "sample", "default", "enterprise" }),
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            }
        );
    }

    /// <summary>
    /// Seeds initial prompt libraries with sample and AI workflow collections using enhanced domain model.
    /// </summary>
    private static void SeedPromptLibraries(ModelBuilder modelBuilder, Guid organizationId)
    {
        modelBuilder.Entity<PromptLibrary>().HasData(
            new PromptLibrary
            {
                Id = SampleLibraryId,
                Name = "Sample Library",
                Description = "A sample library to get you started with basic prompt engineering",
                PromptLabId = DefaultLabId,
                Color = "#1976d2",
                Icon = "library_books",
                Tags = JsonSerializer.Serialize(new[] { "sample", "starter", "basic" }),
                SortOrder = 0,
                IsPinned = true,
                Status = LibraryStatus.Active,
                Visibility = LibraryVisibility.Private,
                RequiresApproval = false,
                TemplateCount = 1,
                LastActivityAt = SeedTimestamp,
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            },
            new PromptLibrary
            {
                Id = AIAgentLibraryId,
                Name = "AI Agent Workflows",
                Description = "Advanced prompt templates designed for AI agents to automate complex multi-step workflows, code analysis, and problem-solving tasks",
                PromptLabId = DefaultLabId,
                Color = "#4caf50",
                Icon = "smart_toy",
                Tags = JsonSerializer.Serialize(new[] { "ai", "agents", "automation", "workflows", "enterprise" }),
                SortOrder = 1,
                IsPinned = false,
                Status = LibraryStatus.Active,
                Visibility = LibraryVisibility.Private,
                RequiresApproval = true,
                TemplateCount = 2,
                LastActivityAt = SeedTimestamp,
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            }
        );
    }

    /// <summary>
    /// Seeds comprehensive prompt templates with sophisticated AI workflow examples using enhanced domain model.
    /// </summary>
    private static void SeedPromptTemplates(ModelBuilder modelBuilder, Guid organizationId)
    {
        modelBuilder.Entity<PromptTemplate>().HasData(
            // Basic Code Review Template
            new PromptTemplate
            {
                Id = CodeReviewTemplateId,
                Name = "Code Review",
                Description = "Review code for best practices and improvements",
                Version = "1.0.0",
                PromptLibraryId = SampleLibraryId,
                Category = TemplateCategory.CodeGeneration,
                Status = TemplateStatus.Published,
                Size = TemplateSize.Small,
                Tags = JsonSerializer.Serialize(new[] { "code-review", "best-practices", "quality" }),
                RequiresApproval = false,
                ExecutionCount = 0,
                AverageResponseTimeMs = 0,
                QualityScore = 0.0m,
                AverageCost = 0.0m,
                OutputLanguage = "en",
                License = "MIT",
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            },

            // Advanced Code Analysis Template
            new PromptTemplate
            {
                Id = AdvancedCodeAnalysisTemplateId,
                Name = "Advanced Code Analysis & Refactoring",
                Description = "Comprehensive code analysis and refactoring recommendations with business context",
                Version = "1.0.0",
                PromptLibraryId = AIAgentLibraryId,
                Category = TemplateCategory.CodeGeneration,
                Status = TemplateStatus.Published,
                Size = TemplateSize.ExtraLarge,
                Tags = JsonSerializer.Serialize(new[] { "code-analysis", "refactoring", "architecture", "enterprise" }),
                RequiresApproval = true,
                ExecutionCount = 0,
                AverageResponseTimeMs = 0,
                QualityScore = 0.0m,
                AverageCost = 0.0m,
                OutputLanguage = "en",
                License = "Enterprise",
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            }
        );
    }

    /// <summary>
    /// Seeds the actual prompt content for the templates in separate entities for performance optimization.
    /// </summary>
    private static void SeedPromptContents(ModelBuilder modelBuilder, Guid organizationId)
    {
        // Code Review Content
        var codeReviewContent = "Please review the following {{language}} code and provide feedback:\n\n```{{language}}\n{{code}}\n```\n\nFocus on:\n- Code quality\n- Performance\n- Security\n- Best practices";

        // Advanced Code Analysis Content
        var advancedCodeAnalysisContent = @"# Advanced Code Analysis & Refactoring

## Context
You are analyzing {{code_type}} code from {{project_name}} for {{analysis_purpose}}.

## Code to Analyze{{source_code}}
## Analysis Requirements
- **Primary Focus**: {{primary_focus}}
- **Secondary Concerns**: {{secondary_concerns}}
- **Performance Requirements**: {{performance_requirements}}
- **Code Standards**: {{coding_standards}}
- **Target Audience**: {{target_audience}}

## Specific Analysis Tasks

### 1. Code Quality Assessment
Evaluate the code for:
- Readability and maintainability
- Performance bottlenecks
- Security vulnerabilities
- Design pattern adherence
- Error handling robustness

### 2. Architecture Review
Analyze:
- Component separation and cohesion
- Dependency management
- Scalability considerations
- Testing coverage gaps
- Documentation completeness

### 3. Refactoring Recommendations
Provide:
- Specific refactoring steps with before/after examples
- Estimated impact on {{business_metrics}}
- Risk assessment for each change
- Implementation priority ranking
- Resource requirements

## Deliverables
1. **Executive Summary**: {{executive_focus}} appropriate insights
2. **Technical Details**: Implementation-ready recommendations
3. **Action Plan**: Prioritized steps with {{timeline_constraints}}
4. **Risk Mitigation**: Strategies for {{deployment_environment}}

## Success Metrics
Measure improvements in:
{{success_metrics}}

## Constraints
- Budget: {{budget_constraints}}
- Timeline: {{timeline_constraints}}
- Team Expertise: {{team_capabilities}}
- Business Requirements: {{business_constraints}}";

        modelBuilder.Entity<PromptContent>().HasData(
            new PromptContent
            {
                Id = CodeReviewContentId,
                PromptTemplateId = CodeReviewTemplateId,
                Content = codeReviewContent,
                ContentType = "text/plain",
                ContentSize = codeReviewContent.Length,
                ContentHash = CalculateContentHash(codeReviewContent),
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            },
            new PromptContent
            {
                Id = AdvancedCodeAnalysisContentId,
                PromptTemplateId = AdvancedCodeAnalysisTemplateId,
                Content = advancedCodeAnalysisContent,
                ContentType = "text/markdown",
                ContentSize = advancedCodeAnalysisContent.Length,
                ContentHash = CalculateContentHash(advancedCodeAnalysisContent),
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            }
        );
    }

    /// <summary>
    /// Seeds sample variables for the basic code review template using enhanced domain model.
    /// </summary>
    private static void SeedPromptVariables(ModelBuilder modelBuilder, Guid organizationId)
    {
        modelBuilder.Entity<PromptVariable>().HasData(
            new PromptVariable
            {
                Id = LanguageVariableId,
                Name = "language",
                Description = "Programming language of the code",
                DefaultValue = "javascript",
                Type = VariableType.String,
                IsRequired = true,
                SortOrder = 1,
                HelpText = "Specify the programming language (e.g., javascript, python, csharp)",
                ExampleValues = JsonSerializer.Serialize(new[] { "javascript", "python", "csharp", "java", "typescript" }),
                ValidationRules = JsonSerializer.Serialize(new { minLength = 2, maxLength = 20 }),
                PromptTemplateId = CodeReviewTemplateId,
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            },
            new PromptVariable
            {
                Id = CodeVariableId,
                Name = "code",
                Description = "The code to review",
                DefaultValue = "// Paste your code here",
                Type = VariableType.String,
                IsRequired = true,
                SortOrder = 2,
                HelpText = "Paste the code you want reviewed here",
                ExampleValues = JsonSerializer.Serialize(new[] { 
                    "function hello() { console.log('Hello World'); }",
                    "def calculate_sum(a, b): return a + b" 
                }),
                ValidationRules = JsonSerializer.Serialize(new { minLength = 10, maxLength = 10000 }),
                PromptTemplateId = CodeReviewTemplateId,
                OrganizationId = organizationId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            }
        );
    }

    #region Helper Methods

    /// <summary>
    /// Calculates a simple hash for content integrity verification.
    /// </summary>
    private static string CalculateContentHash(string content)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(content));
        return Convert.ToHexString(hashBytes)[..32]; // Take first 32 characters
    }

    #endregion

    #region Environment-Specific Seeding

    /// <summary>
    /// Seeds data for development environment with additional test data.
    /// </summary>
    public static void SeedDevelopmentData(ModelBuilder modelBuilder, Guid? organizationId = null)
    {
        SeedData(modelBuilder, organizationId);
        // Additional development-specific data can be added here
    }

    /// <summary>
    /// Seeds minimal data for production environment.
    /// </summary>
    public static void SeedProductionData(ModelBuilder modelBuilder, Guid? organizationId = null)
    {
        var orgId = organizationId ?? DefaultOrganizationId;
        
        // Seed only essential workflow categories for production
        SeedWorkflowCategories(modelBuilder, orgId);
        
        // Seed a minimal default lab
        modelBuilder.Entity<PromptLab>().HasData(
            new PromptLab
            {
                Id = DefaultLabId,
                Name = "Default Lab",
                Description = "Default prompt lab for enterprise operations",
                LabId = "default-lab",
                Owner = "system",
                Status = LabStatus.Active,
                Visibility = LabVisibility.Private,
                Tags = JsonSerializer.Serialize(new[] { "default", "production" }),
                OrganizationId = orgId,
                CreatedAt = SeedTimestamp,
                UpdatedAt = SeedTimestamp,
                CreatedBy = "system",
                UpdatedBy = "system",
                Classification = DataClassification.Internal
            }
        );
    }

    #endregion
}
