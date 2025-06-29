namespace PromptStudio.Core.Domain;

/// <summary>
/// Quality metrics domain value object
/// Used within domain entities to track quality measurements
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

// NOTE: All DTOs (EnhancedExecutionResult, TokenUsageInfo, etc.) have been moved to:
// - PromptStudio.Core.DTOs.Common.*
// - PromptStudio.Core.DTOs.Execution.*
// This file now contains only domain value objects.
