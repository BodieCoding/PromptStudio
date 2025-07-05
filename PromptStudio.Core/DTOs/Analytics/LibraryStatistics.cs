namespace PromptStudio.Core.DTOs.Analytics;

/// <summary>
/// Comprehensive library performance analytics and operational metrics for content management and optimization.
/// 
/// <para><strong>Service Integration:</strong></para>
/// Used by library management services to provide detailed performance assessment, content analysis,
/// and optimization insights for individual prompt libraries. Enables library-level performance tracking,
/// content optimization decisions, and comparative analysis across different library configurations.
/// 
/// <para><strong>Data Contract:</strong></para>
/// Aggregates template usage patterns, execution performance, content characteristics, and quality metrics
/// into comprehensive library assessment data. Provides both quantitative metrics and qualitative insights
/// for data-driven library management and optimization strategies.
/// </summary>
/// <remarks>
/// <para><strong>Service Layer Usage:</strong></para>
/// - Library management services use this for performance monitoring and optimization
/// - Content optimization services analyze usage patterns for improvement recommendations
/// - Quality assurance services evaluate library health and template effectiveness
/// - Analytics services aggregate multiple libraries for comparative analysis
/// - Reporting services generate library performance summaries and trend reports
/// 
/// <para><strong>Optimization Insights:</strong></para>
/// TemplatesByCategory and ExecutionsByModel provide distribution analysis
/// MostUsedVariables indicate optimization opportunities and reuse patterns
/// QualityMetrics enable systematic quality assessment and improvement tracking
/// 
/// <para><strong>Performance Considerations:</strong></para>
/// Statistics are pre-computed for efficient dashboard loading
/// Dictionary collections enable drill-down analysis without additional queries
/// Quality metrics provide actionable insights for library improvement
/// </remarks>
/// <example>
/// <code>
/// // Service layer usage for library optimization
/// var stats = await libraryAnalyticsService.GetStatisticsAsync(libraryId);
/// var utilizationScore = stats.AverageExecutionsPerTemplate;
/// if (stats.SuccessRate < 0.95) {
///     var recommendations = await optimizationService.AnalyzeQualityIssuesAsync(stats.QualityMetrics);
/// }
/// </code>
/// </example>
public class LibraryStatistics
{
    /// <summary>
    /// Total number of templates contained within the library.
    /// Indicates library content volume and scope for capacity planning.
    /// Service layers use this for library size comparison and content inventory tracking.
    /// </summary>
    public long TemplateCount { get; set; }

    /// <summary>
    /// Total number of executions across all templates in the library.
    /// Comprehensive usage indicator for library activity assessment and popularity analysis.
    /// Used by analytics services for usage trend tracking and performance evaluation.
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Count of unique variables defined across all library templates.
    /// Indicates template complexity and parameterization sophistication.
    /// Service layers can analyze this for template design patterns and reusability assessment.
    /// </summary>
    public long UniqueVariableCount { get; set; }

    /// <summary>
    /// Timestamp of the most recent template creation in the library.
    /// Indicates library development activity and content growth patterns.
    /// Used for library maintenance tracking and development velocity analysis.
    /// </summary>
    public DateTime? LastTemplateCreated { get; set; }

    /// <summary>
    /// Timestamp of the most recent template execution in the library.
    /// Indicates library usage recency and operational activity level.
    /// Service layers use this for activity monitoring and engagement assessment.
    /// </summary>
    public DateTime? LastExecution { get; set; }

    /// <summary>
    /// Calculated average executions per template indicating usage distribution efficiency.
    /// Computed as TotalExecutions divided by TemplateCount.
    /// Higher values suggest well-utilized libraries with broadly useful content.
    /// </summary>
    public double AverageExecutionsPerTemplate { get; set; }

    /// <summary>
    /// Total content size in bytes across all library templates.
    /// Indicates library storage requirements and content complexity assessment.
    /// Service layers use this for storage planning and performance optimization decisions.
    /// </summary>
    public long TotalContentSize { get; set; }

    /// <summary>
    /// Success rate percentage for all executions within the library.
    /// Critical quality indicator for library reliability and template effectiveness.
    /// Used by quality assurance services for library health monitoring and improvement planning.
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Average execution time across all library template executions.
    /// Performance indicator for library efficiency and optimization opportunities.
    /// Service layers use this for performance benchmarking and resource allocation planning.
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }

    /// <summary>
    /// Aggregated token consumption across all library executions.
    /// Null when no token-consuming activities have occurred in the library.
    /// Service layers use this for cost analysis and resource utilization optimization.
    /// </summary>
    public TokenUsage? TotalTokenUsage { get; set; }

    /// <summary>
    /// Distribution of templates by category for content organization analysis.
    /// Key: category name, Value: template count in that category.
    /// Service layers can analyze content distribution patterns and organizational effectiveness.
    /// </summary>
    public Dictionary<string, long> TemplatesByCategory { get; set; } = new();

    /// <summary>
    /// Distribution of executions by AI model for model usage analysis.
    /// Key: model identifier, Value: execution count for that model.
    /// Enables model performance comparison and optimization strategy development.
    /// </summary>
    public Dictionary<string, long> ExecutionsByModel { get; set; } = new();

    /// <summary>
    /// List of most frequently used variables across library templates.
    /// Ordered by usage frequency to identify reuse patterns and optimization opportunities.
    /// Service layers can promote common variables for template standardization and efficiency.
    /// </summary>
    public List<string> MostUsedVariables { get; set; } = new();

    /// <summary>
    /// Comprehensive quality assessment metrics for library health evaluation.
    /// Includes error rates, performance indicators, and quality scores.
    /// Service layers use this for systematic quality improvement and optimization planning.
    /// </summary>
    public QualityMetrics? QualityMetrics { get; set; }
}
