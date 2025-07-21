using PromptStudio.Core.Domain;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.Interfaces.Testing;

/// <summary>
/// Service interface for comprehensive A/B testing and experimentation management in enterprise LLMOps environments.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// <para>Core testing service operating in the business service layer, responsible for experiment design,
/// variant management, statistical analysis, and result interpretation. Integrates with execution services
/// to provide controlled testing environments for prompt optimization, model comparison, and performance
/// evaluation across different configurations and parameters.</para>
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// <para>Implementations must support statistically rigorous experimental design, maintain experimental
/// integrity, provide real-time monitoring capabilities, and deliver actionable insights for prompt
/// optimization. All testing operations must be auditable and support regulatory compliance requirements.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
/// <item><description>Implement statistically sound experimental design and sample size calculation</description></item>
/// <item><description>Support multiple testing frameworks (A/B, multivariate, sequential testing)</description></item>
/// <item><description>Maintain experimental isolation and prevent data contamination</description></item>
/// <item><description>Provide real-time statistical monitoring and early stopping capabilities</description></item>
/// <item><description>Ensure reproducible experiments with comprehensive versioning and audit trails</description></item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Integrates with IPromptExecutionService for controlled experiment execution</description></item>
/// <item><description>Coordinates with IAnalyticsService for statistical analysis and reporting</description></item>
/// <item><description>Connects to IPromptTemplateService for variant template management</description></item>
/// <item><description>Utilizes IGovernanceService for experiment audit trails and compliance</description></item>
/// <item><description>Implements event-driven architecture for real-time experiment monitoring</description></item>
/// </list>
/// 
/// <para><strong>Statistical Considerations:</strong></para>
/// <list type="bullet">
/// <item><description>Supports multiple statistical tests (t-test, chi-square, Mann-Whitney U, etc.)</description></item>
/// <item><description>Implements power analysis for optimal sample size determination</description></item>
/// <item><description>Provides multiple comparison correction (Bonferroni, FDR, etc.)</description></item>
/// <item><description>Supports Bayesian and frequentist statistical approaches</description></item>
/// </list>
/// </remarks>
public interface ITestingService
{
    #region Experiment Lifecycle Management

    /// <summary>
    /// Creates a new A/B test experiment with comprehensive configuration and statistical design.
    /// Establishes experimental framework, variants, and success metrics for controlled testing.
    /// </summary>
    /// <param name="name">Display name for the experiment, must be unique within organization scope</param>
    /// <param name="description">Detailed description of experiment purpose and hypothesis</param>
    /// <param name="hypothesis">Formal hypothesis statement for statistical testing</param>
    /// <param name="organizationId">Organization identifier for enterprise multi-tenancy</param>
    /// <param name="createdBy">User identifier for the experiment creator</param>
    /// <param name="entityType">Type of entity being tested (Template, Workflow, etc.)</param>
    /// <param name="entityId">Identifier of the primary entity being tested</param>
    /// <param name="variants">Collection of experiment variants with configurations</param>
    /// <param name="successMetrics">Key metrics for experiment success evaluation</param>
    /// <param name="statisticalConfig">Statistical configuration for experiment analysis</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Created experiment with generated identifiers and statistical configuration</returns>
    /// <exception cref="ArgumentException">Thrown when experiment parameters are invalid</exception>
    /// <exception cref="DuplicateExperimentNameException">Thrown when experiment name already exists</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment creation permissions</exception>
    /// <exception cref="StatisticalDesignException">Thrown when statistical configuration is invalid</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Experiment names must be unique within organization scope</description></item>
    /// <item><description>Creator must have testing permissions for the target entity type</description></item>
    /// <item><description>Experiments require at least two variants for meaningful comparison</description></item>
    /// <item><description>Statistical configuration must specify valid hypothesis and significance levels</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Performs power analysis to recommend optimal sample sizes</description></item>
    /// <item><description>Validates experimental design for statistical soundness</description></item>
    /// <item><description>Creates isolated execution environments for each variant</description></item>
    /// <item><description>Establishes comprehensive audit trail for experimental integrity</description></item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// // Create an A/B test for prompt template optimization
    /// var experiment = await testingService.CreateExperimentAsync(
    ///     "Customer Service Prompt Optimization",
    ///     "Test different prompt structures for customer service automation",
    ///     "Structured prompts will improve response quality scores by 15%",
    ///     organizationId,
    ///     userId,
    ///     "PromptTemplate",
    ///     templateId,
    ///     variants,
    ///     successMetrics,
    ///     statisticalConfig,
    ///     cancellationToken
    /// );
    /// </code>
    /// </remarks>
    Task<ABTest> CreateExperimentAsync(
        string name,
        string description,
        string hypothesis,
        Guid organizationId,
        string createdBy,
        TestEntityType entityType,
        Guid entityId,
        IEnumerable<ExperimentVariant> variants,
        IEnumerable<SuccessMetric> successMetrics,
        StatisticalConfiguration statisticalConfig,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an experiment by its unique identifier with comprehensive configuration and results.
    /// Supports loading of variants, results, and statistical analysis data.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="userId">User identifier for access control validation</param>
    /// <param name="includeResults">Whether to load experiment results and analysis</param>
    /// <param name="includeVariants">Whether to load experiment variants and configurations</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Experiment details or null if not found or access denied</returns>
    /// <exception cref="ArgumentException">Thrown when experimentId is empty or invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks read access to experiment</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>User must have read access to experiment or underlying entity</description></item>
    /// <item><description>Deleted experiments are excluded unless user has admin privileges</description></item>
    /// <item><description>Statistical results are computed in real-time when requested</description></item>
    /// <item><description>Variant data respects entity-level access controls</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements lazy loading for large result datasets</description></item>
    /// <item><description>Validates access permissions before loading sensitive experiment data</description></item>
    /// <item><description>Uses read-optimized queries with appropriate caching strategies</description></item>
    /// <item><description>Supports partial loading to minimize data transfer overhead</description></item>
    /// </list>
    /// </remarks>
    Task<ABTest?> GetExperimentAsync(
        Guid experimentId,
        Guid organizationId,
        string userId,
        bool includeResults = false,
        bool includeVariants = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all experiments accessible to a user within an organization scope.
    /// Supports filtering by status, entity type, and date range with pagination.
    /// </summary>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="userId">User identifier for access-controlled results</param>
    /// <param name="status">Optional filter by experiment status</param>
    /// <param name="entityType">Optional filter by target entity type</param>
    /// <param name="DateTimeRange">Optional filter by experiment creation date</param>
    /// <param name="pageRequest">Pagination parameters for large result sets</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated list of experiments accessible to the user</returns>
    /// <exception cref="ArgumentException">Thrown when organization or user parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment access permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Results include only experiments user has access to view</description></item>
    /// <item><description>Experiments are ordered by creation date (most recent first)</description></item>
    /// <item><description>Filtering respects entity-level access controls</description></item>
    /// <item><description>Deleted experiments are excluded from standard queries</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements efficient query patterns for large experiment datasets</description></item>
    /// <item><description>Supports full-text search on experiment names and descriptions</description></item>
    /// <item><description>Provides summary statistics without loading full result datasets</description></item>
    /// <item><description>Optimizes for experiment discovery and management interfaces</description></item>
    /// </list>
    /// </remarks>
    Task<PagedResult<ExperimentSummary>> GetExperimentsAsync(
        Guid organizationId,
        string userId,
        ABTestStatus? status = null,
        TestEntityType? entityType = null,
        DateTimeRange? DateTimeRange = null,
        PageRequest? pageRequest = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts an experiment and begins data collection with proper randomization and allocation.
    /// Initializes experiment tracking and prepares execution environment for variant testing.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="startedBy">User identifier for experiment activation</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Experiment start result with activation status and configuration</returns>
    /// <exception cref="ArgumentException">Thrown when experiment parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment management permissions</exception>
    /// <exception cref="ExperimentStateException">Thrown when experiment is not in a startable state</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Experiments can only be started from Draft or Paused status</description></item>
    /// <item><description>User must have experiment management permissions</description></item>
    /// <item><description>All variants must be properly configured before starting</description></item>
    /// <item><description>Statistical configuration must be validated before activation</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Initializes randomization seeds for consistent allocation</description></item>
    /// <item><description>Validates all variant configurations and dependencies</description></item>
    /// <item><description>Creates monitoring infrastructure for real-time tracking</description></item>
    /// <item><description>Establishes data collection pipelines for all success metrics</description></item>
    /// </list>
    /// </remarks>
    Task<ExperimentStartResult> StartExperimentAsync(
        Guid experimentId,
        Guid organizationId,
        string startedBy,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops an experiment and finalizes data collection with comprehensive analysis.
    /// Performs final statistical analysis and generates experiment results and recommendations.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="stoppedBy">User identifier for experiment termination</param>
    /// <param name="reason">Business justification for experiment termination</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Experiment stop result with final analysis and recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when experiment parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment management permissions</exception>
    /// <exception cref="ExperimentStateException">Thrown when experiment is not in a stoppable state</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Experiments can only be stopped from Running status</description></item>
    /// <item><description>User must have experiment management permissions</description></item>
    /// <item><description>Final analysis includes statistical significance testing</description></item>
    /// <item><description>All collected data is preserved for future analysis</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Performs comprehensive statistical analysis of all collected data</description></item>
    /// <item><description>Generates actionable recommendations based on experiment results</description></item>
    /// <item><description>Creates final experiment report with detailed findings</description></item>
    /// <item><description>Preserves all experimental data for compliance and audit purposes</description></item>
    /// </list>
    /// </remarks>
    Task<ExperimentStopResult> StopExperimentAsync(
        Guid experimentId,
        Guid organizationId,
        string stoppedBy,
        string reason,
        CancellationToken cancellationToken = default);

    #endregion

    #region Variant Management

    /// <summary>
    /// Adds a new variant to an existing experiment with proper configuration validation.
    /// Supports dynamic variant addition for adaptive experiments and optimization scenarios.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="variant">Variant configuration with parameters and allocation</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="addedBy">User identifier for variant addition</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Added variant with generated identifiers and validated configuration</returns>
    /// <exception cref="ArgumentException">Thrown when variant configuration is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment modification permissions</exception>
    /// <exception cref="ExperimentStateException">Thrown when experiment state doesn't allow variant addition</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Variants can only be added to experiments in Draft status</description></item>
    /// <item><description>Variant names must be unique within the experiment scope</description></item>
    /// <item><description>Total traffic allocation across all variants cannot exceed 100%</description></item>
    /// <item><description>Variant configurations must be compatible with experiment design</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Validates variant configuration against experiment requirements</description></item>
    /// <item><description>Recalculates statistical power and sample size requirements</description></item>
    /// <item><description>Updates experiment metadata with new variant information</description></item>
    /// <item><description>Maintains audit trail for variant addition and configuration changes</description></item>
    /// </list>
    /// </remarks>
    Task<ABTestVariant> AddVariantAsync(
        Guid experimentId,
        ExperimentVariant variant,
        Guid organizationId,
        string addedBy,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing variant configuration with comprehensive validation.
    /// Supports configuration changes while maintaining experimental integrity.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="variantId">Unique variant identifier</param>
    /// <param name="updatedVariant">Updated variant configuration</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="updatedBy">User identifier for variant update</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Updated variant with validated configuration and audit information</returns>
    /// <exception cref="ArgumentException">Thrown when variant parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment modification permissions</exception>
    /// <exception cref="ExperimentStateException">Thrown when experiment state doesn't allow variant updates</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Variant updates are only allowed for experiments in Draft or Paused status</description></item>
    /// <item><description>Configuration changes must maintain statistical validity</description></item>
    /// <item><description>Traffic allocation changes require experiment restart for running experiments</description></item>
    /// <item><description>All variant changes are comprehensively audited</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Validates updated configuration against experiment requirements</description></item>
    /// <item><description>Recalculates statistical metrics if allocation changes</description></item>
    /// <item><description>Preserves historical variant configurations for audit purposes</description></item>
    /// <item><description>Notifies monitoring systems of configuration changes</description></item>
    /// </list>
    /// </remarks>
    Task<ABTestVariant> UpdateVariantAsync(
        Guid experimentId,
        Guid variantId,
        ExperimentVariant updatedVariant,
        Guid organizationId,
        string updatedBy,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a variant from an experiment with proper data preservation.
    /// Supports variant removal while maintaining experimental integrity and audit trails.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="variantId">Unique variant identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="removedBy">User identifier for variant removal</param>
    /// <param name="reason">Business justification for variant removal</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Variant removal result with audit information and impact analysis</returns>
    /// <exception cref="ArgumentException">Thrown when variant parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment modification permissions</exception>
    /// <exception cref="ExperimentStateException">Thrown when variant removal would invalidate experiment</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Variants can only be removed from experiments in Draft status</description></item>
    /// <item><description>At least two variants must remain for valid experiments</description></item>
    /// <item><description>Removal preserves all collected data for audit purposes</description></item>
    /// <item><description>Traffic allocation is redistributed among remaining variants</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Validates that experiment remains statistically valid after removal</description></item>
    /// <item><description>Redistributes traffic allocation proportionally among remaining variants</description></item>
    /// <item><description>Preserves variant configuration and any collected data</description></item>
    /// <item><description>Updates experiment metadata and statistical calculations</description></item>
    /// </list>
    /// </remarks>
    Task<VariantRemovalResult> RemoveVariantAsync(
        Guid experimentId,
        Guid variantId,
        Guid organizationId,
        string removedBy,
        string reason,
        CancellationToken cancellationToken = default);

    #endregion

    #region Data Collection and Analysis

    /// <summary>
    /// Records an experiment result data point with comprehensive metadata and validation.
    /// Supports real-time data collection for ongoing experiments with proper allocation tracking.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="variantId">Unique variant identifier for the result</param>
    /// <param name="sessionId">Session identifier for result correlation</param>
    /// <param name="metricValues">Collection of metric values and measurements</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="metadata">Additional metadata and context for the result</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Recorded result with validation status and statistical updates</returns>
    /// <exception cref="ArgumentException">Thrown when result parameters are invalid</exception>
    /// <exception cref="ExperimentStateException">Thrown when experiment is not accepting data</exception>
    /// <exception cref="DataValidationException">Thrown when metric values fail validation</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Results can only be recorded for running experiments</description></item>
    /// <item><description>Metric values must conform to defined success metric specifications</description></item>
    /// <item><description>Session allocation must be consistent throughout experiment duration</description></item>
    /// <item><description>All result data is immutable once recorded</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Validates metric values against experiment success metric definitions</description></item>
    /// <item><description>Updates real-time statistical calculations and monitoring</description></item>
    /// <item><description>Maintains session consistency for proper randomization</description></item>
    /// <item><description>Implements data quality checks and outlier detection</description></item>
    /// </list>
    /// </remarks>
    Task<ExperimentResultRecord> RecordResultAsync(
        Guid experimentId,
        Guid variantId,
        string sessionId,
        IEnumerable<MetricValue> metricValues,
        Guid organizationId,
        object? metadata = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs comprehensive statistical analysis of experiment results with multiple testing approaches.
    /// Provides detailed insights, significance testing, and actionable recommendations.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="analysisType">Type of statistical analysis to perform</param>
    /// <param name="confidenceLevel">Confidence level for statistical testing (default: 0.95)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive statistical analysis with insights and recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when experiment parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment analysis permissions</exception>
    /// <exception cref="InsufficientDataException">Thrown when insufficient data exists for meaningful analysis</exception>
    /// <remarks>
    /// <para><strong>Analysis Types:</strong></para>
    /// <list type="bullet">
    /// <item><description>Frequentist: Traditional hypothesis testing with p-values and confidence intervals</description></item>
    /// <item><description>Bayesian: Posterior probability distributions and credible intervals</description></item>
    /// <item><description>Sequential: Ongoing analysis with early stopping criteria</description></item>
    /// <item><description>Multivariate: Multi-metric analysis with correlation assessment</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements multiple statistical tests appropriate for data types</description></item>
    /// <item><description>Provides effect size calculations and practical significance assessment</description></item>
    /// <item><description>Generates visualizations and statistical summaries</description></item>
    /// <item><description>Includes power analysis and sample size recommendations</description></item>
    /// </list>
    /// </remarks>
    Task<ExperimentAnalysisResult> AnalyzeExperimentAsync(
        Guid experimentId,
        Guid organizationId,
        StatisticalAnalysisType analysisType = StatisticalAnalysisType.Frequentist,
        double confidenceLevel = 0.95,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves real-time experiment monitoring data with current status and performance metrics.
    /// Provides live insights into experiment progress and early indication of results.
    /// </summary>
    /// <param name="experimentId">Unique experiment identifier</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Real-time experiment monitoring data with current statistics and alerts</returns>
    /// <exception cref="ArgumentException">Thrown when experiment parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks experiment monitoring permissions</exception>
    /// <remarks>
    /// <para><strong>Monitoring Metrics:</strong></para>
    /// <list type="bullet">
    /// <item><description>Sample size progress and allocation distribution</description></item>
    /// <item><description>Current metric performance and trend analysis</description></item>
    /// <item><description>Statistical power and significance monitoring</description></item>
    /// <item><description>Data quality metrics and anomaly detection</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements real-time data aggregation and statistical calculations</description></item>
    /// <item><description>Provides early stopping recommendations based on statistical criteria</description></item>
    /// <item><description>Monitors data quality and alerts for potential issues</description></item>
    /// <item><description>Supports customizable monitoring dashboards and alerting</description></item>
    /// </list>
    /// </remarks>
    Task<ExperimentMonitoringData> GetExperimentMonitoringAsync(
        Guid experimentId,
        Guid organizationId,
        CancellationToken cancellationToken = default);

    #endregion

    #region Testing Analytics

    /// <summary>
    /// Retrieves comprehensive testing analytics for organizational experimentation insights.
    /// Provides portfolio-level insights into experimentation effectiveness and optimization opportunities.
    /// </summary>
    /// <param name="organizationId">Organization context for analytics scope</param>
    /// <param name="DateTimeRange">Time range for analytics data collection</param>
    /// <param name="entityType">Optional filter by target entity type</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive testing analytics with insights and recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when organization ID or date range is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when requestor lacks testing analytics access</exception>
    /// <remarks>
    /// <para><strong>Analytics Categories:</strong></para>
    /// <list type="bullet">
    /// <item><description>Experiment portfolio performance and success rates</description></item>
    /// <item><description>Statistical power analysis and experimental design effectiveness</description></item>
    /// <item><description>Metric performance trends and optimization opportunities</description></item>
    /// <item><description>Resource utilization and experimentation cost analysis</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Aggregates data from all experiments within the specified scope</description></item>
    /// <item><description>Implements meta-analysis techniques for portfolio insights</description></item>
    /// <item><description>Provides predictive analytics for future experimentation success</description></item>
    /// <item><description>Supports executive dashboards and strategic decision making</description></item>
    /// </list>
    /// </remarks>
    Task<TestingAnalytics> GetTestingAnalyticsAsync(
        Guid organizationId,
        DateTimeRange DateTimeRange,
        TestEntityType? entityType = null,
        CancellationToken cancellationToken = default);

    #endregion
}

/// <summary>
/// Configuration for experiment variant with parameters and allocation settings.
/// </summary>
public record ExperimentVariant(
    string Name,
    string Description,
    double TrafficAllocation, // Percentage of traffic (0.0 to 1.0)
    object Configuration, // Variant-specific configuration (prompt, parameters, etc.)
    IEnumerable<string>? Tags = null);

/// <summary>
/// Success metric definition for experiment evaluation.
/// </summary>
public record SuccessMetric(
    string Name,
    string Description,
    MetricType Type,
    string Unit,
    MetricDirection Direction, // Higher or Lower is better
    double? TargetValue = null,
    double? MinimumDetectableEffect = null);

/// <summary>
/// Statistical configuration for experiment design and analysis.
/// </summary>
public record StatisticalConfiguration(
    double SignificanceLevel, // Alpha level (e.g., 0.05)
    double Power, // Statistical power (e.g., 0.8)
    StatisticalTest PrimaryTest,
    bool UseMultipleComparisonCorrection,
    bool EnableEarlyStopping,
    int? MinimumSampleSize = null,
    int? MaximumSampleSize = null);

/// <summary>
/// Summary information for experiment listing and discovery.
/// </summary>
public record ExperimentSummary(
    Guid Id,
    string Name,
    string Description,
    ABTestStatus Status,
    TestEntityType EntityType,
    Guid EntityId,
    int VariantCount,
    DateTimeOffset CreatedAt,
    string CreatedBy,
    DateTimeOffset? StartedAt,
    DateTimeOffset? CompletedAt,
    ExperimentProgress? Progress);

/// <summary>
/// Progress information for running experiments.
/// </summary>
public record ExperimentProgress(
    int TotalSamples,
    int TargetSamples,
    double CompletionPercentage,
    TimeSpan EstimatedTimeRemaining,
    bool HasSignificantResults);

/// <summary>
/// Result of experiment start operation with activation status.
/// </summary>
public record ExperimentStartResult(
    bool Success,
    string? ErrorMessage,
    DateTimeOffset StartedAt,
    ExperimentConfiguration ActiveConfiguration,
    IEnumerable<string> ValidationWarnings);

/// <summary>
/// Active experiment configuration at the time of start.
/// </summary>
public record ExperimentConfiguration(
    IEnumerable<ABTestVariant> Variants,
    IEnumerable<SuccessMetric> SuccessMetrics,
    StatisticalConfiguration StatisticalConfig,
    RandomizationSeed RandomizationSeed);

/// <summary>
/// Randomization seed for consistent experiment allocation.
/// </summary>
public record RandomizationSeed(
    string SeedValue,
    string Algorithm,
    DateTimeOffset GeneratedAt);

/// <summary>
/// Result of experiment stop operation with final analysis.
/// </summary>
public record ExperimentStopResult(
    bool Success,
    string? ErrorMessage,
    DateTimeOffset StoppedAt,
    ExperimentAnalysisResult FinalAnalysis,
    IEnumerable<ExperimentRecommendation> Recommendations);

/// <summary>
/// Experiment recommendation based on results analysis.
/// </summary>
public record ExperimentRecommendation(
    string Type,
    string Title,
    string Description,
    string Confidence,
    string Impact,
    IEnumerable<string> ActionItems);

/// <summary>
/// Result of variant removal operation with impact analysis.
/// </summary>
public record VariantRemovalResult(
    bool Success,
    string? ErrorMessage,
    DateTimeOffset RemovedAt,
    TrafficReallocation TrafficReallocation,
    StatisticalImpactAnalysis ImpactAnalysis);

/// <summary>
/// Traffic reallocation after variant removal.
/// </summary>
public record TrafficReallocation(
    IEnumerable<VariantAllocation> NewAllocations,
    string ReallocationMethod,
    DateTimeOffset EffectiveAt);

/// <summary>
/// Variant traffic allocation information.
/// </summary>
public record VariantAllocation(
    Guid VariantId,
    string VariantName,
    double PreviousAllocation,
    double NewAllocation);

/// <summary>
/// Statistical impact analysis for experiment changes.
/// </summary>
public record StatisticalImpactAnalysis(
    double PowerChange,
    int SampleSizeImpact,
    IEnumerable<string> Recommendations,
    bool RequiresRestart);

/// <summary>
/// Recorded experiment result with validation and metadata.
/// </summary>
public record ExperimentResultRecord(
    Guid Id,
    Guid ExperimentId,
    Guid VariantId,
    string SessionId,
    IEnumerable<MetricValue> MetricValues,
    DateTimeOffset RecordedAt,
    bool IsValid,
    object? Metadata);

/// <summary>
/// Metric value with measurement and context.
/// </summary>
public record MetricValue(
    string MetricName,
    object Value,
    MetricType Type,
    DateTimeOffset MeasuredAt,
    object? Context = null);

/// <summary>
/// Comprehensive statistical analysis result for experiment evaluation.
/// </summary>
public record ExperimentAnalysisResult(
    Guid ExperimentId,
    StatisticalAnalysisType AnalysisType,
    DateTimeOffset AnalyzedAt,
    ExperimentSummaryStatistics SummaryStatistics,
    IEnumerable<VariantAnalysis> VariantAnalyses,
    IEnumerable<MetricComparison> MetricComparisons,
    StatisticalSignificance OverallSignificance,
    IEnumerable<AnalysisInsight> Insights,
    IEnumerable<AnalysisRecommendation> Recommendations);

/// <summary>
/// Summary statistics for the entire experiment.
/// </summary>
public record ExperimentSummaryStatistics(
    int TotalSamples,
    TimeSpan Duration,
    double OverallConversionRate,
    double StatisticalPower,
    ConfidenceInterval PowerConfidenceInterval);

/// <summary>
/// Statistical analysis for a specific variant.
/// </summary>
public record VariantAnalysis(
    Guid VariantId,
    string VariantName,
    int SampleSize,
    IEnumerable<MetricStatistics> MetricStatistics,
    VariantPerformance Performance);

/// <summary>
/// Statistical metrics for a specific success metric.
/// </summary>
public record MetricStatistics(
    string MetricName,
    double Mean,
    double StandardDeviation,
    double Median,
    ConfidenceInterval ConfidenceInterval,
    object? AdditionalStatistics);

/// <summary>
/// Performance assessment for a variant.
/// </summary>
public record VariantPerformance(
    string PerformanceRating, // "Excellent", "Good", "Average", "Poor"
    double RelativePerformance, // Relative to control variant
    IEnumerable<string> StrengthAreas,
    IEnumerable<string> ImprovementAreas);

/// <summary>
/// Statistical comparison between variants for a specific metric.
/// </summary>
public record MetricComparison(
    string MetricName,
    Guid ControlVariantId,
    Guid TreatmentVariantId,
    StatisticalTestResult TestResult,
    EffectSize EffectSize,
    PracticalSignificance PracticalSignificance);

/// <summary>
/// Result of statistical hypothesis testing.
/// </summary>
public record StatisticalTestResult(
    StatisticalTest TestType,
    double TestStatistic,
    double PValue,
    bool IsSignificant,
    ConfidenceInterval ConfidenceInterval,
    string Interpretation);

/// <summary>
/// Effect size measurement for practical significance assessment.
/// </summary>
public record EffectSize(
    EffectSizeMeasure Measure,
    double Value,
    EffectSizeMagnitude Magnitude,
    ConfidenceInterval ConfidenceInterval);

/// <summary>
/// Assessment of practical significance beyond statistical significance.
/// </summary>
public record PracticalSignificance(
    bool IsPracticallySignificant,
    string BusinessImpact,
    double CostBenefitRatio,
    string Recommendation);

/// <summary>
/// Overall statistical significance assessment for the experiment.
/// </summary>
public record StatisticalSignificance(
    bool HasSignificantResults,
    IEnumerable<string> SignificantMetrics,
    Guid? WinningVariantId,
    double Confidence,
    string Interpretation);

/// <summary>
/// Statistical confidence interval.
/// </summary>
public record ConfidenceInterval(
    double LowerBound,
    double UpperBound,
    double ConfidenceLevel);

/// <summary>
/// Analytical insight derived from experiment results.
/// </summary>
public record AnalysisInsight(
    string Type,
    string Title,
    string Description,
    string Confidence,
    IEnumerable<string> SupportingEvidence);

/// <summary>
/// Analysis-based recommendation for future actions.
/// </summary>
public record AnalysisRecommendation(
    string Type,
    string Title,
    string Description,
    string Priority,
    string ExpectedImpact,
    IEnumerable<string> ActionSteps);

/// <summary>
/// Real-time experiment monitoring data with current status and metrics.
/// </summary>
public record ExperimentMonitoringData(
    Guid ExperimentId,
    ABTestStatus CurrentStatus,
    DateTimeOffset LastUpdated,
    SampleSizeProgress SampleProgress,
    IEnumerable<VariantMonitoring> VariantMonitoring,
    IEnumerable<MetricMonitoring> MetricMonitoring,
    IEnumerable<MonitoringAlert> Alerts,
    ExperimentHealthStatus HealthStatus);

/// <summary>
/// Sample size progress tracking for experiment monitoring.
/// </summary>
public record SampleSizeProgress(
    int CurrentSamples,
    int TargetSamples,
    double ProgressPercentage,
    double CurrentPower,
    TimeSpan EstimatedTimeToCompletion);

/// <summary>
/// Real-time monitoring data for a specific variant.
/// </summary>
public record VariantMonitoring(
    Guid VariantId,
    string VariantName,
    int CurrentSamples,
    double ActualTrafficAllocation,
    IEnumerable<MetricMonitoring> MetricPerformance,
    VariantHealthStatus HealthStatus);

/// <summary>
/// Real-time monitoring data for a specific metric.
/// </summary>
public record MetricMonitoring(
    string MetricName,
    double CurrentValue,
    TrendDirection Trend,
    double ChangeFromBaseline,
    bool IsOutlierDetected,
    StatisticalSignificanceStatus SignificanceStatus);

/// <summary>
/// Monitoring alert for experiment anomalies or significant events.
/// </summary>
public record MonitoringAlert(
    string AlertType,
    string Severity,
    string Title,
    string Description,
    DateTimeOffset TriggeredAt,
    string RecommendedAction);

/// <summary>
/// Overall health status for experiment monitoring.
/// </summary>
public record ExperimentHealthStatus(
    string OverallHealth, // "Healthy", "Warning", "Critical"
    IEnumerable<string> HealthIndicators,
    IEnumerable<string> PotentialIssues,
    double DataQualityScore);

/// <summary>
/// Health status for individual variant monitoring.
/// </summary>
public record VariantHealthStatus(
    string Health,
    double DataQualityScore,
    bool HasSufficientSamples,
    IEnumerable<string> QualityIssues);

/// <summary>
/// Comprehensive testing analytics for organizational experimentation insights.
/// </summary>
public record TestingAnalytics(
    Guid OrganizationId,
    DateTimeRange DateTimeRange,
    ExperimentPortfolioAnalytics PortfolioAnalytics,
    StatisticalDesignAnalytics DesignAnalytics,
    MetricPerformanceAnalytics MetricPerformance,
    ResourceUtilizationAnalytics ResourceUtilization,
    IEnumerable<TestingRecommendation> Recommendations,
    DateTimeOffset GeneratedAt);

/// <summary>
/// Analytics for experiment portfolio performance and success rates.
/// </summary>
public record ExperimentPortfolioAnalytics(
    int TotalExperiments,
    int CompletedExperiments,
    int RunningExperiments,
    double SuccessRate,
    double AverageExperimentDuration,
    IEnumerable<ExperimentOutcomeDistribution> OutcomeDistribution,
    IEnumerable<EntityTypePerformance> EntityTypePerformance);

/// <summary>
/// Analytics for statistical design effectiveness and power analysis.
/// </summary>
public record StatisticalDesignAnalytics(
    double AverageStatisticalPower,
    double AverageEffectSize,
    IEnumerable<PowerAnalysisTrend> PowerTrends,
    IEnumerable<DesignEffectivenessMetric> DesignEffectiveness,
    IEnumerable<StatisticalBestPractice> BestPractices);

/// <summary>
/// Analytics for metric performance trends and optimization opportunities.
/// </summary>
public record MetricPerformanceAnalytics(
    IEnumerable<MetricTrendAnalysis> MetricTrends,
    IEnumerable<MetricCorrelationAnalysis> MetricCorrelations,
    IEnumerable<MetricOptimizationOpportunity> OptimizationOpportunities,
    double OverallMetricReliability);

/// <summary>
/// Analytics for resource utilization and experimentation cost analysis.
/// </summary>
public record ResourceUtilizationAnalytics(
    double AverageResourceUtilization,
    IEnumerable<ResourceUsagePattern> UsagePatterns,
    CostEffectivenessAnalysis CostEffectiveness,
    IEnumerable<ResourceOptimizationOpportunity> OptimizationOpportunities);

/// <summary>
/// Testing recommendation for improving experimentation effectiveness.
/// </summary>
public record TestingRecommendation(
    string Type,
    string Title,
    string Description,
    string Impact,
    string Priority,
    IEnumerable<string> ActionItems,
    string ExpectedBenefit);

/// <summary>
/// Distribution of experiment outcomes for portfolio analysis.
/// </summary>
public record ExperimentOutcomeDistribution(
    string OutcomeType,
    int Count,
    double Percentage,
    string Description);

/// <summary>
/// Performance metrics by entity type for portfolio analysis.
/// </summary>
public record EntityTypePerformance(
    TestEntityType EntityType,
    int ExperimentCount,
    double SuccessRate,
    double AverageEffectSize,
    TimeSpan AverageExperimentDuration);

/// <summary>
/// Power analysis trend over time for design analytics.
/// </summary>
public record PowerAnalysisTrend(
    DateTimeOffset TimePoint,
    double AveragePower,
    double MinimumPower,
    double MaximumPower,
    int ExperimentCount);

/// <summary>
/// Design effectiveness metric for statistical analysis.
/// </summary>
public record DesignEffectivenessMetric(
    string MetricName,
    double Score,
    string Category,
    string Description,
    IEnumerable<string> ImprovementSuggestions);

/// <summary>
/// Statistical best practice recommendation for design improvement.
/// </summary>
public record StatisticalBestPractice(
    string PracticeName,
    string Description,
    string Category,
    double AdoptionRate,
    string ImpactLevel,
    IEnumerable<string> ImplementationSteps);

/// <summary>
/// Trend analysis for specific metrics over time.
/// </summary>
public record MetricTrendAnalysis(
    string MetricName,
    TrendDirection OverallTrend,
    double TrendStrength,
    IEnumerable<MetricDataPoint> DataPoints,
    string SeasonalityPattern);

/// <summary>
/// Data point for metric trend analysis.
/// </summary>
public record MetricDataPoint(
    DateTimeOffset Timestamp,
    double Value,
    double MovingAverage,
    double Variance);

/// <summary>
/// Correlation analysis between metrics for performance insights.
/// </summary>
public record MetricCorrelationAnalysis(
    string PrimaryMetric,
    string SecondaryMetric,
    double CorrelationCoefficient,
    string CorrelationStrength,
    string Interpretation,
    bool IsStatisticallySignificant);

/// <summary>
/// Optimization opportunity for specific metrics.
/// </summary>
public record MetricOptimizationOpportunity(
    string MetricName,
    string OpportunityType,
    string Description,
    double PotentialImprovement,
    string Priority,
    IEnumerable<string> RecommendedActions);

/// <summary>
/// Resource usage pattern analysis for utilization optimization.
/// </summary>
public record ResourceUsagePattern(
    string PatternType,
    string Description,
    TimeSpan Duration,
    double Frequency,
    double ResourceIntensity,
    IEnumerable<string> OptimizationOpportunities);

/// <summary>
/// Cost effectiveness analysis for resource utilization.
/// </summary>
public record CostEffectivenessAnalysis(
    double TotalCost,
    double CostPerExperiment,
    double AverageROI,
    IEnumerable<CostBreakdown> CostBreakdowns,
    IEnumerable<string> CostOptimizationRecommendations);

/// <summary>
/// Cost breakdown by category for analysis.
/// </summary>
public record CostBreakdown(
    string Category,
    double Amount,
    double Percentage,
    string Description);

/// <summary>
/// Resource optimization opportunity for efficiency improvement.
/// </summary>
public record ResourceOptimizationOpportunity(
    string OpportunityType,
    string Description,
    double PotentialSavings,
    string ImplementationEffort,
    TimeSpan ExpectedTimeframe,
    IEnumerable<string> ActionSteps);

/// <summary>
/// Enumeration of metric types for success measurement.
/// </summary>
public enum MetricType
{
    Continuous,
    Categorical,
    Binary,
    Count,
    Rate,
    Duration
}

/// <summary>
/// Enumeration of metric improvement direction.
/// </summary>
public enum MetricDirection
{
    Higher,
    Lower
}

/// <summary>
/// Enumeration of statistical tests for hypothesis testing.
/// </summary>
public enum StatisticalTest
{
    TTest,
    ChiSquare,
    MannWhitneyU,
    KruskalWallis,
    FisherExact,
    ANOVA
}

/// <summary>
/// Enumeration of statistical analysis approaches.
/// </summary>
public enum StatisticalAnalysisType
{
    Frequentist,
    Bayesian,
    Sequential,
    Multivariate
}

/// <summary>
/// Enumeration of effect size measures.
/// </summary>
public enum EffectSizeMeasure
{
    CohenD,
    HedgeG,
    GlassD,
    R,
    EtaSquared,
    CramerV
}

/// <summary>
/// Enumeration of effect size magnitude classifications.
/// </summary>
public enum EffectSizeMagnitude
{
    Negligible,
    Small,
    Medium,
    Large,
    VeryLarge
}

/// <summary>
/// Enumeration of trend directions for monitoring.
/// </summary>
public enum TrendDirection
{
    Increasing,
    Decreasing,
    Stable,
    Volatile
}

/// <summary>
/// Enumeration of statistical significance status for real-time monitoring.
/// </summary>
public enum StatisticalSignificanceStatus
{
    NotSignificant,
    TrendingSignificant,
    Significant,
    HighlySignificant
}
