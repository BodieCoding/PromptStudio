namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents token usage metrics and cost information for node execution in LLM-based workflows.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Token tracking DTO used by execution services, cost management systems, and billing engines.
/// Essential for LLMOps cost optimization, usage monitoring, and resource allocation in AI-powered workflows.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Token consumption data with cost calculation support for accurate billing and optimization analysis.
/// Designed for efficient aggregation across multiple executions and cost reporting scenarios.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Real-time cost tracking and budget monitoring</item>
/// <item>Token usage optimization and efficiency analysis</item>
/// <item>Billing and cost allocation for multi-tenant scenarios</item>
/// <item>Performance benchmarking and cost-per-operation analysis</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight token tracking with calculated properties for total values. Cost calculations are performed
/// on-demand to maintain accuracy. Consider aggregating token usage for batch reporting scenarios to improve efficiency.</para>
/// </remarks>
public class TokenUsage
{
    /// <summary>
    /// Gets or sets the number of input tokens consumed during execution.
    /// </summary>
    /// <value>A non-negative integer representing the tokens used for input processing.</value>
    public int InputTokens { get; set; }

    /// <summary>
    /// Gets or sets the number of output tokens generated during execution.
    /// </summary>
    /// <value>A non-negative integer representing the tokens produced as output.</value>
    public int OutputTokens { get; set; }

    /// <summary>
    /// Gets the total number of tokens consumed during execution.
    /// </summary>
    /// <value>The sum of input and output tokens.</value>
    public int TotalTokens => InputTokens + OutputTokens;

    /// <summary>
    /// Gets or sets the cost per input token for billing calculations.
    /// </summary>
    /// <value>A decimal representing the cost per input token, or null if cost tracking is disabled.</value>
    public decimal? InputTokenCost { get; set; }

    /// <summary>
    /// Gets or sets the cost per output token for billing calculations.
    /// </summary>
    /// <value>A decimal representing the cost per output token, or null if cost tracking is disabled.</value>
    public decimal? OutputTokenCost { get; set; }

    /// <summary>
    /// Gets the calculated total cost for this token usage.
    /// </summary>
    /// <value>The sum of input and output costs, or null if cost information is not available.</value>
    public decimal? TotalCost => (InputTokens * (InputTokenCost ?? 0)) + (OutputTokens * (OutputTokenCost ?? 0));
}
