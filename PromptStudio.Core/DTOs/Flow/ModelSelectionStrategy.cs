namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Model selection strategies for execution
/// </summary>
public enum ModelSelectionStrategy
{
    Default = 0,        // Use configured model
    Fastest = 1,        // Select fastest available model
    Cheapest = 2,       // Select cheapest available model
    BestQuality = 3,    // Select highest quality model
    Balanced = 4        // Balance speed, cost, and quality
}
