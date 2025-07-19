using System.ComponentModel.DataAnnotations;

using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Variables;

/// <summary>
/// Analytics result for variable collection performance and usage
/// </summary>
public class CollectionAnalyticsResult
{
    /// <summary>
    /// Gets or sets the collection ID
    /// </summary>
    public Guid CollectionId { get; set; }

    /// <summary>
    /// Gets or sets the collection name
    /// </summary>
    public string CollectionName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the analysis period
    /// </summary>
    public DateTimeRange AnalysisPeriod { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of executions
    /// </summary>
    public int TotalExecutions { get; set; }

    /// <summary>
    /// Gets or sets the success rate (0.0 to 1.0)
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Gets or sets the average execution time in milliseconds
    /// </summary>
    public double AverageExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the median execution time in milliseconds
    /// </summary>
    public double MedianExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the fastest execution time in milliseconds
    /// </summary>
    public double FastestExecution { get; set; }

    /// <summary>
    /// Gets or sets the slowest execution time in milliseconds
    /// </summary>
    public double SlowestExecution { get; set; }

    /// <summary>
    /// Gets or sets execution counts by day
    /// </summary>
    public Dictionary<DateTime, int> ExecutionsByDay { get; set; } = new();

    /// <summary>
    /// Gets or sets success rates by day
    /// </summary>
    public Dictionary<DateTime, double> SuccessRateByDay { get; set; } = new();

    /// <summary>
    /// Gets or sets the most frequently used variables
    /// </summary>
    public Dictionary<string, int> VariableUsageFrequency { get; set; } = new();

    /// <summary>
    /// Gets or sets common error types and their frequencies
    /// </summary>
    public Dictionary<string, int> ErrorFrequency { get; set; } = new();

    /// <summary>
    /// Gets or sets performance trends over time
    /// </summary>
    public Dictionary<DateTime, double> PerformanceTrend { get; set; } = new();

    /// <summary>
    /// Gets or sets usage patterns and insights
    /// </summary>
    public List<string> Insights { get; set; } = new();

    /// <summary>
    /// Gets or sets recommendations for optimization
    /// </summary>
    public List<string> Recommendations { get; set; } = new();

    /// <summary>
    /// Gets or sets additional analytical metadata
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}
