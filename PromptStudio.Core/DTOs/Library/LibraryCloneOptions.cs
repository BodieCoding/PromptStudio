namespace PromptStudio.Core.DTOs.Library;

/// <summary>
/// Comprehensive configuration options for library cloning operations in the Prompt Studio ecosystem.
/// Defines granular control over what content and metadata is included in cloned libraries,
/// supporting various cloning scenarios from simple duplication to complex migration workflows
/// with selective content preservation and customization capabilities.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <list type="bullet">
///   <item>Library Management Service - Clone orchestration and content duplication</item>
///   <item>Template Service - Template cloning and name mapping operations</item>
///   <item>Permission Service - Permission inheritance and isolation control</item>
///   <item>Analytics Service - Usage statistics management and reset operations</item>
///   <item>Metadata Service - Custom metadata application and preservation</item>
/// </list>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <list type="bullet">
///   <item>Boolean flags enable/disable specific cloning behaviors and content types</item>
///   <item>String fields support custom descriptions and naming strategies</item>
///   <item>Dictionary collections enable flexible metadata and mapping configurations</item>
///   <item>JSON serializable for API transport and configuration persistence</item>
/// </list>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
///   <item>Environment promotion (development to staging/production)</item>
///   <item>Template library forking for team-specific customizations</item>
///   <item>Backup and disaster recovery with selective restoration</item>
///   <item>Multi-tenant deployment with isolated library instances</item>
///   <item>Content versioning and experimental branching workflows</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <list type="bullet">
///   <item>Execution history cloning significantly increases operation time and storage</item>
///   <item>Analytics data cloning may impact performance reporting accuracy</item>
///   <item>Template name mapping requires additional validation and conflict resolution</item>
///   <item>Custom metadata size should be monitored for network efficiency</item>
/// </list>
/// </remarks>
public class LibraryCloneOptions
{
    /// <summary>
    /// Determines whether permission settings are cloned from the source library to the new instance.
    /// When true, preserves user access rights, team assignments, and role-based permissions.
    /// When false, cloned library uses default permissions, providing clean access control setup.
    /// Important for security isolation when cloning across different organizational boundaries.
    /// </summary>
    public bool ClonePermissions { get; set; } = false;

    /// <summary>
    /// Controls whether individual templates are included in the cloned library.
    /// When true, duplicates all template content, configurations, and metadata.
    /// When false, creates empty library structure without template content.
    /// Core functionality for most cloning scenarios where content preservation is required.
    /// </summary>
    public bool CloneTemplates { get; set; } = true;

    /// <summary>
    /// Specifies whether historical execution data and performance metrics are cloned.
    /// When true, preserves usage patterns, execution logs, and performance analytics.
    /// When false, cloned library starts with clean execution history for fresh analytics.
    /// Significant impact on clone operation time and storage requirements.
    /// </summary>
    public bool CloneExecutionHistory { get; set; } = false;

    /// <summary>
    /// Controls whether analytics data and usage statistics are included in the clone.
    /// When true, preserves performance metrics, usage trends, and analytical insights.
    /// When false, enables clean analytics baseline for new library environment.
    /// May affect performance reporting and capacity planning accuracy.
    /// </summary>
    public bool CloneAnalytics { get; set; } = false;

    /// <summary>
    /// Determines whether usage statistics are reset to zero in the cloned library.
    /// When true, clears usage counters, access logs, and performance metrics for fresh start.
    /// When false, preserves original usage data for continuity and trend analysis.
    /// Useful for creating clean development copies from production libraries.
    /// </summary>
    public bool ResetUsageStatistics { get; set; } = true;

    /// <summary>
    /// Optional custom description to replace the original library description in the clone.
    /// Provides context-specific information about the cloned library's purpose and scope.
    /// Useful for distinguishing clones, documenting clone purpose, and organizational clarity.
    /// Null value preserves original description with appropriate clone identification.
    /// </summary>
    public string? NewDescription { get; set; }

    /// <summary>
    /// Dictionary mapping original template names to new names in the cloned library.
    /// Enables systematic template renaming during clone operations for namespace management.
    /// Keys represent original names, values represent desired names in clone.
    /// Useful for avoiding naming conflicts and implementing naming conventions.
    /// </summary>
    public Dictionary<string, string>? TemplateNameMappings { get; set; }

    /// <summary>
    /// Additional metadata to associate with the cloned library for categorization and tracking.
    /// Supports custom tagging, organizational categorization, and operational annotations.
    /// Commonly used for clone source tracking, environment identification, and governance.
    /// Key-value pairs are preserved and accessible through library management APIs.
    /// </summary>
    public Dictionary<string, object>? CustomMetadata { get; set; }

    /// <summary>
    /// Extended configuration options for specialized cloning scenarios and custom integrations.
    /// Provides extensibility for advanced cloning behaviors and third-party tool integration.
    /// Values are passed to clone service extensions and custom processing handlers.
    /// Enables sophisticated cloning workflows without modifying core cloning logic.
    /// </summary>
    public Dictionary<string, object> CustomOptions { get; set; } = [];
}
