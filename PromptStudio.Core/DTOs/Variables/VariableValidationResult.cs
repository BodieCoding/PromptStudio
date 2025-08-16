namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Result of variable validation operations with detailed success status and error reporting.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by variable management services to provide structured validation results for template processing
/// and execution workflows. Enables comprehensive error reporting, constraint validation, and business rule
/// enforcement with detailed feedback for template developers and execution systems.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains validation status, detailed error information, warning messages, and supplementary validation
/// metadata. Supports both automated validation workflows and developer feedback with actionable error
/// messages and validation guidance for variable definition and usage scenarios.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>Variable Management Service - Variable constraint and type validation</item>
///   <item>Template Processing Service - Template variable compatibility validation</item>
///   <item>Execution Service - Pre-execution variable validation and readiness checks</item>
///   <item>Development Tools - Real-time validation feedback and error reporting</item>
///   <item>Quality Assurance - Template quality analysis and validation assessment</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Validation status enables quick success/failure determination</item>
///   <item>Error details provide actionable feedback for issue resolution</item>
///   <item>Warning messages highlight non-blocking issues and recommendations</item>
///   <item>Validation metadata supports advanced validation reporting and analytics</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Template validation workflows and development assistance</item>
///   <item>Pre-execution validation checks and readiness assessment</item>
///   <item>Real-time validation feedback in template development interfaces</item>
///   <item>Batch validation reporting for template quality assurance</item>
///   <item>Variable constraint enforcement and business rule validation</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Error collection size depends on template complexity and validation scope</item>
///   <item>Validation metadata should be used sparingly to maintain efficient payloads</item>
///   <item>Consider pagination for very large validation result collections</item>
///   <item>Optimize error message generation for frequently validated templates</item>
/// </list>
/// </remarks>
public class VariableValidationResult
{
    /// <summary>
    /// Gets or sets a value indicating whether all variable validation checks passed successfully.
    /// </summary>
    /// <value>
    /// <c>true</c> if all variables are valid and meet all constraints; otherwise, <c>false</c>.
    /// </value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the collection of validation errors that prevent template execution.
    /// </summary>
    /// <value>
    /// A list of detailed error messages describing validation failures and constraint violations.
    /// Empty if no errors are present.
    /// </value>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of validation warnings that don't prevent execution but indicate potential issues.
    /// </summary>
    /// <value>
    /// A list of warning messages highlighting non-critical issues and recommendations.
    /// Empty if no warnings are present.
    /// </value>
    public List<string> Warnings { get; set; } = [];

    /// <summary>
    /// Gets or sets a dictionary of variable-specific validation results and metadata.
    /// </summary>
    /// <value>
    /// A dictionary mapping variable names to their individual validation results and constraint information.
    /// Useful for providing targeted feedback on specific variable issues.
    /// </value>
    public Dictionary<string, object> VariableResults { get; set; } = [];

    /// <summary>
    /// Gets or sets additional metadata about the validation process and results.
    /// </summary>
    /// <value>
    /// A dictionary containing validation metadata such as processing time, rule counts, and validation context.
    /// Used for analytics, debugging, and validation process optimization.
    /// </value>
    public Dictionary<string, object> ValidationMetadata { get; set; } = [];
}
