namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Double range for numeric filtering
/// </summary>
public class DoubleRange
{
    /// <summary>
    /// Minimum value (inclusive)
    /// </summary>
    public double? Min { get; set; }

    /// <summary>
    /// Maximum value (inclusive)
    /// </summary>
    public double? Max { get; set; }

    /// <summary>
    /// Check if a value is within this range
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <returns>True if value is within range</returns>
    public bool Contains(double value)
    {
        if (Min.HasValue && value < Min.Value) return false;
        if (Max.HasValue && value > Max.Value) return false;
        return true;
    }

    /// <summary>
    /// Check if this range is valid (Min <= Max)
    /// </summary>
    public bool IsValid => !Min.HasValue || !Max.HasValue || Min.Value <= Max.Value;
}
