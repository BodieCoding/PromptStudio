namespace PromptStudio.Core.DTOs.Templates;

/// <summary>
/// Comprehensive analysis results for prompt template optimization and quality assessment with actionable insights.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by template analysis services to return structured assessment results including complexity metrics,
/// optimization recommendations, and quality evaluation. Enables systematic template improvement workflows,
/// automated quality gates, and optimization decision support for template development processes.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Provides quantitative complexity metrics, qualitative assessment indicators, actionable optimization
/// suggestions, and quality issue identification. Supports both automated analysis workflows and human
/// review processes with comprehensive template health evaluation.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Template optimization services use this for improvement recommendations
/// - Quality assurance services evaluate templates against quality thresholds
/// - Development workflows use this for automated template validation gates
/// - Analytics services track template quality trends and optimization impacts
/// - Code review processes incorporate these insights for template approval
/// 
/// <para><strong>Analysis Interpretation:</strong></para>
/// ComplexityScore ranges typically 0.0-1.0 (higher = more complex)
/// OptimizationSuggestions should be prioritized by potential impact
/// QualityIssues should be addressed before production deployment
/// 
/// <para><strong>Decision Support:</strong></para>
/// Service layers can establish quality gates based on complexity thresholds
/// Optimization suggestions enable systematic template improvement planning
/// Quality issues provide actionable feedback for template refinement
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage
/// var analysis = await templateAnalysisService.AnalyzeAsync(templateId);
/// if (analysis.ComplexityScore &gt; 0.8 || analysis.QualityIssues.Any()) {
///     await reviewService.RequestHumanReviewAsync(templateId, analysis);
/// }
/// await optimizationService.ApplySuggestionsAsync(templateId, analysis.OptimizationSuggestions);
/// </code>
/// </example>
public class TemplateAnalysisResult
{
    /// <summary>
    /// Quantitative complexity score indicating template structural and logical complexity.
    /// Typically ranges 0.0-1.0 where higher values indicate more complex templates requiring
    /// additional review, testing, and optimization attention from development teams.
    /// </summary>
    public double ComplexityScore { get; set; }
    
    /// <summary>
    /// Total count of variables defined in the template for parameterization assessment.
    /// Higher variable counts may indicate complexity or flexibility depending on usage patterns.
    /// Service layers can use this for template categorization and complexity analysis.
    /// </summary>
    public int VariableCount { get; set; }
    
    /// <summary>
    /// Estimated token count for the template content to support cost and performance planning.
    /// Includes base template tokens but excludes variable substitution impact.
    /// Used by execution services for resource allocation and cost estimation.
    /// </summary>
    public int TokenCount { get; set; }
    
    /// <summary>
    /// Actionable optimization suggestions for improving template performance and quality.
    /// Ordered by potential impact with specific recommendations for template enhancement.
    /// Service layers can prioritize implementation based on development resources and goals.
    /// </summary>
    public List<string> OptimizationSuggestions { get; set; } = [];
    
    /// <summary>
    /// Identified quality issues requiring attention before production deployment.
    /// Includes structural problems, content issues, and potential execution risks.
    /// Service layers should address these issues to ensure template reliability and compliance.
    /// </summary>
    public List<string> QualityIssues { get; set; } = [];
    
    /// <summary>
    /// Detailed analysis metrics and specialized indicators for advanced template assessment.
    /// Common metrics: readability_score, maintainability_index, error_potential, optimization_potential.
    /// Service layers can extract specialized metrics for advanced analytics and decision-making.
    /// </summary>
    public Dictionary<string, object> Metrics { get; set; } = [];
}
