namespace PromptStudio.Core.Domain;

public class QualityMetrics
{   
    public double AverageRating { get; set; }
    public long TotalRatings { get; set; }
    public double AverageFeedbackLength { get; set; }
    public long TotalFeedbackCount { get; set; }
    public Dictionary<string, long> RatingsByCategory { get; set; } = new();
    public Dictionary<string, long> FeedbackByCategory { get; set; } = new();
}