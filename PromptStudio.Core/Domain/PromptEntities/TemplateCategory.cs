namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines functional categories for prompt templates to enable systematic organization, discovery, and application-specific optimization.
/// 
/// <para><strong>Business Context:</strong></para>
/// Template categories facilitate efficient template discovery, enable specialized optimization strategies,
/// and support organizational workflows by grouping templates according to their primary AI capabilities
/// and business applications. Categories align with common AI use cases and support strategic AI adoption
/// across different business functions and technical requirements.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Categories inform template selection algorithms, enable category-specific optimizations,
/// and support filtering in user interfaces and API endpoints. Each category has associated
/// best practices, performance characteristics, and integration patterns optimized for specific AI tasks.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Systematic template organization and discovery
/// - Category-specific optimization and best practices
/// - Streamlined template selection for specific use cases
/// - Strategic AI capability mapping and planning
/// </summary>
/// <remarks>
/// <para><strong>Category Groupings:</strong></para>
/// - Content: Completion, Summarization, Translation, Creative, Documentation
/// - Technical: CodeGeneration, DataAnalysis, Testing, Security, Optimization
/// - Intelligence: QuestionAnswering, Classification, Extraction, Reasoning
/// - Interactive: Conversation, General
/// 
/// <para><strong>Usage Guidelines:</strong></para>
/// Templates should be assigned to the category that best represents their primary function.
/// Multi-purpose templates should use the General category or the most specific applicable category.
/// 
/// <para><strong>Optimization Considerations:</strong></para>
/// - Different categories benefit from different model parameters
/// - Category-specific performance metrics and evaluation criteria
/// - Specialized training data and fine-tuning approaches per category
/// </remarks>
/// <example>
/// A customer support template might use Conversation, while a data processing template would use DataAnalysis.
/// </example>
public enum TemplateCategory
{
    /// <summary>
    /// General-purpose templates that don't fit specific functional categories.
    /// Suitable for multi-purpose applications, experimental templates,
    /// and use cases that span multiple AI capabilities.
    /// </summary>
    General = 0,
    
    /// <summary>
    /// Text completion and continuation templates for content generation.
    /// Optimized for completing partial content, extending existing text,
    /// and generating structured content based on prompts and context.
    /// </summary>
    Completion = 1,
    
    /// <summary>
    /// Content summarization templates for distilling key information from large texts.
    /// Specialized for extracting main points, creating abstracts, and condensing
    /// information while preserving essential meaning and context.
    /// </summary>
    Summarization = 2,
    
    /// <summary>
    /// Language translation templates for converting content between languages.
    /// Optimized for accuracy, cultural context preservation, and maintaining
    /// tone and meaning across different linguistic and cultural boundaries.
    /// </summary>
    Translation = 3,
    
    /// <summary>
    /// Software code generation and programming assistance templates.
    /// Specialized for creating code snippets, functions, scripts, and providing
    /// programming guidance with language-specific optimizations and best practices.
    /// </summary>
    CodeGeneration = 4,
    
    /// <summary>
    /// Data analysis and interpretation templates for extracting insights from datasets.
    /// Optimized for statistical analysis, pattern recognition, trend identification,
    /// and generating actionable insights from structured and unstructured data.
    /// </summary>
    DataAnalysis = 5,
    
    /// <summary>
    /// Creative content generation templates for artistic and imaginative applications.
    /// Specialized for storytelling, poetry, creative writing, brainstorming,
    /// and generating innovative content with emphasis on creativity and originality.
    /// </summary>
    Creative = 6,
    
    /// <summary>
    /// Question answering templates for providing information and explanations.
    /// Optimized for accuracy, comprehensiveness, and contextual appropriateness
    /// in responding to queries across various domains and complexity levels.
    /// </summary>
    QuestionAnswering = 7,
    
    /// <summary>
    /// Content classification and categorization templates for organizing information.
    /// Specialized for sentiment analysis, topic classification, content tagging,
    /// and systematic organization of textual and multimedia content.
    /// </summary>
    Classification = 8,
    
    /// <summary>
    /// Information extraction templates for identifying and extracting specific data points.
    /// Optimized for named entity recognition, key phrase extraction, structured data
    /// extraction, and converting unstructured content into organized information.
    /// </summary>
    Extraction = 9,
    
    /// <summary>
    /// Logical reasoning and problem-solving templates for complex analytical tasks.
    /// Specialized for multi-step reasoning, causal analysis, logical deduction,
    /// and solving complex problems requiring systematic analytical approaches.
    /// </summary>
    Reasoning = 10,
    
    /// <summary>
    /// Conversational AI templates for interactive dialogue and communication.
    /// Optimized for natural conversation flow, context maintenance, personality
    /// consistency, and engaging user interactions across various conversation types.
    /// </summary>
    Conversation = 11,
    
    /// <summary>
    /// Documentation generation templates for creating technical and business documentation.
    /// Specialized for API documentation, user guides, technical specifications,
    /// and structured documentation with appropriate formatting and organization.
    /// </summary>
    Documentation = 12,
    
    /// <summary>
    /// Software testing and quality assurance templates for validation and verification.
    /// Optimized for test case generation, bug report analysis, quality assessment,
    /// and ensuring software reliability through comprehensive testing approaches.
    /// </summary>
    Testing = 13,
    
    /// <summary>
    /// Security analysis and cybersecurity templates for threat assessment and protection.
    /// Specialized for vulnerability analysis, security policy generation, threat modeling,
    /// and maintaining cybersecurity best practices across systems and applications.
    /// </summary>
    Security = 14,
    
    /// <summary>
    /// Performance optimization templates for improving efficiency and effectiveness.
    /// Optimized for identifying bottlenecks, suggesting improvements, cost optimization,
    /// and enhancing system performance across various technical and business domains.
    /// </summary>
    Optimization = 15
}
