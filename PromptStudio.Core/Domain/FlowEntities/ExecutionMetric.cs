using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Custom metrics for detailed workflow analytics
/// </summary>
public class ExecutionMetric : AuditableEntity
{
    /// <summary>
    /// Associated workflow execution
    /// </summary>
    public Guid FlowExecutionId { get; set; }
    public virtual FlowExecution FlowExecution { get; set; } = null!;
    
    [Required]
    [StringLength(50)]
    public string MetricName { get; set; } = string.Empty;
    
    [Required]
    public string MetricValue { get; set; } = string.Empty;
    
    [StringLength(20)]
    public string MetricType { get; set; } = "string"; // string, number, boolean, json
    
    [StringLength(50)]
    public string? MetricUnit { get; set; }
    
    [StringLength(200)]
    public string? Description { get; set; }
}