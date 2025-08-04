namespace PromptStudio.Core.DTOs.Statistics;

/// <summary>
/// Placeholder DTO for template execution ranking statistics
/// </summary>
public class TemplateExecutionRank
{
    public Guid TemplateId { get; set; }
    public int ExecutionCount { get; set; }
    public DateTime LastExecuted { get; set; }
    public int UniqueUsers { get; set; }
}
