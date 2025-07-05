namespace PromptStudio.Core.DTOs.Templates;

/// <summary>
/// Comprehensive comparison analysis between two template versions with detailed difference identification and impact assessment.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by template versioning services to provide structured comparison results for version control workflows,
/// change impact analysis, and template evolution tracking. Enables systematic review of template modifications,
/// automated change detection, and impact assessment for template management and deployment processes.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Contains version identification, structured difference analysis, variable change tracking, and metric
/// evolution assessment. Supports both automated comparison workflows and human review processes with
/// comprehensive template change visualization and impact analysis capabilities.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// <list type="bullet">
///   <item>Version Control Service - Template change tracking and evolution analysis</item>
///   <item>Review Service - Change impact assessment and approval workflows</item>
///   <item>Deployment Service - Pre-deployment change validation and risk assessment</item>
///   <item>Analytics Service - Template evolution pattern analysis and optimization insights</item>
///   <item>Audit Service - Change documentation and compliance tracking</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Version identifiers enable precise comparison reference and tracking</item>
///   <item>Content differences provide line-by-line change analysis</item>
///   <item>Variable changes track parameter modifications and impacts</item>
///   <item>Metric changes quantify performance and quality implications</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Template version approval workflows and change management</item>
///   <item>Impact analysis for template updates and optimizations</item>
///   <item>Automated change detection and notification systems</item>
///   <item>Template evolution tracking and development analytics</item>
///   <item>Rollback decision support and change risk assessment</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Content differences collection size depends on template complexity</item>
///   <item>Variable change analysis may be computationally intensive for large templates</item>
///   <item>Metric comparison calculations should be cached for repeated access</item>
///   <item>Consider pagination for very large difference collections</item>
/// </list>
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for template version comparison
/// var comparison = await templateService.CompareVersionsAsync(templateId, version1, version2);
/// if (comparison.ContentDifferences.Count &gt; 10 || comparison.VariableChanges.Any()) {
///     await reviewService.RequestChangeReviewAsync(templateId, comparison);
/// }
/// await deploymentService.AssessChangeImpactAsync(comparison.MetricChanges);
/// </code>
/// </example>
public class TemplateComparisonResult
{
    /// <summary>
    /// Identifier or version label for the first template version in the comparison.
    /// Provides reference point for change analysis and enables precise version tracking.
    /// Used for audit trails, rollback operations, and change impact attribution.
    /// </summary>
    public string Version1 { get; set; } = string.Empty;

    /// <summary>
    /// Identifier or version label for the second template version in the comparison.
    /// Represents the target or newer version for change analysis and evolution tracking.
    /// Critical for understanding change direction and impact assessment workflows.
    /// </summary>
    public string Version2 { get; set; } = string.Empty;

    /// <summary>
    /// Collection of specific content differences identified between the template versions.
    /// Contains line-by-line change descriptions, additions, deletions, and modifications.
    /// Used for detailed change review, impact analysis, and change approval workflows.
    /// Essential for understanding the scope and nature of template modifications.
    /// </summary>
    public List<string> ContentDifferences { get; set; } = new();

    /// <summary>
    /// Collection of variable definition changes including additions, removals, and modifications.
    /// Tracks parameter changes that may impact template execution and integration compatibility.
    /// Critical for API compatibility analysis and downstream system impact assessment.
    /// Used for breaking change detection and migration planning workflows.
    /// </summary>
    public List<string> VariableChanges { get; set; } = new();

    /// <summary>
    /// Dictionary of quantitative metric changes between template versions including performance and quality indicators.
    /// Contains before/after metric comparisons for performance analysis and optimization tracking.
    /// Keys represent metric names, values contain comparison data or delta calculations.
    /// Used for performance regression detection and optimization impact measurement.
    /// </summary>
    public Dictionary<string, object> MetricChanges { get; set; } = new();
}
