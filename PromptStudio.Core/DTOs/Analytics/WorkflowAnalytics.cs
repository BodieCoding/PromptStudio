namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Workflow analytics
/// </summary>
public class WorkflowAnalytics
{
    /// <summary>
    /// Common user workflows
    /// </summary>
    public List<UserWorkflow> CommonWorkflows { get; set; } = [];

    /// <summary>
    /// Workflow efficiency metrics
    /// </summary>
    public Dictionary<string, WorkflowEfficiencyMetrics> EfficiencyMetrics { get; set; } = [];

    /// <summary>
    /// Workflow abandonment points
    /// </summary>
    public List<WorkflowAbandonmentPoint> AbandonmentPoints { get; set; } = [];
}
