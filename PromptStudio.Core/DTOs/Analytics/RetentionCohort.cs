namespace PromptStudio.Core.DTOs.Analytics;

public class RetentionCohort
{
    public DateTime CohortDate { get; set; }
    public long InitialUsers { get; set; }
    public List<double> RetentionRates { get; set; } = []; // By period
}
