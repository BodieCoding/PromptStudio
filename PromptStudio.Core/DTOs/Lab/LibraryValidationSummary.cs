namespace PromptStudio.Core.DTOs.Lab;

/// <summary>
/// Represents a library validation summary within a lab environment, providing focused validation results and metrics.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Library-focused validation summary DTO used by lab validation services and library management systems.
/// Provides targeted library validation feedback within broader lab validation workflows and quality assurance processes.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Library-specific validation data with template-level metrics and issue categorization.
/// Designed for integration with library management systems and lab-wide validation reporting.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Library-specific validation feedback within lab contexts</item>
/// <item>Template quality assessment and error localization</item>
/// <item>Library compliance checking and governance reporting</item>
/// <item>Quality metrics aggregation for library management</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight library summary optimized for inclusion in lab validation collections.
/// Template count metrics provide quick quality overview without detailed template enumeration.
/// Error and warning lists should be bounded for effective presentation.</para>
/// </remarks>
public class LibraryValidationSummary
{
    /// <summary>
    /// Gets or sets the unique identifier of the validated library.
    /// </summary>
    /// <value>A GUID that uniquely identifies the library within the lab environment.</value>
    public Guid LibraryId { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the validated library.
    /// </summary>
    /// <value>A string providing a descriptive name for the library for display and identification purposes.</value>
    public string LibraryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the library passes all validation checks.
    /// </summary>
    /// <value>True if the library and all its components are valid; otherwise, false.</value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the collection of library-specific validation errors.
    /// </summary>
    /// <value>A list of critical errors specific to this library that must be resolved.</value>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of library-specific validation warnings.
    /// </summary>
    /// <value>A list of non-critical issues specific to this library suggesting improvements.</value>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Gets or sets the count of invalid templates within the library.
    /// </summary>
    /// <value>A non-negative integer representing the number of templates that failed validation.</value>
    public int InvalidTemplateCount { get; set; }
}
