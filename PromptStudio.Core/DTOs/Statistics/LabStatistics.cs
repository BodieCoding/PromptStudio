namespace PromptStudio.Core.DTOs.Statistics;

/// <summary>
/// Represents comprehensive statistics specific to lab environment operations and experiments.
/// Provides detailed metrics and analytics for prompt experimentation, testing, and development activities within the lab context.
/// </summary>
/// <remarks>
/// <para><strong>Lab-Focused Analytics:</strong></para>
/// <para>Specialized statistics container that focuses on experimental and development activities
/// within the prompt lab environment including experiment success rates, testing patterns,
/// iteration metrics, and development productivity for research and development optimization.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>Lab productivity dashboard and experiment tracking</description></item>
/// <item><description>Research and development metrics for innovation assessment</description></item>
/// <item><description>Experimentation effectiveness analysis and methodology optimization</description></item>
/// <item><description>Developer productivity tracking and resource allocation planning</description></item>
/// </list>
/// 
/// <para><strong>Experimental Context:</strong></para>
/// <para>Designed specifically for tracking experimental workflows, iterative development processes,
/// and research activities that differ from production execution patterns and require specialized
/// metrics for effectiveness assessment and process improvement.</para>
/// </remarks>
public class LabStatistics
{
    /// <summary>
    /// Gets or sets the unique identifier of the lab these statistics represent.
    /// Links the statistics to a specific lab environment for tracking and reporting.
    /// </summary>
    /// <value>
    /// The lab identifier, or null if representing aggregate statistics.
    /// </value>
    public Guid? LabId { get; set; }

    /// <summary>
    /// Gets or sets the name of the lab for display and reporting purposes.
    /// Provides human-readable identification for the statistics context.
    /// </summary>
    /// <value>
    /// The lab name, or null if not specified.
    /// </value>
    public string? LabName { get; set; }

    /// <summary>
    /// Gets or sets the total number of experiments conducted in the lab.
    /// Provides the baseline count for experimental activity assessment.
    /// </summary>
    /// <value>
    /// The total experiment count. Must be non-negative.
    /// </value>
    public long TotalExperiments { get; set; }

    /// <summary>
    /// Gets or sets the number of successful experiments completed.
    /// Represents experiments that achieved their intended outcomes for success rate calculation.
    /// </summary>
    /// <value>
    /// The successful experiment count. Must be non-negative and not exceed TotalExperiments.
    /// </value>
    public long SuccessfulExperiments { get; set; }

    /// <summary>
    /// Gets or sets the number of failed experiments.
    /// Represents experiments that did not achieve intended outcomes for failure analysis.
    /// </summary>
    /// <value>
    /// The failed experiment count. Must be non-negative.
    /// </value>
    public long FailedExperiments { get; set; }

    /// <summary>
    /// Gets or sets the number of experiments currently in progress.
    /// Indicates active research and development workload.
    /// </summary>
    /// <value>
    /// The in-progress experiment count. Must be non-negative.
    /// </value>
    public long InProgressExperiments { get; set; }

    /// <summary>
    /// Gets the experiment success rate as a percentage of completed experiments.
    /// Calculated property providing experimental effectiveness metrics.
    /// </summary>
    /// <value>
    /// The experiment success rate percentage (0-100), or 0 if no completed experiments.
    /// </value>
    public double ExperimentSuccessRate
    {
        get
        {
            var completedExperiments = SuccessfulExperiments + FailedExperiments;
            return completedExperiments > 0 ? (double)SuccessfulExperiments / completedExperiments * 100 : 0;
        }
    }

    /// <summary>
    /// Gets or sets the total number of prompt templates developed in the lab.
    /// Indicates lab productivity and development output.
    /// </summary>
    /// <value>
    /// The total template development count. Must be non-negative.
    /// </value>
    public long TotalTemplatesDeveloped { get; set; }

    /// <summary>
    /// Gets or sets the number of templates that have been promoted to production.
    /// Indicates successful development outcomes and quality achievement.
    /// </summary>
    /// <value>
    /// The promoted template count. Must be non-negative and not exceed TotalTemplatesDeveloped.
    /// </value>
    public long TemplatesPromotedToProduction { get; set; }

    /// <summary>
    /// Gets the template promotion rate as a percentage of developed templates.
    /// Calculated property providing development quality and success metrics.
    /// </summary>
    /// <value>
    /// The template promotion rate percentage (0-100), or 0 if no templates developed.
    /// </value>
    public double TemplatePromotionRate => TotalTemplatesDeveloped > 0 
        ? (double)TemplatesPromotedToProduction / TotalTemplatesDeveloped * 100 
        : 0;

    /// <summary>
    /// Gets or sets the total number of test executions performed in the lab.
    /// Provides testing activity volume for quality assurance assessment.
    /// </summary>
    /// <value>
    /// The total test execution count. Must be non-negative.
    /// </value>
    public long TotalTestExecutions { get; set; }

    /// <summary>
    /// Gets or sets the number of successful test executions.
    /// Represents tests that passed for quality and reliability metrics.
    /// </summary>
    /// <value>
    /// The successful test count. Must be non-negative and not exceed TotalTestExecutions.
    /// </value>
    public long SuccessfulTestExecutions { get; set; }

    /// <summary>
    /// Gets the test success rate as a percentage of total test executions.
    /// Calculated property providing quality assurance effectiveness metrics.
    /// </summary>
    /// <value>
    /// The test success rate percentage (0-100), or 0 if no tests executed.
    /// </value>
    public double TestSuccessRate => TotalTestExecutions > 0 
        ? (double)SuccessfulTestExecutions / TotalTestExecutions * 100 
        : 0;

    /// <summary>
    /// Gets or sets the number of unique researchers using the lab.
    /// Indicates lab adoption and collaboration levels.
    /// </summary>
    /// <value>
    /// The unique researcher count. Must be non-negative.
    /// </value>
    public long UniqueResearchers { get; set; }

    /// <summary>
    /// Gets or sets the average number of experiments per researcher.
    /// Provides productivity metrics and workload distribution analysis.
    /// </summary>
    /// <value>
    /// The average experiments per researcher, or 0 if no researchers recorded.
    /// </value>
    public double AverageExperimentsPerResearcher => UniqueResearchers > 0 
        ? (double)TotalExperiments / UniqueResearchers 
        : 0;

    /// <summary>
    /// Gets or sets the average duration of experiments in the lab.
    /// Provides timeline and resource planning metrics for experimental workflows.
    /// </summary>
    /// <value>
    /// The average experiment duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? AverageExperimentDuration { get; set; }

    /// <summary>
    /// Gets or sets the median duration of experiments in the lab.
    /// Provides robust central tendency measure for experimental timeline analysis.
    /// </summary>
    /// <value>
    /// The median experiment duration, or null if timing data is unavailable.
    /// </value>
    public TimeSpan? MedianExperimentDuration { get; set; }

    /// <summary>
    /// Gets or sets the average number of iterations per experiment.
    /// Indicates experimental depth and iterative development patterns.
    /// </summary>
    /// <value>
    /// The average iterations per experiment. Must be non-negative.
    /// </value>
    public double AverageIterationsPerExperiment { get; set; }

    /// <summary>
    /// Gets or sets the collection of experiment categories and their counts.
    /// Provides categorization analysis for research focus and resource allocation.
    /// </summary>
    /// <value>
    /// A dictionary mapping experiment categories to occurrence counts.
    /// </value>
    public Dictionary<string, long> ExperimentCategories { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of development methodologies and their usage.
    /// Tracks methodology effectiveness and adoption patterns within the lab.
    /// </summary>
    /// <value>
    /// A dictionary mapping methodology names to usage counts.
    /// </value>
    public Dictionary<string, long> DevelopmentMethodologies { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of research tools and their usage frequency.
    /// Provides tool effectiveness analysis and resource planning insights.
    /// </summary>
    /// <value>
    /// A dictionary mapping tool names to usage frequency counts.
    /// </value>
    public Dictionary<string, long> ResearchToolUsage { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of collaboration patterns by team size.
    /// Analyzes team composition effectiveness and collaboration trends.
    /// </summary>
    /// <value>
    /// A dictionary mapping team sizes to frequency of collaborative experiments.
    /// </value>
    public Dictionary<int, long> CollaborationPatterns { get; set; } = new();

    /// <summary>
    /// Gets or sets the average resource utilization percentage for lab operations.
    /// Provides capacity planning and efficiency metrics for resource management.
    /// </summary>
    /// <value>
    /// The average resource utilization percentage (0-100). Must be non-negative.
    /// </value>
    public double AverageResourceUtilization { get; set; }

    /// <summary>
    /// Gets or sets the peak resource utilization percentage recorded.
    /// Indicates maximum capacity usage for infrastructure planning.
    /// </summary>
    /// <value>
    /// The peak resource utilization percentage (0-100). Must be non-negative.
    /// </value>
    public double PeakResourceUtilization { get; set; }

    /// <summary>
    /// Gets or sets the start time of the statistics period.
    /// Defines the beginning of the data collection timeframe.
    /// </summary>
    /// <value>
    /// The start timestamp for this statistics period.
    /// </value>
    public DateTime PeriodStart { get; set; }

    /// <summary>
    /// Gets or sets the end time of the statistics period.
    /// Defines the conclusion of the data collection timeframe.
    /// </summary>
    /// <value>
    /// The end timestamp for this statistics period.
    /// </value>
    public DateTime PeriodEnd { get; set; }

    /// <summary>
    /// Gets the duration of the statistics period.
    /// Calculated property providing the timespan covered by these statistics.
    /// </summary>
    /// <value>
    /// The duration of the statistics period.
    /// </value>
    public TimeSpan PeriodDuration => PeriodEnd - PeriodStart;

    /// <summary>
    /// Gets or sets additional lab-specific metrics and custom analytics.
    /// Provides extensible storage for lab domain-specific measurements.
    /// </summary>
    /// <value>
    /// A dictionary containing lab-specific metrics and analytics data.
    /// </value>
    public Dictionary<string, object> AdditionalLabMetrics { get; set; } = new();

    /// <summary>
    /// Gets or sets the timestamp when these statistics were last updated.
    /// Provides freshness information for data validity assessment.
    /// </summary>
    /// <value>
    /// The last update timestamp, or null if not tracked.
    /// </value>
    public DateTime? LastUpdated { get; set; }

    /// <summary>
    /// Gets or sets the collection of research outcomes and their categorization.
    /// Tracks research impact and knowledge generation effectiveness.
    /// </summary>
    /// <value>
    /// A dictionary mapping outcome categories to occurrence counts.
    /// </value>
    public Dictionary<string, long> ResearchOutcomes { get; set; } = new();

    /// <summary>
    /// Gets or sets the knowledge base contribution count from lab activities.
    /// Measures knowledge generation and documentation effectiveness.
    /// </summary>
    /// <value>
    /// The knowledge base contribution count. Must be non-negative.
    /// </value>
    public long KnowledgeBaseContributions { get; set; }

    /// <summary>
    /// Gets or sets the number of best practices identified and documented.
    /// Indicates process improvement and methodology development outcomes.
    /// </summary>
    /// <value>
    /// The best practices count. Must be non-negative.
    /// </value>
    public long BestPracticesIdentified { get; set; }

    /// <summary>
    /// Initializes a new instance of the LabStatistics class with default values.
    /// Creates an empty lab statistics instance ready for data population.
    /// </summary>
    public LabStatistics()
    {
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the LabStatistics class for a specific lab and period.
    /// Creates a lab statistics instance with defined context and time boundaries.
    /// </summary>
    /// <param name="labId">The unique identifier of the lab</param>
    /// <param name="labName">The name of the lab</param>
    /// <param name="periodStart">The start of the statistics period</param>
    /// <param name="periodEnd">The end of the statistics period</param>
    public LabStatistics(Guid labId, string labName, DateTime periodStart, DateTime periodEnd)
    {
        LabId = labId;
        LabName = labName;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds experiment category data to the statistics.
    /// Provides fluent API for accumulating categorization data.
    /// </summary>
    /// <param name="category">The experiment category name</param>
    /// <param name="count">The number of experiments in this category</param>
    /// <returns>This LabStatistics instance for method chaining</returns>
    public LabStatistics AddExperimentCategory(string category, long count)
    {
        ExperimentCategories[category] = ExperimentCategories.GetValueOrDefault(category, 0) + count;
        return this;
    }

    /// <summary>
    /// Adds development methodology usage data to the statistics.
    /// Provides fluent API for accumulating methodology effectiveness data.
    /// </summary>
    /// <param name="methodology">The methodology name</param>
    /// <param name="usageCount">The number of times this methodology was used</param>
    /// <returns>This LabStatistics instance for method chaining</returns>
    public LabStatistics AddDevelopmentMethodology(string methodology, long usageCount)
    {
        DevelopmentMethodologies[methodology] = DevelopmentMethodologies.GetValueOrDefault(methodology, 0) + usageCount;
        return this;
    }

    /// <summary>
    /// Adds research tool usage data to the statistics.
    /// Provides fluent API for accumulating tool effectiveness analysis.
    /// </summary>
    /// <param name="toolName">The research tool name</param>
    /// <param name="usageCount">The frequency of tool usage</param>
    /// <returns>This LabStatistics instance for method chaining</returns>
    public LabStatistics AddResearchToolUsage(string toolName, long usageCount)
    {
        ResearchToolUsage[toolName] = ResearchToolUsage.GetValueOrDefault(toolName, 0) + usageCount;
        return this;
    }

    /// <summary>
    /// Adds collaboration pattern data to the statistics.
    /// Provides fluent API for accumulating team composition analysis.
    /// </summary>
    /// <param name="teamSize">The team size for collaborative experiments</param>
    /// <param name="frequency">The frequency of this team size occurrence</param>
    /// <returns>This LabStatistics instance for method chaining</returns>
    public LabStatistics AddCollaborationPattern(int teamSize, long frequency)
    {
        CollaborationPatterns[teamSize] = CollaborationPatterns.GetValueOrDefault(teamSize, 0) + frequency;
        return this;
    }

    /// <summary>
    /// Adds research outcome data to the statistics.
    /// Provides fluent API for accumulating research impact data.
    /// </summary>
    /// <param name="outcomeCategory">The research outcome category</param>
    /// <param name="count">The number of outcomes in this category</param>
    /// <returns>This LabStatistics instance for method chaining</returns>
    public LabStatistics AddResearchOutcome(string outcomeCategory, long count)
    {
        ResearchOutcomes[outcomeCategory] = ResearchOutcomes.GetValueOrDefault(outcomeCategory, 0) + count;
        return this;
    }

    /// <summary>
    /// Adds a lab-specific metric to the statistics.
    /// Provides fluent API for accumulating lab domain-specific analytics.
    /// </summary>
    /// <param name="metricName">The metric name</param>
    /// <param name="value">The metric value</param>
    /// <returns>This LabStatistics instance for method chaining</returns>
    public LabStatistics AddLabMetric(string metricName, object value)
    {
        AdditionalLabMetrics[metricName] = value;
        return this;
    }

    /// <summary>
    /// Updates the last modified timestamp to the current time.
    /// Marks the statistics as recently updated for freshness tracking.
    /// </summary>
    /// <returns>This LabStatistics instance for method chaining</returns>
    public LabStatistics MarkUpdated()
    {
        LastUpdated = DateTime.UtcNow;
        return this;
    }

    /// <summary>
    /// Validates the statistical data for consistency and correctness.
    /// Ensures all counts and calculations are logically valid for lab context.
    /// </summary>
    /// <returns>A list of validation error messages, empty if data is valid</returns>
    public List<string> Validate()
    {
        var errors = new List<string>();

        if (TotalExperiments < 0)
            errors.Add("TotalExperiments cannot be negative");

        if (SuccessfulExperiments < 0)
            errors.Add("SuccessfulExperiments cannot be negative");

        if (FailedExperiments < 0)
            errors.Add("FailedExperiments cannot be negative");

        if (InProgressExperiments < 0)
            errors.Add("InProgressExperiments cannot be negative");

        if (SuccessfulExperiments + FailedExperiments + InProgressExperiments > TotalExperiments)
            errors.Add("Sum of experiment statuses cannot exceed total experiments");

        if (TotalTemplatesDeveloped < 0)
            errors.Add("TotalTemplatesDeveloped cannot be negative");

        if (TemplatesPromotedToProduction < 0)
            errors.Add("TemplatesPromotedToProduction cannot be negative");

        if (TemplatesPromotedToProduction > TotalTemplatesDeveloped)
            errors.Add("TemplatesPromotedToProduction cannot exceed TotalTemplatesDeveloped");

        if (TotalTestExecutions < 0)
            errors.Add("TotalTestExecutions cannot be negative");

        if (SuccessfulTestExecutions < 0)
            errors.Add("SuccessfulTestExecutions cannot be negative");

        if (SuccessfulTestExecutions > TotalTestExecutions)
            errors.Add("SuccessfulTestExecutions cannot exceed TotalTestExecutions");

        if (UniqueResearchers < 0)
            errors.Add("UniqueResearchers cannot be negative");

        if (AverageIterationsPerExperiment < 0)
            errors.Add("AverageIterationsPerExperiment cannot be negative");

        if (AverageResourceUtilization < 0 || AverageResourceUtilization > 100)
            errors.Add("AverageResourceUtilization must be between 0 and 100");

        if (PeakResourceUtilization < 0 || PeakResourceUtilization > 100)
            errors.Add("PeakResourceUtilization must be between 0 and 100");

        if (PeakResourceUtilization < AverageResourceUtilization)
            errors.Add("PeakResourceUtilization cannot be less than AverageResourceUtilization");

        if (PeriodEnd < PeriodStart)
            errors.Add("PeriodEnd cannot be before PeriodStart");

        if (KnowledgeBaseContributions < 0)
            errors.Add("KnowledgeBaseContributions cannot be negative");

        if (BestPracticesIdentified < 0)
            errors.Add("BestPracticesIdentified cannot be negative");

        return errors;
    }

    /// <summary>
    /// Creates a comprehensive summary report of the lab statistics.
    /// Provides formatted text summary suitable for lab management and reporting.
    /// </summary>
    /// <returns>A formatted string containing lab-specific statistics and metrics</returns>
    public string CreateSummaryReport()
    {
        var avgExperimentDuration = AverageExperimentDuration?.TotalHours.ToString("F1") ?? "Unknown";
        var medianExperimentDuration = MedianExperimentDuration?.TotalHours.ToString("F1") ?? "Unknown";
        
        return $"Lab Statistics Summary:\n" +
               $"  Lab: {LabName ?? "Unknown"} (ID: {LabId})\n" +
               $"  Period: {PeriodStart:yyyy-MM-dd} to {PeriodEnd:yyyy-MM-dd}\n" +
               $"  Experiments: {TotalExperiments:N0} total, {ExperimentSuccessRate:F1}% success rate\n" +
               $"    - Successful: {SuccessfulExperiments:N0}\n" +
               $"    - Failed: {FailedExperiments:N0}\n" +
               $"    - In Progress: {InProgressExperiments:N0}\n" +
               $"  Templates: {TotalTemplatesDeveloped:N0} developed, {TemplatePromotionRate:F1}% promoted to production\n" +
               $"  Testing: {TotalTestExecutions:N0} tests, {TestSuccessRate:F1}% success rate\n" +
               $"  Researchers: {UniqueResearchers:N0} unique, {AverageExperimentsPerResearcher:F1} avg experiments/researcher\n" +
               $"  Experiment Duration: Avg {avgExperimentDuration} hours, Median {medianExperimentDuration} hours\n" +
               $"  Iterations: {AverageIterationsPerExperiment:F1} avg per experiment\n" +
               $"  Resource Utilization: {AverageResourceUtilization:F1}% avg, {PeakResourceUtilization:F1}% peak\n" +
               $"  Knowledge: {KnowledgeBaseContributions:N0} contributions, {BestPracticesIdentified:N0} best practices\n" +
               $"  Categories: {ExperimentCategories.Count} experiment types, {DevelopmentMethodologies.Count} methodologies\n" +
               $"  Tools: {ResearchToolUsage.Count} research tools tracked\n" +
               $"  Outcomes: {ResearchOutcomes.Values.Sum():N0} total research outcomes\n" +
               $"  Last Updated: {LastUpdated:yyyy-MM-dd HH:mm:ss} UTC";
    }

    /// <summary>
    /// Returns a string representation of the lab statistics for debugging and logging.
    /// Provides essential lab information in a readable format.
    /// </summary>
    /// <returns>String representation including lab context and key metrics</returns>
    public override string ToString()
    {
        return $"LabStatistics: {LabName} - {TotalExperiments:N0} experiments " +
               $"({ExperimentSuccessRate:F1}% success, {TemplatePromotionRate:F1}% promotion rate) " +
               $"from {PeriodStart:yyyy-MM-dd} to {PeriodEnd:yyyy-MM-dd}";
    }
}
