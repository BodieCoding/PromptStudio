namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents a warning that occurred during flow execution.
/// Provides information about non-critical issues that don't prevent execution but may need attention.
/// </summary>
/// <remarks>
/// <para><strong>Warning Tracking:</strong></para>
/// <para>Captures non-critical issues during flow execution that don't stop processing
/// but may indicate potential problems, performance issues, or areas for optimization.
/// Essential for maintaining flow quality and identifying improvement opportunities.</para>
/// </remarks>
public class ExecutionWarning
{
    /// <summary>
    /// Gets or sets the unique identifier for this warning instance.
    /// Enables correlation across logs and monitoring systems.
    /// </summary>
    /// <value>
    /// A unique identifier for this warning occurrence.
    /// </value>
    public Guid WarningId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the warning code or identifier.
    /// Provides categorization for warning handling and monitoring.
    /// </summary>
    /// <value>
    /// A string code identifying the type of warning (e.g., "SLOW_RESPONSE", "DEPRECATED_API").
    /// </value>
    public string? WarningCode { get; set; }

    /// <summary>
    /// Gets or sets the human-readable warning message.
    /// Provides clear description of the potential issue.
    /// </summary>
    /// <value>
    /// A descriptive warning message for users and developers.
    /// </value>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional warning details and context.
    /// Contains supplementary information about the warning condition.
    /// </summary>
    /// <value>
    /// Additional details about the warning and its potential impact.
    /// </value>
    public string? Details { get; set; }

    /// <summary>
    /// Gets or sets the severity level of the warning.
    /// Indicates the importance and urgency of addressing the warning.
    /// </summary>
    /// <value>
    /// The warning severity level.
    /// </value>
    public WarningSeverity Severity { get; set; } = WarningSeverity.Medium;

    /// <summary>
    /// Gets or sets the identifier of the node where the warning occurred.
    /// Provides context for warning location within the flow.
    /// </summary>
    /// <value>
    /// The unique identifier of the flow node that generated the warning.
    /// </value>
    public Guid? NodeId { get; set; }

    /// <summary>
    /// Gets or sets the name of the node where the warning occurred.
    /// Provides human-readable context for warning location.
    /// </summary>
    /// <value>
    /// The name of the flow node that generated the warning.
    /// </value>
    public string? NodeName { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the warning occurred.
    /// Enables temporal analysis of warning patterns.
    /// </summary>
    /// <value>
    /// The UTC timestamp when this warning was generated.
    /// </value>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the source component or system that generated the warning.
    /// Provides context for warning origin and ownership.
    /// </summary>
    /// <value>
    /// The name or identifier of the component that generated the warning.
    /// </value>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets additional metadata about the warning.
    /// Provides extensible storage for warning-specific information.
    /// </summary>
    /// <value>
    /// A dictionary containing additional warning context and metadata.
    /// </value>
    public Dictionary<string, object> Metadata { get; set; } = [];

    /// <summary>
    /// Gets or sets the recommended action to address this warning.
    /// Provides guidance on how to resolve or mitigate the warning condition.
    /// </summary>
    /// <value>
    /// Suggested action or remediation steps for this warning.
    /// </value>
    public string? RecommendedAction { get; set; }

    /// <summary>
    /// Gets or sets the category of the warning.
    /// Groups warnings by type for filtering and analysis.
    /// </summary>
    /// <value>
    /// The warning category for organizational purposes.
    /// </value>
    public WarningCategory Category { get; set; } = WarningCategory.General;

    /// <summary>
    /// Initializes a new instance of the ExecutionWarning class with default values.
    /// </summary>
    public ExecutionWarning()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ExecutionWarning class with a message.
    /// </summary>
    /// <param name="message">The warning message</param>
    public ExecutionWarning(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Initializes a new instance of the ExecutionWarning class with message and severity.
    /// </summary>
    /// <param name="message">The warning message</param>
    /// <param name="severity">The warning severity level</param>
    public ExecutionWarning(string message, WarningSeverity severity)
    {
        Message = message;
        Severity = severity;
    }

    /// <summary>
    /// Returns a string representation of the execution warning.
    /// </summary>
    /// <returns>String representation including warning message and context</returns>
    public override string ToString()
    {
        var nodeContext = !string.IsNullOrEmpty(NodeName) ? $" in {NodeName}" : "";
        var codeContext = !string.IsNullOrEmpty(WarningCode) ? $" [{WarningCode}]" : "";
        return $"ExecutionWarning{codeContext}: {Message}{nodeContext}";
    }
}

/// <summary>
/// Enumeration of warning severity levels.
/// </summary>
public enum WarningSeverity
{
    /// <summary>
    /// Low severity warning that can be safely ignored in most cases.
    /// </summary>
    Low = 0,

    /// <summary>
    /// Medium severity warning that should be reviewed when convenient.
    /// </summary>
    Medium = 1,

    /// <summary>
    /// High severity warning that should be addressed soon.
    /// </summary>
    High = 2,

    /// <summary>
    /// Critical warning that may indicate serious issues and should be addressed immediately.
    /// </summary>
    Critical = 3
}

/// <summary>
/// Enumeration of warning categories for organization and filtering.
/// </summary>
public enum WarningCategory
{
    /// <summary>
    /// General warnings that don't fit other categories.
    /// </summary>
    General = 0,

    /// <summary>
    /// Performance-related warnings about slow operations or resource usage.
    /// </summary>
    Performance = 1,

    /// <summary>
    /// Security-related warnings about potential vulnerabilities.
    /// </summary>
    Security = 2,

    /// <summary>
    /// Data validation warnings about questionable input or output.
    /// </summary>
    Validation = 3,

    /// <summary>
    /// Configuration warnings about settings or setup issues.
    /// </summary>
    Configuration = 4,

    /// <summary>
    /// Deprecation warnings about outdated features or APIs.
    /// </summary>
    Deprecation = 5,

    /// <summary>
    /// Compatibility warnings about version or platform issues.
    /// </summary>
    Compatibility = 6,

    /// <summary>
    /// Resource usage warnings about memory, CPU, or network consumption.
    /// </summary>
    Resource = 7
}
