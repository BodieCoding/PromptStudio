using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Options for CSV template generation
/// </summary>
public class CsvGenerationOptions
{
    /// <summary>
    /// Gets or sets the number of sample rows to generate
    /// Default is 5 sample rows
    /// </summary>
    [Range(1, 100)]
    public int SampleRowCount { get; set; } = 5;

    /// <summary>
    /// Gets or sets whether to include example data in the sample rows
    /// </summary>
    public bool IncludeExampleData { get; set; } = true;

    /// <summary>
    /// Gets or sets the CSV delimiter to use
    /// Default is comma ","
    /// </summary>
    [Required]
    public string Delimiter { get; set; } = ",";

    /// <summary>
    /// Gets or sets whether to include headers in the CSV
    /// </summary>
    public bool IncludeHeaders { get; set; } = true;

    /// <summary>
    /// Gets or sets custom example values for specific variables
    /// Key is variable name, value is the example value to use
    /// </summary>
    public Dictionary<string, string> CustomExamples { get; set; } = new();

    /// <summary>
    /// Gets or sets whether to add descriptive comments as the first row
    /// </summary>
    public bool IncludeComments { get; set; } = false;

    /// <summary>
    /// Gets or sets the encoding to use for the CSV file
    /// Default is UTF-8
    /// </summary>
    public string Encoding { get; set; } = "UTF-8";
}
