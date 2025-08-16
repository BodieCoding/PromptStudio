namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Reason for variable similarity
/// </summary>
public class SimilarityReason
{
    /// <summary>
    /// Gets or sets the type of similarity (Name, Type, Usage, Context, etc.)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the similarity reason
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the weight/importance of this similarity factor (0.0 to 1.0)
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Gets or sets the confidence in this similarity reason (0.0 to 1.0)
    /// </summary>
    public double Confidence { get; set; } = 1.0;
}
