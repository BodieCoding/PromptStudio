namespace PromptStudio.Core.Domain.VariableEntities;

/// <summary>
/// Represents a variable definition within the prompt templating system.
/// Provides comprehensive variable management with type validation, constraints, and metadata support.
/// </summary>
/// <remarks>
/// <para><strong>Variable System Foundation:</strong></para>
/// <para>Core entity that defines reusable variables for prompt templates including data types,
/// validation rules, default values, and usage constraints. Essential for dynamic prompt generation,
/// template reusability, and user input management across the platform.</para>
/// 
/// <para><strong>Type System Integration:</strong></para>
/// <para>Supports strongly-typed variables with built-in validation, custom constraints,
/// and automatic conversion capabilities. Enables safe variable substitution and prevents
/// runtime errors through compile-time and design-time validation.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Template variable definitions with validation rules</description></item>
/// <item><description>User input collection and validation systems</description></item>
/// <item><description>Dynamic content generation with type safety</description></item>
/// <item><description>Reusable variable libraries for organization-wide standards</description></item>
/// </list>
/// </remarks>
public class Variable : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique name identifier for this variable.
    /// Used for template substitution and variable reference throughout the system.
    /// </summary>
    /// <value>
    /// A unique name that follows variable naming conventions. Must be alphanumeric with underscores.
    /// </value>
    /// <example>
    /// Examples: "user_name", "product_description", "analysis_type"
    /// </example>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the human-readable display name for this variable.
    /// Provides user-friendly labeling for forms, documentation, and UI components.
    /// </summary>
    /// <value>
    /// A descriptive display name for user interfaces and documentation.
    /// </value>
    /// <example>
    /// Examples: "User Name", "Product Description", "Analysis Type"
    /// </example>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description explaining the purpose and usage of this variable.
    /// Provides context and guidance for template authors and users.
    /// </summary>
    /// <value>
    /// A comprehensive description of the variable's purpose, expected values, and usage context.
    /// </value>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the data type of this variable for validation and conversion.
    /// Determines how the variable value is validated, stored, and converted.
    /// </summary>
    /// <value>
    /// The variable data type from the supported type enumeration.
    /// </value>
    public VariableType DataType { get; set; } = VariableType.String;

    /// <summary>
    /// Gets or sets the default value for this variable when no value is provided.
    /// Supports template execution with partial variable sets and provides fallback behavior.
    /// </summary>
    /// <value>
    /// The default value as a string that will be converted to the appropriate data type.
    /// Null indicates no default value is available.
    /// </value>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this variable is required for template execution.
    /// Controls validation behavior and user input requirements.
    /// </summary>
    /// <value>
    /// True if the variable must have a value for template execution; false if optional.
    /// </value>
    public bool IsRequired { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether this variable is sensitive and should be handled securely.
    /// Affects logging, display, and storage behavior for security compliance.
    /// </summary>
    /// <value>
    /// True if the variable contains sensitive data (passwords, API keys, PII); false otherwise.
    /// </value>
    public bool IsSensitive { get; set; } = false;

    /// <summary>
    /// Gets or sets the minimum allowed length for string values.
    /// Provides validation constraints for text-based variables.
    /// </summary>
    /// <value>
    /// The minimum character length for string values, or null for no minimum constraint.
    /// </value>
    public int? MinLength { get; set; }

    /// <summary>
    /// Gets or sets the maximum allowed length for string values.
    /// Provides validation constraints and helps prevent excessive resource usage.
    /// </summary>
    /// <value>
    /// The maximum character length for string values, or null for no maximum constraint.
    /// </value>
    public int? MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the minimum allowed value for numeric variables.
    /// Provides range validation for integer and decimal variable types.
    /// </summary>
    /// <value>
    /// The minimum numeric value, or null for no minimum constraint.
    /// </value>
    public decimal? MinValue { get; set; }

    /// <summary>
    /// Gets or sets the maximum allowed value for numeric variables.
    /// Provides range validation for integer and decimal variable types.
    /// </summary>
    /// <value>
    /// The maximum numeric value, or null for no maximum constraint.
    /// </value>
    public decimal? MaxValue { get; set; }

    /// <summary>
    /// Gets or sets the regular expression pattern for value validation.
    /// Provides advanced validation rules for string and formatted data types.
    /// </summary>
    /// <value>
    /// A regular expression pattern that valid values must match, or null for no pattern validation.
    /// </value>
    /// <example>
    /// Examples: "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$" for email validation
    /// </example>
    public string? ValidationPattern { get; set; }

    /// <summary>
    /// Gets or sets the error message displayed when validation fails.
    /// Provides user-friendly feedback for validation errors and input guidance.
    /// </summary>
    /// <value>
    /// A custom error message for validation failures, or null to use default system messages.
    /// </value>
    public string? ValidationErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the collection of predefined values for enumeration-type variables.
    /// Restricts variable values to a specific set of allowed options.
    /// </summary>
    /// <value>
    /// A JSON array of allowed values for selection-based variables, or null for unrestricted input.
    /// </value>
    /// <example>
    /// Example: ["small", "medium", "large"] for size selection
    /// </example>
    public string? AllowedValues { get; set; }

    /// <summary>
    /// Gets or sets help text providing usage guidance and examples for users.
    /// Assists users in understanding how to provide appropriate values for the variable.
    /// </summary>
    /// <value>
    /// Helpful text explaining expected values, format requirements, or usage examples.
    /// </value>
    public string? HelpText { get; set; }

    /// <summary>
    /// Gets or sets placeholder text displayed in input fields.
    /// Provides inline guidance and examples for user input interfaces.
    /// </summary>
    /// <value>
    /// Placeholder text for input fields, or null for no placeholder.
    /// </value>
    /// <example>
    /// Examples: "Enter your email address", "e.g., john@example.com"
    /// </example>
    public string? PlaceholderText { get; set; }

    /// <summary>
    /// Gets or sets the display order for this variable in user interfaces.
    /// Controls the sequence of variables in forms, documentation, and configuration interfaces.
    /// </summary>
    /// <value>
    /// The sort order for display purposes. Lower values appear first.
    /// </value>
    public int DisplayOrder { get; set; } = 0;

    /// <summary>
    /// Gets or sets the category for organizing related variables.
    /// Enables grouping and filtering of variables in management interfaces.
    /// </summary>
    /// <value>
    /// A category name for variable organization, or null for no categorization.
    /// </value>
    /// <example>
    /// Examples: "User Information", "System Configuration", "Content Parameters"
    /// </example>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets collection of tags for enhanced variable discovery and organization.
    /// Supports flexible tagging systems for variable management and search capabilities.
    /// </summary>
    /// <value>
    /// A JSON array of tags associated with this variable, or null for no tags.
    /// </value>
    /// <example>
    /// Example: ["authentication", "required", "user-input"] for login-related variables
    /// </example>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets additional metadata for extensible variable configuration.
    /// Provides storage for custom properties and integration-specific data.
    /// </summary>
    /// <value>
    /// A JSON object containing custom metadata and configuration options.
    /// </value>
    public string? Metadata { get; set; }

    /// <summary>
    /// Gets or sets the version number for this variable definition.
    /// Supports variable evolution and compatibility tracking across template versions.
    /// </summary>
    /// <value>
    /// A version identifier for tracking variable definition changes.
    /// </value>
    public string Version { get; set; } = "1.0";


    /// <summary>
    /// Gets or sets the collection of prompt templates that use this variable.
    /// Provides navigation and dependency tracking for impact analysis.
    /// </summary>
    /// <value>
    /// Navigation property to related prompt templates. Lazy-loaded by Entity Framework.
    /// </value>
    public virtual ICollection<PromptTemplate> PromptTemplates { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of variable collections that include this variable.
    /// Enables organized grouping and batch management of related variables.
    /// </summary>
    /// <value>
    /// Navigation property to related variable collections. Lazy-loaded by Entity Framework.
    /// </value>
    public virtual ICollection<VariableCollection> VariableCollections { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of execution logs that reference this variable.
    /// Provides usage tracking and audit trail for variable utilization analysis.
    /// </summary>
    /// <value>
    /// Navigation property to related execution logs. Lazy-loaded by Entity Framework.
    /// </value>
    public virtual ICollection<PromptExecution> ExecutionLogs { get; set; } = [];

    /// <summary>
    /// Gets a value indicating whether this variable has validation constraints.
    /// Computed property for quick validation requirement assessment.
    /// </summary>
    /// <value>
    /// True if any validation constraints are defined; false otherwise.
    /// </value>
    public bool HasValidationConstraints => 
        IsRequired || 
        MinLength.HasValue || 
        MaxLength.HasValue || 
        MinValue.HasValue || 
        MaxValue.HasValue || 
        !string.IsNullOrEmpty(ValidationPattern) ||
        !string.IsNullOrEmpty(AllowedValues);

    /// <summary>
    /// Gets a value indicating whether this variable has a default value.
    /// Computed property for template execution and form initialization logic.
    /// </summary>
    /// <value>
    /// True if a default value is specified; false otherwise.
    /// </value>
    public bool HasDefaultValue => !string.IsNullOrEmpty(DefaultValue);
   
    /// <summary>
    /// Gets the allowed values as a parsed collection for enumeration handling.
    /// Computed property that deserializes the AllowedValues JSON for programmatic access.
    /// </summary>
    /// <value>
    /// A collection of allowed values, or empty collection if none specified.
    /// </value>
    public ICollection<string> GetAllowedValues()
    {
        if (string.IsNullOrEmpty(AllowedValues))
            return [];

        try
        {
            var values = System.Text.Json.JsonSerializer.Deserialize<string[]>(AllowedValues);
            return values?.ToList() ?? [];
        }
        catch
        {
            return [];
        }
    }

    /// <summary>
    /// Gets the tags as a parsed collection for programmatic access.
    /// Computed property that deserializes the Tags JSON for filtering and search operations.
    /// </summary>
    /// <value>
    /// A collection of tags, or empty collection if none specified.
    /// </value>
    public ICollection<string> GetTags()
    {
        if (string.IsNullOrEmpty(Tags))
            return [];

        try
        {
            var tags = System.Text.Json.JsonSerializer.Deserialize<string[]>(Tags);
            return tags?.ToList() ?? [];
        }
        catch
        {
            return [];
        }
    }

    /// <summary>
    /// Sets the allowed values from a collection and serializes to JSON.
    /// Convenience method for programmatic management of enumeration values.
    /// </summary>
    /// <param name="values">The collection of allowed values</param>
    public void SetAllowedValues(IEnumerable<string> values)
    {
        if (values == null || !values.Any())
        {
            AllowedValues = null;
            return;
        }

        AllowedValues = System.Text.Json.JsonSerializer.Serialize(values.ToArray());
    }

    /// <summary>
    /// Sets the tags from a collection and serializes to JSON.
    /// Convenience method for programmatic tag management and organization.
    /// </summary>
    /// <param name="tags">The collection of tags</param>
    public void SetTags(IEnumerable<string> tags)
    {
        if (tags == null || !tags.Any())
        {
            Tags = null;
            return;
        }

        Tags = System.Text.Json.JsonSerializer.Serialize(tags.ToArray());
    }

    /// <summary>
    /// Validates a value against this variable's constraints and type requirements.
    /// Provides comprehensive validation for runtime value checking and user input validation.
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>A validation result indicating success or failure with detailed error messages</returns>
    public VariableValidationResult ValidateValue(object? value)
    {
        var result = new VariableValidationResult { IsValid = true };

        // Check required constraint
        if (IsRequired && (value == null || string.IsNullOrEmpty(value.ToString())))
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? $"Variable '{DisplayName}' is required.";
            return result;
        }

        // If value is null/empty and not required, it's valid
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return result;
        }

        var stringValue = value.ToString()!;

        // Type-specific validation
        return DataType switch
        {
            VariableType.String => ValidateStringValue(stringValue),
            VariableType.Integer => ValidateIntegerValue(stringValue),
            VariableType.Decimal => ValidateDecimalValue(stringValue),
            VariableType.Boolean => ValidateBooleanValue(stringValue),
            VariableType.Date => ValidateDateValue(stringValue),
            VariableType.Email => ValidateEmailValue(stringValue),
            VariableType.Url => ValidateUrlValue(stringValue),
            VariableType.Json => ValidateJsonValue(stringValue),
            _ => ValidateStringValue(stringValue),
        };
    }

    /// <summary>
    /// Validates a string value against string-specific constraints.
    /// </summary>
    private VariableValidationResult ValidateStringValue(string value)
    {
        var result = new VariableValidationResult { IsValid = true };

        // Length validation
        if (MinLength.HasValue && value.Length < MinLength.Value)
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? $"Value must be at least {MinLength} characters long.";
            return result;
        }

        if (MaxLength.HasValue && value.Length > MaxLength.Value)
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? $"Value must not exceed {MaxLength} characters.";
            return result;
        }

        // Pattern validation
        if (!string.IsNullOrEmpty(ValidationPattern))
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, ValidationPattern))
            {
                result.IsValid = false;
                result.ErrorMessage = ValidationErrorMessage ?? "Value does not match the required pattern.";
                return result;
            }
        }

        // Allowed values validation
        var allowedValues = GetAllowedValues();
        if (allowedValues.Count != 0 && !allowedValues.Contains(value))
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? $"Value must be one of: {string.Join(", ", allowedValues)}";
            return result;
        }

        return result;
    }

    /// <summary>
    /// Validates an integer value against numeric constraints.
    /// </summary>
    private VariableValidationResult ValidateIntegerValue(string value)
    {
        var result = new VariableValidationResult { IsValid = true };

        if (!int.TryParse(value, out var intValue))
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? "Value must be a valid integer.";
            return result;
        }

        if (MinValue.HasValue && intValue < MinValue.Value)
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? $"Value must be at least {MinValue}.";
            return result;
        }

        if (MaxValue.HasValue && intValue > MaxValue.Value)
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? $"Value must not exceed {MaxValue}.";
            return result;
        }

        return result;
    }

    /// <summary>
    /// Validates a decimal value against numeric constraints.
    /// </summary>
    private VariableValidationResult ValidateDecimalValue(string value)
    {
        var result = new VariableValidationResult { IsValid = true };

        if (!decimal.TryParse(value, out var decimalValue))
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? "Value must be a valid decimal number.";
            return result;
        }

        if (MinValue.HasValue && decimalValue < MinValue.Value)
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? $"Value must be at least {MinValue}.";
            return result;
        }

        if (MaxValue.HasValue && decimalValue > MaxValue.Value)
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? $"Value must not exceed {MaxValue}.";
            return result;
        }

        return result;
    }
    /// <summary>
    /// Validates a boolean value.
    /// </summary>
    private VariableValidationResult ValidateBooleanValue(string value)
    {
        string[] sourceArray = ["0", "1", "yes", "no", "y", "n"];
        
        var result = new VariableValidationResult { IsValid = true };

        if (!bool.TryParse(value, out _) &&
            !sourceArray.Contains(value.ToLowerInvariant()))
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? "Value must be a valid boolean (true/false, yes/no, 1/0).";
        }

        return result;
    }

    /// <summary>
    /// Validates a date value.
    /// </summary>
    private VariableValidationResult ValidateDateValue(string value)
    {
        var result = new VariableValidationResult { IsValid = true };

        if (!DateTime.TryParse(value, out _))
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? "Value must be a valid date.";
        }

        return result;
    }

    /// <summary>
    /// Validates an email value.
    /// </summary>
    private VariableValidationResult ValidateEmailValue(string value)
    {
        var result = new VariableValidationResult { IsValid = true };

        var emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        if (!System.Text.RegularExpressions.Regex.IsMatch(value, emailPattern))
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? "Value must be a valid email address.";
        }

        return result;
    }

    /// <summary>
    /// Validates a URL value.
    /// </summary>
    private VariableValidationResult ValidateUrlValue(string value)
    {
        var result = new VariableValidationResult { IsValid = true };

        if (!Uri.TryCreate(value, UriKind.Absolute, out _))
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? "Value must be a valid URL.";
        }

        return result;
    }

    /// <summary>
    /// Validates a JSON value.
    /// </summary>
    private VariableValidationResult ValidateJsonValue(string value)
    {
        var result = new VariableValidationResult { IsValid = true };

        try
        {
            System.Text.Json.JsonDocument.Parse(value);
        }
        catch
        {
            result.IsValid = false;
            result.ErrorMessage = ValidationErrorMessage ?? "Value must be valid JSON.";
        }

        return result;
    }

    /// <summary>
    /// Returns a string representation of the variable for debugging and logging.
    /// </summary>
    public override string ToString()
    {
        return $"Variable: {Name} ({DataType}) - {(IsRequired ? "Required" : "Optional")} - {(IsActive ? "Active" : "Inactive")}";
    }
}
