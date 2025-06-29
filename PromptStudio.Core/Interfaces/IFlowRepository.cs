using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Repository interface for PromptFlow operations
/// </summary>
public interface IFlowRepository
{
    // Basic CRUD operations
    Task<IEnumerable<PromptFlow>> GetFlowsAsync(Guid? tenantId = null, string? userId = null, string? tag = null, string? search = null);
    Task<PromptFlow?> GetFlowByIdAsync(Guid flowId);
    Task<PromptFlow> CreateFlowAsync(PromptFlow flow);
    Task<PromptFlow> UpdateFlowAsync(PromptFlow flow);
    Task<bool> DeleteFlowAsync(Guid flowId);
    
    // Flow structure operations
    Task<IEnumerable<FlowNode>> GetFlowNodesAsync(Guid flowId);
    Task<IEnumerable<FlowEdge>> GetFlowEdgesAsync(Guid flowId);
    Task SyncFlowDataAsync(Guid flowId); // Sync between JSON and relational data
    
    // Library operations
    Task<IEnumerable<PromptFlow>> GetFlowsByLibraryAsync(Guid libraryId);
    Task<IEnumerable<WorkflowLibrary>> GetWorkflowLibrariesAsync(Guid tenantId, Guid userId);
    
    // Version management
    Task<IEnumerable<PromptFlow>> GetFlowVersionsAsync(Guid baseFlowId);
    Task<PromptFlow?> GetFlowVersionAsync(Guid flowId, string version);
    
    // Analytics and metrics
    Task<FlowMetrics> GetFlowMetricsAsync(Guid flowId, DateTime? from = null, DateTime? to = null);
    Task UpdateFlowMetricsAsync(Guid flowId, FlowMetrics metrics);
    
    // Variants and A/B testing
    Task<IEnumerable<FlowVariant>> GetFlowVariantsAsync(Guid baseFlowId);
    Task<FlowVariant?> SelectVariantForExecutionAsync(Guid flowId, string? userId = null);
    
    // AI suggestions
    Task<IEnumerable<WorkflowSuggestion>> GetFlowSuggestionsAsync(Guid flowId);
    Task<WorkflowSuggestion> CreateSuggestionAsync(WorkflowSuggestion suggestion);
    Task UpdateSuggestionStatusAsync(Guid suggestionId, SuggestionStatus status, string? feedback = null);
}

/// <summary>
/// Repository interface for FlowExecution operations
/// </summary>
public interface IFlowExecutionRepository
{
    // Execution management
    Task<FlowExecution> CreateExecutionAsync(FlowExecution execution);
    Task<FlowExecution> UpdateExecutionAsync(FlowExecution execution);
    Task<FlowExecution?> GetExecutionByIdAsync(Guid executionId);
    
    // Execution history
    Task<IEnumerable<FlowExecution>> GetExecutionHistoryAsync(Guid flowId, int limit = 50, int offset = 0);
    Task<IEnumerable<FlowExecution>> GetUserExecutionHistoryAsync(string userId, int limit = 50, int offset = 0);
    
    // Node execution tracking
    Task<NodeExecution> CreateNodeExecutionAsync(NodeExecution nodeExecution);
    Task<NodeExecution> UpdateNodeExecutionAsync(NodeExecution nodeExecution);
    Task<IEnumerable<NodeExecution>> GetNodeExecutionsAsync(Guid flowExecutionId);
    
    // Metrics and analytics
    Task<ExecutionAnalytics> GetExecutionAnalyticsAsync(Guid flowId, DateTime? from = null, DateTime? to = null);
    Task<IEnumerable<ExecutionMetric>> GetExecutionMetricsAsync(Guid executionId);
    Task AddExecutionMetricAsync(ExecutionMetric metric);
    
    // Performance monitoring
    Task<IEnumerable<FlowExecution>> GetFailedExecutionsAsync(DateTime? since = null);
    Task<IEnumerable<FlowExecution>> GetSlowExecutionsAsync(int thresholdMs = 30000);
    Task<decimal> GetAverageCostAsync(Guid flowId, DateTime? from = null, DateTime? to = null);
    Task<double> GetAverageExecutionTimeAsync(Guid flowId, DateTime? from = null, DateTime? to = null);
}

/// <summary>
/// Repository interface for validation operations
/// </summary>
public interface IFlowValidationRepository
{
    Task<FlowValidationSession> CreateValidationSessionAsync(FlowValidationSession session);
    Task<FlowValidationSession?> GetValidationSessionAsync(Guid flowId, string version);
    Task<IEnumerable<FlowValidationSession>> GetValidationHistoryAsync(Guid flowId);
    Task InvalidateValidationCacheAsync(Guid flowId);
}

/// <summary>
/// Aggregated metrics for flows
/// </summary>
public class FlowMetrics
{
    public Guid FlowId { get; set; }
    public long TotalExecutions { get; set; }
    public decimal AverageCost { get; set; }
    public double AverageExecutionTime { get; set; }
    public decimal? SuccessRate { get; set; }
    public decimal? QualityScore { get; set; }
    public DateTime LastExecutedAt { get; set; }
    public DateTime CalculatedAt { get; set; }
}

/// <summary>
/// Execution analytics data
/// </summary>
public class ExecutionAnalytics
{
    public Guid FlowId { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    
    public long TotalExecutions { get; set; }
    public long SuccessfulExecutions { get; set; }
    public long FailedExecutions { get; set; }
    
    public decimal TotalCost { get; set; }
    public decimal AverageCost { get; set; }
    public long TotalTokens { get; set; }
    
    public double AverageExecutionTime { get; set; }
    public double MedianExecutionTime { get; set; }
    public double P95ExecutionTime { get; set; }
    
    public Dictionary<string, long> ErrorTypes { get; set; } = new();
    public Dictionary<string, long> NodeExecutionCounts { get; set; } = new();
    
    public decimal? QualityScore { get; set; }
    public int? UserRatingAverage { get; set; }
    
    public List<DailyMetrics> DailyBreakdown { get; set; } = new();
}

/// <summary>
/// Daily execution metrics
/// </summary>
public class DailyMetrics
{
    public DateTime Date { get; set; }
    public long Executions { get; set; }
    public decimal Cost { get; set; }
    public double AverageTime { get; set; }
    public decimal SuccessRate { get; set; }
}

/// <summary>
/// Flow validation session for caching validation results
/// </summary>
public class FlowValidationSession : AuditableEntity
{
    public Guid FlowId { get; set; }
    public virtual PromptFlow Flow { get; set; } = null!;
    
    [Required]
    [StringLength(20)]
    public string FlowVersion { get; set; } = string.Empty;
    
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Full validation result as JSON
    /// </summary>
    public string ValidationData { get; set; } = "{}";
    
    public DateTime ValidatedAt { get; set; }
    
    [StringLength(100)]
    public string? ValidatedBy { get; set; }
    
    public TimeSpan ValidationDuration { get; set; }
    
    /// <summary>
    /// Hash of flow structure for cache invalidation
    /// </summary>
    [StringLength(64)]
    public string FlowHash { get; set; } = string.Empty;
}
