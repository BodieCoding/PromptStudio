# Library Category Migration and Seeding Strategy

## Overview
This document outlines the migration strategy from the static enum-based `LibraryCategory` to the flexible entity-based category system that supports both system-defined and user-defined categories.

## Migration Steps

### 1. Database Migration
```sql
-- Create LibraryCategories table (handled by EF Core migration)
-- Update PromptLibrary to reference LibraryCategory entity instead of enum

-- Migration will:
-- 1. Create new LibraryCategories table
-- 2. Add CategoryId (Guid) to PromptLibrary table  
-- 3. Migrate existing enum values to category entities
-- 4. Update foreign key references
-- 5. Remove old Category enum column
```

### 2. System-Defined Categories Seeding

Create these standard categories during application startup or migration:

```csharp
public static readonly List<LibraryCategory> SystemDefinedCategories = new()
{
    // Functional Categories
    new() { 
        Name = "General", 
        Description = "General-purpose templates and prompts for various use cases",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 0,
        IconName = "folder",
        Color = "#6b7280"
    },
    new() { 
        Name = "Customer Support", 
        Description = "Templates for customer service, support tickets, and customer communication",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 10,
        IconName = "support",
        Color = "#2563eb"
    },
    new() { 
        Name = "Marketing", 
        Description = "Marketing campaigns, content creation, and promotional materials",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 20,
        IconName = "megaphone",
        Color = "#dc2626"
    },
    new() { 
        Name = "Content Creation", 
        Description = "Content writing, copywriting, and creative content generation",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 30,
        IconName = "edit",
        Color = "#059669"
    },
    new() { 
        Name = "Development", 
        Description = "Software development, code generation, and technical documentation",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 40,
        IconName = "code",
        Color = "#7c3aed"
    },
    new() { 
        Name = "Data Analysis", 
        Description = "Data processing, analysis queries, and reporting templates",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 50,
        IconName = "chart",
        Color = "#0891b2"
    },
    new() { 
        Name = "Research", 
        Description = "Research methodologies, analysis frameworks, and investigation prompts",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 60,
        IconName = "search",
        Color = "#ea580c"
    },
    new() { 
        Name = "Quality Assurance", 
        Description = "Testing procedures, validation prompts, and quality control templates",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 70,
        IconName = "shield",
        Color = "#16a34a"
    },
    new() { 
        Name = "Documentation", 
        Description = "Technical writing, user guides, and documentation generation",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 80,
        IconName = "document",
        Color = "#0d9488"
    },
    new() { 
        Name = "Operations", 
        Description = "Operational procedures, process automation, and workflow templates",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 90,
        IconName = "cog",
        Color = "#7c2d12"
    },

    // Departmental Categories
    new() { 
        Name = "Sales", 
        Description = "Sales processes, customer outreach, and revenue generation templates",
        CategoryType = CategoryType.Departmental,
        IsSystemDefined = true,
        DisplayOrder = 100,
        IconName = "currency",
        Color = "#059669"
    },
    new() { 
        Name = "Legal", 
        Description = "Legal documentation, contract templates, and compliance materials",
        CategoryType = CategoryType.Departmental,
        IsSystemDefined = true,
        DisplayOrder = 110,
        IconName = "scale",
        Color = "#1f2937"
    },
    new() { 
        Name = "Finance", 
        Description = "Financial analysis, reporting, and accounting procedures",
        CategoryType = CategoryType.Departmental,
        IsSystemDefined = true,
        DisplayOrder = 120,
        IconName = "calculator",
        Color = "#166534"
    },
    new() { 
        Name = "Human Resources", 
        Description = "HR processes, employee communication, and organizational templates",
        CategoryType = CategoryType.Departmental,
        IsSystemDefined = true,
        DisplayOrder = 130,
        IconName = "users",
        Color = "#be185d"
    },
    new() { 
        Name = "Design", 
        Description = "Design processes, creative briefs, and design documentation",
        CategoryType = CategoryType.Departmental,
        IsSystemDefined = true,
        DisplayOrder = 140,
        IconName = "palette",
        Color = "#9333ea"
    },
    new() { 
        Name = "Product Management", 
        Description = "Product development, roadmap planning, and feature specifications",
        CategoryType = CategoryType.Departmental,
        IsSystemDefined = true,
        DisplayOrder = 150,
        IconName = "cube",
        Color = "#2563eb"
    },

    // Specialized Categories
    new() { 
        Name = "Security", 
        Description = "Security procedures, threat analysis, and compliance templates",
        CategoryType = CategoryType.Compliance,
        IsSystemDefined = true,
        DisplayOrder = 200,
        IconName = "lock",
        Color = "#dc2626"
    },
    new() { 
        Name = "Training", 
        Description = "Training materials, educational content, and learning resources",
        CategoryType = CategoryType.Functional,
        IsSystemDefined = true,
        DisplayOrder = 210,
        IconName = "academic-cap",
        Color = "#0891b2"
    },
    new() { 
        Name = "Compliance", 
        Description = "Regulatory compliance, audit procedures, and governance templates",
        CategoryType = CategoryType.Compliance,
        IsSystemDefined = true,
        DisplayOrder = 220,
        IconName = "clipboard-check",
        Color = "#16a34a"
    },
    new() { 
        Name = "Integration", 
        Description = "System integration, API documentation, and technical connectivity",
        CategoryType = CategoryType.Integration,
        IsSystemDefined = true,
        DisplayOrder = 230,
        IconName = "link",
        Color = "#7c3aed"
    }
};
```

## Usage Benefits

### 1. **Flexibility**
- Organizations can create custom categories specific to their needs
- Hierarchical categories support complex organizational structures
- System categories provide consistency and best practices

### 2. **Scalability**
- No code changes required for new categories
- Multi-tenant support with tenant-specific categories
- Unlimited category depth and complexity

### 3. **User Experience**
- Visual organization with icons and colors
- Custom ordering and hierarchy display
- Rich descriptions and contextual help

### 4. **Enterprise Features**
- Complete audit trails for category changes
- Soft delete for data preservation
- Role-based category management
- Integration with existing permission systems

## Implementation Notes

### Database Considerations
- Index on `Name`, `ParentCategoryId`, and `TenantId` for performance
- Consider materialized path or nested sets for deep hierarchies
- Implement circular reference prevention in application logic

### Caching Strategy
- Cache system-defined categories globally
- Cache user category trees by tenant
- Invalidate cache on category structure changes

### API Design
```csharp
// Category management API examples
public interface ICategoryService
{
    Task<LibraryCategory> CreateCategoryAsync(LibraryCategory category);
    Task<LibraryCategory> UpdateCategoryAsync(Guid id, LibraryCategory category);
    Task<bool> DeleteCategoryAsync(Guid id);
    Task<IEnumerable<LibraryCategory>> GetCategoriesAsync(Guid? tenantId = null);
    Task<IEnumerable<LibraryCategory>> GetCategoryTreeAsync(Guid? parentId = null);
    Task<bool> CanDeleteCategoryAsync(Guid id); // Check for dependencies
}
```

This flexible category system provides the foundation for sophisticated library organization while maintaining enterprise-grade features and performance.
