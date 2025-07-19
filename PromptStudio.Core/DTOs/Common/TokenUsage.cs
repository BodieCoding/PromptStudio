namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Represents token usage statistics for AI model interactions
/// </summary>
public class TokenUsage
{
    /// <summary>
    /// Gets or sets the number of input tokens used
    /// </summary>
    public int InputTokens { get; set; }

    /// <summary>
    /// Gets or sets the number of output tokens generated
    /// </summary>
    public int OutputTokens { get; set; }

    /// <summary>
    /// Gets the total number of tokens (input + output)
    /// </summary>
    public int TotalTokens => InputTokens + OutputTokens;

    /// <summary>
    /// Gets or sets the cost associated with the token usage (if available)
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Gets or sets the model used for the interaction
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the usage was recorded
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Initializes a new instance of the TokenUsage class
    /// </summary>
    public TokenUsage()
    {
    }

    /// <summary>
    /// Initializes a new instance of the TokenUsage class with specified values
    /// </summary>
    /// <param name="inputTokens">Number of input tokens</param>
    /// <param name="outputTokens">Number of output tokens</param>
    /// <param name="model">Model used for the interaction</param>
    /// <param name="cost">Cost associated with the usage</param>
    public TokenUsage(int inputTokens, int outputTokens, string? model = null, decimal? cost = null)
    {
        InputTokens = inputTokens;
        OutputTokens = outputTokens;
        Model = model;
        Cost = cost;
    }

    /// <summary>
    /// Adds token usage from another instance
    /// </summary>
    /// <param name="other">Other token usage to add</param>
    /// <returns>New TokenUsage instance with combined values</returns>
    public TokenUsage Add(TokenUsage other)
    {
        return new TokenUsage
        {
            InputTokens = InputTokens + other.InputTokens,
            OutputTokens = OutputTokens + other.OutputTokens,
            Cost = (Cost ?? 0) + (other.Cost ?? 0),
            Model = Model ?? other.Model,
            Timestamp = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Returns a string representation of the token usage
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        return $"Input: {InputTokens}, Output: {OutputTokens}, Total: {TotalTokens}" +
               (Cost.HasValue ? $", Cost: ${Cost:F4}" : "") +
               (!string.IsNullOrEmpty(Model) ? $", Model: {Model}" : "");
    }
}
