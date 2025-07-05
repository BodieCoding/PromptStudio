namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the types of processing nodes available in workflow orchestration for building comprehensive AI-driven processes.
/// 
/// <para><strong>Business Context:</strong></para>
/// Flow node types enable the construction of sophisticated workflow patterns that can handle
/// complex business logic, data processing, human interaction, and system integration requirements.
/// Each node type represents a distinct processing capability that can be orchestrated to create
/// end-to-end business processes leveraging AI and automation technologies.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Node types correspond to specific execution engines and processing capabilities within the
/// workflow runtime. Each type has distinct input/output schemas, configuration requirements,
/// and integration patterns that enable flexible workflow composition while maintaining
/// type safety and execution predictability.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Comprehensive workflow building blocks for complex processes
/// - Type-safe workflow composition and validation
/// - Flexible integration with AI services and external systems
/// - Scalable execution patterns for enterprise workloads
/// </summary>
/// <remarks>
/// <para><strong>Node Categories:</strong></para>
/// - Data Flow: Input, Output, Transform, Variable
/// - AI Processing: Prompt, LlmCall, Template
/// - Control Flow: Conditional, Loop, Parallel
/// - Integration: ApiCall, UserInput
/// - Quality: Validation, ErrorHandler
/// - Analytics: Aggregation
/// 
/// <para><strong>Design Patterns:</para>
/// Each node type implements specific execution patterns optimized for their function.
/// Node types support configuration schemas, validation rules, and execution context
/// requirements that ensure reliable workflow operation.
/// 
/// <para><strong>Extension Considerations:</strong></para>
/// New node types can be added to support emerging AI capabilities, integration
/// requirements, or business process patterns while maintaining backward compatibility.
/// </remarks>
/// <example>
/// A customer service workflow might use: Input → Prompt (sentiment analysis) → 
/// Conditional (route based on sentiment) → Template (generate response) → Output
/// </example>
public enum FlowNodeType
{
    /// <summary>
    /// Entry point node that accepts external data input into the workflow.
    /// Handles data validation, format conversion, and initial processing setup
    /// for subsequent workflow stages.
    /// </summary>
    Input = 0,
    
    /// <summary>
    /// AI prompt processing node that executes natural language prompts with variable substitution.
    /// Core node type for LLM interactions and AI-powered content generation within workflows.
    /// </summary>
    Prompt = 1,
    
    /// <summary>
    /// Variable storage and manipulation node for managing workflow state and data.
    /// Enables complex data transformations and state management across workflow execution.
    /// </summary>
    Variable = 2,
    
    /// <summary>
    /// Decision-making node that implements branching logic based on conditions.
    /// Enables dynamic workflow routing based on data content, AI analysis results, or business rules.
    /// </summary>
    Conditional = 3,
    
    /// <summary>
    /// Data transformation node for processing, formatting, and manipulating workflow data.
    /// Supports data cleaning, format conversion, aggregation, and preparation for downstream processing.
    /// </summary>
    Transform = 4,
    
    /// <summary>
    /// Exit point node that formats and delivers final workflow results.
    /// Handles result formatting, delivery mechanisms, and integration with external systems.
    /// </summary>
    Output = 5,
    
    /// <summary>
    /// Direct LLM API integration node for advanced AI model interactions.
    /// Provides low-level access to language models with custom configuration and prompt engineering.
    /// </summary>
    LlmCall = 6,
    
    /// <summary>
    /// Template processing node that applies reusable prompt templates with variable substitution.
    /// Enables standardized AI interactions and consistent prompt management across workflows.
    /// </summary>
    Template = 7,
    
    /// <summary>
    /// Iterative processing node that repeats operations over data collections or until conditions are met.
    /// Supports batch processing, iterative refinement, and collection-based operations.
    /// </summary>
    Loop = 8,
    
    /// <summary>
    /// Parallel execution node that processes multiple operations simultaneously.
    /// Enables concurrent processing for improved performance and complex workflow patterns.
    /// </summary>
    Parallel = 9,
    
    /// <summary>
    /// External API integration node for calling web services and external systems.
    /// Enables workflow integration with third-party services, databases, and enterprise systems.
    /// </summary>
    ApiCall = 10,
    
    /// <summary>
    /// Data validation and quality assurance node for ensuring workflow data integrity.
    /// Implements validation rules, data quality checks, and error detection for reliable processing.
    /// </summary>
    Validation = 11,
    
    /// <summary>
    /// Data aggregation and analytics node for combining and analyzing workflow data.
    /// Supports statistical analysis, data summarization, and metrics calculation.
    /// </summary>
    Aggregation = 12,
    
    /// <summary>
    /// Error handling and recovery node for managing workflow failures and exceptions.
    /// Implements error recovery strategies, fallback processing, and failure notification mechanisms.
    /// </summary>
    ErrorHandler = 13,
    
    /// <summary>
    /// Human interaction node that pauses workflow execution for user input or approval.
    /// Enables human-in-the-loop workflows with approval processes, manual review, and interactive decision-making.
    /// </summary>
    UserInput = 14
}