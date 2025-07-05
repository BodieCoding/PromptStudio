namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the data types supported for variables within prompt templates, enabling type-safe template composition and validation.
/// 
/// <para><strong>Business Context:</strong></para>
/// Variable types ensure data integrity and provide appropriate user interfaces for template parameterization.
/// Type specification enables validation, format checking, and optimized user experiences when collecting
/// variable values for prompt execution, supporting both technical accuracy and user productivity.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Variable types correspond to specific validation rules, UI input components, and data serialization
/// formats within the template execution engine. Each type has associated constraints, formatting
/// requirements, and processing capabilities that ensure reliable template operation.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Type-safe template parameterization and validation
/// - Optimized user interfaces for different data types
/// - Robust error prevention and data quality assurance
/// - Flexible template composition with structured data support
/// </summary>
/// <remarks>
/// <para><strong>Type Categories:</strong></para>
/// - Simple Types: Text, Number, Boolean for basic data values
/// - Complex Types: LargeText for extended content, File for document processing
/// 
/// <para><strong>Validation Considerations:</strong></para>
/// - Text: Length constraints, format validation (email, URL, etc.)
/// - Number: Range validation, precision requirements, integer vs. decimal
/// - Boolean: True/false values with appropriate UI controls
/// - File: Size limits, format restrictions, security scanning
/// - LargeText: Content validation, encoding requirements, size limits
/// 
/// <para><strong>UI Integration:</strong></para>
/// Variable types determine appropriate input controls in template execution interfaces,
/// ensuring optimal user experience and data accuracy for different content types.
/// </remarks>
/// <example>
/// A customer service template might use: Text (customer name), Boolean (urgent flag),
/// LargeText (inquiry description), Number (priority score).
/// </example>
public enum VariableType
{
    /// <summary>
    /// Short text content suitable for names, titles, and brief descriptive content.
    /// Typically limited in length and displayed in single-line input controls
    /// with validation for basic text formatting and character constraints.
    /// </summary>
    Text = 0,
    
    /// <summary>
    /// Numeric values including integers and decimal numbers for calculations and metrics.
    /// Supports range validation, precision control, and mathematical operations
    /// within template processing and conditional logic evaluation.
    /// </summary>
    Number = 1,
    
    /// <summary>
    /// Boolean true/false values for flags, toggles, and binary decision variables.
    /// Rendered as checkboxes, toggle switches, or radio buttons in user interfaces
    /// and used for conditional template logic and flow control.
    /// </summary>
    Boolean = 2,
    
    /// <summary>
    /// File upload variable for document processing, image analysis, and content extraction.
    /// Supports file validation, format restrictions, size limits, and security scanning
    /// for safe integration with document processing and AI analysis workflows.
    /// </summary>
    File = 3,
    
    /// <summary>
    /// Extended text content for detailed descriptions, documentation, and large content blocks.
    /// Supports multi-line input with rich text capabilities, suitable for prompt content,
    /// detailed instructions, and substantial textual data processing requirements.
    /// </summary>
    LargeText = 4
}
