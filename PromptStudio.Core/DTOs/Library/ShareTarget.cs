namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Data transfer object representing a target entity for library and template sharing operations.
/// Defines the recipient specification for content sharing, including identity information,
/// target classification, and optional communication context for sharing notifications.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <list type="bullet">
///   <item>Sharing Service - Target resolution and permission assignment</item>
///   <item>User Management Service - Target identity validation and lookup</item>
///   <item>Notification Service - Sharing notification delivery and messaging</item>
///   <item>Permission Service - Access control and authorization management</item>
///   <item>Analytics Service - Sharing pattern analysis and usage tracking</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>String-based target identification for flexible identity systems</item>
///   <item>Type classification supports users, teams, groups, and custom entities</item>
///   <item>Optional message field enables personalized sharing communication</item>
///   <item>Serializable for API transport and message queue processing</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Individual user sharing with personalized access</item>
///   <item>Team-based sharing for collaborative workflows</item>
///   <item>Group sharing for organizational content distribution</item>
///   <item>Batch sharing operations across multiple targets</item>
///   <item>External sharing with partner organizations</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Target ID validation may require external service calls</item>
///   <item>Type-specific processing affects sharing performance</item>
///   <item>Message content impacts notification delivery times</item>
///   <item>Consider caching target information for repeated operations</item>
/// </list>
/// </remarks>
public class ShareTarget
{
    /// <summary>
    /// Unique identifier of the target entity to receive shared content access.
    /// May be user ID, email address, team identifier, or group name depending on system configuration.
    /// Must be resolvable by the target management service for successful sharing operation.
    /// Used for permission assignment and access control verification.
    /// </summary>
    public string TargetId { get; set; } = string.Empty;

    /// <summary>
    /// Classification of the sharing target entity type for appropriate processing logic.
    /// Supported values include "User", "Team", "Group", and custom organization-specific types.
    /// Determines permission model, notification mechanism, and access scope for shared content.
    /// Default value "User" assumes individual user sharing for simple use cases.
    /// </summary>
    public string TargetType { get; set; } = "User"; // Default to User

    /// <summary>
    /// Optional personalized message to include with the sharing notification.
    /// Provides context about the shared content, usage instructions, or collaboration purpose.
    /// Delivered through the notification service to enhance user experience and adoption.
    /// Useful for building team relationships and explaining content relevance.
    /// </summary>
    public string? Message { get; set; }
}
