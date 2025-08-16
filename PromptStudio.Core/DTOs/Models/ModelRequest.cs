namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Standardized request payload for AI model execution across different providers and execution contexts.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by execution services to abstract model provider differences and provide consistent request structure
/// for template execution, workflow processing, and interactive AI operations. Enables provider-agnostic
/// service implementations while maintaining flexibility for provider-specific optimizations.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Encapsulates essential model execution parameters including model selection, prompt content,
/// system instructions, execution parameters, and variable substitutions in a provider-neutral format
/// that can be adapted for different AI service providers (OpenAI, Azure, etc.).
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Template execution services use this for prompt processing
/// - Model abstraction services translate this to provider-specific formats
/// - Workflow engines use this for node execution requests
/// - API controllers receive and validate this from client requests
/// 
/// <para><strong>Parameter Handling:</strong></para>
/// Parameters dictionary supports provider-specific settings (temperature, max_tokens, etc.)
/// Variables dictionary contains template variable substitutions and runtime values
/// Both dictionaries use object values to support diverse data types and provider requirements
/// 
/// <para><strong>Validation Considerations:</strong></para>
/// Services should validate ModelId against available providers and capabilities
/// Prompt content should be validated for length and content policy compliance
/// Parameters should be validated against model-specific constraints and limits
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage
/// var request = new ModelRequest {
///     ModelId = "gpt-4",
///     Prompt = processedTemplate,
///     SystemMessage = "You are a helpful assistant",
///     Parameters = { ["temperature"] = 0.7, ["max_tokens"] = 1000 },
///     Variables = { ["customerName"] = "John Doe", ["priority"] = "high" }
/// };
/// var response = await modelService.ExecuteAsync(request);
/// </code>
/// </example>
public class ModelRequest
{
    /// <summary>
    /// Identifier for the AI model to be used for execution.
    /// Should correspond to available models in the configured provider ecosystem.
    /// Examples: "gpt-4", "gpt-3.5-turbo", "claude-3", "azure-gpt-4"
    /// </summary>
    public string ModelId { get; set; } = string.Empty;
    
    /// <summary>
    /// Main prompt content to be processed by the AI model.
    /// Should contain the complete prompt after template variable substitution and processing.
    /// Service layers are responsible for content validation and policy compliance.
    /// </summary>
    public string Prompt { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional system message providing context and behavior instructions to the model.
    /// Used for persona definition, response formatting guidelines, and behavioral constraints.
    /// Provider services may handle this differently based on model capabilities.
    /// </summary>
    public string? SystemMessage { get; set; }
    
    /// <summary>
    /// Model-specific execution parameters for controlling generation behavior.
    /// Common parameters: temperature, max_tokens, top_p, frequency_penalty, presence_penalty.
    /// Services should validate parameters against model capabilities and provider constraints.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = [];
    
    /// <summary>
    /// Template variables and their resolved values for execution context.
    /// Used for audit trails, debugging, and execution reproducibility.
    /// Service layers can use this for logging, analytics, and troubleshooting.
    /// </summary>
    public Dictionary<string, object> Variables { get; set; } = [];
}
