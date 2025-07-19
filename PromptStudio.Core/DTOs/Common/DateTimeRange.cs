namespace PromptStudio.Core.DTOs.Common;

/// <summary>
/// Represents a date range filter with optional start and end dates.
/// Used consistently across the application for date-based filtering and analytics.
/// </summary>
public class DateTimeRange
{
    /// <summary>
    /// Gets or sets the start date of the range.
    /// If null, represents an open-ended range from the beginning of time.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the range.
    /// If null, represents an open-ended range to the end of time.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets whether the range has a start date defined
    /// </summary>
    public bool HasStartDate => StartDate.HasValue;

    /// <summary>
    /// Gets whether the range has an end date defined
    /// </summary>
    public bool HasEndDate => EndDate.HasValue;

    /// <summary>
    /// Gets whether this is a complete range (both start and end dates are defined)
    /// </summary>
    public bool IsComplete => HasStartDate && HasEndDate;

    /// <summary>
    /// Gets whether this is an open range (no dates defined)
    /// </summary>
    public bool IsOpen => !HasStartDate && !HasEndDate;

    /// <summary>
    /// Gets the duration of the range if both dates are defined
    /// </summary>
    public TimeSpan? Duration => IsComplete ? EndDate!.Value - StartDate!.Value : null;

    /// <summary>
    /// Initializes a new instance of the DateTimeRange class
    /// </summary>
    public DateTimeRange()
    {
    }

    /// <summary>
    /// Initializes a new instance of the DateTimeRange class with specified dates
    /// </summary>
    /// <param name="startDate">Start date of the range</param>
    /// <param name="endDate">End date of the range</param>
    public DateTimeRange(DateTime? startDate, DateTime? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    /// <summary>
    /// Creates a date range for a specific day
    /// </summary>
    /// <param name="date">The date to create a range for</param>
    /// <returns>DateTimeRange covering the entire day</returns>
    public static DateTimeRange ForDay(DateTime date)
    {
        var startOfDay = date.Date;
        var endOfDay = startOfDay.AddDays(1).AddTicks(-1);
        return new DateTimeRange(startOfDay, endOfDay);
    }

    /// <summary>
    /// Creates a date range for the last N days
    /// </summary>
    /// <param name="days">Number of days to go back</param>
    /// <returns>DateTimeRange for the last N days</returns>
    public static DateTimeRange LastDays(int days)
    {
        var endDate = DateTime.UtcNow;
        var startDate = endDate.AddDays(-days);
        return new DateTimeRange(startDate, endDate);
    }

    /// <summary>
    /// Creates a date range for the current month
    /// </summary>
    /// <returns>DateTimeRange for the current month</returns>
    public static DateTimeRange CurrentMonth()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);
        return new DateTimeRange(startOfMonth, endOfMonth);
    }

    /// <summary>
    /// Checks if a given date falls within this range
    /// </summary>
    /// <param name="date">Date to check</param>
    /// <returns>True if the date is within the range</returns>
    public bool Contains(DateTime date)
    {
        if (HasStartDate && date < StartDate!.Value)
            return false;
        
        if (HasEndDate && date > EndDate!.Value)
            return false;
        
        return true;
    }

    /// <summary>
    /// Returns a string representation of the date range
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        if (IsOpen)
            return "Open Range";
        
        if (!HasStartDate)
            return $"Until {EndDate:yyyy-MM-dd}";
        
        if (!HasEndDate)
            return $"From {StartDate:yyyy-MM-dd}";
        
        return $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}";
    }
}
