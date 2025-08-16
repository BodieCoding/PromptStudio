namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Standardized response payload from AI model execution operations with comprehensive execution metadata.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by model execution services to return structured results including success status, generated content,
/// error information, and execution metrics. Enables consistent response handling across different model
/// providers while supporting analytics, billing, and performance monitoring requirements.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Provides complete execution outcome including success indication, content payload, error details,
/// execution metadata, performance metrics, and resource usage information for comprehensive
/// service layer processing and client feedback.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Model execution services populate this with provider responses
/// - Analytics services consume execution metrics for performance analysis
/// - Billing services use token usage for cost calculation and allocation
/// - Error handling services process failure information for troubleshooting
/// - Audit services log complete execution outcomes for compliance
/// 
/// <para><strong>Error Handling Patterns:</strong></para>
/// Success=false should include ErrorMessage with actionable information
/// Content should be null for failed executions to prevent invalid data usage
/// Metadata can contain provider-specific error codes and diagnostic information
/// 
/// <para><strong>Performance Metrics:</strong></para>
/// ExecutionTimeMs enables SLA monitoring and performance optimization
/// TokensUsed supports cost management and usage analytics
/// Metadata can include provider-specific performance indicators
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage
/// var response = await modelService.ExecuteAsync(request);
/// if (response.Success) {
///     await resultService.ProcessContentAsync(response.Content);
///     await analyticsService.RecordUsageAsync(response.TokensUsed, response.ExecutionTimeMs);
/// } else {
///     logger.LogError("Model execution failed: {{Error}}", response.ErrorMessage);
/// }
/// </code>
/// </example>
public class ModelResponse
{
    /// <summary>
    /// Indicates whether the model execution completed successfully.
    /// Service layers should check this before processing Content.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Generated content from the AI model execution.
    /// Only populated when Success is true; null for failed executions.
    /// Contains the complete model output ready for downstream processing.
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// Detailed error message for failed executions.
    /// Provides actionable information for troubleshooting and user feedback.
    /// Should include provider-specific error codes and context when available.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Provider-specific metadata and additional execution information.
    /// Common entries: model_version, provider_request_id, finish_reason, usage_details.
    /// Service layers can extract provider-specific information for advanced processing.
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = [];
    
    /// <summary>
    /// Execution time in milliseconds for performance monitoring and SLA tracking.
    /// Includes complete request processing time from service invocation to response.
    /// Used by analytics services for performance analysis and optimization insights.
    /// </summary>
    public long ExecutionTimeMs { get; set; }
    
    /// <summary>
    /// Total token count consumed during model execution for cost calculation and usage analytics.
    /// Includes both input and output tokens as reported by the model provider.
    /// Used by billing services for cost allocation and usage monitoring.
    /// </summary>
    public int TokensUsed { get; set; }
}
