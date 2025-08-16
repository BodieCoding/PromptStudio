using System.ComponentModel.DataAnnotations;
using PromptStudio.Core.Interfaces.Data;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a variable used within flow execution contexts.
/// Variables provide dynamic data substitution and state management within prompt flows.
/// </summary>
/// <remarks>
/// <para><strong>Flow Context:</strong></para>
/// <para>Variables in flow execution provide dynamic data that can be passed between nodes,
/// modified during execution, and used for conditional logic and output generation.
/// They support both simple string values and complex data types through JSON serialization.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>Input parameters for flow execution</description></item>
/// <item><description>Intermediate values passed between flow nodes</description></item>
/// <item><description>Configuration parameters for flow behavior</description></item>
/// <item><description>Output results from node processing</description></item>
/// </list>
/// </remarks>
public class Variable : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the variable.
    /// Must be unique within its scope and follow variable naming conventions.
    /// </summary>
    /// <value>
    /// The variable name. Cannot be null or empty.
    /// Maximum length is 100 characters.
    /// </value>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current value of the variable.
    /// Stored as a string but can represent various data types through serialization.
    /// </summary>
    /// <value>
    /// The variable value as a string. Can be null for undefined variables.
    /// </value>
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the data type of the variable.
    /// Indicates how the value should be interpreted and validated.
    /// </summary>
    /// <value>
    /// The variable data type from the VariableDataType enumeration.
    /// Default is String.
    /// </value>
    public VariableDataType DataType { get; set; } = VariableDataType.String;

    /// <summary>
    /// Gets or sets an optional description of the variable's purpose and usage.
    /// Provides documentation for flow designers and maintainers.
    /// </summary>
    /// <value>
    /// A descriptive explanation of the variable's purpose.
    /// Maximum length is 500 characters.
    /// </value>
    [StringLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the scope of the variable within the flow execution.
    /// Determines where and how the variable can be accessed.
    /// </summary>
    /// <value>
    /// The variable scope from the VariableScope enumeration.
    /// Default is Local.
    /// </value>
    public VariableScope Scope { get; set; } = VariableScope.Local;

    /// <summary>
    /// Gets or sets a value indicating whether this variable is required for flow execution.
    /// Required variables must have values before flow execution can begin.
    /// </summary>
    /// <value>
    /// True if the variable is required; false if optional.
    /// Default is false.
    /// </value>
    public bool IsRequired { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether this variable is read-only.
    /// Read-only variables cannot be modified during flow execution.
    /// </summary>
    /// <value>
    /// True if the variable is read-only; false if mutable.
    /// Default is false.
    /// </value>
    public bool IsReadOnly { get; set; } = false;

    /// <summary>
    /// Gets or sets the default value for the variable.
    /// Used when no explicit value is provided during flow execution.
    /// </summary>
    /// <value>
    /// The default value as a string, or null if no default is specified.
    /// </value>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets validation rules for the variable value.
    /// Stored as JSON string containing validation configuration.
    /// </summary>
    /// <value>
    /// JSON string containing validation rules, or null for no validation.
    /// </value>
    public string? ValidationRules { get; set; }

    /// <summary>
    /// Gets or sets tags for categorization and organization.
    /// Stored as JSON array of strings for flexible tagging.
    /// </summary>
    /// <value>
    /// JSON array of tag strings, or null if no tags are assigned.
    /// </value>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets a value indicating whether this variable has a value assigned.
    /// Convenience property for checking variable state.
    /// </summary>
    /// <value>
    /// True if Value is not null or empty; false otherwise.
    /// </value>
    public bool HasValue => !string.IsNullOrEmpty(Value);

    /// <summary>
    /// Gets a value indicating whether this variable has a default value.
    /// Convenience property for checking default value availability.
    /// </summary>
    /// <value>
    /// True if DefaultValue is not null or empty; false otherwise.
    /// </value>
    public bool HasDefaultValue => !string.IsNullOrEmpty(DefaultValue);

    /// <summary>
    /// Gets the effective value of the variable.
    /// Returns the current value if available, otherwise returns the default value.
    /// </summary>
    /// <value>
    /// The current value if not null/empty, otherwise the default value.
    /// </value>
    public string? EffectiveValue => HasValue ? Value : DefaultValue;
}
