using System.ComponentModel.DataAnnotations;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents AI-generated suggestions for workflow optimization, providing intelligent recommendations to improve performance, reduce costs, and enhance user experience.
/// 
/// <para><strong>Business Context:</strong></para>
/// WorkflowSuggestion enables continuous improvement of workflow designs through AI-powered analysis
/// and recommendations. The system identifies optimization opportunities, performance bottlenecks,
/// and enhancement possibilities, providing actionable insights that help teams iteratively improve
/// their workflows while tracking the effectiveness of implemented changes and building organizational
/// knowledge about successful optimization patterns.
/// 
/// <para><strong>Technical Context:</strong></para>
/// WorkflowSuggestion serves as the core entity for AI-driven workflow optimization within the
/// intelligent automation platform, providing structured recommendations with confidence scoring,
/// impact analysis, and implementation tracking capabilities integrated with machine learning
/// models and performance analytics for continuous workflow improvement.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - AI-powered workflow optimization recommendations for continuous improvement
/// - Impact analysis and ROI calculation for data-driven decision making
/// - Implementation tracking and effectiveness measurement for learning loops
/// - Priority-based suggestion management for focused optimization efforts
/// - Comprehensive feedback loops for AI model improvement and accuracy
/// </summary>
/// <remarks>
/// <para><strong>Design Patterns:</strong></para>
/// - Recommendation Engine Pattern: AI-driven suggestion generation and ranking
/// - Feedback Loop Pattern: User response tracking for model improvement
/// - Impact Analysis Pattern: Expected vs. actual outcome measurement
/// - Priority Queue Pattern: Suggestion prioritization and workflow management
/// - Audit Trail Pattern: Complete suggestion lifecycle tracking
/// 
/// <para><strong>AI Integration Framework:</strong></para>
/// - Multiple AI model support for diverse optimization strategies
/// - Confidence scoring for suggestion reliability assessment
/// - Reasoning context for transparent AI decision making
/// - Continuous learning through feedback and outcome tracking
/// - Performance monitoring for model effectiveness evaluation
/// 
/// <para><strong>Optimization Categories:</strong></para>
/// - Performance: Execution speed, resource utilization, throughput
/// - Cost: Resource costs, licensing, operational expenses
/// - Quality: Output accuracy, user satisfaction, error rates
/// - Maintainability: Code complexity, documentation, testability
/// - Scalability: Capacity planning, load handling, growth accommodation
/// 
/// <para><strong>Implementation Lifecycle:</strong></para>
/// 1. Generation: AI analysis and suggestion creation
/// 2. Evaluation: User review and impact assessment
/// 3. Decision: Accept, reject, or defer implementation
/// 4. Implementation: Change execution and deployment
/// 5. Measurement: Actual impact tracking and validation
/// 6. Learning: Feedback incorporation for model improvement
/// 
/// <para><strong>Integration Points:</strong></para>
/// - AI/ML Platform: Suggestion generation and model management
/// - Analytics Engine: Performance measurement and impact analysis
/// - Workflow Engine: Implementation and change management
/// - User Interface: Suggestion presentation and feedback collection
/// - Knowledge Base: Optimization patterns and best practices
/// </remarks>
/// <example>
/// <code>
/// // AI generates optimization suggestion
/// var suggestion = new WorkflowSuggestion
/// {
///     FlowId = customerServiceFlow.Id,
///     Type = SuggestionType.Performance,
///     Title = "Optimize Response Classification Node",
///     Description = "Replace manual classification logic with ML-powered intent recognition to improve accuracy and reduce processing time",
///     SuggestionData = JsonSerializer.Serialize(new
///     {
///         TargetNodeId = "classification-node-001",
///         RecommendedModel = "intent-classifier-v2",
///         ConfigurationChanges = new { confidence_threshold = 0.85, fallback_strategy = "human_review" }
///     }),
///     Priority = SuggestionPriority.High,
///     AiModel = "workflow-optimizer-gpt4",
///     ConfidenceScore = 0.92m,
///     ReasoningContext = "Analysis shows 73% of classification errors occur in customer intent detection. ML model demonstrates 89% accuracy vs current 67%.",
///     ExpectedPerformanceImpact = 35.5m, // 35.5% improvement
///     ExpectedCostImpact = -15.2m, // 15.2% cost reduction
///     ImplementationEffort = 8 // 8 hours estimated
/// };
/// 
/// // User accepts and implements suggestion
/// suggestion.Status = SuggestionStatus.Accepted;
/// suggestion.UserFeedback = "Approved for implementation in next sprint";
/// suggestion.RespondedAt = DateTime.UtcNow;
/// suggestion.RespondedBy = teamLead.Id;
/// 
/// // After implementation, track actual results
/// await Task.Delay(TimeSpan.FromDays(30)); // Simulated monitoring period
/// suggestion.ActualPerformanceImpact = 42.1m; // Better than expected
/// suggestion.ActualCostImpact = -18.7m; // Greater cost savings
/// suggestion.ImplementedAt = DateTime.UtcNow.AddDays(-30);
/// 
/// // AI learns from successful implementation
/// await aiModelTrainer.UpdateModelWithOutcome(suggestion);
/// </code>
/// </example>
public class WorkflowSuggestion : AuditableEntity
{
    /// <summary>
    /// Reference to the target workflow for which this optimization suggestion was generated.
    /// <value>Guid identifier linking this suggestion to the specific workflow under analysis</value>
    /// </summary>
    /// <remarks>
    /// Establishes the scope of optimization and enables workflow-specific suggestion
    /// management, impact tracking, and improvement pattern analysis.
    /// </remarks>
    public Guid FlowId { get; set; }

    /// <summary>
    /// Navigation property to the target workflow for contextual suggestion evaluation.
    /// <value>PromptFlow entity representing the workflow being optimized</value>
    /// </summary>
    /// <remarks>
    /// Provides access to workflow structure, performance metrics, and execution
    /// history for comprehensive suggestion context and impact assessment.
    /// </remarks>
    public virtual PromptFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Classification of the optimization suggestion for appropriate handling and prioritization.
    /// <value>SuggestionType enum categorizing the nature and focus of the optimization</value>
    /// </summary>
    /// <remarks>
    /// Determines the evaluation criteria, implementation approach, and success
    /// metrics for the suggestion based on optimization category and focus area.
    /// </remarks>
    public SuggestionType Type { get; set; }
    
    /// <summary>
    /// Concise, descriptive title summarizing the optimization recommendation.
    /// <value>String up to 200 characters providing a clear suggestion summary</value>
    /// </summary>
    /// <remarks>
    /// Should be actionable and specific enough for quick evaluation and
    /// decision-making while remaining comprehensible to non-technical stakeholders.
    /// </remarks>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Comprehensive description explaining the optimization recommendation and its benefits.
    /// <value>String providing detailed explanation of the suggested changes and rationale</value>
    /// </summary>
    /// <remarks>
    /// Should include the current state, proposed changes, expected benefits,
    /// and any considerations for implementation and evaluation.
    /// </remarks>
    [Required]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Structured implementation details and configuration parameters for the suggestion.
    /// <value>JSON string containing technical specifications, parameters, and implementation guidance</value>
    /// </summary>
    /// <remarks>
    /// Provides machine-readable implementation details that can be used for
    /// automated implementation, validation, and configuration management.
    /// </remarks>
    public string SuggestionData { get; set; } = "{}";
    
    /// <summary>
    /// Business and technical priority level for suggestion implementation planning.
    /// <value>SuggestionPriority enum indicating the urgency and importance of implementation</value>
    /// </summary>
    /// <remarks>
    /// Balances expected impact, implementation effort, and business needs to
    /// guide resource allocation and implementation scheduling decisions.
    /// </remarks>
    public SuggestionPriority Priority { get; set; } = SuggestionPriority.Medium;

    // AI Context
    /// <summary>
    /// Identifier of the AI model that generated this optimization suggestion.
    /// <value>String identifying the specific AI model version and configuration used</value>
    /// </summary>
    /// <remarks>
    /// Enables model performance tracking, suggestion attribution, and
    /// continuous improvement of AI recommendation capabilities and accuracy.
    /// </remarks>
    [StringLength(50)]
    public string AiModel { get; set; } = string.Empty;
    
    /// <summary>
    /// AI model's confidence level in the suggestion's validity and expected outcomes.
    /// <value>Decimal between 0.0 and 1.0 representing the model's confidence score</value>
    /// </summary>
    /// <remarks>
    /// Provides risk assessment for decision-making and helps prioritize
    /// high-confidence suggestions while identifying areas for model improvement.
    /// </remarks>
    public decimal ConfidenceScore { get; set; }
    
    /// <summary>
    /// AI model's reasoning process and analysis context for transparency and validation.
    /// <value>String explaining the model's analysis, data sources, and reasoning chain</value>
    /// </summary>
    /// <remarks>
    /// Supports explainable AI principles, enables validation of recommendations,
    /// and facilitates user understanding and trust in AI-generated suggestions.
    /// </remarks>
    public string? ReasoningContext { get; set; }

    // Expected Impact
    /// <summary>
    /// Predicted performance improvement as a percentage increase from current baseline.
    /// <value>Decimal representing expected performance improvement percentage</value>
    /// </summary>
    /// <remarks>
    /// Quantifies the anticipated performance benefits for ROI analysis and
    /// implementation prioritization based on expected productivity gains.
    /// </remarks>
    public decimal? ExpectedPerformanceImpact { get; set; }
    
    /// <summary>
    /// Predicted financial impact on operational costs from implementing the suggestion.
    /// <value>Decimal representing cost change (positive = increase, negative = savings)</value>
    /// </summary>
    /// <remarks>
    /// Enables cost-benefit analysis and budget planning for optimization
    /// initiatives while tracking return on investment for improvement efforts.
    /// </remarks>
    public decimal? ExpectedCostImpact { get; set; }
    
    /// <summary>
    /// Estimated effort required to implement the suggestion in person-hours.
    /// <value>Integer representing the projected implementation time in hours</value>
    /// </summary>
    /// <remarks>
    /// Supports resource planning, sprint planning, and implementation
    /// scheduling by providing realistic effort estimates for capacity management.
    /// </remarks>
    public int? ImplementationEffort { get; set; }

    // User Response
    /// <summary>
    /// Current status of the suggestion in the evaluation and implementation lifecycle.
    /// <value>SuggestionStatus enum tracking the suggestion's progress through the workflow</value>
    /// </summary>
    /// <remarks>
    /// Enables suggestion lifecycle management, progress tracking, and
    /// workflow automation for optimization process governance and reporting.
    /// </remarks>
    public SuggestionStatus Status { get; set; } = SuggestionStatus.Pending;
    
    /// <summary>
    /// User-provided feedback and comments regarding the suggestion evaluation and decision.
    /// <value>String containing user insights, concerns, modification requests, or implementation notes</value>
    /// </summary>
    /// <remarks>
    /// Captures human expertise and domain knowledge to improve future
    /// suggestions and provide context for implementation and evaluation decisions.
    /// </remarks>
    public string? UserFeedback { get; set; }
    
    /// <summary>
    /// Timestamp when the user provided their response and feedback on the suggestion.
    /// <value>DateTime in UTC recording when the user evaluation and decision occurred</value>
    /// </summary>
    /// <remarks>
    /// Tracks response times for process improvement and provides audit trail
    /// for suggestion evaluation timelines and decision-making patterns.
    /// </remarks>
    public DateTime? RespondedAt { get; set; }
    
    /// <summary>
    /// Identifier of the user who evaluated and responded to the suggestion.
    /// <value>String identifying the user responsible for suggestion evaluation and decision</value>
    /// </summary>
    /// <remarks>
    /// Establishes accountability for suggestion decisions and enables
    /// user-specific feedback analysis for personalized recommendation improvement.
    /// </remarks>
    [StringLength(100)]
    public string? RespondedBy { get; set; }

    // Post-Implementation Tracking
    /// <summary>
    /// Measured performance improvement after suggestion implementation for validation and learning.
    /// <value>Decimal representing actual performance improvement percentage achieved</value>
    /// </summary>
    /// <remarks>
    /// Validates AI prediction accuracy and provides feedback for model
    /// improvement while demonstrating actual value delivered by optimization efforts.
    /// </remarks>
    public decimal? ActualPerformanceImpact { get; set; }
    
    /// <summary>
    /// Measured financial impact after suggestion implementation for ROI calculation and validation.
    /// <value>Decimal representing actual cost change (positive = increase, negative = savings)</value>
    /// </summary>
    /// <remarks>
    /// Enables accurate ROI calculation and validates cost predictions for
    /// future suggestion evaluation and continuous improvement of estimation accuracy.
    /// </remarks>
    public decimal? ActualCostImpact { get; set; }
    
    /// <summary>
    /// Timestamp when the suggestion was successfully implemented in the target workflow.
    /// <value>DateTime in UTC recording the implementation completion moment</value>
    /// </summary>
    /// <remarks>
    /// Establishes the baseline for post-implementation impact measurement and
    /// provides timing data for implementation process analysis and improvement.
    /// </remarks>
    public DateTime? ImplementedAt { get; set; }
}
