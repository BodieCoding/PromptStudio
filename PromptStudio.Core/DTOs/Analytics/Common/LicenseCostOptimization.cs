namespace PromptStudio.Core.DTOs.Analytics;

public class LicenseCostOptimization
{
    public string SoftwareLicense { get; set; } = string.Empty;
    public int CurrentLicenseCount { get; set; }
    public int OptimalLicenseCount { get; set; }
    public decimal CurrentCost { get; set; }
    public decimal OptimizedCost { get; set; }
}
