namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the categories of AI-powered suggestions for workflow optimization and enhancement.
/// 
/// <para><strong>Business Context:</strong></para>
/// AI assistants analyze workflow patterns, execution metrics, and industry best practices
/// to provide intelligent recommendations for improving workflow effectiveness, reducing costs,
/// and enhancing user experiences. These suggestion types enable systematic workflow optimization
/// across multiple dimensions of performance and quality.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Each suggestion type corresponds to specific analysis algorithms and recommendation engines
/// that evaluate different aspects of workflow performance. The categorization enables
/// targeted optimization strategies and allows users to prioritize improvements based on
/// business objectives and technical constraints.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Systematic workflow optimization across multiple dimensions
/// - AI-driven insights for continuous improvement
/// - Structured approach to performance enhancement
/// - Business alignment through categorized recommendations
/// </summary>
/// <remarks>
/// <para><strong>Usage Guidelines:</strong></para>
/// Suggestion types should be selected based on the specific analysis performed and the
/// primary benefit area. Multiple suggestions can target the same workflow with different
/// types to address various improvement opportunities.
/// 
/// <para><strong>Implementation Notes:</strong></para>
/// - Stored as string values in database for extensibility
/// - Used for filtering and categorizing suggestions in UI
/// - Enables metric tracking by suggestion category
/// - Supports custom suggestion type extensions
/// </remarks>
/// <example>
/// Performance suggestions might recommend caching frequently used templates,
/// while Security suggestions could identify potential data exposure risks in workflows.
/// </example>
public enum SuggestionType
{
    /// <summary>
    /// Suggestions focused on improving workflow execution speed, throughput, and resource efficiency.
    /// Includes recommendations for caching, parallel processing, and computational optimization.
    /// </summary>
    Performance = 0,
    
    /// <summary>
    /// Suggestions aimed at reducing operational costs through resource optimization,
    /// model selection, and efficient execution patterns.
    /// </summary>
    CostOptimization = 1,
    
    /// <summary>
    /// Suggestions for enhancing output quality, accuracy, and consistency.
    /// Includes prompt refinement, validation improvements, and quality assurance measures.
    /// </summary>
    QualityImprovement = 2,
    
    /// <summary>
    /// Suggestions for reducing error rates, improving fault tolerance,
    /// and implementing robust error handling mechanisms.
    /// </summary>
    ErrorReduction = 3,
    
    /// <summary>
    /// Suggestions focused on improving user interface, workflow usability,
    /// and overall user satisfaction with workflow interactions.
    /// </summary>
    UserExperience = 4,
    
    /// <summary>
    /// Suggestions for enhancing security posture, data protection,
    /// and compliance with security policies and regulations.
    /// </summary>
    Security = 5,
    
    /// <summary>
    /// Suggestions for improving workflow scalability, handling increased load,
    /// and supporting growing usage patterns.
    /// </summary>
    Scalability = 6,
    
    /// <summary>
    /// Suggestions for better system integration, API connectivity,
    /// and interoperability with external services and platforms.
    /// </summary>
    Integration = 7,
    
    /// <summary>
    /// Suggestions for increasing automation levels, reducing manual intervention,
    /// and streamlining workflow execution processes.
    /// </summary>
    Automation = 8,
    
    /// <summary>
    /// Suggestions based on industry best practices, established patterns,
    /// and proven approaches for workflow design and implementation.
    /// </summary>
    BestPractice = 9
}
