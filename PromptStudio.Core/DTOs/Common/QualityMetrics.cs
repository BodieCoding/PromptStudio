namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Quality metrics
/// </summary>
public class QualityMetrics
{
    public double AverageScore { get; set; }
    public double MedianScore { get; set; }
    public double StandardDeviation { get; set; }
    public double MinScore { get; set; }
    public double MaxScore { get; set; }
    public Dictionary<string, int> ScoreDistribution { get; set; } = new();
}

/// <summary>
/// Token usage information
/// </summary>
public class TokenUsageInfo
{
    public int PromptTokens { get; set; }
    public int CompletionTokens { get; set; }
    public int TotalTokens { get; set; }
    public decimal? Cost { get; set; }
}

/// <summary>
/// Enhanced paged result
/// </summary>
public class EnhancedPagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Skip { get; set; }
    public int Take { get; set; }
    public long TotalCount { get; set; }
    public bool HasMore => Skip + Take < TotalCount;
    public Dictionary<string, object> Metadata { get; set; } = new();
}
