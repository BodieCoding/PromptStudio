using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.DTOs.Execution;

/// <summary>
/// Comprehensive comparison of AI model performance across different metrics
/// </summary>
public class ModelPerformanceComparison
{
    /// <summary>
    /// Time range for the analysis
    /// </summary>
    public DateTimeRange TimeRange { get; set; } = new();

    /// <summary>
    /// Performance data for each model
    /// </summary>
    public List<ModelPerformanceData> ModelPerformances { get; set; } = [];

    /// <summary>
    /// Overall performance summary
    /// </summary>
    public ModelPerformanceSummary Summary { get; set; } = new();

    /// <summary>
    /// Performance rankings by different criteria
    /// </summary>
    public ModelPerformanceRankings Rankings { get; set; } = new();

    /// <summary>
    /// Statistical significance of performance differences
    /// </summary>
    public List<ModelComparisonSignificance>? StatisticalSignificance { get; set; }
}

/// <summary>
/// Performance data for a specific model
/// </summary>
public class ModelPerformanceData
{
    /// <summary>
    /// Model identifier
    /// </summary>
    public string ModelName { get; set; } = string.Empty;

    /// <summary>
    /// AI provider name
    /// </summary>
    public string ProviderName { get; set; } = string.Empty;

    /// <summary>
    /// Total number of executions
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Success rate (0-1)
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Average response time in milliseconds
    /// </summary>
    public double AverageResponseTimeMs { get; set; }

    /// <summary>
    /// Median response time in milliseconds
    /// </summary>
    public double MedianResponseTimeMs { get; set; }

    /// <summary>
    /// 95th percentile response time
    /// </summary>
    public double P95ResponseTimeMs { get; set; }

    /// <summary>
    /// Average quality score
    /// </summary>
    public double AverageQualityScore { get; set; }

    /// <summary>
    /// Quality score standard deviation
    /// </summary>
    public double QualityScoreStdDev { get; set; }

    /// <summary>
    /// Total cost for all executions
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Average cost per execution
    /// </summary>
    public decimal AverageCostPerExecution { get; set; }

    /// <summary>
    /// Cost per token (if applicable)
    /// </summary>
    public decimal? CostPerToken { get; set; }

    /// <summary>
    /// Average tokens per execution
    /// </summary>
    public double AverageTokensPerExecution { get; set; }

    /// <summary>
    /// Token usage breakdown
    /// </summary>
    public TokenUsage TokenUsage { get; set; } = new();

    /// <summary>
    /// Error rate (0-1)
    /// </summary>
    public double ErrorRate { get; set; }

    /// <summary>
    /// Common error types and their frequencies
    /// </summary>
    public Dictionary<string, int> ErrorTypes { get; set; } = [];

    /// <summary>
    /// Performance over time
    /// </summary>
    public List<ModelPerformanceTimePoint>? TimeSeriesData { get; set; }
}

/// <summary>
/// Performance summary across all models
/// </summary>
public class ModelPerformanceSummary
{
    /// <summary>
    /// Total executions across all models
    /// </summary>
    public long TotalExecutions { get; set; }

    /// <summary>
    /// Best performing model by response time
    /// </summary>
    public string? FastestModel { get; set; }

    /// <summary>
    /// Best performing model by quality score
    /// </summary>
    public string? HighestQualityModel { get; set; }

    /// <summary>
    /// Most cost-effective model
    /// </summary>
    public string? MostCostEffectiveModel { get; set; }

    /// <summary>
    /// Most reliable model (highest success rate)
    /// </summary>
    public string? MostReliableModel { get; set; }

    /// <summary>
    /// Overall performance leader
    /// </summary>
    public string? OverallLeader { get; set; }

    /// <summary>
    /// Performance improvement recommendations
    /// </summary>
    public List<string> Recommendations { get; set; } = [];
}

/// <summary>
/// Model rankings by different performance criteria
/// </summary>
public class ModelPerformanceRankings
{
    /// <summary>
    /// Ranking by response time (fastest first)
    /// </summary>
    public List<ModelRanking> ByResponseTime { get; set; } = [];

    /// <summary>
    /// Ranking by quality score (highest first)
    /// </summary>
    public List<ModelRanking> ByQualityScore { get; set; } = [];

    /// <summary>
    /// Ranking by cost effectiveness (most cost-effective first)
    /// </summary>
    public List<ModelRanking> ByCostEffectiveness { get; set; } = [];

    /// <summary>
    /// Ranking by reliability (most reliable first)
    /// </summary>
    public List<ModelRanking> ByReliability { get; set; } = [];

    /// <summary>
    /// Overall composite ranking
    /// </summary>
    public List<ModelRanking> Overall { get; set; } = [];
}

/// <summary>
/// Model ranking entry
/// </summary>
public class ModelRanking
{
    /// <summary>
    /// Rank position (1-based)
    /// </summary>
    public int Rank { get; set; }

    /// <summary>
    /// Model name
    /// </summary>
    public string ModelName { get; set; } = string.Empty;

    /// <summary>
    /// Provider name
    /// </summary>
    public string ProviderName { get; set; } = string.Empty;

    /// <summary>
    /// Score for this ranking criterion
    /// </summary>
    public double Score { get; set; }

    /// <summary>
    /// Relative performance compared to #1 (0-1)
    /// </summary>
    public double RelativePerformance { get; set; }
}

/// <summary>
/// Performance data point over time for a model
/// </summary>
public class ModelPerformanceTimePoint
{
    public DateTime Timestamp { get; set; }
    public double AverageResponseTimeMs { get; set; }
    public double AverageQualityScore { get; set; }
    public decimal AverageCostPerExecution { get; set; }
    public double SuccessRate { get; set; }
    public long ExecutionCount { get; set; }
}

/// <summary>
/// Statistical significance test results between models
/// </summary>
public class ModelComparisonSignificance
{
    /// <summary>
    /// First model being compared
    /// </summary>
    public string Model1 { get; set; } = string.Empty;

    /// <summary>
    /// Second model being compared
    /// </summary>
    public string Model2 { get; set; } = string.Empty;

    /// <summary>
    /// Metric being compared
    /// </summary>
    public string Metric { get; set; } = string.Empty;

    /// <summary>
    /// P-value from statistical test
    /// </summary>
    public double PValue { get; set; }

    /// <summary>
    /// Whether the difference is statistically significant
    /// </summary>
    public bool IsSignificant { get; set; }

    /// <summary>
    /// Confidence level used for the test
    /// </summary>
    public double ConfidenceLevel { get; set; } = 0.95;
}
