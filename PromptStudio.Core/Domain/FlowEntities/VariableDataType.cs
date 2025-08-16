namespace PromptStudio.Core.Domain;

/// <summary>
/// Enumeration of variable data types supported in flow execution.
/// </summary>
public enum VariableDataType
{
    /// <summary>
    /// String data type for text values.
    /// </summary>
    String,

    /// <summary>
    /// Integer data type for whole numbers.
    /// </summary>
    Integer,

    /// <summary>
    /// Decimal data type for floating-point numbers.
    /// </summary>
    Decimal,

    /// <summary>
    /// Boolean data type for true/false values.
    /// </summary>
    Boolean,

    /// <summary>
    /// DateTime data type for date and time values.
    /// </summary>
    DateTime,

    /// <summary>
    /// JSON data type for complex objects and arrays.
    /// </summary>
    Json,

    /// <summary>
    /// Array data type for collections of values.
    /// </summary>
    Array
}
