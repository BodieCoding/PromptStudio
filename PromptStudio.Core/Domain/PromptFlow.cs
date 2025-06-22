namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a visual prompt flow
/// </summary>
public class PromptFlow
{
    /// <summary>
    /// Unique identifier for the flow
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Display name of the flow
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the flow
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Version of the flow (semantic versioning)
    /// </summary>
    public string Version { get; set; } = "1.0.0";

    /// <summary>
    /// User ID who owns this flow
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// JSON serialized tags for categorization
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// JSON serialized flow data (nodes, edges, variables)
    /// </summary>
    public string FlowData { get; set; } = "{}";

    /// <summary>
    /// Whether the flow is active/published
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// When the flow was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// When the flow was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
