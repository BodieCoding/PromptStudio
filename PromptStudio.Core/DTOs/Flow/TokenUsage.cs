namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents token usage information for language model interactions within flow execution.
/// Tracks input, output, and total token consumption for cost monitoring and optimization.
/// </summary>
/// <remarks>
/// <para><strong>Cost Tracking:</strong></para>
/// <para>Provides detailed token consumption metrics essential for cost management, resource
/// planning, and performance optimization in LLM-based flow operations. Critical for
/// enterprise environments requiring precise usage monitoring and budget control.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>Cost calculation and billing for flow executions</description></item>
/// <item><description>Resource utilization monitoring and optimization</description></item>
/// <item><description>Performance analysis and capacity planning</description></item>
/// <item><description>Usage reporting and budget management</description></item>
/// </list>
/// </remarks>
public class TokenUsage
{
    /// <summary>
    /// Gets or sets the number of tokens consumed for input prompts.
    /// Represents the cost of providing context and instructions to the model.
    /// </summary>
    /// <value>
    /// The count of input tokens processed by the language model.
    /// </value>
    public int InputTokens { get; set; }

    /// <summary>
    /// Gets or sets the number of tokens generated in the model's response.
    /// Represents the cost of generating output content from the model.
    /// </summary>
    /// <value>
    /// The count of output tokens produced by the language model.
    /// </value>
    public int OutputTokens { get; set; }

    /// <summary>
    /// Gets the total number of tokens used in the interaction.
    /// Calculated as the sum of input and output tokens.
    /// </summary>
    /// <value>
    /// The combined count of input and output tokens.
    /// </value>
    public int TotalTokens => InputTokens + OutputTokens;

    /// <summary>
    /// Gets or sets the cost per input token in the billing currency.
    /// Used for calculating the cost of input token consumption.
    /// </summary>
    /// <value>
    /// The cost per input token, typically in USD or other billing currency.
    /// </value>
    public decimal InputTokenCost { get; set; }

    /// <summary>
    /// Gets or sets the cost per output token in the billing currency.
    /// Used for calculating the cost of output token generation.
    /// </summary>
    /// <value>
    /// The cost per output token, typically in USD or other billing currency.
    /// </value>
    public decimal OutputTokenCost { get; set; }

    /// <summary>
    /// Gets the estimated total cost for the token usage.
    /// Calculated based on input/output token counts and their respective costs.
    /// </summary>
    /// <value>
    /// The estimated total cost for this token usage instance.
    /// </value>
    public decimal EstimatedCost => (InputTokens * InputTokenCost) + (OutputTokens * OutputTokenCost);

    /// <summary>
    /// Gets or sets the name or identifier of the model used.
    /// Provides context for cost calculation and performance analysis.
    /// </summary>
    /// <value>
    /// The model name or identifier (e.g., "gpt-4", "claude-3-opus").
    /// </value>
    public string? ModelName { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the token usage was recorded.
    /// Enables time-based analysis and usage tracking.
    /// </summary>
    /// <value>
    /// The UTC timestamp when this token usage occurred.
    /// </value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets additional metadata about the token usage.
    /// Provides extensible storage for provider-specific or custom metrics.
    /// </summary>
    /// <value>
    /// A dictionary containing additional token usage metadata.
    /// </value>
    public Dictionary<string, object> Metadata { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the TokenUsage class with default values.
    /// </summary>
    public TokenUsage()
    {
        Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the TokenUsage class with specified token counts.
    /// </summary>
    /// <param name="inputTokens">The number of input tokens</param>
    /// <param name="outputTokens">The number of output tokens</param>
    /// <param name="modelName">The name of the model used</param>
    public TokenUsage(int inputTokens, int outputTokens, string? modelName = null)
    {
        InputTokens = inputTokens;
        OutputTokens = outputTokens;
        ModelName = modelName;
        Timestamp = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds another TokenUsage instance to this one, combining the token counts and costs.
    /// Useful for aggregating token usage across multiple operations.
    /// </summary>
    /// <param name="other">The other TokenUsage instance to add</param>
    /// <returns>This TokenUsage instance for method chaining</returns>
    public TokenUsage Add(TokenUsage other)
    {
        InputTokens += other.InputTokens;
        OutputTokens += other.OutputTokens;
        
        // Update costs if they're not set or if the other usage has costs
        if (InputTokenCost == 0 && other.InputTokenCost > 0)
            InputTokenCost = other.InputTokenCost;
        if (OutputTokenCost == 0 && other.OutputTokenCost > 0)
            OutputTokenCost = other.OutputTokenCost;

        return this;
    }

    /// <summary>
    /// Creates a copy of this TokenUsage instance.
    /// </summary>
    /// <returns>A new TokenUsage instance with identical values</returns>
    public TokenUsage Clone()
    {
        return new TokenUsage(InputTokens, OutputTokens, ModelName)
        {
            InputTokenCost = InputTokenCost,
            OutputTokenCost = OutputTokenCost,
            Timestamp = Timestamp,
            Metadata = new Dictionary<string, object>(Metadata)
        };
    }

    /// <summary>
    /// Returns a string representation of the token usage for debugging and logging.
    /// </summary>
    /// <returns>String representation including token counts and estimated cost</returns>
    public override string ToString()
    {
        var cost = EstimatedCost > 0 ? $" (${EstimatedCost:F4})" : "";
        var model = !string.IsNullOrEmpty(ModelName) ? $" [{ModelName}]" : "";
        return $"TokenUsage: {InputTokens} input + {OutputTokens} output = {TotalTokens} total{cost}{model}";
    }
}