namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines template size categories for optimizing resource allocation, execution planning, and performance management.
/// 
/// <para><strong>Business Context:</strong></para>
/// Template size classification enables predictive resource planning, cost estimation, and performance optimization
/// for AI workloads. Size-based categorization supports SLA planning, capacity management, and efficient
/// allocation of computational resources across different template types and usage patterns.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Size categories correlate with execution time, memory requirements, API costs, and processing complexity.
/// This classification enables automatic resource allocation, queue management, and optimization strategies
/// tailored to different performance characteristics and computational requirements.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Predictive resource planning and capacity management
/// - Optimized execution strategies based on template complexity
/// - Cost estimation and budget planning for AI operations
/// - Performance optimization through size-appropriate handling
/// </summary>
/// <remarks>
/// <para><strong>Resource Allocation Guidelines:</strong></para>
/// - Small: Optimal for real-time, low-latency applications
/// - Medium: Standard processing with balanced performance/cost
/// - Large: Batch processing with extended execution time allowances
/// - ExtraLarge: Specialized handling with dedicated resources
/// 
/// <para><strong>Performance Characteristics:</strong></para>
/// Size directly impacts token consumption, processing time, memory usage,
/// and API costs, enabling informed decisions about template optimization
/// and deployment strategies.
/// 
/// <para><strong>Optimization Strategies:</strong></para>
/// - Small templates: Prioritize for interactive use cases
/// - Medium templates: Standard execution pools and caching
/// - Large templates: Batch processing and asynchronous execution
/// - ExtraLarge templates: Specialized infrastructure and monitoring
/// </remarks>
/// <example>
/// A simple greeting template (Small) executes immediately, while a comprehensive
/// document analysis template (ExtraLarge) requires dedicated processing resources.
/// </example>
public enum TemplateSize
{
    /// <summary>
    /// Compact templates under 1KB optimized for fast, real-time execution.
    /// Ideal for interactive applications, quick responses, and high-frequency operations
    /// requiring minimal latency and computational overhead.
    /// </summary>
    Small = 0,
    
    /// <summary>
    /// Standard templates between 1KB and 10KB suitable for typical business operations.
    /// Balanced performance characteristics for most enterprise use cases with
    /// reasonable execution times and resource requirements.
    /// </summary>
    Medium = 1,
    
    /// <summary>
    /// Complex templates between 10KB and 100KB requiring extended processing time.
    /// Suitable for comprehensive analysis, detailed content generation, and
    /// sophisticated workflows with higher computational requirements.
    /// </summary>
    Large = 2,
    
    /// <summary>
    /// Extensive templates exceeding 100KB requiring specialized resource allocation.
    /// Designed for complex document processing, comprehensive analysis workflows,
    /// and enterprise-scale operations requiring dedicated computational resources.
    /// </summary>
    ExtraLarge = 3
}
