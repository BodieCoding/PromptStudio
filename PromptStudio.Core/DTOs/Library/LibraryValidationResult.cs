namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Comprehensive validation result for library content integrity, compatibility, and quality assessment.
/// Provides detailed analysis of library structure, template validity, dependencies, and overall
/// quality metrics to ensure successful deployment and operation in target environments.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <list type="bullet">
///   <item>Validation Service - Core validation logic and rule execution</item>
///   <item>Quality Assurance Service - Quality scoring and metric calculation</item>
///   <item>Import Service - Pre-import validation and compatibility checks</item>
///   <item>Analytics Service - Quality trend analysis and performance tracking</item>
///   <item>Compliance Service - Regulatory and policy compliance verification</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Boolean validation status for immediate go/no-go decisions</item>
///   <item>Structured error and warning collections for detailed diagnostics</item>
///   <item>Numeric quality score (0.0-1.0) for comparative analysis</item>
///   <item>Extensible validation details for custom validation rules</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Pre-deployment validation gates in CI/CD pipelines</item>
///   <item>Library quality assessment and improvement tracking</item>
///   <item>Import compatibility verification and risk assessment</item>
///   <item>Compliance auditing and regulatory reporting</item>
///   <item>Performance optimization based on quality metrics</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Quality score calculation may be computationally intensive</item>
///   <item>Validation details size should be monitored for large libraries</item>
///   <item>Error/warning collections should be bounded for memory efficiency</item>
///   <item>Consider caching validation results for frequently accessed libraries</item>
/// </list>
/// </remarks>
public class LibraryValidationResult
{
    /// <summary>
    /// Indicates whether the library passed all critical validation checks.
    /// True when library meets minimum requirements for deployment and operation.
    /// False when critical issues prevent safe library usage or deployment.
    /// Service layers should block operations when validation fails.
    /// </summary>
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Collection of critical error messages that caused validation failure.
    /// Contains actionable information for resolving validation issues.
    /// Includes schema violations, dependency conflicts, and security concerns.
    /// Must be addressed before library can be successfully deployed or imported.
    /// </summary>
    public List<string> Errors { get; set; } = [];
    
    /// <summary>
    /// Collection of warning messages for non-critical validation issues.
    /// Contains recommendations for improving library quality and performance.
    /// Does not prevent deployment but may impact functionality or performance.
    /// Useful for continuous improvement and best practice adherence.
    /// </summary>
    public List<string> Warnings { get; set; } = [];
    
    /// <summary>
    /// Normalized quality score ranging from 0.0 (lowest) to 1.0 (highest quality).
    /// Calculated based on template quality, documentation completeness, and best practices.
    /// Used for comparative analysis, quality trending, and improvement tracking.
    /// Scores below 0.7 typically indicate significant quality concerns requiring attention.
    /// </summary>
    public double QualityScore { get; set; }
    
    /// <summary>
    /// Detailed validation analysis results and custom validation rule outcomes.
    /// Contains rule-specific results, performance metrics, and diagnostic information.
    /// Supports extensible validation frameworks and custom compliance requirements.
    /// Used by specialized validation services for in-depth analysis and reporting.
    /// </summary>
    public Dictionary<string, object> ValidationDetails { get; set; } = [];
}
