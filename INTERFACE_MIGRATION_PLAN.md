# Interface Migration Plan: Integer to Guid Identifiers

## Executive Summary

All domain entities have been successfully migrated to use Guid identifiers and enterprise features. Now we need to update all service interfaces to reflect these changes and ensure type consistency throughout the application.

## Current Domain Entity Status

### ✅ Fully Migrated to Guid/AuditableEntity
- **PromptLab** - Uses Guid ID (manual implementation)
- **PromptLibrary** - Uses AuditableEntity (Guid ID)
- **PromptTemplate** - Uses AuditableEntity (Guid ID)
- **PromptVariable** - Uses AuditableEntity (Guid ID) ✅ Just updated
- **PromptExecution** - Uses AuditableEntity (Guid ID) ✅ Just updated  
- **VariableCollection** - Uses AuditableEntity (Guid ID) ✅ Just updated

### ✅ Workflow Entities (Already Guid-based)
- **PromptFlow**, **FlowNode**, **FlowEdge**, **NodeExecution** - All use AuditableEntity

## Interface Files Requiring Updates

### 🔴 HIGH PRIORITY - Core Entity Interfaces

1. **IVariableService.cs** - Currently open, uses `int` parameters
2. **IPromptTemplateService.cs** - Uses `int` templateId parameters
3. **IPromptExecutionService.cs** - Uses `int` templateId, collectionId parameters
4. **IPromptLibraryService.cs** - Likely uses `int` libraryId parameters
5. **IPromptLabService.cs** - May use `int` labId parameters
6. **IPromptService.cs** - Main coordinator service interface

### 🟡 MEDIUM PRIORITY - Workflow Interfaces

7. **IFlowService.cs** - May already be Guid-based but needs verification
8. **IFlowRepository.cs** - Repository patterns for workflow entities

### 🟢 LOW PRIORITY - Infrastructure Interfaces

9. **IPromptStudioDbContext.cs** - DbContext interface updates
10. **IModelProvider.cs** - Provider abstraction (likely no ID changes needed)
11. **IModelProviderManager.cs** - Provider management (likely no ID changes needed)

## Detailed Migration Tasks

### Phase 1: Update Core Entity Interfaces (Priority 1)

#### 1.1 IVariableService.cs Updates
```csharp
// Current (int-based):
Task<List<VariableCollection>> GetVariableCollectionsAsync(int promptId);
Task<VariableCollection> CreateVariableCollectionAsync(string name, int promptId, string csvData, string? description = null);
Task<string> GenerateCsvTemplateAsync(int templateId);
Task<List<VariableCollection>> ListVariableCollectionsAsync(int promptId);

// Target (Guid-based):
Task<List<VariableCollection>> GetVariableCollectionsAsync(Guid promptId);
Task<VariableCollection> CreateVariableCollectionAsync(string name, Guid promptId, string csvData, string? description = null);
Task<string> GenerateCsvTemplateAsync(Guid templateId);
Task<List<VariableCollection>> ListVariableCollectionsAsync(Guid promptId);
```

#### 1.2 IPromptTemplateService.cs Updates
```csharp
// Update all int parameters to Guid:
Task<List<PromptTemplate>> GetPromptTemplatesAsync(Guid? libraryId = null);
Task<PromptTemplate?> GetPromptTemplateByIdAsync(Guid id);
Task<PromptTemplate> CreatePromptTemplateAsync(string name, string content, Guid libraryId, string? description = null);
Task<PromptTemplate?> UpdatePromptTemplateAsync(Guid promptTemplateId, string name, string content, Guid libraryId, string? description);
Task<bool> DeletePromptTemplateAsync(Guid promptTemplateId);
```

#### 1.3 IPromptExecutionService.cs Updates
```csharp
// Update all int parameters to Guid:
Task<ExecutionResult> ExecutePromptTemplateAsync(Guid templateId, string variables, string? aiProvider = null, string? model = null);
Task<ExecutionResult> ExecutePromptTemplateAsync(Guid templateId, Dictionary<string, string> variableValues, string? aiProvider = null, string? model = null);
Task<BatchExecutionResult> ExecuteBatchAsync(Guid collectionId, Guid promptId);
Task<List<IndividualExecutionResult>> BatchExecuteAsync(Guid templateId, List<Dictionary<string, string>> variableSets, string? aiProvider = null, string? model = null);
```

#### 1.4 IPromptLibraryService.cs Updates
```csharp
// Update library operations:
Task<List<PromptLibrary>> GetLibrariesAsync(Guid? labId = null);
Task<PromptLibrary?> GetLibraryByIdAsync(Guid id);
Task<PromptLibrary> CreateLibraryAsync(string name, Guid labId, string? description = null);
Task<PromptLibrary?> UpdateLibraryAsync(Guid libraryId, string name, string? description);
Task<bool> DeleteLibraryAsync(Guid libraryId);
```

#### 1.5 IPromptLabService.cs Updates
```csharp
// PromptLab operations (likely already correct):
Task<List<PromptLab>> GetLabsAsync();
Task<PromptLab?> GetLabByIdAsync(Guid id);
Task<PromptLab?> GetLabByLabIdAsync(string labId);
Task<PromptLab> CreateLabAsync(string name, string labId, string? description = null);
```

### Phase 2: Update Result and DTO Types

#### 2.1 ExecutionResult Types
```csharp
public class ExecutionResult
{
    public Guid ExecutionId { get; set; }      // Was int
    public Guid TemplateId { get; set; }       // Was int
    public Guid? CollectionId { get; set; }    // Was int?
    // ... other properties
}

public class BatchExecutionResult
{
    public Guid CollectionId { get; set; }     // Was int
    public Guid TemplateId { get; set; }       // Was int
    public List<IndividualExecutionResult> Results { get; set; }
}

public class IndividualExecutionResult
{
    public Guid? ExecutionId { get; set; }     // Was int?
    // ... other properties
}
```

### Phase 3: Update Service Implementations

After interface updates, update corresponding service classes:
- `VariableService.cs`
- `PromptTemplateService.cs` 
- `PromptExecutionService.cs`
- `PromptLibraryService.cs`
- `PromptLabService.cs`

### Phase 4: Update Repository Patterns

Update any repository interfaces and implementations:
- Change all `int id` parameters to `Guid id`
- Update query methods and filters
- Ensure proper indexing for Guid columns

### Phase 5: Update API Controllers and DTOs

Update REST API layer:
- Route parameters from `int` to `Guid`
- Request/Response DTOs
- Model binding and validation

## Implementation Order

### Week 1: Core Interfaces
1. **Day 1-2:** Update `IVariableService` and `IPromptTemplateService`
2. **Day 3-4:** Update `IPromptExecutionService` and `IPromptLibraryService`  
3. **Day 5:** Update `IPromptLabService` and test compilation

### Week 2: Service Implementations
1. **Day 1-2:** Update service implementations to match new interfaces
2. **Day 3-4:** Update repository patterns and database queries
3. **Day 5:** Integration testing and bug fixes

### Week 3: API and Integration
1. **Day 1-2:** Update API controllers and routing
2. **Day 3-4:** Update frontend API calls and navigation
3. **Day 5:** End-to-end testing and validation

## Validation Checklist

### ✅ Interface Consistency
- [ ] All entity ID parameters are `Guid` type
- [ ] Return types use `Guid` identifiers  
- [ ] Navigation properties reference correct types
- [ ] Method signatures are consistent across related interfaces

### ✅ Breaking Changes Management
- [ ] Document all breaking changes
- [ ] Provide migration guide for API consumers
- [ ] Maintain backward compatibility where possible
- [ ] Update API versioning strategy

### ✅ Performance Considerations
- [ ] Database indexes updated for Guid columns
- [ ] Query performance validated
- [ ] Caching strategies updated for Guid keys
- [ ] Memory usage patterns assessed

## Expected Benefits Post-Migration

### Immediate
- **Type Safety:** Eliminates int/Guid mixing errors
- **API Consistency:** All endpoints use same identifier type
- **Development Speed:** IntelliSense and compiler catch ID type mismatches

### Long-term
- **Scalability:** Ready for distributed deployments
- **Security:** Non-enumerable identifiers across all APIs
- **Integration:** Compatible with external systems expecting UUIDs

## Risk Mitigation

### Compilation Errors
- Update interfaces incrementally to catch issues early
- Use compiler flags to treat warnings as errors
- Comprehensive unit test coverage

### Runtime Issues  
- Extensive integration testing
- Database migration validation
- API contract testing with updated schemas

### Performance Regression
- Benchmark key operations before/after migration
- Monitor database query performance
- Load testing with realistic data volumes

This comprehensive migration ensures PromptStudio's interface layer aligns with the enterprise-grade domain model architecture.
