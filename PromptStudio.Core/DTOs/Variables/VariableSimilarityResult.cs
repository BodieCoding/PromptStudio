namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of variable similarity analysis
/// </summary>
public class VariableSimilarityResult
{
    /// <summary>
    /// Gets or sets the variable that is similar to the reference variable
    /// </summary>
    public Guid VariableId { get; set; }

    /// <summary>
    /// Gets or sets the name of the similar variable
    /// </summary>
    public string VariableName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the similar variable
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the similar variable
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the similarity score (0.0 to 1.0, where 1.0 is identical)
    /// </summary>
    public double SimilarityScore { get; set; }

    /// <summary>
    /// Gets or sets the reasons for similarity
    /// </summary>
    public List<SimilarityReason> SimilarityReasons { get; set; } = [];

    /// <summary>
    /// Gets or sets the template ID where this similar variable is used
    /// </summary>
    public Guid? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the template name where this similar variable is used
    /// </summary>
    public string? TemplateName { get; set; }

    /// <summary>
    /// Gets or sets the usage frequency of the similar variable
    /// </summary>
    public int UsageCount { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the similar variable
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets additional metadata about the similarity
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = [];
}
