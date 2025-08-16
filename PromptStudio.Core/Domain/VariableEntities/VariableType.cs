namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the supported data types for variables in the prompt templating system.
/// </summary>
public enum VariableType
{
    /// <summary>
    /// String/text data type for general text content.
    /// </summary>
    String = 0,

    /// <summary>
    /// Integer numeric data type for whole numbers.
    /// </summary>
    Integer = 1,

    /// <summary>
    /// Decimal numeric data type for fractional numbers.
    /// </summary>
    Decimal = 2,

    /// <summary>
    /// Boolean data type for true/false values.
    /// </summary>
    Boolean = 3,

    /// <summary>
    /// Date/time data type for temporal values.
    /// </summary>
    Date = 4,

    /// <summary>
    /// Email address data type with built-in validation.
    /// </summary>
    Email = 5,

    /// <summary>
    /// URL data type with built-in validation.
    /// </summary>
    Url = 6,

    /// <summary>
    /// JSON data type for structured data.
    /// </summary>
    Json = 7
}