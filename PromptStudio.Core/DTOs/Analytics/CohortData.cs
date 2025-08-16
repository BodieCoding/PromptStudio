namespace PromptStudio.Core.DTOs.Analytics;

public class CohortData
{
    public DateTime CohortDate { get; set; }
    public long CohortSize { get; set; }
    public List<double> PeriodValues { get; set; } = [];
}
