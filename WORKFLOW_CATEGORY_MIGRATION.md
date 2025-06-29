# WorkflowCategory Migration Strategy

## Overview
This document outlines the migration strategy for converting the WorkflowCategory enum to a flexible WorkflowCategory entity, enabling hierarchical categorization, multi-tenant support, and user-defined categories while preserving existing categorization schemes.

## Migration Goals
1. **Preserve Existing Categories**: Maintain all current workflow categorizations
2. **Enable Flexibility**: Support user-defined and hierarchical categories
3. **Enterprise Features**: Add multi-tenancy, audit trails, and soft delete
4. **Backward Compatibility**: Ensure smooth transition with minimal disruption

## Entity Structure

### WorkflowCategory Entity
```csharp
public class WorkflowCategory : AuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public bool IsSystemDefined { get; set; }
    public CategoryType CategoryType { get; set; }
    public int DisplayOrder { get; set; }
    public string? IconName { get; set; }
    public string? Color { get; set; }
    
    // Navigation properties
    public virtual WorkflowCategory? ParentCategory { get; set; }
    public virtual ICollection<WorkflowCategory> ChildCategories { get; set; }
    public virtual ICollection<PromptFlow> PromptFlows { get; set; }
}
```

### Legacy Enum Support
```csharp
public enum WorkflowCategoryType
{
    General = 0,
    DataProcessing = 1,
    ContentGeneration = 2,
    Analysis = 3,
    CustomerService = 4,
    Marketing = 5,
    Development = 6,
    QualityAssurance = 7,
    Research = 8,
    Operations = 9,
    Training = 10,
    Integration = 11,
    Experimentation = 12,
    Compliance = 13,
    Security = 14,
    Finance = 15,
    HumanResources = 16,
    Reporting = 17,
    Automation = 18,
    Testing = 19,
    Monitoring = 20
}
```

## Migration Steps

### Phase 1: Database Schema Migration

#### 1.1 Create WorkflowCategory Table
```sql
CREATE TABLE WorkflowCategories (
    Id uniqueidentifier PRIMARY KEY DEFAULT NEWID(),
    Name nvarchar(100) NOT NULL,
    Description nvarchar(500) NULL,
    ParentCategoryId uniqueidentifier NULL,
    IsSystemDefined bit NOT NULL DEFAULT 0,
    CategoryType int NOT NULL DEFAULT 0,
    DisplayOrder int NOT NULL DEFAULT 0,
    IconName nvarchar(50) NULL,
    Color nvarchar(7) NULL,
    
    -- AuditableEntity fields
    OrganizationId uniqueidentifier NULL,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    DeletedAt datetime2 NULL,
    CreatedBy nvarchar(100) NULL,
    UpdatedBy nvarchar(100) NULL,
    RowVersion rowversion NOT NULL,
    Classification int NOT NULL DEFAULT 1,
    
    -- Foreign key constraints
    CONSTRAINT FK_WorkflowCategories_ParentCategory 
        FOREIGN KEY (ParentCategoryId) REFERENCES WorkflowCategories(Id)
);
```

#### 1.2 Add WorkflowCategoryId to PromptFlows
```sql
-- Add new column
ALTER TABLE PromptFlows 
ADD WorkflowCategoryId uniqueidentifier NULL;

-- Add foreign key constraint
ALTER TABLE PromptFlows
ADD CONSTRAINT FK_PromptFlows_WorkflowCategory 
    FOREIGN KEY (WorkflowCategoryId) REFERENCES WorkflowCategories(Id);
```

### Phase 2: Seed System Categories

#### 2.1 Create System-Defined Categories
```csharp
var systemCategories = new[]
{
    new WorkflowCategory 
    { 
        Name = "General", 
        Description = "General-purpose workflows not fitting specific categories",
        IsSystemDefined = true, 
        CategoryType = CategoryType.General, 
        DisplayOrder = 0,
        IconName = "workflow",
        Color = "#6b7280"
    },
    new WorkflowCategory 
    { 
        Name = "Data Processing", 
        Description = "Data processing, transformation, and analysis workflows",
        IsSystemDefined = true, 
        CategoryType = CategoryType.Technical, 
        DisplayOrder = 10,
        IconName = "data-processing",
        Color = "#2563eb"
    },
    new WorkflowCategory 
    { 
        Name = "Content Generation", 
        Description = "Content creation, generation, and publishing workflows",
        IsSystemDefined = true, 
        CategoryType = CategoryType.Functional, 
        DisplayOrder = 20,
        IconName = "content-generation",
        Color = "#059669"
    },
    new WorkflowCategory 
    { 
        Name = "Analysis", 
        Description = "Data analysis, reporting, and insight generation workflows",
        IsSystemDefined = true, 
        CategoryType = CategoryType.Analytical, 
        DisplayOrder = 30,
        IconName = "analytics",
        Color = "#7c3aed"
    },
    new WorkflowCategory 
    { 
        Name = "Customer Service", 
        Description = "Customer service, support, and interaction workflows",
        IsSystemDefined = true, 
        CategoryType = CategoryType.Functional, 
        DisplayOrder = 40,
        IconName = "customer-service",
        Color = "#dc2626"
    },
    // ... continue for all legacy categories
};
```

#### 2.2 Seeding Service
```csharp
public class WorkflowCategorySeeder
{
    private readonly PromptStudioDbContext _context;
    
    public async Task SeedSystemCategoriesAsync()
    {
        var existingCategories = await _context.WorkflowCategories
            .Where(c => c.IsSystemDefined)
            .ToListAsync();
            
        if (existingCategories.Any()) return; // Already seeded
        
        var systemCategories = GetSystemCategories();
        
        await _context.WorkflowCategories.AddRangeAsync(systemCategories);
        await _context.SaveChangesAsync();
    }
    
    private WorkflowCategory[] GetSystemCategories()
    {
        return Enum.GetValues<WorkflowCategoryType>()
            .Select(categoryType => new WorkflowCategory
            {
                Name = GetDisplayName(categoryType),
                Description = GetDescription(categoryType),
                IsSystemDefined = true,
                CategoryType = GetCategoryType(categoryType),
                DisplayOrder = (int)categoryType * 10,
                IconName = GetIconName(categoryType),
                Color = GetColor(categoryType)
            }).ToArray();
    }
}
```

### Phase 3: Data Migration

#### 3.1 Migrate Existing Workflow Categories
```sql
-- Update PromptFlows with corresponding WorkflowCategory IDs
UPDATE pf 
SET WorkflowCategoryId = wc.Id
FROM PromptFlows pf
INNER JOIN WorkflowCategories wc ON 
    CASE pf.Category 
        WHEN 0 THEN 'General'
        WHEN 1 THEN 'Data Processing'
        WHEN 2 THEN 'Content Generation'
        WHEN 3 THEN 'Analysis'
        WHEN 4 THEN 'Customer Service'
        WHEN 5 THEN 'Marketing'
        WHEN 6 THEN 'Development'
        WHEN 7 THEN 'Quality Assurance'
        WHEN 8 THEN 'Research'
        WHEN 9 THEN 'Operations'
        WHEN 10 THEN 'Training'
        WHEN 11 THEN 'Integration'
        WHEN 12 THEN 'Experimentation'
        WHEN 13 THEN 'Compliance'
        WHEN 14 THEN 'Security'
        WHEN 15 THEN 'Finance'
        WHEN 16 THEN 'Human Resources'
        WHEN 17 THEN 'Reporting'
        WHEN 18 THEN 'Automation'
        WHEN 19 THEN 'Testing'
        WHEN 20 THEN 'Monitoring'
    END = wc.Name
WHERE wc.IsSystemDefined = 1;
```

#### 3.2 Validation Query
```sql
-- Verify migration completed successfully
SELECT 
    COUNT(*) as TotalFlows,
    COUNT(WorkflowCategoryId) as MigratedFlows,
    COUNT(*) - COUNT(WorkflowCategoryId) as UnmigratedFlows
FROM PromptFlows;
```

### Phase 4: Code Updates

#### 4.1 Update Entity Framework Configuration
```csharp
public class PromptFlowConfiguration : IEntityTypeConfiguration<PromptFlow>
{
    public void Configure(EntityTypeBuilder<PromptFlow> builder)
    {
        // Remove old Category enum property mapping
        builder.Ignore(e => e.Category);
        
        // Configure WorkflowCategory relationship
        builder.HasOne(e => e.WorkflowCategory)
            .WithMany(c => c.PromptFlows)
            .HasForeignKey(e => e.WorkflowCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
```

#### 4.2 Update Services and Repositories
```csharp
public class WorkflowService
{
    public async Task<PromptFlow> CreateWorkflowAsync(CreateWorkflowRequest request)
    {
        var workflow = new PromptFlow
        {
            Name = request.Name,
            WorkflowCategoryId = request.CategoryId ?? await GetDefaultCategoryIdAsync(),
            // ... other properties
        };
        
        return await _repository.CreateAsync(workflow);
    }
    
    private async Task<Guid> GetDefaultCategoryIdAsync()
    {
        var generalCategory = await _context.WorkflowCategories
            .FirstAsync(c => c.Name == "General" && c.IsSystemDefined);
        return generalCategory.Id;
    }
}
```

### Phase 5: Cleanup

#### 5.1 Remove Legacy Column (After Validation)
```sql
-- Remove old Category column after successful migration
ALTER TABLE PromptFlows DROP COLUMN Category;
```

#### 5.2 Add Required Constraint
```sql
-- Make WorkflowCategoryId required after migration
ALTER TABLE PromptFlows 
ALTER COLUMN WorkflowCategoryId uniqueidentifier NOT NULL;
```

## Testing Strategy

### 5.1 Unit Tests
```csharp
[Test]
public async Task WorkflowCategory_HierarchicalStructure_WorksCorrectly()
{
    // Arrange
    var parentCategory = new WorkflowCategory { Name = "Operations" };
    var childCategory = new WorkflowCategory 
    { 
        Name = "Quality Control", 
        ParentCategoryId = parentCategory.Id 
    };
    
    // Act & Assert
    Assert.That(childCategory.ParentCategory, Is.EqualTo(parentCategory));
    Assert.That(parentCategory.ChildCategories, Contains.Item(childCategory));
}
```

### 5.2 Integration Tests
```csharp
[Test]
public async Task WorkflowService_CreateWithCategory_AssignsCorrectly()
{
    // Test workflow creation with category assignment
    var categoryId = await SeedTestCategory();
    var workflow = await _workflowService.CreateAsync(new CreateWorkflowRequest
    {
        Name = "Test Workflow",
        CategoryId = categoryId
    });
    
    Assert.That(workflow.WorkflowCategoryId, Is.EqualTo(categoryId));
}
```

## Rollback Strategy

### Emergency Rollback
1. **Restore Legacy Column**: Add back the Category enum column
2. **Reverse Migration**: Copy data from WorkflowCategory back to enum
3. **Update Code**: Revert to enum-based implementation
4. **Validate**: Ensure all functionality works as before

### Rollback SQL
```sql
-- Add back legacy column
ALTER TABLE PromptFlows ADD Category int NOT NULL DEFAULT 0;

-- Restore data from WorkflowCategory
UPDATE pf 
SET Category = CASE wc.Name 
    WHEN 'General' THEN 0
    WHEN 'Data Processing' THEN 1
    -- ... etc
END
FROM PromptFlows pf
INNER JOIN WorkflowCategories wc ON pf.WorkflowCategoryId = wc.Id;
```

## Success Criteria

1. **Data Integrity**: All existing workflows maintain their categorization
2. **Feature Parity**: All existing functionality continues to work
3. **Performance**: No significant performance degradation
4. **User Experience**: Seamless transition for end users
5. **Flexibility**: New hierarchical and user-defined categories work correctly

## Timeline

- **Week 1**: Database schema changes and seeding
- **Week 2**: Data migration and validation
- **Week 3**: Code updates and testing
- **Week 4**: Deployment and monitoring
- **Week 5**: Cleanup and optimization

## Monitoring

- **Migration Progress**: Track conversion of workflows to new categories
- **Performance Impact**: Monitor query performance and database load
- **Error Rates**: Watch for category-related errors or issues
- **User Feedback**: Collect feedback on new categorization features

This migration strategy ensures a smooth transition from enum-based to entity-based workflow categorization while preserving data integrity and enabling new enterprise features.
