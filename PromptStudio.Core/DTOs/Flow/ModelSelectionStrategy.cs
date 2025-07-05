namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Defines model selection strategies for automatic model choice in flow execution scenarios.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Strategy enumeration used by model selection services, cost optimization engines, and execution orchestrators.
/// Essential for automated model routing decisions in multi-model LLMOps environments based on performance, cost, and quality criteria.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Enumeration values representing different optimization strategies for model selection.
/// Designed for efficient strategy communication between services and consistent model routing logic.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Automated model routing and load balancing</item>
/// <item>Cost optimization and budget management</item>
/// <item>Performance tuning and SLA compliance</item>
/// <item>Quality optimization for critical workflows</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight enumeration with minimal serialization overhead. Strategy evaluation should be cached
/// when possible to avoid repeated model capability analysis for similar execution patterns.</para>
/// </remarks>
public enum ModelSelectionStrategy
{
    /// <summary>
    /// Use the configured default model for the flow or template.
    /// </summary>
    Default = 0,

    /// <summary>
    /// Select the fastest available model to minimize response time.
    /// </summary>
    Fastest = 1,

    /// <summary>
    /// Select the most cost-effective model to minimize execution costs.
    /// </summary>
    Cheapest = 2,

    /// <summary>
    /// Select the highest quality model for optimal results.
    /// </summary>
    BestQuality = 3,

    /// <summary>
    /// Balance speed, cost, and quality factors for optimal overall value.
    /// </summary>
    Balanced = 4
}
