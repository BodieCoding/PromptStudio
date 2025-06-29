using System.ComponentModel.DataAnnotations;
using System.Security;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Enhanced PromptFlow with enterprise features, template traceability, and AI-assisted development
/// Supports complex workflow orchestration with full audit trail and performance analytics
/// </summary>
public class PromptFlow : AuditableEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Semantic version of the workflow (e.g., "1.2.0")
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// The workflow library this flow belongs to (organizational structure)
    /// </summary>
    public Guid WorkflowLibraryId { get; set; }
    public virtual WorkflowLibrary WorkflowLibrary { get; set; } = null!;
    
    /// <summary>
    /// Workflow status for approval workflows and lifecycle management
    /// </summary>
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Draft;
      /// <summary>
    /// Whether this workflow is currently active/published
    /// </summary>
    public new bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Whether this workflow requires approval before publishing
    /// </summary>
    public bool RequiresApproval { get; set; } = false;
    
    /// <summary>
    /// Base workflow for inheritance/forking scenarios
    /// </summary>
    public Guid? BaseFlowId { get; set; }
    public virtual PromptFlow? BaseFlow { get; set; }
    
    /// <summary>
    /// Workflow category for organization and discovery
    /// </summary>
    public WorkflowCategory Category { get; set; } = WorkflowCategory.General;
    
    /// <summary>
    /// Tags for categorization and search (JSON array format)
    /// </summary>
    [StringLength(1000)]
    public string? Tags { get; set; }
    
    /// <summary>
    /// Expected execution time in milliseconds (for resource planning)
    /// </summary>
    public int? ExpectedExecutionTime { get; set; }
    
    /// <summary>
    /// Expected cost per execution (for budgeting)
    /// </summary>
    public decimal? ExpectedCost { get; set; }
    
    // Performance and usage metrics (updated via background processes)
    public long ExecutionCount { get; set; } = 0;
    public decimal AverageCost { get; set; } = 0;
    public int AverageExecutionTime { get; set; } = 0;
    public decimal? QualityScore { get; set; }
    public DateTime? LastExecutedAt { get; set; }
    
    // AI-Assisted Development
    /// <summary>
    /// Whether this workflow was generated using AI assistance
    /// </summary>
    public bool IsAiGenerated { get; set; } = false;
    
    /// <summary>
    /// Original prompt used to generate this workflow (if AI-generated)
    /// </summary>
    public string? GenerationPrompt { get; set; }
    
    /// <summary>
    /// AI confidence score for generated workflows (0.0 - 1.0)
    /// </summary>
    public decimal? AiConfidenceScore { get; set; }
    
    /// <summary>
    /// Complexity score for resource allocation and optimization (1-10)
    /// </summary>
    public int ComplexityScore { get; set; } = 1;
      /// <summary>
    /// Whether this workflow is currently being used in A/B testing
    /// </summary>
    public bool IsInExperiment { get; set; } = false;
    
    // Flow Data Storage (Hybrid Approach)
    /// <summary>
    /// React Flow compatible JSON data for visual builder compatibility
    /// Contains the complete flow structure for frontend consumption
    /// </summary>
    public string FlowData { get; set; } = "{}";
    
    /// <summary>
    /// Storage mode for this workflow
    /// </summary>
    public FlowStorageMode StorageMode { get; set; } = FlowStorageMode.Hybrid;
    
    /// <summary>
    /// Last synchronization timestamp between JSON and relational data
    /// </summary>
    public DateTime? LastSyncAt { get; set; }
    
    /// <summary>
    /// Hash of the flow structure for change detection
    /// </summary>
    [StringLength(64)]
    public string? FlowHash { get; set; }
    
    // Navigation properties for relational workflow structure
    public virtual ICollection<FlowNode> Nodes { get; set; } = new List<FlowNode>();
    public virtual ICollection<FlowEdge> Edges { get; set; } = new List<FlowEdge>();
    public virtual ICollection<FlowExecution> Executions { get; set; } = new List<FlowExecution>();
    public virtual ICollection<WorkflowTemplateUsage> TemplateUsages { get; set; } = new List<WorkflowTemplateUsage>();
    public virtual ICollection<FlowVariant> Variants { get; set; } = new List<FlowVariant>();
    public virtual ICollection<WorkflowSuggestion> Suggestions { get; set; } = new List<WorkflowSuggestion>();
}
