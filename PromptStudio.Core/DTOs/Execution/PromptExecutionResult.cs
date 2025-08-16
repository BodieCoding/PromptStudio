using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Result of a single prompt execution
/// </summary>
public class PromptExecutionResult
{
    /// <summary>
    /// Execution ID
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Name of the executed prompt template
    /// </summary>
    public string PromptName { get; set; } = string.Empty;

    /// <summary>
    /// Resolved prompt content with variables substituted
    /// </summary>
    public string ResolvedPrompt { get; set; } = string.Empty;

    /// <summary>
    /// Variables used in the execution
    /// </summary>
    public Dictionary<string, string> Variables { get; set; } = [];

    /// <summary>
    /// Whether the execution was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// Error code for categorization
    /// </summary>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Execution timestamp
    /// </summary>
    public DateTime ExecutedAt { get; set; }

    /// <summary>
    /// Execution duration
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// AI provider used
    /// </summary>
    public string? AiProvider { get; set; }

    /// <summary>
    /// Model used
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Token usage information
    /// </summary>
    public TokenUsage? TokenUsage { get; set; }

    /// <summary>
    /// Quality score if available
    /// </summary>
    public double? QualityScore { get; set; }

    /// <summary>
    /// Additional execution metadata
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = [];
}
