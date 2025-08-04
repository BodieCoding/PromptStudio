namespace PromptStudio.Core.DTOs.Statistics;

/// <summary>
/// Represents comprehensive statistics specific to library execution operations.
/// Extends execution statistics with library-focused metrics and performance data for library management and optimization.
/// </summary>
/// <remarks>
/// <para><strong>Library-Specific Analytics:</strong></para>
/// <para>Specialized statistics container that focuses on library-level execution patterns,
/// template usage distribution, collection performance, and library health metrics.
/// Essential for library management, usage optimization, and content curation decisions.</para>
/// 
/// <para><strong>Usage Scenarios:</strong></para>
/// <list type="bullet">
/// <item><description>Library management dashboard and monitoring</description></item>
/// <item><description>Template popularity and usage analytics</description></item>
/// <item><description>Collection performance assessment and optimization</description></item>
/// <item><description>Library content curation and quality metrics</description></item>
/// </list>
/// 
/// <para><strong>Inheritance Pattern:</strong></para>
/// <para>Inherits all functionality from ExecutionStatistics while adding library-specific
/// metrics and analysis capabilities for comprehensive library performance assessment.</para>
/// </remarks>
public class LibraryExecutionStatistics : ExecutionStatistics
{
    /// <summary>
    /// Gets or sets the unique identifier of the library these statistics represent.
    /// Links the statistics to a specific library for tracking and reporting.
    /// </summary>
    /// <value>
    /// The library identifier, or null if representing aggregate statistics.
    /// </value>
    public Guid? LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the name of the library for display and reporting purposes.
    /// Provides human-readable identification for the statistics context.
    /// </summary>
    /// <value>
    /// The library name, or null if not specified.
    /// </value>
    public string? LibraryName { get; set; }

    /// <summary>
    /// Gets or sets the total number of prompt templates in the library.
    /// Provides context for template utilization and coverage analysis.
    /// </summary>
    /// <value>
    /// The total template count in the library. Must be non-negative.
    /// </value>
    public long TotalTemplatesInLibrary { get; set; }

    /// <summary>
    /// Gets or sets the number of templates that have been executed at least once.
    /// Indicates library adoption and template utilization rates.
    /// </summary>
    /// <value>
    /// The count of executed templates. Must be non-negative and not exceed TotalTemplatesInLibrary.
    /// </value>
    public long ExecutedTemplates { get; set; }

    /// <summary>
    /// Gets the percentage of templates in the library that have been executed.
    /// Calculated property providing library utilization metrics.
    /// </summary>
    /// <value>
    /// The template utilization rate as a percentage (0-100).
    /// </value>
    public double TemplateUtilizationRate => TotalTemplatesInLibrary > 0 
        ? (double)ExecutedTemplates / TotalTemplatesInLibrary * 100 
        : 0;

    /// <summary>
    /// Gets or sets the number of templates that have never been executed.
    /// Identifies unused content for potential optimization or removal.
    /// </summary>
    /// <value>
    /// The count of unused templates. Must be non-negative.
    /// </value>
    public long UnusedTemplates => TotalTemplatesInLibrary - ExecutedTemplates;

    /// <summary>
    /// Gets or sets the total number of collections in the library.
    /// Provides organization structure context for the statistics.
    /// </summary>
    /// <value>
    /// The total collection count. Must be non-negative.
    /// </value>
    public long TotalCollections { get; set; }

    /// <summary>
    /// Gets or sets the number of collections that have had templates executed.
    /// Indicates collection-level usage patterns and organization effectiveness.
    /// </summary>
    /// <value>
    /// The count of active collections. Must be non-negative and not exceed TotalCollections.
    /// </value>
    public long ActiveCollections { get; set; }

    /// <summary>
    /// Gets the percentage of collections that have active template usage.
    /// Calculated property providing collection utilization metrics.
    /// </summary>
    /// <value>
    /// The collection utilization rate as a percentage (0-100).
    /// </value>
    public double CollectionUtilizationRate => TotalCollections > 0 
        ? (double)ActiveCollections / TotalCollections * 100 
        : 0;

    /// <summary>
    /// Gets or sets the collection of template execution counts by template ID.
    /// Provides detailed usage distribution for individual template analysis.
    /// </summary>
    /// <value>
    /// A dictionary mapping template IDs to their execution counts.
    /// </value>
    public Dictionary<int, long> TemplateExecutionCounts { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of execution counts by collection ID.
    /// Provides collection-level usage analysis and performance metrics.
    /// </summary>
    /// <value>
    /// A dictionary mapping collection IDs to their total execution counts.
    /// </value>
    public Dictionary<int, long> CollectionExecutionCounts { get; set; } = new();

    /// <summary>
    /// Gets or sets the most frequently executed template in the library.
    /// Identifies the most popular content for feature promotion or analysis.
    /// </summary>
    /// <value>
    /// Information about the most popular template, or null if no executions.
    /// </value>
    public TemplatePopularityInfo? MostPopularTemplate { get; set; }

    /// <summary>
    /// Gets or sets the collection of top N most popular templates.
    /// Provides ranking information for popular content identification.
    /// </summary>
    /// <value>
    /// A list of template popularity information ordered by execution count.
    /// </value>
    public List<TemplatePopularityInfo> TopPopularTemplates { get; set; } = new();

    /// <summary>
    /// Gets or sets the average execution count per template in the library.
    /// Provides baseline usage metrics for library performance assessment.
    /// </summary>
    /// <value>
    /// The average executions per template, or 0 if no templates exist.
    /// </value>
    public double AverageExecutionsPerLibraryTemplate => TotalTemplatesInLibrary > 0 
        ? (double)TotalExecutions / TotalTemplatesInLibrary 
        : 0;

    /// <summary>
    /// Gets or sets the average execution count per collection in the library.
    /// Provides collection-level performance and organization effectiveness metrics.
    /// </summary>
    /// <value>
    /// The average executions per collection, or 0 if no collections exist.
    /// </value>
    public double AverageExecutionsPerCollection => TotalCollections > 0 
        ? (double)TotalExecutions / TotalCollections 
        : 0;

    /// <summary>
    /// Gets or sets the total number of unique contributors to the library.
    /// Indicates library collaboration and community engagement levels.
    /// </summary>
    /// <value>
    /// The count of unique contributors. Must be non-negative.
    /// </value>
    public long UniqueContributors { get; set; }

    /// <summary>
    /// Gets or sets the average success rate across all templates in the library.
    /// Provides library-wide quality metrics for content assessment.
    /// </summary>
    /// <value>
    /// The average template success rate as a percentage (0-100).
    /// </value>
    public double AverageTemplateSuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the standard deviation of template success rates.
    /// Indicates consistency of template quality across the library.
    /// </summary>
    /// <value>
    /// The standard deviation of success rates. Must be non-negative.
    /// </value>
    public double TemplateSuccessRateStandardDeviation { get; set; }

    /// <summary>
    /// Gets or sets library-specific performance metrics and custom analytics.
    /// Provides extensible storage for library domain-specific measurements.
    /// </summary>
    /// <value>
    /// A dictionary containing library-specific metrics and analytics data.
    /// </value>
    public Dictionary<string, object> LibrarySpecificMetrics { get; set; } = new();

    /// <summary>
    /// Initializes a new instance of the LibraryExecutionStatistics class with default values.
    /// Creates an empty library statistics instance ready for data population.
    /// </summary>
    public LibraryExecutionStatistics()
    {
    }

    /// <summary>
    /// Initializes a new instance of the LibraryExecutionStatistics class for a specific library and period.
    /// Creates a library statistics instance with defined context and time boundaries.
    /// </summary>
    /// <param name="libraryId">The unique identifier of the library</param>
    /// <param name="libraryName">The name of the library</param>
    /// <param name="periodStart">The start of the statistics period</param>
    /// <param name="periodEnd">The end of the statistics period</param>
    public LibraryExecutionStatistics(Guid libraryId, string libraryName, DateTime periodStart, DateTime periodEnd)
        : base(periodStart, periodEnd)
    {
        LibraryId = libraryId;
        LibraryName = libraryName;
    }

    /// <summary>
    /// Adds template execution count data to the statistics.
    /// Provides fluent API for accumulating template-level usage data.
    /// </summary>
    /// <param name="templateId">The template identifier</param>
    /// <param name="executionCount">The number of executions for this template</param>
    /// <returns>This LibraryExecutionStatistics instance for method chaining</returns>
    public LibraryExecutionStatistics AddTemplateExecutionCount(int templateId, long executionCount)
    {
        TemplateExecutionCounts[templateId] = executionCount;
        return this;
    }

    /// <summary>
    /// Adds collection execution count data to the statistics.
    /// Provides fluent API for accumulating collection-level usage data.
    /// </summary>
    /// <param name="collectionId">The collection identifier</param>
    /// <param name="executionCount">The number of executions for this collection</param>
    /// <returns>This LibraryExecutionStatistics instance for method chaining</returns>
    public LibraryExecutionStatistics AddCollectionExecutionCount(int collectionId, long executionCount)
    {
        CollectionExecutionCounts[collectionId] = executionCount;
        return this;
    }

    /// <summary>
    /// Adds a template to the popular templates list.
    /// Provides fluent API for building popularity rankings.
    /// </summary>
    /// <param name="templateInfo">The template popularity information</param>
    /// <returns>This LibraryExecutionStatistics instance for method chaining</returns>
    public LibraryExecutionStatistics AddPopularTemplate(TemplatePopularityInfo templateInfo)
    {
        TopPopularTemplates.Add(templateInfo);
        
        // Update most popular if this is the highest
        if (MostPopularTemplate == null || templateInfo.ExecutionCount > MostPopularTemplate.ExecutionCount)
        {
            MostPopularTemplate = templateInfo;
        }
        
        return this;
    }

    /// <summary>
    /// Adds a library-specific metric to the statistics.
    /// Provides fluent API for accumulating library domain-specific analytics.
    /// </summary>
    /// <param name="metricName">The metric name</param>
    /// <param name="value">The metric value</param>
    /// <returns>This LibraryExecutionStatistics instance for method chaining</returns>
    public LibraryExecutionStatistics AddLibraryMetric(string metricName, object value)
    {
        LibrarySpecificMetrics[metricName] = value;
        return this;
    }

    /// <summary>
    /// Calculates and updates the template popularity rankings.
    /// Sorts the popular templates list by execution count in descending order.
    /// </summary>
    /// <param name="maxCount">The maximum number of templates to include in the ranking</param>
    /// <returns>This LibraryExecutionStatistics instance for method chaining</returns>
    public LibraryExecutionStatistics UpdatePopularityRankings(int maxCount = 10)
    {
        TopPopularTemplates = TopPopularTemplates
            .OrderByDescending(t => t.ExecutionCount)
            .Take(maxCount)
            .ToList();
            
        MostPopularTemplate = TopPopularTemplates.FirstOrDefault();
        
        return this;
    }

    /// <summary>
    /// Validates the library statistical data for consistency and correctness.
    /// Ensures all counts and calculations are logically valid for library context.
    /// </summary>
    /// <returns>A list of validation error messages, empty if data is valid</returns>
    public new List<string> Validate()
    {
        var errors = base.Validate();

        if (TotalTemplatesInLibrary < 0)
            errors.Add("TotalTemplatesInLibrary cannot be negative");

        if (ExecutedTemplates < 0)
            errors.Add("ExecutedTemplates cannot be negative");

        if (ExecutedTemplates > TotalTemplatesInLibrary)
            errors.Add("ExecutedTemplates cannot exceed TotalTemplatesInLibrary");

        if (TotalCollections < 0)
            errors.Add("TotalCollections cannot be negative");

        if (ActiveCollections < 0)
            errors.Add("ActiveCollections cannot be negative");

        if (ActiveCollections > TotalCollections)
            errors.Add("ActiveCollections cannot exceed TotalCollections");

        if (UniqueContributors < 0)
            errors.Add("UniqueContributors cannot be negative");

        if (AverageTemplateSuccessRate < 0 || AverageTemplateSuccessRate > 100)
            errors.Add("AverageTemplateSuccessRate must be between 0 and 100");

        if (TemplateSuccessRateStandardDeviation < 0)
            errors.Add("TemplateSuccessRateStandardDeviation cannot be negative");

        return errors;
    }

    /// <summary>
    /// Creates a comprehensive summary report of the library execution statistics.
    /// Provides formatted text summary suitable for library management and reporting.
    /// </summary>
    /// <returns>A formatted string containing library-specific statistics and metrics</returns>
    public new string CreateSummaryReport()
    {
        var baseReport = base.CreateSummaryReport();
        var libraryReport = $"Library-Specific Statistics:\n" +
                           $"  Library: {LibraryName ?? "Unknown"} (ID: {LibraryId})\n" +
                           $"  Total Templates: {TotalTemplatesInLibrary:N0}\n" +
                           $"  Template Utilization: {TemplateUtilizationRate:F1}% ({ExecutedTemplates:N0} used)\n" +
                           $"  Unused Templates: {UnusedTemplates:N0}\n" +
                           $"  Total Collections: {TotalCollections:N0}\n" +
                           $"  Collection Utilization: {CollectionUtilizationRate:F1}% ({ActiveCollections:N0} active)\n" +
                           $"  Avg Executions/Template: {AverageExecutionsPerLibraryTemplate:F2}\n" +
                           $"  Avg Executions/Collection: {AverageExecutionsPerCollection:F2}\n" +
                           $"  Contributors: {UniqueContributors:N0}\n" +
                           $"  Avg Template Success Rate: {AverageTemplateSuccessRate:F1}% (±{TemplateSuccessRateStandardDeviation:F1}%)\n" +
                           $"  Most Popular Template: {MostPopularTemplate?.TemplateName ?? "None"} ({MostPopularTemplate?.ExecutionCount:N0} executions)";
        
        return $"{baseReport}\n\n{libraryReport}";
    }

    /// <summary>
    /// Returns a string representation of the library execution statistics for debugging and logging.
    /// Provides essential library information in a readable format.
    /// </summary>
    /// <returns>String representation including library context and key metrics</returns>
    public override string ToString()
    {
        return $"LibraryExecutionStatistics: {LibraryName} - {TotalExecutions:N0} executions " +
               $"({TemplateUtilizationRate:F1}% template utilization) from {PeriodStart:yyyy-MM-dd} to {PeriodEnd:yyyy-MM-dd}";
    }
}

/// <summary>
/// Represents popularity information for a specific template within library statistics.
/// Provides detailed metrics about template usage and performance.
/// </summary>
public class TemplatePopularityInfo
{
    /// <summary>
    /// Gets or sets the unique identifier of the template.
    /// </summary>
    public int TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the name of the template for display purposes.
    /// </summary>
    public string? TemplateName { get; set; }

    /// <summary>
    /// Gets or sets the total number of executions for this template.
    /// </summary>
    public long ExecutionCount { get; set; }

    /// <summary>
    /// Gets or sets the success rate for this template as a percentage.
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution duration for this template.
    /// </summary>
    public TimeSpan? AverageExecutionDuration { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last execution for this template.
    /// </summary>
    public DateTime? LastExecuted { get; set; }

    /// <summary>
    /// Gets or sets the rank of this template in the popularity list.
    /// </summary>
    public int Rank { get; set; }

    /// <summary>
    /// Gets or sets the collection ID that contains this template.
    /// </summary>
    public int? CollectionId { get; set; }

    /// <summary>
    /// Gets or sets the name of the collection that contains this template.
    /// </summary>
    public string? CollectionName { get; set; }
}
