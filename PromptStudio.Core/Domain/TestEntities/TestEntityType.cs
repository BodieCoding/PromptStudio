namespace PromptStudio.Core.Domain;

/// <summary>
/// Categorizes the types of entities that can be subjected to A/B testing for comprehensive experiment coverage.
/// 
/// <para><strong>Business Context:</strong></para>
/// TestEntityType enables systematic testing across all critical components of the LLMOps pipeline,
/// from individual prompt templates to complete workflow configurations. This comprehensive approach
/// ensures organizations can optimize every aspect of their AI implementations through controlled
/// experimentation and data-driven decision making.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The entity type determines the testing methodology, metrics collection approach, and analysis
/// framework applied to each experiment. Different entity types require specialized handling
/// for traffic routing, performance measurement, and result interpretation.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Comprehensive testing coverage across the entire LLMOps stack
/// - Type-specific optimization strategies and metrics collection
/// - Systematic approach to AI system performance improvement
/// - Specialized analysis frameworks for different component types
/// - Risk mitigation through controlled testing of critical components
/// </summary>
/// <remarks>
/// <para><strong>Testing Strategies by Type:</strong></para>
/// - PromptTemplate: Content variations, parameter tuning, format optimization
/// - Workflow: Flow logic, step sequencing, conditional branching
/// - VariableSet: Input parameter combinations and value ranges
/// - Model: Different AI models, versions, and configuration settings
/// - Configuration: System settings, performance parameters, feature flags
/// 
/// <para><strong>Metrics Considerations:</strong></para>
/// - PromptTemplate: Response quality, relevance, completion rate
/// - Workflow: Execution time, success rate, error handling
/// - VariableSet: Output variability, consistency, edge case handling
/// - Model: Accuracy, latency, cost, token usage
/// - Configuration: System performance, reliability, user experience
/// 
/// <para><strong>Analysis Frameworks:</strong></para>
/// - Statistical significance testing for quantitative metrics
/// - Qualitative analysis for subjective quality assessments
/// - Performance benchmarking for technical measurements
/// - Cost-benefit analysis for resource optimization
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Experiment Design: Type-specific test configuration
/// - Traffic Router: Entity-appropriate routing logic
/// - Metrics Collector: Type-specific measurement strategies
/// - Analysis Engine: Specialized statistical models per type
/// </remarks>
/// <example>
/// <code>
/// // Configure experiment based on entity type
/// switch (abTest.EntityType)
/// {
///     case TestEntityType.PromptTemplate:
///         // Focus on response quality and relevance metrics
///         metrics.Add("response_quality", "semantic_similarity", "completion_rate");
///         break;
///     
///     case TestEntityType.Workflow:
///         // Emphasize execution performance and reliability
///         metrics.Add("execution_time", "success_rate", "error_count");
///         break;
///     
///     case TestEntityType.Model:
///         // Monitor accuracy, cost, and performance
///         metrics.Add("accuracy_score", "cost_per_request", "latency_p95");
///         break;
/// }
/// </code>
/// </example>
public enum TestEntityType
{
    /// <summary>
    /// Individual prompt templates for content optimization and parameter tuning.
    /// <value>0 - Testing prompt template variations and configurations</value>
    /// </summary>
    /// <remarks>
    /// Focus on prompt content, structure, parameters, and formatting variations.
    /// Metrics typically include response quality, relevance, and completion rates.
    /// </remarks>
    PromptTemplate = 0,

    /// <summary>
    /// Complete workflow configurations including flow logic and step sequencing.
    /// <value>1 - Testing workflow designs and execution patterns</value>
    /// </summary>
    /// <remarks>
    /// Encompasses end-to-end workflow testing including conditional logic,
    /// step ordering, error handling, and integration points.
    /// </remarks>
    Workflow = 1,

    /// <summary>
    /// Variable sets and parameter combinations for input optimization.
    /// <value>2 - Testing variable combinations and parameter ranges</value>
    /// </summary>
    /// <remarks>
    /// Testing different input parameter combinations, value ranges,
    /// and variable set configurations for optimal results.
    /// </remarks>
    VariableSet = 2,

    /// <summary>
    /// AI model comparisons including different models, versions, and settings.
    /// <value>3 - Testing different AI models and configurations</value>
    /// </summary>
    /// <remarks>
    /// Comparing different AI models, model versions, and configuration
    /// parameters for performance, accuracy, and cost optimization.
    /// </remarks>
    Model = 3,

    /// <summary>
    /// System configurations including performance settings and feature flags.
    /// <value>4 - Testing system configurations and feature variations</value>
    /// </summary>
    /// <remarks>
    /// Testing system-level configurations, feature flags, performance
    /// parameters, and other environmental settings.
    /// </remarks>
    Configuration = 4
}
