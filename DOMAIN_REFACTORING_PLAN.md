# Enterprise Domain Model Refactoring Plan

## Overview
Comprehensive refactoring of the PromptStudio backend Domain and Service layers for enterprise scalability and maintainability. This document tracks progress and provides a roadmap for the multi-phase refactoring effort.

## Objectives
- Migrate all core domain entities to use Guid identifiers
- Add multi-tenancy, audit, and soft delete support
- Move all DTOs out of Domain into dedicated DTOs folder
- Apply comprehensive enterprise-grade XML documentation
- Ensure each class/entity is in its own file
- Prepare architecture for advanced LLMOps features

---

## Phase 1: Domain Model Organization & Documentation *(Current Phase)*

### ✅ **Completed Entities**

#### **Core Entities with Full Documentation:**
- ✅ **PromptLab.cs** - Lab management with comprehensive docs
- ✅ **PromptLibrary.cs** - Library organization with full docs
- ✅ **PromptTemplate.cs** - Template management with comprehensive docs
- ✅ **PromptExecution.cs** - Execution tracking with full docs
- ✅ **PromptVariable.cs** - Variable management with comprehensive docs
- ✅ **VariableCollection.cs** - Collection management with full docs
- ✅ **TemplateVersion.cs** - Version control with comprehensive docs
- ✅ **PromptContent.cs** - Content management with full docs

#### **Workflow & Flow Entities:**
- ✅ **FlowNode.cs** - Workflow node with comprehensive docs
- ✅ **FlowEdge.cs** - Workflow edge with comprehensive docs
- ✅ **FlowExecution.cs** - Flow execution with comprehensive docs
- ✅ **NodeExecution.cs** - Node execution with comprehensive docs
- ✅ **EdgeTraversal.cs** - Edge traversal with comprehensive docs
- ✅ **PromptFlow.cs** - Main flow entity with comprehensive docs
- ✅ **FlowVariant.cs** - Flow variants with comprehensive docs
- ✅ **NodeExecutionStatus.cs** - Enum with comprehensive docs
- ✅ **WorkflowLibrary.cs** - Workflow library management with comprehensive docs

#### **Permission & Access Entities:**
- ✅ **TemplatePermission.cs** - Template access with comprehensive docs
- ✅ **LibraryPermission.cs** - Library access with comprehensive docs

#### **Lab & Member Entities:**
- ✅ **LabMember.cs** - Upgraded to full entity with comprehensive docs

#### **Category Entities (Converted from Enums):**
- ✅ **LibraryCategory.cs** - Converted to entity with comprehensive docs
- ✅ **CategoryType.cs** - Supporting enum with comprehensive docs
- ✅ **WorkflowCategory.cs** - Converted to entity with comprehensive docs
- ✅ **WorkflowCategoryType.cs** - Supporting enum with comprehensive docs

#### **Testing Entities:**
- ✅ **ABTestVariant.cs** - Variant entity with comprehensive docs
- ✅ **ABTestStatus.cs** - Status enum with comprehensive docs
- ✅ **TestEntityType.cs** - Entity type enum with comprehensive docs
- ✅ **VariantStatus.cs** - Variant status enum with comprehensive docs
- ✅ **ABTestResult.cs** - Result entity with comprehensive docs
- ✅ **ABTest.cs** - Main test entity with comprehensive docs

#### **Other Enums with Enhanced Documentation:**
- ✅ **TrendGranularity.cs** - Time granularity enum with comprehensive docs

### 🔄 **Currently Working On**

#### **Workflow & Library Entities (Next Priority):**
- 🔄 **WorkflowSuggestion.cs** - AI-powered workflow suggestions (needs comprehensive docs)

### 📋 **Still To Do in Phase 1**

#### **Workflow & Library Entities Needing Documentation:**
- 📝 **WorkflowTemplateUsage.cs** - Template usage tracking (needs comprehensive docs)

#### **Additional Enums Needing Documentation Enhancement:**
- 📝 **ExecutionStatus.cs** - Execution status enum (verify docs)
- 📝 **TemplateStatus.cs** - Template status enum (verify docs)
- 📝 **SuggestionType.cs**, **SuggestionStatus.cs**, **SuggestionPriority.cs** - Suggestion enums (verify docs)
- 📝 **FlowNodeType.cs**, **EdgeType.cs**, **FlowExecutionStatus.cs** - Flow enums (verify docs)

#### **Validation Tasks:**
- 📝 Ensure all persistent entities inherit from AuditableEntity
- 📝 Verify navigation properties are properly defined
- 📝 Check for any remaining DTOs in Domain folder
- 📝 Validate entity file organization (one class per file)

---

## Phase 2: Database Layer Updates

### 📋 **Tasks:**
1. **Update EnhancedPromptStudioDbContext:**
   - Add configurations for new entities (WorkflowCategory, etc.)
   - Remove configurations for converted enums
   - Update relationship mappings
   - Fix any compilation errors

2. **Generate Entity Framework Migrations:**
   - Create migration scripts for schema changes
   - Handle data migration for enum-to-entity conversions
   - Validate migration scripts

### 📁 **Key Files:**
- `PromptStudio.Data/EnhancedPromptStudioDbContext.cs`
- `PromptStudio.Data/Migrations/`

---

## Phase 3: Interface Layer Refactoring

### 📋 **Tasks:**
1. **Update repository interfaces:**
   - Modify to use Guid IDs
   - Add multi-tenancy support
   - Update method signatures for new entity relationships

2. **Update service interfaces:**
   - Align with new domain model
   - Add new methods for entity management
   - Remove deprecated methods

### 📁 **Key Files:**
- `PromptStudio.Core/Interfaces/`
- All `I*Repository.cs` and `I*Service.cs` files

---

## Phase 4: Implementation Layer Updates

### 📋 **Tasks:**
1. **Update repository implementations:**
   - Implement new interface methods
   - Add proper filtering for soft delete and multi-tenancy
   - Update queries for new relationships

2. **Update service implementations:**
   - Business logic updates for new model
   - Validation logic updates
   - Error handling improvements

### 📁 **Key Files:**
- `PromptStudio.Core/Services/`
- `PromptStudio.Data/Services/`

---

## Phase 5: MCP Server Updates

### 📋 **Tasks:**
1. **Update MCP server tools:**
   - Align with new domain model
   - Update DTOs and mappings
   - Test MCP integration

### 📁 **Key Files:**
- `PromptStudioMcpServer/`
- `PromptStudio.Mcp/`

---

## Phase 6: Web Application Updates

### 📋 **Tasks:**
1. **Frontend integration:**
   - Update UI components for new model
   - Update API controllers
   - Test end-to-end functionality

### 📁 **Key Files:**
- `PromptStudio/Controllers/`
- `PromptStudio/Pages/`

---

## Migration Documentation Created

### 📄 **Supporting Documentation:**
- ✅ `DTO_MIGRATION_PROGRESS.md` - Tracks DTO relocation progress
- ✅ `REFERENCE_DATA_MANAGEMENT_STRATEGY.md` - Enum management strategy
- ✅ `XML_DOCUMENTATION_SETUP.md` - VS Code XML doc setup guide
- ✅ `LIBRARY_CATEGORY_MIGRATION.md` - LibraryCategory conversion guide
- ✅ `WORKFLOW_CATEGORY_MIGRATION.md` - WorkflowCategory conversion guide

---

## Current Status

### **Where We Left Off:**
Working on completing ABTest.cs entity documentation and file separation.

### **Immediate Next Steps:**
1. ✅ Complete ABTest entity comprehensive documentation
2. 📝 Document WorkflowLibrary.cs with comprehensive XML docs
3. 📝 Document remaining workflow entities (WorkflowSuggestion, WorkflowTemplateUsage)
4. 📝 Enhance enum documentation (TrendGranularity, etc.)
5. 📝 Validate all entities follow established patterns

### **Key Patterns Established:**
- **Comprehensive XML Documentation:** Business context, technical context, value proposition, remarks with design patterns, performance considerations, integration points, examples
- **Entity Structure:** Guid IDs, AuditableEntity inheritance, multi-tenancy, audit trails, soft delete
- **File Organization:** One class per file, logical folder structure
- **DTO Separation:** All DTOs moved to dedicated DTOs folder structure

### **Quality Gates:**
- All entities must have comprehensive XML documentation
- All entities must use Guid identifiers
- All entities must support multi-tenancy and auditing
- All DTOs must be in dedicated DTOs folder
- Each class must be in its own file

---

## Branch Information
- **Working Branch:** `feature/visual-workflow-builder`
- **Target Branch:** `master` (after completion and testing)

---

*Last Updated: June 29, 2025*
*Current Phase: Phase 1 - Domain Model Organization & Documentation*
