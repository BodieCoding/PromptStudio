namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Position information for a variable reference
/// </summary>
public class VariablePosition
{
    /// <summary>
    /// Gets or sets the line number (1-based)
    /// </summary>
    public int Line { get; set; }

    /// <summary>
    /// Gets or sets the column number (1-based)
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Gets or sets the character index in the content
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or sets the length of the variable reference
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets the full text of the variable reference
    /// </summary>
    public string ReferenceText { get; set; } = string.Empty;
}
