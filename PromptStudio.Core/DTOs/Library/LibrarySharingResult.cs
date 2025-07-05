namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Comprehensive result data transfer object for library sharing operations across users, teams, and organizations.
/// Encapsulates the complete outcome of sharing requests including success metrics, failure analysis,
/// target-specific results, and detailed diagnostics for multi-target sharing scenarios.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <list type="bullet">
///   <item>Sharing Service - Primary sharing orchestration and result aggregation</item>
///   <item>Permission Service - Access control assignment and verification</item>
///   <item>Notification Service - Share completion alerts and failure notifications</item>
///   <item>Analytics Service - Sharing success rate tracking and usage analysis</item>
///   <item>Audit Service - Sharing activity logging and compliance documentation</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>JSON serializable for API responses and asynchronous processing</item>
///   <item>Contains collections of successful and failed sharing targets</item>
///   <item>Provides both aggregate metrics and detailed per-target results</item>
///   <item>Supports batch sharing analysis and partial success scenarios</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Bulk sharing operations with mixed success/failure outcomes</item>
///   <item>Share validation and error reporting for user interfaces</item>
///   <item>Retry logic implementation for failed sharing attempts</item>
///   <item>Sharing audit trails and compliance reporting</item>
///   <item>Performance analysis for sharing system optimization</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Large target collections may impact memory usage and serialization time</item>
///   <item>Detailed results enable granular error analysis but increase payload size</item>
///   <item>Consider pagination for very large sharing operations</item>
///   <item>Error message collection should be bounded for system stability</item>
/// </list>
/// </remarks>
public class LibrarySharingResult
{
    /// <summary>
    /// Indicates the overall success status of the library sharing operation.
    /// True when at least one target was successfully shared and no critical errors occurred.
    /// False when the sharing operation failed completely or encountered critical system errors.
    /// Service layers should check this before processing sharing results or sending notifications.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Collection of share targets that successfully received access to the library.
    /// Contains complete target information for confirmation notifications and audit trails.
    /// Empty collection when no targets were successfully shared or operation failed completely.
    /// Used for success confirmation and permission verification workflows.
    /// </summary>
    public List<ShareTarget> SharedTargets { get; set; } = new List<ShareTarget>();

    /// <summary>
    /// Collection of share targets that failed to receive access due to errors or restrictions.
    /// Contains target information for retry operations and failure analysis.
    /// Includes targets with permission conflicts, invalid identities, or system errors.
    /// Critical for troubleshooting sharing issues and implementing retry logic.
    /// </summary>
    public List<ShareTarget> FailedTargets { get; set; } = new List<ShareTarget>();

    /// <summary>
    /// Collection of error messages encountered during the sharing operation.
    /// Contains system-level errors, permission conflicts, and validation failures.
    /// Provides actionable information for troubleshooting and user feedback.
    /// Service layers should log these errors and provide appropriate user notifications.
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Detailed results for each sharing target including success status and granted permissions.
    /// Provides granular outcome information for complex sharing scenarios and audit requirements.
    /// Contains both successful and failed target results with specific error details.
    /// Used for detailed reporting, permission verification, and retry logic implementation.
    /// </summary>
    public List<ShareTargetResult> Results { get; set; } = new();

    /// <summary>
    /// Total count of targets that successfully received library access.
    /// Provides quick metrics for sharing operation assessment and reporting.
    /// Used for success rate calculation and performance monitoring.
    /// Should match the count of SharedTargets collection for consistency.
    /// </summary>
    public int SuccessfulShares { get; set; }

    /// <summary>
    /// Total count of targets that failed to receive library access.
    /// Provides quick metrics for error analysis and retry planning.
    /// Used for failure rate calculation and system health monitoring.
    /// Should match the count of FailedTargets collection for consistency.
    /// </summary>
    public int FailedShares { get; set; }

    /// <summary>
    /// Optional descriptive message providing additional context about the sharing operation outcome.
    /// Contains summary information, special conditions, or operational notes for user communication.
    /// Useful for providing rich feedback in user interfaces and notification systems.
    /// May include performance notes, partial success explanations, or next steps guidance.
    /// </summary>
    public string? Message { get; set; }
}

/// <summary>
/// Detailed result information for individual sharing target operations within library sharing workflows.
/// Encapsulates the outcome of sharing with a specific target including success status, error details,
/// and granted permission information for granular sharing analysis and audit requirements.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <list type="bullet">
///   <item>Sharing Service - Target-specific result aggregation and analysis</item>
///   <item>Permission Service - Permission grant verification and documentation</item>
///   <item>Error Handling Service - Target-specific error analysis and resolution</item>
///   <item>Audit Service - Detailed sharing activity logging and compliance tracking</item>
///   <item>Retry Service - Failed target identification and retry orchestration</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Contains complete target information for identification and verification</item>
///   <item>Boolean success indicator for immediate result assessment</item>
///   <item>Optional error message for failure diagnosis and troubleshooting</item>
///   <item>Permission collection for access control verification and audit</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Individual target result processing in batch sharing operations</item>
///   <item>Granular error analysis and retry logic implementation</item>
///   <item>Permission audit trails and compliance documentation</item>
///   <item>User notification generation for sharing status updates</item>
///   <item>Performance analysis for target-specific sharing bottlenecks</item>
/// </list>
/// </remarks>
public class ShareTargetResult
{
    /// <summary>
    /// Complete target information for the sharing operation including identity and type classification.
    /// References the specific target entity that was processed during the sharing operation.
    /// Used for target identification, error correlation, and audit trail maintenance.
    /// Contains all necessary information for retry operations and user communication.
    /// </summary>
    public ShareTarget Target { get; set; } = new();
    
    /// <summary>
    /// Indicates whether sharing with this specific target completed successfully.
    /// True when target received appropriate access permissions without errors.
    /// False when target sharing failed due to permissions, validation, or system issues.
    /// Used for retry logic and success rate calculations in batch operations.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Detailed error message for failed sharing attempts with this target.
    /// Contains actionable information for troubleshooting and retry operations.
    /// Includes permission errors, validation failures, and system-level issues.
    /// Null or empty when Success is true, populated with diagnostic details when false.
    /// </summary>
    public string? Error { get; set; }
    
    /// <summary>
    /// Collection of specific permissions granted to this target during successful sharing.
    /// Contains permission names or codes that define the target's access level to shared content.
    /// Used for permission verification, audit trails, and access control management.
    /// Empty collection for failed sharing attempts, populated for successful operations.
    /// </summary>
    public List<string> GrantedPermissions { get; set; } = new();
}
