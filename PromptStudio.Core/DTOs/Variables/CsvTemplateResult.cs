using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Represents the result of CSV template generation
/// </summary>
public class CsvTemplateResult
{
    /// <summary>
    /// Gets or sets the generated CSV template content
    /// </summary>
    [Required]
    public string CsvContent { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the template name used for generation
    /// </summary>
    [Required]
    public string TemplateName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of variable names included in the template
    /// </summary>
    public List<string> VariableNames { get; set; } = new();

    /// <summary>
    /// Gets or sets the number of sample rows generated
    /// </summary>
    public int SampleRowCount { get; set; }

    /// <summary>
    /// Gets or sets any warnings or notes about the generated template
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Gets or sets the generation timestamp
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}
