namespace PromptStudio.Core.Domain;

/// <summary>
/// Type of variable used in a prompt template
/// </summary>
public enum VariableType
{
    Text,
    Number,
    Boolean,
    File,
    LargeText
}
