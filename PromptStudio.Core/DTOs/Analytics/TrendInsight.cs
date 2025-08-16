namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// AI-generated analytical insights from trend analysis with actionable recommendations for optimization.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by analytics services and AI recommendation engines to deliver intelligent insights derived
/// from trend analysis, pattern recognition, and predictive analytics. Enables automated optimization
/// recommendations, proactive issue identification, and strategic planning support for decision-making.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Provides structured insights including categorization, severity assessment, confidence scoring,
/// and actionable recommendations. Contains both analytical findings and strategic guidance derived
/// from comprehensive data analysis and machine learning algorithms.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Analytics services generate insights from trend data analysis
/// - Recommendation engines use this for optimization strategy delivery
/// - Alert services process high-severity insights for immediate attention
/// - Reporting services present insights in executive dashboards and summaries
/// - Decision support systems incorporate insights into strategic planning processes
/// 
/// <para><strong>Insight Categories:</strong></para>
/// Type field categorizes insights (performance, quality, usage, cost, security, etc.)
/// Severity indicates urgency and priority for organizational response
/// Confidence score helps prioritize insights based on analytical reliability
/// 
/// <para><strong>Actionability:</strong></para>
/// Recommendations provide specific, actionable steps for insight implementation
/// Data dictionary contains supporting metrics and evidence for insight validation
/// Service layers can prioritize actions based on severity and confidence scores
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for insight processing
/// var insights = await analyticsService.GenerateInsightsAsync(labId, timeRange);
/// var criticalInsights = insights.Where(i => i.Severity == "Critical" && i.Confidence > 0.8);
/// foreach (var insight in criticalInsights) {
///     await actionService.CreateActionItemsAsync(insight.Recommendations);
/// }
/// </code>
/// </example>
public class TrendInsight
{
    /// <summary>
    /// Category classification for the insight enabling systematic organization and processing.
    /// Common types: "Performance", "Quality", "Usage", "Cost", "Security", "Optimization".
    /// Service layers use this for insight routing, filtering, and specialized processing workflows.
    /// </summary>
    public string Type { get; set; } = string.Empty;
    
    /// <summary>
    /// Concise, human-readable title summarizing the key insight finding.
    /// Designed for dashboard headlines and executive summary presentation.
    /// Should clearly communicate the primary insight without requiring detailed context.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed explanation of the insight including context, analysis, and implications.
    /// Provides comprehensive understanding of the finding including supporting data,
    /// trends identified, and potential business impact for informed decision-making.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Severity classification indicating urgency and priority for organizational response.
    /// Common values: "Low", "Medium", "High", "Critical" for systematic prioritization.
    /// Service layers use this for alert routing, escalation procedures, and resource allocation.
    /// </summary>
    public string Severity { get; set; } = string.Empty;
    
    /// <summary>
    /// Confidence score (0.0-1.0) indicating analytical reliability and statistical significance.
    /// Higher values indicate more reliable insights based on stronger data evidence.
    /// Service layers can prioritize high-confidence insights for immediate action planning.
    /// </summary>
    public double Confidence { get; set; }
    
    /// <summary>
    /// Supporting data and metrics providing evidence and context for the insight.
    /// Contains quantitative analysis results, trend data, and statistical evidence.
    /// Service layers can extract specific metrics for validation and drill-down analysis.
    /// </summary>
    public Dictionary<string, object> Data { get; set; } = [];
    
    /// <summary>
    /// Actionable recommendations for addressing the insight and capturing optimization opportunities.
    /// Ordered by priority with specific, implementable steps for insight resolution.
    /// Service layers can convert these into action items and implementation workflows.
    /// </summary>
    public List<string> Recommendations { get; set; } = [];
}
