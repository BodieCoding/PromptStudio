namespace PromptStudio.Core.Domain;

/// <summary>
/// Enumeration of AI model provider types supported in the PromptStudio system.
/// Categorizes different provider technologies and APIs for configuration management.
/// </summary>
public enum ModelProviderType
{
    /// <summary>
    /// OpenAI API-compatible providers.
    /// Includes OpenAI and OpenAI-compatible services.
    /// </summary>
    OpenAI,

    /// <summary>
    /// Anthropic Claude API providers.
    /// Anthropic's Claude models and API.
    /// </summary>
    Anthropic,

    /// <summary>
    /// Azure OpenAI Service.
    /// Microsoft Azure's hosted OpenAI models.
    /// </summary>
    Azure,

    /// <summary>
    /// Google AI Platform providers.
    /// Google's AI/ML services including Vertex AI.
    /// </summary>
    Google,

    /// <summary>
    /// Amazon Web Services AI services.
    /// AWS Bedrock, SageMaker, and related services.
    /// </summary>
    AWS,

    /// <summary>
    /// Hugging Face model hosting.
    /// Hugging Face Inference API and endpoints.
    /// </summary>
    HuggingFace,

    /// <summary>
    /// Open source or self-hosted models.
    /// Local deployments, Ollama, etc.
    /// </summary>
    OpenSource,

    /// <summary>
    /// Custom or enterprise-specific providers.
    /// Organization-specific or custom integrations.
    /// </summary>
    Custom
}
