# Application Architecture Analysis: Visual Builder Domain Requirements

## Critical Domain Gaps Identified

### 1. **Workflow-Template Relationship Missing**
- **Problem**: No clear association between PromptTemplates used in workflows and their execution history
- **Impact**: Cannot track which template versions were used in specific workflow executions
- **Solution**: Need `WorkflowTemplateUsage` entity for traceability

### 2. **Inadequate Workflow Organization**
- **Problem**: PromptFlow lacks library-style organization like PromptLibrary
- **Impact**: No hierarchical organization for complex workflow portfolios
- **Solution**: Need `WorkflowLibrary` entity similar to PromptLibrary

### 3. **Missing Workflow Analytics & Optimization**
- **Problem**: No domain support for A/B testing, performance comparison, or AI-assisted optimization
- **Impact**: Cannot achieve "AI-assisted workflow optimization" from project plan
- **Solution**: Need dedicated analytics and experimentation entities

### 4. **Poor Hybrid Storage Strategy**
- **Problem**: Storing complex workflow graphs as JSON limits queryability and performance
- **Impact**: Cannot efficiently query workflow structures or optimize execution paths
- **Solution**: Hybrid approach with relational nodes/edges + document storage

### 5. **No AI-Assisted Development Support**
- **Problem**: No domain entities to support AI helping users create flows
- **Impact**: Cannot achieve "AI-powered prompt suggestions" feature from project plan
- **Solution**: Need `WorkflowSuggestion` and `FlowGenerationContext` entities

## Recommended Enhanced Domain Architecture

### Core Workflow Organization
```csharp
public class WorkflowLibrary : AuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid PromptLabId { get; set; }
    public WorkflowCategory Category { get; set; }
    public LibraryVisibility Visibility { get; set; }
    
    // Organization
    public double SortOrder { get; set; }
    public string? Tags { get; set; }
    public bool IsPinned { get; set; }
    
    // Analytics
    public int WorkflowCount { get; set; }
    public DateTime? LastActivityAt { get; set; }
    
    // Navigation
    public virtual PromptLab PromptLab { get; set; } = null!;
    public virtual ICollection<PromptFlow> Workflows { get; set; }
    public virtual ICollection<WorkflowLibraryPermission> Permissions { get; set; }
}
```

### Enhanced PromptFlow with Enterprise Features
```csharp
public class PromptFlow : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Version { get; set; } = "1.0.0";
    
    // Organization
    public Guid WorkflowLibraryId { get; set; }
    public virtual WorkflowLibrary WorkflowLibrary { get; set; } = null!;
    
    // Status and Lifecycle
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Draft;
    public bool IsActive { get; set; } = true;
    public bool RequiresApproval { get; set; } = false;
    
    // Template Inheritance
    public Guid? BaseFlowId { get; set; }
    public virtual PromptFlow? BaseFlow { get; set; }
    
    // Performance and Analytics
    public long ExecutionCount { get; set; } = 0;
    public decimal AverageCost { get; set; } = 0;
    public int AverageExecutionTime { get; set; } = 0;
    public decimal? QualityScore { get; set; }
    public DateTime? LastExecutedAt { get; set; }
    
    // AI-Assisted Development
    public bool IsAiGenerated { get; set; } = false;
    public string? GenerationPrompt { get; set; }
    public decimal? AiConfidenceScore { get; set; }
    
    // Navigation Properties
    public virtual ICollection<FlowNode> Nodes { get; set; }
    public virtual ICollection<FlowEdge> Edges { get; set; }
    public virtual ICollection<FlowExecution> Executions { get; set; }
    public virtual ICollection<WorkflowTemplateUsage> TemplateUsages { get; set; }
    public virtual ICollection<FlowVariant> Variants { get; set; } // For A/B testing
}
```

### Relational Flow Structure (Hybrid Approach)
```csharp
public class FlowNode : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    public string NodeKey { get; set; } = string.Empty; // User-friendly identifier
    public FlowNodeType NodeType { get; set; }
    public string NodeData { get; set; } = "{}"; // JSON for node-specific config
    
    // Position and Layout
    public double PositionX { get; set; }
    public double PositionY { get; set; }
    public double Width { get; set; } = 200;
    public double Height { get; set; } = 100;
    
    // Template Association (when node uses a PromptTemplate)
    public Guid? PromptTemplateId { get; set; }
    public virtual PromptTemplate? PromptTemplate { get; set; }
    public string? TemplateVersion { get; set; } // Version used
    
    // Performance Tracking
    public long ExecutionCount { get; set; } = 0;
    public int AverageExecutionTime { get; set; } = 0;
    public decimal AverageCost { get; set; } = 0;
    
    // Navigation
    public virtual ICollection<FlowEdge> IncomingEdges { get; set; }
    public virtual ICollection<FlowEdge> OutgoingEdges { get; set; }
    public virtual ICollection<NodeExecution> Executions { get; set; }
}

public class FlowEdge : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    public Guid SourceNodeId { get; set; }
    public virtual FlowNode SourceNode { get; set; } = null!;
    
    public Guid TargetNodeId { get; set; }
    public virtual FlowNode TargetNode { get; set; } = null!;
    
    public string SourceHandle { get; set; } = "output";
    public string TargetHandle { get; set; } = "input";
    
    // Conditional Logic
    public string? Condition { get; set; } // JSON condition for conditional edges
    public bool IsDefault { get; set; } = false; // Default path for conditionals
    
    // Analytics
    public long TraversalCount { get; set; } = 0;
}
```

### Template-Workflow Association Tracking
```csharp
public class WorkflowTemplateUsage : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    public Guid TemplateId { get; set; }
    public virtual PromptTemplate Template { get; set; } = null!;
    
    public Guid NodeId { get; set; }
    public virtual FlowNode Node { get; set; } = null!;
    
    // Version tracking
    public string TemplateVersion { get; set; } = string.Empty;
    public string? TemplateSnapshot { get; set; } // Content snapshot
    
    // Usage context
    public string NodeRole { get; set; } = string.Empty; // "primary", "fallback", "validation"
    public bool IsActive { get; set; } = true;
    
    // Performance in this context
    public long ExecutionCount { get; set; } = 0;
    public decimal AverageCost { get; set; } = 0;
    public decimal? QualityScore { get; set; }
}
```

### Workflow Execution with Template Traceability
```csharp
public class FlowExecution : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    // Execution Context
    public string FlowVersion { get; set; } = string.Empty;
    public string InputVariables { get; set; } = "{}"; // JSON
    public string OutputResult { get; set; } = "{}"; // JSON
    
    // Performance Metrics
    public int TotalExecutionTime { get; set; }
    public decimal TotalCost { get; set; }
    public long TotalTokens { get; set; }
    
    // Status and Quality
    public ExecutionStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
    public decimal? QualityScore { get; set; }
    
    // Experimentation
    public Guid? ExperimentId { get; set; }
    public string? VariantId { get; set; }
    
    // User Feedback
    public int? UserRating { get; set; }
    public string? UserFeedback { get; set; }
    
    // Navigation
    public virtual ICollection<NodeExecution> NodeExecutions { get; set; }
}

public class NodeExecution : AuditableEntity
{
    public Guid FlowExecutionId { get; set; }
    public virtual FlowExecution FlowExecution { get; set; } = null!;
    
    public Guid NodeId { get; set; }
    public virtual FlowNode Node { get; set; } = null!;
    
    // Template Traceability (CRITICAL for your question)
    public Guid? PromptTemplateId { get; set; }
    public virtual PromptTemplate? PromptTemplate { get; set; }
    public string? TemplateVersion { get; set; }
    public Guid? PromptExecutionId { get; set; } // Links to PromptExecution for full traceability
    
    // Execution Details
    public int ExecutionOrder { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Input { get; set; } = "{}";
    public string Output { get; set; } = "{}";
    
    // Performance
    public int ExecutionTimeMs { get; set; }
    public decimal Cost { get; set; }
    public int TokensUsed { get; set; }
    
    // Status
    public NodeExecutionStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
}
```

### AI-Assisted Workflow Development
```csharp
public class WorkflowSuggestion : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    public SuggestionType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SuggestionData { get; set; } = "{}"; // JSON with specific suggestion details
    
    // AI Context
    public string AiModel { get; set; } = string.Empty;
    public decimal ConfidenceScore { get; set; }
    public string? ReasoningContext { get; set; }
    
    // User Response
    public SuggestionStatus Status { get; set; } = SuggestionStatus.Pending;
    public string? UserFeedback { get; set; }
    public DateTime? RespondedAt { get; set; }
    
    // Performance Impact (if applied)
    public decimal? PerformanceImpact { get; set; }
    public decimal? CostImpact { get; set; }
}

public class FlowGenerationContext : AuditableEntity
{
    public string UserIntent { get; set; } = string.Empty; // Natural language description
    public string? ExampleInputs { get; set; } // JSON array of example inputs
    public string? ExpectedOutputs { get; set; } // JSON array of expected outputs
    
    // Available Resources
    public Guid PromptLabId { get; set; }
    public string? AvailableTemplates { get; set; } // JSON array of template IDs
    public string? PreferredProviders { get; set; } // JSON array of AI providers
    
    // Generated Results
    public Guid? GeneratedFlowId { get; set; }
    public virtual PromptFlow? GeneratedFlow { get; set; }
    public decimal AiConfidenceScore { get; set; }
    public string GenerationLog { get; set; } = "{}"; // AI reasoning log
}
```

### A/B Testing and Optimization
```csharp
public class FlowVariant : AuditableEntity
{
    public Guid BaseFlowId { get; set; }
    public virtual PromptFlow BaseFlow { get; set; } = null!;
    
    public string VariantName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Changes { get; set; } = "{}"; // JSON describing what changed
    
    // A/B Testing
    public bool IsActive { get; set; } = true;
    public double TrafficPercentage { get; set; } = 0.0; // 0-100
    
    // Performance Comparison
    public long ExecutionCount { get; set; } = 0;
    public decimal AverageCost { get; set; } = 0;
    public int AverageExecutionTime { get; set; } = 0;
    public decimal? QualityScore { get; set; }
    public decimal? ConversionRate { get; set; }
    
    // Statistical Significance
    public bool IsStatisticallySignificant { get; set; } = false;
    public decimal? ConfidenceLevel { get; set; }
    
    // Navigation
    public virtual ICollection<FlowExecution> Executions { get; set; }
}
```

## Storage Strategy Recommendation: **Hybrid Approach**

### Why Hybrid?
1. **Relational for Structure**: Nodes, edges, and relationships benefit from relational integrity
2. **Document for Flexibility**: Node configurations and complex data structures in JSON
3. **Performance**: Can optimize queries on relational structure while maintaining flexibility
4. **Analytics**: Can efficiently analyze flow patterns and performance

### Implementation
```csharp
// Relational structure for queries and integrity
public class FlowNode : AuditableEntity { /* ... */ }
public class FlowEdge : AuditableEntity { /* ... */ }

// Document storage for complex configurations
public class NodeConfiguration
{
    public Dictionary<string, object> Properties { get; set; }
    public List<VariableMapping> Variables { get; set; }
    public ProviderSettings ProviderSettings { get; set; }
}
```

## Addressing Your Specific Questions

### 1. **PromptTemplate → Node → Execution Association**
✅ **Solution**: `WorkflowTemplateUsage` + `NodeExecution.PromptTemplateId` + `NodeExecution.PromptExecutionId`
- Complete traceability from template to workflow execution
- Version tracking for template changes
- Performance comparison across contexts

### 2. **Workflow Organization like PromptLibrary**
✅ **Solution**: `WorkflowLibrary` entity with same organizational features as PromptLibrary
- Hierarchical organization within PromptLabs
- Permissions and sharing controls
- Category and tag-based organization

### 3. **Performance Monitoring & A/B Testing**
✅ **Solution**: `FlowVariant` + comprehensive analytics entities
- Built-in A/B testing framework
- Statistical significance tracking
- Performance comparison across variants

### 4. **AI-Assisted Flow Creation**
✅ **Solution**: `WorkflowSuggestion` + `FlowGenerationContext`
- Natural language to workflow conversion
- Template recommendation engine
- Continuous optimization suggestions

This enhanced domain model provides enterprise-grade workflow management while maintaining the flexibility needed for AI-assisted development and complex analytics scenarios.
