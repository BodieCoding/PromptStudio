using PromptStudio.Core.Interfaces.Data;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents the configuration settings for an AI model provider in the PromptStudio system.
/// Stores provider-specific settings, credentials, rate limits, and operational parameters
/// required to integrate with various AI service providers.
/// 
/// <para><strong>Domain Context:</strong></para>
/// <para>Model provider configurations enable the PromptStudio system to connect and interact
/// with multiple AI service providers (OpenAI, Anthropic, Azure, etc.) while maintaining
/// centralized configuration management, security, and operational monitoring.</para>
/// 
/// <para><strong>Configuration Management:</strong></para>
/// <para>Provides secure storage and management of provider credentials, API endpoints,
/// rate limiting configurations, and provider-specific operational parameters.
/// Supports dynamic configuration updates and multi-provider scenarios.</para>
/// </summary>
/// <remarks>
/// <para><strong>Security Considerations:</strong></para>
/// <list type="bullet">
/// <item><description>API keys and secrets should be encrypted at rest</description></item>
/// <item><description>Credentials should support rotation without system downtime</description></item>
/// <item><description>Configuration access should be audited and logged</description></item>
/// <item><description>Sensitive data should never appear in logs or error messages</description></item>
/// </list>
/// 
/// <para><strong>Provider Integration:</strong></para>
/// <list type="bullet">
/// <item><description>Support for multiple concurrent provider configurations</description></item>
/// <item><description>Provider-specific model catalogs and capability mappings</description></item>
/// <item><description>Automatic failover and load balancing configurations</description></item>
/// <item><description>Cost tracking and budget management per provider</description></item>
/// </list>
/// </remarks>
public class ModelProviderConfig : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the AI model provider.
    /// A human-readable identifier for the provider (e.g., "OpenAI", "Anthropic").
    /// </summary>
    /// <value>The provider name as a string (max 50 characters).</value>
    /// <example>
    /// <code>
    /// ProviderName = "OpenAI";
    /// ProviderName = "Anthropic";
    /// ProviderName = "Azure OpenAI";
    /// ProviderName = "Google Vertex";
    /// </code>
    /// </example>
    public required string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the specific model name or identifier within the provider.
    /// Specifies the exact model (e.g., "gpt-4", "claude-3-opus").
    /// </summary>
    /// <value>The model name as a string (max 100 characters).</value>
    /// <example>
    /// <code>
    /// ModelName = "gpt-4-turbo";
    /// ModelName = "claude-3-opus-20240229";
    /// ModelName = "gemini-pro";
    /// ModelName = "text-davinci-003";
    /// </code>
    /// </example>
    public required string ModelName { get; set; }

    /// <summary>
    /// Gets or sets the category or type of model provider service.
    /// Categorizes the provider for filtering and management purposes.
    /// </summary>
    /// <value>The model provider type from the ModelProviderType enumeration.</value>
    /// <example>
    /// <code>
    /// ModelProviderType = ModelProviderType.OpenAI;      // OpenAI-compatible API
    /// ModelProviderType = ModelProviderType.Anthropic;   // Anthropic Claude
    /// ModelProviderType = ModelProviderType.Azure;       // Azure OpenAI Service
    /// ModelProviderType = ModelProviderType.Custom;      // Custom/enterprise provider
    /// </code>
    /// </example>
    public required ModelProviderType ModelProviderType { get; set; }

    /// <summary>
    /// Gets or sets the API endpoint URL for the provider service.
    /// The base URL where API requests should be sent.
    /// </summary>
    /// <value>The API endpoint URL as a string.</value>
    /// <example>
    /// <code>
    /// ApiEndpoint = "https://api.openai.com/v1";
    /// ApiEndpoint = "https://api.anthropic.com";
    /// ApiEndpoint = "https://your-resource.openai.azure.com";
    /// </code>
    /// </example>
    public required string ApiEndpoint { get; set; }

    /// <summary>
    /// Gets or sets the API key or authentication token for the provider.
    /// Encrypted storage is recommended for security.
    /// </summary>
    /// <value>The API key as an encrypted string.</value>
    /// <remarks>
    /// <para><strong>Security Warning:</strong></para>
    /// <para>This field should be encrypted at rest and never logged in plain text.
    /// Consider using Azure Key Vault, AWS Secrets Manager, or similar secure storage.</para>
    /// </remarks>
    public required string ApiKey { get; set; }

    /// <summary>
    /// Gets or sets additional configuration parameters specific to this provider.
    /// Stores provider-specific settings as a JSON string.
    /// </summary>
    /// <value>A JSON string containing provider-specific configuration, or null if not needed.</value>
    /// <example>
    /// <code>
    /// Configuration = "{\"temperature\": 0.7, \"max_tokens\": 4000, \"top_p\": 0.9}";
    /// Configuration = "{\"region\": \"us-west-2\", \"deployment_name\": \"gpt-4-deployment\"}";
    /// </code>
    /// </example>
    public string? Configuration { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of requests per minute allowed for this provider.
    /// Used for rate limiting and request throttling.
    /// </summary>
    /// <value>Maximum requests per minute, or null for unlimited.</value>
    /// <example>
    /// <code>
    /// RateLimitPerMinute = 60;    // 60 requests per minute
    /// RateLimitPerMinute = 1000;  // 1000 requests per minute  
    /// RateLimitPerMinute = null;  // No rate limiting
    /// </code>
    /// </example>
    public int? RateLimitPerMinute { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of tokens per minute allowed for this provider.
    /// Used for token-based rate limiting and cost management.
    /// </summary>
    /// <value>Maximum tokens per minute, or null for unlimited.</value>
    /// <example>
    /// <code>
    /// TokenLimitPerMinute = 150000;  // 150K tokens per minute
    /// TokenLimitPerMinute = 1000000; // 1M tokens per minute
    /// TokenLimitPerMinute = null;    // No token limiting
    /// </code>
    /// </example>
    public int? TokenLimitPerMinute { get; set; }

    /// <summary>
    /// Gets or sets the monthly budget limit for this provider in USD.
    /// Used for cost control and budget monitoring.
    /// </summary>
    /// <value>Monthly budget limit as a decimal, or null for unlimited.</value>
    /// <example>
    /// <code>
    /// MonthlyBudgetLimit = 1000.00m;  // $1000 monthly budget
    /// MonthlyBudgetLimit = 500.50m;   // $500.50 monthly budget
    /// MonthlyBudgetLimit = null;      // No budget limit
    /// </code>
    /// </example>
    public decimal? MonthlyBudgetLimit { get; set; }

    /// <summary>
    /// Gets or sets whether this provider configuration is currently enabled.
    /// Disabled providers will not be used for new requests.
    /// </summary>
    /// <value>True if the provider is enabled; false if disabled.</value>
    /// <remarks>
    /// Disabling a provider allows for maintenance, debugging, or temporary
    /// suspension without deleting the configuration.
    /// </remarks>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the priority level for this provider when multiple providers are available.
    /// Lower numbers indicate higher priority (1 = highest priority).
    /// </summary>
    /// <value>An integer representing the provider priority (1-100).</value>
    /// <example>
    /// <code>
    /// Priority = 1;   // Primary provider
    /// Priority = 2;   // Secondary provider  
    /// Priority = 10;  // Fallback provider
    /// </code>
    /// </example>
    public int Priority { get; set; } = 10;

    /// <summary>
    /// Gets or sets the timeout duration for API requests to this provider.
    /// Specifies how long to wait for API responses before timing out.
    /// </summary>
    /// <value>Timeout duration as a TimeSpan.</value>
    /// <example>
    /// <code>
    /// TimeoutDuration = TimeSpan.FromSeconds(30);  // 30 second timeout
    /// TimeoutDuration = TimeSpan.FromMinutes(2);   // 2 minute timeout
    /// </code>
    /// </example>
    public TimeSpan TimeoutDuration { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>
    /// Gets or sets the number of retry attempts for failed requests.
    /// Specifies how many times to retry a failed request before giving up.
    /// </summary>
    /// <value>Number of retry attempts (0-10).</value>
    /// <example>
    /// <code>
    /// MaxRetries = 3;  // Retry up to 3 times
    /// MaxRetries = 1;  // Retry once
    /// MaxRetries = 0;  // No retries
    /// </code>
    /// </example>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Gets or sets the current health status of this provider.
    /// Tracks operational status for monitoring and alerting.
    /// </summary>
    /// <value>The provider health status from the ProviderHealthStatus enumeration.</value>
    public ModelProviderHealthStatus HealthStatus { get; set; } = ModelProviderHealthStatus.Unknown;

    /// <summary>
    /// Gets or sets the timestamp of the last successful health check.
    /// Used for monitoring provider availability and reliability.
    /// </summary>
    /// <value>DateTimeOffset of the last successful health check, or null if never checked.</value>
    public DateTimeOffset? LastHealthCheck { get; set; }

    /// <summary>
    /// Gets or sets notes or comments about this provider configuration.
    /// Provides additional context for administrators and operators.
    /// </summary>
    /// <value>Optional notes as a string (max 500 characters).</value>
    /// <example>
    /// <code>
    /// Notes = "Primary provider for production workloads";
    /// Notes = "Testing configuration for new model evaluation";
    /// Notes = "Backup provider for peak usage periods";
    /// </code>
    /// </example>
    public string? Notes { get; set; }
}
