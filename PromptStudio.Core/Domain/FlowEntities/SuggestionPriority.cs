namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines priority levels for AI-generated workflow suggestions to enable effective triage and resource allocation.
/// 
/// <para><strong>Business Context:</strong></para>
/// Priority classification enables organizations to systematically address workflow improvements
/// based on business impact, urgency, and available resources. This ensures critical optimizations
/// are addressed first while maintaining visibility into all improvement opportunities across
/// the workflow ecosystem.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Priority levels integrate with notification systems, dashboard displays, and automated
/// workflow management tools to ensure appropriate attention and response times for different
/// classes of suggestions. The classification supports SLA-based suggestion handling.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Systematic prioritization of improvement opportunities
/// - Resource allocation aligned with business impact
/// - SLA-based suggestion response management
/// - Clear visibility into urgent optimization needs
/// </summary>
/// <remarks>
/// <para><strong>Priority Guidelines:</strong></para>
/// - Critical: Immediate attention required (security, compliance, major performance issues)
/// - High: Address within current sprint/cycle (significant impact on efficiency)
/// - Medium: Schedule for upcoming iteration (moderate improvements)
/// - Low: Consider for future optimization cycles (minor enhancements)
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// Priority should be assigned based on potential business impact, implementation effort,
/// and strategic alignment. AI systems may suggest initial priority based on analysis,
/// but human review should validate and adjust based on business context.
/// 
/// <para><strong>Integration Considerations:</strong></para>
/// - Notification urgency mapping to priority levels
/// - Dashboard sorting and filtering by priority
/// - Automated escalation for high-priority items
/// - SLA compliance tracking by priority category
/// </remarks>
/// <example>
/// A Security suggestion about potential data exposure would typically be Critical priority,
/// while a minor UI improvement suggestion might be Low priority.
/// </example>
public enum SuggestionPriority
{
    /// <summary>
    /// Low-impact suggestions that can be addressed in future optimization cycles.
    /// Typically includes minor improvements, UI enhancements, or convenience features
    /// that don't significantly impact business operations.
    /// </summary>
    Low = 0,
    
    /// <summary>
    /// Moderate-impact suggestions that should be scheduled for upcoming iterations.
    /// Includes efficiency improvements, moderate cost optimizations, and workflow
    /// enhancements that provide measurable but non-critical benefits.
    /// </summary>
    Medium = 1,
    
    /// <summary>
    /// High-impact suggestions that should be addressed within the current development cycle.
    /// Includes significant performance improvements, substantial cost savings, or quality
    /// enhancements that materially impact business operations.
    /// </summary>
    High = 2,
    
    /// <summary>
    /// Critical suggestions requiring immediate attention and resolution.
    /// Includes security vulnerabilities, compliance issues, major performance problems,
    /// or suggestions that prevent normal business operations.
    /// </summary>
    Critical = 3
}
