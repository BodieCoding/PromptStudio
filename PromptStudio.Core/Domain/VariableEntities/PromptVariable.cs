using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents a configurable variable that can be substituted in prompt templates for dynamic content generation.
/// 
/// <para><strong>Business Context:</strong></para>
/// <para>This entity enables dynamic prompt composition by defining placeholders that can be filled with different values
/// during execution. It supports enterprise requirements for template reusability, data validation, user experience
/// optimization, and systematic testing across different input scenarios in production LLMOps environments.</para>
/// 
/// <para><strong>Technical Context:</strong></para>
/// <para>The entity provides comprehensive variable definition capabilities including type safety, validation rules,
/// default values, and user interface configuration. It integrates with template processing engines to enable
/// secure and validated variable substitution while maintaining audit trails and performance optimization.</para>
/// 
/// <para><strong>Value Proposition:</strong></para>
/// <list type="bullet">
/// <item><description>Dynamic prompt composition with type-safe variable substitution</description></item>
/// <item><description>Enterprise-grade validation and security for variable inputs</description></item>
/// <item><description>Enhanced user experience with help text, examples, and smart ordering</description></item>
/// <item><description>Systematic testing support through example values and validation rules</description></item>
/// <item><description>Comprehensive audit trails for variable usage and modification history</description></item>
/// </list>
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Template Method Pattern: Defines variable placeholders for template processing</description></item>
/// <item><description>Strategy Pattern: Different variable types with type-specific validation logic</description></item>
/// <item><description>Builder Pattern: Supports incremental variable definition and configuration</description></item>
/// <item><description>Multi-tenancy: Inherits tenant isolation from AuditableEntity</description></item>
/// </list>
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// <list type="bullet">
/// <item><description>Variable validation should be cached for frequently used templates</description></item>
/// <item><description>JSON fields (ValidationRules, ExampleValues) may benefit from indexing</description></item>
/// <item><description>Sort order enables efficient UI rendering without additional sorting</description></item>
/// <item><description>Type-specific processing can optimize validation performance</description></item>
/// </list>
/// 
/// <para><strong>Integration Points:</strong></para>
/// <list type="bullet">
/// <item><description>Template Engine: Core variable substitution and processing</description></item>
/// <item><description>Validation Framework: Rule-based input validation and sanitization</description></item>
/// <item><description>User Interface: Dynamic form generation and user experience</description></item>
/// <item><description>Testing Framework: Example values for automated template testing</description></item>
/// <item><description>Analytics: Variable usage patterns and optimization insights</description></item>
/// </list>
/// 
/// <para><strong>Usage Examples:</strong></para>
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
/// </remarks>
public class PromptVariable : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique name of the variable used for template substitution.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Serves as the identifier for variable substitution in templates using {{variable_name}} syntax,
    /// enabling dynamic content generation and template reusability across different use cases
    /// and organizational scenarios in enterprise prompt management systems.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>Must be unique within the template scope and follow variable naming conventions.
    /// Used directly in template processing for variable substitution operations.</para>
    /// </summary>
    /// <value>
    /// A string representing the variable name (e.g., "user_name", "product_category").
    /// Cannot be null or empty. Maximum length is 50 characters.
    /// </value>
    /// <remarks>
    /// <para>Should follow naming conventions (lowercase with underscores recommended).
    /// Used directly in template substitution patterns like {{variable_name}}.</para>
    /// </remarks>
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets an optional description explaining the variable's purpose and usage.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Provides context and documentation for template users and developers,
    /// improving usability and reducing errors in enterprise environments where
    /// templates are shared across teams and organizational units.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>Used for documentation, user interface tooltips, and API documentation.
    /// Should clearly explain the variable's role in the template's functionality.</para>
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
    /// <para>Improves user experience and template reliability by providing sensible defaults,
    /// reducing input requirements and enabling template execution with minimal
    /// configuration in enterprise self-service scenarios.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>Used when no value is provided during template execution. Should be
    /// compatible with the variable's type and validation rules.</para>
    /// </summary>
    /// <value>
    /// A string representing the default value, or null if no default is specified.
    /// </value>
    /// <remarks>
    /// <para>Default value must comply with the variable's type and validation rules.
    /// Used to improve template usability and reduce required input fields.</para>
    /// </remarks>
    public string? DefaultValue { get; set; }
    
    /// <summary>
    /// Gets or sets the data type of this variable for validation and processing.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Ensures type safety and appropriate validation for variable inputs,
    /// preventing errors and improving data quality in enterprise environments
    /// with strict data governance and quality requirements.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>Determines validation logic, input controls, and processing behavior.
    /// Used by template engines for type-specific variable handling.</para>
    /// </summary>
    /// <value>
    /// A <see cref="VariableType"/> enum value indicating the variable's data type.
    /// Default is Text.
    /// </value>
    /// <remarks>
    /// <para>Determines validation logic and user interface rendering.
    /// Must be compatible with default value and validation rules.</para>
    /// </remarks>
    [Required]
    public VariableType Type { get; set; } = VariableType.String;
    
    /// <summary>
    /// Gets or sets the unique identifier of the prompt template that owns this variable.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Links the variable to its parent template, enabling template-specific
    /// variable management and ensuring proper scope and organization
    /// in enterprise prompt library management systems.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>Foreign key relationship with PromptTemplate entity. Required for all variables.
    /// Used for template-variable relationship management and validation.</para>
    /// </summary>
    /// <value>
    /// A <see cref="Guid"/> representing the owning template's unique identifier.
    /// </value>
    public Guid PromptTemplateId { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether this variable must have a value provided.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Enforces data completeness requirements and prevents template execution errors
    /// by ensuring critical variables are populated, supporting enterprise data quality
    /// and process reliability requirements.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>Required variables must have a value (either provided or default) before
    /// template execution. Used for validation during template processing.</para>
    /// </summary>
    /// <value>
    /// <c>true</c> if the variable must have a value; otherwise, <c>false</c>.
    /// Default is <c>true</c>.
    /// </value>
    /// <remarks>
    /// <para>Required variables must have either a provided value or a valid default value.
    /// Used for template validation and user interface generation.</para>
    /// </remarks>
    public bool IsRequired { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the validation rules for this variable in JSON format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Ensures data quality and security by validating variable inputs against
    /// business rules and security constraints, preventing invalid data from
    /// affecting template execution in enterprise production environments.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>JSON object containing validation constraints specific to the variable type.
    /// Examples include length limits, format patterns, allowed values, and ranges.</para>
    /// </summary>
    /// <value>
    /// A JSON string containing validation rules, or null for no specific validation.
    /// Maximum length is 1000 characters.
    /// </value>
    /// <example>
    /// {"minLength": 3, "maxLength": 50, "pattern": "^[a-zA-Z0-9]+$", "required": true}
    /// </example>
    /// <remarks>
    /// <para>Validation rules are enforced during template execution and user input.
    /// Should be compatible with the variable's data type.</para>
    /// </remarks>
    [StringLength(1000)]
    public string? ValidationRules { get; set; }
    
    /// <summary>
    /// Gets or sets the display order for this variable in user interfaces.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Controls the presentation order of variables in forms and interfaces,
    /// enabling optimized user experience and logical input flow in enterprise
    /// self-service template execution environments.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>Used for sorting variables in user interfaces. Lower numbers appear first.
    /// Enables logical grouping and flow of variable input forms.</para>
    /// </summary>
    /// <value>
    /// An integer representing the display order. Default is 0.
    /// </value>
    /// <remarks>
    /// <para>Used for UI sorting and logical variable grouping.
    /// Variables with the same sort order may be sorted by name as secondary criteria.</para>
    /// </remarks>
    public int SortOrder { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets helpful guidance text for users filling out this variable.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Improves user experience and reduces input errors by providing clear
    /// guidance and examples for variable input, supporting enterprise self-service
    /// scenarios where users may not be familiar with template requirements.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>Displayed in user interfaces as help text, tooltips, or placeholder text.
    /// Should provide clear, actionable guidance for variable input.</para>
    /// </summary>
    /// <value>
    /// A string containing user guidance text, or null if no help text is provided.
    /// Maximum length is 500 characters.
    /// </value>
    /// <example>
    /// "Enter the customer's full name as it appears in the CRM system"
    /// </example>
    /// <remarks>
    /// <para>Displayed in user interfaces to guide variable input.
    /// Should be clear, concise, and actionable.</para>
    /// </remarks>
    [StringLength(500)]
    public string? HelpText { get; set; }
    
    /// <summary>
    /// Gets or sets example values for this variable in JSON array format.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Provides concrete examples to guide user input and enables automated testing
    /// with realistic data, supporting enterprise quality assurance and user
    /// experience optimization in template-based workflows.</para>
    /// 
    /// <para><strong>Technical Details:</strong></para>
    /// <para>JSON array of example values that comply with the variable's type and
    /// validation rules. Used for testing and user guidance.</para>
    /// </summary>
    /// <value>
    /// A JSON array of example values, or null if no examples are provided.
    /// Maximum length is 1000 characters.
    /// </value>
    /// <example>
    /// ["John Smith", "Jane Doe", "Robert Johnson"]
    /// </example>
    /// <remarks>
    /// <para>Examples should comply with validation rules and represent realistic use cases.
    /// Used for user guidance and automated testing scenarios.</para>
    /// </remarks>
    [StringLength(1000)]
    public string? ExampleValues { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property to the owning prompt template.
    /// 
    /// <para><strong>Business Value:</strong></para>
    /// <para>Provides access to template context and metadata for variable
    /// processing and validation without requiring separate database queries.</para>
    /// </summary>
    /// <value>
    /// A <see cref="PromptTemplate"/> instance representing the owning template.
    /// </value>
    public virtual PromptTemplate PromptTemplate { get; set; } = null!;
}
