using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a configurable variable that can be substituted in prompt templates for dynamic content generation.
/// 
/// <para><strong>Business Context:</strong></para>
/// This entity enables dynamic prompt composition by defining placeholders that can be filled with different values
/// during execution. It supports enterprise requirements for template reusability, data validation, user experience
/// optimization, and systematic testing across different input scenarios in production LLMOps environments.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity provides comprehensive variable definition capabilities including type safety, validation rules,
/// default values, and user interface configuration. It integrates with template processing engines to enable
/// secure and validated variable substitution while maintaining audit trails and performance optimization.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Dynamic prompt composition with type-safe variable substitution
/// - Enterprise-grade validation and security for variable inputs
/// - Enhanced user experience with help text, examples, and smart ordering
/// - Systematic testing support through example values and validation rules
/// - Comprehensive audit trails for variable usage and modification history
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Template Method Pattern: Defines variable placeholders for template processing
/// - Strategy Pattern: Different variable types with type-specific validation logic
/// - Builder Pattern: Supports incremental variable definition and configuration
/// - Multi-tenancy: Inherits tenant isolation from AuditableEntity
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// - Variable validation should be cached for frequently used templates
/// - JSON fields (ValidationRules, ExampleValues) may benefit from indexing
/// - Sort order enables efficient UI rendering without additional sorting
/// - Type-specific processing can optimize validation performance
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Template Engine: Core variable substitution and processing
/// - Validation Framework: Rule-based input validation and sanitization
/// - User Interface: Dynamic form generation and user experience
/// - Testing Framework: Example values for automated template testing
/// - Analytics: Variable usage patterns and optimization insights
/// </remarks>
/// <example>
/// <code>
/// // Creating a required text variable with validation
/// var variable = new PromptVariable
/// {
///     Name = "user_name",
///     Description = "The name of the user for personalized responses",
///     Type = VariableType.Text,
///     IsRequired = true,
///     DefaultValue = null,
///     ValidationRules = JsonSerializer.Serialize(new 
///     { 
///         minLength = 2, 
///         maxLength = 50, 
///         pattern = "^[a-zA-Z\\s]+$" 
///     }),
///     HelpText = "Enter the user's full name (letters and spaces only)",
///     ExampleValues = JsonSerializer.Serialize(new[] { "John Doe", "Jane Smith" }),
///     SortOrder = 1,
///     PromptTemplateId = templateId,
///     TenantId = currentTenantId
/// };
/// 
/// // Creating a numeric variable with range validation
/// var ageVariable = new PromptVariable
/// {
///     Name = "age",
///     Description = "User's age for age-appropriate content",
///     Type = VariableType.Number,
///     IsRequired = false,
///     DefaultValue = "25",
///     ValidationRules = JsonSerializer.Serialize(new { min = 13, max = 120 }),
///     HelpText = "Enter age (13-120). Leave blank if not applicable.",
///     ExampleValues = JsonSerializer.Serialize(new[] { 25, 35, 45 }),
///     SortOrder = 2,
///     PromptTemplateId = templateId,
///     TenantId = currentTenantId
/// };
/// </code>
/// </example>
public class PromptVariable : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique name of the variable used for template substitution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Serves as the identifier for variable substitution in templates using {{variable_name}} syntax,
    /// enabling dynamic content generation and template reusability across different use cases
    /// and organizational scenarios in enterprise prompt management systems.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Must be unique within the template scope and follow variable naming conventions.
    /// Used directly in template processing for variable substitution operations.
    /// </summary>
    /// <value>
    /// A string representing the variable name (e.g., "user_name", "product_category").
    /// Cannot be null or empty. Maximum length is 50 characters.
    /// </value>
    /// <remarks>
    /// Should follow naming conventions (lowercase with underscores recommended).
    /// Used directly in template substitution patterns like {{variable_name}}.
    /// </remarks>
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional description explaining the variable's purpose and usage.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides context and documentation for template users and developers,
    /// improving usability and reducing errors in enterprise environments where
    /// templates are shared across teams and organizational units.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Used for documentation, user interface tooltips, and API documentation.
    /// Should clearly explain the variable's role in the template's functionality.
    /// </summary>
    /// <value>
    /// A descriptive explanation of the variable's purpose.
    /// Optional field with maximum length of 200 characters.
    /// </value>
    [StringLength(200)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the default value for this variable when no input is provided.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Improves user experience and template reliability by providing sensible defaults,
    /// reducing input requirements and enabling template execution with minimal
    /// configuration in enterprise self-service scenarios.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Used when no value is provided during template execution. Should be
    /// compatible with the variable's type and validation rules.
    /// </summary>
    /// <value>
    /// A string representing the default value, or null if no default is specified.
    /// </value>
    /// <remarks>
    /// Default value must comply with the variable's type and validation rules.
    /// Used to improve template usability and reduce required input fields.
    /// </remarks>
    public string? DefaultValue { get; set; }
    
    /// <summary>
    /// Gets or sets the data type of this variable for validation and processing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Ensures type safety and appropriate validation for variable inputs,
    /// preventing errors and improving data quality in enterprise environments
    /// with strict data governance and quality requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Determines validation logic, input controls, and processing behavior.
    /// Used by template engines for type-specific variable handling.
    /// </summary>
    /// <value>
    /// A <see cref="VariableType"/> enum value indicating the variable's data type.
    /// Default is Text.
    /// </value>
    /// <remarks>
    /// Determines validation logic and user interface rendering.
    /// Must be compatible with default value and validation rules.
    /// </remarks>
    [Required]
    public VariableType Type { get; set; } = VariableType.Text;
    
    /// <summary>
    /// Gets or sets the unique identifier of the prompt template that owns this variable.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Links the variable to its parent template, enabling template-specific
    /// variable management and ensuring proper scope and organization
    /// in enterprise prompt library management systems.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Foreign key relationship with PromptTemplate entity. Required for all variables.
    /// Used for template-variable relationship management and validation.
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the owning template's unique identifier.
    /// </value>
    public Guid PromptTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether this variable must have a value provided.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Enforces data completeness requirements and prevents template execution errors
    /// by ensuring critical variables are populated, supporting enterprise data quality
    /// and process reliability requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Required variables must have a value (either provided or default) before
    /// template execution. Used for validation during template processing.
    /// </summary>
    /// <value>
    /// <c>true</c> if the variable must have a value; otherwise, <c>false</c>.
    /// Default is <c>true</c>.
    /// </value>
    /// <remarks>
    /// Required variables must have either a provided value or a valid default value.
    /// Used for template validation and user interface generation.
    /// </remarks>
    public bool IsRequired { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the validation rules for this variable in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Ensures data quality and security by validating variable inputs against
    /// business rules and security constraints, preventing invalid data from
    /// affecting template execution in enterprise production environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON object containing validation constraints specific to the variable type.
    /// Examples include length limits, format patterns, allowed values, and ranges.
    /// </summary>
    /// <value>
    /// A JSON string containing validation rules, or null for no specific validation.
    /// Maximum length is 1000 characters.
    /// </value>
    /// <example>
    /// {"minLength": 3, "maxLength": 50, "pattern": "^[a-zA-Z0-9]+$", "required": true}
    /// </example>
    /// <remarks>
    /// Validation rules are enforced during template execution and user input.
    /// Should be compatible with the variable's data type.
    /// </remarks>
    [StringLength(1000)]
    public string? ValidationRules { get; set; }
    
    /// <summary>
    /// Gets or sets the display order for this variable in user interfaces.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Controls the presentation order of variables in forms and interfaces,
    /// enabling optimized user experience and logical input flow in enterprise
    /// self-service template execution environments.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Used for sorting variables in user interfaces. Lower numbers appear first.
    /// Enables logical grouping and flow of variable input forms.
    /// </summary>
    /// <value>
    /// An integer representing the display order. Default is 0.
    /// </value>
    /// <remarks>
    /// Used for UI sorting and logical variable grouping.
    /// Variables with the same sort order may be sorted by name as secondary criteria.
    /// </remarks>
    public int SortOrder { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets helpful guidance text for users filling out this variable.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Improves user experience and reduces input errors by providing clear
    /// guidance and examples for variable input, supporting enterprise self-service
    /// scenarios where users may not be familiar with template requirements.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// Displayed in user interfaces as help text, tooltips, or placeholder text.
    /// Should provide clear, actionable guidance for variable input.
    /// </summary>
    /// <value>
    /// A string containing user guidance text, or null if no help text is provided.
    /// Maximum length is 500 characters.
    /// </value>
    /// <example>
    /// "Enter the customer's full name as it appears in the CRM system"
    /// </example>
    /// <remarks>
    /// Displayed in user interfaces to guide variable input.
    /// Should be clear, concise, and actionable.
    /// </remarks>
    [StringLength(500)]
    public string? HelpText { get; set; }
    
    /// <summary>
    /// Gets or sets example values for this variable in JSON array format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides concrete examples to guide user input and enables automated testing
    /// with realistic data, supporting enterprise quality assurance and user
    /// experience optimization in template-based workflows.
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// JSON array of example values that comply with the variable's type and
    /// validation rules. Used for testing and user guidance.
    /// </summary>
    /// <value>
    /// A JSON array of example values, or null if no examples are provided.
    /// Maximum length is 1000 characters.
    /// </value>
    /// <example>
    /// ["John Smith", "Jane Doe", "Robert Johnson"]
    /// </example>
    /// <remarks>
    /// Examples should comply with validation rules and represent realistic use cases.
    /// Used for user guidance and automated testing scenarios.
    /// </remarks>
    [StringLength(1000)]
    public string? ExampleValues { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the owning prompt template.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// Provides access to template context and metadata for variable
    /// processing and validation without requiring separate database queries.
    /// </summary>
    /// <value>
    /// A <see cref="PromptTemplate"/> instance representing the owning template.
    /// </value>
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
}
