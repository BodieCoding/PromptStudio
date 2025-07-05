namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the outcome states of template and workflow execution operations for monitoring and analysis.
/// 
/// <para><strong>Business Context:</strong></para>
/// Execution status provides critical operational visibility for AI system performance monitoring,
/// SLA compliance tracking, and business process reliability assessment. Status classification enables
/// systematic analysis of execution patterns, failure modes, and performance optimization opportunities.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Execution status integrates with monitoring systems, alerting frameworks, and analytics platforms
/// to provide comprehensive operational observability. Each status has specific logging requirements,
/// escalation procedures, and recovery mechanisms that support enterprise operational excellence.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Comprehensive execution monitoring and operational visibility
/// - SLA compliance tracking and performance analysis
/// - Systematic failure analysis and recovery planning
/// - Business process reliability and optimization insights
/// </summary>
/// <remarks>
/// <para><strong>Status Categories:</strong></para>
/// - Success States: Success, PartialSuccess for various completion levels
/// - Failure States: Failed, Timeout, Cancelled for different failure modes
/// 
/// <para><strong>Operational Response:</strong></para>
/// - Success: Normal operation, metrics collection
/// - PartialSuccess: Investigation, potential optimization
/// - Failed: Error analysis, recovery procedures
/// - Timeout: Performance investigation, resource scaling
/// - Cancelled: User experience analysis, workflow optimization
/// 
/// <para><strong>Analytics Integration:</strong></para>
/// Status distribution analysis provides insights into system health,
/// user experience quality, and optimization opportunities for AI operations.
/// </remarks>
/// <example>
/// A document processing template might show 95% Success, 3% Timeout (for large files),
/// and 2% Failed (for unsupported formats), indicating optimization opportunities.
/// </example>
public enum ExecutionStatus
{
    /// <summary>
    /// Complete successful execution with all operations completed as expected.
    /// Indicates optimal system performance with full objective achievement
    /// and normal operational metrics within acceptable parameters.
    /// </summary>
    Success = 0,
    
    /// <summary>
    /// Execution failure due to errors, exceptions, or unrecoverable conditions.
    /// Requires error analysis, troubleshooting, and potential recovery actions
    /// to restore normal operation and prevent similar failures.
    /// </summary>
    Failed = 1,
    
    /// <summary>
    /// Execution terminated due to exceeding maximum allowed processing time.
    /// Indicates potential performance issues, resource constraints, or optimization
    /// opportunities requiring investigation and system tuning.
    /// </summary>
    Timeout = 2,
    
    /// <summary>
    /// Execution terminated by user request or system intervention before completion.
    /// May indicate user experience issues, workflow problems, or operational
    /// changes requiring analysis and potential process improvements.
    /// </summary>
    Cancelled = 3,
    
    /// <summary>
    /// Partial completion with some objectives achieved but others incomplete or failed.
    /// Provides nuanced execution analysis for complex workflows where partial results
    /// may still provide business value despite overall execution incompletion.
    /// </summary>
    PartialSuccess = 4
}
